using Entities.Models;

namespace Entities.DTOs.SettingsDto
{
    public class SettingsDto
    {
        public int ID { get; init; }
        public ICollection<SettingsTranslation>? Translations { get; init; }
        public DateTime? CreatedAt { get; init; }
        public DateTime? UpdatedAt { get; init; }
    }
}
