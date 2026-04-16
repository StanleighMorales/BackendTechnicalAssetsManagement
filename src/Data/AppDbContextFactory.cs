using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace BackendTechnicalAssetsManagement.src.Data
{
    /// <summary>
    /// Factory for creating AppDbContext instances at design-time (for EF Core migrations).
    /// </summary>
    public class AppDbContextFactory : IDesignTimeDbContextFactory<AppDbContext>
    {
        public AppDbContext CreateDbContext(string[] args)
        {
            DotNetEnv.Env.Load();

            var configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", optional: true)
                .AddJsonFile("appsettings.Development.json", optional: true)
                .AddEnvironmentVariables()
                .Build();

            var connectionString = configuration.GetConnectionString("Supabase")
                ?? configuration.GetConnectionString("DefaultConnection")
                ?? throw new InvalidOperationException("Supabase connection string not found. Check your .env file.");

            AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);

            var optionsBuilder = new DbContextOptionsBuilder<AppDbContext>();
            optionsBuilder.UseNpgsql(connectionString);

            Console.WriteLine($"[EF Core] Using Supabase: {connectionString.Split(';')[0]}");

            return new AppDbContext(optionsBuilder.Options);
        }
    }
}
