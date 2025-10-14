using Entities.Models;

namespace Repositories.Contracts
{
    public interface IVideoRepository : IRepositoryBase<Video>
    {
        Task<IEnumerable<Video>> GetAllVideosAsync(string lang, bool? trackChanges);
        Task<IEnumerable<Video>> GetAllVideosByVideoGroupIdAsync(int videoGroupId, string lang, bool? trackChanges);
        Task<Video> GetVideoByIdAsync(int id, bool? trackChanges);
        Video CreateVideo(Video video);
        Video UpdateVideo(Video video);
        Video DeleteVideo(Video video);
    }
}
