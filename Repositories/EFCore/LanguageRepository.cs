using Entities.Models;
using Microsoft.EntityFrameworkCore;
using Repositories.Contracts;

namespace Repositories.EFCore
{
    public class LanguageRepository : RepositoryBase<Language>, ILanguageRepository
    {
        public LanguageRepository(RepositoryContext context) : base(context) { }

        public Language CreateLanguage(Language language)
        {
            Create(language);
            return language;
        }

        public Language DeleteLanguage(Language language)
        {
            Delete(language);
            return language;
        }

        public async Task<IEnumerable<Language>> GetAllLanguagesAsync(string lang, bool? trackChanges) =>
            await FindAll(trackChanges)
                .OrderByDescending(s => s.ID)
                .Include(s => s.Translations!.Where(t => t.Lang == lang))
                .ToListAsync();

        public async Task<Language?> GetLanguageByIdAsync(int id, string lang, bool? trackChanges) =>
            await FindByCondition(s => s.ID.Equals(id), trackChanges)
                .Include(s => s.Translations!.Where(t => t.Lang == lang))
                .SingleOrDefaultAsync();

        public Language UpdateLanguage(Language language)
        {
            Update(language);
            return language;
        }
    }
}
