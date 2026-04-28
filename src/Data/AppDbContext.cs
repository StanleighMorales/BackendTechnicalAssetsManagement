// The 'using' statements import necessary namespaces.
// Microsoft.EntityFrameworkCore is the core library for EF Core functionality.
// BackendTechnicalAssetsManagement.src.Classes is where your entity classes (models) are defined.
using BackendTechnicalAssetsManagement.src.Classes;
using BackendTechnicalAssetsManagement.src.Extensions;
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

        public override int SaveChanges()
        {
            SetTimestamps();
            return base.SaveChanges();
        }

        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            SetTimestamps();
            return base.SaveChangesAsync(cancellationToken);
        }

        private void SetTimestamps()
        {
            var entries = ChangeTracker.Entries()
                .Where(e => e.State == EntityState.Added || e.State == EntityState.Modified);

            foreach (var entry in entries)
            {
                if (entry.Entity.GetType().GetProperty("UpdatedAt") != null)
                {
                    entry.Property("UpdatedAt").CurrentValue = DateTime.UtcNow;
                }

                if (entry.State == EntityState.Added && entry.Entity.GetType().GetProperty("CreatedAt") != null)
                {
                    entry.Property("CreatedAt").CurrentValue = DateTime.UtcNow;
                }
            }
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

        // Activity Logs
        public DbSet<ActivityLog> ActivityLogs { get; set; }

        // RFID Cards
        public DbSet<Rfid> Rfids { get; set; }

        // RFID Registration Sessions (web-triggered, ESP32-completed)
        public DbSet<RfidRegistrationSession> RfidRegistrationSessions { get; set; }

        // Student RFID Registration Sessions (web/mobile-triggered, ESP32-completed)
        public DbSet<StudentRfidRegistrationSession> StudentRfidRegistrationSessions { get; set; }

        // Borrow Sessions (web-triggered, ESP32-completed)
        public DbSet<BorrowSession> BorrowSessions { get; set; }

        // Return Sessions (web-triggered, ESP32-completed)
        public DbSet<ReturnSession> ReturnSessions { get; set; }

        // Item Scan Sessions (web-triggered, ESP32-completed — used for guest borrow/reserve)
        public DbSet<ItemScanSession> ItemScanSessions { get; set; }

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
            
            // Seed the database with initial data
            modelBuilder.Seed();

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

            // Configure ArchiveLentItems relationships to prevent cascade delete issues
            modelBuilder.Entity<ArchiveLentItems>(entity =>
            {
                // Make Item FK optional and disable cascade delete
                // This prevents issues when the referenced item is deleted/archived
                entity.HasOne(e => e.Item)
                    .WithMany()
                    .HasForeignKey(e => e.ItemId)
                    .OnDelete(DeleteBehavior.NoAction)
                    .IsRequired(false);

                // User and Teacher FKs are already nullable, but ensure no cascade delete
                entity.HasOne(e => e.User)
                    .WithMany()
                    .HasForeignKey(e => e.UserId)
                    .OnDelete(DeleteBehavior.NoAction)
                    .IsRequired(false);

                entity.HasOne(e => e.Teacher)
                    .WithMany()
                    .HasForeignKey(e => e.TeacherId)
                    .OnDelete(DeleteBehavior.NoAction)
                    .IsRequired(false);
            });
            // Configure unique index for StudentIdNumber (PostgreSQL syntax)
            modelBuilder.Entity<Student>()
               .HasIndex(s => s.StudentIdNumber)
               .IsUnique()
               .HasFilter("(\"StudentIdNumber\" IS NOT NULL AND \"StudentIdNumber\" <> '')");

            // Index on RefreshTokens(UserId, IsRevoked) — makes RevokeAllForUserAsync and
            // GetLatestActiveTokenForUserAsync fast instead of doing a full table scan.
            modelBuilder.Entity<RefreshToken>()
                .HasIndex(rt => new { rt.UserId, rt.IsRevoked })
                .HasDatabaseName("IX_RefreshTokens_UserId_IsRevoked");

            // TODO: This is a good place to add more advanced configurations in the future, such as:
            // - Defining complex relationships (many-to-many).
            // - Creating database indexes for performance (.HasIndex()).
            // - Setting up unique constraints (.IsUnique()).
            // - Configuring cascade delete behaviors (.OnDelete()).
        }
    }
}