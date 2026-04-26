using Entities.Models;

namespace Entities.DTOs.PageDto
{
    public abstract record PageDtoForManipulation
    {
        public List<PageTranslation>? Translations { get; init; } = new();
        public List<PageSection>? Sections { get; init; } = new();
    }
}
