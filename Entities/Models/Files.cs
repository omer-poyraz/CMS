using System.Text.Json;

namespace Entities.Models
{
    public class Files
    {
        public int ID { get; set; }
        public string? FileUrl { get; set; }
        public string? FileType { get; set; }
        public bool? WaterMarked { get; set; }
        public JsonDocument? User { get; set; }
        public DateTime? CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
    }
}
