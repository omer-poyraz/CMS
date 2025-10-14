using System.Text.Json;

namespace Entities.Models
{
    public class Language
    {
        public int ID { get; set; }
        public string? Flag { get; set; }
        public string? Code { get; set; }
        public string? ZipCode { get; set; }
        public JsonDocument? User { get; set; }
        public DateTime? CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
    }
}
