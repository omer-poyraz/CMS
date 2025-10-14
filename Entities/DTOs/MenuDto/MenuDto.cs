using System.Text.Json;
using Entities.Models;

namespace Entities.DTOs.MenuDto
{
    public class MenuDto
    {
        public int ID { get; init; }
        public string? Icon { get; init; }
        public JsonDocument? Slug { get; init; }
        public JsonDocument? Title { get; init; }
        public JsonDocument? SpecialField1 { get; init; }
        public List<Menu>? SubMenus { get; init; }
        public MenuGroup? MenuGroup { get; init; }
        public int? MenuGroupID { get; init; }
        public int? ParentMenuId { get; init; }
        public int? Sort { get; init; }
        public JsonDocument? User { get; init; }
        public DateTime? CreatedAt { get; init; }
        public DateTime? UpdatedAt { get; init; }
    }
}
