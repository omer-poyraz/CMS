using System.ComponentModel.DataAnnotations.Schema;

namespace Entities.Models
{
    public class PageTranslation
    {
        public int ID { get; set; }

        [ForeignKey("PageID")]
        public Page? Page { get; set; }
        public int? PageID { get; set; }

        public string? Lang { get; set; }
        public string? Title { get; set; }
        public string? Slug { get; set; }
        public string? SeoTitle { get; set; }
        public string? SeoDescription { get; set; }
        public string? SeoKeywords { get; set; }
        public string? SeoAuthor { get; set; }
        public string? SeoCanonical { get; set; }
        public DateTime? CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
    }
}
