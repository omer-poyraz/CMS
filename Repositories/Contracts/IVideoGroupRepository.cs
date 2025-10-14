using Entities.Models;

namespace Repositories.Contracts
{
    public interface IVideoGroupRepository : IRepositoryBase<VideoGroup>
    {
        Task<IEnumerable<VideoGroup>> GetAllVideoGroupsAsync(string lang, bool? trackChanges);
        Task<VideoGroup> GetVideoGroupByIdAsync(int id, bool? trackChanges);
        VideoGroup CreateVideoGroup(VideoGroup videoGroup);
        VideoGroup UpdateVideoGroup(VideoGroup videoGroup);
        VideoGroup DeleteVideoGroup(VideoGroup videoGroup);
    }
}
