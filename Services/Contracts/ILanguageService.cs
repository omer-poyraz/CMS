using Entities.DTOs.LanguageDto;

namespace Services.Contracts
{
    public interface ILanguageService
    {
        Task<IEnumerable<LanguageDto>> GetAllLanguagesAsync(bool? trackChanges);
        Task<LanguageDto> GetLanguageByIdAsync(int id, bool? trackChanges);
        Task<LanguageDto> GetLanguageByCodeAsync(string code, bool? trackChanges);
        Task<LanguageDto> CreateLanguageAsync(LanguageDtoForInsertion languageDtoForInsertion);
        Task<LanguageDto> UpdateLanguageAsync(LanguageDtoForUpdate languageDtoForUpdate);
        Task<LanguageDto> DeleteLanguageAsync(int id, bool? trackChanges);
    }
}
