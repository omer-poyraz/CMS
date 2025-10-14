namespace Entities.DTOs.PopupDto
{
    public record PopupDtoForInsertion : PopupDtoForManipulation
    {
        public DateTime? CreatedAt { get; init; } = DateTime.UtcNow;
    }
}
