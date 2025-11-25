using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace BackendTechnicalAssetsManagement.Migrations
{
    /// <inheritdoc />
    public partial class SeedInitialData : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Items",
                columns: new[] { "Id", "Barcode", "BarcodeImage", "Category", "Condition", "CreatedAt", "Description", "Image", "ImageMimeType", "ItemMake", "ItemModel", "ItemName", "ItemType", "SerialNumber", "Status", "UpdatedAt" },
                values: new object[,]
                {
                    { new Guid("10000000-0000-0000-0000-000000000001"), "ITEM-SN-HDMI-001", null, "Electronics", "Good", new DateTime(2025, 11, 26, 4, 19, 1, 653, DateTimeKind.Local).AddTicks(5591), null, null, null, "", null, "HDMI Cable 10ft", "", "SN-HDMI-001", "Borrowed", new DateTime(2025, 11, 26, 4, 19, 1, 653, DateTimeKind.Local).AddTicks(5615) },
                    { new Guid("10000000-0000-0000-0000-000000000002"), "ITEM-SN-MIC-002", null, "MediaEquipment", "Good", new DateTime(2025, 11, 26, 4, 19, 1, 653, DateTimeKind.Local).AddTicks(5637), null, null, null, "", null, "Wireless Microphone", "", "SN-MIC-002", "Borrowed", new DateTime(2025, 11, 26, 4, 19, 1, 653, DateTimeKind.Local).AddTicks(5638) },
                    { new Guid("10000000-0000-0000-0000-000000000003"), "ITEM-SN-SPK-003", null, "MediaEquipment", "Good", new DateTime(2025, 11, 26, 4, 19, 1, 653, DateTimeKind.Local).AddTicks(5646), null, null, null, "", null, "Portable Bluetooth Speaker", "", "SN-SPK-003", "Available", new DateTime(2025, 11, 26, 4, 19, 1, 653, DateTimeKind.Local).AddTicks(5646) },
                    { new Guid("10000000-0000-0000-0000-000000000004"), "ITEM-SN-MOUSE-004", null, "Electronics", "Good", new DateTime(2025, 11, 26, 4, 19, 1, 653, DateTimeKind.Local).AddTicks(5650), null, null, null, "", null, "Wireless Mouse", "", "SN-MOUSE-004", "Borrowed", new DateTime(2025, 11, 26, 4, 19, 1, 653, DateTimeKind.Local).AddTicks(5651) },
                    { new Guid("10000000-0000-0000-0000-000000000005"), "ITEM-SN-KB-005", null, "Electronics", "Good", new DateTime(2025, 11, 26, 4, 19, 1, 653, DateTimeKind.Local).AddTicks(5655), null, null, null, "", null, "Mechanical Keyboard", "", "SN-KB-005", "Available", new DateTime(2025, 11, 26, 4, 19, 1, 653, DateTimeKind.Local).AddTicks(5656) },
                    { new Guid("10000000-0000-0000-0000-000000000006"), "ITEM-SN-EXT-006", null, "Electronics", "Good", new DateTime(2025, 11, 26, 4, 19, 1, 653, DateTimeKind.Local).AddTicks(5659), null, null, null, "", null, "Extension Wire 15ft", "", "SN-EXT-006", "Available", new DateTime(2025, 11, 26, 4, 19, 1, 653, DateTimeKind.Local).AddTicks(5659) },
                    { new Guid("10000000-0000-0000-0000-000000000007"), "ITEM-SN-HDMI-007", null, "Electronics", "Good", new DateTime(2025, 11, 26, 4, 19, 1, 653, DateTimeKind.Local).AddTicks(5722), null, null, null, "", null, "HDMI Cable 6ft", "", "SN-HDMI-007", "Available", new DateTime(2025, 11, 26, 4, 19, 1, 653, DateTimeKind.Local).AddTicks(5723) },
                    { new Guid("10000000-0000-0000-0000-000000000008"), "ITEM-SN-MIC-008", null, "MediaEquipment", "Good", new DateTime(2025, 11, 26, 4, 19, 1, 653, DateTimeKind.Local).AddTicks(5727), null, null, null, "", null, "USB Microphone", "", "SN-MIC-008", "Available", new DateTime(2025, 11, 26, 4, 19, 1, 653, DateTimeKind.Local).AddTicks(5728) }
                });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "Email", "FirstName", "LastName", "MiddleName", "PasswordHash", "PhoneNumber", "Status", "UserRole", "Username" },
                values: new object[,]
                {
                    { new Guid("00000000-0000-0000-0000-000000000001"), "superadmin@example.com", "Super", "Admin", null, "$2a$11$1k4ReyHunPIaXxHdXbFbreZojQQGJcsCVG1SREuD5UhND5E5KDie2", null, "", "SuperAdmin", "superadmin" },
                    { new Guid("00000000-0000-0000-0000-000000000002"), "maria.santos@example.com", "Maria", "Santos", null, "$2a$11$1k4ReyHunPIaXxHdXbFbreZojQQGJcsCVG1SREuD5UhND5E5KDie2", null, "", "Admin", "msantos" },
                    { new Guid("00000000-0000-0000-0000-000000000003"), "juan.delacruz@example.com", "Juan", "Dela Cruz", null, "$2a$11$1k4ReyHunPIaXxHdXbFbreZojQQGJcsCVG1SREuD5UhND5E5KDie2", null, "", "Admin", "jdelacruz" },
                    { new Guid("00000000-0000-0000-0000-000000000004"), "ana.reyes@example.com", "Ana", "Reyes", null, "$2a$11$1k4ReyHunPIaXxHdXbFbreZojQQGJcsCVG1SREuD5UhND5E5KDie2", null, "", "Admin", "areyes" },
                    { new Guid("00000000-0000-0000-0000-000000000005"), "carlos.mendoza@example.com", "Carlos", "Mendoza", null, "$2a$11$1k4ReyHunPIaXxHdXbFbreZojQQGJcsCVG1SREuD5UhND5E5KDie2", null, "", "Staff", "cmendoza" },
                    { new Guid("00000000-0000-0000-0000-000000000006"), "rosa.garcia@example.com", "Rosa", "Garcia", null, "$2a$11$1k4ReyHunPIaXxHdXbFbreZojQQGJcsCVG1SREuD5UhND5E5KDie2", null, "", "Staff", "rgarcia" },
                    { new Guid("00000000-0000-0000-0000-000000000007"), "miguel.torres@example.com", "Miguel", "Torres", null, "$2a$11$1k4ReyHunPIaXxHdXbFbreZojQQGJcsCVG1SREuD5UhND5E5KDie2", null, "", "Staff", "mtorres" },
                    { new Guid("00000000-0000-0000-0000-000000000008"), "alice.williams@example.com", "Alice", "Williams", null, "$2a$11$1k4ReyHunPIaXxHdXbFbreZojQQGJcsCVG1SREuD5UhND5E5KDie2", null, "", "Teacher", "awilliams" },
                    { new Guid("00000000-0000-0000-0000-000000000009"), "roberto.cruz@example.com", "Roberto", "Cruz", null, "$2a$11$1k4ReyHunPIaXxHdXbFbreZojQQGJcsCVG1SREuD5UhND5E5KDie2", null, "", "Teacher", "rcruz" },
                    { new Guid("00000000-0000-0000-0000-00000000000a"), "elena.fernandez@example.com", "Elena", "Fernandez", null, "$2a$11$1k4ReyHunPIaXxHdXbFbreZojQQGJcsCVG1SREuD5UhND5E5KDie2", null, "", "Teacher", "efernandez" },
                    { new Guid("00000000-0000-0000-0000-00000000000b"), "david.ramos@example.com", "David", "Ramos", null, "$2a$11$1k4ReyHunPIaXxHdXbFbreZojQQGJcsCVG1SREuD5UhND5E5KDie2", null, "", "Teacher", "dramos" },
                    { new Guid("00000000-0000-0000-0000-00000000000c"), "john.doe@student.example.com", "John", "Doe", null, "$2a$11$1k4ReyHunPIaXxHdXbFbreZojQQGJcsCVG1SREuD5UhND5E5KDie2", null, "", "Student", "jdoe" },
                    { new Guid("00000000-0000-0000-0000-00000000000d"), "jane.smith@student.example.com", "Jane", "Smith", null, "$2a$11$1k4ReyHunPIaXxHdXbFbreZojQQGJcsCVG1SREuD5UhND5E5KDie2", null, "", "Student", "jsmith" },
                    { new Guid("00000000-0000-0000-0000-00000000000e"), "peter.jones@student.example.com", "Peter", "Jones", null, "$2a$11$1k4ReyHunPIaXxHdXbFbreZojQQGJcsCVG1SREuD5UhND5E5KDie2", null, "", "Student", "pjones" },
                    { new Guid("00000000-0000-0000-0000-00000000000f"), "maria.lopez@student.example.com", "Maria", "Lopez", null, "$2a$11$1k4ReyHunPIaXxHdXbFbreZojQQGJcsCVG1SREuD5UhND5E5KDie2", null, "", "Student", "mlopez" },
                    { new Guid("00000000-0000-0000-0000-000000000010"), "carlos.rivera@student.example.com", "Carlos", "Rivera", null, "$2a$11$1k4ReyHunPIaXxHdXbFbreZojQQGJcsCVG1SREuD5UhND5E5KDie2", null, "", "Student", "crivera" },
                    { new Guid("00000000-0000-0000-0000-000000000011"), "sofia.gonzales@student.example.com", "Sofia", "Gonzales", null, "$2a$11$1k4ReyHunPIaXxHdXbFbreZojQQGJcsCVG1SREuD5UhND5E5KDie2", null, "", "Student", "sgonzales" }
                });

            migrationBuilder.InsertData(
                table: "LentItems",
                columns: new[] { "Id", "Barcode", "BarcodeImage", "BorrowerFullName", "BorrowerRole", "CreatedAt", "FrontStudentIdPicture", "IsHiddenFromUser", "ItemId", "ItemName", "LentAt", "Remarks", "ReservedFor", "ReturnedAt", "Room", "Status", "StudentIdNumber", "SubjectTimeSchedule", "TeacherFullName", "TeacherId", "UpdatedAt", "UserId" },
                values: new object[,]
                {
                    { new Guid("20000000-0000-0000-0000-000000000001"), "LENT-20251126-001", null, "", "", new DateTime(2025, 11, 25, 20, 19, 1, 653, DateTimeKind.Utc).AddTicks(5811), null, false, new Guid("10000000-0000-0000-0000-000000000001"), "", new DateTime(2025, 11, 21, 4, 19, 1, 653, DateTimeKind.Local).AddTicks(5978), null, null, null, "", "Borrowed", null, "", "", null, new DateTime(2025, 11, 25, 20, 19, 1, 653, DateTimeKind.Utc).AddTicks(5812), new Guid("00000000-0000-0000-0000-00000000000c") },
                    { new Guid("20000000-0000-0000-0000-000000000002"), "LENT-20251116-002", null, "", "", new DateTime(2025, 11, 25, 20, 19, 1, 653, DateTimeKind.Utc).AddTicks(5990), null, false, new Guid("10000000-0000-0000-0000-000000000003"), "", new DateTime(2025, 11, 16, 4, 19, 1, 653, DateTimeKind.Local).AddTicks(6004), null, null, new DateTime(2025, 11, 24, 4, 19, 1, 653, DateTimeKind.Local).AddTicks(6005), "", "Returned", null, "", "", null, new DateTime(2025, 11, 25, 20, 19, 1, 653, DateTimeKind.Utc).AddTicks(5991), new Guid("00000000-0000-0000-0000-00000000000d") },
                    { new Guid("20000000-0000-0000-0000-000000000003"), "LENT-20251126-003", null, "", "", new DateTime(2025, 11, 25, 20, 19, 1, 653, DateTimeKind.Utc).AddTicks(6010), null, false, new Guid("10000000-0000-0000-0000-000000000002"), "", new DateTime(2025, 11, 25, 4, 19, 1, 653, DateTimeKind.Local).AddTicks(6027), null, null, null, "", "Borrowed", null, "", "", null, new DateTime(2025, 11, 25, 20, 19, 1, 653, DateTimeKind.Utc).AddTicks(6011), new Guid("00000000-0000-0000-0000-00000000000e") }
                });

            migrationBuilder.InsertData(
                table: "Staff",
                columns: new[] { "Id", "Position" },
                values: new object[,]
                {
                    { new Guid("00000000-0000-0000-0000-000000000005"), "Lab Technician" },
                    { new Guid("00000000-0000-0000-0000-000000000006"), "Equipment Manager" },
                    { new Guid("00000000-0000-0000-0000-000000000007"), "IT Support" }
                });

            migrationBuilder.InsertData(
                table: "Students",
                columns: new[] { "Id", "BackStudentIdPicture", "CityMunicipality", "Course", "FrontStudentIdPicture", "GeneratedPassword", "PostalCode", "ProfilePicture", "Province", "Section", "Street", "StudentIdNumber", "Year" },
                values: new object[,]
                {
                    { new Guid("00000000-0000-0000-0000-00000000000c"), null, "", "Computer Science", null, null, "", null, "", "A", "", "2023-0001", "3rd Year" },
                    { new Guid("00000000-0000-0000-0000-00000000000d"), null, "", "Information Technology", null, null, "", null, "", "B", "", "2023-0002", "2nd Year" },
                    { new Guid("00000000-0000-0000-0000-00000000000e"), null, "", "Computer Science", null, null, "", null, "", "A", "", "2023-0003", "3rd Year" },
                    { new Guid("00000000-0000-0000-0000-00000000000f"), null, "", "Multimedia Arts", null, null, "", null, "", "C", "", "2023-0004", "1st Year" },
                    { new Guid("00000000-0000-0000-0000-000000000010"), null, "", "Information Technology", null, null, "", null, "", "A", "", "2023-0005", "2nd Year" },
                    { new Guid("00000000-0000-0000-0000-000000000011"), null, "", "Computer Science", null, null, "", null, "", "B", "", "2024-0001", "1st Year" }
                });

            migrationBuilder.InsertData(
                table: "Teachers",
                columns: new[] { "Id", "Department" },
                values: new object[,]
                {
                    { new Guid("00000000-0000-0000-0000-000000000008"), "Information Technology" },
                    { new Guid("00000000-0000-0000-0000-000000000009"), "Computer Science" },
                    { new Guid("00000000-0000-0000-0000-00000000000a"), "Information Technology" },
                    { new Guid("00000000-0000-0000-0000-00000000000b"), "Multimedia Arts" }
                });

            migrationBuilder.InsertData(
                table: "LentItems",
                columns: new[] { "Id", "Barcode", "BarcodeImage", "BorrowerFullName", "BorrowerRole", "CreatedAt", "FrontStudentIdPicture", "IsHiddenFromUser", "ItemId", "ItemName", "LentAt", "Remarks", "ReservedFor", "ReturnedAt", "Room", "Status", "StudentIdNumber", "SubjectTimeSchedule", "TeacherFullName", "TeacherId", "UpdatedAt", "UserId" },
                values: new object[,]
                {
                    { new Guid("20000000-0000-0000-0000-000000000004"), "LENT-20251126-004", null, "", "", new DateTime(2025, 11, 25, 20, 19, 1, 653, DateTimeKind.Utc).AddTicks(6040), null, false, new Guid("10000000-0000-0000-0000-000000000004"), "", new DateTime(2025, 11, 23, 4, 19, 1, 653, DateTimeKind.Local).AddTicks(6049), null, null, null, "", "Borrowed", null, "", "", new Guid("00000000-0000-0000-0000-000000000008"), new DateTime(2025, 11, 25, 20, 19, 1, 653, DateTimeKind.Utc).AddTicks(6041), new Guid("00000000-0000-0000-0000-000000000008") },
                    { new Guid("20000000-0000-0000-0000-000000000005"), "LENT-20251027-005", null, "", "", new DateTime(2025, 11, 25, 20, 19, 1, 653, DateTimeKind.Utc).AddTicks(6059), null, false, new Guid("10000000-0000-0000-0000-000000000005"), "", new DateTime(2025, 10, 27, 4, 19, 1, 653, DateTimeKind.Local).AddTicks(6198), null, null, new DateTime(2025, 11, 11, 4, 19, 1, 653, DateTimeKind.Local).AddTicks(6199), "", "Returned", null, "", "", new Guid("00000000-0000-0000-0000-000000000009"), new DateTime(2025, 11, 25, 20, 19, 1, 653, DateTimeKind.Utc).AddTicks(6060), new Guid("00000000-0000-0000-0000-000000000009") }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Items",
                keyColumn: "Id",
                keyValue: new Guid("10000000-0000-0000-0000-000000000006"));

            migrationBuilder.DeleteData(
                table: "Items",
                keyColumn: "Id",
                keyValue: new Guid("10000000-0000-0000-0000-000000000007"));

            migrationBuilder.DeleteData(
                table: "Items",
                keyColumn: "Id",
                keyValue: new Guid("10000000-0000-0000-0000-000000000008"));

            migrationBuilder.DeleteData(
                table: "LentItems",
                keyColumn: "Id",
                keyValue: new Guid("20000000-0000-0000-0000-000000000001"));

            migrationBuilder.DeleteData(
                table: "LentItems",
                keyColumn: "Id",
                keyValue: new Guid("20000000-0000-0000-0000-000000000002"));

            migrationBuilder.DeleteData(
                table: "LentItems",
                keyColumn: "Id",
                keyValue: new Guid("20000000-0000-0000-0000-000000000003"));

            migrationBuilder.DeleteData(
                table: "LentItems",
                keyColumn: "Id",
                keyValue: new Guid("20000000-0000-0000-0000-000000000004"));

            migrationBuilder.DeleteData(
                table: "LentItems",
                keyColumn: "Id",
                keyValue: new Guid("20000000-0000-0000-0000-000000000005"));

            migrationBuilder.DeleteData(
                table: "Staff",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000005"));

            migrationBuilder.DeleteData(
                table: "Staff",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000006"));

            migrationBuilder.DeleteData(
                table: "Staff",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000007"));

            migrationBuilder.DeleteData(
                table: "Students",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-00000000000c"));

            migrationBuilder.DeleteData(
                table: "Students",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-00000000000d"));

            migrationBuilder.DeleteData(
                table: "Students",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-00000000000e"));

            migrationBuilder.DeleteData(
                table: "Students",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-00000000000f"));

            migrationBuilder.DeleteData(
                table: "Students",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000010"));

            migrationBuilder.DeleteData(
                table: "Students",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000011"));

            migrationBuilder.DeleteData(
                table: "Teachers",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-00000000000a"));

            migrationBuilder.DeleteData(
                table: "Teachers",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-00000000000b"));

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000001"));

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000002"));

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000003"));

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000004"));

            migrationBuilder.DeleteData(
                table: "Items",
                keyColumn: "Id",
                keyValue: new Guid("10000000-0000-0000-0000-000000000001"));

            migrationBuilder.DeleteData(
                table: "Items",
                keyColumn: "Id",
                keyValue: new Guid("10000000-0000-0000-0000-000000000002"));

            migrationBuilder.DeleteData(
                table: "Items",
                keyColumn: "Id",
                keyValue: new Guid("10000000-0000-0000-0000-000000000003"));

            migrationBuilder.DeleteData(
                table: "Items",
                keyColumn: "Id",
                keyValue: new Guid("10000000-0000-0000-0000-000000000004"));

            migrationBuilder.DeleteData(
                table: "Items",
                keyColumn: "Id",
                keyValue: new Guid("10000000-0000-0000-0000-000000000005"));

            migrationBuilder.DeleteData(
                table: "Teachers",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000008"));

            migrationBuilder.DeleteData(
                table: "Teachers",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000009"));

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000005"));

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000006"));

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000007"));

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-00000000000a"));

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-00000000000b"));

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-00000000000c"));

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-00000000000d"));

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-00000000000e"));

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-00000000000f"));

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000010"));

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000011"));

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000008"));

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000009"));
        }
    }
}
