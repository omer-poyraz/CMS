using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json;

namespace Entities.Models
{
    public class Video
    {
        public int ID { get; set; }
        public JsonDocument? Files { get; set; }
        public JsonDocument? Title { get; set; }
        public JsonDocument? Description { get; set; }
        [ForeignKey("VideoGroupID")]
        public VideoGroup? VideoGroup { get; set; }
        public int? VideoGroupID { get; set; }
        public JsonDocument? User { get; set; }
        public DateTime? CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
    }
}
