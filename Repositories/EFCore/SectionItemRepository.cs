using Entities.Models;
using Microsoft.EntityFrameworkCore;
using Repositories.Contracts;

namespace Repositories.EFCore
{
    public class SectionItemRepository : RepositoryBase<SectionItem>, ISectionItemRepository
    {

        public SectionItemRepository(RepositoryContext context) : base(context) { }

        public SectionItem CreateSectionItem(SectionItem sectionItem)
        {
            Create(sectionItem);
            return sectionItem;
        }

        public SectionItem DeleteSectionItem(SectionItem sectionItem)
        {
            Delete(sectionItem);
            return sectionItem;
        }

        public async Task<IEnumerable<SectionItem>> GetAllSectionItemsAsync(bool? trackChanges)
        {
            var sectionItems = await FindAll(trackChanges)
                .OrderByDescending(s => s.ID)
                .ToListAsync();

            return sectionItems;
        }

        public async Task<SectionItem> GetSectionItemByIdAsync(int id, bool? trackChanges)
        {
            return await FindByCondition(s => s.ID.Equals(id), trackChanges)
                .SingleOrDefaultAsync();
        }

        public SectionItem UpdateSectionItem(SectionItem sectionItem)
        {
            Update(sectionItem);
            return sectionItem;
        }
    }
}
