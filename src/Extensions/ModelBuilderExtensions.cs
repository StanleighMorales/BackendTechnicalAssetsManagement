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
            if (SkipSeedData) return;

            var passwordHasher = new PasswordHashingService(workFactor: 4);
            string pw = passwordHasher.HashPassword("@Pass123");

            // =========================================================
            // FIXED GUIDs — deterministic so migrations stay stable
            // =========================================================

            // --- Users ---
            var superAdminId  = Guid.Parse("00000001-0000-0000-0000-000000000001");
            var admin1Id      = Guid.Parse("00000001-0000-0000-0000-000000000002");
            var admin2Id      = Guid.Parse("00000001-0000-0000-0000-000000000003");
            var admin3Id      = Guid.Parse("00000001-0000-0000-0000-000000000004");

            // --- Staff ---
            var staff1Id      = Guid.Parse("00000002-0000-0000-0000-000000000001");
            var staff2Id      = Guid.Parse("00000002-0000-0000-0000-000000000002");
            var staff3Id      = Guid.Parse("00000002-0000-0000-0000-000000000003");

            // --- Teachers ---
            var teacher1Id    = Guid.Parse("00000003-0000-0000-0000-000000000001");
            var teacher2Id    = Guid.Parse("00000003-0000-0000-0000-000000000002");
            var teacher3Id    = Guid.Parse("00000003-0000-0000-0000-000000000003");
            var teacher4Id    = Guid.Parse("00000003-0000-0000-0000-000000000004");

            // --- Students ---
            var student1Id    = Guid.Parse("00000004-0000-0000-0000-000000000001");
            var student2Id    = Guid.Parse("00000004-0000-0000-0000-000000000002");
            var student3Id    = Guid.Parse("00000004-0000-0000-0000-000000000003");
            var student4Id    = Guid.Parse("00000004-0000-0000-0000-000000000004");
            var student5Id    = Guid.Parse("00000004-0000-0000-0000-000000000005");
            var student6Id    = Guid.Parse("00000004-0000-0000-0000-000000000006");

            // --- Items ---
            var item1Id       = Guid.Parse("00000005-0000-0000-0000-000000000001");
            var item2Id       = Guid.Parse("00000005-0000-0000-0000-000000000002");
            var item3Id       = Guid.Parse("00000005-0000-0000-0000-000000000003");
            var item4Id       = Guid.Parse("00000005-0000-0000-0000-000000000004");
            var item5Id       = Guid.Parse("00000005-0000-0000-0000-000000000005");
            var item6Id       = Guid.Parse("00000005-0000-0000-0000-000000000006");
            var item7Id       = Guid.Parse("00000005-0000-0000-0000-000000000007");
            var item8Id       = Guid.Parse("00000005-0000-0000-0000-000000000008");

            // --- LentItems ---
            var lent1Id       = Guid.Parse("00000006-0000-0000-0000-000000000001");
            var lent2Id       = Guid.Parse("00000006-0000-0000-0000-000000000002");
            var lent3Id       = Guid.Parse("00000006-0000-0000-0000-000000000003");
            var lent4Id       = Guid.Parse("00000006-0000-0000-0000-000000000004");
            var lent5Id       = Guid.Parse("00000006-0000-0000-0000-000000000005");

            // --- Archive IDs (separate from originals) ---
            var archiveUser1Id      = Guid.Parse("00000007-0000-0000-0000-000000000001"); // archived admin3
            var archiveStaff1Id     = Guid.Parse("00000007-0000-0000-0000-000000000002"); // archived staff3
            var archiveTeacher1Id   = Guid.Parse("00000007-0000-0000-0000-000000000003"); // archived teacher4
            var archiveStudent1Id   = Guid.Parse("00000007-0000-0000-0000-000000000004"); // archived student6
            var archiveItem1Id      = Guid.Parse("00000007-0000-0000-0000-000000000005"); // archived item7
            var archiveItem2Id      = Guid.Parse("00000007-0000-0000-0000-000000000006"); // archived item8
            var archiveLent1Id      = Guid.Parse("00000007-0000-0000-0000-000000000007"); // archived lent5

            var archivedAt = new DateTime(2025, 1, 15, 10, 0, 0, DateTimeKind.Utc);

            // =========================================================
            // USERS
            // =========================================================

            modelBuilder.Entity<User>().HasData(
                new User
                {
                    Id = superAdminId,
                    Username = "superadmin",
                    Email = "superadmin@gmail.com",
                    FirstName = "Super",
                    LastName = "Admin",
                    PasswordHash = pw,
                    UserRole = UserRole.SuperAdmin,
                    Status = "Active"
                },
                new User
                {
                    Id = admin1Id,
                    Username = "msantos",
                    Email = "maria.santos@gmail.com",
                    FirstName = "Maria",
                    LastName = "Santos",
                    PasswordHash = pw,
                    UserRole = UserRole.Admin,
                    Status = "Active"
                },
                new User
                {
                    Id = admin2Id,
                    Username = "jdelacruz",
                    Email = "juan.delacruz@gmail.com",
                    FirstName = "Juan",
                    LastName = "Dela Cruz",
                    PasswordHash = pw,
                    UserRole = UserRole.Admin,
                    Status = "Active"
                },
                // admin3 is seeded here so its FK is valid for ArchiveUser
                new User
                {
                    Id = admin3Id,
                    Username = "areyes",
                    Email = "ana.reyes@gmail.com",
                    FirstName = "Ana",
                    LastName = "Reyes",
                    PasswordHash = pw,
                    UserRole = UserRole.Admin,
                    Status = "Inactive"
                }
            );

            // =========================================================
            // STAFF
            // =========================================================

            modelBuilder.Entity<Staff>().HasData(
                new Staff
                {
                    Id = staff1Id,
                    Username = "cmendoza",
                    Email = "carlos.mendoza@gmail.com",
                    FirstName = "Carlos",
                    LastName = "Mendoza",
                    PasswordHash = pw,
                    UserRole = UserRole.Staff,
                    Status = "Active",
                    Position = "Lab Technician"
                },
                new Staff
                {
                    Id = staff2Id,
                    Username = "rgarcia",
                    Email = "rosa.garcia@gmail.com",
                    FirstName = "Rosa",
                    LastName = "Garcia",
                    PasswordHash = pw,
                    UserRole = UserRole.Staff,
                    Status = "Active",
                    Position = "Equipment Manager"
                },
                // staff3 kept active so FK in ArchiveStaff is valid
                new Staff
                {
                    Id = staff3Id,
                    Username = "mtorres",
                    Email = "miguel.torres@gmail.com",
                    FirstName = "Miguel",
                    LastName = "Torres",
                    PasswordHash = pw,
                    UserRole = UserRole.Staff,
                    Status = "Inactive",
                    Position = "IT Support"
                }
            );

            // =========================================================
            // TEACHERS
            // =========================================================

            modelBuilder.Entity<Teacher>().HasData(
                new Teacher
                {
                    Id = teacher1Id,
                    Username = "awilliams",
                    Email = "alice.williams@gmail.com",
                    FirstName = "Alice",
                    LastName = "Williams",
                    PasswordHash = pw,
                    UserRole = UserRole.Teacher,
                    Status = "Active",
                    Department = "Information Technology"
                },
                new Teacher
                {
                    Id = teacher2Id,
                    Username = "rcruz",
                    Email = "roberto.cruz@gmail.com",
                    FirstName = "Roberto",
                    LastName = "Cruz",
                    PasswordHash = pw,
                    UserRole = UserRole.Teacher,
                    Status = "Active",
                    Department = "Computer Science"
                },
                new Teacher
                {
                    Id = teacher3Id,
                    Username = "efernandez",
                    Email = "elena.fernandez@gmail.com",
                    FirstName = "Elena",
                    LastName = "Fernandez",
                    PasswordHash = pw,
                    UserRole = UserRole.Teacher,
                    Status = "Active",
                    Department = "Information Technology"
                },
                // teacher4 kept in DB so FK in ArchiveTeacher is valid
                new Teacher
                {
                    Id = teacher4Id,
                    Username = "dramos",
                    Email = "david.ramos@gmail.com",
                    FirstName = "David",
                    LastName = "Ramos",
                    PasswordHash = pw,
                    UserRole = UserRole.Teacher,
                    Status = "Inactive",
                    Department = "Multimedia Arts"
                }
            );

            // =========================================================
            // STUDENTS
            // =========================================================

            modelBuilder.Entity<Student>().HasData(
                new Student
                {
                    Id = student1Id,
                    Username = "jdoe",
                    Email = "john.doe@gmail.com",
                    FirstName = "John",
                    LastName = "Doe",
                    PasswordHash = pw,
                    UserRole = UserRole.Student,
                    Status = "Active",
                    StudentIdNumber = "2023-0001",
                    Course = "Computer Science",
                    Year = "3rd Year",
                    Section = "A"
                },
                new Student
                {
                    Id = student2Id,
                    Username = "jsmith",
                    Email = "jane.smith@gmail.com",
                    FirstName = "Jane",
                    LastName = "Smith",
                    PasswordHash = pw,
                    UserRole = UserRole.Student,
                    Status = "Active",
                    StudentIdNumber = "2023-0002",
                    Course = "Information Technology",
                    Year = "2nd Year",
                    Section = "B"
                },
                new Student
                {
                    Id = student3Id,
                    Username = "pjones",
                    Email = "peter.jones@gmail.com",
                    FirstName = "Peter",
                    LastName = "Jones",
                    PasswordHash = pw,
                    UserRole = UserRole.Student,
                    Status = "Active",
                    StudentIdNumber = "2023-0003",
                    Course = "Computer Science",
                    Year = "3rd Year",
                    Section = "A"
                },
                new Student
                {
                    Id = student4Id,
                    Username = "mlopez",
                    Email = "maria.lopez@gmail.com",
                    FirstName = "Maria",
                    LastName = "Lopez",
                    PasswordHash = pw,
                    UserRole = UserRole.Student,
                    Status = "Active",
                    StudentIdNumber = "2023-0004",
                    Course = "Multimedia Arts",
                    Year = "1st Year",
                    Section = "C"
                },
                new Student
                {
                    Id = student5Id,
                    Username = "crivera",
                    Email = "carlos.rivera@gmail.com",
                    FirstName = "Carlos",
                    LastName = "Rivera",
                    PasswordHash = pw,
                    UserRole = UserRole.Student,
                    Status = "Active",
                    StudentIdNumber = "2023-0005",
                    Course = "Information Technology",
                    Year = "2nd Year",
                    Section = "A"
                },
                // student6 kept in DB so FK in ArchiveStudent is valid
                new Student
                {
                    Id = student6Id,
                    Username = "sgonzales",
                    Email = "sofia.gonzales@gmail.com",
                    FirstName = "Sofia",
                    LastName = "Gonzales",
                    PasswordHash = pw,
                    UserRole = UserRole.Student,
                    Status = "Inactive",
                    StudentIdNumber = "2024-0001",
                    Course = "Computer Science",
                    Year = "1st Year",
                    Section = "B"
                }
            );

            // =========================================================
            // ITEMS  (item7 & item8 will also appear in ArchiveItems)
            // =========================================================

            modelBuilder.Entity<Item>().HasData(
                new Item
                {
                    Id = item1Id,
                    ItemName = "HDMI Cable 10ft",
                    SerialNumber = "SN-HDMI-001",
                    ItemType = "Cable",
                    ItemMake = "Generic",
                    Category = ItemCategory.Electronics,
                    Condition = ItemCondition.Good,
                    Status = ItemStatus.Borrowed,
                    CreatedAt = new DateTime(2024, 6, 1, 0, 0, 0, DateTimeKind.Utc),
                    UpdatedAt = new DateTime(2024, 6, 1, 0, 0, 0, DateTimeKind.Utc)
                },
                new Item
                {
                    Id = item2Id,
                    ItemName = "Wireless Microphone",
                    SerialNumber = "SN-MIC-002",
                    ItemType = "Microphone",
                    ItemMake = "Shure",
                    Category = ItemCategory.MediaEquipment,
                    Condition = ItemCondition.Good,
                    Status = ItemStatus.Borrowed,
                    CreatedAt = new DateTime(2024, 6, 1, 0, 0, 0, DateTimeKind.Utc),
                    UpdatedAt = new DateTime(2024, 6, 1, 0, 0, 0, DateTimeKind.Utc)
                },
                new Item
                {
                    Id = item3Id,
                    ItemName = "Portable Bluetooth Speaker",
                    SerialNumber = "SN-SPK-003",
                    RfidUid = "RFID-ITEM-003",
                    ItemType = "Speaker",
                    ItemMake = "JBL",
                    Category = ItemCategory.MediaEquipment,
                    Condition = ItemCondition.Good,
                    Status = ItemStatus.Available,
                    CreatedAt = new DateTime(2024, 6, 1, 0, 0, 0, DateTimeKind.Utc),
                    UpdatedAt = new DateTime(2024, 6, 1, 0, 0, 0, DateTimeKind.Utc)
                },
                new Item
                {
                    Id = item4Id,
                    ItemName = "Wireless Mouse",
                    SerialNumber = "SN-MOUSE-004",
                    ItemType = "Peripheral",
                    ItemMake = "Logitech",
                    Category = ItemCategory.Electronics,
                    Condition = ItemCondition.Good,
                    Status = ItemStatus.Borrowed,
                    CreatedAt = new DateTime(2024, 6, 1, 0, 0, 0, DateTimeKind.Utc),
                    UpdatedAt = new DateTime(2024, 6, 1, 0, 0, 0, DateTimeKind.Utc)
                },
                new Item
                {
                    Id = item5Id,
                    ItemName = "Mechanical Keyboard",
                    SerialNumber = "SN-KB-005",
                    RfidUid = "RFID-ITEM-005",
                    ItemType = "Peripheral",
                    ItemMake = "Keychron",
                    Category = ItemCategory.Electronics,
                    Condition = ItemCondition.Good,
                    Status = ItemStatus.Available,
                    CreatedAt = new DateTime(2024, 6, 1, 0, 0, 0, DateTimeKind.Utc),
                    UpdatedAt = new DateTime(2024, 6, 1, 0, 0, 0, DateTimeKind.Utc)
                },
                new Item
                {
                    Id = item6Id,
                    ItemName = "Extension Wire 15ft",
                    SerialNumber = "SN-EXT-006",
                    RfidUid = "RFID-ITEM-006",
                    ItemType = "Cable",
                    ItemMake = "Generic",
                    Category = ItemCategory.Electronics,
                    Condition = ItemCondition.Good,
                    Status = ItemStatus.Available,
                    CreatedAt = new DateTime(2024, 6, 1, 0, 0, 0, DateTimeKind.Utc),
                    UpdatedAt = new DateTime(2024, 6, 1, 0, 0, 0, DateTimeKind.Utc)
                },
                // item7 & item8 remain in Items table so ArchiveLentItems FK is valid
                new Item
                {
                    Id = item7Id,
                    ItemName = "HDMI Cable 6ft",
                    SerialNumber = "SN-HDMI-007",
                    ItemType = "Cable",
                    ItemMake = "Generic",
                    Category = ItemCategory.Electronics,
                    Condition = ItemCondition.Good,
                    Status = ItemStatus.Archived,
                    CreatedAt = new DateTime(2024, 6, 1, 0, 0, 0, DateTimeKind.Utc),
                    UpdatedAt = new DateTime(2024, 6, 1, 0, 0, 0, DateTimeKind.Utc)
                },
                new Item
                {
                    Id = item8Id,
                    ItemName = "USB Microphone",
                    SerialNumber = "SN-MIC-008",
                    ItemType = "Microphone",
                    ItemMake = "Blue",
                    Category = ItemCategory.MediaEquipment,
                    Condition = ItemCondition.Defective,
                    Status = ItemStatus.Archived,
                    CreatedAt = new DateTime(2024, 6, 1, 0, 0, 0, DateTimeKind.Utc),
                    UpdatedAt = new DateTime(2024, 6, 1, 0, 0, 0, DateTimeKind.Utc)
                }
            );

            // =========================================================
            // LENT ITEMS
            // =========================================================

            modelBuilder.Entity<LentItems>().HasData(
                new LentItems
                {
                    Id = lent1Id,
                    ItemId = item1Id,
                    UserId = student1Id,
                    ItemName = "HDMI Cable 10ft",
                    BorrowerFullName = "John Doe",
                    BorrowerRole = "Student",
                    StudentIdNumber = "2023-0001",
                    Room = "Lab 101",
                    SubjectTimeSchedule = "CS301 - 8:00 AM",
                    Status = "Borrowed",
                    LentAt = new DateTime(2025, 4, 11, 8, 0, 0, DateTimeKind.Utc),
                    CreatedAt = new DateTime(2025, 4, 11, 8, 0, 0, DateTimeKind.Utc),
                    UpdatedAt = new DateTime(2025, 4, 11, 8, 0, 0, DateTimeKind.Utc)
                },
                new LentItems
                {
                    Id = lent2Id,
                    ItemId = item3Id,
                    UserId = student2Id,
                    ItemName = "Portable Bluetooth Speaker",
                    BorrowerFullName = "Jane Smith",
                    BorrowerRole = "Student",
                    StudentIdNumber = "2023-0002",
                    Room = "Lab 102",
                    SubjectTimeSchedule = "IT201 - 10:00 AM",
                    Status = "Returned",
                    LentAt = new DateTime(2025, 4, 6, 10, 0, 0, DateTimeKind.Utc),
                    ReturnedAt = new DateTime(2025, 4, 14, 10, 0, 0, DateTimeKind.Utc),
                    CreatedAt = new DateTime(2025, 4, 6, 10, 0, 0, DateTimeKind.Utc),
                    UpdatedAt = new DateTime(2025, 4, 14, 10, 0, 0, DateTimeKind.Utc)
                },
                new LentItems
                {
                    Id = lent3Id,
                    ItemId = item2Id,
                    UserId = student3Id,
                    ItemName = "Wireless Microphone",
                    BorrowerFullName = "Peter Jones",
                    BorrowerRole = "Student",
                    StudentIdNumber = "2023-0003",
                    Room = "Lab 103",
                    SubjectTimeSchedule = "CS302 - 1:00 PM",
                    Status = "Borrowed",
                    LentAt = new DateTime(2025, 4, 15, 13, 0, 0, DateTimeKind.Utc),
                    CreatedAt = new DateTime(2025, 4, 15, 13, 0, 0, DateTimeKind.Utc),
                    UpdatedAt = new DateTime(2025, 4, 15, 13, 0, 0, DateTimeKind.Utc)
                },
                new LentItems
                {
                    Id = lent4Id,
                    ItemId = item4Id,
                    UserId = teacher1Id,
                    TeacherId = teacher1Id,
                    ItemName = "Wireless Mouse",
                    BorrowerFullName = "Alice Williams",
                    BorrowerRole = "Teacher",
                    TeacherFullName = "Alice Williams",
                    Room = "Room 201",
                    SubjectTimeSchedule = "IT401 - 3:00 PM",
                    Status = "Borrowed",
                    LentAt = new DateTime(2025, 4, 13, 15, 0, 0, DateTimeKind.Utc),
                    CreatedAt = new DateTime(2025, 4, 13, 15, 0, 0, DateTimeKind.Utc),
                    UpdatedAt = new DateTime(2025, 4, 13, 15, 0, 0, DateTimeKind.Utc)
                },
                new LentItems
                {
                    Id = lent5Id,
                    ItemId = item5Id,
                    UserId = teacher2Id,
                    TeacherId = teacher2Id,
                    ItemName = "Mechanical Keyboard",
                    BorrowerFullName = "Roberto Cruz",
                    BorrowerRole = "Teacher",
                    TeacherFullName = "Roberto Cruz",
                    Room = "Room 202",
                    SubjectTimeSchedule = "CS201 - 9:00 AM",
                    Status = "Returned",
                    LentAt = new DateTime(2025, 3, 17, 9, 0, 0, DateTimeKind.Utc),
                    ReturnedAt = new DateTime(2025, 4, 1, 9, 0, 0, DateTimeKind.Utc),
                    CreatedAt = new DateTime(2025, 3, 17, 9, 0, 0, DateTimeKind.Utc),
                    UpdatedAt = new DateTime(2025, 4, 1, 9, 0, 0, DateTimeKind.Utc)
                }
            );

            // =========================================================
            // ARCHIVE USERS
            // Archived records keep OriginalUserId pointing to the live
            // Users table row, so unarchiving can restore the link.
            // =========================================================

            // Archived Admin (ana.reyes — still exists in Users, Status = Inactive)
            modelBuilder.Entity<ArchiveUser>().HasData(new ArchiveUser
            {
                Id = archiveUser1Id,
                OriginalUserId = admin3Id,
                Username = "areyes",
                Email = "ana.reyes@gmail.com",
                FirstName = "Ana",
                LastName = "Reyes",
                PasswordHash = pw,
                UserRole = UserRole.Admin,
                Status = "Inactive",
                ArchivedAt = archivedAt
            });

            // Archived Staff (miguel.torres)
            modelBuilder.Entity<ArchiveStaff>().HasData(new ArchiveStaff
            {
                Id = archiveStaff1Id,
                OriginalUserId = staff3Id,
                Username = "mtorres",
                Email = "miguel.torres@gmail.com",
                FirstName = "Miguel",
                LastName = "Torres",
                PasswordHash = pw,
                UserRole = UserRole.Staff,
                Status = "Inactive",
                Position = "IT Support",
                ArchivedAt = archivedAt
            });

            // Archived Teacher (david.ramos)
            modelBuilder.Entity<ArchiveTeacher>().HasData(new ArchiveTeacher
            {
                Id = archiveTeacher1Id,
                OriginalUserId = teacher4Id,
                Username = "dramos",
                Email = "david.ramos@gmail.com",
                FirstName = "David",
                LastName = "Ramos",
                PasswordHash = pw,
                UserRole = UserRole.Teacher,
                Status = "Inactive",
                Department = "Multimedia Arts",
                ArchivedAt = archivedAt
            });

            // Archived Student (sofia.gonzales)
            modelBuilder.Entity<ArchiveStudent>().HasData(new ArchiveStudent
            {
                Id = archiveStudent1Id,
                OriginalUserId = student6Id,
                Username = "sgonzales",
                Email = "sofia.gonzales@gmail.com",
                FirstName = "Sofia",
                LastName = "Gonzales",
                PasswordHash = pw,
                UserRole = UserRole.Student,
                Status = "Inactive",
                StudentIdNumber = "2024-0001",
                Course = "Computer Science",
                Year = "1st Year",
                Section = "B",
                ArchivedAt = archivedAt
            });

            // =========================================================
            // ARCHIVE ITEMS
            // item7 & item8 still exist in Items (Status = Archived),
            // so ArchiveLentItems can still FK into Items.
            // =========================================================

            modelBuilder.Entity<ArchiveItems>().HasData(
                new ArchiveItems
                {
                    Id = archiveItem1Id,
                    SerialNumber = "SN-HDMI-007",
                    ItemName = "HDMI Cable 6ft",
                    ItemType = "Cable",
                    ItemMake = "Generic",
                    Category = ItemCategory.Electronics,
                    Condition = ItemCondition.Good,
                    Status = ItemStatus.Archived,
                    CreatedAt = new DateTime(2024, 6, 1, 0, 0, 0, DateTimeKind.Utc),
                    UpdatedAt = archivedAt
                },
                new ArchiveItems
                {
                    Id = archiveItem2Id,
                    SerialNumber = "SN-MIC-008",
                    ItemName = "USB Microphone",
                    ItemType = "Microphone",
                    ItemMake = "Blue",
                    Category = ItemCategory.MediaEquipment,
                    Condition = ItemCondition.Defective,
                    Status = ItemStatus.Archived,
                    CreatedAt = new DateTime(2024, 6, 1, 0, 0, 0, DateTimeKind.Utc),
                    UpdatedAt = archivedAt
                }
            );

            // =========================================================
            // ARCHIVE LENT ITEMS
            // References item7 (still in Items) and teacher4 (still in
            // Teachers) so FK integrity is maintained. If unarchived,
            // the original parent records are still reachable.
            // =========================================================

            modelBuilder.Entity<ArchiveLentItems>().HasData(new ArchiveLentItems
            {
                Id = archiveLent1Id,
                ItemId = item7Id,          // FK → Items (item7, Status = Archived)
                UserId = teacher4Id,       // FK → Users (teacher4, Status = Inactive)
                TeacherId = teacher4Id,    // FK → Teachers (teacher4)
                ItemName = "HDMI Cable 6ft",
                BorrowerFullName = "David Ramos",
                BorrowerRole = "Teacher",
                TeacherFullName = "David Ramos",
                Room = "Room 301",
                SubjectTimeSchedule = "MA101 - 11:00 AM",
                Status = "Returned",
                LentAt = new DateTime(2025, 1, 10, 11, 0, 0, DateTimeKind.Utc),
                ReturnedAt = new DateTime(2025, 1, 14, 11, 0, 0, DateTimeKind.Utc),
                CreatedAt = new DateTime(2025, 1, 10, 11, 0, 0, DateTimeKind.Utc),
                UpdatedAt = archivedAt
            });
        }
    }
}
