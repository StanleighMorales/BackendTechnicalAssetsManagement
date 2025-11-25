using BackendTechnicalAssetsManagement.src.Classes;
using BackendTechnicalAssetsManagement.src.Utils;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using static BackendTechnicalAssetsManagement.src.Classes.Enums;

namespace BackendTechnicalAssetsManagement.src.Data
{
    public static class ModelBuilderExtensions
    {
        // Static flag to control seeding (can be set by tests)
        public static bool SkipSeedData { get; set; } = false;

        public static void Seed(this ModelBuilder modelBuilder)
        {
            // PERFORMANCE OPTIMIZATION: Skip seed data when flag is set (e.g., in tests)
            // Seed data loading takes ~2 seconds and is not needed for unit tests
            if (SkipSeedData)
            {
                return;
            }

            string defaultPassword = "@Pass123";
            // PERFORMANCE OPTIMIZATION: Pre-compute password hash once instead of 17 times
            // BCrypt is intentionally slow (~300-500ms per hash), so computing it 17 times = 5-8 seconds
            // Pre-computing reduces seed time from ~6 seconds to ~50ms
            string defaultPasswordHash = BCrypt.Net.BCrypt.HashPassword(defaultPassword);
            
            // Load mock image for items and users
            // OPTIMIZATION: Use a faster check and cache the result to avoid repeated file system access
            byte[]? mockImageBytes = null;
            string imagePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "images", "mockImage.png");
            
