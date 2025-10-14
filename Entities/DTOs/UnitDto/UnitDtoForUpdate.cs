namespace Entities.DTOs.UnitDto
{
    public record UnitDtoForUpdate : UnitDtoForManipulation
    {
        public int ID { get; init; }
        public DateTime? UpdatedAt { get; init; } = DateTime.UtcNow;
    }
}
