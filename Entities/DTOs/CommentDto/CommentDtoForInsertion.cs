namespace Entities.DTOs.CommentDto
{
    public record CommentDtoForInsertion : CommentDtoForManipulation
    {
        public DateTime? CreatedAt { get; init; } = DateTime.UtcNow;
    }
}
