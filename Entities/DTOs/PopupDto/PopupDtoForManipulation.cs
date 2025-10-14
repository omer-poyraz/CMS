using System.Text.Json;

namespace Entities.DTOs.PopupDto
{
    public abstract record PopupDtoForManipulation
    {
        public JsonDocument? Title { get; init; }
        public JsonDocument? Content { get; init; }
        public JsonDocument? Footer { get; init; }
        public bool? IsDaily { get; init; }
        public bool? IsWeekly { get; init; }
        public bool? IsMonthly { get; init; }
        public bool? IsYearly { get; init; }
        public bool? IsOneTime { get; init; }
        public DateTime? StartDate { get; init; }
        public DateTime? EndDate { get; init; }
    }
}
