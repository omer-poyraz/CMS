namespace Entities.DTOs.LogDto
{
    public record LogDtoForInsertion : LogDtoForManipulation
    {
        public DateTime? CreatedAt { get; init; } = DateTime.UtcNow;
    }
}
