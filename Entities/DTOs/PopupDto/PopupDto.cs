using System.Text.Json;

namespace Entities.DTOs.PopupDto
{
    public class PopupDto
    {
        public int ID { get; init; }
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
        public JsonDocument? User { get; init; }
        public DateTime? CreatedAt { get; init; }
        public DateTime? UpdatedAt { get; init; }
    }
}
