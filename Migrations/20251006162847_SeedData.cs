using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace BackendTechnicalAssetsManagement.Migrations
{
    /// <inheritdoc />
    public partial class SeedData : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Items",
                columns: new[] { "Id", "Category", "Condition", "CreatedAt", "Description", "Image", "ItemMake", "ItemModel", "ItemName", "ItemType", "SerialNumber", "UpdatedAt" },
                values: new object[,]
                {
                    { new Guid("17ab667c-6223-4f3e-af98-def86d4df41b"), "Electronics", "New", new DateTime(2025, 10, 6, 16, 28, 46, 846, DateTimeKind.Utc).AddTicks(3910), "High-performance laptop for students.", null, "Dell", "XPS 15", "Laptop", "Electronic", "SN123456", new DateTime(2025, 10, 6, 16, 28, 46, 846, DateTimeKind.Utc).AddTicks(3911) },
                    { new Guid("44f49436-802a-4123-a7e3-56a6d6698f51"), "MediaEquipment", "Good", new DateTime(2025, 10, 6, 16, 28, 46, 846, DateTimeKind.Utc).AddTicks(3916), "Portable projector for classroom use.", null, "Epson", "PowerLite 1781W", "Projector", "Electronic", "SN654321", new DateTime(2025, 10, 6, 16, 28, 46, 846, DateTimeKind.Utc).AddTicks(3916) }
                });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "Email", "FirstName", "LastName", "MiddleName", "PasswordHash", "Status", "UserRole", "Username" },
                values: new object[,]
                {
                    { new Guid("1801f88e-918f-40f3-bf4e-4ac76484c45c"), "superadmin@example.com", "Super", "Admin", null, "AQAAAAIAAYagAAAAEIDZpIPp5QDoSU8KhzHfi05O70LaVthMRcBpDDQs/xR1IC4ZmBn59cMLHt/kqoDGBA==", "Active", "SuperAdmin", "superadmin" },
                    { new Guid("346f47a0-beb7-4582-9d4e-9132795ea136"), "student@example.com", "Peter", "Jones", null, "AQAAAAIAAYagAAAAEL8ABoVGUq3eaqfKm320Gcdg/9JVoMp5i8cCPL4O5y1rCPR9w27NLcdFdVvJPo59mQ==", "Active", "Student", "student" },
                    { new Guid("4594b00d-9a8a-497f-8193-132def803275"), "staff@example.com", "Jane", "Smith", null, "AQAAAAIAAYagAAAAELZHgzdFAxWmjzPRhWRweyULh9rQTT3vWlNSAMwPn0ZESCVBS0QarTJjKY140sRFAA==", "Active", "Staff", "staff" },
                    { new Guid("d569775c-6b95-40c4-be66-15cfdebbf9c9"), "teacher@example.com", "Mary", "Williams", null, "AQAAAAIAAYagAAAAEFDaeC+8/CUAwZoMoKKe0I/bH+M048clO4El8Noo0J3/hXWFCdXIS0cOhtTCBr6b5Q==", "Active", "Teacher", "teacher" },
                    { new Guid("deb6d5e4-9c80-4d8c-a0ab-038a6183c983"), "admin@gmail.com", "admin", "admin", null, "AQAAAAIAAYagAAAAEJzXqsq8R3h5N6bH3hKgaGG2wB0bGZcNcZC4lG5/4lWzuHKY1biVz+cNOw1rL48CqQ==", "Active", "Admin", "admin" }
                });

            migrationBuilder.InsertData(
                table: "Admins",
                columns: new[] { "Id", "PhoneNumber" },
                values: new object[,]
                {
                    { new Guid("1801f88e-918f-40f3-bf4e-4ac76484c45c"), "999-999-9999" },
                    { new Guid("deb6d5e4-9c80-4d8c-a0ab-038a6183c983"), "123-456-7890" }
                });

            migrationBuilder.InsertData(
                table: "Staff",
                columns: new[] { "Id", "PhoneNumber", "Position" },
                values: new object[] { new Guid("4594b00d-9a8a-497f-8193-132def803275"), "098-765-4321", "Lab Technician" });

            migrationBuilder.InsertData(
                table: "Students",
                columns: new[] { "Id", "BackStudentIdPicture", "CityMunicipality", "Course", "FrontStudentIdPicture", "PhoneNumber", "PostalCode", "ProfilePicture", "Province", "Section", "Street", "StudentIdNumber", "Year" },
                values: new object[] { new Guid("346f47a0-beb7-4582-9d4e-9132795ea136"), null, "Anytown", "Computer Science", null, "555-123-4567", "12345", null, "Anyprovince", "A", "123 Main St", "2023-0001", "3" });

            migrationBuilder.InsertData(
                table: "Teachers",
                columns: new[] { "Id", "Department", "PhoneNumber" },
                values: new object[] { new Guid("d569775c-6b95-40c4-be66-15cfdebbf9c9"), "Information Technology", "555-987-6543" });

            migrationBuilder.InsertData(
                table: "LentItems",
                columns: new[] { "Id", "BorrowerFullName", "BorrowerRole", "ItemId", "LentAt", "Remarks", "ReturnedAt", "Room", "StudentIdNumber", "SubjectTimeSchedule", "TeacherFullName", "TeacherId", "UserId" },
                values: new object[] { new Guid("4246f7b7-9fc4-42c2-a76c-0149d66cee13"), "Peter Jones", "Student", new Guid("17ab667c-6223-4f3e-af98-def86d4df41b"), new DateTime(2025, 10, 6, 16, 28, 46, 846, DateTimeKind.Utc).AddTicks(3997), "Borrowed", null, "Room 101", "2023-0001", "MWF 10:00-11:00 AM", "Mary Williams", new Guid("d569775c-6b95-40c4-be66-15cfdebbf9c9"), new Guid("346f47a0-beb7-4582-9d4e-9132795ea136") });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Admins",
                keyColumn: "Id",
                keyValue: new Guid("1801f88e-918f-40f3-bf4e-4ac76484c45c"));

            migrationBuilder.DeleteData(
                table: "Admins",
                keyColumn: "Id",
                keyValue: new Guid("deb6d5e4-9c80-4d8c-a0ab-038a6183c983"));

            migrationBuilder.DeleteData(
                table: "Items",
                keyColumn: "Id",
                keyValue: new Guid("17ab667c-6223-4f3e-af98-def86d4df41b"));

            migrationBuilder.DeleteData(
                table: "Items",
                keyColumn: "Id",
                keyValue: new Guid("44f49436-802a-4123-a7e3-56a6d6698f51"));

            migrationBuilder.DeleteData(
                table: "LentItems",
                keyColumn: "Id",
                keyValue: new Guid("4246f7b7-9fc4-42c2-a76c-0149d66cee13"));

            migrationBuilder.DeleteData(
                table: "Staff",
                keyColumn: "Id",
                keyValue: new Guid("4594b00d-9a8a-497f-8193-132def803275"));

            migrationBuilder.DeleteData(
                table: "Students",
                keyColumn: "Id",
                keyValue: new Guid("346f47a0-beb7-4582-9d4e-9132795ea136"));

            migrationBuilder.DeleteData(
                table: "Teachers",
                keyColumn: "Id",
                keyValue: new Guid("d569775c-6b95-40c4-be66-15cfdebbf9c9"));

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("1801f88e-918f-40f3-bf4e-4ac76484c45c"));

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("346f47a0-beb7-4582-9d4e-9132795ea136"));

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("4594b00d-9a8a-497f-8193-132def803275"));

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("deb6d5e4-9c80-4d8c-a0ab-038a6183c983"));

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("d569775c-6b95-40c4-be66-15cfdebbf9c9"));
        }
    }
}
