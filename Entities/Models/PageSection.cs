using System.ComponentModel.DataAnnotations.Schema;

namespace Entities.Models
{
    public class PageSection
    {
        public int ID { get; set; }

        [ForeignKey("PageID")]
        public Page? Page { get; set; }
        public int? PageID { get; set; }

        public string? Type { get; set; }
        public int? Sort { get; set; }

        public ICollection<SectionField>? Fields { get; set; } = new List<SectionField>();
        public ICollection<SectionItem>? Items { get; set; } = new List<SectionItem>();

        public DateTime? CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
    }
}
