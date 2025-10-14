using Entities.Models;
using Microsoft.EntityFrameworkCore;
using Repositories.Contracts;

namespace Repositories.EFCore
{
    public class VideoGroupRepository : RepositoryBase<VideoGroup>, IVideoGroupRepository
    {
        public VideoGroupRepository(RepositoryContext context) : base(context) { }

        public VideoGroup CreateVideoGroup(VideoGroup videoGroup)
        {
            Create(videoGroup);
            return videoGroup;
        }

        public VideoGroup DeleteVideoGroup(VideoGroup videoGroup)
        {
            Delete(videoGroup);
            return videoGroup;
        }

        public async Task<IEnumerable<VideoGroup>> GetAllVideoGroupsAsync(string lang, bool? trackChanges)
        {
            var videoGroups = await FindAll(trackChanges).OrderBy(s => s.ID).ToListAsync();
            var dbContext = (RepositoryContext)base._context;
            foreach (var videoGroup in videoGroups)
            {
                if (videoGroup.Title is not null)
                {
                    try
                    {
                        var root = videoGroup.Title.RootElement;
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
                                    videoGroup.Title = System.Text.Json.JsonDocument.Parse(json);
                                }
                                else
                                {
                                    videoGroup.Title = System.Text.Json.JsonDocument.Parse("null");
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
            return videoGroups;
        }

        public async Task<VideoGroup> GetVideoGroupByIdAsync(int id, bool? trackChanges) =>
            await FindByCondition(s => s.ID.Equals(id), trackChanges).SingleOrDefaultAsync();

        public VideoGroup UpdateVideoGroup(VideoGroup videoGroup)
        {
            Update(videoGroup);
            return videoGroup;
        }
    }
}
