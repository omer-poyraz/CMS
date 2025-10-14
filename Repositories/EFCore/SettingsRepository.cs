using System.Text.Json;
using Entities.Models;
using Microsoft.EntityFrameworkCore;
using Repositories.Contracts;

namespace Repositories.EFCore
{
    public class SettingsRepository : RepositoryBase<Settings>, ISettingsRepository
    {
        public SettingsRepository(RepositoryContext context) : base(context) { }

        public Settings CreateSettings(Settings settings)
        {
            Create(settings);
            return settings;
        }

        public Settings DeleteSettings(Settings settings)
        {
            Delete(settings);
            return settings;
        }

        public async Task<IEnumerable<Settings>> GetAllSettingsAsync(bool? trackChanges)
        {
            return await FindAll(trackChanges).OrderBy(s => s.ID).ToListAsync();
        }

        public async Task<Settings> GetSettingsByIdAsync(int id, bool? trackChanges)
        {
            var dbContext = (RepositoryContext)base._context;
            var settings = await FindByCondition(s => s.ID.Equals(id), trackChanges).SingleOrDefaultAsync();
            if (settings == null) return null;

            async Task<System.Text.Json.JsonDocument?> ReplaceContentIds(System.Text.Json.JsonDocument? doc)
            {
                if (doc is null) return null;
                var root = doc.RootElement;
                if (root.ValueKind == System.Text.Json.JsonValueKind.Number && root.TryGetInt32(out int id))
                {
                    var content = await dbContext.Contents.FindAsync(id);
                    var obj = content != null ? new { content.Code, content.Value, content.Type } : null;
                    return System.Text.Json.JsonDocument.Parse(System.Text.Json.JsonSerializer.Serialize(obj));
                }
                else if (root.ValueKind == System.Text.Json.JsonValueKind.Object)
                {
                    var obj = new Dictionary<string, object?>();
                    foreach (var prop in root.EnumerateObject())
                    {
                        if (prop.Value.ValueKind == System.Text.Json.JsonValueKind.Number && prop.Value.TryGetInt32(out int id2))
                        {
                            var content = await dbContext.Contents.FindAsync(id2);
                            obj[prop.Name] = content != null ? new { content.Code, content.Value, content.Type } : null;
                        }
                        else if (prop.Value.ValueKind == System.Text.Json.JsonValueKind.Array)
                        {
                            var arr = new List<object?>();
                            foreach (var el in prop.Value.EnumerateArray())
                            {
                                if (el.ValueKind == System.Text.Json.JsonValueKind.Number && el.TryGetInt32(out int id3))
                                {
                                    var content = await dbContext.Contents.FindAsync(id3);
                                    arr.Add(content != null ? new { content.Code, content.Value, content.Type } : null);
                                }
                                else arr.Add(el);
                            }
                            obj[prop.Name] = arr;
                        }
                        else obj[prop.Name] = prop.Value;
                    }
                    return System.Text.Json.JsonDocument.Parse(System.Text.Json.JsonSerializer.Serialize(obj));
                }
                else if (root.ValueKind == System.Text.Json.JsonValueKind.Array)
                {
                    var arr = new List<object?>();
                    foreach (var el in root.EnumerateArray())
                    {
                        if (el.ValueKind == System.Text.Json.JsonValueKind.Number && el.TryGetInt32(out int id4))
                        {
                            var content = await dbContext.Contents.FindAsync(id4);
                            arr.Add(content != null ? new { content.Code, content.Value, content.Type } : null);
                        }
                        else arr.Add(el);
                    }
                    return System.Text.Json.JsonDocument.Parse(System.Text.Json.JsonSerializer.Serialize(arr));
                }
                return doc;
            }

            async Task<System.Text.Json.JsonDocument?> ReplaceMenuGroupIds(System.Text.Json.JsonDocument? doc)
            {
                if (doc is null) return null;
                var root = doc.RootElement;
                if (root.ValueKind == System.Text.Json.JsonValueKind.Number && root.TryGetInt32(out int id))
                {
                    var mg = await dbContext.MenuGroups.Include(m => m.Menus).FirstOrDefaultAsync(m => m.ID == id);
                    if (mg != null && mg.Menus != null)
                    {
                        foreach (var menu in mg.Menus)
                        {
                            menu.MenuGroup = null;
                        }
                    }
                    return System.Text.Json.JsonDocument.Parse(System.Text.Json.JsonSerializer.Serialize(mg));
                }
                else if (root.ValueKind == System.Text.Json.JsonValueKind.Object)
                {
                    var obj = new Dictionary<string, object?>();
                    foreach (var prop in root.EnumerateObject())
                    {
                        if (prop.Value.ValueKind == System.Text.Json.JsonValueKind.Number && prop.Value.TryGetInt32(out int id2))
                        {
                            var mg = await dbContext.MenuGroups.Include(m => m.Menus).FirstOrDefaultAsync(m => m.ID == id2);
                            if (mg != null && mg.Menus != null)
                            {
                                foreach (var menu in mg.Menus)
                                {
                                    menu.MenuGroup = null;
                                }
                            }
                            obj[prop.Name] = mg;
                        }
                        else if (prop.Value.ValueKind == System.Text.Json.JsonValueKind.Array)
                        {
                            var arr = new List<object?>();
                            foreach (var el in prop.Value.EnumerateArray())
                            {
                                if (el.ValueKind == System.Text.Json.JsonValueKind.Number && el.TryGetInt32(out int id3))
                                {
                                    var mg = await dbContext.MenuGroups.Include(m => m.Menus).FirstOrDefaultAsync(m => m.ID == id3);
                                    if (mg != null && mg.Menus != null)
                                    {
                                        foreach (var menu in mg.Menus)
                                        {
                                            menu.MenuGroup = null;
                                        }
                                    }
                                    arr.Add(mg);
                                }
                                else arr.Add(el);
                            }
                            obj[prop.Name] = arr;
                        }
                        else obj[prop.Name] = prop.Value;
                    }
                    return System.Text.Json.JsonDocument.Parse(System.Text.Json.JsonSerializer.Serialize(obj));
                }
                else if (root.ValueKind == System.Text.Json.JsonValueKind.Array)
                {
                    var arr = new List<object?>();
                    foreach (var el in root.EnumerateArray())
                    {
                        if (el.ValueKind == System.Text.Json.JsonValueKind.Number && el.TryGetInt32(out int id4))
                        {
                            var mg = await dbContext.MenuGroups.Include(m => m.Menus).FirstOrDefaultAsync(m => m.ID == id4);
                            if (mg != null && mg.Menus != null)
                            {
                                foreach (var menu in mg.Menus)
                                {
                                    menu.MenuGroup = null;
                                }
                            }
                            arr.Add(mg);
                        }
                        else arr.Add(el);
                    }
                    return System.Text.Json.JsonDocument.Parse(System.Text.Json.JsonSerializer.Serialize(arr));
                }
                return doc;
            }

            settings.Menu = await ReplaceMenuGroupIds(settings.Menu);
            settings.Footer = await ReplaceMenuGroupIds(settings.Footer);

            settings.SiteName = await ReplaceContentIds(settings.SiteName);
            settings.Meta = await ReplaceContentIds(settings.Meta);
            settings.Files = await ReplaceContentIds(settings.Files);
            settings.Contact = await ReplaceContentIds(settings.Contact);
            settings.Location = await ReplaceContentIds(settings.Location);
            settings.References = await ReplaceContentIds(settings.References);
            settings.SocialMedias = await ReplaceContentIds(settings.SocialMedias);
            settings.User = await ReplaceContentIds(settings.User);
            return settings;
        }

