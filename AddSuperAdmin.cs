/// <summary>
/// Utility script to add a Super Admin user to the database
/// 
/// Usage:
/// 1. Temporarily add this code to Program.cs before app.Run():
///    await AddSuperAdminIfNeeded(app.Services);
/// 
/// 2. Deploy or run the application once
/// 
/// 3. Remove the code after the super admin is created
/// </summary>

using BackendTechnicalAssetsManagement.src.Classes;
using BackendTechnicalAssetsManagement.src.Data;
using BackendTechnicalAssetsManagement.src.Services;
using Microsoft.EntityFrameworkCore;
using static BackendTechnicalAssetsManagement.src.Classes.Enums;

public static class SuperAdminSeeder
{
    public static async Task AddSuperAdminIfNeeded(IServiceProvider services)
    {
        using var scope = services.CreateScope();
        var context = scope.ServiceProvider.GetRequiredService<AppDbContext>();
        var passwordHasher = new PasswordHashingService();

        try
        {
            // Ensure database is created and migrations are applied
            Console.WriteLine("Checking database and applying migrations...");
            await context.Database.MigrateAsync();
            Console.WriteLine("✓ Database migrations applied successfully!");

            // Check if super admin already exists
            var existingSuperAdmin = await context.Users
                .FirstOrDefaultAsync(u => u.UserRole == UserRole.SuperAdmin);

            if (existingSuperAdmin == null)
            {
                Console.WriteLine("No SuperAdmin found. Creating default SuperAdmin...");

                var superAdmin = new User
                {
                    Id = Guid.NewGuid(),
                    Username = "admin",
                    Email = "admin@yourdomain.com",
                    FirstName = "System",
                    LastName = "Administrator",
                    PasswordHash = passwordHasher.HashPassword("@Pass123"),
                    UserRole = UserRole.SuperAdmin
                };

                context.Users.Add(superAdmin);
                await context.SaveChangesAsync();

                Console.WriteLine("✓ SuperAdmin created successfully!");
                Console.WriteLine($"  Username: {superAdmin.Username}");
                Console.WriteLine($"  Email: {superAdmin.Email}");
                Console.WriteLine("  Password: @Pass123");
                Console.WriteLine("  ⚠️  IMPORTANT: Change this password immediately after first login!");
            }
            else
            {
                Console.WriteLine($"✓ SuperAdmin already exists: {existingSuperAdmin.Username}");
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"⚠️  Error during SuperAdmin seeding: {ex.Message}");
            Console.WriteLine("The application will continue, but you may need to add a SuperAdmin manually.");
            // Don't throw - let the app continue running
        }
    }
}
