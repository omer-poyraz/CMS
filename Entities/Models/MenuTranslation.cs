using System.ComponentModel.DataAnnotations.Schema;

namespace Entities.Models
{
    public class MenuTranslation
    {
        public int ID { get; set; }
        [ForeignKey("MenuID")]
        public Menu? Menu { get; set; }
        public int MenuID { get; set; }
        public string? Lang { get; set; }
        public string? File { get; set; }
        public string? Title { get; set; }
        public string? Slug { get; set; }
        public bool? Active { get; set; }
    }
}
