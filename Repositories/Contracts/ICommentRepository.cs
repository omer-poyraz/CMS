using Entities.Models;

namespace Repositories.Contracts
{
    public interface ICommentRepository : IRepositoryBase<Comment>
    {
        Task<IEnumerable<Comment>> GetAllCommentsAsync(bool? trackChanges);
        Task<Comment?> GetCommentByIdAsync(int id, bool? trackChanges);
        Task<Comment?> GetCommentByUserIdAsync(string userId, bool? trackChanges);
        Comment CreateComment(Comment comment);
        Comment UpdateComment(Comment comment);
        Comment DeleteComment(Comment comment);
    }
}
