using BackendTechnicalAssetsManagement.src.Classes;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using static BackendTechnicalAssetsManagement.src.Classes.Enums;

namespace BackendTechnicalAssetsManagement.src.Data
{
    public static class ModelBuilderExtensions
    {
        public static void Seed(this ModelBuilder modelBuilder)
        {
            string defaultPassword = "@Pass123";
            string imagePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "image", "mockImage.png");
            byte[]? mockImageBytes = File.Exists(imagePath) ? File.ReadAllBytes(imagePath) : null;

            // ===============================
            // SECTION 1 — PREPARE ALL USERS
            // ===============================

            // --- Base Users (Admins) ---
            var users = new List<User>
            {
                new User
                {
                    Id = Guid.NewGuid(),
                    FirstName = "Super",
                    LastName = "Admin",
                    Username = "superadmin",
                    Email = "superadmin@example.com",
                    PasswordHash = BCrypt.Net.BCrypt.HashPassword(defaultPassword),
                    UserRole = UserRole.SuperAdmin,
                    Status = "Offline"
                }
            };
            for (int i = 1; i <= 5; i++)
            {
                users.Add(new User { Id = Guid.NewGuid(), FirstName = $"Admin{i}", LastName = "User", Username = $"admin{i}", Email = $"admin{i}@example.com", PasswordHash = BCrypt.Net.BCrypt.HashPassword(defaultPassword), UserRole = UserRole.Admin, Status = "Offline" });
            }

            // --- Staff ---
            var staff = new List<Staff>();
            for (int i = 1; i <= 5; i++)
            {
                staff.Add(new Staff { Id = Guid.NewGuid(), FirstName = $"Staff{i}", LastName = "Doe", Username = $"staff{i}", Email = $"staff{i}@example.com", PasswordHash = BCrypt.Net.BCrypt.HashPassword(defaultPassword), UserRole = UserRole.Staff, Position = "Lab Technician", PhoneNumber = $"0998{i}76543", Status = "Offline" });
            }

            // --- Teachers (including the specific ones we need for LentItems) ---
            var teachers = new List<Teacher>();
            var teacher1 = new Teacher { Id = Guid.NewGuid(), FirstName = "Alice", LastName = "Williams", Username = "alicewilliams", Email = "alice.williams@example.com", PasswordHash = BCrypt.Net.BCrypt.HashPassword(defaultPassword), UserRole = UserRole.Teacher, Department = "Information Technology", PhoneNumber = $"0917123456", Status = "Offline" };
            var teacher2 = new Teacher { Id = Guid.NewGuid(), FirstName = "Bob", LastName = "Brown", Username = "bobbrown", Email = "bob.brown@example.com", PasswordHash = BCrypt.Net.BCrypt.HashPassword(defaultPassword), UserRole = UserRole.Teacher, Department = "Information Technology", PhoneNumber = $"0917223456", Status = "Offline" };
            teachers.Add(teacher1);
            teachers.Add(teacher2);
            for (int i = 3; i <= 5; i++) // Add a few more generic teachers
            {
                teachers.Add(new Teacher { Id = Guid.NewGuid(), FirstName = $"Teacher{i}", LastName = "Smith", Username = $"teacher{i}", Email = $"teacher{i}@example.com", PasswordHash = BCrypt.Net.BCrypt.HashPassword(defaultPassword), UserRole = UserRole.Teacher, Department = "Information Technology", PhoneNumber = $"0917{i}23456", Status = "Offline" });
            }

            // --- Students (including the specific ones we need for LentItems) ---
            var students = new List<Student>();
            var student1 = new Student { Id = Guid.NewGuid(), FirstName = "John", LastName = "Doe", Username = "johndoe", Email = "john.doe@example.com", PasswordHash = BCrypt.Net.BCrypt.HashPassword(defaultPassword), UserRole = UserRole.Student, StudentIdNumber = "S2021-001", Course = "Computer Science", Year = "3", Section = "A", PhoneNumber = $"0912134567", Status = "Offline", ProfilePicture = mockImageBytes };
            var student2 = new Student { Id = Guid.NewGuid(), FirstName = "Jane", LastName = "Smith", Username = "janesmith", Email = "jane.smith@example.com", PasswordHash = BCrypt.Net.BCrypt.HashPassword(defaultPassword), UserRole = UserRole.Student, StudentIdNumber = "S2021-002", Course = "Computer Science", Year = "3", Section = "A", PhoneNumber = $"0912234567", Status = "Offline", ProfilePicture = mockImageBytes };
            var student3 = new Student { Id = Guid.NewGuid(), FirstName = "Peter", LastName = "Jones", Username = "peterjones", Email = "peter.jones@example.com", PasswordHash = BCrypt.Net.BCrypt.HashPassword(defaultPassword), UserRole = UserRole.Student, StudentIdNumber = "S2021-003", Course = "Computer Science", Year = "3", Section = "A", PhoneNumber = $"0912334567", Status = "Offline", ProfilePicture = mockImageBytes };
            students.Add(student1);
            students.Add(student2);
            students.Add(student3);
            for (int i = 4; i <= 5; i++) // Add a few more generic students
            {
                students.Add(new Student { Id = Guid.NewGuid(), FirstName = $"Student{i}", LastName = "Jones", Username = $"student{i}", Email = $"student{i}@example.com", PasswordHash = BCrypt.Net.BCrypt.HashPassword(defaultPassword), UserRole = UserRole.Student, StudentIdNumber = $"2023-000{i}", Course = "Computer Science", Year = "3", Section = "A", PhoneNumber = $"0912{i}34567", Status = "Offline", ProfilePicture = mockImageBytes });
            }

