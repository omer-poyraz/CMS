namespace Entities.DTOs.ProductDto
{
    public record ProductDtoForUpdate : ProductDtoForManipulation
    {
        public int ID { get; init; }
        public DateTime? UpdatedAt { get; init; } = DateTime.UtcNow;
    }
}
