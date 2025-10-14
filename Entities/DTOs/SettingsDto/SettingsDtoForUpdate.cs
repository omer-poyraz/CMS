namespace Entities.DTOs.SettingsDto
{
    public record SettingsDtoForUpdate : SettingsDtoForManipulation
    {
        public int ID { get; init; }
        public DateTime? UpdatedAt { get; init; } = DateTime.UtcNow;
    }
}
