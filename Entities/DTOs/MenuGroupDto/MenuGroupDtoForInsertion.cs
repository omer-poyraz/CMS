namespace Entities.DTOs.MenuGroupDto
{
    public record MenuGroupDtoForInsertion : MenuGroupDtoForManipulation
    {
        public DateTime? CreatedAt { get; init; } = DateTime.UtcNow;
    }
}
