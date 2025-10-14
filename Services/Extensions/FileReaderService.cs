namespace Services.Extensions
{
    public class FileReaderService
    {
        public async Task<string> ReadFileAsync(string file)
        {
            string newPath = Path.Combine(Directory.GetCurrentDirectory(), file);
            if (File.Exists(newPath))
            {
                return await File.ReadAllTextAsync(newPath);
            }

            return " ";
        }
    }
}
