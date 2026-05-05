using Entities.DTOs.LanguageDto;

namespace Services.Contracts
{
    public interface ILanguageService
    {
        Task<IEnumerable<LanguageDto>> GetAllLanguagesAsync(string lang, bool? trackChanges);
        Task<LanguageDto> GetLanguageByIdAsync(int id, string lang, bool? trackChanges);
        Task<LanguageDto> CreateLanguageAsync(LanguageDtoForInsertion languageDtoForInsertion);
        Task<LanguageDto> UpdateLanguageAsync(LanguageDtoForUpdate languageDtoForUpdate);
        Task<LanguageDto> DeleteLanguageAsync(int id, bool? trackChanges);
    }
}
