using System.Reflection;
using Entities.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Repositories.EFCore
{
    public class RepositoryContext : IdentityDbContext<User>
    {
        private readonly Microsoft.AspNetCore.Http.IHttpContextAccessor? _httpContextAccessor;

        public RepositoryContext(DbContextOptions options, Microsoft.AspNetCore.Http.IHttpContextAccessor? httpContextAccessor = null) : base(options)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        public DbSet<Basket> Baskets { get; set; }
        public DbSet<Comment> Comments { get; set; }
        public DbSet<Content> Contents { get; set; }
        public DbSet<Files> Filess { get; set; }
        public DbSet<GoogleAnalytics> GaoogleAnalytics { get; set; }
        public DbSet<Language> Languages { get; set; }
        public DbSet<Log> Logs { get; set; }
        public DbSet<Menu> Menus { get; set; }
        public DbSet<MenuGroup> MenuGroups { get; set; }
        public DbSet<Page> Pages { get; set; }
        public DbSet<Popup> Popups { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<Settings> Settings { get; set; }
        public DbSet<Unit> Units { get; set; }
        public DbSet<UserProfile> UserProfiles { get; set; }
        public DbSet<Versioning> Versionings { get; set; }
        public DbSet<VideoGroup> VideoGroups { get; set; }
        public DbSet<Video> Videos { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        }

        public override int SaveChanges()
        {
            SetUserFields();
            return base.SaveChanges();
        }

        public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            SetUserFields();
            return await base.SaveChangesAsync(cancellationToken);
        }

        private void SetUserFields()
        {
            if (_httpContextAccessor == null || _httpContextAccessor.HttpContext == null || !_httpContextAccessor.HttpContext.User.Identity.IsAuthenticated)
                return;

            var user = _httpContextAccessor.HttpContext.User;
            var userId = user.Claims.FirstOrDefault(c => c.Type == "sub" || c.Type == "userId" || c.Type.EndsWith("nameidentifier"))?.Value;
            var firstName = user.Claims.FirstOrDefault(c => c.Type == "given_name" || c.Type == "firstName")?.Value;
            var lastName = user.Claims.FirstOrDefault(c => c.Type == "family_name" || c.Type == "lastName")?.Value;

            if (string.IsNullOrEmpty(userId) && string.IsNullOrEmpty(firstName) && string.IsNullOrEmpty(lastName))
                return;

            var entries = ChangeTracker.Entries()
                .Where(e => (e.State == EntityState.Added || e.State == EntityState.Modified) && e.Entity != null);

            foreach (var entry in entries)
            {
                var userProp = entry.Entity.GetType().GetProperty("User");
                if (userProp != null && userProp.PropertyType == typeof(System.Text.Json.JsonDocument))
                {
                    var userObj = new { userId, firstName, lastName };
                    var json = System.Text.Json.JsonSerializer.Serialize(userObj);
                    var doc = System.Text.Json.JsonDocument.Parse(json);
                    userProp.SetValue(entry.Entity, doc);
                }
            }
        }
    }
}
