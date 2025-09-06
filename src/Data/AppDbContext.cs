using BackendTechnicalAssetsManagement.Model;
using BackendTechnicalAssetsManagement.src.Models;
using Microsoft.EntityFrameworkCore;


namespace BackendTechnicalAssetsManagement.src.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {

        }
        public DbSet<User> Users { get; set; }
        public DbSet<Item> Items { get; set; }
    }
}
