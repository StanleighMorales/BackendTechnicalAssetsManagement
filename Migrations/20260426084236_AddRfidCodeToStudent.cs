using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace BackendTechnicalAssetsManagement.Migrations
{
    /// <inheritdoc />
    public partial class AddRfidCodeToStudent : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "ArchiveLentItems",
                keyColumn: "Id",
                keyValue: new Guid("00000007-0000-0000-0000-000000000007"));

            migrationBuilder.DeleteData(
                table: "Students",
                keyColumn: "Id",
                keyValue: new Guid("00000004-0000-0000-0000-000000000006"));

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("00000004-0000-0000-0000-000000000006"));

            migrationBuilder.UpdateData(
                table: "ArchiveItems",
                keyColumn: "Id",
                keyValue: new Guid("00000007-0000-0000-0000-000000000005"),
                columns: new[] { "CreatedAt", "Description", "ImageUrl", "Location", "RfidUid", "SerialNumber" },
                values: new object[] { new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "6-foot HDMI cable, retired from service.", "https://oeyoyyxeluzaeckwpcsa.supabase.co/storage/v1/object/public/technical-bucket/items/item_06_hdmi_short.png", "Storage Room", "RFID-ITEM-006", "SN-HDMI-006" });

            migrationBuilder.UpdateData(
                table: "ArchiveItems",
                keyColumn: "Id",
                keyValue: new Guid("00000007-0000-0000-0000-000000000006"),
                columns: new[] { "CreatedAt", "Description", "ImageUrl", "Location", "RfidUid", "SerialNumber" },
                values: new object[] { new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "USB condenser microphone, defective.", "https://oeyoyyxeluzaeckwpcsa.supabase.co/storage/v1/object/public/technical-bucket/items/item_07_usb_mic.png", "Storage Room", "RFID-ITEM-007", "SN-MIC-007" });

            migrationBuilder.InsertData(
                table: "ArchiveItems",
                columns: new[] { "Id", "Category", "Condition", "CreatedAt", "Description", "ImageUrl", "ItemMake", "ItemModel", "ItemName", "ItemType", "Location", "RfidUid", "SerialNumber", "Status", "UpdatedAt" },
                values: new object[,]
                {
                    { new Guid("00000007-0000-0000-0000-000000000007"), "Electronics", "NeedRepair", new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "Wireless optical mouse, needs repair.", "https://oeyoyyxeluzaeckwpcsa.supabase.co/storage/v1/object/public/technical-bucket/items/item_08_mouse.png", "Logitech", null, "Wireless Mouse", "Peripheral", "Storage Room", "RFID-ITEM-008", "SN-MOUSE-008", "Archived", new DateTime(2025, 1, 15, 10, 0, 0, 0, DateTimeKind.Utc) },
                    { new Guid("00000007-0000-0000-0000-000000000008"), "Electronics", "Good", new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "15-foot extension wire, retired.", "https://oeyoyyxeluzaeckwpcsa.supabase.co/storage/v1/object/public/technical-bucket/items/item_09_extension.png", "Generic", null, "Extension Wire 15ft", "Cable", "Storage Room", "RFID-ITEM-009", "SN-EXT-009", "Archived", new DateTime(2025, 1, 15, 10, 0, 0, 0, DateTimeKind.Utc) },
                    { new Guid("00000007-0000-0000-0000-000000000009"), "Electronics", "Refurbished", new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "7-port USB 3.0 hub, refurbished.", "https://oeyoyyxeluzaeckwpcsa.supabase.co/storage/v1/object/public/technical-bucket/items/item_10_usb_hub.png", "Anker", null, "USB Hub 7-Port", "Peripheral", "Storage Room", "RFID-ITEM-010", "SN-HUB-010", "Archived", new DateTime(2025, 1, 15, 10, 0, 0, 0, DateTimeKind.Utc) }
                });

            migrationBuilder.InsertData(
                table: "ArchiveLentItems",
                columns: new[] { "Id", "BorrowerFullName", "BorrowerRole", "ContactNumber", "CreatedAt", "FrontStudentIdPictureUrl", "GuestImageUrl", "IsHiddenFromUser", "ItemId", "ItemName", "LentAt", "Organization", "Purpose", "Remarks", "ReservedFor", "ReturnedAt", "Room", "Status", "StudentIdNumber", "StudentRfid", "SubjectTimeSchedule", "TagUid", "TeacherFullName", "TeacherId", "UpdatedAt", "UserId" },
                values: new object[,]
                {
                    { new Guid("00000007-0000-0000-0000-000000000011"), "Archived Student", "Student", null, new DateTime(2024, 12, 5, 14, 0, 0, 0, DateTimeKind.Utc), null, null, false, new Guid("00000005-0000-0000-0000-000000000007"), "USB Microphone", new DateTime(2024, 12, 5, 14, 0, 0, 0, DateTimeKind.Utc), null, null, null, null, new DateTime(2024, 12, 7, 14, 0, 0, 0, DateTimeKind.Utc), "Lab 201", "Returned", "2022-0099", null, "IT301 - 2:00 PM", null, "", null, new DateTime(2024, 12, 7, 14, 0, 0, 0, DateTimeKind.Utc), new Guid("00000004-0000-0000-0000-000000000005") },
                    { new Guid("00000007-0000-0000-0000-000000000012"), "Juan Dela Cruz", "Student", null, new DateTime(2024, 11, 20, 8, 0, 0, 0, DateTimeKind.Utc), null, null, false, new Guid("00000005-0000-0000-0000-000000000008"), "Wireless Mouse", new DateTime(2024, 11, 20, 8, 0, 0, 0, DateTimeKind.Utc), null, null, null, null, new DateTime(2024, 11, 22, 8, 0, 0, 0, DateTimeKind.Utc), "Lab 101", "Returned", "2023-0001", null, "CS201 - 8:00 AM", null, "", null, new DateTime(2024, 11, 22, 8, 0, 0, 0, DateTimeKind.Utc), new Guid("00000004-0000-0000-0000-000000000001") }
                });

            migrationBuilder.UpdateData(
                table: "ArchiveStaff",
                keyColumn: "Id",
                keyValue: new Guid("00000007-0000-0000-0000-000000000002"),
                column: "Position",
                value: "Former Technician");

            migrationBuilder.UpdateData(
                table: "ArchiveStudents",
                keyColumn: "Id",
                keyValue: new Guid("00000007-0000-0000-0000-000000000004"),
                columns: new[] { "Course", "Section", "StudentIdNumber", "Year" },
                values: new object[] { "Bachelor of Science in Information Technology", "D", "2022-0099", "4th Year" });

            migrationBuilder.UpdateData(
                table: "ArchiveTeachers",
                keyColumn: "Id",
                keyValue: new Guid("00000007-0000-0000-0000-000000000003"),
                column: "Department",
                value: "Former Department");

            migrationBuilder.UpdateData(
                table: "ArchiveUsers",
                keyColumn: "Id",
                keyValue: new Guid("00000007-0000-0000-0000-000000000001"),
                columns: new[] { "Email", "FirstName", "LastName", "OriginalUserId", "PasswordHash", "Username" },
                values: new object[] { "archived.admin@school.edu.ph", "Archived", "Admin", new Guid("00000001-0000-0000-0000-000000000005"), "$2a$04$WuSJZNktuI9FLcH.Az5HpeKJFhyNCxNzz7tR3jTVdzusa7qAJBBSK", "archived.admin" });

            migrationBuilder.UpdateData(
                table: "ArchiveUsers",
                keyColumn: "Id",
                keyValue: new Guid("00000007-0000-0000-0000-000000000002"),
                columns: new[] { "Email", "FirstName", "LastName", "OriginalUserId", "PasswordHash", "Username" },
                values: new object[] { "archived.staff@school.edu.ph", "Archived", "Staff", new Guid("00000002-0000-0000-0000-000000000005"), "$2a$04$WuSJZNktuI9FLcH.Az5HpeKJFhyNCxNzz7tR3jTVdzusa7qAJBBSK", "archived.staff" });

            migrationBuilder.UpdateData(
                table: "ArchiveUsers",
                keyColumn: "Id",
                keyValue: new Guid("00000007-0000-0000-0000-000000000003"),
                columns: new[] { "Email", "FirstName", "LastName", "OriginalUserId", "PasswordHash", "Username" },
                values: new object[] { "archived.teacher@school.edu.ph", "Archived", "Teacher", new Guid("00000003-0000-0000-0000-000000000005"), "$2a$04$WuSJZNktuI9FLcH.Az5HpeKJFhyNCxNzz7tR3jTVdzusa7qAJBBSK", "archived.teacher" });

            migrationBuilder.UpdateData(
                table: "ArchiveUsers",
                keyColumn: "Id",
                keyValue: new Guid("00000007-0000-0000-0000-000000000004"),
                columns: new[] { "Email", "FirstName", "LastName", "OriginalUserId", "PasswordHash", "Username" },
                values: new object[] { "archived.student@school.edu.ph", "Archived", "Student", new Guid("00000004-0000-0000-0000-000000000005"), "$2a$04$WuSJZNktuI9FLcH.Az5HpeKJFhyNCxNzz7tR3jTVdzusa7qAJBBSK", "archived.student" });

            migrationBuilder.UpdateData(
                table: "Items",
                keyColumn: "Id",
                keyValue: new Guid("00000005-0000-0000-0000-000000000001"),
                columns: new[] { "Description", "ImageUrl", "ItemModel", "Location", "RfidUid", "UpdatedAt" },
                values: new object[] { "10-foot HDMI 2.0 cable for display connections.", "https://oeyoyyxeluzaeckwpcsa.supabase.co/storage/v1/object/public/technical-bucket/items/item_01_hdmi_cable.png", "Standard HDMI 2.0", "Lab Cabinet A", "RFID-ITEM-001", new DateTime(2025, 4, 11, 8, 0, 0, 0, DateTimeKind.Utc) });

            migrationBuilder.UpdateData(
                table: "Items",
                keyColumn: "Id",
                keyValue: new Guid("00000005-0000-0000-0000-000000000002"),
                columns: new[] { "Description", "ImageUrl", "ItemModel", "Location", "RfidUid", "Status", "UpdatedAt" },
                values: new object[] { "Dual-channel wireless microphone system.", "https://oeyoyyxeluzaeckwpcsa.supabase.co/storage/v1/object/public/technical-bucket/items/item_02_wireless_mic.png", "BLX288/PG58", "Media Room Shelf", "RFID-ITEM-002", "Unavailable", new DateTime(2025, 4, 20, 9, 0, 0, 0, DateTimeKind.Utc) });

            migrationBuilder.UpdateData(
                table: "Items",
                keyColumn: "Id",
                keyValue: new Guid("00000005-0000-0000-0000-000000000003"),
                columns: new[] { "Description", "ImageUrl", "ItemMake", "ItemModel", "ItemName", "ItemType", "Location", "SerialNumber", "Status", "UpdatedAt" },
                values: new object[] { "3600-lumen portable LCD projector.", "https://oeyoyyxeluzaeckwpcsa.supabase.co/storage/v1/object/public/technical-bucket/items/item_03_projector.png", "Epson", "EB-X41", "Portable Projector", "Projector", "AV Room Cabinet", "SN-PROJ-003", "Reserved", new DateTime(2025, 4, 19, 10, 0, 0, 0, DateTimeKind.Utc) });

            migrationBuilder.UpdateData(
                table: "Items",
                keyColumn: "Id",
                keyValue: new Guid("00000005-0000-0000-0000-000000000004"),
                columns: new[] { "Category", "Description", "ImageUrl", "ItemMake", "ItemModel", "ItemName", "ItemType", "Location", "RfidUid", "SerialNumber", "Status", "UpdatedAt" },
                values: new object[] { "MediaEquipment", "Portable waterproof Bluetooth speaker.", "https://oeyoyyxeluzaeckwpcsa.supabase.co/storage/v1/object/public/technical-bucket/items/item_04_speaker.png", "JBL", "Charge 5", "Bluetooth Speaker", "Speaker", "Lab Cabinet B", "RFID-ITEM-004", "SN-SPK-004", "Available", new DateTime(2025, 4, 10, 14, 0, 0, 0, DateTimeKind.Utc) });

            migrationBuilder.UpdateData(
                table: "Items",
                keyColumn: "Id",
                keyValue: new Guid("00000005-0000-0000-0000-000000000005"),
                columns: new[] { "Description", "ImageUrl", "ItemModel", "Location", "UpdatedAt" },
                values: new object[] { "Compact TKL mechanical keyboard with RGB.", "https://oeyoyyxeluzaeckwpcsa.supabase.co/storage/v1/object/public/technical-bucket/items/item_05_keyboard.png", "K2 Pro", "Lab Cabinet A", new DateTime(2025, 3, 25, 9, 0, 0, 0, DateTimeKind.Utc) });

            migrationBuilder.UpdateData(
                table: "Items",
                keyColumn: "Id",
                keyValue: new Guid("00000005-0000-0000-0000-000000000006"),
                columns: new[] { "CreatedAt", "Description", "ImageUrl", "ItemName", "Location", "SerialNumber", "Status", "UpdatedAt" },
                values: new object[] { new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "6-foot HDMI cable, retired from service.", "https://oeyoyyxeluzaeckwpcsa.supabase.co/storage/v1/object/public/technical-bucket/items/item_06_hdmi_short.png", "HDMI Cable 6ft", "Storage Room", "SN-HDMI-006", "Archived", new DateTime(2025, 1, 15, 10, 0, 0, 0, DateTimeKind.Utc) });

            migrationBuilder.UpdateData(
                table: "Items",
                keyColumn: "Id",
                keyValue: new Guid("00000005-0000-0000-0000-000000000007"),
                columns: new[] { "Category", "Condition", "CreatedAt", "Description", "ImageUrl", "ItemMake", "ItemName", "ItemType", "Location", "RfidUid", "SerialNumber", "UpdatedAt" },
                values: new object[] { "MediaEquipment", "Defective", new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "USB condenser microphone, defective.", "https://oeyoyyxeluzaeckwpcsa.supabase.co/storage/v1/object/public/technical-bucket/items/item_07_usb_mic.png", "Blue", "USB Microphone", "Microphone", "Storage Room", "RFID-ITEM-007", "SN-MIC-007", new DateTime(2025, 1, 15, 10, 0, 0, 0, DateTimeKind.Utc) });

            migrationBuilder.UpdateData(
                table: "Items",
                keyColumn: "Id",
                keyValue: new Guid("00000005-0000-0000-0000-000000000008"),
                columns: new[] { "Category", "Condition", "CreatedAt", "Description", "ImageUrl", "ItemMake", "ItemName", "ItemType", "Location", "RfidUid", "SerialNumber", "UpdatedAt" },
                values: new object[] { "Electronics", "NeedRepair", new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "Wireless optical mouse, needs repair.", "https://oeyoyyxeluzaeckwpcsa.supabase.co/storage/v1/object/public/technical-bucket/items/item_08_mouse.png", "Logitech", "Wireless Mouse", "Peripheral", "Storage Room", "RFID-ITEM-008", "SN-MOUSE-008", new DateTime(2025, 1, 15, 10, 0, 0, 0, DateTimeKind.Utc) });

            migrationBuilder.InsertData(
                table: "Items",
                columns: new[] { "Id", "Category", "Condition", "CreatedAt", "Description", "ImageUrl", "ItemMake", "ItemModel", "ItemName", "ItemType", "Location", "RfidUid", "SerialNumber", "Status", "UpdatedAt" },
                values: new object[,]
                {
                    { new Guid("00000005-0000-0000-0000-000000000009"), "Electronics", "Good", new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "15-foot extension wire, retired.", "https://oeyoyyxeluzaeckwpcsa.supabase.co/storage/v1/object/public/technical-bucket/items/item_09_extension.png", "Generic", null, "Extension Wire 15ft", "Cable", "Storage Room", "RFID-ITEM-009", "SN-EXT-009", "Archived", new DateTime(2025, 1, 15, 10, 0, 0, 0, DateTimeKind.Utc) },
                    { new Guid("00000005-0000-0000-0000-000000000010"), "Electronics", "Refurbished", new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "7-port USB 3.0 hub, refurbished.", "https://oeyoyyxeluzaeckwpcsa.supabase.co/storage/v1/object/public/technical-bucket/items/item_10_usb_hub.png", "Anker", null, "USB Hub 7-Port", "Peripheral", "Storage Room", "RFID-ITEM-010", "SN-HUB-010", "Archived", new DateTime(2025, 1, 15, 10, 0, 0, 0, DateTimeKind.Utc) }
                });

            migrationBuilder.UpdateData(
                table: "LentItems",
                keyColumn: "Id",
                keyValue: new Guid("00000006-0000-0000-0000-000000000001"),
                columns: new[] { "BorrowerFullName", "CreatedAt", "FrontStudentIdPictureUrl", "LentAt", "StudentRfid", "TagUid", "UpdatedAt" },
                values: new object[] { "Juan Dela Cruz", new DateTime(2025, 4, 20, 8, 0, 0, 0, DateTimeKind.Utc), "https://oeyoyyxeluzaeckwpcsa.supabase.co/storage/v1/object/public/technical-bucket/students/id-front/student_01_id_front.png", new DateTime(2025, 4, 20, 8, 0, 0, 0, DateTimeKind.Utc), "RFID-STU-001", "RFID-ITEM-001", new DateTime(2025, 4, 20, 8, 0, 0, 0, DateTimeKind.Utc) });

            migrationBuilder.UpdateData(
                table: "LentItems",
                keyColumn: "Id",
                keyValue: new Guid("00000006-0000-0000-0000-000000000002"),
                columns: new[] { "BorrowerFullName", "CreatedAt", "FrontStudentIdPictureUrl", "ItemId", "ItemName", "LentAt", "ReservedFor", "ReturnedAt", "Status", "StudentRfid", "TagUid", "UpdatedAt" },
                values: new object[] { "Maria Santos", new DateTime(2025, 4, 20, 9, 0, 0, 0, DateTimeKind.Utc), "https://oeyoyyxeluzaeckwpcsa.supabase.co/storage/v1/object/public/technical-bucket/students/id-front/student_02_id_front.png", new Guid("00000005-0000-0000-0000-000000000002"), "Wireless Microphone", null, new DateTime(2025, 4, 27, 10, 0, 0, 0, DateTimeKind.Utc), null, "Pending", "RFID-STU-002", "RFID-ITEM-002", new DateTime(2025, 4, 20, 9, 0, 0, 0, DateTimeKind.Utc) });

            migrationBuilder.UpdateData(
                table: "LentItems",
                keyColumn: "Id",
                keyValue: new Guid("00000006-0000-0000-0000-000000000003"),
                columns: new[] { "BorrowerFullName", "BorrowerRole", "CreatedAt", "ItemId", "ItemName", "LentAt", "ReservedFor", "Room", "Status", "StudentIdNumber", "SubjectTimeSchedule", "TagUid", "TeacherFullName", "TeacherId", "UpdatedAt", "UserId" },
                values: new object[] { "Alice Williams", "Teacher", new DateTime(2025, 4, 19, 10, 0, 0, 0, DateTimeKind.Utc), new Guid("00000005-0000-0000-0000-000000000003"), "Portable Projector", null, new DateTime(2025, 4, 26, 13, 0, 0, 0, DateTimeKind.Utc), "Room 201", "Approved", null, "IT401 - 1:00 PM", "RFID-ITEM-003", "Alice Williams", new Guid("00000003-0000-0000-0000-000000000001"), new DateTime(2025, 4, 19, 11, 0, 0, 0, DateTimeKind.Utc), new Guid("00000003-0000-0000-0000-000000000001") });

            migrationBuilder.UpdateData(
                table: "LentItems",
                keyColumn: "Id",
                keyValue: new Guid("00000006-0000-0000-0000-000000000004"),
                columns: new[] { "BorrowerFullName", "BorrowerRole", "CreatedAt", "FrontStudentIdPictureUrl", "ItemName", "LentAt", "ReturnedAt", "Room", "Status", "StudentIdNumber", "StudentRfid", "SubjectTimeSchedule", "TagUid", "TeacherFullName", "TeacherId", "UpdatedAt", "UserId" },
                values: new object[] { "Pedro Reyes", "Student", new DateTime(2025, 4, 10, 13, 0, 0, 0, DateTimeKind.Utc), "https://oeyoyyxeluzaeckwpcsa.supabase.co/storage/v1/object/public/technical-bucket/students/id-front/student_03_id_front.png", "Bluetooth Speaker", new DateTime(2025, 4, 10, 13, 0, 0, 0, DateTimeKind.Utc), new DateTime(2025, 4, 14, 15, 0, 0, 0, DateTimeKind.Utc), "Lab 103", "Returned", "2023-0003", "RFID-STU-003", "CS302 - 1:00 PM", "RFID-ITEM-004", "", null, new DateTime(2025, 4, 14, 15, 0, 0, 0, DateTimeKind.Utc), new Guid("00000004-0000-0000-0000-000000000003") });

            migrationBuilder.UpdateData(
                table: "LentItems",
                keyColumn: "Id",
                keyValue: new Guid("00000006-0000-0000-0000-000000000005"),
                columns: new[] { "CreatedAt", "LentAt", "ReservedFor", "ReturnedAt", "Status", "TagUid", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 4, 14, 9, 0, 0, 0, DateTimeKind.Utc), null, new DateTime(2025, 4, 15, 9, 0, 0, 0, DateTimeKind.Utc), null, "Expired", "RFID-ITEM-005", new DateTime(2025, 4, 15, 9, 31, 0, 0, DateTimeKind.Utc) });

            migrationBuilder.UpdateData(
                table: "Students",
                keyColumn: "Id",
                keyValue: new Guid("00000004-0000-0000-0000-000000000001"),
                columns: new[] { "BackStudentIdPictureUrl", "CityMunicipality", "Course", "FrontStudentIdPictureUrl", "PostalCode", "ProfilePictureUrl", "Province", "RfidUid", "Street" },
                values: new object[] { "https://oeyoyyxeluzaeckwpcsa.supabase.co/storage/v1/object/public/technical-bucket/students/id-back/student_01_id_back.png", "Quezon City", "Bachelor of Science in Computer Science", "https://oeyoyyxeluzaeckwpcsa.supabase.co/storage/v1/object/public/technical-bucket/students/id-front/student_01_id_front.png", "1100", "https://oeyoyyxeluzaeckwpcsa.supabase.co/storage/v1/object/public/technical-bucket/students/profile/student_01_profile.png", "Metro Manila", "RFID-STU-001", "123 Rizal Street" });

            migrationBuilder.UpdateData(
                table: "Students",
                keyColumn: "Id",
                keyValue: new Guid("00000004-0000-0000-0000-000000000002"),
                columns: new[] { "BackStudentIdPictureUrl", "CityMunicipality", "Course", "FrontStudentIdPictureUrl", "PostalCode", "ProfilePictureUrl", "Province", "RfidUid", "Street" },
                values: new object[] { "https://oeyoyyxeluzaeckwpcsa.supabase.co/storage/v1/object/public/technical-bucket/students/id-back/student_02_id_back.png", "Makati City", "Bachelor of Science in Information Technology", "https://oeyoyyxeluzaeckwpcsa.supabase.co/storage/v1/object/public/technical-bucket/students/id-front/student_02_id_front.png", "1200", "https://oeyoyyxeluzaeckwpcsa.supabase.co/storage/v1/object/public/technical-bucket/students/profile/student_02_profile.png", "Metro Manila", "RFID-STU-002", "456 Mabini Avenue" });

            migrationBuilder.UpdateData(
                table: "Students",
                keyColumn: "Id",
                keyValue: new Guid("00000004-0000-0000-0000-000000000003"),
                columns: new[] { "BackStudentIdPictureUrl", "CityMunicipality", "Course", "FrontStudentIdPictureUrl", "PostalCode", "ProfilePictureUrl", "Province", "RfidUid", "Street" },
                values: new object[] { "https://oeyoyyxeluzaeckwpcsa.supabase.co/storage/v1/object/public/technical-bucket/students/id-back/student_03_id_back.png", "Pasig City", "Bachelor of Science in Computer Science", "https://oeyoyyxeluzaeckwpcsa.supabase.co/storage/v1/object/public/technical-bucket/students/id-front/student_03_id_front.png", "1600", "https://oeyoyyxeluzaeckwpcsa.supabase.co/storage/v1/object/public/technical-bucket/students/profile/student_03_profile.png", "Metro Manila", "RFID-STU-003", "789 Bonifacio Road" });

            migrationBuilder.UpdateData(
                table: "Students",
                keyColumn: "Id",
                keyValue: new Guid("00000004-0000-0000-0000-000000000004"),
                columns: new[] { "BackStudentIdPictureUrl", "CityMunicipality", "Course", "FrontStudentIdPictureUrl", "PostalCode", "ProfilePictureUrl", "Province", "RfidUid", "Street" },
                values: new object[] { "https://oeyoyyxeluzaeckwpcsa.supabase.co/storage/v1/object/public/technical-bucket/students/id-back/student_04_id_back.png", "Mandaluyong City", "Bachelor of Multimedia Arts", "https://oeyoyyxeluzaeckwpcsa.supabase.co/storage/v1/object/public/technical-bucket/students/id-front/student_04_id_front.png", "1550", "https://oeyoyyxeluzaeckwpcsa.supabase.co/storage/v1/object/public/technical-bucket/students/profile/student_04_profile.png", "Metro Manila", "RFID-STU-004", "321 Luna Street" });

            migrationBuilder.UpdateData(
                table: "Students",
                keyColumn: "Id",
                keyValue: new Guid("00000004-0000-0000-0000-000000000005"),
                columns: new[] { "BackStudentIdPictureUrl", "CityMunicipality", "Course", "FrontStudentIdPictureUrl", "PostalCode", "ProfilePictureUrl", "Province", "Section", "Street", "StudentIdNumber", "Year" },
                values: new object[] { "https://oeyoyyxeluzaeckwpcsa.supabase.co/storage/v1/object/public/technical-bucket/students/id-back/student_05_id_back.png", "Taguig City", "Bachelor of Science in Information Technology", "https://oeyoyyxeluzaeckwpcsa.supabase.co/storage/v1/object/public/technical-bucket/students/id-front/student_05_id_front.png", "1630", "https://oeyoyyxeluzaeckwpcsa.supabase.co/storage/v1/object/public/technical-bucket/students/profile/student_05_profile.png", "Metro Manila", "D", "999 Old Street", "2022-0099", "4th Year" });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("00000001-0000-0000-0000-000000000001"),
                columns: new[] { "Email", "PasswordHash", "Status", "Username" },
                values: new object[] { "superadmin@school.edu.ph", "$2a$04$WuSJZNktuI9FLcH.Az5HpeKJFhyNCxNzz7tR3jTVdzusa7qAJBBSK", "Offline", "sadmin" });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("00000001-0000-0000-0000-000000000002"),
                columns: new[] { "Email", "FirstName", "LastName", "PasswordHash", "Status", "Username" },
                values: new object[] { "christian.admin@school.edu.ph", "Christian", "Dela Cruz", "$2a$04$WuSJZNktuI9FLcH.Az5HpeKJFhyNCxNzz7tR3jTVdzusa7qAJBBSK", "Offline", "christian" });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("00000001-0000-0000-0000-000000000003"),
                columns: new[] { "Email", "FirstName", "LastName", "PasswordHash", "Status", "Username" },
                values: new object[] { "ejay.admin@school.edu.ph", "Ejay", "Santos", "$2a$04$WuSJZNktuI9FLcH.Az5HpeKJFhyNCxNzz7tR3jTVdzusa7qAJBBSK", "Offline", "ejay" });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("00000001-0000-0000-0000-000000000004"),
                columns: new[] { "Email", "FirstName", "PasswordHash", "Status", "Username" },
                values: new object[] { "stan.admin@school.edu.ph", "Stan", "$2a$04$WuSJZNktuI9FLcH.Az5HpeKJFhyNCxNzz7tR3jTVdzusa7qAJBBSK", "Offline", "stan" });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("00000002-0000-0000-0000-000000000001"),
                columns: new[] { "Email", "PasswordHash", "Status" },
                values: new object[] { "carlos.mendoza@school.edu.ph", "$2a$04$WuSJZNktuI9FLcH.Az5HpeKJFhyNCxNzz7tR3jTVdzusa7qAJBBSK", "Offline" });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("00000002-0000-0000-0000-000000000002"),
                columns: new[] { "Email", "PasswordHash", "Status" },
                values: new object[] { "rosa.garcia@school.edu.ph", "$2a$04$WuSJZNktuI9FLcH.Az5HpeKJFhyNCxNzz7tR3jTVdzusa7qAJBBSK", "Offline" });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("00000002-0000-0000-0000-000000000003"),
                columns: new[] { "Email", "FirstName", "PasswordHash", "Status", "Username" },
                values: new object[] { "ben.torres@school.edu.ph", "Ben", "$2a$04$WuSJZNktuI9FLcH.Az5HpeKJFhyNCxNzz7tR3jTVdzusa7qAJBBSK", "Offline", "btorres" });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("00000003-0000-0000-0000-000000000001"),
                columns: new[] { "Email", "PasswordHash", "Status" },
                values: new object[] { "alice.williams@school.edu.ph", "$2a$04$WuSJZNktuI9FLcH.Az5HpeKJFhyNCxNzz7tR3jTVdzusa7qAJBBSK", "Offline" });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("00000003-0000-0000-0000-000000000002"),
                columns: new[] { "Email", "PasswordHash", "Status" },
                values: new object[] { "roberto.cruz@school.edu.ph", "$2a$04$WuSJZNktuI9FLcH.Az5HpeKJFhyNCxNzz7tR3jTVdzusa7qAJBBSK", "Offline" });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("00000003-0000-0000-0000-000000000003"),
                columns: new[] { "Email", "PasswordHash", "Status" },
                values: new object[] { "elena.fernandez@school.edu.ph", "$2a$04$WuSJZNktuI9FLcH.Az5HpeKJFhyNCxNzz7tR3jTVdzusa7qAJBBSK", "Offline" });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("00000003-0000-0000-0000-000000000004"),
                columns: new[] { "Email", "FirstName", "LastName", "PasswordHash", "Status", "Username" },
                values: new object[] { "jose.miranda@school.edu.ph", "Jose", "Miranda", "$2a$04$WuSJZNktuI9FLcH.Az5HpeKJFhyNCxNzz7tR3jTVdzusa7qAJBBSK", "Offline", "jmiranda" });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("00000004-0000-0000-0000-000000000001"),
                columns: new[] { "Email", "FirstName", "LastName", "PasswordHash", "PhoneNumber", "Status", "Username" },
                values: new object[] { "juan.delacruz@school.edu.ph", "Juan", "Dela Cruz", "$2a$04$WuSJZNktuI9FLcH.Az5HpeKJFhyNCxNzz7tR3jTVdzusa7qAJBBSK", "09171234501", "Offline", "jdelacruz" });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("00000004-0000-0000-0000-000000000002"),
                columns: new[] { "Email", "FirstName", "LastName", "PasswordHash", "PhoneNumber", "Status", "Username" },
                values: new object[] { "maria.santos@school.edu.ph", "Maria", "Santos", "$2a$04$WuSJZNktuI9FLcH.Az5HpeKJFhyNCxNzz7tR3jTVdzusa7qAJBBSK", "09171234502", "Offline", "msantos" });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("00000004-0000-0000-0000-000000000003"),
                columns: new[] { "Email", "FirstName", "LastName", "PasswordHash", "PhoneNumber", "Status", "Username" },
                values: new object[] { "pedro.reyes@school.edu.ph", "Pedro", "Reyes", "$2a$04$WuSJZNktuI9FLcH.Az5HpeKJFhyNCxNzz7tR3jTVdzusa7qAJBBSK", "09171234503", "Offline", "preyes" });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("00000004-0000-0000-0000-000000000004"),
                columns: new[] { "Email", "FirstName", "LastName", "PasswordHash", "PhoneNumber", "Status", "Username" },
                values: new object[] { "ana.garcia@school.edu.ph", "Ana", "Garcia", "$2a$04$WuSJZNktuI9FLcH.Az5HpeKJFhyNCxNzz7tR3jTVdzusa7qAJBBSK", "09171234504", "Offline", "agarcia" });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("00000004-0000-0000-0000-000000000005"),
                columns: new[] { "Email", "FirstName", "LastName", "PasswordHash", "PhoneNumber", "Status", "Username" },
                values: new object[] { "archived.student@school.edu.ph", "Archived", "Student", "$2a$04$WuSJZNktuI9FLcH.Az5HpeKJFhyNCxNzz7tR3jTVdzusa7qAJBBSK", "09170000000", "Inactive", "archived.student" });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "Email", "FirstName", "LastName", "MiddleName", "PasswordHash", "PhoneNumber", "Status", "UserRole", "Username" },
                values: new object[,]
                {
                    { new Guid("00000001-0000-0000-0000-000000000005"), "archived.admin@school.edu.ph", "Archived", "Admin", null, "$2a$04$WuSJZNktuI9FLcH.Az5HpeKJFhyNCxNzz7tR3jTVdzusa7qAJBBSK", null, "Inactive", "Admin", "archived.admin" },
                    { new Guid("00000002-0000-0000-0000-000000000004"), "liza.villanueva@school.edu.ph", "Liza", "Villanueva", null, "$2a$04$WuSJZNktuI9FLcH.Az5HpeKJFhyNCxNzz7tR3jTVdzusa7qAJBBSK", null, "Offline", "Staff", "lvillanueva" },
                    { new Guid("00000002-0000-0000-0000-000000000005"), "archived.staff@school.edu.ph", "Archived", "Staff", null, "$2a$04$WuSJZNktuI9FLcH.Az5HpeKJFhyNCxNzz7tR3jTVdzusa7qAJBBSK", null, "Inactive", "Staff", "archived.staff" },
                    { new Guid("00000003-0000-0000-0000-000000000005"), "archived.teacher@school.edu.ph", "Archived", "Teacher", null, "$2a$04$WuSJZNktuI9FLcH.Az5HpeKJFhyNCxNzz7tR3jTVdzusa7qAJBBSK", null, "Inactive", "Teacher", "archived.teacher" }
                });

            migrationBuilder.InsertData(
                table: "ArchiveLentItems",
                columns: new[] { "Id", "BorrowerFullName", "BorrowerRole", "ContactNumber", "CreatedAt", "FrontStudentIdPictureUrl", "GuestImageUrl", "IsHiddenFromUser", "ItemId", "ItemName", "LentAt", "Organization", "Purpose", "Remarks", "ReservedFor", "ReturnedAt", "Room", "Status", "StudentIdNumber", "StudentRfid", "SubjectTimeSchedule", "TagUid", "TeacherFullName", "TeacherId", "UpdatedAt", "UserId" },
                values: new object[,]
                {
                    { new Guid("00000007-0000-0000-0000-000000000013"), "Alice Williams", "Teacher", null, new DateTime(2024, 10, 15, 15, 0, 0, 0, DateTimeKind.Utc), null, null, false, new Guid("00000005-0000-0000-0000-000000000009"), "Extension Wire 15ft", new DateTime(2024, 10, 15, 15, 0, 0, 0, DateTimeKind.Utc), null, null, null, null, new DateTime(2024, 10, 17, 15, 0, 0, 0, DateTimeKind.Utc), "Room 201", "Returned", null, null, "IT401 - 3:00 PM", null, "Alice Williams", new Guid("00000003-0000-0000-0000-000000000001"), new DateTime(2024, 10, 17, 15, 0, 0, 0, DateTimeKind.Utc), new Guid("00000003-0000-0000-0000-000000000001") },
                    { new Guid("00000007-0000-0000-0000-000000000014"), "Maria Santos", "Student", null, new DateTime(2024, 9, 9, 10, 0, 0, 0, DateTimeKind.Utc), null, null, false, new Guid("00000005-0000-0000-0000-000000000010"), "USB Hub 7-Port", null, null, null, null, new DateTime(2024, 9, 10, 10, 0, 0, 0, DateTimeKind.Utc), null, "Lab 102", "Expired", "2023-0002", null, "IT201 - 10:00 AM", null, "", null, new DateTime(2024, 9, 10, 10, 31, 0, 0, DateTimeKind.Utc), new Guid("00000004-0000-0000-0000-000000000002") }
                });

            migrationBuilder.InsertData(
                table: "Staff",
                columns: new[] { "Id", "Position" },
                values: new object[,]
                {
                    { new Guid("00000002-0000-0000-0000-000000000004"), "Asset Custodian" },
                    { new Guid("00000002-0000-0000-0000-000000000005"), "Former Technician" }
                });

            migrationBuilder.InsertData(
                table: "Teachers",
                columns: new[] { "Id", "Department" },
                values: new object[] { new Guid("00000003-0000-0000-0000-000000000005"), "Former Department" });

            migrationBuilder.InsertData(
                table: "ArchiveLentItems",
                columns: new[] { "Id", "BorrowerFullName", "BorrowerRole", "ContactNumber", "CreatedAt", "FrontStudentIdPictureUrl", "GuestImageUrl", "IsHiddenFromUser", "ItemId", "ItemName", "LentAt", "Organization", "Purpose", "Remarks", "ReservedFor", "ReturnedAt", "Room", "Status", "StudentIdNumber", "StudentRfid", "SubjectTimeSchedule", "TagUid", "TeacherFullName", "TeacherId", "UpdatedAt", "UserId" },
                values: new object[] { new Guid("00000007-0000-0000-0000-000000000010"), "Archived Teacher", "Teacher", null, new DateTime(2025, 1, 10, 11, 0, 0, 0, DateTimeKind.Utc), null, null, false, new Guid("00000005-0000-0000-0000-000000000006"), "HDMI Cable 6ft", new DateTime(2025, 1, 10, 11, 0, 0, 0, DateTimeKind.Utc), null, null, null, null, new DateTime(2025, 1, 14, 11, 0, 0, 0, DateTimeKind.Utc), "Room 301", "Returned", null, null, "MA101 - 11:00 AM", null, "Archived Teacher", new Guid("00000003-0000-0000-0000-000000000005"), new DateTime(2025, 1, 15, 10, 0, 0, 0, DateTimeKind.Utc), new Guid("00000003-0000-0000-0000-000000000005") });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "ArchiveItems",
                keyColumn: "Id",
                keyValue: new Guid("00000007-0000-0000-0000-000000000007"));

            migrationBuilder.DeleteData(
                table: "ArchiveItems",
                keyColumn: "Id",
                keyValue: new Guid("00000007-0000-0000-0000-000000000008"));

            migrationBuilder.DeleteData(
                table: "ArchiveItems",
                keyColumn: "Id",
                keyValue: new Guid("00000007-0000-0000-0000-000000000009"));

            migrationBuilder.DeleteData(
                table: "ArchiveLentItems",
                keyColumn: "Id",
                keyValue: new Guid("00000007-0000-0000-0000-000000000010"));

            migrationBuilder.DeleteData(
                table: "ArchiveLentItems",
                keyColumn: "Id",
                keyValue: new Guid("00000007-0000-0000-0000-000000000011"));

            migrationBuilder.DeleteData(
                table: "ArchiveLentItems",
                keyColumn: "Id",
                keyValue: new Guid("00000007-0000-0000-0000-000000000012"));

            migrationBuilder.DeleteData(
                table: "ArchiveLentItems",
                keyColumn: "Id",
                keyValue: new Guid("00000007-0000-0000-0000-000000000013"));

            migrationBuilder.DeleteData(
                table: "ArchiveLentItems",
                keyColumn: "Id",
                keyValue: new Guid("00000007-0000-0000-0000-000000000014"));

            migrationBuilder.DeleteData(
                table: "Staff",
                keyColumn: "Id",
                keyValue: new Guid("00000002-0000-0000-0000-000000000004"));

            migrationBuilder.DeleteData(
                table: "Staff",
                keyColumn: "Id",
                keyValue: new Guid("00000002-0000-0000-0000-000000000005"));

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("00000001-0000-0000-0000-000000000005"));

            migrationBuilder.DeleteData(
                table: "Items",
                keyColumn: "Id",
                keyValue: new Guid("00000005-0000-0000-0000-000000000009"));

            migrationBuilder.DeleteData(
                table: "Items",
                keyColumn: "Id",
                keyValue: new Guid("00000005-0000-0000-0000-000000000010"));

            migrationBuilder.DeleteData(
                table: "Teachers",
                keyColumn: "Id",
                keyValue: new Guid("00000003-0000-0000-0000-000000000005"));

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("00000002-0000-0000-0000-000000000004"));

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("00000002-0000-0000-0000-000000000005"));

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("00000003-0000-0000-0000-000000000005"));

            migrationBuilder.UpdateData(
                table: "ArchiveItems",
                keyColumn: "Id",
                keyValue: new Guid("00000007-0000-0000-0000-000000000005"),
                columns: new[] { "CreatedAt", "Description", "ImageUrl", "Location", "RfidUid", "SerialNumber" },
                values: new object[] { new DateTime(2024, 6, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, null, null, null, "SN-HDMI-007" });

            migrationBuilder.UpdateData(
                table: "ArchiveItems",
                keyColumn: "Id",
                keyValue: new Guid("00000007-0000-0000-0000-000000000006"),
                columns: new[] { "CreatedAt", "Description", "ImageUrl", "Location", "RfidUid", "SerialNumber" },
                values: new object[] { new DateTime(2024, 6, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, null, null, null, "SN-MIC-008" });

            migrationBuilder.InsertData(
                table: "ArchiveLentItems",
                columns: new[] { "Id", "BorrowerFullName", "BorrowerRole", "ContactNumber", "CreatedAt", "FrontStudentIdPictureUrl", "GuestImageUrl", "IsHiddenFromUser", "ItemId", "ItemName", "LentAt", "Organization", "Purpose", "Remarks", "ReservedFor", "ReturnedAt", "Room", "Status", "StudentIdNumber", "StudentRfid", "SubjectTimeSchedule", "TagUid", "TeacherFullName", "TeacherId", "UpdatedAt", "UserId" },
                values: new object[] { new Guid("00000007-0000-0000-0000-000000000007"), "David Ramos", "Teacher", null, new DateTime(2025, 1, 10, 11, 0, 0, 0, DateTimeKind.Utc), null, null, false, new Guid("00000005-0000-0000-0000-000000000007"), "HDMI Cable 6ft", new DateTime(2025, 1, 10, 11, 0, 0, 0, DateTimeKind.Utc), null, null, null, null, new DateTime(2025, 1, 14, 11, 0, 0, 0, DateTimeKind.Utc), "Room 301", "Returned", null, null, "MA101 - 11:00 AM", null, "David Ramos", new Guid("00000003-0000-0000-0000-000000000004"), new DateTime(2025, 1, 15, 10, 0, 0, 0, DateTimeKind.Utc), new Guid("00000003-0000-0000-0000-000000000004") });

            migrationBuilder.UpdateData(
                table: "ArchiveStaff",
                keyColumn: "Id",
                keyValue: new Guid("00000007-0000-0000-0000-000000000002"),
                column: "Position",
                value: "IT Support");

            migrationBuilder.UpdateData(
                table: "ArchiveStudents",
                keyColumn: "Id",
                keyValue: new Guid("00000007-0000-0000-0000-000000000004"),
                columns: new[] { "Course", "Section", "StudentIdNumber", "Year" },
                values: new object[] { "Computer Science", "B", "2024-0001", "1st Year" });

            migrationBuilder.UpdateData(
                table: "ArchiveTeachers",
                keyColumn: "Id",
                keyValue: new Guid("00000007-0000-0000-0000-000000000003"),
                column: "Department",
                value: "Multimedia Arts");

            migrationBuilder.UpdateData(
                table: "ArchiveUsers",
                keyColumn: "Id",
                keyValue: new Guid("00000007-0000-0000-0000-000000000001"),
                columns: new[] { "Email", "FirstName", "LastName", "OriginalUserId", "PasswordHash", "Username" },
                values: new object[] { "ana.reyes@gmail.com", "Ana", "Reyes", new Guid("00000001-0000-0000-0000-000000000004"), "$2a$04$XXYFHeKNDbLGqUzLB1yWjuPghWlA2zdvpbVQwQEa20ChcEN9hxUsO", "areyes" });

            migrationBuilder.UpdateData(
                table: "ArchiveUsers",
                keyColumn: "Id",
                keyValue: new Guid("00000007-0000-0000-0000-000000000002"),
                columns: new[] { "Email", "FirstName", "LastName", "OriginalUserId", "PasswordHash", "Username" },
                values: new object[] { "miguel.torres@gmail.com", "Miguel", "Torres", new Guid("00000002-0000-0000-0000-000000000003"), "$2a$04$XXYFHeKNDbLGqUzLB1yWjuPghWlA2zdvpbVQwQEa20ChcEN9hxUsO", "mtorres" });

            migrationBuilder.UpdateData(
                table: "ArchiveUsers",
                keyColumn: "Id",
                keyValue: new Guid("00000007-0000-0000-0000-000000000003"),
                columns: new[] { "Email", "FirstName", "LastName", "OriginalUserId", "PasswordHash", "Username" },
                values: new object[] { "david.ramos@gmail.com", "David", "Ramos", new Guid("00000003-0000-0000-0000-000000000004"), "$2a$04$XXYFHeKNDbLGqUzLB1yWjuPghWlA2zdvpbVQwQEa20ChcEN9hxUsO", "dramos" });

            migrationBuilder.UpdateData(
                table: "ArchiveUsers",
                keyColumn: "Id",
                keyValue: new Guid("00000007-0000-0000-0000-000000000004"),
                columns: new[] { "Email", "FirstName", "LastName", "OriginalUserId", "PasswordHash", "Username" },
                values: new object[] { "sofia.gonzales@gmail.com", "Sofia", "Gonzales", new Guid("00000004-0000-0000-0000-000000000006"), "$2a$04$XXYFHeKNDbLGqUzLB1yWjuPghWlA2zdvpbVQwQEa20ChcEN9hxUsO", "sgonzales" });

            migrationBuilder.UpdateData(
                table: "Items",
                keyColumn: "Id",
                keyValue: new Guid("00000005-0000-0000-0000-000000000001"),
                columns: new[] { "Description", "ImageUrl", "ItemModel", "Location", "RfidUid", "UpdatedAt" },
                values: new object[] { null, null, null, null, null, new DateTime(2024, 6, 1, 0, 0, 0, 0, DateTimeKind.Utc) });

            migrationBuilder.UpdateData(
                table: "Items",
                keyColumn: "Id",
                keyValue: new Guid("00000005-0000-0000-0000-000000000002"),
                columns: new[] { "Description", "ImageUrl", "ItemModel", "Location", "RfidUid", "Status", "UpdatedAt" },
                values: new object[] { null, null, null, null, null, "Borrowed", new DateTime(2024, 6, 1, 0, 0, 0, 0, DateTimeKind.Utc) });

            migrationBuilder.UpdateData(
                table: "Items",
                keyColumn: "Id",
                keyValue: new Guid("00000005-0000-0000-0000-000000000003"),
                columns: new[] { "Description", "ImageUrl", "ItemMake", "ItemModel", "ItemName", "ItemType", "Location", "SerialNumber", "Status", "UpdatedAt" },
                values: new object[] { null, null, "JBL", null, "Portable Bluetooth Speaker", "Speaker", null, "SN-SPK-003", "Available", new DateTime(2024, 6, 1, 0, 0, 0, 0, DateTimeKind.Utc) });

            migrationBuilder.UpdateData(
                table: "Items",
                keyColumn: "Id",
                keyValue: new Guid("00000005-0000-0000-0000-000000000004"),
                columns: new[] { "Category", "Description", "ImageUrl", "ItemMake", "ItemModel", "ItemName", "ItemType", "Location", "RfidUid", "SerialNumber", "Status", "UpdatedAt" },
                values: new object[] { "Electronics", null, null, "Logitech", null, "Wireless Mouse", "Peripheral", null, null, "SN-MOUSE-004", "Borrowed", new DateTime(2024, 6, 1, 0, 0, 0, 0, DateTimeKind.Utc) });

            migrationBuilder.UpdateData(
                table: "Items",
                keyColumn: "Id",
                keyValue: new Guid("00000005-0000-0000-0000-000000000005"),
                columns: new[] { "Description", "ImageUrl", "ItemModel", "Location", "UpdatedAt" },
                values: new object[] { null, null, null, null, new DateTime(2024, 6, 1, 0, 0, 0, 0, DateTimeKind.Utc) });

            migrationBuilder.UpdateData(
                table: "Items",
                keyColumn: "Id",
                keyValue: new Guid("00000005-0000-0000-0000-000000000006"),
                columns: new[] { "CreatedAt", "Description", "ImageUrl", "ItemName", "Location", "SerialNumber", "Status", "UpdatedAt" },
                values: new object[] { new DateTime(2024, 6, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, null, "Extension Wire 15ft", null, "SN-EXT-006", "Available", new DateTime(2024, 6, 1, 0, 0, 0, 0, DateTimeKind.Utc) });

            migrationBuilder.UpdateData(
                table: "Items",
                keyColumn: "Id",
                keyValue: new Guid("00000005-0000-0000-0000-000000000007"),
                columns: new[] { "Category", "Condition", "CreatedAt", "Description", "ImageUrl", "ItemMake", "ItemName", "ItemType", "Location", "RfidUid", "SerialNumber", "UpdatedAt" },
                values: new object[] { "Electronics", "Good", new DateTime(2024, 6, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, null, "Generic", "HDMI Cable 6ft", "Cable", null, null, "SN-HDMI-007", new DateTime(2024, 6, 1, 0, 0, 0, 0, DateTimeKind.Utc) });

            migrationBuilder.UpdateData(
                table: "Items",
                keyColumn: "Id",
                keyValue: new Guid("00000005-0000-0000-0000-000000000008"),
                columns: new[] { "Category", "Condition", "CreatedAt", "Description", "ImageUrl", "ItemMake", "ItemName", "ItemType", "Location", "RfidUid", "SerialNumber", "UpdatedAt" },
                values: new object[] { "MediaEquipment", "Defective", new DateTime(2024, 6, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, null, "Blue", "USB Microphone", "Microphone", null, null, "SN-MIC-008", new DateTime(2024, 6, 1, 0, 0, 0, 0, DateTimeKind.Utc) });

            migrationBuilder.UpdateData(
                table: "LentItems",
                keyColumn: "Id",
                keyValue: new Guid("00000006-0000-0000-0000-000000000001"),
                columns: new[] { "BorrowerFullName", "CreatedAt", "FrontStudentIdPictureUrl", "LentAt", "StudentRfid", "TagUid", "UpdatedAt" },
                values: new object[] { "John Doe", new DateTime(2025, 4, 11, 8, 0, 0, 0, DateTimeKind.Utc), null, new DateTime(2025, 4, 11, 8, 0, 0, 0, DateTimeKind.Utc), null, null, new DateTime(2025, 4, 11, 8, 0, 0, 0, DateTimeKind.Utc) });

            migrationBuilder.UpdateData(
                table: "LentItems",
                keyColumn: "Id",
                keyValue: new Guid("00000006-0000-0000-0000-000000000002"),
                columns: new[] { "BorrowerFullName", "CreatedAt", "FrontStudentIdPictureUrl", "ItemId", "ItemName", "LentAt", "ReservedFor", "ReturnedAt", "Status", "StudentRfid", "TagUid", "UpdatedAt" },
                values: new object[] { "Jane Smith", new DateTime(2025, 4, 6, 10, 0, 0, 0, DateTimeKind.Utc), null, new Guid("00000005-0000-0000-0000-000000000003"), "Portable Bluetooth Speaker", new DateTime(2025, 4, 6, 10, 0, 0, 0, DateTimeKind.Utc), null, new DateTime(2025, 4, 14, 10, 0, 0, 0, DateTimeKind.Utc), "Returned", null, null, new DateTime(2025, 4, 14, 10, 0, 0, 0, DateTimeKind.Utc) });

            migrationBuilder.UpdateData(
                table: "LentItems",
                keyColumn: "Id",
                keyValue: new Guid("00000006-0000-0000-0000-000000000003"),
                columns: new[] { "BorrowerFullName", "BorrowerRole", "CreatedAt", "ItemId", "ItemName", "LentAt", "ReservedFor", "Room", "Status", "StudentIdNumber", "SubjectTimeSchedule", "TagUid", "TeacherFullName", "TeacherId", "UpdatedAt", "UserId" },
                values: new object[] { "Peter Jones", "Student", new DateTime(2025, 4, 15, 13, 0, 0, 0, DateTimeKind.Utc), new Guid("00000005-0000-0000-0000-000000000002"), "Wireless Microphone", new DateTime(2025, 4, 15, 13, 0, 0, 0, DateTimeKind.Utc), null, "Lab 103", "Borrowed", "2023-0003", "CS302 - 1:00 PM", null, "", null, new DateTime(2025, 4, 15, 13, 0, 0, 0, DateTimeKind.Utc), new Guid("00000004-0000-0000-0000-000000000003") });

            migrationBuilder.UpdateData(
                table: "LentItems",
                keyColumn: "Id",
                keyValue: new Guid("00000006-0000-0000-0000-000000000004"),
                columns: new[] { "BorrowerFullName", "BorrowerRole", "CreatedAt", "FrontStudentIdPictureUrl", "ItemName", "LentAt", "ReturnedAt", "Room", "Status", "StudentIdNumber", "StudentRfid", "SubjectTimeSchedule", "TagUid", "TeacherFullName", "TeacherId", "UpdatedAt", "UserId" },
                values: new object[] { "Alice Williams", "Teacher", new DateTime(2025, 4, 13, 15, 0, 0, 0, DateTimeKind.Utc), null, "Wireless Mouse", new DateTime(2025, 4, 13, 15, 0, 0, 0, DateTimeKind.Utc), null, "Room 201", "Borrowed", null, null, "IT401 - 3:00 PM", null, "Alice Williams", new Guid("00000003-0000-0000-0000-000000000001"), new DateTime(2025, 4, 13, 15, 0, 0, 0, DateTimeKind.Utc), new Guid("00000003-0000-0000-0000-000000000001") });

            migrationBuilder.UpdateData(
                table: "LentItems",
                keyColumn: "Id",
                keyValue: new Guid("00000006-0000-0000-0000-000000000005"),
                columns: new[] { "CreatedAt", "LentAt", "ReservedFor", "ReturnedAt", "Status", "TagUid", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 3, 17, 9, 0, 0, 0, DateTimeKind.Utc), new DateTime(2025, 3, 17, 9, 0, 0, 0, DateTimeKind.Utc), null, new DateTime(2025, 4, 1, 9, 0, 0, 0, DateTimeKind.Utc), "Returned", null, new DateTime(2025, 4, 1, 9, 0, 0, 0, DateTimeKind.Utc) });

            migrationBuilder.UpdateData(
                table: "Students",
                keyColumn: "Id",
                keyValue: new Guid("00000004-0000-0000-0000-000000000001"),
                columns: new[] { "BackStudentIdPictureUrl", "CityMunicipality", "Course", "FrontStudentIdPictureUrl", "PostalCode", "ProfilePictureUrl", "Province", "RfidUid", "Street" },
                values: new object[] { null, "", "Computer Science", null, "", null, "", null, "" });

            migrationBuilder.UpdateData(
                table: "Students",
                keyColumn: "Id",
                keyValue: new Guid("00000004-0000-0000-0000-000000000002"),
                columns: new[] { "BackStudentIdPictureUrl", "CityMunicipality", "Course", "FrontStudentIdPictureUrl", "PostalCode", "ProfilePictureUrl", "Province", "RfidUid", "Street" },
                values: new object[] { null, "", "Information Technology", null, "", null, "", null, "" });

            migrationBuilder.UpdateData(
                table: "Students",
                keyColumn: "Id",
                keyValue: new Guid("00000004-0000-0000-0000-000000000003"),
                columns: new[] { "BackStudentIdPictureUrl", "CityMunicipality", "Course", "FrontStudentIdPictureUrl", "PostalCode", "ProfilePictureUrl", "Province", "RfidUid", "Street" },
                values: new object[] { null, "", "Computer Science", null, "", null, "", null, "" });

            migrationBuilder.UpdateData(
                table: "Students",
                keyColumn: "Id",
                keyValue: new Guid("00000004-0000-0000-0000-000000000004"),
                columns: new[] { "BackStudentIdPictureUrl", "CityMunicipality", "Course", "FrontStudentIdPictureUrl", "PostalCode", "ProfilePictureUrl", "Province", "RfidUid", "Street" },
                values: new object[] { null, "", "Multimedia Arts", null, "", null, "", null, "" });

            migrationBuilder.UpdateData(
                table: "Students",
                keyColumn: "Id",
                keyValue: new Guid("00000004-0000-0000-0000-000000000005"),
                columns: new[] { "BackStudentIdPictureUrl", "CityMunicipality", "Course", "FrontStudentIdPictureUrl", "PostalCode", "ProfilePictureUrl", "Province", "Section", "Street", "StudentIdNumber", "Year" },
                values: new object[] { null, "", "Information Technology", null, "", null, "", "A", "", "2023-0005", "2nd Year" });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("00000001-0000-0000-0000-000000000001"),
                columns: new[] { "Email", "PasswordHash", "Status", "Username" },
                values: new object[] { "superadmin@gmail.com", "$2a$04$XXYFHeKNDbLGqUzLB1yWjuPghWlA2zdvpbVQwQEa20ChcEN9hxUsO", "Active", "superadmin" });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("00000001-0000-0000-0000-000000000002"),
                columns: new[] { "Email", "FirstName", "LastName", "PasswordHash", "Status", "Username" },
                values: new object[] { "maria.santos@gmail.com", "Maria", "Santos", "$2a$04$XXYFHeKNDbLGqUzLB1yWjuPghWlA2zdvpbVQwQEa20ChcEN9hxUsO", "Active", "msantos" });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("00000001-0000-0000-0000-000000000003"),
                columns: new[] { "Email", "FirstName", "LastName", "PasswordHash", "Status", "Username" },
                values: new object[] { "juan.delacruz@gmail.com", "Juan", "Dela Cruz", "$2a$04$XXYFHeKNDbLGqUzLB1yWjuPghWlA2zdvpbVQwQEa20ChcEN9hxUsO", "Active", "jdelacruz" });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("00000001-0000-0000-0000-000000000004"),
                columns: new[] { "Email", "FirstName", "PasswordHash", "Status", "Username" },
                values: new object[] { "ana.reyes@gmail.com", "Ana", "$2a$04$XXYFHeKNDbLGqUzLB1yWjuPghWlA2zdvpbVQwQEa20ChcEN9hxUsO", "Inactive", "areyes" });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("00000002-0000-0000-0000-000000000001"),
                columns: new[] { "Email", "PasswordHash", "Status" },
                values: new object[] { "carlos.mendoza@gmail.com", "$2a$04$XXYFHeKNDbLGqUzLB1yWjuPghWlA2zdvpbVQwQEa20ChcEN9hxUsO", "Active" });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("00000002-0000-0000-0000-000000000002"),
                columns: new[] { "Email", "PasswordHash", "Status" },
                values: new object[] { "rosa.garcia@gmail.com", "$2a$04$XXYFHeKNDbLGqUzLB1yWjuPghWlA2zdvpbVQwQEa20ChcEN9hxUsO", "Active" });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("00000002-0000-0000-0000-000000000003"),
                columns: new[] { "Email", "FirstName", "PasswordHash", "Status", "Username" },
                values: new object[] { "miguel.torres@gmail.com", "Miguel", "$2a$04$XXYFHeKNDbLGqUzLB1yWjuPghWlA2zdvpbVQwQEa20ChcEN9hxUsO", "Inactive", "mtorres" });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("00000003-0000-0000-0000-000000000001"),
                columns: new[] { "Email", "PasswordHash", "Status" },
                values: new object[] { "alice.williams@gmail.com", "$2a$04$XXYFHeKNDbLGqUzLB1yWjuPghWlA2zdvpbVQwQEa20ChcEN9hxUsO", "Active" });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("00000003-0000-0000-0000-000000000002"),
                columns: new[] { "Email", "PasswordHash", "Status" },
                values: new object[] { "roberto.cruz@gmail.com", "$2a$04$XXYFHeKNDbLGqUzLB1yWjuPghWlA2zdvpbVQwQEa20ChcEN9hxUsO", "Active" });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("00000003-0000-0000-0000-000000000003"),
                columns: new[] { "Email", "PasswordHash", "Status" },
                values: new object[] { "elena.fernandez@gmail.com", "$2a$04$XXYFHeKNDbLGqUzLB1yWjuPghWlA2zdvpbVQwQEa20ChcEN9hxUsO", "Active" });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("00000003-0000-0000-0000-000000000004"),
                columns: new[] { "Email", "FirstName", "LastName", "PasswordHash", "Status", "Username" },
                values: new object[] { "david.ramos@gmail.com", "David", "Ramos", "$2a$04$XXYFHeKNDbLGqUzLB1yWjuPghWlA2zdvpbVQwQEa20ChcEN9hxUsO", "Inactive", "dramos" });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("00000004-0000-0000-0000-000000000001"),
                columns: new[] { "Email", "FirstName", "LastName", "PasswordHash", "PhoneNumber", "Status", "Username" },
                values: new object[] { "john.doe@gmail.com", "John", "Doe", "$2a$04$XXYFHeKNDbLGqUzLB1yWjuPghWlA2zdvpbVQwQEa20ChcEN9hxUsO", null, "Active", "jdoe" });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("00000004-0000-0000-0000-000000000002"),
                columns: new[] { "Email", "FirstName", "LastName", "PasswordHash", "PhoneNumber", "Status", "Username" },
                values: new object[] { "jane.smith@gmail.com", "Jane", "Smith", "$2a$04$XXYFHeKNDbLGqUzLB1yWjuPghWlA2zdvpbVQwQEa20ChcEN9hxUsO", null, "Active", "jsmith" });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("00000004-0000-0000-0000-000000000003"),
                columns: new[] { "Email", "FirstName", "LastName", "PasswordHash", "PhoneNumber", "Status", "Username" },
                values: new object[] { "peter.jones@gmail.com", "Peter", "Jones", "$2a$04$XXYFHeKNDbLGqUzLB1yWjuPghWlA2zdvpbVQwQEa20ChcEN9hxUsO", null, "Active", "pjones" });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("00000004-0000-0000-0000-000000000004"),
                columns: new[] { "Email", "FirstName", "LastName", "PasswordHash", "PhoneNumber", "Status", "Username" },
                values: new object[] { "maria.lopez@gmail.com", "Maria", "Lopez", "$2a$04$XXYFHeKNDbLGqUzLB1yWjuPghWlA2zdvpbVQwQEa20ChcEN9hxUsO", null, "Active", "mlopez" });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("00000004-0000-0000-0000-000000000005"),
                columns: new[] { "Email", "FirstName", "LastName", "PasswordHash", "PhoneNumber", "Status", "Username" },
                values: new object[] { "carlos.rivera@gmail.com", "Carlos", "Rivera", "$2a$04$XXYFHeKNDbLGqUzLB1yWjuPghWlA2zdvpbVQwQEa20ChcEN9hxUsO", null, "Active", "crivera" });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "Email", "FirstName", "LastName", "MiddleName", "PasswordHash", "PhoneNumber", "Status", "UserRole", "Username" },
                values: new object[] { new Guid("00000004-0000-0000-0000-000000000006"), "sofia.gonzales@gmail.com", "Sofia", "Gonzales", null, "$2a$04$XXYFHeKNDbLGqUzLB1yWjuPghWlA2zdvpbVQwQEa20ChcEN9hxUsO", null, "Inactive", "Student", "sgonzales" });

            migrationBuilder.InsertData(
                table: "Students",
                columns: new[] { "Id", "BackStudentIdPictureUrl", "CityMunicipality", "Course", "FrontStudentIdPictureUrl", "GeneratedPassword", "PostalCode", "ProfilePictureUrl", "Province", "RfidCode", "RfidUid", "Section", "Street", "StudentIdNumber", "Year" },
                values: new object[] { new Guid("00000004-0000-0000-0000-000000000006"), null, "", "Computer Science", null, null, "", null, "", null, null, "B", "", "2024-0001", "1st Year" });
        }
    }
}
