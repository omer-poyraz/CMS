using Entities.Models;

namespace Repositories.Contracts
{
    public interface IVersioningRepository : IRepositoryBase<Versioning>
    {
        Task<Versioning> GetVersioningByIdAsync(int id, bool? trackChanges);
        Versioning UpdateVersioning(Versioning versioning);
    }
}
