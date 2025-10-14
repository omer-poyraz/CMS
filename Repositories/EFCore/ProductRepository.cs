using System.Text.Json;
using Microsoft.EntityFrameworkCore;
using Entities.Models;
using Repositories.Contracts;
using Entities.RequestFeature;
using Entities.RequestFeature.Product;

namespace Repositories.EFCore
{
    public class ProductRepository : RepositoryBase<Product>, IProductRepository
    {
        public ProductRepository(RepositoryContext context) : base(context) { }

        public Product CreateProduct(Product product)
        {
            Create(product);
            return product;
        }

        public Product DeleteProduct(Product product)
        {
            Delete(product);
            return product;
        }

        public async Task<PagedList<Product>> GetAllProductsAsync(ProductParameters productParameters, bool? trackChanges)
        {
            List<Product> items;
            int skip = (productParameters.PageNumber - 1) * productParameters.PageSize;
            int take = productParameters.PageSize;
            int totalCount;
            if (!string.IsNullOrWhiteSpace(productParameters.SearchTerm))
            {
                string countSql = "SELECT COUNT(*) FROM \"Products\" WHERE (CAST(\"Files\" AS TEXT) ILIKE {0} OR CAST(\"Title\" AS TEXT) ILIKE {0} OR CAST(\"Slug\" AS TEXT) ILIKE {0} OR CAST(\"Description\" AS TEXT) ILIKE {0} OR CAST(\"Content\" AS TEXT) ILIKE {0})";
                var countResult = await _context.Database.ExecuteSqlRawAsync(countSql, $"%{productParameters.SearchTerm}%");
                totalCount = countResult;
                string sql = $"SELECT * FROM \"Products\" WHERE (CAST(\"Files\" AS TEXT) ILIKE {{0}} OR CAST(\"Title\" AS TEXT) ILIKE {{0}} OR CAST(\"Slug\" AS TEXT) ILIKE {{0}} OR CAST(\"Description\" AS TEXT) ILIKE {{0}} OR CAST(\"Content\" AS TEXT) ILIKE {{0}}) ORDER BY \"ID\" LIMIT {take} OFFSET {skip}";
                items = await _context.Products.FromSqlRaw(sql, $"%{productParameters.SearchTerm}%").ToListAsync();
            }
            else
            {
                totalCount = await FindAll(trackChanges).CountAsync();
                items = await FindAll(trackChanges)
                    .OrderBy(s => s.ID)
                    .Skip(skip)
                    .Take(take)
                    .ToListAsync();
            }
            return new PagedList<Product>(items, totalCount, productParameters.PageSize, productParameters.PageNumber);
        }

        public async Task<Product> GetProductByIdAsync(int id, string lang, bool? trackChanges)
        {
            return await FindByCondition(s => s.ID.Equals(id), trackChanges).SingleOrDefaultAsync();
        }

