using System.Text.Json;

namespace Entities.Models
{
    public class Settings
    {
        public int ID { get; set; }
        public JsonDocument? SiteName { get; set; }
        public JsonDocument? Files { get; set; }
        public JsonDocument? Meta { get; set; }
        public JsonDocument? Theme { get; set; }
        public JsonDocument? Contact { get; set; }
        public JsonDocument? Contracts { get; set; }
        public JsonDocument? Location { get; set; }
        public JsonDocument? Menu { get; set; }
        public JsonDocument? Footer { get; set; }
        public JsonDocument? References { get; set; }
        public JsonDocument? SocialMedias { get; set; }
        public JsonDocument? User { get; set; }
        public DateTime? CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
    }
}
