using Entities.Models;

namespace Repositories.Contracts
{
    public interface ISectionItemRepository : IRepositoryBase<SectionItem>
    {
        Task<IEnumerable<SectionItem>> GetAllSectionItemsAsync(bool? trackChanges);
        Task<SectionItem> GetSectionItemByIdAsync(int id, bool? trackChanges);
        SectionItem CreateSectionItem(SectionItem sectionItem);
        SectionItem UpdateSectionItem(SectionItem sectionItem);
        SectionItem DeleteSectionItem(SectionItem sectionItem);
    }
}
