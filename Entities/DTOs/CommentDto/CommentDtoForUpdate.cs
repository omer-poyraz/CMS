namespace Entities.DTOs.CommentDto
{
    public record CommentDtoForUpdate : CommentDtoForManipulation
    {
        public int ID { get; init; }
        public DateTime? UpdatedAt { get; init; } = DateTime.UtcNow;
    }
}
