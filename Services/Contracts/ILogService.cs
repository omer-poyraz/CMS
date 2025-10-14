using Entities.DTOs.LogDto;
using Entities.Models;
using Entities.RequestFeature;
using Entities.RequestFeature.LogEntry;

namespace Services.Contracts
{
    public interface ILogService
    {
        Task<(IEnumerable<LogDto> logDtos, MetaData metaData)> GetAllLogsAsync(LogEntryParameters logParameters, bool? trackChanges);
        Task<LogDto> GetLogByIdAsync(int id, bool? trackChanges);
        Task<LogDto> CreateLogAsync(LogDtoForInsertion logDtoForInsertion);
        Task<LogDto> DeleteLogAsync(int id, bool? trackChanges);
        Task SaveLogAsync(Log log);
    }
}
