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
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<User>()
                .Property(user => user.UserRole)
                .HasConversion<string>();
           
            modelBuilder.Entity<Item>()
                .Property(i => i.Condition)
                .HasConversion<string>();

            
            modelBuilder.Entity<Item>()
                .Property(i => i.Category)
                .HasConversion<string>();



            modelBuilder.Entity<Teacher>().HasBaseType<User>();
            modelBuilder.Entity<Staff>().HasBaseType<User>();
            modelBuilder.Entity<Student>().HasBaseType<User>();

            // TODO: Understand more about this later
        }
    }
}
