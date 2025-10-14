namespace Entities.DTOs.MenuDto
{
    public record MenuDtoForUpdate : MenuDtoForManipulation
    {
        public int ID { get; init; }
        public DateTime? UpdatedAt { get; init; } = DateTime.UtcNow;
    }
}
