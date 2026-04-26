namespace Entities.DTOs.SectionItemDto
{
    public record SectionItemDtoForUpdate : SectionItemDtoForManipulation
    {
        public int ID { get; init; }
        public DateTime? UpdatedAt { get; init; } = DateTime.UtcNow;
    }
}
