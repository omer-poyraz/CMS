using Microsoft.AspNetCore.Http;

namespace Entities.DTOs.FilesDto
{
    public record FilesDtoForUpdate : FilesDtoForManipulation
    {
        public int ID { get; init; }
        public IFormFile? file { get; set; }
        public DateTime? UpdatedAt { get; init; } = DateTime.UtcNow;
    }
}
