namespace Entities.DTOs.SectionFieldDto
{
    public record SectionFieldDtoForUpdate : SectionFieldDtoForManipulation
    {
        public int ID { get; init; }
        public DateTime? UpdatedAt { get; init; } = DateTime.UtcNow;
    }
}
