namespace Entities.DTOs.UserProfileDto
{
    public record UserProfileDtoForUpdate : UserProfileDtoForManipulation
    {
        public int ID { get; init; }
        public DateTime? UpdatedAt { get; init; } = DateTime.UtcNow;
    }
}
