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

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Teacher>().HasBaseType<User>();
            modelBuilder.Entity<Staff>().HasBaseType<User>();
            modelBuilder.Entity<Student>().HasBaseType<User>();

            // TODO: Understand more about this later
        }
    }
}
