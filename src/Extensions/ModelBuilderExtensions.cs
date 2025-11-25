using BackendTechnicalAssetsManagement.src.Classes;
using BackendTechnicalAssetsManagement.src.Services;
using Microsoft.EntityFrameworkCore;
using static BackendTechnicalAssetsManagement.src.Classes.Enums;

namespace BackendTechnicalAssetsManagement.src.Extensions
{
    public static class ModelBuilderExtensions
    {
        // Static flag to control seeding (can be set by tests)
        public static bool SkipSeedData { get; set; } = false;

        public static void Seed(this ModelBuilder modelBuilder)
        {
            if (SkipSeedData)
            {
                return;
            }

            // Password hashing service
            var passwordHasher = new PasswordHashingService();
            string defaultPassword = passwordHasher.HashPassword("@Pass123");

            // === USERS ===
            
            // SuperAdmin
            var superAdminId = Guid.Parse("00000000-0000-0000-0000-000000000001");
            modelBuilder.Entity<User>().HasData(new User
            {
                Id = superAdminId,
                Username = "superadmin",
                Email = "superadmin@example.com",
                FirstName = "Super",
                LastName = "Admin",
                PasswordHash = defaultPassword,
                UserRole = UserRole.SuperAdmin
            });

            // Admins
            var admin1Id = Guid.Parse("00000000-0000-0000-0000-000000000002");
            modelBuilder.Entity<User>().HasData(new User
            {
                Id = admin1Id,
                Username = "msantos",
                Email = "maria.santos@example.com",
                FirstName = "Maria",
                LastName = "Santos",
                PasswordHash = defaultPassword,
                UserRole = UserRole.Admin
            });

            var admin2Id = Guid.Parse("00000000-0000-0000-0000-000000000003");
            modelBuilder.Entity<User>().HasData(new User
            {
                Id = admin2Id,
                Username = "jdelacruz",
                Email = "juan.delacruz@example.com",
                FirstName = "Juan",
                LastName = "Dela Cruz",
                PasswordHash = defaultPassword,
                UserRole = UserRole.Admin
            });

            var admin3Id = Guid.Parse("00000000-0000-0000-0000-000000000004");
            modelBuilder.Entity<User>().HasData(new User
            {
                Id = admin3Id,
                Username = "areyes",
                Email = "ana.reyes@example.com",
                FirstName = "Ana",
                LastName = "Reyes",
                PasswordHash = defaultPassword,
                UserRole = UserRole.Admin
            });

            // Staff
            var staff1Id = Guid.Parse("00000000-0000-0000-0000-000000000005");
            modelBuilder.Entity<Staff>().HasData(new Staff
            {
                Id = staff1Id,
                Username = "cmendoza",
                Email = "carlos.mendoza@example.com",
                FirstName = "Carlos",
                LastName = "Mendoza",
                PasswordHash = defaultPassword,
                UserRole = UserRole.Staff,
                Position = "Lab Technician"
            });

            var staff2Id = Guid.Parse("00000000-0000-0000-0000-000000000006");
            modelBuilder.Entity<Staff>().HasData(new Staff
            {
                Id = staff2Id,
                Username = "rgarcia",
                Email = "rosa.garcia@example.com",
                FirstName = "Rosa",
                LastName = "Garcia",
                PasswordHash = defaultPassword,
                UserRole = UserRole.Staff,
                Position = "Equipment Manager"
            });

            var staff3Id = Guid.Parse("00000000-0000-0000-0000-000000000007");
            modelBuilder.Entity<Staff>().HasData(new Staff
            {
                Id = staff3Id,
                Username = "mtorres",
                Email = "miguel.torres@example.com",
                FirstName = "Miguel",
                LastName = "Torres",
                PasswordHash = defaultPassword,
                UserRole = UserRole.Staff,
                Position = "IT Support"
            });

            // Teachers
            var teacher1Id = Guid.Parse("00000000-0000-0000-0000-000000000008");
            modelBuilder.Entity<Teacher>().HasData(new Teacher
            {
                Id = teacher1Id,
                Username = "awilliams",
                Email = "alice.williams@example.com",
                FirstName = "Alice",
                LastName = "Williams",
                PasswordHash = defaultPassword,
                UserRole = UserRole.Teacher,
                Department = "Information Technology"
            });

            var teacher2Id = Guid.Parse("00000000-0000-0000-0000-000000000009");
            modelBuilder.Entity<Teacher>().HasData(new Teacher
            {
                Id = teacher2Id,
                Username = "rcruz",
                Email = "roberto.cruz@example.com",
                FirstName = "Roberto",
                LastName = "Cruz",
                PasswordHash = defaultPassword,
                UserRole = UserRole.Teacher,
                Department = "Computer Science"
            });

            var teacher3Id = Guid.Parse("00000000-0000-0000-0000-00000000000A");
            modelBuilder.Entity<Teacher>().HasData(new Teacher
            {
                Id = teacher3Id,
                Username = "efernandez",
                Email = "elena.fernandez@example.com",
                FirstName = "Elena",
                LastName = "Fernandez",
                PasswordHash = defaultPassword,
                UserRole = UserRole.Teacher,
                Department = "Information Technology"
            });

            var teacher4Id = Guid.Parse("00000000-0000-0000-0000-00000000000B");
            modelBuilder.Entity<Teacher>().HasData(new Teacher
            {
                Id = teacher4Id,
                Username = "dramos",
                Email = "david.ramos@example.com",
                FirstName = "David",
                LastName = "Ramos",
                PasswordHash = defaultPassword,
                UserRole = UserRole.Teacher,
                Department = "Multimedia Arts"
            });

            // Students
            var student1Id = Guid.Parse("00000000-0000-0000-0000-00000000000C");
            modelBuilder.Entity<Student>().HasData(new Student
            {
                Id = student1Id,
                Username = "jdoe",
                Email = "john.doe@student.example.com",
                FirstName = "John",
                LastName = "Doe",
                PasswordHash = defaultPassword,
                UserRole = UserRole.Student,
                StudentIdNumber = "2023-0001",
                Course = "Computer Science",
                Year = "3rd Year",
                Section = "A"
            });

            var student2Id = Guid.Parse("00000000-0000-0000-0000-00000000000D");
            modelBuilder.Entity<Student>().HasData(new Student
            {
                Id = student2Id,
                Username = "jsmith",
                Email = "jane.smith@student.example.com",
                FirstName = "Jane",
                LastName = "Smith",
                PasswordHash = defaultPassword,
                UserRole = UserRole.Student,
                StudentIdNumber = "2023-0002",
                Course = "Information Technology",
                Year = "2nd Year",
                Section = "B"
            });

            var student3Id = Guid.Parse("00000000-0000-0000-0000-00000000000E");
            modelBuilder.Entity<Student>().HasData(new Student
            {
                Id = student3Id,
                Username = "pjones",
                Email = "peter.jones@student.example.com",
                FirstName = "Peter",
                LastName = "Jones",
                PasswordHash = defaultPassword,
                UserRole = UserRole.Student,
                StudentIdNumber = "2023-0003",
                Course = "Computer Science",
                Year = "3rd Year",
                Section = "A"
            });

            var student4Id = Guid.Parse("00000000-0000-0000-0000-00000000000F");
            modelBuilder.Entity<Student>().HasData(new Student
            {
                Id = student4Id,
                Username = "mlopez",
                Email = "maria.lopez@student.example.com",
                FirstName = "Maria",
                LastName = "Lopez",
                PasswordHash = defaultPassword,
                UserRole = UserRole.Student,
                StudentIdNumber = "2023-0004",
                Course = "Multimedia Arts",
                Year = "1st Year",
                Section = "C"
            });

            var student5Id = Guid.Parse("00000000-0000-0000-0000-000000000010");
            modelBuilder.Entity<Student>().HasData(new Student
            {
                Id = student5Id,
                Username = "crivera",
                Email = "carlos.rivera@student.example.com",
                FirstName = "Carlos",
                LastName = "Rivera",
                PasswordHash = defaultPassword,
                UserRole = UserRole.Student,
                StudentIdNumber = "2023-0005",
                Course = "Information Technology",
                Year = "2nd Year",
                Section = "A"
            });

            var student6Id = Guid.Parse("00000000-0000-0000-0000-000000000011");
            modelBuilder.Entity<Student>().HasData(new Student
            {
                Id = student6Id,
                Username = "sgonzales",
                Email = "sofia.gonzales@student.example.com",
                FirstName = "Sofia",
                LastName = "Gonzales",
                PasswordHash = defaultPassword,
                UserRole = UserRole.Student,
                StudentIdNumber = "2024-0001",
                Course = "Computer Science",
                Year = "1st Year",
                Section = "B"
            });

            // === ITEMS ===
            var item1Id = Guid.Parse("10000000-0000-0000-0000-000000000001");
            var item2Id = Guid.Parse("10000000-0000-0000-0000-000000000002");
            var item3Id = Guid.Parse("10000000-0000-0000-0000-000000000003");
            var item4Id = Guid.Parse("10000000-0000-0000-0000-000000000004");
            var item5Id = Guid.Parse("10000000-0000-0000-0000-000000000005");
            var item6Id = Guid.Parse("10000000-0000-0000-0000-000000000006");
            var item7Id = Guid.Parse("10000000-0000-0000-0000-000000000007");
            var item8Id = Guid.Parse("10000000-0000-0000-0000-000000000008");

            modelBuilder.Entity<Item>().HasData(
                new Item
                {
                    Id = item1Id,
                    ItemName = "HDMI Cable 10ft",
                    SerialNumber = "SN-HDMI-001",
                    Barcode = BarcodeGeneratorService.GenerateItemBarcodeStatic("SN-HDMI-001"),
                    Category = ItemCategory.Electronics,
                    Condition = ItemCondition.Good,
                    Status = ItemStatus.Borrowed
                },
                new Item
                {
                    Id = item2Id,
                    ItemName = "Wireless Microphone",
                    SerialNumber = "SN-MIC-002",
                    Barcode = BarcodeGeneratorService.GenerateItemBarcodeStatic("SN-MIC-002"),
                    Category = ItemCategory.MediaEquipment,
                    Condition = ItemCondition.Good,
                    Status = ItemStatus.Borrowed
                },
                new Item
                {
                    Id = item3Id,
                    ItemName = "Portable Bluetooth Speaker",
                    SerialNumber = "SN-SPK-003",
                    Barcode = BarcodeGeneratorService.GenerateItemBarcodeStatic("SN-SPK-003"),
                    Category = ItemCategory.MediaEquipment,
                    Condition = ItemCondition.Good,
                    Status = ItemStatus.Available
                },
                new Item
                {
                    Id = item4Id,
                    ItemName = "Wireless Mouse",
                    SerialNumber = "SN-MOUSE-004",
                    Barcode = BarcodeGeneratorService.GenerateItemBarcodeStatic("SN-MOUSE-004"),
                    Category = ItemCategory.Electronics,
                    Condition = ItemCondition.Good,
                    Status = ItemStatus.Borrowed
                },
                new Item
                {
                    Id = item5Id,
                    ItemName = "Mechanical Keyboard",
                    SerialNumber = "SN-KB-005",
                    Barcode = BarcodeGeneratorService.GenerateItemBarcodeStatic("SN-KB-005"),
                    Category = ItemCategory.Electronics,
                    Condition = ItemCondition.Good,
                    Status = ItemStatus.Available
                },
                new Item
                {
                    Id = item6Id,
                    ItemName = "Extension Wire 15ft",
                    SerialNumber = "SN-EXT-006",
                    Barcode = BarcodeGeneratorService.GenerateItemBarcodeStatic("SN-EXT-006"),
                    Category = ItemCategory.Electronics,
                    Condition = ItemCondition.Good,
                    Status = ItemStatus.Available
                },
                new Item
                {
                    Id = item7Id,
                    ItemName = "HDMI Cable 6ft",
                    SerialNumber = "SN-HDMI-007",
                    Barcode = BarcodeGeneratorService.GenerateItemBarcodeStatic("SN-HDMI-007"),
                    Category = ItemCategory.Electronics,
                    Condition = ItemCondition.Good,
                    Status = ItemStatus.Available
                },
                new Item
                {
                    Id = item8Id,
                    ItemName = "USB Microphone",
                    SerialNumber = "SN-MIC-008",
                    Barcode = BarcodeGeneratorService.GenerateItemBarcodeStatic("SN-MIC-008"),
                    Category = ItemCategory.MediaEquipment,
                    Condition = ItemCondition.Good,
                    Status = ItemStatus.Available
                }
            );

            // === LENT ITEMS ===
            modelBuilder.Entity<LentItems>().HasData(
                // John Doe borrowed HDMI Cable (currently borrowed)
                new LentItems
                {
                    Id = Guid.Parse("20000000-0000-0000-0000-000000000001"),
                    ItemId = item1Id,
                    UserId = student1Id,
                    Barcode = "LENT-" + DateTime.Now.ToString("yyyyMMdd") + "-001",
                    Status = "Borrowed",
                    LentAt = DateTime.Now.AddDays(-5)
                },
                // Jane Smith borrowed Speaker (returned)
                new LentItems
                {
                    Id = Guid.Parse("20000000-0000-0000-0000-000000000002"),
                    ItemId = item3Id,
                    UserId = student2Id,
                    Barcode = "LENT-" + DateTime.Now.AddDays(-10).ToString("yyyyMMdd") + "-002",
                    Status = "Returned",
                    LentAt = DateTime.Now.AddDays(-10),
                    ReturnedAt = DateTime.Now.AddDays(-2)
                },
                // Peter Jones borrowed Microphone (currently borrowed)
                new LentItems
                {
                    Id = Guid.Parse("20000000-0000-0000-0000-000000000003"),
                    ItemId = item2Id,
                    UserId = student3Id,
                    Barcode = "LENT-" + DateTime.Now.ToString("yyyyMMdd") + "-003",
                    Status = "Borrowed",
                    LentAt = DateTime.Now.AddDays(-1)
                },
                // Alice Williams (Teacher) borrowed Mouse (currently borrowed)
                new LentItems
                {
                    Id = Guid.Parse("20000000-0000-0000-0000-000000000004"),
                    ItemId = item4Id,
                    UserId = teacher1Id,
                    TeacherId = teacher1Id,
                    Barcode = "LENT-" + DateTime.Now.ToString("yyyyMMdd") + "-004",
                    Status = "Borrowed",
                    LentAt = DateTime.Now.AddDays(-3)
                },
                // Roberto Cruz (Teacher) borrowed Keyboard (returned)
                new LentItems
                {
                    Id = Guid.Parse("20000000-0000-0000-0000-000000000005"),
                    ItemId = item5Id,
                    UserId = teacher2Id,
                    TeacherId = teacher2Id,
                    Barcode = "LENT-" + DateTime.Now.AddDays(-30).ToString("yyyyMMdd") + "-005",
                    Status = "Returned",
                    LentAt = DateTime.Now.AddDays(-30),
                    ReturnedAt = DateTime.Now.AddDays(-15)
                }
            );
        }
    }
}
