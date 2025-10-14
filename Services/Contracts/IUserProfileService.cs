using Entities.DTOs.UserProfileDto;

namespace Services.Contracts
{
    public interface IUserProfileService
    {
        Task<IEnumerable<UserProfileDto>> GetAllUserProfilesAsync(bool? trackChanges);
        Task<UserProfileDto> GetUserProfileByIdAsync(int id, bool? trackChanges);
        Task<UserProfileDto> GetUserProfileByUserIdAsync(string userId, bool? trackChanges);
        Task<UserProfileDto> CreateUserProfileAsync(UserProfileDtoForInsertion userProfileDtoForInsertion);
        Task<UserProfileDto> UpdateUserProfileAsync(UserProfileDtoForUpdate userProfileDtoForUpdate);
        Task<UserProfileDto> DeleteUserProfileAsync(int id, bool? trackChanges);
    }
}
