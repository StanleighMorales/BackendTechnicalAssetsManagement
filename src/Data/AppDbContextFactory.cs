using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;


namespace BackendTechnicalAssetsManagement.src.Data
{
    /// <summary>
    /// Factory for creating AppDbContext instances at design-time (for EF Core tools like migrations).
    /// This respects the DatabaseProvider setting from .env file or environment variables.
    /// </summary>
    public class AppDbContextFactory : IDesignTimeDbContextFactory<AppDbContext>
    {
        public AppDbContext CreateDbContext(string[] args)
        {
            // Load .env file first (for local development)
            DotNetEnv.Env.Load();

            // Build configuration from multiple sources
            var configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", optional: true)
                .AddJsonFile("appsettings.Development.json", optional: true)
                .AddEnvironmentVariables()
                .Build();

            var optionsBuilder = new DbContextOptionsBuilder<AppDbContext>();
            
            // Get database provider setting (defaults to SqlServer for backward compatibility)
            var databaseProvider = configuration["DatabaseProvider"] ?? "SqlServer";
            string? connectionString = null;

            // Select connection string and provider based on DatabaseProvider setting
            switch (databaseProvider.ToLower())
            {
                case "sqlserver":
                    connectionString = configuration.GetConnectionString("SqlServer") 
                        ?? configuration.GetConnectionString("DefaultConnection");
                    optionsBuilder.UseSqlServer(connectionString);
                    Console.WriteLine($"[EF Core] Using SQL Server: {connectionString?.Split(';')[0]}");
                    break;

                case "postgresql":
                    connectionString = configuration.GetConnectionString("PostgreSQL") 
                        ?? configuration.GetConnectionString("DefaultConnection");
                    AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);
                    optionsBuilder.UseNpgsql(connectionString);
                    Console.WriteLine($"[EF Core] Using PostgreSQL: {connectionString?.Split(';')[0]}");
                    break;

                case "supabase":
                    connectionString = configuration.GetConnectionString("Supabase") 
                        ?? configuration.GetConnectionString("DefaultConnection");
                    AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);
                    optionsBuilder.UseNpgsql(connectionString);
                    Console.WriteLine($"[EF Core] Using Supabase: {connectionString?.Split(';')[0]}");
                    break;

                default:
                    // Fallback: Auto-detect based on connection string format
                    connectionString = configuration.GetConnectionString("DefaultConnection");
                    if (connectionString?.Contains("Host=") == true || connectionString?.Contains("host=") == true)
                    {
                        // PostgreSQL connection string
                        AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);
                        optionsBuilder.UseNpgsql(connectionString);
                        Console.WriteLine($"[EF Core] Auto-detected PostgreSQL: {connectionString?.Split(';')[0]}");
                    }
                    else
                    {
                        // SQL Server connection string
                        optionsBuilder.UseSqlServer(connectionString);
                        Console.WriteLine($"[EF Core] Auto-detected SQL Server: {connectionString?.Split(';')[0]}");
                    }
                    break;
            }

            if (string.IsNullOrEmpty(connectionString))
            {
                throw new InvalidOperationException(
                    $"Connection string not found for provider '{databaseProvider}'. " +
                    $"Please check your .env file or appsettings.json.");
            }

            return new AppDbContext(optionsBuilder.Options);
        }
    }
}
