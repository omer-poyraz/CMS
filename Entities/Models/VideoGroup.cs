using System.Text.Json;

namespace Entities.Models
{
    public class VideoGroup
    {
        public int ID { get; set; }
        public JsonDocument? Files { get; set; }
        public JsonDocument? Title { get; set; }
        public JsonDocument? Description { get; set; }
        public List<Video>? Videos { get; set; }
        public JsonDocument? User { get; set; }
        public DateTime? CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
    }
}
