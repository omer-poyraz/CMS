using Entities.DTOs.SettingsDto;

namespace Services.Contracts
{
    public interface ISettingsService
    {
        Task<IEnumerable<SettingsDto>> GetAllSettingsAsync(string? lang, bool? trackChanges);
        Task<SettingsDto> GetSettingsByIdAsync(int id, bool? trackChanges);
        Task<SettingsDto> GetSettingsAsync(string? lang, bool? trackChanges);
        Task<SettingsDto> CreateSettingsAsync(SettingsDtoForInsertion settingsDtoForInsertion);
        Task<SettingsDto> UpdateSettingsAsync(SettingsDtoForUpdate settingsDtoForUpdate);
        Task<SettingsDto> DeleteSettingsAsync(int id, bool? trackChanges);
    }
}
