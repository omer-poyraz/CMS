using System.ComponentModel.DataAnnotations.Schema;

namespace Entities.Models
{
    public class SectionField
    {
        public int ID { get; set; }

        [ForeignKey("PageSectionID")]
        public PageSection? PageSection { get; set; }
        public int? PageSectionID { get; set; }

        [ForeignKey("SectionItemID")]
        public SectionItem? SectionItem { get; set; }
        public int? SectionItemID { get; set; }

        public string? Lang { get; set; }
        public string? Key { get; set; }
        public string? Value { get; set; }

        public DateTime? CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
    }
}
