using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json;

namespace Entities.Models
{
    public class Page
    {
        public int ID { get; set; }
        public JsonDocument? Slug { get; set; }
        public JsonDocument? Content { get; set; }
        [ForeignKey("PopupID")]
        public Popup? Popup { get; set; }
        public int? PopupID { get; set; }
        public int? View { get; set; }
        public JsonDocument? Meta { get; set; }
        public JsonDocument? User { get; set; }
        public DateTime? CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
    }
}
