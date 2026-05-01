using Entities.Models;

namespace Repositories.Contracts
{
    public interface ISettingsRepository : IRepositoryBase<Settings>
    {
        Task<IEnumerable<Settings>> GetAllSettingsAsync(string? lang, bool? trackChanges);
        Task<Settings?> GetSettingsByIdAsync(int id, bool? trackChanges);
        Task<Settings?> GetSettingsAsync(string? lang, bool? trackChanges);
        Settings CreateSettings(Settings settings);
        Settings UpdateSettings(Settings settings);
        Settings DeleteSettings(Settings settings);
    }
}
