using System.Text.Json;

namespace Entities.Models
{
    public class Product
    {
        public int ID { get; set; }
        public JsonDocument? Files { get; set; }
        public JsonDocument? Title { get; set; }
        public JsonDocument? Slug { get; set; }
        public JsonDocument? Description { get; set; }
        public JsonDocument? Content { get; set; }
        public JsonDocument? SpecialField { get; set; }
        public List<int>? ContentID { get; set; }
        public List<Content>? Contents { get; set; }
        public int? Sort { get; set; }
        public int? Time { get; set; }
        public decimal? Price { get; set; }
        public string? PriceUnit { get; set; }
        public int? Stock { get; set; }
        public decimal? Weight { get; set; }
        public bool? IsPhysical { get; set; }
        public JsonDocument? User { get; set; }
        public DateTime? CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
    }
}
