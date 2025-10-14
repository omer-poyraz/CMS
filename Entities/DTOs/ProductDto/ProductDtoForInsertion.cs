namespace Entities.DTOs.ProductDto
{
    public record ProductDtoForInsertion : ProductDtoForManipulation
    {
        public DateTime? CreatedAt { get; init; } = DateTime.UtcNow;
    }
}
