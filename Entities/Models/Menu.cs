using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json;

namespace Entities.Models
{
    public class Menu
    {
        public int ID { get; set; }
        public string? Icon { get; set; }
        public JsonDocument? Slug { get; set; }
        public JsonDocument? Title { get; set; }
        public JsonDocument? SpecialField1 { get; set; }
        public List<Menu>? SubMenus { get; set; }
        [ForeignKey("MenuGroupID")]
        public MenuGroup? MenuGroup { get; set; }
        public int? MenuGroupID { get; set; }
        public int? ParentMenuId { get; set; }
        public int? Sort { get; set; }
        public JsonDocument? User { get; set; }
        public DateTime? CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
    }
}
