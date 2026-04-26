namespace Entities.DTOs.SectionItemDto
{
    public record SectionItemDtoForInsertion : SectionItemDtoForManipulation
    {
        public DateTime? CreatedAt { get; init; } = DateTime.UtcNow;
    }
}
