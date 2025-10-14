using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json;

namespace Entities.Models
{
    public class Popup
    {
        public int ID { get; set; }
        public JsonDocument? Title { get; set; }
        public JsonDocument? Content { get; set; }
        public JsonDocument? Footer { get; set; }
        public bool? IsDaily { get; set; }
        public bool? IsWeekly { get; set; }
        public bool? IsMonthly { get; set; }
        public bool? IsYearly { get; set; }
        public bool? IsOneTime { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public JsonDocument? User { get; set; }
        public DateTime? CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
    }
}
