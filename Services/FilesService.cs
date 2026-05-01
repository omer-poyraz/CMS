using AutoMapper;
using Entities.DTOs.FilesDto;
using Entities.RequestFeature;
using Entities.RequestFeature.Files;
using Repositories.Contracts;
using Services.Contracts;

namespace Services
{
    public class FilesService : IFilesService
    {
        private readonly IRepositoryManager _manager;
        private readonly IMapper _mapper;

        public FilesService(IRepositoryManager manager, IMapper mapper)
        {
            _manager = manager;
            _mapper = mapper;
        }

        public async Task<FilesDto> CreateFilesAsync(FilesDtoForInsertion filesDtoForInsertion)
        {
            var files = _mapper.Map<Entities.Models.Files>(filesDtoForInsertion);
            _manager.FilesRepository.CreateFiles(files);
            await _manager.SaveAsync();
            return _mapper.Map<FilesDto>(files);
        }

        public async Task<FilesDto> DeleteFilesAsync(int id, bool? trackChanges)
        {
            var files = await _manager.FilesRepository.GetFilesByIdAsync(id, trackChanges);
            _manager.FilesRepository.DeleteFiles(files!);
            await _manager.SaveAsync();
            return _mapper.Map<FilesDto>(files);
        }

        public async Task<(IEnumerable<FilesDto> filesDtos, MetaData metaData)> GetAllFilesAsync(FilesParameters filesParameters, bool? trackChanges)
        {
            var files = await _manager.FilesRepository.GetAllFilesAsync(filesParameters, trackChanges);
            var filesDto = _mapper.Map<IEnumerable<FilesDto>>(files);
            return (filesDto, files.MetaData);
        }

        public async Task<IEnumerable<FilesDto>> GetAllFilessFileTypeAsync(string fileType, bool? trackChanges)
        {
            var filess = await _manager.FilesRepository.GetAllFilessByFileTypeAsync(fileType, trackChanges);
            return _mapper.Map<IEnumerable<FilesDto>>(filess);
        }

        public async Task<FilesDto> GetFilesByIdAsync(int id, bool? trackChanges)
        {
            var files = await _manager.FilesRepository.GetFilesByIdAsync(id, trackChanges);
            return _mapper.Map<FilesDto>(files);
        }

        public async Task<FilesDto> UpdateFilesAsync(FilesDtoForUpdate filesDtoForUpdate)
        {
            var files = await _manager.FilesRepository.GetFilesByIdAsync(filesDtoForUpdate.ID, false);
            _mapper.Map(filesDtoForUpdate, files);
            _manager.FilesRepository.UpdateFiles(files!);
            await _manager.SaveAsync();
            return _mapper.Map<FilesDto>(files);
        }
    }
}
