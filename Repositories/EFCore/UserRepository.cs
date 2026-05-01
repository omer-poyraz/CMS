using Entities.Models;
using Entities.RequestFeature.User;
using Entities.RequestFeature;
using Repositories.Contracts;
using Microsoft.EntityFrameworkCore;
using Repositories.EFCore.Extensions;

namespace Repositories.EFCore
{
    public class UserRepository : RepositoryBase<User>, IUserRepository
    {
        public UserRepository(RepositoryContext context) : base(context) { }

        public User ChangePassword(User user)
        {
            Update(user);
            return user;
        }

        public User DeleteOneUser(User user)
        {
            Delete(user);
            return user;
        }

        public async Task<PagedList<User>> GetAllUsersAsync(UserParameters userParameters, bool? trackChanges)
        {
            var users = await FindAll(trackChanges)
                .OrderBy(u => u.Id)
                .Include(r => r.Roles)
                .SearchUser(userParameters.SearchTerm!)
                .ToListAsync();

            return PagedList<User>.ToPagedList(users, userParameters.PageNumber, userParameters.PageSize);
        }

        public async Task<PagedList<User>> GetAllUnActiveUsersAsync(UserParameters userParameters, bool? trackChanges)
        {
            var users = await FindAll(trackChanges)
                .Where(u => u.Active == false)
                .OrderBy(u => u.Id)
                .Include(r => r.Roles)
                .SearchUser(userParameters.SearchTerm!)
                .ToListAsync();

            return PagedList<User>.ToPagedList(users, userParameters.PageNumber, userParameters.PageSize);
        }

        public async Task<User?> GetOneUserByIdAsync(string? userId, bool? trackChanges) =>
            await FindByCondition(u => u.Id.Equals(userId), trackChanges)
                .Include(r => r.Roles)
                .SingleOrDefaultAsync();

        public User UpdateOneUser(User user)
        {
            Update(user);
            return user;
        }
    }
}