        public async Task<Settings> GetSettingsAsync(bool? trackChanges)
        {
            var dbContext = (RepositoryContext)base._context;
            var settings = await FindByCondition(s => s.ID.Equals(1), trackChanges).SingleOrDefaultAsync();
            if (settings == null) return null;

            async Task<System.Text.Json.JsonDocument?> ReplaceContentIds(System.Text.Json.JsonDocument? doc)
            {
                if (doc is null) return null;
                var root = doc.RootElement;
                if (root.ValueKind == System.Text.Json.JsonValueKind.Object)
                {
                    var obj = new Dictionary<string, object?>();
                    foreach (var prop in root.EnumerateObject())
                    {
                        if (prop.Value.ValueKind == System.Text.Json.JsonValueKind.Number && prop.Value.TryGetInt32(out int id))
                        {
                            var content = await dbContext.Contents.FindAsync(id);
                            obj[prop.Name] = content != null ? new { content.Code, content.Value, content.Type } : null;
                        }
                        else if (prop.Value.ValueKind == System.Text.Json.JsonValueKind.Array)
                        {
                            var arr = new List<object?>();
                            foreach (var el in prop.Value.EnumerateArray())
                            {
                                if (el.ValueKind == System.Text.Json.JsonValueKind.Number && el.TryGetInt32(out int id2))
                                {
                                    var content = await dbContext.Contents.FindAsync(id2);
                                    arr.Add(content != null ? new { content.Code, content.Value, content.Type } : null);
                                }
                                else arr.Add(el);
                            }
                            obj[prop.Name] = arr;
                        }
                        else obj[prop.Name] = prop.Value;
                    }
                    return JsonDocument.Parse(System.Text.Json.JsonSerializer.Serialize(obj));
                }
                else if (root.ValueKind == System.Text.Json.JsonValueKind.Array)
                {
                    var arr = new List<object?>();
                    foreach (var el in root.EnumerateArray())
                    {
                        if (el.ValueKind == System.Text.Json.JsonValueKind.Number && el.TryGetInt32(out int id))
                        {
                            var content = await dbContext.Contents.FindAsync(id);
                            arr.Add(content != null ? new { content.Code, content.Value, content.Type } : null);
                        }
                        else arr.Add(el);
                    }
                    return JsonDocument.Parse(System.Text.Json.JsonSerializer.Serialize(arr));
                }
                return doc;
            }

            async Task<System.Text.Json.JsonDocument?> ReplaceMenuGroupIds(System.Text.Json.JsonDocument? doc)
            {
                if (doc is null) return null;
                var root = doc.RootElement;
                if (root.ValueKind == System.Text.Json.JsonValueKind.Object)
                {
                    var obj = new Dictionary<string, object?>();
                    foreach (var prop in root.EnumerateObject())
                    {
                        if (prop.Value.ValueKind == System.Text.Json.JsonValueKind.Number && prop.Value.TryGetInt32(out int id))
                        {
                            var mg = await dbContext.MenuGroups.Include(m => m.Menus).FirstOrDefaultAsync(m => m.ID == id);
                            obj[prop.Name] = mg;
                        }
                        else if (prop.Value.ValueKind == System.Text.Json.JsonValueKind.Array)
                        {
                            var arr = new List<object?>();
                            foreach (var el in prop.Value.EnumerateArray())
                            {
                                if (el.ValueKind == System.Text.Json.JsonValueKind.Number && el.TryGetInt32(out int id2))
                                {
                                    var mg = await dbContext.MenuGroups.Include(m => m.Menus).FirstOrDefaultAsync(m => m.ID == id2);
                                    arr.Add(mg);
                                }
                                else arr.Add(el);
                            }
                            obj[prop.Name] = arr;
                        }
                        else obj[prop.Name] = prop.Value;
                    }
                    return JsonDocument.Parse(System.Text.Json.JsonSerializer.Serialize(obj));
                }
                else if (root.ValueKind == System.Text.Json.JsonValueKind.Array)
                {
                    var arr = new List<object?>();
                    foreach (var el in root.EnumerateArray())
                    {
                        if (el.ValueKind == System.Text.Json.JsonValueKind.Number && el.TryGetInt32(out int id))
                        {
                            var mg = await dbContext.MenuGroups.Include(m => m.Menus).FirstOrDefaultAsync(m => m.ID == id);
                            arr.Add(mg);
                        }
                        else arr.Add(el);
                    }
                    return JsonDocument.Parse(System.Text.Json.JsonSerializer.Serialize(arr));
                }
                return doc;
            }

            settings.Menu = await ReplaceMenuGroupIds(settings.Menu);
            settings.Footer = await ReplaceMenuGroupIds(settings.Footer);

            settings.SiteName = await ReplaceContentIds(settings.SiteName);
            settings.Meta = await ReplaceContentIds(settings.Meta);
            settings.Files = await ReplaceContentIds(settings.Files);
            settings.Contact = await ReplaceContentIds(settings.Contact);
            settings.Location = await ReplaceContentIds(settings.Location);
            settings.References = await ReplaceContentIds(settings.References);
            settings.SocialMedias = await ReplaceContentIds(settings.SocialMedias);
            settings.User = await ReplaceContentIds(settings.User);
            return settings;
        }

        public Settings UpdateSettings(Settings settings)
        {
            Update(settings);
            return settings;
        }
    }
}
