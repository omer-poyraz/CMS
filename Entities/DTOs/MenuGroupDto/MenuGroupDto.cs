using System.Text.Json;
using Entities.Models;

namespace Entities.DTOs.MenuGroupDto
{
    public class MenuGroupDto
    {
        public int ID { get; init; }
        public JsonDocument? Title { get; init; }
        public JsonDocument? SpecialField { get; init; }
        public List<Menu>? Menus { get; init; }
        public JsonDocument? User { get; init; }
        public DateTime? CreatedAt { get; init; }
        public DateTime? UpdatedAt { get; init; }
    }
}
