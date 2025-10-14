namespace Entities.DTOs.UnitDto
{
    public record UnitDtoForInsertion : UnitDtoForManipulation
    {
        public DateTime? CreatedAt { get; init; } = DateTime.UtcNow;
    }
}
