using System.Text.Json;

namespace Entities.DTOs.ProductDto
{
    public abstract record ProductDtoForManipulation
    {
        public JsonDocument? Files { get; init; }
        public JsonDocument? Title { get; init; }
        public JsonDocument? Slug { get; init; }
        public JsonDocument? Description { get; init; }
        public JsonDocument? Content { get; init; }
        public JsonDocument? SpecialField { get; init; }
        public List<int>? ContentID { get; init; }
        public int? Sort { get; init; }
        public int? Time { get; init; }
        public decimal? Price { get; init; }
        public string? PriceUnit { get; init; }
        public int? Stock { get; init; }
        public bool? IsPhysical { get; init; }
        public decimal? Weight { get; init; }
    }
}
