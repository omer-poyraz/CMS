using System.ComponentModel.DataAnnotations.Schema;

namespace Entities.Models
{
    public class SectionItem
    {
        public int ID { get; set; }

        [ForeignKey("PageSectionID")]
        public PageSection? PageSection { get; set; }
        public int? PageSectionID { get; set; }

        public int? Sort { get; set; }
        public ICollection<SectionField>? Fields { get; set; } = new List<SectionField>();

        public DateTime? CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
    }
}
