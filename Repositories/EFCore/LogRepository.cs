using Entities.Models;
using Entities.RequestFeature;
using Entities.RequestFeature.LogEntry;
using Microsoft.EntityFrameworkCore;
using Repositories.Contracts;
using Repositories.EFCore.Extensions;

namespace Repositories.EFCore
{
    public class LogRepository : RepositoryBase<Log>, ILogRepository
    {
        public LogRepository(RepositoryContext context) : base(context) { }

        public Log CreateLog(Log log)
        {
            Create(log);
            return log;
        }

        public Log DeleteLog(Log log)
        {
            Delete(log);
            return log;
        }

        public async Task<PagedList<Log>> GetAllLogsAsync(LogEntryParameters logParameters, bool? trackChanges)
        {
            var logs = await FindAll(trackChanges)
                .OrderByDescending(s => s.ID)
                .SearchLog(logParameters.SearchTerm!)
                .ToListAsync();
            return PagedList<Log>.ToPagedList(logs, logParameters.PageNumber, logParameters.PageSize);
        }

        public async Task<Log?> GetLogByIdAsync(int id, bool? trackChanges) =>
            await FindByCondition(s => s.ID.Equals(id), trackChanges)
                .SingleOrDefaultAsync();
    }
}
