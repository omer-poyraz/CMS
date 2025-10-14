
using Entities.Models;
using Entities.RequestFeature;
using Entities.RequestFeature.Content;

namespace Repositories.Contracts
{
    public interface IContentRepository : IRepositoryBase<Content>
    {
        Task<PagedList<Content>> GetAllContentsAsync(ContentParameters contentParameters, bool? trackChanges);
        Task<Content> GetContentByIdAsync(int id, bool? trackChanges);
        Task<Content> GetContentAsync(bool? trackChanges);
        Content CreateContent(Content content);
        Content UpdateContent(Content content);
        Content DeleteContent(Content content);
        Task<IEnumerable<Content>> FindByIdsAsync(IEnumerable<int> ids);
    }
}
