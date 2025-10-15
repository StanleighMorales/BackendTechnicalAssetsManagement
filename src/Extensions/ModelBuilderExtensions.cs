using BackendTechnicalAssetsManagement.src.Classes;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;

namespace BackendTechnicalAssetsManagement.src.Data
{
    /// <summary>
    /// A static class containing extension methods for <see cref="ModelBuilder"/>.
    /// This class is used to centralize and organize database seeding logic.
    /// </summary>
    public static class ModelBuilderExtensions
    {
        /// <summary>
        /// Seeds the database with initial data for development and testing purposes.
        /// This method populates users (including SuperAdmin, Admin, Staff, etc.), items,
        /// and lent item records.
        /// </summary>
        /// <param name="modelBuilder">The ModelBuilder instance from Entity Framework Core's OnModelCreating method.</param>
        public static void Seed(this ModelBuilder modelBuilder)
        {
            // --- SECTION 1: INITIAL SETUP ---

            // Generate unique identifiers (GUIDs) for the entities upfront.
            // This is essential to establish relationships between the seeded entities,
            // for example, linking a LentItem record to a specific User and a specific Item.
            var superAdminId = Guid.NewGuid();
            var adminId = Guid.NewGuid();
            var staffId = Guid.NewGuid();
            var studentId = Guid.NewGuid();
            var teacherId = Guid.NewGuid();
            var itemId1 = Guid.NewGuid();
            var itemId2 = Guid.NewGuid();

            // Instantiate the PasswordHasher provided by ASP.NET Core Identity.
            // This is the standard and secure way to hash and salt passwords before storing them.
            // Never store plain-text passwords in the database.
            var passwordHasher = new PasswordHasher<User>();

            // --- SECTION 2: USER SEEDING ---
            // This section populates the database with a set of default users,
            // each assigned a specific role and credentials for testing different access levels.

            // Seed a SuperAdmin user. This user has the highest level of privileges
            // and is intended for system-wide configuration and user management.
            

            // Seed a Staff user, representing a non-teaching employee like a lab technician.
            modelBuilder.Entity<Staff>().HasData(new Staff
            {
                Id = staffId,
                FirstName = "Jane",
                LastName = "Smith",
                Email = "staff@example.com",
                Username = "staff",
                PasswordHash = passwordHasher.HashPassword(null, "Staff@123"),
                UserRole = Enums.UserRole.Staff,
                Status = "",
                Position = "Lab Technician",
                PhoneNumber = "098-765-4321"
            });

            // Seed a Student user.
            modelBuilder.Entity<Student>().HasData(new Student
            {
                Id = studentId,
                FirstName = "Peter",
                LastName = "Jones",
                Email = "student@example.com",
                Username = "student",
                PasswordHash = passwordHasher.HashPassword(null, "Student@123"),
                UserRole = Enums.UserRole.Student,
                Status = "",
                StudentIdNumber = "2023-0001",
                Course = "Computer Science",
                Year = "3",
                Section = "A",
                Street = "123 Main St",
                CityMunicipality = "Anytown",
                Province = "Anyprovince",
                PostalCode = "12345",
                PhoneNumber = "555-123-4567"
            });

            // Seed a Teacher user.
            modelBuilder.Entity<Teacher>().HasData(new Teacher
            {
                Id = teacherId,
                FirstName = "Mary",
                LastName = "Williams",
                Email = "teacher@example.com",
                Username = "teacher",
                PasswordHash = passwordHasher.HashPassword(null, "Teacher@123"),
                UserRole = Enums.UserRole.Teacher,
                Status = "",
                Department = "Information Technology",
                PhoneNumber = "555-987-6543"
            });

            // --- SECTION 3: ITEM SEEDING ---
            // This section populates the database with sample technical assets (Items)
            // that can be borrowed by users.
            modelBuilder.Entity<Item>().HasData(
                new Item
                {
                    Id = itemId1,
                    ItemName = "Laptop",
                    ItemMake = "Dell",
                    ItemModel = "XPS 15",
                    SerialNumber = "SN123456",
                    ItemType = "Electronic",
                    Category = Enums.ItemCategory.Electronics,
                    Condition = Enums.ItemCondition.New,
                    CreatedAt = DateTime.UtcNow,
                    UpdatedAt = DateTime.UtcNow,
                    Description = "High-performance laptop for students."
                },
                new Item
                {
                    Id = itemId2,
                    ItemName = "Projector",
                    ItemMake = "Epson",
                    ItemModel = "PowerLite 1781W",
                    SerialNumber = "SN654321",
                    ItemType = "Electronic",
                    Category = Enums.ItemCategory.MediaEquipment,
                    Condition = Enums.ItemCondition.Good,
                    CreatedAt = DateTime.UtcNow,
                    UpdatedAt = DateTime.UtcNow,
                    Description = "Portable projector for classroom use."
                }
            );

            // --- SECTION 4: LENT ITEMS SEEDING ---
            // This section creates an initial transaction record to demonstrate
            // an item being lent to a user. It links a user, an item, and a teacher.
            modelBuilder.Entity<LentItems>().HasData(new LentItems
            {
                Id = Guid.NewGuid(),
                ItemId = itemId1, // The Dell XPS 15 laptop
                BorrowerFullName = "Peter Jones",
                BorrowerRole = "Student",
                StudentIdNumber = "2023-0001",
                TeacherFullName = "Mary Williams",
                TeacherId = teacherId, // Foreign key to the seeded teacher
                UserId = studentId,    // Foreign key to the seeded student
                Room = "Room 101",
                SubjectTimeSchedule = "MWF 10:00-11:00 AM",
                LentAt = DateTime.UtcNow,
                Remarks = "Borrowed",
                ReturnedAt = null // 'null' indicates the item has not yet been returned
            });
        }
    }
}