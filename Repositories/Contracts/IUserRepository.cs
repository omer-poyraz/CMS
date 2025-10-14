using Entities.Models;
using Entities.RequestFeature;
using Entities.RequestFeature.User;

namespace Repositories.Contracts
{
    public interface IUserRepository : IRepositoryBase<User>
    {
        Task<PagedList<User>> GetAllUsersAsync(UserParameters userParameters, bool? trackChanges);
        Task<PagedList<User>> GetAllUnActiveUsersAsync(UserParameters userParameters, bool? trackChanges);
        Task<User> GetOneUserByIdAsync(string? userId, bool? trackChanges);
        User UpdateOneUser(User user);
        User ChangePassword(User user);
        User DeleteOneUser(User user);
    }
}
