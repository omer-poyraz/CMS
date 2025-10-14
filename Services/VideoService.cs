using AutoMapper;
using Entities.DTOs.VideoDto;
using Repositories.Contracts;
using Services.Contracts;

namespace Services
{
    public class VideoService : IVideoService
    {
        private readonly IRepositoryManager _manager;
        private readonly IMapper _mapper;

        public VideoService(IRepositoryManager manager, IMapper mapper)
        {
            _manager = manager;
            _mapper = mapper;
        }

        public async Task<VideoDto> CreateVideoAsync(VideoDtoForInsertion videoDtoForInsertion)
        {
            var video = _mapper.Map<Entities.Models.Video>(videoDtoForInsertion);
            _manager.VideoRepository.CreateVideo(video);
            await _manager.SaveAsync();
            return _mapper.Map<VideoDto>(video);
        }

        public async Task<VideoDto> DeleteVideoAsync(int id, bool? trackChanges)
        {
            var video = await _manager.VideoRepository.GetVideoByIdAsync(id, trackChanges);
            _manager.VideoRepository.DeleteVideo(video);
            await _manager.SaveAsync();
            return _mapper.Map<VideoDto>(video);
        }

        public async Task<IEnumerable<VideoDto>> GetAllVideosAsync(string lang, bool? trackChanges)
        {
            var video = await _manager.VideoRepository.GetAllVideosAsync(lang, trackChanges);
            return _mapper.Map<IEnumerable<VideoDto>>(video);
        }

        public async Task<IEnumerable<VideoDto>> GetAllVideosByVideoGroupIdAsync(int videoGroupId, string lang, bool? trackChanges)
        {
            var video = await _manager.VideoRepository.GetAllVideosByVideoGroupIdAsync(videoGroupId, lang, trackChanges);
            return _mapper.Map<IEnumerable<VideoDto>>(video);
        }

        public async Task<VideoDto> GetVideoByIdAsync(int id, bool? trackChanges)
        {
            var video = await _manager.VideoRepository.GetVideoByIdAsync(id, trackChanges);
            return _mapper.Map<VideoDto>(video);
        }

        public async Task<VideoDto> UpdateVideoAsync(VideoDtoForUpdate videoDtoForUpdate)
        {
            var video = await _manager.VideoRepository.GetVideoByIdAsync(videoDtoForUpdate.ID, false);
            _mapper.Map(videoDtoForUpdate, video);
            _manager.VideoRepository.UpdateVideo(video);
            await _manager.SaveAsync();
            return _mapper.Map<VideoDto>(video);
        }
    }
}