            try
            {
                // Fast check: Only attempt to read if the directory exists first
                string? imageDirectory = Path.GetDirectoryName(imagePath);
                if (imageDirectory != null && Directory.Exists(imageDirectory))
                {
                    if (File.Exists(imagePath))
                    {
                        mockImageBytes = File.ReadAllBytes(imagePath);
                        Console.WriteLine($"✓ Loaded mock image: {mockImageBytes.Length} bytes");
                    }
                }
                // Silently skip if directory doesn't exist (common in test environments)
            }
            catch
            {
                // Silently ignore file access errors in test/development environments
            }

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
                    PasswordHash = defaultPasswordHash,
                    UserRole = UserRole.SuperAdmin,
                    PhoneNumber = "09171234567",
                    Status = "Offline"
                },
                new User { Id = Guid.NewGuid(), FirstName = "Maria", LastName = "Santos", Username = "msantos", Email = "maria.santos@example.com", PasswordHash = defaultPasswordHash, UserRole = UserRole.Admin, PhoneNumber = "09181234567", Status = "Offline" },
                new User { Id = Guid.NewGuid(), FirstName = "Juan", LastName = "Dela Cruz", Username = "jdelacruz", Email = "juan.delacruz@example.com", PasswordHash = defaultPasswordHash, UserRole = UserRole.Admin, PhoneNumber = "09191234567", Status = "Offline" },
                new User { Id = Guid.NewGuid(), FirstName = "Ana", LastName = "Reyes", Username = "areyes", Email = "ana.reyes@example.com", PasswordHash = defaultPasswordHash, UserRole = UserRole.Admin, PhoneNumber = "09201234567", Status = "Offline" }
            };

            // --- Staff ---
            var staff = new List<Staff>
            {
                new Staff { Id = Guid.NewGuid(), FirstName = "Carlos", LastName = "Mendoza", Username = "cmendoza", Email = "carlos.mendoza@example.com", PasswordHash = defaultPasswordHash, UserRole = UserRole.Staff, Position = "Lab Technician", PhoneNumber = "09981234567", Status = "Offline" },
                new Staff { Id = Guid.NewGuid(), FirstName = "Rosa", LastName = "Garcia", Username = "rgarcia", Email = "rosa.garcia@example.com", PasswordHash = defaultPasswordHash, UserRole = UserRole.Staff, Position = "Equipment Manager", PhoneNumber = "09982234567", Status = "Offline" },
                new Staff { Id = Guid.NewGuid(), FirstName = "Miguel", LastName = "Torres", Username = "mtorres", Email = "miguel.torres@example.com", PasswordHash = defaultPasswordHash, UserRole = UserRole.Staff, Position = "IT Support", PhoneNumber = "09983234567", Status = "Offline" }
            };

            // --- Teachers (including the specific ones we need for LentItems) ---
            var teacher1 = new Teacher { Id = Guid.NewGuid(), FirstName = "Alice", LastName = "Williams", Username = "awilliams", Email = "alice.williams@example.com", PasswordHash = defaultPasswordHash, UserRole = UserRole.Teacher, Department = "Information Technology", PhoneNumber = "09171234567", Status = "Offline" };
            var teacher2 = new Teacher { Id = Guid.NewGuid(), FirstName = "Roberto", LastName = "Cruz", Username = "rcruz", Email = "roberto.cruz@example.com", PasswordHash = defaultPasswordHash, UserRole = UserRole.Teacher, Department = "Computer Science", PhoneNumber = "09172234567", Status = "Offline" };
            var teacher3 = new Teacher { Id = Guid.NewGuid(), FirstName = "Elena", LastName = "Fernandez", Username = "efernandez", Email = "elena.fernandez@example.com", PasswordHash = defaultPasswordHash, UserRole = UserRole.Teacher, Department = "Information Technology", PhoneNumber = "09173234567", Status = "Offline" };
            var teacher4 = new Teacher { Id = Guid.NewGuid(), FirstName = "David", LastName = "Ramos", Username = "dramos", Email = "david.ramos@example.com", PasswordHash = defaultPasswordHash, UserRole = UserRole.Teacher, Department = "Multimedia Arts", PhoneNumber = "09174234567", Status = "Offline" };
            
            var teachers = new List<Teacher> { teacher1, teacher2, teacher3, teacher4 };

            // --- Students (including the specific ones we need for LentItems) ---
            var student1 = new Student { Id = Guid.NewGuid(), FirstName = "John", LastName = "Doe", Username = "jdoe", Email = "john.doe@student.example.com", PasswordHash = defaultPasswordHash, UserRole = UserRole.Student, StudentIdNumber = "2023-0001", Course = "Computer Science", Year = "3", Section = "A", PhoneNumber = "09121234567", Status = "Offline", ProfilePicture = mockImageBytes, FrontStudentIdPicture = mockImageBytes, Street = "123 Main St", CityMunicipality = "Manila", Province = "Metro Manila", PostalCode = "1000" };
            var student2 = new Student { Id = Guid.NewGuid(), FirstName = "Jane", LastName = "Smith", Username = "jsmith", Email = "jane.smith@student.example.com", PasswordHash = defaultPasswordHash, UserRole = UserRole.Student, StudentIdNumber = "2023-0002", Course = "Information Technology", Year = "2", Section = "B", PhoneNumber = "09122234567", Status = "Offline", ProfilePicture = mockImageBytes, FrontStudentIdPicture = mockImageBytes, Street = "456 Oak Ave", CityMunicipality = "Quezon City", Province = "Metro Manila", PostalCode = "1100" };
            var student3 = new Student { Id = Guid.NewGuid(), FirstName = "Peter", LastName = "Jones", Username = "pjones", Email = "peter.jones@student.example.com", PasswordHash = defaultPasswordHash, UserRole = UserRole.Student, StudentIdNumber = "2023-0003", Course = "Computer Science", Year = "3", Section = "A", PhoneNumber = "09123234567", Status = "Offline", ProfilePicture = mockImageBytes, FrontStudentIdPicture = mockImageBytes, Street = "789 Pine Rd", CityMunicipality = "Makati", Province = "Metro Manila", PostalCode = "1200" };
            var student4 = new Student { Id = Guid.NewGuid(), FirstName = "Maria", LastName = "Lopez", Username = "mlopez", Email = "maria.lopez@student.example.com", PasswordHash = defaultPasswordHash, UserRole = UserRole.Student, StudentIdNumber = "2023-0004", Course = "Multimedia Arts", Year = "1", Section = "C", PhoneNumber = "09124234567", Status = "Offline", ProfilePicture = mockImageBytes, FrontStudentIdPicture = mockImageBytes, Street = "321 Elm St", CityMunicipality = "Pasig", Province = "Metro Manila", PostalCode = "1600" };
            var student5 = new Student { Id = Guid.NewGuid(), FirstName = "Carlos", LastName = "Rivera", Username = "crivera", Email = "carlos.rivera@student.example.com", PasswordHash = defaultPasswordHash, UserRole = UserRole.Student, StudentIdNumber = "2023-0005", Course = "Information Technology", Year = "4", Section = "A", PhoneNumber = "09125234567", Status = "Offline", ProfilePicture = mockImageBytes, FrontStudentIdPicture = mockImageBytes, Street = "654 Maple Dr", CityMunicipality = "Taguig", Province = "Metro Manila", PostalCode = "1630" };
            var student6 = new Student { Id = Guid.NewGuid(), FirstName = "Sofia", LastName = "Gonzales", Username = "sgonzales", Email = "sofia.gonzales@student.example.com", PasswordHash = defaultPasswordHash, UserRole = UserRole.Student, StudentIdNumber = "2024-0001", Course = "Computer Science", Year = "1", Section = "A", PhoneNumber = "09126234567", Status = "Offline", ProfilePicture = mockImageBytes, FrontStudentIdPicture = mockImageBytes, Street = "987 Cedar Ln", CityMunicipality = "Mandaluyong", Province = "Metro Manila", PostalCode = "1550" };
            
            var students = new List<Student> { student1, student2, student3, student4, student5, student6 };

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

            // PERFORMANCE OPTIMIZATION: Skip barcode image generation during seeding
            // Barcode images will be generated on-demand when items are created/updated
            // This reduces seed time from ~5 seconds to ~50ms
            var barcode1 = BarcodeGenerator.GenerateItemBarcode("SN-HDMI-001");
            byte[]? barcodeImage1 = null; // Generate on-demand instead of during seed

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
                    BarcodeImage = null, // Generated on-demand for performance
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
                    BarcodeImage = null, // Generated on-demand for performance
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
                    BarcodeImage = null, // Generated on-demand for performance
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
                    BarcodeImage = null, // Generated on-demand for performance
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
                    BarcodeImage = null, // Generated on-demand for performance
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
                    BarcodeImage = null, // Generated on-demand for performance
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
                    BarcodeImage = null, // Generated on-demand for performance
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
                    BarcodeImage = null, // Generated on-demand for performance
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
                    BarcodeImage = null, // Generated on-demand for performance
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
                    BarcodeImage = null, // Generated on-demand for performance
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
                    BarcodeImage = null, // Generated on-demand for performance
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
                    BarcodeImage = null, // Generated on-demand for performance
                    IsHiddenFromUser = false
                }
            );
        }
    }
}




