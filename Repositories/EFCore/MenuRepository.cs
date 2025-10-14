using Entities.Models;
using Microsoft.EntityFrameworkCore;
using Repositories.Contracts;

namespace Repositories.EFCore
{
    public class MenuRepository : RepositoryBase<Menu>, IMenuRepository
    {
        public MenuRepository(RepositoryContext context) : base(context) { }

        public Menu CreateMenu(Menu menu)
        {
            Create(menu);
            return menu;
        }

        public Menu DeleteMenu(Menu menu)
        {
            Delete(menu);
            return menu;
        }

        public async Task<IEnumerable<Menu>> GetAllMenusAsync(bool? trackChanges)
        {
            var menus = await FindAll(trackChanges).OrderBy(s => s.ID).Include(s => s.MenuGroup).Include(s => s.SubMenus).ToListAsync();
            var rootMenus = menus.Where(c => !c.ParentMenuId.HasValue).ToList();

            foreach (var menu in rootMenus)
            {
                await AddSubMenusAsync(menu, menus);
                await PopulateMenuContentAsync(menu);
            }

            return rootMenus;
        }

        public async Task<IEnumerable<Menu>> GetAllMenusByGroupAsync(int id, bool? trackChanges)
        {
            var menus = await FindAll(trackChanges).Where(s => s.MenuGroupID.Equals(id)).OrderBy(s => s.ID).Include(s => s.SubMenus).ToListAsync();
            var rootMenus = menus.Where(c => !c.ParentMenuId.HasValue).ToList();

            foreach (var menu in rootMenus)
            {
                await AddSubMenusAsync(menu, menus);
                await PopulateMenuContentAsync(menu);
            }

            return rootMenus;
        }

        public async Task<Menu> GetMenuByIdAsync(int id, bool? trackChanges)
        {
            var menu = await FindByCondition(s => s.ID.Equals(id), trackChanges).Include(s => s.MenuGroup).SingleOrDefaultAsync();
            if (menu == null) return null!;
            var allMenus = await FindAll(trackChanges).ToListAsync();
            await AddSubMenusAsync(menu, allMenus);
            await PopulateMenuContentAsync(menu);
            return menu;
        }

        public async Task<Menu> SortMenuAsync(int id, int sort, bool? trackChanges)
        {
            var menu = await FindByCondition(s => s.ID.Equals(id), trackChanges)
                .SingleOrDefaultAsync();

            if (menu == null)
                return null!;

            var existingMenuAtSort = await FindByCondition(
                    s => s.Sort.Equals(sort),
                    trackChanges
                )
                .SingleOrDefaultAsync();

            if (existingMenuAtSort != null)
            {
                var tempSort = menu.Sort;
                menu.Sort = sort;
                existingMenuAtSort.Sort = tempSort;

                Update(existingMenuAtSort);
            }
            else
            {
                menu.Sort = sort;
            }

            Update(menu);
            return menu;
        }

        public Menu UpdateMenu(Menu menu)
        {
            Update(menu);
            return menu;
        }

        private async Task AddSubMenusAsync(Menu parent, List<Menu> allMenus)
        {
            var subMenus = allMenus
                .Where(c => c.ParentMenuId == parent.ID)
                .ToList();

            foreach (var subMenu in subMenus)
            {
                await AddSubMenusAsync(subMenu, allMenus);
                await PopulateMenuContentAsync(subMenu);
            }

            parent.SubMenus = subMenus;
        }

        private async Task PopulateMenuContentAsync(Menu menu)
        {
            // Slug alanını doldur
            if (menu.Slug != null)
            {
                await PopulateJsonFieldWithContentAsync(menu.Slug, (content) => menu.Slug = content);
            }

            // Title alanını doldur
            if (menu.Title != null)
            {
                await PopulateJsonFieldWithContentAsync(menu.Title, (content) => menu.Title = content);
            }

            // SpecialField1 alanını doldur
            if (menu.SpecialField1 != null)
            {
                await PopulateJsonFieldWithContentAsync(menu.SpecialField1, (content) => menu.SpecialField1 = content);
            }

            // User alanını doldur
            if (menu.User != null)
            {
                await PopulateJsonFieldWithContentAsync(menu.User, (content) => menu.User = content);
            }
        }

        private async Task PopulateJsonFieldWithContentAsync(System.Text.Json.JsonDocument jsonField, Action<System.Text.Json.JsonDocument> setter)
        {
            var root = jsonField.RootElement;
            var contentIds = new List<int>();

            // JsonDocument'tan ID'leri çıkar
            if (root.ValueKind == System.Text.Json.JsonValueKind.Object)
            {
                foreach (var prop in root.EnumerateObject())
                {
                    if (prop.Value.ValueKind == System.Text.Json.JsonValueKind.Number && prop.Value.TryGetInt32(out int id))
                        contentIds.Add(id);
                    else if (prop.Value.ValueKind == System.Text.Json.JsonValueKind.Array)
                        contentIds.AddRange(prop.Value.EnumerateArray().Where(x => x.ValueKind == System.Text.Json.JsonValueKind.Number && x.TryGetInt32(out _)).Select(x => x.GetInt32()));
                }
            }
            else if (root.ValueKind == System.Text.Json.JsonValueKind.Array)
            {
                foreach (var el in root.EnumerateArray())
                {
                    if (el.ValueKind == System.Text.Json.JsonValueKind.Number && el.TryGetInt32(out int id))
                        contentIds.Add(id);
                }
            }
            else if (root.ValueKind == System.Text.Json.JsonValueKind.Number && root.TryGetInt32(out int singleId))
            {
                contentIds.Add(singleId);
            }

            // ID'ler varsa, ilgili Content'leri çek
            if (contentIds.Any())
            {
                var contentObjs = await _context.Contents
                    .Where(c => contentIds.Contains(c.ID))
                    .ToListAsync();

                var allProducts = await _context.Products.ToListAsync();
                
                // Her Content için Products listesini doldur
                foreach (var content in contentObjs)
                {
                    content.Products = allProducts.Where(p => p.ContentID != null && p.ContentID.Contains(content.ID)).ToList();
                }

                var ordered = contentIds
                    .Select(id => contentObjs.FirstOrDefault(x => x.ID == id))
                    .Where(x => x != null)
                    .Select(x => new { x!.Value, x.Type, x.Code, products = x.Products })
                    .ToList();

                setter(System.Text.Json.JsonDocument.Parse(System.Text.Json.JsonSerializer.Serialize(ordered)));
            }
        }
    }
}
