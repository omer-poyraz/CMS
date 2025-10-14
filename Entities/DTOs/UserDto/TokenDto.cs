using Entities.Models;

namespace Entities.DTOs.UserDto
{
    public class TokenDto
    {
        public User? User { get; set; }
        public int? IsAdmin { get; init; }
        public string? AccessToken { get; init; }
        public string? RefreshToken { get; init; }
    }
}
