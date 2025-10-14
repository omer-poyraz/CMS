using Entities.DTOs.VersioningDto;

namespace Services.Contracts
{
    public interface IVersioningService
    {
        Task<VersioningDto> GetVersioningByIdAsync();
        Task<VersioningDto> UpdateVersioningAsync();
    }
}
