using Entities.Models;
using Entities.RequestFeature;
using Entities.RequestFeature.Files;
using Microsoft.EntityFrameworkCore;
using Repositories.Contracts;
using Repositories.EFCore.Extensions;

namespace Repositories.EFCore
{
    public class FilesRepository : RepositoryBase<Files>, IFilesRepository
    {
        public FilesRepository(RepositoryContext context) : base(context) { }

        public Files CreateFiles(Files files)
        {
            Create(files);
            return files;
        }

        public Files DeleteFiles(Files files)
        {
            Delete(files);
            return files;
        }

        public async Task<PagedList<Files>> GetAllFilesAsync(FilesParameters filesParameters, bool? trackChanges)
        {
            var files = await FindAll(trackChanges).OrderBy(s => s.ID).SearchFile(filesParameters.SearchTerm!).ToListAsync();
            return PagedList<Files>.ToPagedList(files, filesParameters.PageNumber, filesParameters.PageSize);
        }

        public async Task<IEnumerable<Files>> GetAllFilessByFileTypeAsync(string fileType, bool? trackChanges) =>
            await FindAll(trackChanges)
                .Where(s => s.FileType!.ToLower().Equals(fileType.ToLower()))
                .OrderBy(s => s.ID)
                .ToListAsync();

        public async Task<Files?> GetFilesByIdAsync(int id, bool? trackChanges) =>
            await FindByCondition(s => s.ID.Equals(id), trackChanges)
                .SingleOrDefaultAsync();

        public Files UpdateFiles(Files files)
        {
            Update(files);
            return files;
        }
    }
}
