namespace Entities.DTOs.PopupDto
{
    public record PopupDtoForUpdate : PopupDtoForManipulation
    {
        public int ID { get; init; }
        public DateTime? UpdatedAt { get; init; } = DateTime.UtcNow;
    }
}
