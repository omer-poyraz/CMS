using Entities.DTOs.VideoGroupDto;

namespace Services.Contracts
{
    public interface IVideoGroupService
    {
        Task<IEnumerable<VideoGroupDto>> GetAllVideoGroupsAsync(string lang, bool? trackChanges);
        Task<VideoGroupDto> GetVideoGroupByIdAsync(int id, bool? trackChanges);
        Task<VideoGroupDto> CreateVideoGroupAsync(VideoGroupDtoForInsertion videoGroupDtoForInsertion);
        Task<VideoGroupDto> UpdateVideoGroupAsync(VideoGroupDtoForUpdate videoGroupDtoForUpdate);
        Task<VideoGroupDto> DeleteVideoGroupAsync(int id, bool? trackChanges);
    }
}
