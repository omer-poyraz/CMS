using Entities.Models;
using Microsoft.EntityFrameworkCore;
using Repositories.Contracts;

namespace Repositories.EFCore
{
    public class CommentRepository : RepositoryBase<Comment>, ICommentRepository
    {
        public CommentRepository(RepositoryContext context) : base(context) { }

        public Comment CreateComment(Comment comment)
        {
            Create(comment);
            return comment;
        }

        public Comment DeleteComment(Comment comment)
        {
            Delete(comment);
            return comment;
        }

        public async Task<IEnumerable<Comment>> GetAllCommentsAsync(bool? trackChanges) =>
            await FindAll(trackChanges)
                .OrderByDescending(s => s.ID)
                .ToListAsync();

        public async Task<Comment?> GetCommentByIdAsync(int id, bool? trackChanges) =>
            await FindByCondition(s => s.ID.Equals(id), trackChanges)
                .SingleOrDefaultAsync();

        public async Task<Comment?> GetCommentByUserIdAsync(string userId, bool? trackChanges) =>
            await FindByCondition(s => s.UserId!.Equals(userId), trackChanges)
                .SingleOrDefaultAsync();

        public Comment UpdateComment(Comment comment)
        {
            Update(comment);
            return comment;
        }
    }
}
