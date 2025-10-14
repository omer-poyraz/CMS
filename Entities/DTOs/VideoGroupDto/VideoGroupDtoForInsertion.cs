namespace Entities.DTOs.VideoGroupDto
{
    public record VideoGroupDtoForInsertion : VideoGroupDtoForManipulation
    {
        public DateTime? CreatedAt { get; init; } = DateTime.UtcNow;
    }
}
