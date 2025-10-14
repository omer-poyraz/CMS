using System.Text.Json;
using Entities.Models;

namespace Entities.DTOs.ProductDto
{
    public class ProductDto
    {
        public int ID { get; init; }
        public JsonDocument? Files { get; init; }
        public JsonDocument? Title { get; init; }
        public JsonDocument? Slug { get; init; }
        public JsonDocument? Description { get; init; }
        public JsonDocument? Content { get; init; }
        public JsonDocument? SpecialField { get; init; }
        public List<Entities.DTOs.ContentDto.ContentDto>? Contents { get; set; }
        public int? Sort { get; init; }
        public int? Time { get; init; }
        public decimal? Price { get; init; }
        public string? PriceUnit { get; init; }
        public int? Stock { get; init; }
        public bool? IsPhysical { get; init; }
        public decimal? Weight { get; init; }
        public JsonDocument? User { get; init; }
        public DateTime? CreatedAt { get; init; }
        public DateTime? UpdatedAt { get; init; }
    }
}
