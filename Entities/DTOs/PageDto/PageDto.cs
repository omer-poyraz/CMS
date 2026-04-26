using Entities.Models;

namespace Entities.DTOs.PageDto
{
    public class PageDto
    {
        public int ID { get; init; }
        public List<PageTranslationDto.PageTranslationDto>? Translations { get; init; } = new();
        public List<PageSectionDto.PageSectionDto>? Sections { get; init; } = new();
        public int? View { get; init; }
        public DateTime? CreatedAt { get; init; }
        public DateTime? UpdatedAt { get; init; }
    }
}
