using Entities.Models;

namespace Entities.DTOs.PageTranslationDto
{
    public abstract record PageTranslationDtoForManipulation
    {
        public Page? Page { get; init; }
        public int? PageID { get; init; }
        public string? Lang { get; init; }
        public string? Title { get; init; }
        public string? Slug { get; init; }
        public string? SeoTitle { get; init; }
        public string? SeoDescription { get; init; }
        public string? SeoKeywords { get; init; }
        public string? SeoAuthor { get; init; }
        public string? SeoCanonical { get; init; }
    }
}
