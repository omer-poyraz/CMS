using Entities.Models;
using Microsoft.EntityFrameworkCore;
using Repositories.Contracts;
using Entities.RequestFeature;
using Entities.RequestFeature.Page;

namespace Repositories.EFCore
{
    public class PageRepository : RepositoryBase<Page>, IPageRepository
    {

        public PageRepository(RepositoryContext context) : base(context) { }

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
            var pages = await FindAll(trackChanges)
                .OrderByDescending(s => s.ID)
                .Include(p => p.Translations!.Where(t => t.Lang!.Equals(pageParameters.Lang)))
                .ToListAsync();

            return PagedList<Page>.ToPagedList(pages, pageParameters.PageNumber, pageParameters.PageSize);
        }

        public async Task<Page?> GetPageByIdAsync(int id, bool? trackChanges) =>
            await FindByCondition(s => s.ID.Equals(id), trackChanges)
                .Include(p => p.Translations)
                .Include(p => p.Sections)
                .SingleOrDefaultAsync();

        public async Task<Page?> GetPageBySlugAsync(string slug, string lang, bool? trackChanges) =>
            await FindByCondition(s => s.Translations!.Any(t => t.Slug!.Equals(slug) && t.Lang!.Equals(lang)), trackChanges)
                .Include(p => p.Translations!.Where(t => t.Slug!.Equals(slug) && t.Lang!.Equals(lang)))
                .Include(p => p.Sections)!
                    .ThenInclude(s => s.Fields!.Where(f => f.Lang!.Equals(lang)))
                .Include(p => p.Sections)!
                    .ThenInclude(s => s.Items)!
                        .ThenInclude(i => i.Fields!.Where(f => f.Lang!.Equals(lang)))
                .SingleOrDefaultAsync();

        public Page UpdatePage(Page page)
        {
            Update(page);
            return page;
        }
    }
}
