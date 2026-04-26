using Entities.Models;
using Entities.RequestFeature;
using Entities.RequestFeature.Page;

namespace Repositories.Contracts
{
    public interface IPageRepository : IRepositoryBase<Page>
    {
        Task<PagedList<Page>> GetAllPagesAsync(PageParameters pageParameters, bool? trackChanges);
        Task<Page?> GetPageByIdAsync(int id, bool? trackChanges);
        Task<Page?> GetPageBySlugAsync(string slug, string lang, bool? trackChanges);
        Page CreatePage(Page page);
        Page UpdatePage(Page page);
        Page DeletePage(Page page);
    }
}
