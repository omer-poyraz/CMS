using AutoMapper;
using Entities.DTOs.VideoGroupDto;
using Repositories.Contracts;
using Services.Contracts;

namespace Services
{
    public class VideoGroupService : IVideoGroupService
    {
        private readonly IRepositoryManager _manager;
        private readonly IMapper _mapper;

        public VideoGroupService(IRepositoryManager manager, IMapper mapper)
        {
            _manager = manager;
            _mapper = mapper;
        }

        public async Task<VideoGroupDto> CreateVideoGroupAsync(VideoGroupDtoForInsertion videoGroupDtoForInsertion)
        {
            var videoGroup = _mapper.Map<Entities.Models.VideoGroup>(videoGroupDtoForInsertion);
            _manager.VideoGroupRepository.CreateVideoGroup(videoGroup);
            await _manager.SaveAsync();
            return _mapper.Map<VideoGroupDto>(videoGroup);
        }

        public async Task<VideoGroupDto> DeleteVideoGroupAsync(int id, bool? trackChanges)
        {
            var videoGroup = await _manager.VideoGroupRepository.GetVideoGroupByIdAsync(id, trackChanges);
            _manager.VideoGroupRepository.DeleteVideoGroup(videoGroup);
            await _manager.SaveAsync();
            return _mapper.Map<VideoGroupDto>(videoGroup);
        }

        public async Task<IEnumerable<VideoGroupDto>> GetAllVideoGroupsAsync(string lang, bool? trackChanges)
        {
            var videoGroup = await _manager.VideoGroupRepository.GetAllVideoGroupsAsync(lang, trackChanges);
            return _mapper.Map<IEnumerable<VideoGroupDto>>(videoGroup);
        }

        public async Task<VideoGroupDto> GetVideoGroupByIdAsync(int id, bool? trackChanges)
        {
            var videoGroup = await _manager.VideoGroupRepository.GetVideoGroupByIdAsync(id, trackChanges);
            return _mapper.Map<VideoGroupDto>(videoGroup);
        }

        public async Task<VideoGroupDto> UpdateVideoGroupAsync(VideoGroupDtoForUpdate videoGroupDtoForUpdate)
        {
            var videoGroup = await _manager.VideoGroupRepository.GetVideoGroupByIdAsync(videoGroupDtoForUpdate.ID, false);
            _mapper.Map(videoGroupDtoForUpdate, videoGroup);
            _manager.VideoGroupRepository.UpdateVideoGroup(videoGroup);
            await _manager.SaveAsync();
            return _mapper.Map<VideoGroupDto>(videoGroup);
        }
    }
}
