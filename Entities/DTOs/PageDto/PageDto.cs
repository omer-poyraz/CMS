using System.Text.Json;
using Entities.Models;

namespace Entities.DTOs.PageDto
{
    public class PageDto
    {
        public int ID { get; init; }
        public JsonDocument? Slug { get; init; }
        public JsonDocument? Content { get; init; }
        public Popup? Popup { get; init; }
        public int? PopupID { get; init; }
        public int? View { get; init; }
        public JsonDocument? Meta { get; init; }
        public JsonDocument? User { get; init; }
        public DateTime? CreatedAt { get; init; }
        public DateTime? UpdatedAt { get; init; }
    }
}
