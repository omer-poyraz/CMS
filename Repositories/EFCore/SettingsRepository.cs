using Entities.Models;
using Microsoft.EntityFrameworkCore;
using Repositories.Contracts;

namespace Repositories.EFCore
{
    public class SettingsRepository : RepositoryBase<Settings>, ISettingsRepository
    {
        public SettingsRepository(RepositoryContext context) : base(context) { }

        public Settings CreateSettings(Settings settings)
        {
            Create(settings);
            return settings;
        }

        public Settings DeleteSettings(Settings settings)
        {
            Delete(settings);
            return settings;
        }

        public async Task<IEnumerable<Settings>> GetAllSettingsAsync(string? lang, bool? trackChanges) =>
            await FindAll(trackChanges)
                .OrderByDescending(s => s.ID)
                .Include(s => s.Translations!.Where(t => t.Lang == lang))!
                    .ThenInclude(t => t.Logos)
                .Include(s => s.Translations!.Where(t => t.Lang == lang))!
                    .ThenInclude(t => t.Theme)
                .Include(s => s.Translations!.Where(t => t.Lang == lang))!
                    .ThenInclude(t => t.Contacts)
                .Include(s => s.Translations!.Where(t => t.Lang == lang))!
                    .ThenInclude(t => t.Contracts)
                .Include(s => s.Translations!.Where(t => t.Lang == lang))!
                    .ThenInclude(t => t.Locations)
                .Include(s => s.Translations!.Where(t => t.Lang == lang))!
                    .ThenInclude(t => t.References)
                .Include(s => s.Translations!.Where(t => t.Lang == lang))!
                    .ThenInclude(t => t.SocialMedias)
                .ToListAsync();

        public async Task<Settings?> GetSettingsByIdAsync(int id, bool? trackChanges) =>
            await FindByCondition(s => s.ID.Equals(id), trackChanges)
                .FirstOrDefaultAsync();

        public async Task<Settings?> GetSettingsAsync(string? lang, bool? trackChanges) =>
            await FindAll(trackChanges)
                .Include(s => s.Translations!.Where(t => t.Lang == lang))!
                    .ThenInclude(t => t.Logos)
                .Include(s => s.Translations!.Where(t => t.Lang == lang))!
                    .ThenInclude(t => t.Theme)
                .Include(s => s.Translations!.Where(t => t.Lang == lang))!
                    .ThenInclude(t => t.Contacts)
                .Include(s => s.Translations!.Where(t => t.Lang == lang))!
                    .ThenInclude(t => t.Contracts)
                .Include(s => s.Translations!.Where(t => t.Lang == lang))!
                    .ThenInclude(t => t.Locations)
                .Include(s => s.Translations!.Where(t => t.Lang == lang))!
                    .ThenInclude(t => t.References)
                .Include(s => s.Translations!.Where(t => t.Lang == lang))!
                    .ThenInclude(t => t.SocialMedias)
                .FirstOrDefaultAsync();

        public Settings UpdateSettings(Settings settings)
        {
            Update(settings);
            return settings;
        }
    }
}
