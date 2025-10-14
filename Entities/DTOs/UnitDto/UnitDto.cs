using System.Text.Json;

namespace Entities.DTOs.UnitDto
{
    public class UnitDto
    {
        public int ID { get; init; }
        public JsonDocument? Files { get; init; }
        public JsonDocument? Title { get; init; }
        public JsonDocument? Description { get; init; }
        public JsonDocument? Content { get; init; }
        public JsonDocument? User { get; init; }
        public DateTime? CreatedAt { get; init; }
        public DateTime? UpdatedAt { get; init; }
    }
}
