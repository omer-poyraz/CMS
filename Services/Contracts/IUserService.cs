using Entities.DTOs.UserDto;
using Entities.RequestFeature;
using Entities.RequestFeature.User;

namespace Services.Contracts
{
    public interface IUserService
    {
        Task<(IEnumerable<UserDto> userDtos, MetaData metaData)> GetAllUsersAsync(UserParameters userParameters, bool? trackChanges);
        Task<(IEnumerable<UserDto> userDtos, MetaData metaData)> GetAllUnActiveUsersAsync(UserParameters userParameters, bool? trackChanges);
        Task<UserDto> GetOneUserByIdAsync(string? userId, bool? trackChanges);
        Task<UserDto> UpdateOneUserAsync(string? userId, UserDtoForUpdate userDtoForUpdate, bool? trackChanges);
        Task<UserDto> UserActivationAsync(string userId, bool trackChanges);
        Task<UserDto> UserIsMealAsync(string userId, bool isMeal, bool trackChanges);
        Task<UserDto> UserUnActiveAsync(string userId, bool trackChanges);
        Task<UserDto> ChangePasswordAsync(string? userId, string currentPassword, string newPassword, bool? trackChanges);
        Task<bool> ResetPasswordAsync(string? mail);
        Task<UserDto> DeleteOneUserAsync(string? userId, bool? trackChanges);
    }
}
