using BackendTechnicalAssetsManagement.src.Classes;
using Microsoft.EntityFrameworkCore;


namespace BackendTechnicalAssetsManagement.src.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {


        }
        public DbSet<User> Users { get; set; }
        public DbSet<Teacher> Teachers { get; set; }
        public DbSet<Student> Students { get; set; }
        public DbSet<Staff> Staff {  get; set; }
        public DbSet<Admin> Admins { get; set; }
        public DbSet<Item> Items { get; set; }

        public DbSet<ItemsLent> ItemsLents { get; set; }

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

            modelBuilder.Entity<Student>().ToTable("Students");
            modelBuilder.Entity<Teacher>().ToTable("Teachers");
            modelBuilder.Entity<Admin>().ToTable("Admins");
            modelBuilder.Entity<Staff>().ToTable("Staff");


            // TODO: Understand more about this later
        }
    }
}
