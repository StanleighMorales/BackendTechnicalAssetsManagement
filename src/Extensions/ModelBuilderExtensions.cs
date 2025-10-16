using BackendTechnicalAssetsManagement.src.Classes;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using static BackendTechnicalAssetsManagement.src.Classes.Enums;

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
            var passwordHasher = new PasswordHasher<User>();
            string defaultPassword = "@Pass123";

            // Load mock image bytes
            string imagePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "image", "mockImage.png");
            byte[]? mockImageBytes = File.Exists(imagePath) ? File.ReadAllBytes(imagePath) : null;

            // ===============================
            // SECTION 1 — BASE USERS (Admin + SuperAdmin)
            // ===============================

            var users = new List<User>();

            // 1 SuperAdmin
            users.Add(new User
            {
                Id = Guid.NewGuid(),
                FirstName = "Super",
                LastName = "Admin",
                Username = "superadmin",
                Email = "superadmin@example.com",
                PasswordHash = BCrypt.Net.BCrypt.HashPassword(defaultPassword),
                UserRole = UserRole.SuperAdmin,
                Status = "Offline"
            });

            // 5 Admins
            for (int i = 1; i <= 5; i++)
            {
                users.Add(new User
                {
                    Id = Guid.NewGuid(),
                    FirstName = $"Admin{i}",
                    LastName = "User",
                    Username = $"admin{i}",
                    Email = $"admin{i}@example.com",
                    PasswordHash = BCrypt.Net.BCrypt.HashPassword(defaultPassword),
                    UserRole = UserRole.Admin,
                    Status = "Offline"
                });
            }

            modelBuilder.Entity<User>().HasData(users);

            // ===============================
            // SECTION 2 — TEACHERS
            // ===============================
            var teachers = new List<Teacher>();

            for (int i = 1; i <= 5; i++)
            {
                teachers.Add(new Teacher
                {
                    Id = Guid.NewGuid(),
                    FirstName = $"Teacher{i}",
                    LastName = "Smith",
                    Username = $"teacher{i}",
                    Email = $"teacher{i}@example.com",
                    PasswordHash = BCrypt.Net.BCrypt.HashPassword(defaultPassword),
                    UserRole = UserRole.Teacher,
                    Department = "Information Technology",
                    PhoneNumber = $"0917{i}23456",
                    Status = "Offline"
                });
            }

            modelBuilder.Entity<Teacher>().HasData(teachers);

            // ===============================
            // SECTION 3 — STAFF
            // ===============================
            var staff = new List<Staff>();

            for (int i = 1; i <= 5; i++)
            {
                staff.Add(new Staff
                {
                    Id = Guid.NewGuid(),
                    FirstName = $"Staff{i}",
                    LastName = "Doe",
                    Username = $"staff{i}",
                    Email = $"staff{i}@example.com",
                    PasswordHash = BCrypt.Net.BCrypt.HashPassword(defaultPassword),
                    UserRole = UserRole.Staff,
                    Position = "Lab Technician",
                    PhoneNumber = $"0998{i}76543",
                    Status = "Offline"
                });
            }

            modelBuilder.Entity<Staff>().HasData(staff);

            // ===============================
            // SECTION 4 — STUDENTS
            // ===============================
            var students = new List<Student>();

            for (int i = 1; i <= 5; i++)
            {
                students.Add(new Student
                {
                    Id = Guid.NewGuid(),
                    FirstName = $"Student{i}",
                    LastName = "Jones",
                    Username = $"student{i}",
                    Email = $"student{i}@example.com",
                    PasswordHash = BCrypt.Net.BCrypt.HashPassword(defaultPassword),
                    UserRole = UserRole.Student,
                    StudentIdNumber = $"2023-000{i}",
                    Course = "Computer Science",
                    Year = "3",
                    Section = "A",
                    Street = $"123 Main St #{i}",
                    CityMunicipality = "Sample City",
                    Province = "Sample Province",
                    PostalCode = "12345",
                    PhoneNumber = $"0912{i}34567",
                    Status = "Offline",
                    ProfilePicture = mockImageBytes,
                    FrontStudentIdPicture = mockImageBytes,
                    BackStudentIdPicture = mockImageBytes
                });
            }

            modelBuilder.Entity<Student>().HasData(students);
        }
    }
}