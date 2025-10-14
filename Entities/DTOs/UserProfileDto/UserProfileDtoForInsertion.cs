namespace Entities.DTOs.UserProfileDto
{
    public record UserProfileDtoForInsertion : UserProfileDtoForManipulation
    {
        public DateTime? CreatedAt { get; init; } = DateTime.UtcNow;
    }
}
