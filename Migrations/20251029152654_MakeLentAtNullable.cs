using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace BackendTechnicalAssetsManagement.Migrations
{
    /// <inheritdoc />
    public partial class MakeLentAtNullable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "LentItems",
                keyColumn: "Id",
                keyValue: new Guid("4fd60aec-0b5d-42d9-a65f-972122aff81a"));

            migrationBuilder.DeleteData(
                table: "LentItems",
                keyColumn: "Id",
                keyValue: new Guid("b7d644f0-727f-4311-9973-d16f06bd9ba1"));

            migrationBuilder.DeleteData(
                table: "LentItems",
                keyColumn: "Id",
                keyValue: new Guid("c56f51db-ef35-4cc8-9b52-59bda7896aa1"));

            migrationBuilder.DeleteData(
                table: "LentItems",
                keyColumn: "Id",
                keyValue: new Guid("d7e847fe-e4b2-407d-9f34-051a34f9e915"));

            migrationBuilder.DeleteData(
                table: "LentItems",
                keyColumn: "Id",
                keyValue: new Guid("eb2337a8-e9d5-4474-87dd-01cac3a4e330"));

            migrationBuilder.DeleteData(
                table: "Staff",
                keyColumn: "Id",
                keyValue: new Guid("1fa15a68-cb7a-4797-9a59-57b68edb4a6e"));

            migrationBuilder.DeleteData(
                table: "Staff",
                keyColumn: "Id",
                keyValue: new Guid("7ce158ee-72a7-44d0-b0fc-40234fac7b75"));

            migrationBuilder.DeleteData(
                table: "Staff",
                keyColumn: "Id",
                keyValue: new Guid("a918b1ee-f128-4233-929f-df631ef843b4"));

            migrationBuilder.DeleteData(
                table: "Staff",
                keyColumn: "Id",
                keyValue: new Guid("f3678581-4513-46fe-8e2d-e9c4059ddcc2"));

            migrationBuilder.DeleteData(
                table: "Staff",
                keyColumn: "Id",
                keyValue: new Guid("ff8f0411-1b61-4223-bfba-1d23c24e571a"));

            migrationBuilder.DeleteData(
                table: "Students",
                keyColumn: "Id",
                keyValue: new Guid("333a16d4-02f9-457b-bd42-4fbb972fd5fb"));

            migrationBuilder.DeleteData(
                table: "Students",
                keyColumn: "Id",
                keyValue: new Guid("34acfc5d-c3dd-4e57-943b-b2b00be3662c"));

            migrationBuilder.DeleteData(
                table: "Students",
                keyColumn: "Id",
                keyValue: new Guid("a02f3fa3-3add-441e-959a-b2cad649d164"));

            migrationBuilder.DeleteData(
                table: "Students",
                keyColumn: "Id",
                keyValue: new Guid("af6bf375-ecd6-49f5-bfc9-711d1cf18c12"));

            migrationBuilder.DeleteData(
                table: "Students",
                keyColumn: "Id",
                keyValue: new Guid("fe790f52-f317-45e9-a8b0-5858a7aa66dd"));

            migrationBuilder.DeleteData(
                table: "Teachers",
                keyColumn: "Id",
                keyValue: new Guid("08efd894-7889-48f1-9c29-d8541eb828f7"));

            migrationBuilder.DeleteData(
                table: "Teachers",
                keyColumn: "Id",
                keyValue: new Guid("244773dd-5ac9-4cc3-8b29-89e7185c82cc"));

            migrationBuilder.DeleteData(
                table: "Teachers",
                keyColumn: "Id",
                keyValue: new Guid("ffde25a6-29aa-4a75-aa35-a5ba3d5bd74a"));

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("2250b3a5-8c28-4c9d-a725-7c96b4fa3ec5"));

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("32f0c9e7-c19a-492d-b1d8-083e7ca1f43f"));

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("34cbe681-2c94-46e1-9ff4-3b3eb5cb033a"));

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("7a1afac4-0933-467f-a099-21f59c7f0adb"));

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("99f7870c-0603-4b93-ac7a-eed3315ca07a"));

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("de880717-2d07-49eb-a609-de246068c28c"));

            migrationBuilder.DeleteData(
                table: "Items",
                keyColumn: "Id",
                keyValue: new Guid("1a512806-4072-4749-8c60-fed2e7456cb8"));

            migrationBuilder.DeleteData(
                table: "Items",
                keyColumn: "Id",
                keyValue: new Guid("2e41a6e8-1b80-4756-a204-5ecde0ad6fb6"));

            migrationBuilder.DeleteData(
                table: "Items",
                keyColumn: "Id",
                keyValue: new Guid("6331053f-c190-476f-8e08-01d44b3650a4"));

            migrationBuilder.DeleteData(
                table: "Items",
                keyColumn: "Id",
                keyValue: new Guid("7f19c228-c324-47e1-aa6b-46aa6f68a98e"));

            migrationBuilder.DeleteData(
                table: "Items",
                keyColumn: "Id",
                keyValue: new Guid("9f078b90-1140-4932-8fb7-dfed46541540"));

            migrationBuilder.DeleteData(
                table: "Teachers",
                keyColumn: "Id",
                keyValue: new Guid("a5bce19e-4c57-45cb-aca7-db7625a0c112"));

            migrationBuilder.DeleteData(
                table: "Teachers",
                keyColumn: "Id",
                keyValue: new Guid("d82702b6-8326-466e-8083-ad55a9b1e03a"));

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("08efd894-7889-48f1-9c29-d8541eb828f7"));

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("1fa15a68-cb7a-4797-9a59-57b68edb4a6e"));

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("244773dd-5ac9-4cc3-8b29-89e7185c82cc"));

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("333a16d4-02f9-457b-bd42-4fbb972fd5fb"));

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("34acfc5d-c3dd-4e57-943b-b2b00be3662c"));

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("7ce158ee-72a7-44d0-b0fc-40234fac7b75"));

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("a02f3fa3-3add-441e-959a-b2cad649d164"));

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("a918b1ee-f128-4233-929f-df631ef843b4"));

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("af6bf375-ecd6-49f5-bfc9-711d1cf18c12"));

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("f3678581-4513-46fe-8e2d-e9c4059ddcc2"));

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("fe790f52-f317-45e9-a8b0-5858a7aa66dd"));

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("ff8f0411-1b61-4223-bfba-1d23c24e571a"));

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("ffde25a6-29aa-4a75-aa35-a5ba3d5bd74a"));

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("a5bce19e-4c57-45cb-aca7-db7625a0c112"));

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("d82702b6-8326-466e-8083-ad55a9b1e03a"));

            migrationBuilder.AlterColumn<DateTime>(
                name: "LentAt",
                table: "LentItems",
                type: "datetime2",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "datetime2");

            migrationBuilder.AlterColumn<DateTime>(
                name: "LentAt",
                table: "ArchiveLentItems",
                type: "datetime2",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "datetime2");

            migrationBuilder.InsertData(
                table: "Items",
                columns: new[] { "Id", "Barcode", "BarcodeImage", "Category", "Condition", "CreatedAt", "Description", "Image", "ImageMimeType", "ItemMake", "ItemModel", "ItemName", "ItemType", "SerialNumber", "Status", "UpdatedAt" },
                values: new object[,]
                {
                    { new Guid("2521ec9a-c75e-4782-8531-4c3daf46f87a"), null, null, "MediaEquipment", "New", new DateTime(2025, 10, 29, 15, 26, 53, 410, DateTimeKind.Utc).AddTicks(1525), "1080p projector for classroom presentations.", null, null, "Epson", "HC 2250", "Epson Home Cinema Projector", "Projector", "SN-PR-EPS-002", "Available", new DateTime(2025, 10, 29, 15, 26, 53, 410, DateTimeKind.Utc).AddTicks(1525) },
                    { new Guid("257e43ef-bf46-4f85-96d0-7fce1de4e456"), null, null, "Electronics", "Good", new DateTime(2025, 10, 29, 15, 26, 53, 410, DateTimeKind.Utc).AddTicks(1538), "Tablet for digital art and design classes.", null, null, "Apple", "iPad Pro 12.9-inch", "Apple iPad Pro", "Tablet", "SN-TB-APL-005", "Available", new DateTime(2025, 10, 29, 15, 26, 53, 410, DateTimeKind.Utc).AddTicks(1538) },
                    { new Guid("6be4b325-5947-4a6a-871e-8cdb03795e22"), null, null, "MediaEquipment", "Good", new DateTime(2025, 10, 29, 15, 26, 53, 410, DateTimeKind.Utc).AddTicks(1529), "Full-frame mirrorless camera with 4K video.", null, null, "Canon", "EOS R6", "Canon EOS R6 Camera", "Camera", "SN-CM-CAN-003", "Available", new DateTime(2025, 10, 29, 15, 26, 53, 410, DateTimeKind.Utc).AddTicks(1529) },
                    { new Guid("e5b41e0c-0288-437b-81ee-bfa3ef084f12"), null, null, "Electronics", "Good", new DateTime(2025, 10, 29, 15, 26, 53, 410, DateTimeKind.Utc).AddTicks(1510), "High-performance laptop for video editing.", null, null, "Dell", "XPS 15 9510", "Dell XPS 15 Laptop", "Laptop", "SN-LP-DELL-001", "Available", new DateTime(2025, 10, 29, 15, 26, 53, 410, DateTimeKind.Utc).AddTicks(1510) },
                    { new Guid("efa11cc5-ff67-438b-b6fb-a1904cce334d"), null, null, "MediaEquipment", "Good", new DateTime(2025, 10, 29, 15, 26, 53, 410, DateTimeKind.Utc).AddTicks(1535), "Dynamic microphone for vocal recording.", null, null, "Shure", "SM7B", "Shure SM7B Microphone", "Microphone", "SN-MC-SHR-004", "Available", new DateTime(2025, 10, 29, 15, 26, 53, 410, DateTimeKind.Utc).AddTicks(1535) }
                });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "Email", "FirstName", "LastName", "MiddleName", "PasswordHash", "PhoneNumber", "Status", "UserRole", "Username" },
                values: new object[,]
                {
                    { new Guid("2a7ca1c5-b8e4-4c70-8403-7162ecef9d66"), "john.doe@example.com", "John", "Doe", null, "$2a$11$JEJzdjuakGPYcsOUZp5JcOOyjcb.QC4dEdBurBgUSAaN17Rd2hVSW", "0912134567", "Offline", "Student", "johndoe" },
                    { new Guid("3656e8af-b4ef-4a8b-a5d1-80e1cff4352d"), "staff1@example.com", "Staff1", "Doe", null, "$2a$11$cqclT5Xsv8iBptvliMoecOWKYaCRqlYF8IXBkUITlNnubQ1Zqtnji", "0998176543", "Offline", "Staff", "staff1" },
                    { new Guid("3c9c8e15-41fb-4253-b7f0-b6e2475d8eec"), "staff5@example.com", "Staff5", "Doe", null, "$2a$11$fPvH.yf.l5nPUu2ksOMKPeOLJHF1ELNwCjsddVC3CLajUVmb86Cuq", "0998576543", "Offline", "Staff", "staff5" },
                    { new Guid("3fd04f17-d0a2-4804-bb40-9faced29af00"), "jane.smith@example.com", "Jane", "Smith", null, "$2a$11$12U7CDSnDuPljDPoBw4I/ug2D13rydkwcIjoiQxtGblYW8nCHMr2a", "0912234567", "Offline", "Student", "janesmith" },
                    { new Guid("452a856b-22e9-476c-a6e2-4adf4e676783"), "teacher3@example.com", "Teacher3", "Smith", null, "$2a$11$0UzE62REtkZdWaoQ1PETTOzM.5xAaRjUFlJBrOMUwm/NHwWNUswL.", "0917323456", "Offline", "Teacher", "teacher3" },
                    { new Guid("492fe915-d579-43b6-818e-093215cd1868"), "admin5@example.com", "Admin5", "User", null, "$2a$11$CK.YpT1zYJrWvUT0QtJJeOyOsBh5m1K.e1J.V0i8pR9U65T1gWd6q", null, "Offline", "Admin", "admin5" },
                    { new Guid("5adad555-c176-48d8-b53d-8112e568a9fb"), "staff4@example.com", "Staff4", "Doe", null, "$2a$11$PBXIwPzGBQgxO5Q4fFmG0eLlCQJRCJDlV4egEE6ejLHxBYAVdpwF2", "0998476543", "Offline", "Staff", "staff4" },
                    { new Guid("5d78a98e-59a0-4993-9ad2-d38781bc0ed6"), "staff3@example.com", "Staff3", "Doe", null, "$2a$11$WheLKCYoJPrUXnEYmOfgj.VgkoGh1wKB8MDsM0KoL3gj2FJiKSWAW", "0998376543", "Offline", "Staff", "staff3" },
                    { new Guid("66f6e1c4-42b7-4e8f-b979-05171ed7214e"), "student5@example.com", "Student5", "Jones", null, "$2a$11$WkVcu9ZtzyNAQcqRQCwC/OqmKx.sPtr3AZdph1vy9KCAX4jVgSSfS", "0912534567", "Offline", "Student", "student5" },
                    { new Guid("722e7dbc-ce0b-4ae0-88ab-902e0f7ce03a"), "admin4@example.com", "Admin4", "User", null, "$2a$11$nfy3o3zg3iH98HHbpBYqXODazDAAb.Kr01wf5s/aCRUp/gp6JlLFW", null, "Offline", "Admin", "admin4" },
                    { new Guid("72f88225-2575-48d6-a69e-895e5cb65ad3"), "admin2@example.com", "Admin2", "User", null, "$2a$11$zbmv0z9.wsLnpo1b4KwnOO0qSIjaDnwsmmev90qkgwrgyJ6DlD2oq", null, "Offline", "Admin", "admin2" },
                    { new Guid("7a232ead-e7aa-4dca-bdcc-39d31bae6174"), "teacher4@example.com", "Teacher4", "Smith", null, "$2a$11$PCJjSqQZryQ77gIjGo5uw.2Ipuh8aqHU1EgVkB0LIVKTJgN5bOepK", "0917423456", "Offline", "Teacher", "teacher4" },
                    { new Guid("7e0bb7c6-21ca-4215-a801-8b7423d14632"), "admin3@example.com", "Admin3", "User", null, "$2a$11$yUuQsa66u0BI3LmJSKo7feX7.DlgIL5NibMzFW1jKfrM4rVPv3V.K", null, "Offline", "Admin", "admin3" },
                    { new Guid("81b4d6e5-4369-41ad-9ea1-d6ae122df801"), "staff2@example.com", "Staff2", "Doe", null, "$2a$11$qb2LeQmO/73iOZbnQ68XRO.oBR65SWT5sRzPXkiUONCDhnhPz3YD6", "0998276543", "Offline", "Staff", "staff2" },
                    { new Guid("983fbbd0-f51e-40de-89a0-f71f7dff8b29"), "peter.jones@example.com", "Peter", "Jones", null, "$2a$11$PEtfmTNOGfm8yFirU0Xd9.JS5TqBL064hp8MRdcx/cOHhvWe.Ar1K", "0912334567", "Offline", "Student", "peterjones" },
                    { new Guid("a5fe58cb-534c-4583-81a7-19ab7bc0bc97"), "teacher5@example.com", "Teacher5", "Smith", null, "$2a$11$jqMaHnrzDRN5sJ4W4W1oleZ1h1soBYAszO00sko9pkBlL7xZYZrNK", "0917523456", "Offline", "Teacher", "teacher5" },
                    { new Guid("b6283ce5-be1a-45eb-8f1d-3717309efd93"), "bob.brown@example.com", "Bob", "Brown", null, "$2a$11$F7sxBPeJ2eSgtiFygXfqo.KIiPw4R53.K1tPBOgX6dS3Z9/hxvYW2", "0917223456", "Offline", "Teacher", "bobbrown" },
                    { new Guid("d704ad35-782c-4e33-961b-d5a19234491c"), "alice.williams@example.com", "Alice", "Williams", null, "$2a$11$uGO/nKesp.oB9lG7LwcNTODoj.R86rnQ9JW0FWTw87fvgYq/8aui2", "0917123456", "Offline", "Teacher", "alicewilliams" },
                    { new Guid("d94efbe6-6725-4d35-8b87-a8b15eb5e55b"), "student4@example.com", "Student4", "Jones", null, "$2a$11$B9KKWQxfgoCfhC9r.vc6H.lgW/PpuwkCfQqcPCq/Cj6S1jBpy9zka", "0912434567", "Offline", "Student", "student4" },
                    { new Guid("ddc38afd-3ab7-4c14-a797-1c23b2dd8c78"), "admin1@example.com", "Admin1", "User", null, "$2a$11$Dvkc1OBXbHy8inbHLKMekugp26R89bOH/82kuOtHiHar/nQijVQNy", null, "Offline", "Admin", "admin1" },
                    { new Guid("f0b0ba71-b261-4429-b0db-065828de48ef"), "superadmin@example.com", "Super", "Admin", null, "$2a$11$JlOD.Lmu9Gqi36.1hmnNkO4cqhQGGdAozrSjb0i0N7RWr6yXhqzRW", null, "Offline", "SuperAdmin", "superadmin" }
                });

            migrationBuilder.InsertData(
                table: "LentItems",
                columns: new[] { "Id", "Barcode", "BarcodeImage", "BorrowerFullName", "BorrowerRole", "IsHiddenFromUser", "ItemId", "ItemName", "LentAt", "Remarks", "ReturnedAt", "Room", "Status", "StudentIdNumber", "SubjectTimeSchedule", "TeacherFullName", "TeacherId", "UserId" },
                values: new object[,]
                {
                    { new Guid("2e7332a7-931e-4847-b9e6-2965518c4b5e"), null, null, "Bob Brown", "Teacher", false, new Guid("257e43ef-bf46-4f85-96d0-7fce1de4e456"), "Apple iPad Pro", new DateTime(2025, 9, 29, 15, 26, 53, 410, DateTimeKind.Utc).AddTicks(2117), null, new DateTime(2025, 10, 14, 15, 26, 53, 410, DateTimeKind.Utc).AddTicks(2118), "", "Returned", null, "", "", null, new Guid("b6283ce5-be1a-45eb-8f1d-3717309efd93") },
                    { new Guid("7f10908d-0cb0-4b2e-a4b4-3ae8bd4af134"), null, null, "Alice Williams", "Teacher", false, new Guid("efa11cc5-ff67-438b-b6fb-a1904cce334d"), "Shure SM7B Microphone", new DateTime(2025, 10, 26, 15, 26, 53, 410, DateTimeKind.Utc).AddTicks(2114), null, null, "", "Borrowed", null, "", "", null, new Guid("d704ad35-782c-4e33-961b-d5a19234491c") }
                });

            migrationBuilder.InsertData(
                table: "Staff",
                columns: new[] { "Id", "Position" },
                values: new object[,]
                {
                    { new Guid("3656e8af-b4ef-4a8b-a5d1-80e1cff4352d"), "Lab Technician" },
                    { new Guid("3c9c8e15-41fb-4253-b7f0-b6e2475d8eec"), "Lab Technician" },
                    { new Guid("5adad555-c176-48d8-b53d-8112e568a9fb"), "Lab Technician" },
                    { new Guid("5d78a98e-59a0-4993-9ad2-d38781bc0ed6"), "Lab Technician" },
                    { new Guid("81b4d6e5-4369-41ad-9ea1-d6ae122df801"), "Lab Technician" }
                });

            migrationBuilder.InsertData(
                table: "Students",
                columns: new[] { "Id", "BackStudentIdPicture", "CityMunicipality", "Course", "FrontStudentIdPicture", "PostalCode", "ProfilePicture", "Province", "Section", "Street", "StudentIdNumber", "Year" },
                values: new object[,]
                {
                    { new Guid("2a7ca1c5-b8e4-4c70-8403-7162ecef9d66"), null, "", "Computer Science", null, "", null, "", "A", "", "S2021-001", "3" },
                    { new Guid("3fd04f17-d0a2-4804-bb40-9faced29af00"), null, "", "Computer Science", null, "", null, "", "A", "", "S2021-002", "3" },
                    { new Guid("66f6e1c4-42b7-4e8f-b979-05171ed7214e"), null, "", "Computer Science", null, "", null, "", "A", "", "2023-0005", "3" },
                    { new Guid("983fbbd0-f51e-40de-89a0-f71f7dff8b29"), null, "", "Computer Science", null, "", null, "", "A", "", "S2021-003", "3" },
                    { new Guid("d94efbe6-6725-4d35-8b87-a8b15eb5e55b"), null, "", "Computer Science", null, "", null, "", "A", "", "2023-0004", "3" }
                });

            migrationBuilder.InsertData(
                table: "Teachers",
                columns: new[] { "Id", "Department" },
                values: new object[,]
                {
                    { new Guid("452a856b-22e9-476c-a6e2-4adf4e676783"), "Information Technology" },
                    { new Guid("7a232ead-e7aa-4dca-bdcc-39d31bae6174"), "Information Technology" },
                    { new Guid("a5fe58cb-534c-4583-81a7-19ab7bc0bc97"), "Information Technology" },
                    { new Guid("b6283ce5-be1a-45eb-8f1d-3717309efd93"), "Information Technology" },
                    { new Guid("d704ad35-782c-4e33-961b-d5a19234491c"), "Information Technology" }
                });

            migrationBuilder.InsertData(
                table: "LentItems",
                columns: new[] { "Id", "Barcode", "BarcodeImage", "BorrowerFullName", "BorrowerRole", "IsHiddenFromUser", "ItemId", "ItemName", "LentAt", "Remarks", "ReturnedAt", "Room", "Status", "StudentIdNumber", "SubjectTimeSchedule", "TeacherFullName", "TeacherId", "UserId" },
                values: new object[,]
                {
                    { new Guid("aa3c4916-14ac-4636-a8a6-8eb88be3b4d3"), null, null, "John Doe", "Student", false, new Guid("e5b41e0c-0288-437b-81ee-bfa3ef084f12"), "Dell XPS 15 Laptop", new DateTime(2025, 10, 24, 15, 26, 53, 410, DateTimeKind.Utc).AddTicks(2081), null, null, "", "Borrowed", "S2021-001", "", "Alice Williams", new Guid("d704ad35-782c-4e33-961b-d5a19234491c"), new Guid("2a7ca1c5-b8e4-4c70-8403-7162ecef9d66") },
                    { new Guid("cc7331ef-fcb8-4792-844d-53537df0170e"), null, null, "Jane Smith", "Student", false, new Guid("6be4b325-5947-4a6a-871e-8cdb03795e22"), "Canon EOS R6 Camera", new DateTime(2025, 10, 19, 15, 26, 53, 410, DateTimeKind.Utc).AddTicks(2098), null, new DateTime(2025, 10, 27, 15, 26, 53, 410, DateTimeKind.Utc).AddTicks(2099), "", "Returned", "S2021-002", "", "Bob Brown", new Guid("b6283ce5-be1a-45eb-8f1d-3717309efd93"), new Guid("3fd04f17-d0a2-4804-bb40-9faced29af00") },
                    { new Guid("d8d45203-9be3-424c-ad83-2c09510f73f3"), null, null, "Peter Jones", "Student", false, new Guid("2521ec9a-c75e-4782-8531-4c3daf46f87a"), "Epson Home Cinema Projector", new DateTime(2025, 10, 28, 15, 26, 53, 410, DateTimeKind.Utc).AddTicks(2110), null, null, "", "Borrowed", "S2021-003", "", "Alice Williams", new Guid("d704ad35-782c-4e33-961b-d5a19234491c"), new Guid("983fbbd0-f51e-40de-89a0-f71f7dff8b29") }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "LentItems",
                keyColumn: "Id",
                keyValue: new Guid("2e7332a7-931e-4847-b9e6-2965518c4b5e"));

            migrationBuilder.DeleteData(
                table: "LentItems",
                keyColumn: "Id",
                keyValue: new Guid("7f10908d-0cb0-4b2e-a4b4-3ae8bd4af134"));

            migrationBuilder.DeleteData(
                table: "LentItems",
                keyColumn: "Id",
                keyValue: new Guid("aa3c4916-14ac-4636-a8a6-8eb88be3b4d3"));

            migrationBuilder.DeleteData(
                table: "LentItems",
                keyColumn: "Id",
                keyValue: new Guid("cc7331ef-fcb8-4792-844d-53537df0170e"));

            migrationBuilder.DeleteData(
                table: "LentItems",
                keyColumn: "Id",
                keyValue: new Guid("d8d45203-9be3-424c-ad83-2c09510f73f3"));

            migrationBuilder.DeleteData(
                table: "Staff",
                keyColumn: "Id",
                keyValue: new Guid("3656e8af-b4ef-4a8b-a5d1-80e1cff4352d"));

            migrationBuilder.DeleteData(
                table: "Staff",
                keyColumn: "Id",
                keyValue: new Guid("3c9c8e15-41fb-4253-b7f0-b6e2475d8eec"));

            migrationBuilder.DeleteData(
                table: "Staff",
                keyColumn: "Id",
                keyValue: new Guid("5adad555-c176-48d8-b53d-8112e568a9fb"));

            migrationBuilder.DeleteData(
                table: "Staff",
                keyColumn: "Id",
                keyValue: new Guid("5d78a98e-59a0-4993-9ad2-d38781bc0ed6"));

            migrationBuilder.DeleteData(
                table: "Staff",
                keyColumn: "Id",
                keyValue: new Guid("81b4d6e5-4369-41ad-9ea1-d6ae122df801"));

            migrationBuilder.DeleteData(
                table: "Students",
                keyColumn: "Id",
                keyValue: new Guid("2a7ca1c5-b8e4-4c70-8403-7162ecef9d66"));

            migrationBuilder.DeleteData(
                table: "Students",
                keyColumn: "Id",
                keyValue: new Guid("3fd04f17-d0a2-4804-bb40-9faced29af00"));

            migrationBuilder.DeleteData(
                table: "Students",
                keyColumn: "Id",
                keyValue: new Guid("66f6e1c4-42b7-4e8f-b979-05171ed7214e"));

            migrationBuilder.DeleteData(
                table: "Students",
                keyColumn: "Id",
                keyValue: new Guid("983fbbd0-f51e-40de-89a0-f71f7dff8b29"));

            migrationBuilder.DeleteData(
                table: "Students",
                keyColumn: "Id",
                keyValue: new Guid("d94efbe6-6725-4d35-8b87-a8b15eb5e55b"));

            migrationBuilder.DeleteData(
                table: "Teachers",
                keyColumn: "Id",
                keyValue: new Guid("452a856b-22e9-476c-a6e2-4adf4e676783"));

            migrationBuilder.DeleteData(
                table: "Teachers",
                keyColumn: "Id",
                keyValue: new Guid("7a232ead-e7aa-4dca-bdcc-39d31bae6174"));

            migrationBuilder.DeleteData(
                table: "Teachers",
                keyColumn: "Id",
                keyValue: new Guid("a5fe58cb-534c-4583-81a7-19ab7bc0bc97"));

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("492fe915-d579-43b6-818e-093215cd1868"));

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("722e7dbc-ce0b-4ae0-88ab-902e0f7ce03a"));

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("72f88225-2575-48d6-a69e-895e5cb65ad3"));

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("7e0bb7c6-21ca-4215-a801-8b7423d14632"));

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("ddc38afd-3ab7-4c14-a797-1c23b2dd8c78"));

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("f0b0ba71-b261-4429-b0db-065828de48ef"));

            migrationBuilder.DeleteData(
                table: "Items",
                keyColumn: "Id",
                keyValue: new Guid("2521ec9a-c75e-4782-8531-4c3daf46f87a"));

            migrationBuilder.DeleteData(
                table: "Items",
                keyColumn: "Id",
                keyValue: new Guid("257e43ef-bf46-4f85-96d0-7fce1de4e456"));

            migrationBuilder.DeleteData(
                table: "Items",
                keyColumn: "Id",
                keyValue: new Guid("6be4b325-5947-4a6a-871e-8cdb03795e22"));

            migrationBuilder.DeleteData(
                table: "Items",
                keyColumn: "Id",
                keyValue: new Guid("e5b41e0c-0288-437b-81ee-bfa3ef084f12"));

            migrationBuilder.DeleteData(
                table: "Items",
                keyColumn: "Id",
                keyValue: new Guid("efa11cc5-ff67-438b-b6fb-a1904cce334d"));

            migrationBuilder.DeleteData(
                table: "Teachers",
                keyColumn: "Id",
                keyValue: new Guid("b6283ce5-be1a-45eb-8f1d-3717309efd93"));

            migrationBuilder.DeleteData(
                table: "Teachers",
                keyColumn: "Id",
                keyValue: new Guid("d704ad35-782c-4e33-961b-d5a19234491c"));

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("2a7ca1c5-b8e4-4c70-8403-7162ecef9d66"));

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("3656e8af-b4ef-4a8b-a5d1-80e1cff4352d"));

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("3c9c8e15-41fb-4253-b7f0-b6e2475d8eec"));

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("3fd04f17-d0a2-4804-bb40-9faced29af00"));

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("452a856b-22e9-476c-a6e2-4adf4e676783"));

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("5adad555-c176-48d8-b53d-8112e568a9fb"));

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("5d78a98e-59a0-4993-9ad2-d38781bc0ed6"));

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("66f6e1c4-42b7-4e8f-b979-05171ed7214e"));

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("7a232ead-e7aa-4dca-bdcc-39d31bae6174"));

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("81b4d6e5-4369-41ad-9ea1-d6ae122df801"));

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("983fbbd0-f51e-40de-89a0-f71f7dff8b29"));

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("a5fe58cb-534c-4583-81a7-19ab7bc0bc97"));

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("d94efbe6-6725-4d35-8b87-a8b15eb5e55b"));

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("b6283ce5-be1a-45eb-8f1d-3717309efd93"));

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("d704ad35-782c-4e33-961b-d5a19234491c"));

            migrationBuilder.AlterColumn<DateTime>(
                name: "LentAt",
                table: "LentItems",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "LentAt",
                table: "ArchiveLentItems",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldNullable: true);

            migrationBuilder.InsertData(
                table: "Items",
                columns: new[] { "Id", "Barcode", "BarcodeImage", "Category", "Condition", "CreatedAt", "Description", "Image", "ImageMimeType", "ItemMake", "ItemModel", "ItemName", "ItemType", "SerialNumber", "Status", "UpdatedAt" },
                values: new object[,]
                {
                    { new Guid("1a512806-4072-4749-8c60-fed2e7456cb8"), null, null, "MediaEquipment", "Good", new DateTime(2025, 10, 29, 14, 18, 11, 613, DateTimeKind.Utc).AddTicks(7973), "Dynamic microphone for vocal recording.", null, null, "Shure", "SM7B", "Shure SM7B Microphone", "Microphone", "SN-MC-SHR-004", "Available", new DateTime(2025, 10, 29, 14, 18, 11, 613, DateTimeKind.Utc).AddTicks(7974) },
                    { new Guid("2e41a6e8-1b80-4756-a204-5ecde0ad6fb6"), null, null, "MediaEquipment", "Good", new DateTime(2025, 10, 29, 14, 18, 11, 613, DateTimeKind.Utc).AddTicks(7966), "Full-frame mirrorless camera with 4K video.", null, null, "Canon", "EOS R6", "Canon EOS R6 Camera", "Camera", "SN-CM-CAN-003", "Available", new DateTime(2025, 10, 29, 14, 18, 11, 613, DateTimeKind.Utc).AddTicks(7967) },
                    { new Guid("6331053f-c190-476f-8e08-01d44b3650a4"), null, null, "MediaEquipment", "New", new DateTime(2025, 10, 29, 14, 18, 11, 613, DateTimeKind.Utc).AddTicks(7960), "1080p projector for classroom presentations.", null, null, "Epson", "HC 2250", "Epson Home Cinema Projector", "Projector", "SN-PR-EPS-002", "Available", new DateTime(2025, 10, 29, 14, 18, 11, 613, DateTimeKind.Utc).AddTicks(7961) },
                    { new Guid("7f19c228-c324-47e1-aa6b-46aa6f68a98e"), null, null, "Electronics", "Good", new DateTime(2025, 10, 29, 14, 18, 11, 613, DateTimeKind.Utc).AddTicks(7954), "High-performance laptop for video editing.", null, null, "Dell", "XPS 15 9510", "Dell XPS 15 Laptop", "Laptop", "SN-LP-DELL-001", "Available", new DateTime(2025, 10, 29, 14, 18, 11, 613, DateTimeKind.Utc).AddTicks(7955) },
                    { new Guid("9f078b90-1140-4932-8fb7-dfed46541540"), null, null, "Electronics", "Good", new DateTime(2025, 10, 29, 14, 18, 11, 613, DateTimeKind.Utc).AddTicks(7983), "Tablet for digital art and design classes.", null, null, "Apple", "iPad Pro 12.9-inch", "Apple iPad Pro", "Tablet", "SN-TB-APL-005", "Available", new DateTime(2025, 10, 29, 14, 18, 11, 613, DateTimeKind.Utc).AddTicks(7986) }
                });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "Email", "FirstName", "LastName", "MiddleName", "PasswordHash", "PhoneNumber", "Status", "UserRole", "Username" },
                values: new object[,]
                {
                    { new Guid("08efd894-7889-48f1-9c29-d8541eb828f7"), "teacher4@example.com", "Teacher4", "Smith", null, "$2a$11$dXfM6jqpqV5SVEOsBSGB4OlJK.5ApWSQlHMSYz9Vfk5hj3NlLCDzi", "0917423456", "Offline", "Teacher", "teacher4" },
                    { new Guid("1fa15a68-cb7a-4797-9a59-57b68edb4a6e"), "staff3@example.com", "Staff3", "Doe", null, "$2a$11$AOK3bTJ1pRRc0MUCKRt/o.fqyElsVoYe5ynyDI.Q3V0zu/jVqJnhi", "0998376543", "Offline", "Staff", "staff3" },
                    { new Guid("2250b3a5-8c28-4c9d-a725-7c96b4fa3ec5"), "admin4@example.com", "Admin4", "User", null, "$2a$11$tJnyg2e.lJ1RJMhP5h4nJO/F4spVe0Nf/PReM91v7ted9URJQY1eS", null, "Offline", "Admin", "admin4" },
                    { new Guid("244773dd-5ac9-4cc3-8b29-89e7185c82cc"), "teacher3@example.com", "Teacher3", "Smith", null, "$2a$11$ucpDpm2naJn3Yix6xTklu.83.56pfkfQK.KPIqf7d9wXcpdMuci0m", "0917323456", "Offline", "Teacher", "teacher3" },
                    { new Guid("32f0c9e7-c19a-492d-b1d8-083e7ca1f43f"), "superadmin@example.com", "Super", "Admin", null, "$2a$11$SufLjpayC//YRa5PsVAryOgksALNhgGfGlLN6iz8tQM1R6hnzGSEK", null, "Offline", "SuperAdmin", "superadmin" },
                    { new Guid("333a16d4-02f9-457b-bd42-4fbb972fd5fb"), "john.doe@example.com", "John", "Doe", null, "$2a$11$m2Lfj2TQkalHmXtxUq9MaOtyHmahBGuZXulDclD/JGUpiBBNChUdG", "0912134567", "Offline", "Student", "johndoe" },
                    { new Guid("34acfc5d-c3dd-4e57-943b-b2b00be3662c"), "student5@example.com", "Student5", "Jones", null, "$2a$11$XpzmesGDYAbE/e7Dm1VApemGgLKibz0zopznf79kcvqkaCnuVzgmC", "0912534567", "Offline", "Student", "student5" },
                    { new Guid("34cbe681-2c94-46e1-9ff4-3b3eb5cb033a"), "admin5@example.com", "Admin5", "User", null, "$2a$11$hXLxK/Te48WpM0bxtdMSjOUF4rAbCvTYVa8cQVMoxZopi4QxMAp5u", null, "Offline", "Admin", "admin5" },
                    { new Guid("7a1afac4-0933-467f-a099-21f59c7f0adb"), "admin2@example.com", "Admin2", "User", null, "$2a$11$sQ1quNVlLCsRh4I5ISMpGur6otCobchuRmTRvy8bG1FEqR9nbb8NO", null, "Offline", "Admin", "admin2" },
                    { new Guid("7ce158ee-72a7-44d0-b0fc-40234fac7b75"), "staff4@example.com", "Staff4", "Doe", null, "$2a$11$pmlpUxzsJX/8aX6MEk6w1ex/TZD39LfGPG31W5wsD8GEJW0vYn/sW", "0998476543", "Offline", "Staff", "staff4" },
                    { new Guid("99f7870c-0603-4b93-ac7a-eed3315ca07a"), "admin1@example.com", "Admin1", "User", null, "$2a$11$8fZWemky4kkBiV18.npA8uiJyA5Ox4X1jaD/shPpwet3R85k0Bg96", null, "Offline", "Admin", "admin1" },
                    { new Guid("a02f3fa3-3add-441e-959a-b2cad649d164"), "peter.jones@example.com", "Peter", "Jones", null, "$2a$11$lwShbvU23bsf8zen1YFHk.qQGNZppZiDMTW.ucbpF1e18Z/GAzEsy", "0912334567", "Offline", "Student", "peterjones" },
                    { new Guid("a5bce19e-4c57-45cb-aca7-db7625a0c112"), "bob.brown@example.com", "Bob", "Brown", null, "$2a$11$/e2KPmi3QZ0/eE7vs67rAel2NwcV1U4gnjdOPvRCj4h2nbs/JNNcu", "0917223456", "Offline", "Teacher", "bobbrown" },
                    { new Guid("a918b1ee-f128-4233-929f-df631ef843b4"), "staff2@example.com", "Staff2", "Doe", null, "$2a$11$y4HPXM46dADD5eQddoMaJu08WDHcLfumrvvijoHs.mei.WeUMIEvW", "0998276543", "Offline", "Staff", "staff2" },
                    { new Guid("af6bf375-ecd6-49f5-bfc9-711d1cf18c12"), "student4@example.com", "Student4", "Jones", null, "$2a$11$qLgfIV6GGIIqtC4geDiXrud2QDU8khgQO84aRN60TD/MvzP..lGJS", "0912434567", "Offline", "Student", "student4" },
                    { new Guid("d82702b6-8326-466e-8083-ad55a9b1e03a"), "alice.williams@example.com", "Alice", "Williams", null, "$2a$11$RUEnQy6SKQkFOIJKFmTyVeSQZsKGEuJjXsFY38wDYuzqnKSp6hNjO", "0917123456", "Offline", "Teacher", "alicewilliams" },
                    { new Guid("de880717-2d07-49eb-a609-de246068c28c"), "admin3@example.com", "Admin3", "User", null, "$2a$11$i7kTO0F6dtCiLRSpT9foxutKefHqVvt4MMOkA4AfkO.rhEaGOkkli", null, "Offline", "Admin", "admin3" },
                    { new Guid("f3678581-4513-46fe-8e2d-e9c4059ddcc2"), "staff5@example.com", "Staff5", "Doe", null, "$2a$11$t90TbbTTdyi9qctP.AG/xOkpW6el/iKaIagmYmtAk3M0Wupd4hXPW", "0998576543", "Offline", "Staff", "staff5" },
                    { new Guid("fe790f52-f317-45e9-a8b0-5858a7aa66dd"), "jane.smith@example.com", "Jane", "Smith", null, "$2a$11$jf7l6Mzm6Imhpz1bWmkNjumgLcJ/ScqvlMCKHD0FrqeHJZW2Qmcuy", "0912234567", "Offline", "Student", "janesmith" },
                    { new Guid("ff8f0411-1b61-4223-bfba-1d23c24e571a"), "staff1@example.com", "Staff1", "Doe", null, "$2a$11$dyS0G2jGnwgHuxFaASoY7uyO4hCA39qJV8FZ0Xm8Bjt0P/SEB12cK", "0998176543", "Offline", "Staff", "staff1" },
                    { new Guid("ffde25a6-29aa-4a75-aa35-a5ba3d5bd74a"), "teacher5@example.com", "Teacher5", "Smith", null, "$2a$11$J4Gs9.KIXb8fLee8guRU/e4Ek1kBoBSSVtlFn0JaupeER6YPqdTVq", "0917523456", "Offline", "Teacher", "teacher5" }
                });

            migrationBuilder.InsertData(
                table: "LentItems",
                columns: new[] { "Id", "Barcode", "BarcodeImage", "BorrowerFullName", "BorrowerRole", "IsHiddenFromUser", "ItemId", "ItemName", "LentAt", "Remarks", "ReturnedAt", "Room", "Status", "StudentIdNumber", "SubjectTimeSchedule", "TeacherFullName", "TeacherId", "UserId" },
                values: new object[,]
                {
                    { new Guid("b7d644f0-727f-4311-9973-d16f06bd9ba1"), null, null, "Bob Brown", "Teacher", false, new Guid("9f078b90-1140-4932-8fb7-dfed46541540"), "Apple iPad Pro", new DateTime(2025, 9, 29, 14, 18, 11, 613, DateTimeKind.Utc).AddTicks(8771), null, new DateTime(2025, 10, 14, 14, 18, 11, 613, DateTimeKind.Utc).AddTicks(8772), "", "Returned", null, "", "", null, new Guid("a5bce19e-4c57-45cb-aca7-db7625a0c112") },
                    { new Guid("d7e847fe-e4b2-407d-9f34-051a34f9e915"), null, null, "Alice Williams", "Teacher", false, new Guid("1a512806-4072-4749-8c60-fed2e7456cb8"), "Shure SM7B Microphone", new DateTime(2025, 10, 26, 14, 18, 11, 613, DateTimeKind.Utc).AddTicks(8756), null, null, "", "Borrowed", null, "", "", null, new Guid("d82702b6-8326-466e-8083-ad55a9b1e03a") }
                });

            migrationBuilder.InsertData(
                table: "Staff",
                columns: new[] { "Id", "Position" },
                values: new object[,]
                {
                    { new Guid("1fa15a68-cb7a-4797-9a59-57b68edb4a6e"), "Lab Technician" },
                    { new Guid("7ce158ee-72a7-44d0-b0fc-40234fac7b75"), "Lab Technician" },
                    { new Guid("a918b1ee-f128-4233-929f-df631ef843b4"), "Lab Technician" },
                    { new Guid("f3678581-4513-46fe-8e2d-e9c4059ddcc2"), "Lab Technician" },
                    { new Guid("ff8f0411-1b61-4223-bfba-1d23c24e571a"), "Lab Technician" }
                });

            migrationBuilder.InsertData(
                table: "Students",
                columns: new[] { "Id", "BackStudentIdPicture", "CityMunicipality", "Course", "FrontStudentIdPicture", "PostalCode", "ProfilePicture", "Province", "Section", "Street", "StudentIdNumber", "Year" },
                values: new object[,]
                {
                    { new Guid("333a16d4-02f9-457b-bd42-4fbb972fd5fb"), null, "", "Computer Science", null, "", null, "", "A", "", "S2021-001", "3" },
                    { new Guid("34acfc5d-c3dd-4e57-943b-b2b00be3662c"), null, "", "Computer Science", null, "", null, "", "A", "", "2023-0005", "3" },
                    { new Guid("a02f3fa3-3add-441e-959a-b2cad649d164"), null, "", "Computer Science", null, "", null, "", "A", "", "S2021-003", "3" },
                    { new Guid("af6bf375-ecd6-49f5-bfc9-711d1cf18c12"), null, "", "Computer Science", null, "", null, "", "A", "", "2023-0004", "3" },
                    { new Guid("fe790f52-f317-45e9-a8b0-5858a7aa66dd"), null, "", "Computer Science", null, "", null, "", "A", "", "S2021-002", "3" }
                });

            migrationBuilder.InsertData(
                table: "Teachers",
                columns: new[] { "Id", "Department" },
                values: new object[,]
                {
                    { new Guid("08efd894-7889-48f1-9c29-d8541eb828f7"), "Information Technology" },
                    { new Guid("244773dd-5ac9-4cc3-8b29-89e7185c82cc"), "Information Technology" },
                    { new Guid("a5bce19e-4c57-45cb-aca7-db7625a0c112"), "Information Technology" },
                    { new Guid("d82702b6-8326-466e-8083-ad55a9b1e03a"), "Information Technology" },
                    { new Guid("ffde25a6-29aa-4a75-aa35-a5ba3d5bd74a"), "Information Technology" }
                });

            migrationBuilder.InsertData(
                table: "LentItems",
                columns: new[] { "Id", "Barcode", "BarcodeImage", "BorrowerFullName", "BorrowerRole", "IsHiddenFromUser", "ItemId", "ItemName", "LentAt", "Remarks", "ReturnedAt", "Room", "Status", "StudentIdNumber", "SubjectTimeSchedule", "TeacherFullName", "TeacherId", "UserId" },
                values: new object[,]
                {
                    { new Guid("4fd60aec-0b5d-42d9-a65f-972122aff81a"), null, null, "Jane Smith", "Student", false, new Guid("2e41a6e8-1b80-4756-a204-5ecde0ad6fb6"), "Canon EOS R6 Camera", new DateTime(2025, 10, 19, 14, 18, 11, 613, DateTimeKind.Utc).AddTicks(8734), null, new DateTime(2025, 10, 27, 14, 18, 11, 613, DateTimeKind.Utc).AddTicks(8735), "", "Returned", "S2021-002", "", "Bob Brown", new Guid("a5bce19e-4c57-45cb-aca7-db7625a0c112"), new Guid("fe790f52-f317-45e9-a8b0-5858a7aa66dd") },
                    { new Guid("c56f51db-ef35-4cc8-9b52-59bda7896aa1"), null, null, "John Doe", "Student", false, new Guid("7f19c228-c324-47e1-aa6b-46aa6f68a98e"), "Dell XPS 15 Laptop", new DateTime(2025, 10, 24, 14, 18, 11, 613, DateTimeKind.Utc).AddTicks(8712), null, null, "", "Borrowed", "S2021-001", "", "Alice Williams", new Guid("d82702b6-8326-466e-8083-ad55a9b1e03a"), new Guid("333a16d4-02f9-457b-bd42-4fbb972fd5fb") },
                    { new Guid("eb2337a8-e9d5-4474-87dd-01cac3a4e330"), null, null, "Peter Jones", "Student", false, new Guid("6331053f-c190-476f-8e08-01d44b3650a4"), "Epson Home Cinema Projector", new DateTime(2025, 10, 28, 14, 18, 11, 613, DateTimeKind.Utc).AddTicks(8748), null, null, "", "Borrowed", "S2021-003", "", "Alice Williams", new Guid("d82702b6-8326-466e-8083-ad55a9b1e03a"), new Guid("a02f3fa3-3add-441e-959a-b2cad649d164") }
                });
        }
    }
}
