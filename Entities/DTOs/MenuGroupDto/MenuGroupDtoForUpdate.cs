namespace Entities.DTOs.MenuGroupDto
{
    public record MenuGroupDtoForUpdate : MenuGroupDtoForManipulation
    {
        public int ID { get; init; }
        public DateTime? UpdatedAt { get; init; } = DateTime.UtcNow;
    }
}
