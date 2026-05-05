namespace Entities.DTOs.FilesDto
{
    public abstract record FilesDtoForManipulation
    {
        public string? FileUrl { get; set; }
        public string? FileType { get; set; }
    }
}
