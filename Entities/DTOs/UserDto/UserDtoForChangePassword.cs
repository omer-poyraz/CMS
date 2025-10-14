using System.ComponentModel.DataAnnotations;

namespace Entities.DTOs.UserDto
{
    public record UserDtoForChangePassword
    {
        [Required]
        public string? CurrentPassword { get; init; }

        [Required]
        [MinLength(5)]
        public string? NewPassword { get; init; }
    }
}
