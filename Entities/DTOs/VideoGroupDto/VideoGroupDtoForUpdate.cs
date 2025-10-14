namespace Entities.DTOs.VideoGroupDto
{
    public record VideoGroupDtoForUpdate : VideoGroupDtoForManipulation
    {
        public int ID { get; init; }
        public DateTime? UpdatedAt { get; init; } = DateTime.UtcNow;
    }
}
