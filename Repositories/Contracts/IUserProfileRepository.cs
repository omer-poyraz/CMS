using Entities.Models;

namespace Repositories.Contracts
{
    public interface IUserProfileRepository : IRepositoryBase<UserProfile>
    {
        Task<IEnumerable<UserProfile>> GetAllUserProfilesAsync(bool? trackChanges);
        Task<UserProfile> GetUserProfileByIdAsync(int id, bool? trackChanges);
        Task<UserProfile> GetUserProfileByUserIdAsync(string userId, bool? trackChanges);
        UserProfile CreateUserProfile(UserProfile userProfile);
        UserProfile UpdateUserProfile(UserProfile userProfile);
        UserProfile DeleteUserProfile(UserProfile userProfile);
    }
}
