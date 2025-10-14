using System.Text.Json;

namespace Entities.Models
{
    public class Content
    {
        public int ID { get; set; }
        public string? Code { get; set; }
        public JsonDocument? Value { get; set; }
        public string? Type { get; set; }
        public JsonDocument? User { get; set; }
        
        public List<Product>? Products { get; set; }

        public DateTime? CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
    }
}
