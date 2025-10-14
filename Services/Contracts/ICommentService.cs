using Entities.DTOs.CommentDto;

namespace Services.Contracts
{
    public interface ICommentService
    {
        Task<IEnumerable<CommentDto>> GetAllCommentsAsync(bool? trackChanges);
        Task<CommentDto> GetCommentByIdAsync(int id, bool? trackChanges);
        Task<CommentDto> GetCommentByUserIdAsync(string userId, bool? trackChanges);
        Task<CommentDto> CreateCommentAsync(CommentDtoForInsertion commentDtoForInsertion);
        Task<CommentDto> UpdateCommentAsync(CommentDtoForUpdate commentDtoForUpdate);
        Task<CommentDto> DeleteCommentAsync(int id, bool? trackChanges);
    }
}
