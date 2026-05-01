using Entities.Models;
using Entities.RequestFeature;
using Entities.RequestFeature.LogEntry;

namespace Repositories.Contracts
{
    public interface ILogRepository : IRepositoryBase<Log>
    {
        Task<PagedList<Log>> GetAllLogsAsync(LogEntryParameters logParameters, bool? trackChanges);
        Task<Log?> GetLogByIdAsync(int id, bool? trackChanges);
        Log CreateLog(Log log);
        Log DeleteLog(Log log);
    }
}
