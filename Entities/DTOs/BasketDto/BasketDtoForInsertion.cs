namespace Entities.DTOs.BasketDto
{
    public record BasketDtoForInsertion : BasketDtoForManipulation
    {
        public DateTime? CreatedAt { get; init; } = DateTime.UtcNow;
    }
}
