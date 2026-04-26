using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Repositories.EFCore;

namespace API.ContextFactory
{
    public class RepositoryContextFactory : IDesignTimeDbContextFactory<RepositoryContext>
    {
        public RepositoryContext CreateDbContext(string[] args)
        {
            var config = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json")
                .Build();

            var build = new DbContextOptionsBuilder<RepositoryContext>().UseNpgsql(config.GetConnectionString("DefaultConnection"), prj => prj.MigrationsAssembly("API"));

            return new RepositoryContext(build.Options);
        }
    }
}
