using System.Text.Json;

namespace Entities.Models
{
    public class Comment
    {
        public int ID { get; set; }
        public JsonDocument? Files { get; set; }
        public JsonDocument? Title { get; set; }
        public JsonDocument? Description { get; set; }
        public JsonDocument? Content { get; set; }
        public string? UserId { get; set; }
        public JsonDocument? User { get; set; }
        public DateTime? CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
    }
}
