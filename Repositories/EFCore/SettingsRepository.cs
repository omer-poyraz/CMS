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

        public async Task<IEnumerable<Settings>> GetAllSettingsAsync(bool? trackChanges) =>
            await FindAll(trackChanges)
                .OrderBy(s => s.ID)
                .ToListAsync();

        public async Task<Settings?> GetSettingsByIdAsync(int id, bool? trackChanges) =>
            await FindByCondition(s => s.ID.Equals(id), trackChanges)
                .FirstOrDefaultAsync();

        public async Task<Settings?> GetSettingsAsync(bool? trackChanges) =>
            await FindAll(trackChanges)
                .OrderByDescending(s => s.ID)
                .FirstOrDefaultAsync();

        public Settings UpdateSettings(Settings settings)
        {
            Update(settings);
            return settings;
        }
    }
}
