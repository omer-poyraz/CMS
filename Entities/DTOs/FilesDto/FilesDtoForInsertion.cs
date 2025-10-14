using Microsoft.AspNetCore.Http;

namespace Entities.DTOs.FilesDto
{
    public record FilesDtoForInsertion : FilesDtoForManipulation
    {
        public IFormFile? file { get; set; }
        public DateTime? CreatedAt { get; init; } = DateTime.UtcNow;
    }
}
