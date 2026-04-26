using Entities.Models;
using Microsoft.EntityFrameworkCore;
using Repositories.Contracts;

namespace Repositories.EFCore
{
    public class PageSectionRepository : RepositoryBase<PageSection>, IPageSectionRepository
    {

        public PageSectionRepository(RepositoryContext context) : base(context) { }

        public PageSection CreatePageSection(PageSection pageSection)
        {
            Create(pageSection);
            return pageSection;
        }

        public PageSection DeletePageSection(PageSection pageSection)
        {
            Delete(pageSection);
            return pageSection;
        }

        public async Task<IEnumerable<PageSection>> GetAllPageSectionsAsync(bool? trackChanges)
        {
            var pageSections = await FindAll(trackChanges)
                .OrderByDescending(s => s.ID)
                .ToListAsync();

            return pageSections;
        }

        public async Task<PageSection> GetPageSectionByIdAsync(int id, bool? trackChanges)
        {
            return await FindByCondition(s => s.ID.Equals(id), trackChanges)
                .SingleOrDefaultAsync();
        }

        public PageSection UpdatePageSection(PageSection pageSection)
        {
            Update(pageSection);
            return pageSection;
        }
    }
}