            // ===============================
            // SECTION 2 — COMMIT USERS TO DATABASE
            // ===============================
            modelBuilder.Entity<User>().HasData(users);
            modelBuilder.Entity<Staff>().HasData(staff);
            modelBuilder.Entity<Teacher>().HasData(teachers);
            modelBuilder.Entity<Student>().HasData(students);

            // ===============================
            // SECTION 3 — SEED ITEMS
            // ===============================
            var item1Id = Guid.NewGuid();
            var item2Id = Guid.NewGuid();
            var item3Id = Guid.NewGuid();
            var item4Id = Guid.NewGuid();
            var item5Id = Guid.NewGuid();

            modelBuilder.Entity<Item>().HasData(
                new Item { Id = item1Id, SerialNumber = "SN-LP-DELL-001", ItemName = "Dell XPS 15 Laptop", ItemType = "Laptop", ItemModel = "XPS 15 9510", ItemMake = "Dell", Description = "High-performance laptop for video editing.", Category = ItemCategory.Electronics, Condition = ItemCondition.Good, CreatedAt = DateTime.UtcNow, UpdatedAt = DateTime.UtcNow },
                new Item { Id = item2Id, SerialNumber = "SN-PR-EPS-002", ItemName = "Epson Home Cinema Projector", ItemType = "Projector", ItemModel = "HC 2250", ItemMake = "Epson", Description = "1080p projector for classroom presentations.", Category = ItemCategory.MediaEquipment, Condition = ItemCondition.New, CreatedAt = DateTime.UtcNow, UpdatedAt = DateTime.UtcNow },
                new Item { Id = item3Id, SerialNumber = "SN-CM-CAN-003", ItemName = "Canon EOS R6 Camera", ItemType = "Camera", ItemModel = "EOS R6", ItemMake = "Canon", Description = "Full-frame mirrorless camera with 4K video.", Category = ItemCategory.MediaEquipment, Condition = ItemCondition.Good, CreatedAt = DateTime.UtcNow, UpdatedAt = DateTime.UtcNow },
                new Item { Id = item4Id, SerialNumber = "SN-MC-SHR-004", ItemName = "Shure SM7B Microphone", ItemType = "Microphone", ItemModel = "SM7B", ItemMake = "Shure", Description = "Dynamic microphone for vocal recording.", Category = ItemCategory.MediaEquipment, Condition = ItemCondition.Good, CreatedAt = DateTime.UtcNow, UpdatedAt = DateTime.UtcNow },
                new Item { Id = item5Id, SerialNumber = "SN-TB-APL-005", ItemName = "Apple iPad Pro", ItemType = "Tablet", ItemModel = "iPad Pro 12.9-inch", ItemMake = "Apple", Description = "Tablet for digital art and design classes.", Category = ItemCategory.Electronics, Condition = ItemCondition.Good, CreatedAt = DateTime.UtcNow, UpdatedAt = DateTime.UtcNow }
            );

            // ===============================
            // SECTION 4 — SEED LENT ITEMS (Referencing the users created above)
            // ===============================
            modelBuilder.Entity<LentItems>().HasData(
                new LentItems { Id = Guid.NewGuid(), ItemId = item1Id, ItemName = "Dell XPS 15 Laptop", UserId = student1.Id, BorrowerFullName = $"{student1.FirstName} {student1.LastName}", BorrowerRole = "Student", StudentIdNumber = student1.StudentIdNumber, TeacherId = teacher1.Id, TeacherFullName = $"{teacher1.FirstName} {teacher1.LastName}", LentAt = DateTime.UtcNow.AddDays(-5), Status = "Borrowed" },
                new LentItems { Id = Guid.NewGuid(), ItemId = item3Id, ItemName = "Canon EOS R6 Camera", UserId = student2.Id, BorrowerFullName = $"{student2.FirstName} {student2.LastName}", BorrowerRole = "Student", StudentIdNumber = student2.StudentIdNumber, TeacherId = teacher2.Id, TeacherFullName = $"{teacher2.FirstName} {teacher2.LastName}", LentAt = DateTime.UtcNow.AddDays(-10), ReturnedAt = DateTime.UtcNow.AddDays(-2), Status = "Returned" },
                new LentItems { Id = Guid.NewGuid(), ItemId = item2Id, ItemName = "Epson Home Cinema Projector", UserId = student3.Id, BorrowerFullName = $"{student3.FirstName} {student3.LastName}", BorrowerRole = "Student", StudentIdNumber = student3.StudentIdNumber, TeacherId = teacher1.Id, TeacherFullName = $"{teacher1.FirstName} {teacher1.LastName}", LentAt = DateTime.UtcNow.AddDays(-1), Status = "Borrowed" },
                new LentItems { Id = Guid.NewGuid(), ItemId = item4Id, ItemName = "Shure SM7B Microphone", UserId = teacher1.Id, BorrowerFullName = $"{teacher1.FirstName} {teacher1.LastName}", BorrowerRole = "Teacher", LentAt = DateTime.UtcNow.AddDays(-3), Status = "Borrowed" },
                new LentItems { Id = Guid.NewGuid(), ItemId = item5Id, ItemName = "Apple iPad Pro", UserId = teacher2.Id, BorrowerFullName = $"{teacher2.FirstName} {teacher2.LastName}", BorrowerRole = "Teacher", LentAt = DateTime.UtcNow.AddDays(-30), ReturnedAt = DateTime.UtcNow.AddDays(-15), Status = "Returned" }
            );
        }
    }
}