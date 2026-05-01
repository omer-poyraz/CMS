using Entities.Models;

namespace Entities.DTOs.SettingsDto
{
    public abstract record SettingsDtoForManipulation
    {
        public ICollection<SettingsTranslation>? Translations { get; init; }
    }
}
