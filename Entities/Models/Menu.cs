using System.ComponentModel.DataAnnotations.Schema;

namespace Entities.Models
{
    public class Menu
    {
        public int ID { get; set; }
        [ForeignKey("MenuGroupID")]
        public MenuGroup? MenuGroup { get; set; }
        public int? MenuGroupID { get; set; }
        [ForeignKey("ParentMenuID")]
        public Menu? ParentMenu { get; set; }
        public int? ParentMenuID { get; set; }
        public ICollection<MenuTranslation>? Translations { get; set; }
        public int? Sort { get; set; }
        public List<Menu>? SubMenus { get; set; }
        public DateTime? CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
    }
}
