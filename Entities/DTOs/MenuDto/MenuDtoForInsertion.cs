namespace Entities.DTOs.MenuDto
{
    public record MenuDtoForInsertion : MenuDtoForManipulation
    {
        public DateTime? CreatedAt { get; init; } = DateTime.UtcNow;
    }
}
