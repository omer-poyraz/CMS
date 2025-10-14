namespace Entities.DTOs.VideoDto
{
    public record VideoDtoForUpdate : VideoDtoForManipulation
    {
        public int ID { get; init; }
        public DateTime? UpdatedAt { get; init; } = DateTime.UtcNow;
    }
}
