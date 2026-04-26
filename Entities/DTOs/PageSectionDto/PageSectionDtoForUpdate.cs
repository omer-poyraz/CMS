namespace Entities.DTOs.PageSectionDto
{
    public record PageSectionDtoForUpdate : PageSectionDtoForManipulation
    {
        public int ID { get; init; }
        public DateTime? UpdatedAt { get; init; } = DateTime.UtcNow;
    }
}
