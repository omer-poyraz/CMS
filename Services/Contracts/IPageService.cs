using Entities.DTOs.PageDto;
using Entities.RequestFeature;
using Entities.RequestFeature.Page;

namespace Services.Contracts
{
    public interface IPageService
    {
        Task<(IEnumerable<PageDto> pageDtos, MetaData metaData)> GetAllPagesAsync(PageParameters pageParameters, bool? trackChanges);
        Task<PageDto> GetPageByIdAsync(int id, bool? trackChanges);
        Task<PageDto> GetPageBySlugAsync(string slug, string lang, bool? trackChanges);
        Task<PageDto> CreatePageAsync(PageDtoForInsertion pageDtoForInsertion);
        Task<PageDto> UpdatePageAsync(PageDtoForUpdate pageDtoForUpdate);
        Task<PageDto> DeletePageAsync(int id, bool? trackChanges);
    }
}
