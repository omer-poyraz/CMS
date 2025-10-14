using AutoMapper;
using Entities.DTOs.VersioningDto;
using Repositories.Contracts;
using Services.Contracts;

namespace Services
{
    public class VersioningService : IVersioningService
    {
        private readonly IRepositoryManager _manager;
        private readonly IMapper _mapper;

        public VersioningService(IRepositoryManager manager, IMapper mapper)
        {
            _manager = manager;
            _mapper = mapper;
        }

        public async Task<VersioningDto> GetVersioningByIdAsync()
        {
            var versioning = await _manager.VersioningRepository.GetVersioningByIdAsync(1, false);
            return _mapper.Map<VersioningDto>(versioning);
        }

        public async Task<VersioningDto> UpdateVersioningAsync()
        {
            var versioning = await _manager.VersioningRepository.GetVersioningByIdAsync(1, false);
            versioning.Version = versioning.Version + 1;
            versioning.UpdatedAt = DateTime.UtcNow;
            _manager.VersioningRepository.UpdateVersioning(versioning);
            await _manager.SaveAsync();
            return _mapper.Map<VersioningDto>(versioning);
        }
    }
}