        public async Task<Product> GetProductBySlugAsync(string slug, string lang, bool? trackChanges)
        {
            var contentCandidates = await _context.Contents
                .Where(c => c.Type != null && c.Type.ToLower() == "slug")
                .Select(c => new { c.ID, c.Code, c.Value, c.Type })
                .ToListAsync();

            int? contentId = null;
            string langNorm = lang.ToLower();
            foreach (var c in contentCandidates)
            {
                if (c.Code == null || c.Code.ToLower() != langNorm) continue;
                if (c.Value == null) continue;
                var root = c.Value.RootElement;
                if (root.ValueKind == JsonValueKind.String && root.GetString()?.ToLower() == slug.ToLower())
                {
                    contentId = c.ID;
                    break;
                }
                if (root.ValueKind == JsonValueKind.Array && root.EnumerateArray().Any(x => x.ValueKind == JsonValueKind.String && x.GetString()?.ToLower() == slug.ToLower()))
                {
                    contentId = c.ID;
                    break;
                }
            }

            if (contentId == null)
                return null!;

            var productCandidates = await FindByCondition(p => p.Slug != null, trackChanges).Select(p => new { p.ID, p.Slug, p.Content }).ToListAsync();

            int? foundProductId = null;
            foreach (var p in productCandidates)
            {
                var slugRoot = p.Slug.RootElement;
                var slugIds = new List<int>();
                if (slugRoot.ValueKind == JsonValueKind.Object)
                {
                    foreach (var prop in slugRoot.EnumerateObject())
                    {
                        if (prop.Value.ValueKind == JsonValueKind.Number && prop.Value.TryGetInt32(out int id))
                            slugIds.Add(id);
                        else if (prop.Value.ValueKind == JsonValueKind.Array)
                            slugIds.AddRange(prop.Value.EnumerateArray().Where(x => x.ValueKind == JsonValueKind.Number && x.TryGetInt32(out _)).Select(x => x.GetInt32()));
                    }
                }
                else if (slugRoot.ValueKind == JsonValueKind.Array)
                {
                    foreach (var el in slugRoot.EnumerateArray())
                    {
                        if (el.ValueKind == JsonValueKind.Number && el.TryGetInt32(out int id))
                            slugIds.Add(id);
                    }
                }
                if (slugIds.Contains(contentId.Value))
                {
                    foundProductId = p.ID;
                    break;
                }
            }

            if (foundProductId == null)
                return null!;

            var foundProduct = await FindByCondition(p => p.ID == foundProductId.Value, trackChanges).SingleOrDefaultAsync();
            if (foundProduct == null)
                return null!;

            if (foundProduct.Slug != null)
            {
                var slugRoot = foundProduct.Slug.RootElement;
                var slugIds = new List<int>();
                if (slugRoot.ValueKind == JsonValueKind.Object)
                {
                    foreach (var prop in slugRoot.EnumerateObject())
                    {
                        if (prop.Value.ValueKind == JsonValueKind.Number && prop.Value.TryGetInt32(out int id))
                            slugIds.Add(id);
                        else if (prop.Value.ValueKind == JsonValueKind.Array)
                            slugIds.AddRange(prop.Value.EnumerateArray().Where(x => x.ValueKind == JsonValueKind.Number && x.TryGetInt32(out _)).Select(x => x.GetInt32()));
                    }
                }
                else if (slugRoot.ValueKind == JsonValueKind.Array)
                {
                    foreach (var el in slugRoot.EnumerateArray())
                    {
                        if (el.ValueKind == JsonValueKind.Number && el.TryGetInt32(out int id))
                            slugIds.Add(id);
                    }
                }
                if (slugIds.Any())
                {
                    var slugContents = await _context.Contents
                        .Where(c => slugIds.Contains(c.ID) && c.Code != null && c.Code.ToLower() == langNorm)
                        .Select(c => new { c.Value, c.Type })
                        .ToListAsync();
                    var ordered = slugContents.OrderBy(x => slugIds.IndexOf(x.Value != null ? slugIds.FirstOrDefault() : 0)).Select(x => new { x.Value, x.Type }).ToList();
                    // Her zaman array dön
                    foundProduct.Slug = JsonDocument.Parse(System.Text.Json.JsonSerializer.Serialize(ordered));
                }
            }

            if (foundProduct.Content != null)
            {
                var contentRoot = foundProduct.Content.RootElement;
                var contentIds = new List<int>();
                if (contentRoot.ValueKind == JsonValueKind.Number && contentRoot.TryGetInt32(out int contentIdVal))
                {
                    contentIds.Add(contentIdVal);
                }
                else if (contentRoot.ValueKind == JsonValueKind.Array)
                {
                    contentIds.AddRange(contentRoot.EnumerateArray().Where(x => x.ValueKind == JsonValueKind.Number && x.TryGetInt32(out _)).Select(x => x.GetInt32()));
                }
                if (contentIds.Any())
                {
                    var contentObjs = await _context.Contents
                        .Where(c => contentIds.Contains(c.ID) && c.Code != null && c.Code.ToLower() == langNorm)
                        .Select(c => new { c.Value, c.Type })
                        .ToListAsync();
                    var ordered = contentObjs.OrderBy(x => contentIds.IndexOf(x.Value != null ? contentIds.FirstOrDefault() : 0)).Select(x => new { x.Value, x.Type }).ToList();
                    // Her zaman array dön
                    foundProduct.Content = JsonDocument.Parse(System.Text.Json.JsonSerializer.Serialize(ordered));
                }
            }

            return foundProduct!;
        }

        public async Task<Product> SortProductAsync(int id, int sort, bool? trackChanges)
        {
            var product = await FindByCondition(s => s.ID.Equals(id), trackChanges)
                .SingleOrDefaultAsync();

            if (product == null)
                return null!;

            var existingProductAtSort = await FindByCondition(
                    s => s.Sort.Equals(sort),
                    trackChanges
                )
                .SingleOrDefaultAsync();

            if (existingProductAtSort != null)
            {
                var tempSort = product.Sort;
                product.Sort = sort;
                existingProductAtSort.Sort = tempSort;

                Update(existingProductAtSort);
            }
            else
            {
                product.Sort = sort;
            }

            Update(product);
            return product;
        }

        public Product UpdateProduct(Product product)
        {
            Update(product);
            return product;
        }
    }
}
