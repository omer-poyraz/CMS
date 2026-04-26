using Entities.DTOs.PageTranslationDto;

namespace Services.Contracts
{
    public interface IPageTranslationService
    {
        Task<IEnumerable<PageTranslationDto>> GetAllPageTranslationsAsync(bool? trackChanges);
        Task<PageTranslationDto> GetPageTranslationByIdAsync(int id, bool? trackChanges);
        Task<PageTranslationDto> CreatePageTranslationAsync(PageTranslationDtoForInsertion pageTranslationDtoForInsertion);
        Task<PageTranslationDto> UpdatePageTranslationAsync(PageTranslationDtoForUpdate pageTranslationDtoForUpdate);
        Task<PageTranslationDto> DeletePageTranslationAsync(int id, bool? trackChanges);
    }
}
