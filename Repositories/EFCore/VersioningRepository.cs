using Entities.Models;
using Microsoft.EntityFrameworkCore;
using Repositories.Contracts;

namespace Repositories.EFCore
{
    public class VersioningRepository : RepositoryBase<Versioning>, IVersioningRepository
    {
        public VersioningRepository(RepositoryContext context) : base(context) { }

        public async Task<Versioning> GetVersioningByIdAsync(int id, bool? trackChanges) =>
            await FindByCondition(s => s.ID.Equals(id), trackChanges).SingleOrDefaultAsync();


        public Versioning UpdateVersioning(Versioning versioning)
        {
            Update(versioning);
            return versioning;
        }
    }
}
