using System.Text.Json;

namespace Entities.Models
{
    public class MenuGroup
    {
        public int ID { get; set; }
        public JsonDocument? Title { get; set; }
        public JsonDocument? SpecialField { get; set; }
        public List<Menu>? Menus { get; set; }
        public JsonDocument? User { get; set; }
        public DateTime? CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
    }
}
