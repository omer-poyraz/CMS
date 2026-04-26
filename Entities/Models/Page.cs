namespace Entities.Models
{
    public class Page
    {
        public int ID { get; set; }

        public ICollection<PageTranslation>? Translations { get; set; } = new List<PageTranslation>();
        public ICollection<PageSection>? Sections { get; set; } = new List<PageSection>();

        public int? View { get; set; }
        public DateTime? CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
    }
}
