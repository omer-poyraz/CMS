namespace Entities.DTOs.ContentDto
{
    public record ContentDtoForUpdate : ContentDtoForManipulation
    {
        public int ID { get; set; }
        public DateTime? UpdatedAt { get; init; } = DateTime.UtcNow;
    }
}
