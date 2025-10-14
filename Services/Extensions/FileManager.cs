using Microsoft.AspNetCore.Http;

namespace Services.Extensions
{
    public class FileManager
    {
        public static async Task<List<Dictionary<string, object>>> FileUpload(
            ICollection<IFormFile> files,
            int id,
            string folder
        )
        {
            var result = new List<Dictionary<string, object>>();

            var folderPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", folder);
            if (!Directory.Exists(folderPath))
                Directory.CreateDirectory(folderPath);

            foreach (var file in files)
            {
                var fileNameWithoutExtension = Path.GetFileNameWithoutExtension(file.FileName);
                var fileExtension = Path.GetExtension(file.FileName);

                var uniqueFileName =
                    $"{fileNameWithoutExtension}-{DateTime.UtcNow:yyyyMMddHHmmss}-{id}{fileExtension}";

                var path = Path.Combine(folderPath, uniqueFileName);

                using (var stream = new FileStream(path, FileMode.Create))
                {
                    await file.CopyToAsync(stream);
                }

                var fileUrl = $"/{folder}/{uniqueFileName}";

                result.Add(
                    new Dictionary<string, object>
                    {
                        { "FilesName", uniqueFileName },
                        { "FilesPath", folderPath },
                        { "FilesFullPath", fileUrl },
                    }
                );
            }

            return result;
        }

        public static void FileDelete(string path, string file)
        {
            var filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "images", file);
            if (File.Exists(filePath))
                File.Delete(filePath);
        }
    }
}
