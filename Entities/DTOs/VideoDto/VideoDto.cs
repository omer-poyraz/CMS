using System.Text.Json;
using Entities.Models;

namespace Entities.DTOs.VideoDto
{
    public class VideoDto
    {
        public int ID { get; set; }
        public JsonDocument? Files { get; set; }
        public JsonDocument? Title { get; set; }
        public JsonDocument? Description { get; set; }
        public VideoGroup? VideoGroup { get; set; }
        public int? VideoGroupID { get; set; }
        public JsonDocument? User { get; set; }
        public DateTime? CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
    }
}
