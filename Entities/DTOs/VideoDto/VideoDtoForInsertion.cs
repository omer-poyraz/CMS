namespace Entities.DTOs.VideoDto
{
    public record VideoDtoForInsertion : VideoDtoForManipulation
    {
        public DateTime? CreatedAt { get; init; } = DateTime.UtcNow;
    }
}
