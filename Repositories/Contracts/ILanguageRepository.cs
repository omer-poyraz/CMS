using Entities.Models;

namespace Repositories.Contracts
{
    public interface ILanguageRepository : IRepositoryBase<Language>
    {
        Task<IEnumerable<Language>> GetAllLanguagesAsync(string lang, bool? trackChanges);
        Task<Language?> GetLanguageByIdAsync(int id, string lang, bool? trackChanges);
        Language CreateLanguage(Language language);
        Language UpdateLanguage(Language language);
        Language DeleteLanguage(Language language);
    }
}
