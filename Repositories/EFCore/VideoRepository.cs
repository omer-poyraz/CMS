using Entities.Models;
using Microsoft.EntityFrameworkCore;
using Repositories.Contracts;

namespace Repositories.EFCore
{
    public class VideoRepository : RepositoryBase<Video>, IVideoRepository
    {
        public VideoRepository(RepositoryContext context) : base(context) { }

        public Video CreateVideo(Video video)
        {
            Create(video);
            return video;
        }

        public Video DeleteVideo(Video video)
        {
            Delete(video);
            return video;
        }

        public async Task<IEnumerable<Video>> GetAllVideosAsync(string lang, bool? trackChanges)
        {
            var videos = await FindAll(trackChanges).OrderBy(s => s.ID).ToListAsync();
            var dbContext = (RepositoryContext)base._context;
            foreach (var video in videos)
            {
                if (video.Title is not null)
                {
                    try
                    {
                        var root = video.Title.RootElement;
                        if (root.ValueKind == System.Text.Json.JsonValueKind.Array)
                        {
                            var idList = new List<int>();
                            foreach (var el in root.EnumerateArray())
                            {
                                if (el.ValueKind == System.Text.Json.JsonValueKind.Number && el.TryGetInt32(out int id))
                                    idList.Add(id);
                            }
                            if (idList.Count > 0)
                            {
                                var contents = dbContext.Contents
                                    .Where(c => idList.Contains(c.ID))
                                    .Select(c => new
                                    {
                                        code = c.Code,
                                        value = c.Value,
                                        type = c.Type
                                    })
                                    .ToList();
                                var langLower = lang?.ToLowerInvariant();
                                var match = contents.FirstOrDefault(c => c.code?.ToLowerInvariant() == langLower);
                                if (match != null)
                                {
                                    var json = System.Text.Json.JsonSerializer.Serialize(match);
                                    video.Title = System.Text.Json.JsonDocument.Parse(json);
                                }
                                else
                                {
                                    video.Title = System.Text.Json.JsonDocument.Parse("null");
                                }
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        throw new Exception("Error parsing Title JSON", ex);
                    }
                }
            }
            return videos;
        }

        public async Task<IEnumerable<Video>> GetAllVideosByVideoGroupIdAsync(int videoGroupId, string lang, bool? trackChanges)
        {
            var videos = await FindAll(trackChanges).Where(s => s.VideoGroupID == videoGroupId).OrderBy(s => s.ID).ToListAsync();
            var dbContext = (RepositoryContext)base._context;
            foreach (var video in videos)
            {
                if (video.Title is not null)
                {
                    try
                    {
                        var root = video.Title.RootElement;
                        if (root.ValueKind == System.Text.Json.JsonValueKind.Array)
                        {
                            var idList = new List<int>();
                            foreach (var el in root.EnumerateArray())
                            {
                                if (el.ValueKind == System.Text.Json.JsonValueKind.Number && el.TryGetInt32(out int id))
                                    idList.Add(id);
                            }
                            if (idList.Count > 0)
                            {
                                var contents = dbContext.Contents
                                    .Where(c => idList.Contains(c.ID))
                                    .Select(c => new
                                    {
                                        code = c.Code,
                                        value = c.Value,
                                        type = c.Type
                                    })
                                    .ToList();
                                var langLower = lang?.ToLowerInvariant();
                                var match = contents.FirstOrDefault(c => c.code?.ToLowerInvariant() == langLower);
                                if (match != null)
                                {
                                    var json = System.Text.Json.JsonSerializer.Serialize(match);
                                    video.Title = System.Text.Json.JsonDocument.Parse(json);
                                }
                                else
                                {
                                    video.Title = System.Text.Json.JsonDocument.Parse("null");
                                }
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        throw new Exception("Error parsing Title JSON", ex);
                    }
                }
            }
            return videos;
        }

        public async Task<Video> GetVideoByIdAsync(int id, bool? trackChanges) =>
            await FindByCondition(s => s.ID.Equals(id), trackChanges).SingleOrDefaultAsync();

        public Video UpdateVideo(Video video)
        {
            Update(video);
            return video;
        }
    }
}
