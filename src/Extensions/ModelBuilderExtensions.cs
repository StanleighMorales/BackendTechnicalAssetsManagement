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

            // NOTE: This seed data is only applied during the INITIAL migration creation.
            // When you add NEW migrations later, EF Core will NOT re-insert this data
            // because it tracks what's already been seeded via the __EFMigrationsHistory table.
            // 
            // The seed data below will only be inserted when:
            // 1. Creating the initial migration (already done)
            // 2. Applying migrations to a fresh/empty database
            //
            // Future migrations will only contain schema changes (ALTER TABLE, etc.),
            // not data inserts, unless you explicitly add new HasData() calls.

            // ─────────────────────────────────────────────────────────────────
            // Read sensitive values from environment variables (loaded from
            // .env via DotNetEnv.Env.Load() in both AppDbContextFactory and
            // Program.cs before the DbContext is ever constructed).
            // ─────────────────────────────────────────────────────────────────
            var seedPassword = Environment.GetEnvironmentVariable("SeedPassword")
                ?? throw new InvalidOperationException(
                    "SeedPassword is not set. Add SeedPassword=<value> to your .env file.");

            var storageBase = Environment.GetEnvironmentVariable("Supabase_Storage_Bucket")
                ?? throw new InvalidOperationException(
                    "Supabase_Storage_Bucket is not set. Add Supabase_Storage_Bucket=<value> to your .env file.");

            var passwordHasher = new PasswordHashingService(workFactor: 4);
            string pw = passwordHasher.HashPassword(seedPassword);

            // =========================================================
            // FIXED GUIDs — deterministic so migrations stay stable
            // =========================================================

            // --- SuperAdmin ---
            var superAdminId    = Guid.Parse("00000001-0000-0000-0000-000000000001");

            // --- Admins (3 active + 1 archived-source) ---
            var admin1Id        = Guid.Parse("00000001-0000-0000-0000-000000000002"); // christian
            var admin2Id        = Guid.Parse("00000001-0000-0000-0000-000000000003"); // ejay
            var admin3Id        = Guid.Parse("00000001-0000-0000-0000-000000000004"); // stan
            var admin4Id        = Guid.Parse("00000001-0000-0000-0000-000000000005"); // archived source

            // --- Staff (4 active + 1 archived-source) ---
            var staff1Id        = Guid.Parse("00000002-0000-0000-0000-000000000001");
            var staff2Id        = Guid.Parse("00000002-0000-0000-0000-000000000002");
            var staff3Id        = Guid.Parse("00000002-0000-0000-0000-000000000003");
            var staff4Id        = Guid.Parse("00000002-0000-0000-0000-000000000004");
            var staff5Id        = Guid.Parse("00000002-0000-0000-0000-000000000005"); // archived source

            // --- Teachers (4 active + 1 archived-source) ---
            var teacher1Id      = Guid.Parse("00000003-0000-0000-0000-000000000001");
            var teacher2Id      = Guid.Parse("00000003-0000-0000-0000-000000000002");
            var teacher3Id      = Guid.Parse("00000003-0000-0000-0000-000000000003");
            var teacher4Id      = Guid.Parse("00000003-0000-0000-0000-000000000004");
            var teacher5Id      = Guid.Parse("00000003-0000-0000-0000-000000000005"); // archived source

            // --- Students (4 active + 1 archived-source) ---
            var student1Id      = Guid.Parse("00000004-0000-0000-0000-000000000001");
            var student2Id      = Guid.Parse("00000004-0000-0000-0000-000000000002");
            var student3Id      = Guid.Parse("00000004-0000-0000-0000-000000000003");
            var student4Id      = Guid.Parse("00000004-0000-0000-0000-000000000004");
            var student5Id      = Guid.Parse("00000004-0000-0000-0000-000000000005"); // archived source

            // --- Active Items (5) ---
            var item1Id         = Guid.Parse("00000005-0000-0000-0000-000000000001"); // Borrowed
            var item2Id         = Guid.Parse("00000005-0000-0000-0000-000000000002"); // Unavailable (Pending)
            var item3Id         = Guid.Parse("00000005-0000-0000-0000-000000000003"); // Reserved (Approved)
            var item4Id         = Guid.Parse("00000005-0000-0000-0000-000000000004"); // Available (Returned)
            var item5Id         = Guid.Parse("00000005-0000-0000-0000-000000000005"); // Available (Expired)

            // --- Archived Items (5 — kept in Items table with Status=Archived for FK integrity) ---
            var item6Id         = Guid.Parse("00000005-0000-0000-0000-000000000006");
            var item7Id         = Guid.Parse("00000005-0000-0000-0000-000000000007");
            var item8Id         = Guid.Parse("00000005-0000-0000-0000-000000000008");
            var item9Id         = Guid.Parse("00000005-0000-0000-0000-000000000009");
            var item10Id        = Guid.Parse("00000005-0000-0000-0000-000000000010");

            // --- Active LentItems (5 — one per status) ---
            var lent1Id         = Guid.Parse("00000006-0000-0000-0000-000000000001"); // Borrowed
            var lent2Id         = Guid.Parse("00000006-0000-0000-0000-000000000002"); // Pending
            var lent3Id         = Guid.Parse("00000006-0000-0000-0000-000000000003"); // Approved
            var lent4Id         = Guid.Parse("00000006-0000-0000-0000-000000000004"); // Returned
            var lent5Id         = Guid.Parse("00000006-0000-0000-0000-000000000005"); // Expired

            // --- Archive record IDs ---
            var archiveAdmin1Id     = Guid.Parse("00000007-0000-0000-0000-000000000001");
            var archiveStaff1Id     = Guid.Parse("00000007-0000-0000-0000-000000000002");
            var archiveTeacher1Id   = Guid.Parse("00000007-0000-0000-0000-000000000003");
            var archiveStudent1Id   = Guid.Parse("00000007-0000-0000-0000-000000000004");
            var archiveItem1Id      = Guid.Parse("00000007-0000-0000-0000-000000000005");
            var archiveItem2Id      = Guid.Parse("00000007-0000-0000-0000-000000000006");
            var archiveItem3Id      = Guid.Parse("00000007-0000-0000-0000-000000000007");
            var archiveItem4Id      = Guid.Parse("00000007-0000-0000-0000-000000000008");
            var archiveItem5Id      = Guid.Parse("00000007-0000-0000-0000-000000000009");
            var archiveLent1Id      = Guid.Parse("00000007-0000-0000-0000-000000000010");
            var archiveLent2Id      = Guid.Parse("00000007-0000-0000-0000-000000000011");
            var archiveLent3Id      = Guid.Parse("00000007-0000-0000-0000-000000000012");
            var archiveLent4Id      = Guid.Parse("00000007-0000-0000-0000-000000000013");
            var archiveLent5Id      = Guid.Parse("00000007-0000-0000-0000-000000000014");

            var archivedAt = new DateTime(2025, 1, 15, 10, 0, 0, DateTimeKind.Utc);

            // =========================================================
            // USERS  (SuperAdmin + 3 active Admins + 1 archived-source Admin)
            // Status "Offline" = registered but not currently logged in.
            // Status "Inactive" = account disabled / pending archive.
            // =========================================================

            modelBuilder.Entity<User>().HasData(
                // ── SuperAdmin ────────────────────────────────────────
                new User
                {
                    Id = superAdminId,
                    Username = "sadmin",
                    Email = "superadmin@school.edu.ph",
                    FirstName = "Super",
                    LastName = "Admin",
                    PasswordHash = pw,
                    UserRole = UserRole.SuperAdmin,
                    Status = "Offline"
                },
                // ── Active Admins ─────────────────────────────────────
                new User
                {
                    Id = admin1Id,
                    Username = "christian",
                    Email = "christian.admin@school.edu.ph",
                    FirstName = "Christian",
                    LastName = "Dela Cruz",
                    PasswordHash = pw,
                    UserRole = UserRole.Admin,
                    Status = "Offline"
                },
                new User
                {
                    Id = admin2Id,
                    Username = "ejay",
                    Email = "ejay.admin@school.edu.ph",
                    FirstName = "Ejay",
                    LastName = "Santos",
                    PasswordHash = pw,
                    UserRole = UserRole.Admin,
                    Status = "Offline"
                },
                new User
                {
                    Id = admin3Id,
                    Username = "stan",
                    Email = "stan.admin@school.edu.ph",
                    FirstName = "Stan",
                    LastName = "Reyes",
                    PasswordHash = pw,
                    UserRole = UserRole.Admin,
                    Status = "Offline"
                },
                // ── Archived-source Admin (kept in Users so ArchiveUser FK is valid) ──
                new User
                {
                    Id = admin4Id,
                    Username = "archived.admin",
                    Email = "archived.admin@school.edu.ph",
                    FirstName = "Archived",
                    LastName = "Admin",
                    PasswordHash = pw,
                    UserRole = UserRole.Admin,
                    Status = "Inactive"
                }
            );

            // =========================================================
            // STAFF  (4 active + 1 archived-source)
            // =========================================================

            modelBuilder.Entity<Staff>().HasData(
                new Staff
                {
                    Id = staff1Id,
                    Username = "cmendoza",
                    Email = "carlos.mendoza@school.edu.ph",
                    FirstName = "Carlos",
                    LastName = "Mendoza",
                    PasswordHash = pw,
                    UserRole = UserRole.Staff,
                    Status = "Offline",
                    Position = "Lab Technician"
                },
                new Staff
                {
                    Id = staff2Id,
                    Username = "rgarcia",
                    Email = "rosa.garcia@school.edu.ph",
                    FirstName = "Rosa",
                    LastName = "Garcia",
                    PasswordHash = pw,
                    UserRole = UserRole.Staff,
                    Status = "Offline",
                    Position = "Equipment Manager"
                },
                new Staff
                {
                    Id = staff3Id,
                    Username = "btorres",
                    Email = "ben.torres@school.edu.ph",
                    FirstName = "Ben",
                    LastName = "Torres",
                    PasswordHash = pw,
                    UserRole = UserRole.Staff,
                    Status = "Offline",
                    Position = "IT Support"
                },
                new Staff
                {
                    Id = staff4Id,
                    Username = "lvillanueva",
                    Email = "liza.villanueva@school.edu.ph",
                    FirstName = "Liza",
                    LastName = "Villanueva",
                    PasswordHash = pw,
                    UserRole = UserRole.Staff,
                    Status = "Offline",
                    Position = "Asset Custodian"
                },
                // Archived-source Staff (kept in DB so ArchiveStaff FK is valid)
                new Staff
                {
                    Id = staff5Id,
                    Username = "archived.staff",
                    Email = "archived.staff@school.edu.ph",
                    FirstName = "Archived",
                    LastName = "Staff",
                    PasswordHash = pw,
                    UserRole = UserRole.Staff,
                    Status = "Inactive",
                    Position = "Former Technician"
                }
            );

            // =========================================================
            // TEACHERS  (4 active + 1 archived-source)
            // =========================================================

            modelBuilder.Entity<Teacher>().HasData(
                new Teacher
                {
                    Id = teacher1Id,
                    Username = "awilliams",
                    Email = "alice.williams@school.edu.ph",
                    FirstName = "Alice",
                    LastName = "Williams",
                    PasswordHash = pw,
                    UserRole = UserRole.Teacher,
                    Status = "Offline",
                    Department = "Information Technology"
                },
                new Teacher
                {
                    Id = teacher2Id,
                    Username = "rcruz",
                    Email = "roberto.cruz@school.edu.ph",
                    FirstName = "Roberto",
                    LastName = "Cruz",
                    PasswordHash = pw,
                    UserRole = UserRole.Teacher,
                    Status = "Offline",
                    Department = "Computer Science"
                },
                new Teacher
                {
                    Id = teacher3Id,
                    Username = "efernandez",
                    Email = "elena.fernandez@school.edu.ph",
                    FirstName = "Elena",
                    LastName = "Fernandez",
                    PasswordHash = pw,
                    UserRole = UserRole.Teacher,
                    Status = "Offline",
                    Department = "Information Technology"
                },
                new Teacher
                {
                    Id = teacher4Id,
                    Username = "jmiranda",
                    Email = "jose.miranda@school.edu.ph",
                    FirstName = "Jose",
                    LastName = "Miranda",
                    PasswordHash = pw,
                    UserRole = UserRole.Teacher,
                    Status = "Offline",
                    Department = "Multimedia Arts"
                },
                // Archived-source Teacher (kept in DB so ArchiveTeacher FK is valid)
                new Teacher
                {
                    Id = teacher5Id,
                    Username = "archived.teacher",
                    Email = "archived.teacher@school.edu.ph",
                    FirstName = "Archived",
                    LastName = "Teacher",
                    PasswordHash = pw,
                    UserRole = UserRole.Teacher,
                    Status = "Inactive",
                    Department = "Former Department"
                }
            );

            // =========================================================
            // STUDENTS  (4 active + 1 archived-source)
            // All required profile fields are populated so borrowing
            // validation (ValidateStudentProfileComplete) passes.
            // Image URLs point to the Supabase bucket.
            // =========================================================

            modelBuilder.Entity<Student>().HasData(
                new Student
                {
                    Id = student1Id,
                    Username = "jdelacruz",
                    Email = "juan.delacruz@school.edu.ph",
                    FirstName = "Juan",
                    LastName = "Dela Cruz",
                    PasswordHash = pw,
                    UserRole = UserRole.Student,
                    Status = "Offline",
                    PhoneNumber = "09171234501",
                    StudentIdNumber = "2023-0001",
                    Course = "Bachelor of Science in Computer Science",
                    Year = "3rd Year",
                    Section = "A",
                    Street = "123 Rizal Street",
                    CityMunicipality = "Quezon City",
                    Province = "Metro Manila",
                    PostalCode = "1100",
                    RfidUid = "RFID-STU-001",
                    ProfilePictureUrl    = $"{storageBase}/students/profile/student_01_profile.png",
                    FrontStudentIdPictureUrl = $"{storageBase}/students/id-front/student_01_id_front.png",
                    BackStudentIdPictureUrl  = $"{storageBase}/students/id-back/student_01_id_back.png"
                },
                new Student
                {
                    Id = student2Id,
                    Username = "msantos",
                    Email = "maria.santos@school.edu.ph",
                    FirstName = "Maria",
                    LastName = "Santos",
                    PasswordHash = pw,
                    UserRole = UserRole.Student,
                    Status = "Offline",
                    PhoneNumber = "09171234502",
                    StudentIdNumber = "2023-0002",
                    Course = "Bachelor of Science in Information Technology",
                    Year = "2nd Year",
                    Section = "B",
                    Street = "456 Mabini Avenue",
                    CityMunicipality = "Makati City",
                    Province = "Metro Manila",
                    PostalCode = "1200",
                    RfidUid = "RFID-STU-002",
                    ProfilePictureUrl    = $"{storageBase}/students/profile/student_02_profile.png",
                    FrontStudentIdPictureUrl = $"{storageBase}/students/id-front/student_02_id_front.png",
                    BackStudentIdPictureUrl  = $"{storageBase}/students/id-back/student_02_id_back.png"
                },
                new Student
                {
                    Id = student3Id,
                    Username = "preyes",
                    Email = "pedro.reyes@school.edu.ph",
                    FirstName = "Pedro",
                    LastName = "Reyes",
                    PasswordHash = pw,
                    UserRole = UserRole.Student,
                    Status = "Offline",
                    PhoneNumber = "09171234503",
                    StudentIdNumber = "2023-0003",
                    Course = "Bachelor of Science in Computer Science",
                    Year = "3rd Year",
                    Section = "A",
                    Street = "789 Bonifacio Road",
                    CityMunicipality = "Pasig City",
                    Province = "Metro Manila",
                    PostalCode = "1600",
                    RfidUid = "RFID-STU-003",
                    ProfilePictureUrl    = $"{storageBase}/students/profile/student_03_profile.png",
                    FrontStudentIdPictureUrl = $"{storageBase}/students/id-front/student_03_id_front.png",
                    BackStudentIdPictureUrl  = $"{storageBase}/students/id-back/student_03_id_back.png"
                },
                new Student
                {
                    Id = student4Id,
                    Username = "agarcia",
                    Email = "ana.garcia@school.edu.ph",
                    FirstName = "Ana",
                    LastName = "Garcia",
                    PasswordHash = pw,
                    UserRole = UserRole.Student,
                    Status = "Offline",
                    PhoneNumber = "09171234504",
                    StudentIdNumber = "2023-0004",
                    Course = "Bachelor of Multimedia Arts",
                    Year = "1st Year",
                    Section = "C",
                    Street = "321 Luna Street",
                    CityMunicipality = "Mandaluyong City",
                    Province = "Metro Manila",
                    PostalCode = "1550",
                    RfidUid = "RFID-STU-004",
                    ProfilePictureUrl    = $"{storageBase}/students/profile/student_04_profile.png",
                    FrontStudentIdPictureUrl = $"{storageBase}/students/id-front/student_04_id_front.png",
                    BackStudentIdPictureUrl  = $"{storageBase}/students/id-back/student_04_id_back.png"
                },
                // Archived-source Student (kept in DB so ArchiveStudent FK is valid)
                new Student
                {
                    Id = student5Id,
                    Username = "archived.student",
                    Email = "archived.student@school.edu.ph",
                    FirstName = "Archived",
                    LastName = "Student",
                    PasswordHash = pw,
                    UserRole = UserRole.Student,
                    Status = "Inactive",
                    PhoneNumber = "09170000000",
                    StudentIdNumber = "2022-0099",
                    Course = "Bachelor of Science in Information Technology",
                    Year = "4th Year",
                    Section = "D",
                    Street = "999 Old Street",
                    CityMunicipality = "Taguig City",
                    Province = "Metro Manila",
                    PostalCode = "1630",
                    ProfilePictureUrl    = $"{storageBase}/students/profile/student_05_profile.png",
                    FrontStudentIdPictureUrl = $"{storageBase}/students/id-front/student_05_id_front.png",
                    BackStudentIdPictureUrl  = $"{storageBase}/students/id-back/student_05_id_back.png"
                }
            );

            // =========================================================
            // ITEMS
            // ─────────────────────────────────────────────────────────
            // Items 1–5: active items whose Status is kept in sync with
            //            their corresponding LentItems record below.
            //
            //   item1 → Status=Borrowed      (lent1 Borrowed)
            //   item2 → Status=Unavailable   (lent2 Pending)
            //   item3 → Status=Reserved      (lent3 Approved)
            //   item4 → Status=Available     (lent4 Returned — item free again)
            //   item5 → Status=Available     (lent5 Expired  — item free again)
            //
            // Items 6–10: archived items (Status=Archived), kept in the
            //             Items table so ArchiveLentItems FKs stay valid.
            // =========================================================

            modelBuilder.Entity<Item>().HasData(
                // ── Active Items ──────────────────────────────────────
                new Item
                {
                    Id = item1Id,
                    ItemName = "HDMI Cable 10ft",
                    SerialNumber = "SN-HDMI-001",
                    RfidUid = "RFID-ITEM-001",
                    ImageUrl = $"{storageBase}/items/item_01_hdmi_cable.png",
                    ItemType = "Cable",
                    ItemModel = "Standard HDMI 2.0",
                    ItemMake = "Generic",
                    Description = "10-foot HDMI 2.0 cable for display connections.",
                    Category = ItemCategory.Electronics,
                    Condition = ItemCondition.Good,
                    Status = ItemStatus.Borrowed,
                    Location = "Lab Cabinet A",
                    CreatedAt = new DateTime(2024, 6, 1, 0, 0, 0, DateTimeKind.Utc),
                    UpdatedAt = new DateTime(2025, 4, 11, 8, 0, 0, DateTimeKind.Utc)
                },
                new Item
                {
                    Id = item2Id,
                    ItemName = "Wireless Microphone",
                    SerialNumber = "SN-MIC-002",
                    RfidUid = "RFID-ITEM-002",
                    ImageUrl = $"{storageBase}/items/item_02_wireless_mic.png",
                    ItemType = "Microphone",
                    ItemModel = "BLX288/PG58",
                    ItemMake = "Shure",
                    Description = "Dual-channel wireless microphone system.",
                    Category = ItemCategory.MediaEquipment,
                    Condition = ItemCondition.Good,
                    Status = ItemStatus.Unavailable,
                    Location = "Media Room Shelf",
                    CreatedAt = new DateTime(2024, 6, 1, 0, 0, 0, DateTimeKind.Utc),
                    UpdatedAt = new DateTime(2025, 4, 20, 9, 0, 0, DateTimeKind.Utc)
                },
                new Item
                {
                    Id = item3Id,
                    ItemName = "Portable Projector",
                    SerialNumber = "SN-PROJ-003",
                    RfidUid = "RFID-ITEM-003",
                    ImageUrl = $"{storageBase}/items/item_03_projector.png",
                    ItemType = "Projector",
                    ItemModel = "EB-X41",
                    ItemMake = "Epson",
                    Description = "3600-lumen portable LCD projector.",
                    Category = ItemCategory.MediaEquipment,
                    Condition = ItemCondition.Good,
                    Status = ItemStatus.Reserved,
                    Location = "AV Room Cabinet",
                    CreatedAt = new DateTime(2024, 6, 1, 0, 0, 0, DateTimeKind.Utc),
                    UpdatedAt = new DateTime(2025, 4, 19, 10, 0, 0, DateTimeKind.Utc)
                },
                new Item
                {
                    Id = item4Id,
                    ItemName = "Bluetooth Speaker",
                    SerialNumber = "SN-SPK-004",
                    RfidUid = "RFID-ITEM-004",
                    ImageUrl = $"{storageBase}/items/item_04_speaker.png",
                    ItemType = "Speaker",
                    ItemModel = "Charge 5",
                    ItemMake = "JBL",
                    Description = "Portable waterproof Bluetooth speaker.",
                    Category = ItemCategory.MediaEquipment,
                    Condition = ItemCondition.Good,
                    Status = ItemStatus.Available,
                    Location = "Lab Cabinet B",
                    CreatedAt = new DateTime(2024, 6, 1, 0, 0, 0, DateTimeKind.Utc),
                    UpdatedAt = new DateTime(2025, 4, 10, 14, 0, 0, DateTimeKind.Utc)
                },
                new Item
                {
                    Id = item5Id,
                    ItemName = "Mechanical Keyboard",
                    SerialNumber = "SN-KB-005",
                    RfidUid = "RFID-ITEM-005",
                    ImageUrl = $"{storageBase}/items/item_05_keyboard.png",
                    ItemType = "Peripheral",
                    ItemModel = "K2 Pro",
                    ItemMake = "Keychron",
                    Description = "Compact TKL mechanical keyboard with RGB.",
                    Category = ItemCategory.Electronics,
                    Condition = ItemCondition.Good,
                    Status = ItemStatus.Available,
                    Location = "Lab Cabinet A",
                    CreatedAt = new DateTime(2024, 6, 1, 0, 0, 0, DateTimeKind.Utc),
                    UpdatedAt = new DateTime(2025, 3, 25, 9, 0, 0, DateTimeKind.Utc)
                },
                // ── Archived Items (Status=Archived, kept for FK integrity) ──
                new Item
                {
                    Id = item6Id,
                    ItemName = "HDMI Cable 6ft",
                    SerialNumber = "SN-HDMI-006",
                    RfidUid = "RFID-ITEM-006",
                    ImageUrl = $"{storageBase}/items/item_06_hdmi_short.png",
                    ItemType = "Cable",
                    ItemMake = "Generic",
                    Description = "6-foot HDMI cable, retired from service.",
                    Category = ItemCategory.Electronics,
                    Condition = ItemCondition.Good,
                    Status = ItemStatus.Archived,
                    Location = "Storage Room",
                    CreatedAt = new DateTime(2024, 1, 1, 0, 0, 0, DateTimeKind.Utc),
                    UpdatedAt = archivedAt
                },
                new Item
                {
                    Id = item7Id,
                    ItemName = "USB Microphone",
                    SerialNumber = "SN-MIC-007",
                    RfidUid = "RFID-ITEM-007",
                    ImageUrl = $"{storageBase}/items/item_07_usb_mic.png",
                    ItemType = "Microphone",
                    ItemMake = "Blue",
                    Description = "USB condenser microphone, defective.",
                    Category = ItemCategory.MediaEquipment,
                    Condition = ItemCondition.Defective,
                    Status = ItemStatus.Archived,
                    Location = "Storage Room",
                    CreatedAt = new DateTime(2024, 1, 1, 0, 0, 0, DateTimeKind.Utc),
                    UpdatedAt = archivedAt
                },
                new Item
                {
                    Id = item8Id,
                    ItemName = "Wireless Mouse",
                    SerialNumber = "SN-MOUSE-008",
                    RfidUid = "RFID-ITEM-008",
                    ImageUrl = $"{storageBase}/items/item_08_mouse.png",
                    ItemType = "Peripheral",
                    ItemMake = "Logitech",
                    Description = "Wireless optical mouse, needs repair.",
                    Category = ItemCategory.Electronics,
                    Condition = ItemCondition.NeedRepair,
                    Status = ItemStatus.Archived,
                    Location = "Storage Room",
                    CreatedAt = new DateTime(2024, 1, 1, 0, 0, 0, DateTimeKind.Utc),
                    UpdatedAt = archivedAt
                },
                new Item
                {
                    Id = item9Id,
                    ItemName = "Extension Wire 15ft",
                    SerialNumber = "SN-EXT-009",
                    RfidUid = "RFID-ITEM-009",
                    ImageUrl = $"{storageBase}/items/item_09_extension.png",
                    ItemType = "Cable",
                    ItemMake = "Generic",
                    Description = "15-foot extension wire, retired.",
                    Category = ItemCategory.Electronics,
                    Condition = ItemCondition.Good,
                    Status = ItemStatus.Archived,
                    Location = "Storage Room",
                    CreatedAt = new DateTime(2024, 1, 1, 0, 0, 0, DateTimeKind.Utc),
                    UpdatedAt = archivedAt
                },
                new Item
                {
                    Id = item10Id,
                    ItemName = "USB Hub 7-Port",
                    SerialNumber = "SN-HUB-010",
                    RfidUid = "RFID-ITEM-010",
                    ImageUrl = $"{storageBase}/items/item_10_usb_hub.png",
                    ItemType = "Peripheral",
                    ItemMake = "Anker",
                    Description = "7-port USB 3.0 hub, refurbished.",
                    Category = ItemCategory.Electronics,
                    Condition = ItemCondition.Refurbished,
                    Status = ItemStatus.Archived,
                    Location = "Storage Room",
                    CreatedAt = new DateTime(2024, 1, 1, 0, 0, 0, DateTimeKind.Utc),
                    UpdatedAt = archivedAt
                }
            );

            // =========================================================
            // LENT ITEMS  — 5 records, one per meaningful status
            // ─────────────────────────────────────────────────────────
            // lent1 → Borrowed   item1=Borrowed      student1 currently has it
            // lent2 → Pending    item2=Unavailable   student2 reservation awaiting approval
            // lent3 → Approved   item3=Reserved      teacher1 approved, not yet picked up
            // lent4 → Returned   item4=Available     student3 returned it
            // lent5 → Expired    item5=Available     teacher2 reservation expired
            // =========================================================

            modelBuilder.Entity<LentItems>().HasData(
                // ── 1. Borrowed ───────────────────────────────────────
                new LentItems
                {
                    Id = lent1Id,
                    ItemId = item1Id,
                    UserId = student1Id,
                    ItemName = "HDMI Cable 10ft",
                    BorrowerFullName = "Juan Dela Cruz",
                    BorrowerRole = "Student",
                    StudentIdNumber = "2023-0001",
                    TagUid = "RFID-ITEM-001",
                    StudentRfid = "RFID-STU-001",
                    FrontStudentIdPictureUrl = $"{storageBase}/students/id-front/student_01_id_front.png",
                    Room = "Lab 101",
                    SubjectTimeSchedule = "CS301 - 8:00 AM",
                    Status = "Borrowed",
                    LentAt = new DateTime(2025, 4, 20, 8, 0, 0, DateTimeKind.Utc),
                    CreatedAt = new DateTime(2025, 4, 20, 8, 0, 0, DateTimeKind.Utc),
                    UpdatedAt = new DateTime(2025, 4, 20, 8, 0, 0, DateTimeKind.Utc)
                },
                // ── 2. Pending (reservation awaiting admin approval) ──
                new LentItems
                {
                    Id = lent2Id,
                    ItemId = item2Id,
                    UserId = student2Id,
                    ItemName = "Wireless Microphone",
                    BorrowerFullName = "Maria Santos",
                    BorrowerRole = "Student",
                    StudentIdNumber = "2023-0002",
                    TagUid = "RFID-ITEM-002",
                    StudentRfid = "RFID-STU-002",
                    FrontStudentIdPictureUrl = $"{storageBase}/students/id-front/student_02_id_front.png",
                    Room = "Lab 102",
                    SubjectTimeSchedule = "IT201 - 10:00 AM",
                    ReservedFor = new DateTime(2025, 4, 27, 10, 0, 0, DateTimeKind.Utc),
                    Status = "Pending",
                    CreatedAt = new DateTime(2025, 4, 20, 9, 0, 0, DateTimeKind.Utc),
                    UpdatedAt = new DateTime(2025, 4, 20, 9, 0, 0, DateTimeKind.Utc)
                },
                // ── 3. Approved (reservation approved, waiting for pickup) ──
                new LentItems
                {
                    Id = lent3Id,
                    ItemId = item3Id,
                    UserId = teacher1Id,
                    TeacherId = teacher1Id,
                    ItemName = "Portable Projector",
                    BorrowerFullName = "Alice Williams",
                    BorrowerRole = "Teacher",
                    TeacherFullName = "Alice Williams",
                    TagUid = "RFID-ITEM-003",
                    Room = "Room 201",
                    SubjectTimeSchedule = "IT401 - 1:00 PM",
                    ReservedFor = new DateTime(2025, 4, 26, 13, 0, 0, DateTimeKind.Utc),
                    Status = "Approved",
                    CreatedAt = new DateTime(2025, 4, 19, 10, 0, 0, DateTimeKind.Utc),
                    UpdatedAt = new DateTime(2025, 4, 19, 11, 0, 0, DateTimeKind.Utc)
                },
                // ── 4. Returned (completed borrow — item is free again) ──
                new LentItems
                {
                    Id = lent4Id,
                    ItemId = item4Id,
                    UserId = student3Id,
                    ItemName = "Bluetooth Speaker",
                    BorrowerFullName = "Pedro Reyes",
                    BorrowerRole = "Student",
                    StudentIdNumber = "2023-0003",
                    TagUid = "RFID-ITEM-004",
                    StudentRfid = "RFID-STU-003",
                    FrontStudentIdPictureUrl = $"{storageBase}/students/id-front/student_03_id_front.png",
                    Room = "Lab 103",
                    SubjectTimeSchedule = "CS302 - 1:00 PM",
                    Status = "Returned",
                    LentAt = new DateTime(2025, 4, 10, 13, 0, 0, DateTimeKind.Utc),
                    ReturnedAt = new DateTime(2025, 4, 14, 15, 0, 0, DateTimeKind.Utc),
                    CreatedAt = new DateTime(2025, 4, 10, 13, 0, 0, DateTimeKind.Utc),
                    UpdatedAt = new DateTime(2025, 4, 14, 15, 0, 0, DateTimeKind.Utc)
                },
                // ── 5. Expired (reservation not picked up — item is free again) ──
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
                    TagUid = "RFID-ITEM-005",
                    Room = "Room 202",
                    SubjectTimeSchedule = "CS201 - 9:00 AM",
                    ReservedFor = new DateTime(2025, 4, 15, 9, 0, 0, DateTimeKind.Utc),
                    Status = "Expired",
                    CreatedAt = new DateTime(2025, 4, 14, 9, 0, 0, DateTimeKind.Utc),
                    UpdatedAt = new DateTime(2025, 4, 15, 9, 31, 0, DateTimeKind.Utc)
                }
            );

            // =========================================================
            // ARCHIVE USERS  (5 records — one per role type)
            // OriginalUserId → live Users row so unarchiving can restore
            // the relationship. The live row stays with Status=Inactive.
            // =========================================================

            modelBuilder.Entity<ArchiveUser>().HasData(new ArchiveUser
            {
                Id = archiveAdmin1Id,
                OriginalUserId = admin4Id,
                Username = "archived.admin",
                Email = "archived.admin@school.edu.ph",
                FirstName = "Archived",
                LastName = "Admin",
                PasswordHash = pw,
                UserRole = UserRole.Admin,
                Status = "Inactive",
                ArchivedAt = archivedAt
            });

            modelBuilder.Entity<ArchiveStaff>().HasData(new ArchiveStaff
            {
                Id = archiveStaff1Id,
                OriginalUserId = staff5Id,
                Username = "archived.staff",
                Email = "archived.staff@school.edu.ph",
                FirstName = "Archived",
                LastName = "Staff",
                PasswordHash = pw,
                UserRole = UserRole.Staff,
                Status = "Inactive",
                Position = "Former Technician",
                ArchivedAt = archivedAt
            });

            modelBuilder.Entity<ArchiveTeacher>().HasData(new ArchiveTeacher
            {
                Id = archiveTeacher1Id,
                OriginalUserId = teacher5Id,
                Username = "archived.teacher",
                Email = "archived.teacher@school.edu.ph",
                FirstName = "Archived",
                LastName = "Teacher",
                PasswordHash = pw,
                UserRole = UserRole.Teacher,
                Status = "Inactive",
                Department = "Former Department",
                ArchivedAt = archivedAt
            });

            modelBuilder.Entity<ArchiveStudent>().HasData(new ArchiveStudent
            {
                Id = archiveStudent1Id,
                OriginalUserId = student5Id,
                Username = "archived.student",
                Email = "archived.student@school.edu.ph",
                FirstName = "Archived",
                LastName = "Student",
                PasswordHash = pw,
                UserRole = UserRole.Student,
                Status = "Inactive",
                StudentIdNumber = "2022-0099",
                Course = "Bachelor of Science in Information Technology",
                Year = "4th Year",
                Section = "D",
                ArchivedAt = archivedAt
            });

            // =========================================================
            // ARCHIVE ITEMS  (5 records — mirrors items 6–10)
            // Items 6–10 remain in the Items table (Status=Archived) so
            // ArchiveLentItems can still FK into Items.
            // =========================================================

            modelBuilder.Entity<ArchiveItems>().HasData(
                new ArchiveItems
                {
                    Id = archiveItem1Id,
                    SerialNumber = "SN-HDMI-006",
                    RfidUid = "RFID-ITEM-006",
                    ImageUrl = $"{storageBase}/items/item_06_hdmi_short.png",
                    ItemName = "HDMI Cable 6ft",
                    ItemType = "Cable",
                    ItemMake = "Generic",
                    Description = "6-foot HDMI cable, retired from service.",
                    Category = ItemCategory.Electronics,
                    Condition = ItemCondition.Good,
                    Status = ItemStatus.Archived,
                    Location = "Storage Room",
                    CreatedAt = new DateTime(2024, 1, 1, 0, 0, 0, DateTimeKind.Utc),
                    UpdatedAt = archivedAt
                },
                new ArchiveItems
                {
                    Id = archiveItem2Id,
                    SerialNumber = "SN-MIC-007",
                    RfidUid = "RFID-ITEM-007",
                    ImageUrl = $"{storageBase}/items/item_07_usb_mic.png",
                    ItemName = "USB Microphone",
                    ItemType = "Microphone",
                    ItemMake = "Blue",
                    Description = "USB condenser microphone, defective.",
                    Category = ItemCategory.MediaEquipment,
                    Condition = ItemCondition.Defective,
                    Status = ItemStatus.Archived,
                    Location = "Storage Room",
                    CreatedAt = new DateTime(2024, 1, 1, 0, 0, 0, DateTimeKind.Utc),
                    UpdatedAt = archivedAt
                },
                new ArchiveItems
                {
                    Id = archiveItem3Id,
                    SerialNumber = "SN-MOUSE-008",
                    RfidUid = "RFID-ITEM-008",
                    ImageUrl = $"{storageBase}/items/item_08_mouse.png",
                    ItemName = "Wireless Mouse",
                    ItemType = "Peripheral",
                    ItemMake = "Logitech",
                    Description = "Wireless optical mouse, needs repair.",
                    Category = ItemCategory.Electronics,
                    Condition = ItemCondition.NeedRepair,
                    Status = ItemStatus.Archived,
                    Location = "Storage Room",
                    CreatedAt = new DateTime(2024, 1, 1, 0, 0, 0, DateTimeKind.Utc),
                    UpdatedAt = archivedAt
                },
                new ArchiveItems
                {
                    Id = archiveItem4Id,
                    SerialNumber = "SN-EXT-009",
                    RfidUid = "RFID-ITEM-009",
                    ImageUrl = $"{storageBase}/items/item_09_extension.png",
                    ItemName = "Extension Wire 15ft",
                    ItemType = "Cable",
                    ItemMake = "Generic",
                    Description = "15-foot extension wire, retired.",
                    Category = ItemCategory.Electronics,
                    Condition = ItemCondition.Good,
                    Status = ItemStatus.Archived,
                    Location = "Storage Room",
                    CreatedAt = new DateTime(2024, 1, 1, 0, 0, 0, DateTimeKind.Utc),
                    UpdatedAt = archivedAt
                },
                new ArchiveItems
                {
                    Id = archiveItem5Id,
                    SerialNumber = "SN-HUB-010",
                    RfidUid = "RFID-ITEM-010",
                    ImageUrl = $"{storageBase}/items/item_10_usb_hub.png",
                    ItemName = "USB Hub 7-Port",
                    ItemType = "Peripheral",
                    ItemMake = "Anker",
                    Description = "7-port USB 3.0 hub, refurbished.",
                    Category = ItemCategory.Electronics,
                    Condition = ItemCondition.Refurbished,
                    Status = ItemStatus.Archived,
                    Location = "Storage Room",
                    CreatedAt = new DateTime(2024, 1, 1, 0, 0, 0, DateTimeKind.Utc),
                    UpdatedAt = archivedAt
                }
            );

            // =========================================================
            // ARCHIVE LENT ITEMS  (5 records)
            // All reference archived items (6–10) and the archived-source
            // users so every FK points to a real row.
            // =========================================================

            modelBuilder.Entity<ArchiveLentItems>().HasData(
                new ArchiveLentItems
                {
                    Id = archiveLent1Id,
                    ItemId = item6Id,
                    UserId = teacher5Id,
                    TeacherId = teacher5Id,
                    ItemName = "HDMI Cable 6ft",
                    BorrowerFullName = "Archived Teacher",
                    BorrowerRole = "Teacher",
                    TeacherFullName = "Archived Teacher",
                    Room = "Room 301",
                    SubjectTimeSchedule = "MA101 - 11:00 AM",
                    Status = "Returned",
                    LentAt = new DateTime(2025, 1, 10, 11, 0, 0, DateTimeKind.Utc),
                    ReturnedAt = new DateTime(2025, 1, 14, 11, 0, 0, DateTimeKind.Utc),
                    CreatedAt = new DateTime(2025, 1, 10, 11, 0, 0, DateTimeKind.Utc),
                    UpdatedAt = archivedAt
                },
                new ArchiveLentItems
                {
                    Id = archiveLent2Id,
                    ItemId = item7Id,
                    UserId = student5Id,
                    ItemName = "USB Microphone",
                    BorrowerFullName = "Archived Student",
                    BorrowerRole = "Student",
                    StudentIdNumber = "2022-0099",
                    Room = "Lab 201",
                    SubjectTimeSchedule = "IT301 - 2:00 PM",
                    Status = "Returned",
                    LentAt = new DateTime(2024, 12, 5, 14, 0, 0, DateTimeKind.Utc),
                    ReturnedAt = new DateTime(2024, 12, 7, 14, 0, 0, DateTimeKind.Utc),
                    CreatedAt = new DateTime(2024, 12, 5, 14, 0, 0, DateTimeKind.Utc),
                    UpdatedAt = new DateTime(2024, 12, 7, 14, 0, 0, DateTimeKind.Utc)
                },
                new ArchiveLentItems
                {
                    Id = archiveLent3Id,
                    ItemId = item8Id,
                    UserId = student1Id,
                    ItemName = "Wireless Mouse",
                    BorrowerFullName = "Juan Dela Cruz",
                    BorrowerRole = "Student",
                    StudentIdNumber = "2023-0001",
                    Room = "Lab 101",
                    SubjectTimeSchedule = "CS201 - 8:00 AM",
                    Status = "Returned",
                    LentAt = new DateTime(2024, 11, 20, 8, 0, 0, DateTimeKind.Utc),
                    ReturnedAt = new DateTime(2024, 11, 22, 8, 0, 0, DateTimeKind.Utc),
                    CreatedAt = new DateTime(2024, 11, 20, 8, 0, 0, DateTimeKind.Utc),
                    UpdatedAt = new DateTime(2024, 11, 22, 8, 0, 0, DateTimeKind.Utc)
                },
                new ArchiveLentItems
                {
                    Id = archiveLent4Id,
                    ItemId = item9Id,
                    UserId = teacher1Id,
                    TeacherId = teacher1Id,
                    ItemName = "Extension Wire 15ft",
                    BorrowerFullName = "Alice Williams",
                    BorrowerRole = "Teacher",
                    TeacherFullName = "Alice Williams",
                    Room = "Room 201",
                    SubjectTimeSchedule = "IT401 - 3:00 PM",
                    Status = "Returned",
                    LentAt = new DateTime(2024, 10, 15, 15, 0, 0, DateTimeKind.Utc),
                    ReturnedAt = new DateTime(2024, 10, 17, 15, 0, 0, DateTimeKind.Utc),
                    CreatedAt = new DateTime(2024, 10, 15, 15, 0, 0, DateTimeKind.Utc),
                    UpdatedAt = new DateTime(2024, 10, 17, 15, 0, 0, DateTimeKind.Utc)
                },
                new ArchiveLentItems
                {
                    Id = archiveLent5Id,
                    ItemId = item10Id,
                    UserId = student2Id,
                    ItemName = "USB Hub 7-Port",
                    BorrowerFullName = "Maria Santos",
                    BorrowerRole = "Student",
                    StudentIdNumber = "2023-0002",
                    Room = "Lab 102",
                    SubjectTimeSchedule = "IT201 - 10:00 AM",
                    Status = "Expired",
                    ReservedFor = new DateTime(2024, 9, 10, 10, 0, 0, DateTimeKind.Utc),
                    CreatedAt = new DateTime(2024, 9, 9, 10, 0, 0, DateTimeKind.Utc),
                    UpdatedAt = new DateTime(2024, 9, 10, 10, 31, 0, DateTimeKind.Utc)
                }
            );
        }
    }
}
