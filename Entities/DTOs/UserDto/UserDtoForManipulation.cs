using System.ComponentModel.DataAnnotations;

namespace Entities.DTOs.UserDto
{
    public abstract record UserDtoForManipulation
    {
        public string? FirstName { get; init; }
        public string? LastName { get; init; }
        public string? UserName { get; init; }
        public bool? Active { get; init; }
        public string? Email { get; init; }
        public string? PhoneNumber { get; init; }
    }
}
