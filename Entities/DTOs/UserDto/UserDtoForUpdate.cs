namespace Entities.DTOs.UserDto
{
    public record UserDtoForUpdate : UserDtoForManipulation
    {
        public string? UserId { get; init; }
        public DateTime? UpdatedAt { get; init; } = DateTime.UtcNow;
    }
}
