using Entities.DTOs.FilesDto;
using Entities.RequestFeature;
using Entities.RequestFeature.Files;

namespace Services.Contracts
{
    public interface IFilesService
    {
        Task<(IEnumerable<FilesDto> filesDtos, MetaData metaData)> GetAllFilesAsync(FilesParameters filesParameters, bool? trackChanges);
        Task<IEnumerable<FilesDto>> GetAllFilessFileTypeAsync(string fileType, bool? trackChanges);
        Task<FilesDto> GetFilesByIdAsync(int id, bool? trackChanges);
        Task<FilesDto> CreateFilesAsync(FilesDtoForInsertion filesDtoForInsertion);
        Task<FilesDto> UpdateFilesAsync(FilesDtoForUpdate filesDtoForUpdate);
        Task<FilesDto> DeleteFilesAsync(int id, bool? trackChanges);
    }
}
