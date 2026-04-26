using Entities.Models;

namespace Entities.DTOs.SectionItemDto
{
    public abstract record SectionItemDtoForManipulation
    {
        public PageSection? PageSection { get; init; }
        public int? PageSectionID { get; init; }
        public int? Sort { get; init; }
        public List<SectionField>? Fields { get; init; } = new();
    }
}
