using Entities.Models;

namespace Repositories.Contracts
{
    public interface IPageSectionRepository : IRepositoryBase<PageSection>
    {
        Task<IEnumerable<PageSection>> GetAllPageSectionsAsync(bool? trackChanges);
        Task<PageSection> GetPageSectionByIdAsync(int id, bool? trackChanges);
        PageSection CreatePageSection(PageSection pageSection);
        PageSection UpdatePageSection(PageSection pageSection);
        PageSection DeletePageSection(PageSection pageSection);
    }
}
