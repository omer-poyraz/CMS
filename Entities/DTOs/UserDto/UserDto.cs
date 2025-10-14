using Microsoft.AspNetCore.Identity;

namespace Entities.DTOs.UserDto
{
    public record UserDto
    {
        public string? UserId { get; init; }
        public string? FirstName { get; init; }
        public string? LastName { get; init; }
        public string? UserName { get; init; }
        public string? Email { get; init; }
        public bool? Active { get; init; }
        public int? ProductID { get; init; }
        public bool? IsMeal { get; init; }
        public string? PhoneNumber { get; init; }
        public List<IdentityRole>? Roles { get; init; }
        public DateTime? CreatedAt { get; init; } = DateTime.UtcNow;
        public DateTime? UpdatedAt { get; init; } = DateTime.UtcNow;
        public DateTime? DeletedAt { get; init; } = DateTime.UtcNow;
    }
}
