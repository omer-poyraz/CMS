namespace Entities.DTOs.SettingsDto
{
    public record SettingsDtoForInsertion : SettingsDtoForManipulation
    {
        public DateTime? CreatedAt { get; init; } = DateTime.UtcNow;
    }
}
