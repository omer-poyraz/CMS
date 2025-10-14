using Entities.DTOs.ContentDto;
using Entities.RequestFeature;
using Entities.RequestFeature.Content;

namespace Services.Contracts
{
    public interface IContentService
    {
        Task<(IEnumerable<ContentDto> contentDtos, MetaData metaData)> GetAllContentsAsync(ContentParameters contentParameters, bool? trackChanges);
        Task<ContentDto> GetContentByIdAsync(int id, bool? trackChanges);
        Task<ContentDto> GetContentAsync(bool? trackChanges);
        Task<ContentDto> CreateContentAsync(ContentDtoForInsertion contentDtoForInsertion);
        Task<ContentDto> UpdateContentAsync(ContentDtoForUpdate contentDtoForUpdate);
        Task<ContentDto> DeleteContentAsync(int id, bool? trackChanges);
    }
}
