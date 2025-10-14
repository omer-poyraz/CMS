using Entities.Models;
using Microsoft.EntityFrameworkCore;
using Repositories.Contracts;

namespace Repositories.EFCore
{
    public class UserProfileRepository : RepositoryBase<UserProfile>, IUserProfileRepository
    {
        public UserProfileRepository(RepositoryContext context) : base(context) { }

        public UserProfile CreateUserProfile(UserProfile userProfile)
        {
            Create(userProfile);
            return userProfile;
        }

        public UserProfile DeleteUserProfile(UserProfile userProfile)
        {
            Delete(userProfile);
            return userProfile;
        }

        public async Task<IEnumerable<UserProfile>> GetAllUserProfilesAsync(bool? trackChanges) =>
            await FindAll(trackChanges).OrderByDescending(s => s.ID).ToListAsync();

        public async Task<UserProfile> GetUserProfileByIdAsync(int id, bool? trackChanges) =>
            await FindByCondition(s => s.ID.Equals(id), trackChanges).SingleOrDefaultAsync();

        public async Task<UserProfile> GetUserProfileByUserIdAsync(string userId, bool? trackChanges) =>
            await FindByCondition(s => s.UserId.Equals(userId), trackChanges).SingleOrDefaultAsync();

        public UserProfile UpdateUserProfile(UserProfile userProfile)
        {
            Update(userProfile);
            return userProfile;
        }
    }
}
