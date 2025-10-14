namespace Entities.DTOs.ContentDto
{
    public record ContentDtoForInsertion : ContentDtoForManipulation
    {
        public DateTime? CreatedAt { get; init; } = DateTime.UtcNow;
    }
}
