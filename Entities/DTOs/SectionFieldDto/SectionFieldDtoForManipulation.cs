using Entities.Models;

namespace Entities.DTOs.SectionFieldDto
{
    public abstract record SectionFieldDtoForManipulation
    {
        public PageSection? PageSection { get; init; }
        public int? PageSectionID { get; init; }
        public SectionItem? SectionItem { get; init; }
        public int? SectionItemID { get; init; }
        public string? Lang { get; init; }
        public string? Key { get; init; }
        public string? Value { get; init; }
    }
}
