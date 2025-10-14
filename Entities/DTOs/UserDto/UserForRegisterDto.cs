namespace Entities.DTOs.UserDto
{
    public record UserForRegisterDto
    {
        public string FirstName { get; init; }
        public string? LastName { get; init; }
        public string UserName { get; init; }
        public string Email { get; init; }
        public string? PhoneNumber { get; init; }
        public int? ProductID { get; set; }
        public string Password { get; init; }
        public List<string> Roles { get; init; }
        public DateTime? CreatedAt { get; init; } = DateTime.UtcNow;
    }
}
