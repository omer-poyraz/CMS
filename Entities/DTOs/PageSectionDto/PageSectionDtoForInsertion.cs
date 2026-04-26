namespace Entities.DTOs.PageSectionDto
{
    public record PageSectionDtoForInsertion : PageSectionDtoForManipulation
    {
        public DateTime? CreatedAt { get; init; } = DateTime.UtcNow;
    }
}
