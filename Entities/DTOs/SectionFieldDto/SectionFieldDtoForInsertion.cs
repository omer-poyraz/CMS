namespace Entities.DTOs.SectionFieldDto
{
    public record SectionFieldDtoForInsertion : SectionFieldDtoForManipulation
    {
        public DateTime? CreatedAt { get; init; } = DateTime.UtcNow;
    }
}
