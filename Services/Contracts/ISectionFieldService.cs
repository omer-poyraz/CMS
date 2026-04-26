using Entities.DTOs.SectionFieldDto;

namespace Services.Contracts
{
    public interface ISectionFieldService
    {
        Task<IEnumerable<SectionFieldDto>> GetAllSectionFieldsAsync(bool? trackChanges);
        Task<SectionFieldDto> GetSectionFieldByIdAsync(int id, bool? trackChanges);
        Task<SectionFieldDto> CreateSectionFieldAsync(SectionFieldDtoForInsertion sectionFieldDtoForInsertion);
        Task<SectionFieldDto> UpdateSectionFieldAsync(SectionFieldDtoForUpdate sectionFieldDtoForUpdate);
        Task<SectionFieldDto> DeleteSectionFieldAsync(int id, bool? trackChanges);
    }
}
