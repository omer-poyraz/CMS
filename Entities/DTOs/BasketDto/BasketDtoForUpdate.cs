namespace Entities.DTOs.BasketDto
{
    public record BasketDtoForUpdate : BasketDtoForManipulation
    {
        public int ID { get; init; }
        public DateTime? UpdatedAt { get; init; } = DateTime.UtcNow;
    }
}
