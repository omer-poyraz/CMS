using Entities.Models;

namespace Entities.DTOs.MenuDto
{
    public class MenuDto
    {
        public int ID { get; set; }
        public MenuGroup? MenuGroup { get; set; }
        public int? MenuGroupID { get; set; }
        public Menu? ParentMenu { get; set; }
        public int? ParentMenuID { get; set; }
        public ICollection<MenuTranslation>? Translations { get; set; }
        public int? Sort { get; set; }
        public List<Menu>? SubMenus { get; set; }
        public DateTime? CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
    }
}
