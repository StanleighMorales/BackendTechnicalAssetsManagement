using BackendTechnicalAssetsManagement.src.Classes;
using BackendTechnicalAssetsManagement.src.Utils;
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
            
            // Load mock image for items and users
            string imagePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "images", "mockImage.png");
            byte[]? mockImageBytes = File.Exists(imagePath) ? File.ReadAllBytes(imagePath) : null;

            // ===============================
            // SECTION 1 � PREPARE ALL USERS
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
            // SECTION 2 � COMMIT USERS TO DATABASE
            // ===============================
            modelBuilder.Entity<User>().HasData(users);
            modelBuilder.Entity<Staff>().HasData(staff);
            modelBuilder.Entity<Teacher>().HasData(teachers);
            modelBuilder.Entity<Student>().HasData(students);

            // ===============================
            // SECTION 3 � SEED ITEMS
            // ===============================
            var item1Id = Guid.NewGuid();
            var item2Id = Guid.NewGuid();
            var item3Id = Guid.NewGuid();
            var item4Id = Guid.NewGuid();
            var item5Id = Guid.NewGuid();
            var item6Id = Guid.NewGuid();
            var item7Id = Guid.NewGuid();
            var item8Id = Guid.NewGuid();

            var now = DateTime.Now;

            // Generate barcodes and barcode images for items
            var barcode1 = BarcodeGenerator.GenerateItemBarcode("SN-HDMI-001");
            var barcodeImage1 = BarcodeImageUtil.GenerateBarcodeImageBytes(barcode1);

            modelBuilder.Entity<Item>().HasData(
                new Item 
                { 
                    Id = item1Id, 
                    SerialNumber = "SN-HDMI-001", 
                    ItemName = "HDMI Cable 10ft", 
                    ItemType = "Cable", 
                    ItemModel = "HDMI 2.0", 
                    ItemMake = "Belkin", 
                    Description = "10-foot HDMI cable for display connections.", 
                    Category = ItemCategory.Electronics, 
                    Condition = ItemCondition.Good, 
                    Status = ItemStatus.Borrowed, // Currently borrowed
                    Image = mockImageBytes,
                    ImageMimeType = "image/png",
                    Barcode = barcode1,
                    BarcodeImage = barcodeImage1,
                    CreatedAt = now, 
                    UpdatedAt = now 
                },
                new Item 
                { 
                    Id = item2Id, 
                    SerialNumber = "SN-MIC-002", 
                    ItemName = "Wireless Microphone", 
                    ItemType = "Microphone", 
                    ItemModel = "WM-200", 
                    ItemMake = "Shure", 
                    Description = "Wireless microphone for presentations.", 
                    Category = ItemCategory.MediaEquipment, 
                    Condition = ItemCondition.Good, 
                    Status = ItemStatus.Borrowed, // Currently borrowed
                    Image = mockImageBytes,
                    ImageMimeType = "image/png",
                    Barcode = BarcodeGenerator.GenerateItemBarcode("SN-MIC-002"),
                    BarcodeImage = BarcodeImageUtil.GenerateBarcodeImageBytes(BarcodeGenerator.GenerateItemBarcode("SN-MIC-002")),
                    CreatedAt = now, 
                    UpdatedAt = now 
                },
                new Item 
                { 
                    Id = item3Id, 
                    SerialNumber = "SN-SPK-003", 
                    ItemName = "Portable Bluetooth Speaker", 
                    ItemType = "Speaker", 
                    ItemModel = "Flip 6", 
                    ItemMake = "JBL", 
                    Description = "Portable speaker for classroom audio.", 
                    Category = ItemCategory.MediaEquipment, 
                    Condition = ItemCondition.Good, 
                    Status = ItemStatus.Available, // Returned
                    Image = mockImageBytes,
                    ImageMimeType = "image/png",
                    Barcode = BarcodeGenerator.GenerateItemBarcode("SN-SPK-003"),
                    BarcodeImage = BarcodeImageUtil.GenerateBarcodeImageBytes(BarcodeGenerator.GenerateItemBarcode("SN-SPK-003")),
                    CreatedAt = now, 
                    UpdatedAt = now 
                },
                new Item 
                { 
                    Id = item4Id, 
                    SerialNumber = "SN-MOUSE-004", 
                    ItemName = "Wireless Mouse", 
                    ItemType = "Mouse", 
                    ItemModel = "MX Master 3", 
                    ItemMake = "Logitech", 
                    Description = "Ergonomic wireless mouse.", 
                    Category = ItemCategory.Electronics, 
                    Condition = ItemCondition.Good, 
                    Status = ItemStatus.Borrowed, // Currently borrowed
                    Image = mockImageBytes,
                    ImageMimeType = "image/png",
                    Barcode = BarcodeGenerator.GenerateItemBarcode("SN-MOUSE-004"),
                    BarcodeImage = BarcodeImageUtil.GenerateBarcodeImageBytes(BarcodeGenerator.GenerateItemBarcode("SN-MOUSE-004")),
                    CreatedAt = now, 
                    UpdatedAt = now 
                },
                new Item 
                { 
                    Id = item5Id, 
                    SerialNumber = "SN-KB-005", 
                    ItemName = "Mechanical Keyboard", 
                    ItemType = "Keyboard", 
                    ItemModel = "K380", 
                    ItemMake = "Logitech", 
                    Description = "Compact mechanical keyboard.", 
                    Category = ItemCategory.Electronics, 
                    Condition = ItemCondition.Good, 
                    Status = ItemStatus.Available, // Returned
                    Image = mockImageBytes,
                    ImageMimeType = "image/png",
                    Barcode = BarcodeGenerator.GenerateItemBarcode("SN-KB-005"),
                    BarcodeImage = BarcodeImageUtil.GenerateBarcodeImageBytes(BarcodeGenerator.GenerateItemBarcode("SN-KB-005")),
                    CreatedAt = now, 
                    UpdatedAt = now 
                },
                new Item 
                { 
                    Id = item6Id, 
                    SerialNumber = "SN-EXT-006", 
                    ItemName = "Extension Wire 15ft", 
                    ItemType = "Extension Wire", 
                    ItemModel = "Heavy Duty", 
                    ItemMake = "Belkin", 
                    Description = "15-foot extension cord with surge protection.", 
                    Category = ItemCategory.Electronics, 
                    Condition = ItemCondition.Good, 
                    Status = ItemStatus.Available,
                    Image = mockImageBytes,
                    ImageMimeType = "image/png",
                    Barcode = BarcodeGenerator.GenerateItemBarcode("SN-EXT-006"),
                    BarcodeImage = BarcodeImageUtil.GenerateBarcodeImageBytes(BarcodeGenerator.GenerateItemBarcode("SN-EXT-006")),
                    CreatedAt = now, 
                    UpdatedAt = now 
                },
                new Item 
                { 
                    Id = item7Id, 
                    SerialNumber = "SN-HDMI-007", 
                    ItemName = "HDMI Cable 6ft", 
                    ItemType = "Cable", 
                    ItemModel = "HDMI 2.1", 
                    ItemMake = "AmazonBasics", 
                    Description = "6-foot HDMI cable for short connections.", 
                    Category = ItemCategory.Electronics, 
                    Condition = ItemCondition.New, 
                    Status = ItemStatus.Available,
                    Image = mockImageBytes,
                    ImageMimeType = "image/png",
                    Barcode = BarcodeGenerator.GenerateItemBarcode("SN-HDMI-007"),
                    BarcodeImage = BarcodeImageUtil.GenerateBarcodeImageBytes(BarcodeGenerator.GenerateItemBarcode("SN-HDMI-007")),
                    CreatedAt = now, 
                    UpdatedAt = now 
                },
                new Item 
                { 
                    Id = item8Id, 
                    SerialNumber = "SN-MIC-008", 
                    ItemName = "USB Microphone", 
                    ItemType = "Microphone", 
                    ItemModel = "Yeti", 
                    ItemMake = "Blue", 
                    Description = "USB condenser microphone for recording.", 
                    Category = ItemCategory.MediaEquipment, 
                    Condition = ItemCondition.Good, 
                    Status = ItemStatus.Available,
                    Image = mockImageBytes,
                    ImageMimeType = "image/png",
                    Barcode = BarcodeGenerator.GenerateItemBarcode("SN-MIC-008"),
                    BarcodeImage = BarcodeImageUtil.GenerateBarcodeImageBytes(BarcodeGenerator.GenerateItemBarcode("SN-MIC-008")),
                    CreatedAt = now, 
                    UpdatedAt = now 
                }
            );

            // ===============================
            // SECTION 4 � SEED LENT ITEMS (Referencing the users created above)
            // ===============================
            var lentDate1 = now.AddDays(-5);
            var lentDate2 = now.AddDays(-10);
            var lentDate3 = now.AddDays(-1);
            var lentDate4 = now.AddDays(-3);
            var lentDate5 = now.AddDays(-30);

            // Generate barcodes for lent items
            var lentBarcode1 = $"LENT-{lentDate1:yyyyMMdd}-001";
            var lentBarcode2 = $"LENT-{lentDate2:yyyyMMdd}-001";
            var lentBarcode3 = $"LENT-{lentDate3:yyyyMMdd}-001";
            var lentBarcode4 = $"LENT-{lentDate4:yyyyMMdd}-001";
            var lentBarcode5 = $"LENT-{lentDate5:yyyyMMdd}-001";

            modelBuilder.Entity<LentItems>().HasData(
                // Student 1 borrowed HDMI Cable - Currently Borrowed
                new LentItems 
                { 
                    Id = Guid.NewGuid(), 
                    ItemId = item1Id, 
                    ItemName = "HDMI Cable 10ft", 
                    UserId = student1.Id, 
                    BorrowerFullName = $"{student1.FirstName} {student1.LastName}", 
                    BorrowerRole = "Student", 
                    StudentIdNumber = student1.StudentIdNumber, 
                    TeacherId = teacher1.Id, 
                    TeacherFullName = $"{teacher1.FirstName} {teacher1.LastName}", 
                    Room = "Room 301",
                    SubjectTimeSchedule = "CS101 - MWF 9:00-10:30 AM",
                    LentAt = lentDate1, 
                    Status = "Borrowed",
                    Barcode = lentBarcode1,
                    BarcodeImage = BarcodeImageUtil.GenerateBarcodeImageBytes(lentBarcode1),
                    IsHiddenFromUser = false
                },
                // Student 2 borrowed Speaker - Returned
                new LentItems 
                { 
                    Id = Guid.NewGuid(), 
                    ItemId = item3Id, 
                    ItemName = "Portable Bluetooth Speaker", 
                    UserId = student2.Id, 
                    BorrowerFullName = $"{student2.FirstName} {student2.LastName}", 
                    BorrowerRole = "Student", 
                    StudentIdNumber = student2.StudentIdNumber, 
                    TeacherId = teacher2.Id, 
                    TeacherFullName = $"{teacher2.FirstName} {teacher2.LastName}", 
                    Room = "Room 205",
                    SubjectTimeSchedule = "MEDIA101 - TTH 1:00-2:30 PM",
                    LentAt = lentDate2, 
                    ReturnedAt = now.AddDays(-2), 
                    Status = "Returned",
                    Barcode = lentBarcode2,
                    BarcodeImage = BarcodeImageUtil.GenerateBarcodeImageBytes(lentBarcode2),
                    IsHiddenFromUser = false
                },
                // Student 3 borrowed Wireless Microphone - Currently Borrowed
                new LentItems 
                { 
                    Id = Guid.NewGuid(), 
                    ItemId = item2Id, 
                    ItemName = "Wireless Microphone", 
                    UserId = student3.Id, 
                    BorrowerFullName = $"{student3.FirstName} {student3.LastName}", 
                    BorrowerRole = "Student", 
                    StudentIdNumber = student3.StudentIdNumber, 
                    TeacherId = teacher1.Id, 
                    TeacherFullName = $"{teacher1.FirstName} {teacher1.LastName}", 
                    Room = "Room 401",
                    SubjectTimeSchedule = "CS201 - MWF 2:00-3:30 PM",
                    LentAt = lentDate3, 
                    Status = "Borrowed",
                    Barcode = lentBarcode3,
                    BarcodeImage = BarcodeImageUtil.GenerateBarcodeImageBytes(lentBarcode3),
                    IsHiddenFromUser = false
                },
                // Teacher 1 borrowed Wireless Mouse - Currently Borrowed
                new LentItems 
                { 
                    Id = Guid.NewGuid(), 
                    ItemId = item4Id, 
                    ItemName = "Wireless Mouse", 
                    UserId = teacher1.Id, 
                    BorrowerFullName = $"{teacher1.FirstName} {teacher1.LastName}", 
                    BorrowerRole = "Teacher",
                    Room = "Room 101",
                    SubjectTimeSchedule = "Faculty Meeting - Friday 3:00 PM",
                    LentAt = lentDate4, 
                    Status = "Borrowed",
                    Barcode = lentBarcode4,
                    BarcodeImage = BarcodeImageUtil.GenerateBarcodeImageBytes(lentBarcode4),
                    IsHiddenFromUser = false
                },
                // Teacher 2 borrowed Mechanical Keyboard - Returned
                new LentItems 
                { 
                    Id = Guid.NewGuid(), 
                    ItemId = item5Id, 
                    ItemName = "Mechanical Keyboard", 
                    UserId = teacher2.Id, 
                    BorrowerFullName = $"{teacher2.FirstName} {teacher2.LastName}", 
                    BorrowerRole = "Teacher",
                    Room = "Room 302",
                    SubjectTimeSchedule = "IT Workshop - Daily",
                    LentAt = lentDate5, 
                    ReturnedAt = now.AddDays(-15), 
                    Status = "Returned",
                    Barcode = lentBarcode5,
                    BarcodeImage = BarcodeImageUtil.GenerateBarcodeImageBytes(lentBarcode5),
                    IsHiddenFromUser = false
                }
            );
        }
    }
}
