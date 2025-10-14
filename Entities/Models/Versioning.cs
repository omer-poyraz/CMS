using System.Text.Json;

namespace Entities.Models
{
    public class Versioning
    {
        public int ID { get; set; }
        public int? Version { get; set; }
        public JsonDocument? User { get; set; }
        public DateTime? CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
    }
}
