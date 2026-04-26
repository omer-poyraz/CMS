using Entities.Models;

namespace Entities.DTOs.PageSectionDto
{
    public abstract record PageSectionDtoForManipulation
    {
        public Page? Page { get; init; }
        public int? PageID { get; init; }

        public string? Type { get; init; }
        public int? Sort { get; init; }

        public List<SectionField>? Fields { get; init; } = new();
        public List<SectionItem>? Items { get; init; } = new();
    }
}
