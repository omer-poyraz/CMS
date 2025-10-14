using Entities.DTOs.VideoDto;

namespace Services.Contracts
{
    public interface IVideoService
    {
        Task<IEnumerable<VideoDto>> GetAllVideosAsync(string lang, bool? trackChanges);
        Task<IEnumerable<VideoDto>> GetAllVideosByVideoGroupIdAsync(int videoGroupId, string lang, bool? trackChanges);
        Task<VideoDto> GetVideoByIdAsync(int id, bool? trackChanges);
        Task<VideoDto> CreateVideoAsync(VideoDtoForInsertion videoDtoForInsertion);
        Task<VideoDto> UpdateVideoAsync(VideoDtoForUpdate videoDtoForUpdate);
        Task<VideoDto> DeleteVideoAsync(int id, bool? trackChanges);
    }
}
