
using Entities.Models;
using Entities.RequestFeature;
using Entities.RequestFeature.Content;
using Microsoft.EntityFrameworkCore;
using Repositories.Contracts;

namespace Repositories.EFCore
{
    public class ContentRepository : RepositoryBase<Content>, IContentRepository
    {
        public ContentRepository(RepositoryContext context) : base(context) { }

        public async Task<IEnumerable<Content>> FindByIdsAsync(IEnumerable<int> ids)
        {
            return await _context.Contents.Where(c => ids.Contains(c.ID)).ToListAsync();
        }

        public Content CreateContent(Content content)
        {
            Create(content);
            return content;
        }

        public Content DeleteContent(Content content)
        {
            Delete(content);
            return content;
        }

        public async Task<PagedList<Content>> GetAllContentsAsync(ContentParameters contentParameters, bool? trackChanges)
        {
            List<Content> items;
            int skip = (contentParameters.PageNumber - 1) * contentParameters.PageSize;
            int take = contentParameters.PageSize;
            int totalCount;
            if (!string.IsNullOrWhiteSpace(contentParameters.SearchTerm))
            {
                string countSql = "SELECT COUNT(*) FROM \"Contents\" WHERE (CAST(\"Value\" AS TEXT) ILIKE {0} OR \"Type\" ILIKE {0})";
                totalCount = await _context.Database.ExecuteSqlRawAsync(countSql, $"%{contentParameters.SearchTerm}%");
                string sql = $"SELECT * FROM \"Contents\" WHERE (CAST(\"Value\" AS TEXT) ILIKE {{0}} OR \"Type\" ILIKE {{0}}) ORDER BY \"ID\" LIMIT {take} OFFSET {skip}";
                items = await _context.Contents.FromSqlRaw(sql, $"%{contentParameters.SearchTerm}%").ToListAsync();
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
            return new PagedList<Content>(items, totalCount, contentParameters.PageSize, contentParameters.PageNumber);
        }

        public async Task<Content> GetContentByIdAsync(int id, bool? trackChanges) =>
            await FindByCondition(s => s.ID.Equals(id), trackChanges).SingleOrDefaultAsync();

        public async Task<Content> GetContentAsync(bool? trackChanges) =>
            await FindByCondition(s => s.ID.Equals(1), trackChanges).SingleOrDefaultAsync();

        public Content UpdateContent(Content content)
        {
            Update(content);
            return content;
        }
    }
}
