using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;


namespace BackendTechnicalAssetsManagement.src.Data
{
    public class AppDbContextFactory : IDesignTimeDbContextFactory<AppDbContext>
    {
        public AppDbContext CreateDbContext(string[] args)
        {
            DotNetEnv.Env.Load();
            // Load configuration from appsettings + user-secrets

            var configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", optional: true)
                .AddJsonFile("appsettings.Development.json", optional: true)
                .AddEnvironmentVariables()
                .Build();

            var optionsBuilder = new DbContextOptionsBuilder<AppDbContext>();
            var connectionString = configuration.GetConnectionString("DefaultConnection");
            
            // Detect if it's PostgreSQL or SQL Server based on connection string
            if (connectionString?.Contains("Host=") == true || connectionString?.Contains("host=") == true)
            {
                // PostgreSQL connection string
                AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);
                optionsBuilder.UseNpgsql(connectionString);
            }
            else
            {
                // SQL Server connection string
                optionsBuilder.UseSqlServer(connectionString);
            }

            return new AppDbContext(optionsBuilder.Options);
        }
    }
}
