namespace Entities.DTOs.PageDto
{
    public record PageDtoForUpdate : PageDtoForManipulation
    {
        public int ID { get; init; }
        public DateTime? UpdatedAt { get; init; } = DateTime.UtcNow;
    }
}
