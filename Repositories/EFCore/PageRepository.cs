using Entities.Models;
using Microsoft.EntityFrameworkCore;
using Repositories.Contracts;
using Entities.RequestFeature;
using Entities.RequestFeature.Page;

namespace Repositories.EFCore
{
    public class PageRepository : RepositoryBase<Page>, IPageRepository
    {
        private readonly Microsoft.AspNetCore.Http.IHttpContextAccessor _httpContextAccessor;
        static HashSet<string> uniqueVisitors = new HashSet<string>();

        public PageRepository(RepositoryContext context, Microsoft.AspNetCore.Http.IHttpContextAccessor httpContextAccessor) : base(context)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        public Page CreatePage(Page page)
        {
            Create(page);
            return page;
        }

        public Page DeletePage(Page page)
        {
            Delete(page);
            return page;
        }

        public async Task<PagedList<Page>> GetAllPagesAsync(PageParameters pageParameters, bool? trackChanges)
        {
            List<Page> items;
            int skip = (pageParameters.PageNumber - 1) * pageParameters.PageSize;
            int take = pageParameters.PageSize;
            int totalCount;
            if (!string.IsNullOrWhiteSpace(pageParameters.SearchTerm))
            {
                var searchTermLower = pageParameters.SearchTerm.ToLower();
                var allContents = await _context.Contents.ToListAsync();
                var matchedContentIds = allContents
                    .Where(c => (c.Value != null && c.Value.RootElement.ToString().ToLower().Contains(searchTermLower))
                             || (c.Type != null && c.Type.ToLower().Contains(searchTermLower)))
                    .Select(c => c.ID)
                    .ToList();

                if (matchedContentIds.Count == 0)
                {
                    return new PagedList<Page>(new List<Page>(), 0, pageParameters.PageSize, pageParameters.PageNumber);
                }

                var idList = string.Join(",", matchedContentIds);
                string countSql = $"SELECT COUNT(*) FROM \"Pages\" WHERE (EXISTS (SELECT 1 FROM jsonb_array_elements(CAST(\"Slug\" AS jsonb)) AS elem WHERE (elem)::int IN ({idList})) OR EXISTS (SELECT 1 FROM jsonb_array_elements(CAST(\"Content\" AS jsonb)) AS elem WHERE (elem)::int IN ({idList})))";
                int count = 0;
                using (var command = _context.Database.GetDbConnection().CreateCommand())
                {
                    command.CommandText = countSql;
                    _context.Database.OpenConnection();
                    var result = await command.ExecuteScalarAsync();
                    int.TryParse(result?.ToString(), out count);
                    _context.Database.CloseConnection();
                }
                totalCount = count;

                string sql = $"SELECT * FROM \"Pages\" WHERE (EXISTS (SELECT 1 FROM jsonb_array_elements(CAST(\"Slug\" AS jsonb)) AS elem WHERE (elem)::int IN ({idList})) OR EXISTS (SELECT 1 FROM jsonb_array_elements(CAST(\"Content\" AS jsonb)) AS elem WHERE (elem)::int IN ({idList}))) ORDER BY \"ID\" LIMIT {take} OFFSET {skip}";
                items = await _context.Pages.FromSqlRaw(sql).ToListAsync();
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
            return new PagedList<Page>(items, totalCount, pageParameters.PageSize, pageParameters.PageNumber);
        }

        public async Task<Page> GetPageByIdAsync(int id, bool? trackChanges)
        {
            return await FindByCondition(s => s.ID.Equals(id), trackChanges).SingleOrDefaultAsync();
        }

        public async Task<Page> GetPageBySlugAsync(string slug, string lang, bool? trackChanges)
        {
            string? ip = _httpContextAccessor?.HttpContext?.Connection?.RemoteIpAddress?.ToString();

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
                if (root.ValueKind == System.Text.Json.JsonValueKind.String && root.GetString()?.ToLower() == slug.ToLower())
                {
                    contentId = c.ID;
                    break;
                }
                if (root.ValueKind == System.Text.Json.JsonValueKind.Array && root.EnumerateArray().Any(x => x.ValueKind == System.Text.Json.JsonValueKind.String && x.GetString()?.ToLower() == slug.ToLower()))
                {
                    contentId = c.ID;
                    break;
                }
            }

            if (contentId == null)
                return null!;

            var pageCandidates = await FindByCondition(p => p.Slug != null, trackChanges).Select(p => new { p.ID, p.Slug, p.Content }).ToListAsync();

            int? foundPageId = null;
            foreach (var p in pageCandidates)
            {
                var slugRoot = p.Slug.RootElement;
                var slugIds = new List<int>();
                if (slugRoot.ValueKind == System.Text.Json.JsonValueKind.Object)
                {
                    foreach (var prop in slugRoot.EnumerateObject())
                    {
                        if (prop.Value.ValueKind == System.Text.Json.JsonValueKind.Number && prop.Value.TryGetInt32(out int id))
                            slugIds.Add(id);
                        else if (prop.Value.ValueKind == System.Text.Json.JsonValueKind.Array)
                            slugIds.AddRange(prop.Value.EnumerateArray().Where(x => x.ValueKind == System.Text.Json.JsonValueKind.Number && x.TryGetInt32(out _)).Select(x => x.GetInt32()));
                    }
                }
                else if (slugRoot.ValueKind == System.Text.Json.JsonValueKind.Array)
                {
                    foreach (var el in slugRoot.EnumerateArray())
                    {
                        if (el.ValueKind == System.Text.Json.JsonValueKind.Number && el.TryGetInt32(out int id))
                            slugIds.Add(id);
                    }
                }
                if (slugIds.Contains(contentId.Value))
                {
                    foundPageId = p.ID;
                    break;
                }
            }

            if (foundPageId == null)
                return null!;

            var foundPage = await FindByCondition(p => p.ID == foundPageId.Value, trackChanges).SingleOrDefaultAsync();
            if (foundPage == null)
                return null!;

            if (!string.IsNullOrEmpty(ip) && !uniqueVisitors.Contains($"{foundPage.ID}:{ip}"))
            {
                foundPage.View = (foundPage.View ?? 0) + 1;
                uniqueVisitors.Add($"{foundPage.ID}:{ip}");
                await _context.SaveChangesAsync();
            }

            if (foundPage.Slug != null)
            {
                var slugRoot = foundPage.Slug.RootElement;
                var slugIds = new List<int>();
                if (slugRoot.ValueKind == System.Text.Json.JsonValueKind.Object)
                {
                    foreach (var prop in slugRoot.EnumerateObject())
                    {
                        if (prop.Value.ValueKind == System.Text.Json.JsonValueKind.Number && prop.Value.TryGetInt32(out int id))
                            slugIds.Add(id);
                        else if (prop.Value.ValueKind == System.Text.Json.JsonValueKind.Array)
                            slugIds.AddRange(prop.Value.EnumerateArray().Where(x => x.ValueKind == System.Text.Json.JsonValueKind.Number && x.TryGetInt32(out _)).Select(x => x.GetInt32()));
                    }
                }
                else if (slugRoot.ValueKind == System.Text.Json.JsonValueKind.Array)
                {
                    foreach (var el in slugRoot.EnumerateArray())
                    {
                        if (el.ValueKind == System.Text.Json.JsonValueKind.Number && el.TryGetInt32(out int id))
                            slugIds.Add(id);
                    }
                }
                if (slugIds.Any())
                {
                    var slugContents = await _context.Contents
                        .Where(c => slugIds.Contains(c.ID) && c.Code == lang)
                        .Select(c => new { c.ID, c.Value, c.Type })
                        .ToListAsync();
                    var ordered = slugIds
                        .Select(id => slugContents.FirstOrDefault(x => x.ID == id))
                        .Where(x => x != null)
                        .Select(x => new { x.Value, x.Type })
                        .ToList();
                    foundPage.Slug = System.Text.Json.JsonDocument.Parse(System.Text.Json.JsonSerializer.Serialize(ordered));
                }
            }

            if (foundPage.Content != null)
            {
                var contentRoot = foundPage.Content.RootElement;
                var contentIds = new List<int>();
                if (contentRoot.ValueKind == System.Text.Json.JsonValueKind.Number && contentRoot.TryGetInt32(out int contentIdVal))
                {
                    contentIds.Add(contentIdVal);
                }
                else if (contentRoot.ValueKind == System.Text.Json.JsonValueKind.Array)
                {
                    contentIds.AddRange(contentRoot.EnumerateArray().Where(x => x.ValueKind == System.Text.Json.JsonValueKind.Number && x.TryGetInt32(out _)).Select(x => x.GetInt32()));
                }
                if (contentIds.Any())
                {
                    var contentObjs = await _context.Contents
                        .Where(c => contentIds.Contains(c.ID) && c.Code == lang)
                        .ToListAsync();
                    var allProducts = await _context.Products.ToListAsync();
                    // Her Content için Products listesini doldur
                    foreach (var content in contentObjs)
                    {
                        content.Products = allProducts.Where(p => p.ContentID != null && p.ContentID.Contains(content.ID)).ToList();
                    }
                    var ordered = contentIds
                        .Select(id => contentObjs.FirstOrDefault(x => x.ID == id))
                        .Where(x => x != null)
                        .Select(x => new { x.Value, x.Type, products = x.Products })
                        .ToList();
                    foundPage.Content = System.Text.Json.JsonDocument.Parse(System.Text.Json.JsonSerializer.Serialize(ordered));
                }
            }

            return foundPage!;
        }

        public Page UpdatePage(Page page)
        {
            Update(page);
            return page;
        }
    }
}
