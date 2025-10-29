// The 'using' statements import necessary namespaces.
// Microsoft.EntityFrameworkCore is the core library for EF Core functionality.
// BackendTechnicalAssetsManagement.src.Classes is where your entity classes (models) are defined.
using BackendTechnicalAssetsManagement.src.Classes;
using Microsoft.EntityFrameworkCore;

namespace BackendTechnicalAssetsManagement.src.Data
{
    /// <summary>
    /// Represents the database context for the application. This class is the primary bridge
    /// between your C# entity classes and the relational database. It is responsible for
    /// querying and saving data.
    /// </summary>
    public class AppDbContext : DbContext
    {
        /// <summary>
        /// Initializes a new instance of the AppDbContext class.
        /// This constructor is used by the Dependency Injection (DI) container to pass in
        /// configuration options, such as the database provider and connection string.
        /// </summary>
        /// <param name="options">The options to be used by the DbContext.</param>
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
            // The constructor body is often empty when using DI, as the base constructor
            // handles the setup with the provided options.
        }

        // --- DbSet Properties ---
        // Each DbSet<T> property maps to a table in the database.
        // EF Core uses these properties to perform Create, Read, Update, and Delete (CRUD) operations.

        public DbSet<User> Users { get; set; }
        public DbSet<RefreshToken> RefreshTokens { get; set; }
        public DbSet<Teacher> Teachers { get; set; }
        public DbSet<Student> Students { get; set; }
        public DbSet<Staff> Staff { get; set; }
        public DbSet<Item> Items { get; set; }
        public DbSet<LentItems> LentItems { get; set; }

        //Archives
        public DbSet<ArchiveItems> ArchiveItems { get; set; }
        public DbSet<ArchiveLentItems> ArchiveLentItems { get; set; }
        public DbSet<ArchiveUser> ArchiveUsers { get; set; }

        /// <summary>
        /// Overridden method used to configure the database model and relationships using the ModelBuilder API.
        /// EF Core calls this method once when it is building its internal model of your database.
        /// </summary>
        /// <param name="modelBuilder">The builder being used to construct the model for this context.</param>
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // It's a best practice to call the base method first. This ensures any configuration
            // from the base DbContext class is applied before your custom configurations.
            base.OnModelCreating(modelBuilder);
            modelBuilder.Seed();

            // This is a custom extension method that encapsulates all the data seeding logic.
            // Calling .Seed() here executes the code from your ModelBuilderExtensions class,
            // keeping this OnModelCreating method clean and organized.
            //modelBuilder.Seed();

            // --- ENUM TO STRING CONVERSIONS ---
            // By default, EF Core stores enums in the database as integers (0, 1, 2, etc.).
            // The .HasConversion<string>() method overrides this behavior, telling EF Core
            // to store the string representation of the enum value (e.g., "Admin", "New").
            // This makes the data in the database much more human-readable.
            modelBuilder.Entity<User>()
                .Property(user => user.UserRole)
                .HasConversion<string>();

            modelBuilder.Entity<Item>()
                .Property(i => i.Condition)
                .HasConversion<string>();

            modelBuilder.Entity<Item>()
                .Property(i => i.Category)
                .HasConversion<string>();

            modelBuilder.Entity<Item>()
                .Property(i => i.Status)
                .HasConversion<string>();
            modelBuilder.Entity<LentItems>()
                .Property(li => li.Remarks)
                .HasConversion<string>();

            // --- INHERITANCE STRATEGY CONFIGURATION (Table-Per-Type) ---
            // Your User class has several derived classes (Student, Teacher, etc.).
            // These lines explicitly configure the Table-Per-Type (TPT) inheritance strategy.
            // In TPT, a base 'Users' table stores the common data, and separate tables
            // ('Students', 'Teachers', etc.) store the fields specific to each derived type.
            modelBuilder.Entity<Student>().ToTable("Students");
            modelBuilder.Entity<Teacher>().ToTable("Teachers");
            modelBuilder.Entity<Staff>().ToTable("Staff");

            modelBuilder.Entity<ArchiveStudent>().ToTable("ArchiveStudents");
            modelBuilder.Entity<ArchiveTeacher>().ToTable("ArchiveTeachers");
            modelBuilder.Entity<ArchiveStaff>().ToTable("ArchiveStaff");


            modelBuilder.Entity<ArchiveItems>(entity =>
            {
                // This line tells EF Core to convert the Category enum to a string when saving
                // and back to an enum when reading.
                entity.Property(e => e.Category)
                      .HasConversion<string>();

                // Do the same for the Condition property
                entity.Property(e => e.Condition)
                      .HasConversion<string>();

                // Convert Status enum to string as well
                entity.Property(e => e.Status)
                      .HasConversion<string>();
            });

            modelBuilder.Entity<Item>(entity =>
            {
                // This tells EF Core that the value for the 'Id' property is provided
                // by the application and is not generated by the database.
                // This is essential for the restore operation to work correctly.
                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.HasIndex(e => e.SerialNumber).IsUnique();
            });
            modelBuilder.Entity<Student>()
               .HasIndex(s => s.StudentIdNumber)
               .IsUnique()
               .HasFilter("([StudentIdNumber] IS NOT NULL AND [StudentIdNumber] <> '')");

            // TODO: This is a good place to add more advanced configurations in the future, such as:
            // - Defining complex relationships (many-to-many).
            // - Creating database indexes for performance (.HasIndex()).
            // - Setting up unique constraints (.IsUnique()).
            // - Configuring cascade delete behaviors (.OnDelete()).
        }
    }
}