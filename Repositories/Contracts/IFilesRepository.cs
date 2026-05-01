using Entities.Models;
using Entities.RequestFeature;
using Entities.RequestFeature.Files;

namespace Repositories.Contracts
{
    public interface IFilesRepository : IRepositoryBase<Files>
    {
        Task<PagedList<Files>> GetAllFilesAsync(FilesParameters filesParameters, bool? trackChanges);
        Task<IEnumerable<Files>> GetAllFilessByFileTypeAsync(string fileType, bool? trackChanges);
        Task<Files?> GetFilesByIdAsync(int id, bool? trackChanges);
        Files CreateFiles(Files files);
        Files UpdateFiles(Files files);
        Files DeleteFiles(Files files);
    }
}
