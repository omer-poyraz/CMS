using System.Text.Json;

namespace Entities.DTOs.BasketDto
{
    public abstract record BasketDtoForManipulation
    {
        public int? Piece { get; init; }
        public int? ProductID { get; init; }
        public string? UserId { get; init; }
    }
}
