using Entities.Models;

namespace Repositories.Contracts
{
    public interface ILanguageRepository : IRepositoryBase<Language>
    {
        Task<IEnumerable<Language>> GetAllLanguagesAsync(bool? trackChanges);
        Task<Language?> GetLanguageByIdAsync(int id, bool? trackChanges);
        Task<Language?> GetLanguageByCodeAsync(string code, bool? trackChanges);
        Language CreateLanguage(Language language);
        Language UpdateLanguage(Language language);
        Language DeleteLanguage(Language language);
    }
}
