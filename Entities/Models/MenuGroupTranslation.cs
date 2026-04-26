using System.ComponentModel.DataAnnotations.Schema;

namespace Entities.Models
{
    public class MenuGroupTranslation
    {
        public int ID { get; set; }
        [ForeignKey("MenuGroupID")]
        public MenuGroup? MenuGroup { get; set; }
        public int MenuGroupID { get; set; }
        public string? Lang { get; set; }
        public string? File { get; set; }
        public string? Title { get; set; }
        public string? Description { get; set; }
        public bool? Active { get; set; }
    }
}
