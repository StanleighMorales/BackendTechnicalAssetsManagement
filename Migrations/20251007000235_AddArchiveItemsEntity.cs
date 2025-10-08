using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace BackendTechnicalAssetsManagement.Migrations
{
    /// <inheritdoc />
    public partial class AddArchiveItemsEntity : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Admins",
                keyColumn: "Id",
                keyValue: new Guid("4ac0d1ef-5215-4bf7-91d2-70f774848f1e"));

            migrationBuilder.DeleteData(
                table: "Admins",
                keyColumn: "Id",
                keyValue: new Guid("57258329-3957-4836-ab7e-1e88cb653a28"));

            migrationBuilder.DeleteData(
                table: "Items",
                keyColumn: "Id",
                keyValue: new Guid("8cbe9212-f645-48ec-ad38-1f46a164bb8e"));

            migrationBuilder.DeleteData(
                table: "Items",
                keyColumn: "Id",
                keyValue: new Guid("b859a050-631c-4c35-b165-a85145662e54"));

            migrationBuilder.DeleteData(
                table: "LentItems",
                keyColumn: "Id",
                keyValue: new Guid("6407e5d5-b669-41ea-9c80-ae3a77a2801e"));

            migrationBuilder.DeleteData(
                table: "Staff",
                keyColumn: "Id",
                keyValue: new Guid("9017663a-bfc2-4992-9283-68f1f150f5bc"));

            migrationBuilder.DeleteData(
                table: "Students",
                keyColumn: "Id",
                keyValue: new Guid("d50e3a99-5a6f-4252-95bb-92615e460d23"));

            migrationBuilder.DeleteData(
                table: "Teachers",
                keyColumn: "Id",
                keyValue: new Guid("1636bb97-afcd-41a1-848b-c233dfa18b02"));

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("4ac0d1ef-5215-4bf7-91d2-70f774848f1e"));

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("57258329-3957-4836-ab7e-1e88cb653a28"));

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("9017663a-bfc2-4992-9283-68f1f150f5bc"));

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("d50e3a99-5a6f-4252-95bb-92615e460d23"));

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("1636bb97-afcd-41a1-848b-c233dfa18b02"));

            migrationBuilder.CreateTable(
                name: "ArchiveItems",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ItemId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    SerialNumber = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Image = table.Column<byte[]>(type: "varbinary(max)", nullable: true),
                    ItemName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ItemType = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ItemModel = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ItemMake = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Category = table.Column<int>(type: "int", nullable: false),
                    Condition = table.Column<int>(type: "int", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ArchiveItems", x => x.Id);
                });

            migrationBuilder.InsertData(
                table: "Items",
                columns: new[] { "Id", "Category", "Condition", "CreatedAt", "Description", "Image", "ItemMake", "ItemModel", "ItemName", "ItemType", "SerialNumber", "UpdatedAt" },
                values: new object[,]
                {
                    { new Guid("6524a65c-9195-4132-9a21-86b6151feb60"), "Electronics", "New", new DateTime(2025, 10, 7, 0, 2, 34, 228, DateTimeKind.Utc).AddTicks(8625), "High-performance laptop for students.", null, "Dell", "XPS 15", "Laptop", "Electronic", "SN123456", new DateTime(2025, 10, 7, 0, 2, 34, 228, DateTimeKind.Utc).AddTicks(8625) },
                    { new Guid("982cdd99-0219-4508-a342-6d55056ef784"), "MediaEquipment", "Good", new DateTime(2025, 10, 7, 0, 2, 34, 228, DateTimeKind.Utc).AddTicks(8634), "Portable projector for classroom use.", null, "Epson", "PowerLite 1781W", "Projector", "Electronic", "SN654321", new DateTime(2025, 10, 7, 0, 2, 34, 228, DateTimeKind.Utc).AddTicks(8635) }
                });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "Email", "FirstName", "LastName", "MiddleName", "PasswordHash", "Status", "UserRole", "Username" },
                values: new object[,]
                {
                    { new Guid("0a6dfab6-b88c-4038-a2e0-afecb682ee8d"), "student@example.com", "Peter", "Jones", null, "AQAAAAIAAYagAAAAEAqak473FFcxcRYRA/YZ8FHjXjMDvstvWropMcXMnJjED4hcYRpYmhO3ePUgqkkKZQ==", "", "Student", "student" },
                    { new Guid("55393592-65bb-4df2-bcc8-8e8225f8732e"), "admin@gmail.com", "admin", "admin", null, "AQAAAAIAAYagAAAAEAG9aonrSvffnOZuk9v03IZTya/FSV9dRQb+4Mv/IcgpNGD455gn257OsV81fhD9hg==", "", "Admin", "admin" },
                    { new Guid("64e0d350-8f60-4455-9f84-99136f526fa5"), "superadmin@example.com", "Super", "Admin", null, "AQAAAAIAAYagAAAAEMUVt1ZKxWf3yedhOzHilgy8jVbqk11hok/ewXg9v1/SWkx0yMxBMvD1w+Ctr3I9tA==", "", "SuperAdmin", "superadmin" },
                    { new Guid("6d91578b-2030-4c59-934e-4415f9d326ab"), "teacher@example.com", "Mary", "Williams", null, "AQAAAAIAAYagAAAAENJubTz/gJibKZsciCUNdXhN0cC66jDetcO69NcOO5TOztgkf40lyENPxOh20LAgPw==", "", "Teacher", "teacher" },
                    { new Guid("94fd2f53-95e8-4782-82ac-a364942a4c84"), "staff@example.com", "Jane", "Smith", null, "AQAAAAIAAYagAAAAEHLgdQ8CVwSCsQnhQsC7JTMec7cZuI0J14JfGeQ4bggb4zR78hiKNmXmhFQHk2P3kg==", "", "Staff", "staff" }
                });

            migrationBuilder.InsertData(
                table: "Admins",
                columns: new[] { "Id", "PhoneNumber" },
                values: new object[,]
                {
                    { new Guid("55393592-65bb-4df2-bcc8-8e8225f8732e"), "123-456-7890" },
                    { new Guid("64e0d350-8f60-4455-9f84-99136f526fa5"), "999-999-9999" }
                });

            migrationBuilder.InsertData(
                table: "Staff",
                columns: new[] { "Id", "PhoneNumber", "Position" },
                values: new object[] { new Guid("94fd2f53-95e8-4782-82ac-a364942a4c84"), "098-765-4321", "Lab Technician" });

            migrationBuilder.InsertData(
                table: "Students",
                columns: new[] { "Id", "BackStudentIdPicture", "CityMunicipality", "Course", "FrontStudentIdPicture", "PhoneNumber", "PostalCode", "ProfilePicture", "Province", "Section", "Street", "StudentIdNumber", "Year" },
                values: new object[] { new Guid("0a6dfab6-b88c-4038-a2e0-afecb682ee8d"), null, "Anytown", "Computer Science", null, "555-123-4567", "12345", null, "Anyprovince", "A", "123 Main St", "2023-0001", "3" });

            migrationBuilder.InsertData(
                table: "Teachers",
                columns: new[] { "Id", "Department", "PhoneNumber" },
                values: new object[] { new Guid("6d91578b-2030-4c59-934e-4415f9d326ab"), "Information Technology", "555-987-6543" });

            migrationBuilder.InsertData(
                table: "LentItems",
                columns: new[] { "Id", "BorrowerFullName", "BorrowerRole", "ItemId", "LentAt", "Remarks", "ReturnedAt", "Room", "StudentIdNumber", "SubjectTimeSchedule", "TeacherFullName", "TeacherId", "UserId" },
                values: new object[] { new Guid("98129664-1245-47ec-ba29-c479f2113665"), "Peter Jones", "Student", new Guid("6524a65c-9195-4132-9a21-86b6151feb60"), new DateTime(2025, 10, 7, 0, 2, 34, 228, DateTimeKind.Utc).AddTicks(8725), "Borrowed", null, "Room 101", "2023-0001", "MWF 10:00-11:00 AM", "Mary Williams", new Guid("6d91578b-2030-4c59-934e-4415f9d326ab"), new Guid("0a6dfab6-b88c-4038-a2e0-afecb682ee8d") });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ArchiveItems");

            migrationBuilder.DeleteData(
                table: "Admins",
                keyColumn: "Id",
                keyValue: new Guid("55393592-65bb-4df2-bcc8-8e8225f8732e"));

            migrationBuilder.DeleteData(
                table: "Admins",
                keyColumn: "Id",
                keyValue: new Guid("64e0d350-8f60-4455-9f84-99136f526fa5"));

            migrationBuilder.DeleteData(
                table: "Items",
                keyColumn: "Id",
                keyValue: new Guid("6524a65c-9195-4132-9a21-86b6151feb60"));

            migrationBuilder.DeleteData(
                table: "Items",
                keyColumn: "Id",
                keyValue: new Guid("982cdd99-0219-4508-a342-6d55056ef784"));

            migrationBuilder.DeleteData(
                table: "LentItems",
                keyColumn: "Id",
                keyValue: new Guid("98129664-1245-47ec-ba29-c479f2113665"));

            migrationBuilder.DeleteData(
                table: "Staff",
                keyColumn: "Id",
                keyValue: new Guid("94fd2f53-95e8-4782-82ac-a364942a4c84"));

            migrationBuilder.DeleteData(
                table: "Students",
                keyColumn: "Id",
                keyValue: new Guid("0a6dfab6-b88c-4038-a2e0-afecb682ee8d"));

            migrationBuilder.DeleteData(
                table: "Teachers",
                keyColumn: "Id",
                keyValue: new Guid("6d91578b-2030-4c59-934e-4415f9d326ab"));

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("0a6dfab6-b88c-4038-a2e0-afecb682ee8d"));

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("55393592-65bb-4df2-bcc8-8e8225f8732e"));

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("64e0d350-8f60-4455-9f84-99136f526fa5"));

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("94fd2f53-95e8-4782-82ac-a364942a4c84"));

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("6d91578b-2030-4c59-934e-4415f9d326ab"));

            migrationBuilder.InsertData(
                table: "Items",
                columns: new[] { "Id", "Category", "Condition", "CreatedAt", "Description", "Image", "ItemMake", "ItemModel", "ItemName", "ItemType", "SerialNumber", "UpdatedAt" },
                values: new object[,]
                {
                    { new Guid("8cbe9212-f645-48ec-ad38-1f46a164bb8e"), "MediaEquipment", "Good", new DateTime(2025, 10, 6, 16, 35, 19, 13, DateTimeKind.Utc).AddTicks(9225), "Portable projector for classroom use.", null, "Epson", "PowerLite 1781W", "Projector", "Electronic", "SN654321", new DateTime(2025, 10, 6, 16, 35, 19, 13, DateTimeKind.Utc).AddTicks(9226) },
                    { new Guid("b859a050-631c-4c35-b165-a85145662e54"), "Electronics", "New", new DateTime(2025, 10, 6, 16, 35, 19, 13, DateTimeKind.Utc).AddTicks(9220), "High-performance laptop for students.", null, "Dell", "XPS 15", "Laptop", "Electronic", "SN123456", new DateTime(2025, 10, 6, 16, 35, 19, 13, DateTimeKind.Utc).AddTicks(9220) }
                });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "Email", "FirstName", "LastName", "MiddleName", "PasswordHash", "Status", "UserRole", "Username" },
                values: new object[,]
                {
                    { new Guid("1636bb97-afcd-41a1-848b-c233dfa18b02"), "teacher@example.com", "Mary", "Williams", null, "AQAAAAIAAYagAAAAECfwNds9P07oWEVF+iKJUwUw0EN4rZ6mWSZ1yiSg6YTBmHUqzHtdZFawDb7GThTxXw==", "", "Teacher", "teacher" },
                    { new Guid("4ac0d1ef-5215-4bf7-91d2-70f774848f1e"), "superadmin@example.com", "Super", "Admin", null, "AQAAAAIAAYagAAAAEHZIJEyncO6f5OUnDawy1X9XtS6v4IJNpd5DAbWRL5IpNnoHdCf5BR6g/T5zlaMPrw==", "", "SuperAdmin", "superadmin" },
                    { new Guid("57258329-3957-4836-ab7e-1e88cb653a28"), "admin@gmail.com", "admin", "admin", null, "AQAAAAIAAYagAAAAEHszbpaEq+0czhhGgObqjByQG5jvgyY/xYJJqChT1blmG56CU2DoMRUEDxNAFBGY4g==", "", "Admin", "admin" },
                    { new Guid("9017663a-bfc2-4992-9283-68f1f150f5bc"), "staff@example.com", "Jane", "Smith", null, "AQAAAAIAAYagAAAAEBp07o4v5NIgT8uVTMUNy0oWicdW37UQezm6cCdiq3zBMP6yBIk2515ORyPkEmvWjw==", "", "Staff", "staff" },
                    { new Guid("d50e3a99-5a6f-4252-95bb-92615e460d23"), "student@example.com", "Peter", "Jones", null, "AQAAAAIAAYagAAAAEPtUXxu9JusVhxgRLISGTe3G6AxjGBfYYPkw9Yv9VCLymjQpFIpuc4lkx59iXRXZqQ==", "", "Student", "student" }
                });

            migrationBuilder.InsertData(
                table: "Admins",
                columns: new[] { "Id", "PhoneNumber" },
                values: new object[,]
                {
                    { new Guid("4ac0d1ef-5215-4bf7-91d2-70f774848f1e"), "999-999-9999" },
                    { new Guid("57258329-3957-4836-ab7e-1e88cb653a28"), "123-456-7890" }
                });

            migrationBuilder.InsertData(
                table: "Staff",
                columns: new[] { "Id", "PhoneNumber", "Position" },
                values: new object[] { new Guid("9017663a-bfc2-4992-9283-68f1f150f5bc"), "098-765-4321", "Lab Technician" });

            migrationBuilder.InsertData(
                table: "Students",
                columns: new[] { "Id", "BackStudentIdPicture", "CityMunicipality", "Course", "FrontStudentIdPicture", "PhoneNumber", "PostalCode", "ProfilePicture", "Province", "Section", "Street", "StudentIdNumber", "Year" },
                values: new object[] { new Guid("d50e3a99-5a6f-4252-95bb-92615e460d23"), null, "Anytown", "Computer Science", null, "555-123-4567", "12345", null, "Anyprovince", "A", "123 Main St", "2023-0001", "3" });

            migrationBuilder.InsertData(
                table: "Teachers",
                columns: new[] { "Id", "Department", "PhoneNumber" },
                values: new object[] { new Guid("1636bb97-afcd-41a1-848b-c233dfa18b02"), "Information Technology", "555-987-6543" });

            migrationBuilder.InsertData(
                table: "LentItems",
                columns: new[] { "Id", "BorrowerFullName", "BorrowerRole", "ItemId", "LentAt", "Remarks", "ReturnedAt", "Room", "StudentIdNumber", "SubjectTimeSchedule", "TeacherFullName", "TeacherId", "UserId" },
                values: new object[] { new Guid("6407e5d5-b669-41ea-9c80-ae3a77a2801e"), "Peter Jones", "Student", new Guid("b859a050-631c-4c35-b165-a85145662e54"), new DateTime(2025, 10, 6, 16, 35, 19, 13, DateTimeKind.Utc).AddTicks(9294), "Borrowed", null, "Room 101", "2023-0001", "MWF 10:00-11:00 AM", "Mary Williams", new Guid("1636bb97-afcd-41a1-848b-c233dfa18b02"), new Guid("d50e3a99-5a6f-4252-95bb-92615e460d23") });
        }
    }
}
