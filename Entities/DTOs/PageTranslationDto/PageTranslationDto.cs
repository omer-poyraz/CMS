using Entities.Models;

namespace Entities.DTOs.PageTranslationDto
{
    public class PageTranslationDto
    {
        public int ID { get; init; }
        public int? PageID { get; init; }
        public string? Lang { get; init; }
        public string? Title { get; init; }
        public string? Slug { get; init; }
        public string? SeoTitle { get; init; }
        public string? SeoDescription { get; init; }
        public string? SeoKeywords { get; init; }
        public string? SeoAuthor { get; init; }
        public string? SeoCanonical { get; init; }
        public DateTime? CreatedAt { get; init; }
        public DateTime? UpdatedAt { get; init; }
    }
}
