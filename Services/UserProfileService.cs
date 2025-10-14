using AutoMapper;
using Entities.DTOs.UserProfileDto;
using Repositories.Contracts;
using Services.Contracts;

namespace Services
{
    public class UserProfileService : IUserProfileService
    {
        private readonly IRepositoryManager _manager;
        private readonly IMapper _mapper;

        public UserProfileService(IRepositoryManager manager, IMapper mapper)
        {
            _manager = manager;
            _mapper = mapper;
        }

        public async Task<UserProfileDto> CreateUserProfileAsync(UserProfileDtoForInsertion userProfileDtoForInsertion)
        {
            ConvertDatesToUtc(userProfileDtoForInsertion);
            var userProfile = _mapper.Map<Entities.Models.UserProfile>(userProfileDtoForInsertion);
            var hasUserProfile = await _manager.UserProfileRepository.GetUserProfileByUserIdAsync(userProfileDtoForInsertion.UserId!, false);
            if (hasUserProfile != null)
            {
                throw new InvalidOperationException("User profile already exists for this user.");
            }
            _manager.UserProfileRepository.CreateUserProfile(userProfile);
            await _manager.SaveAsync();
            return _mapper.Map<UserProfileDto>(userProfile);
        }

        public async Task<UserProfileDto> DeleteUserProfileAsync(int id, bool? trackChanges)
        {
            var userProfile = await _manager.UserProfileRepository.GetUserProfileByIdAsync(id, trackChanges);
            _manager.UserProfileRepository.DeleteUserProfile(userProfile);
            await _manager.SaveAsync();
            return _mapper.Map<UserProfileDto>(userProfile);
        }

        public async Task<IEnumerable<UserProfileDto>> GetAllUserProfilesAsync(bool? trackChanges)
        {
            var userProfile = await _manager.UserProfileRepository.GetAllUserProfilesAsync(trackChanges);
            return _mapper.Map<IEnumerable<UserProfileDto>>(userProfile);
        }

        public async Task<UserProfileDto> GetUserProfileByIdAsync(int id, bool? trackChanges)
        {
            var userProfile = await _manager.UserProfileRepository.GetUserProfileByIdAsync(id, trackChanges);
            return _mapper.Map<UserProfileDto>(userProfile);
        }

        public async Task<UserProfileDto> GetUserProfileByUserIdAsync(string userId, bool? trackChanges)
        {
            var userProfile = await _manager.UserProfileRepository.GetUserProfileByUserIdAsync(userId, trackChanges);
            return _mapper.Map<UserProfileDto>(userProfile);
        }

        public async Task<UserProfileDto> UpdateUserProfileAsync(UserProfileDtoForUpdate userProfileDtoForUpdate)
        {
            ConvertDatesToUtc(userProfileDtoForUpdate);
            var userProfile = await _manager.UserProfileRepository.GetUserProfileByIdAsync(userProfileDtoForUpdate.ID, false);
            _mapper.Map(userProfileDtoForUpdate, userProfile);
            _manager.UserProfileRepository.UpdateUserProfile(userProfile);
            await _manager.SaveAsync();
            return _mapper.Map<UserProfileDto>(userProfile);
        }

        private void ConvertDatesToUtc<T>(T dto) where T : UserProfileDtoForManipulation
        {
            if (dto.BirthDate.HasValue)
                dto.BirthDate = DateTime.SpecifyKind(dto.BirthDate.Value, DateTimeKind.Utc);

            if (dto.ProgramStartDate.HasValue)
                dto.ProgramStartDate = DateTime.SpecifyKind(dto.ProgramStartDate.Value, DateTimeKind.Utc);
        }
    }
}
