using System.Text.Json;
using Entities.Models;

namespace Entities.DTOs.BasketDto
{
    public class BasketDto
    {
        public int ID { get; init; }
        public int? Piece { get; init; }
        public Product? Product { get; init; }
        public int? ProductID { get; init; }
        public string? UserId { get; init; }
        public JsonDocument? User { get; init; }
        public DateTime? CreatedAt { get; init; }
        public DateTime? UpdatedAt { get; init; }
    }
}
