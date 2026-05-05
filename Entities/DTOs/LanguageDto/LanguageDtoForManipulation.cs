using Entities.Models;

namespace Entities.DTOs.LanguageDto
{
    public abstract record LanguageDtoForManipulation
    {
        public ICollection<LanguageTranslation>? Translations { get; init; }
    }
}
