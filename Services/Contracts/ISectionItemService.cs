using Entities.DTOs.SectionItemDto;

namespace Services.Contracts
{
    public interface ISectionItemService
    {
        Task<IEnumerable<SectionItemDto>> GetAllSectionItemsAsync(bool? trackChanges);
        Task<SectionItemDto> GetSectionItemByIdAsync(int id, bool? trackChanges);
        Task<SectionItemDto> CreateSectionItemAsync(SectionItemDtoForInsertion sectionItemDtoForInsertion);
        Task<SectionItemDto> UpdateSectionItemAsync(SectionItemDtoForUpdate sectionItemDtoForUpdate);
        Task<SectionItemDto> DeleteSectionItemAsync(int id, bool? trackChanges);
    }
}
