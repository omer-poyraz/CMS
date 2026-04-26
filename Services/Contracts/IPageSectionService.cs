using Entities.DTOs.PageSectionDto;

namespace Services.Contracts
{
    public interface IPageSectionService
    {
        Task<IEnumerable<PageSectionDto>> GetAllPageSectionsAsync(bool? trackChanges);
        Task<PageSectionDto> GetPageSectionByIdAsync(int id, bool? trackChanges);
        Task<PageSectionDto> CreatePageSectionAsync(PageSectionDtoForInsertion pageSectionDtoForInsertion);
        Task<PageSectionDto> UpdatePageSectionAsync(PageSectionDtoForUpdate pageSectionDtoForUpdate);
        Task<PageSectionDto> DeletePageSectionAsync(int id, bool? trackChanges);
    }
}
