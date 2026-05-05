using Entities.Models;

namespace Entities.DTOs.LanguageDto
{
    public class LanguageDto
    {
        public int ID { get; init; }
        public ICollection<LanguageTranslation>? Translations { get; init; }
        public DateTime? CreatedAt { get; init; }
        public DateTime? UpdatedAt { get; init; }
    }
}
