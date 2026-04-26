using Entities.Models;

namespace Entities.DTOs.SectionFieldDto
{
    public class SectionFieldDto
    {
        public int ID { get; init; }
        public int? PageSectionID { get; init; }
        public int? SectionItemID { get; init; }
        public string? Lang { get; init; }
        public string? Key { get; init; }
        public string? Value { get; init; }
        public DateTime? CreatedAt { get; init; }
        public DateTime? UpdatedAt { get; init; }
    }
}
