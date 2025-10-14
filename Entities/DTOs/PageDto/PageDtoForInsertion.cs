namespace Entities.DTOs.PageDto
{
    public record PageDtoForInsertion : PageDtoForManipulation
    {
        public DateTime? CreatedAt { get; init; } = DateTime.UtcNow;
    }
}
