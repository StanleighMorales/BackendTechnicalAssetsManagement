using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace BackendTechnicalAssetsManagement.Migrations
{
    /// <inheritdoc />
    public partial class AddItemStatusField : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "LentItems",
                keyColumn: "Id",
                keyValue: new Guid("606c96be-6019-4f1f-9f9c-f2eb0f1bbfdc"));

            migrationBuilder.DeleteData(
                table: "LentItems",
                keyColumn: "Id",
                keyValue: new Guid("753ddf13-a470-4d54-924c-46b6eb22a7f6"));

            migrationBuilder.DeleteData(
                table: "LentItems",
                keyColumn: "Id",
                keyValue: new Guid("c7aef9c8-6fac-4fa0-90a1-2ebc0f7d44f2"));

            migrationBuilder.DeleteData(
                table: "LentItems",
                keyColumn: "Id",
                keyValue: new Guid("d7ef5226-6204-4cc3-936a-21e70f72b92b"));

            migrationBuilder.DeleteData(
                table: "LentItems",
                keyColumn: "Id",
                keyValue: new Guid("ecbedfef-95b4-4ae7-bd48-b19e9e877bcc"));

            migrationBuilder.DeleteData(
                table: "Staff",
                keyColumn: "Id",
                keyValue: new Guid("0d05cec5-e7ac-40d8-ba84-3e86705625f9"));

            migrationBuilder.DeleteData(
                table: "Staff",
                keyColumn: "Id",
                keyValue: new Guid("6b493779-85c1-4bdb-99d7-e6a796dcb07e"));

            migrationBuilder.DeleteData(
                table: "Staff",
                keyColumn: "Id",
                keyValue: new Guid("8e37afc4-d422-4c2d-a54b-6244a6e3ebaa"));

            migrationBuilder.DeleteData(
                table: "Staff",
                keyColumn: "Id",
                keyValue: new Guid("927a9650-a8bd-4e00-b1d3-c80d75e5fb2c"));

            migrationBuilder.DeleteData(
                table: "Staff",
                keyColumn: "Id",
                keyValue: new Guid("93884b89-9910-440a-be14-89180ce5336e"));

            migrationBuilder.DeleteData(
                table: "Students",
                keyColumn: "Id",
                keyValue: new Guid("08c6f64d-f286-47dd-9d0c-00ab54a33fc3"));

            migrationBuilder.DeleteData(
                table: "Students",
                keyColumn: "Id",
                keyValue: new Guid("3d5fb3ce-08f8-485c-824b-f2b68c7a98b8"));

            migrationBuilder.DeleteData(
                table: "Students",
                keyColumn: "Id",
                keyValue: new Guid("420df9ac-3b37-4c43-8318-4083025d1409"));

            migrationBuilder.DeleteData(
                table: "Students",
                keyColumn: "Id",
                keyValue: new Guid("bf82ec20-545a-4caa-92a4-32e4172abf87"));

            migrationBuilder.DeleteData(
                table: "Students",
                keyColumn: "Id",
                keyValue: new Guid("c0d30f61-9b9f-4425-b0c9-d51e7fe87b5c"));

            migrationBuilder.DeleteData(
                table: "Teachers",
                keyColumn: "Id",
                keyValue: new Guid("2315dbf4-f226-45bd-a110-fefc778286a8"));

            migrationBuilder.DeleteData(
                table: "Teachers",
                keyColumn: "Id",
                keyValue: new Guid("28a911a2-812e-497d-9c9a-edcee261e06c"));

            migrationBuilder.DeleteData(
                table: "Teachers",
                keyColumn: "Id",
                keyValue: new Guid("b1f64a63-6509-4f8b-aa0d-c3e0136f38ad"));

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("586a3811-312d-494d-82d4-0a13365c75ea"));

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("76637bfa-93a8-4eca-9c6b-17ff70a5fc47"));

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("784301f4-2faa-44f5-8de4-3fa534cd306f"));

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("d5db98c4-5556-4587-b12e-f58cc6193358"));

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("df2e3eca-0646-4dd3-96bb-520f87268676"));

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("e0cb2e0e-2d55-47cc-987e-e220339a7f3b"));

            migrationBuilder.DeleteData(
                table: "Items",
                keyColumn: "Id",
                keyValue: new Guid("05ed6e12-5eb6-41cf-b35e-60a5caa33e98"));

            migrationBuilder.DeleteData(
                table: "Items",
                keyColumn: "Id",
                keyValue: new Guid("52c44875-1e75-4d8e-8179-0c6121e2a5d3"));

            migrationBuilder.DeleteData(
                table: "Items",
                keyColumn: "Id",
                keyValue: new Guid("a276a41a-70ea-4224-9357-4369eda33618"));

            migrationBuilder.DeleteData(
                table: "Items",
                keyColumn: "Id",
                keyValue: new Guid("f44075e4-ac7c-4920-b2bf-26779f6e5cd2"));

            migrationBuilder.DeleteData(
                table: "Items",
                keyColumn: "Id",
                keyValue: new Guid("fd51ee56-38e8-4711-841c-f5bbcc361af3"));

            migrationBuilder.DeleteData(
                table: "Teachers",
                keyColumn: "Id",
                keyValue: new Guid("33e38d82-7028-4891-bcec-6fe60f48cdc6"));

            migrationBuilder.DeleteData(
                table: "Teachers",
                keyColumn: "Id",
                keyValue: new Guid("98e0c707-9383-484c-bb66-31a0823b0936"));

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("08c6f64d-f286-47dd-9d0c-00ab54a33fc3"));

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("0d05cec5-e7ac-40d8-ba84-3e86705625f9"));

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("2315dbf4-f226-45bd-a110-fefc778286a8"));

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("28a911a2-812e-497d-9c9a-edcee261e06c"));

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("3d5fb3ce-08f8-485c-824b-f2b68c7a98b8"));

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("420df9ac-3b37-4c43-8318-4083025d1409"));

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("6b493779-85c1-4bdb-99d7-e6a796dcb07e"));

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("8e37afc4-d422-4c2d-a54b-6244a6e3ebaa"));

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("927a9650-a8bd-4e00-b1d3-c80d75e5fb2c"));

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("93884b89-9910-440a-be14-89180ce5336e"));

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("b1f64a63-6509-4f8b-aa0d-c3e0136f38ad"));

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("bf82ec20-545a-4caa-92a4-32e4172abf87"));

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("c0d30f61-9b9f-4425-b0c9-d51e7fe87b5c"));

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("33e38d82-7028-4891-bcec-6fe60f48cdc6"));

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("98e0c707-9383-484c-bb66-31a0823b0936"));

            migrationBuilder.AddColumn<string>(
                name: "Status",
                table: "Items",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.InsertData(
                table: "Items",
                columns: new[] { "Id", "Barcode", "BarcodeImage", "Category", "Condition", "CreatedAt", "Description", "Image", "ImageMimeType", "ItemMake", "ItemModel", "ItemName", "ItemType", "SerialNumber", "Status", "UpdatedAt" },
                values: new object[,]
                {
                    { new Guid("0bf74594-7fa6-4e7f-82ca-5a24df3368a6"), null, null, "MediaEquipment", "Good", new DateTime(2025, 10, 29, 8, 1, 57, 351, DateTimeKind.Utc).AddTicks(1828), "Dynamic microphone for vocal recording.", null, null, "Shure", "SM7B", "Shure SM7B Microphone", "Microphone", "SN-MC-SHR-004", "Available", new DateTime(2025, 10, 29, 8, 1, 57, 351, DateTimeKind.Utc).AddTicks(1828) },
                    { new Guid("6411d0c0-e592-4007-b55d-050042a89929"), null, null, "MediaEquipment", "Good", new DateTime(2025, 10, 29, 8, 1, 57, 351, DateTimeKind.Utc).AddTicks(1820), "Full-frame mirrorless camera with 4K video.", null, null, "Canon", "EOS R6", "Canon EOS R6 Camera", "Camera", "SN-CM-CAN-003", "Available", new DateTime(2025, 10, 29, 8, 1, 57, 351, DateTimeKind.Utc).AddTicks(1822) },
                    { new Guid("9065d5eb-5671-4f18-858e-cee5ec3ede26"), null, null, "Electronics", "Good", new DateTime(2025, 10, 29, 8, 1, 57, 351, DateTimeKind.Utc).AddTicks(1804), "High-performance laptop for video editing.", null, null, "Dell", "XPS 15 9510", "Dell XPS 15 Laptop", "Laptop", "SN-LP-DELL-001", "Available", new DateTime(2025, 10, 29, 8, 1, 57, 351, DateTimeKind.Utc).AddTicks(1805) },
                    { new Guid("aed4af47-3898-4ec1-b098-f0cb312c71f9"), null, null, "MediaEquipment", "New", new DateTime(2025, 10, 29, 8, 1, 57, 351, DateTimeKind.Utc).AddTicks(1812), "1080p projector for classroom presentations.", null, null, "Epson", "HC 2250", "Epson Home Cinema Projector", "Projector", "SN-PR-EPS-002", "Available", new DateTime(2025, 10, 29, 8, 1, 57, 351, DateTimeKind.Utc).AddTicks(1813) },
                    { new Guid("f8b9deca-0835-434e-b8b2-f2c54731e8c5"), null, null, "Electronics", "Good", new DateTime(2025, 10, 29, 8, 1, 57, 351, DateTimeKind.Utc).AddTicks(1834), "Tablet for digital art and design classes.", null, null, "Apple", "iPad Pro 12.9-inch", "Apple iPad Pro", "Tablet", "SN-TB-APL-005", "Available", new DateTime(2025, 10, 29, 8, 1, 57, 351, DateTimeKind.Utc).AddTicks(1837) }
                });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "Email", "FirstName", "LastName", "MiddleName", "PasswordHash", "PhoneNumber", "Status", "UserRole", "Username" },
                values: new object[,]
                {
                    { new Guid("075ce85c-f3d1-45c1-931d-ac3be6514c9c"), "staff1@example.com", "Staff1", "Doe", null, "$2a$11$4CuuL03nyAWXPyA/9ufs4.Hr0wwB8CqXCfbn8QA7KqTxN3G8bvdXK", "0998176543", "Offline", "Staff", "staff1" },
                    { new Guid("2652755c-dcc0-4620-b473-e6e949c65801"), "admin5@example.com", "Admin5", "User", null, "$2a$11$vVnpEUtb5CemSSH1LSL1c.gtBeRxJzrceIVUzJXWmRwxuLicVY8k2", null, "Offline", "Admin", "admin5" },
                    { new Guid("3f721c0d-475e-4544-869a-53ba10f10bbd"), "student5@example.com", "Student5", "Jones", null, "$2a$11$IO04SFUeqnJVvMdpPP4WcehCls2f79XxFmH4wvNXQCsu6DJiAmTyy", "0912534567", "Offline", "Student", "student5" },
                    { new Guid("3f8137d2-73c6-447c-b317-2004d639b8ca"), "peter.jones@example.com", "Peter", "Jones", null, "$2a$11$x47esQcw48dfU3IbXwK3rOv6TlbVXeH2DXfB79qR.hg.CZEt/JgsK", "0912334567", "Offline", "Student", "peterjones" },
                    { new Guid("433e1f2f-b6db-4b3c-ab81-6da03575c7ba"), "alice.williams@example.com", "Alice", "Williams", null, "$2a$11$ojFTxgYH1OLhivWMhLYrb.tOuWcevCmcIc3LimMzPsyXD4AUZ9ShO", "0917123456", "Offline", "Teacher", "alicewilliams" },
                    { new Guid("45d7b3aa-c6a2-44ee-a9d8-c7f18e1db8d2"), "staff5@example.com", "Staff5", "Doe", null, "$2a$11$G70VJtC7tAkpSEaCM0V50uR4Oz2XLJfdHz73hH0wcvZO7NOzXwmvK", "0998576543", "Offline", "Staff", "staff5" },
                    { new Guid("4c7f39d0-17b4-4ebe-8f3d-e81f300f0d8b"), "staff2@example.com", "Staff2", "Doe", null, "$2a$11$VifNCn8TvVecjShpgBAiAee5iQCriF7aD6/vuFjHBh5/usVm/u75i", "0998276543", "Offline", "Staff", "staff2" },
                    { new Guid("52dd452d-0178-4f21-a4a0-3224a2f50534"), "john.doe@example.com", "John", "Doe", null, "$2a$11$Azdf/5Q3QzVzE1wdudXrAe819msKG10xjOXNT7zHgE1M0MlnSJRNC", "0912134567", "Offline", "Student", "johndoe" },
                    { new Guid("5b7f652c-b7fc-4688-8e1a-067e46545faa"), "admin4@example.com", "Admin4", "User", null, "$2a$11$CSQPh8KmwWiZnhf2ck9Xxuv8u/K8wl0KLqjcqdRg/2dOqDejq2FI6", null, "Offline", "Admin", "admin4" },
                    { new Guid("6659dd70-13f9-426e-b64b-dee3705a1a1f"), "teacher5@example.com", "Teacher5", "Smith", null, "$2a$11$ZIjFPOqTees7ga3kbrwp8eYKmYq130WR0pCu7hpqRQzl7IkXqkXeu", "0917523456", "Offline", "Teacher", "teacher5" },
                    { new Guid("7bc04053-3d3f-4d82-9822-e37e3aeda613"), "student4@example.com", "Student4", "Jones", null, "$2a$11$efFFv3/Qc6G0L7LOr6z5iufqR0lYGTafpretj8lB/05JIYbYfPGRS", "0912434567", "Offline", "Student", "student4" },
                    { new Guid("7bdb44cc-8856-44ef-a1cb-f077b45ddc96"), "superadmin@example.com", "Super", "Admin", null, "$2a$11$cfjaYlJdXPRmKr4C2doMq.YvlAsj7wqJtlk3DvkDJXti68mz5yJyy", null, "Offline", "SuperAdmin", "superadmin" },
                    { new Guid("8b8e92c9-b084-466a-b73a-b8cbd2a69cfb"), "jane.smith@example.com", "Jane", "Smith", null, "$2a$11$DRGXzXBG/JKGAvKs75IXhubTGoORmGUgnVK5pCng2vHU/x3hWoUc.", "0912234567", "Offline", "Student", "janesmith" },
                    { new Guid("988e52ee-574d-4661-9e51-5e652e466928"), "bob.brown@example.com", "Bob", "Brown", null, "$2a$11$RHLQ1UgijwsyyIVz/sxMDuJH.VAewZslOt62o7oPjfhyO8IW5cVZy", "0917223456", "Offline", "Teacher", "bobbrown" },
                    { new Guid("9d922d34-1875-4c6a-b477-0a172848879e"), "teacher4@example.com", "Teacher4", "Smith", null, "$2a$11$Upo.QuzuoQHDBKEougDeFepBhPL1OqtqEBYxYIXye53fkK4X3BzAC", "0917423456", "Offline", "Teacher", "teacher4" },
                    { new Guid("b2c0d79c-4424-48a3-afe1-bba5b4d08079"), "teacher3@example.com", "Teacher3", "Smith", null, "$2a$11$8JEZ9OpB7T2e6xNFaz0VIub5NwRNRRO9qSguq5OGJNUslPhXnQ0H2", "0917323456", "Offline", "Teacher", "teacher3" },
                    { new Guid("b34df533-7e58-43cb-9a63-c465e0e62a2b"), "staff3@example.com", "Staff3", "Doe", null, "$2a$11$KKguTjY06QqhnNf.Utm7/.Jf6Uos4Z0XFwQWVX2ytoP4OSlCrM6ti", "0998376543", "Offline", "Staff", "staff3" },
                    { new Guid("c162c052-f4fc-4791-bab3-aefdfb42bc04"), "staff4@example.com", "Staff4", "Doe", null, "$2a$11$6ADv.hywxEfAP4u0ID8/GOTUDtIpc9W7O4AwIuDg4q58qzr0PkEjy", "0998476543", "Offline", "Staff", "staff4" },
                    { new Guid("c370b9e1-857e-4037-bbf4-43752a965a6c"), "admin3@example.com", "Admin3", "User", null, "$2a$11$GXhBIkV9ol5yrtmhfue6nOPigWYlqwaouYcCeB7tY6wnKsGjv34/y", null, "Offline", "Admin", "admin3" },
                    { new Guid("ce1c6a6c-c656-4ec6-8f33-52f088af6a5d"), "admin1@example.com", "Admin1", "User", null, "$2a$11$By.o22QSVv3laotkYAPqD./FY16nNuOLltKdqiYSkiiMC6aOfPO/2", null, "Offline", "Admin", "admin1" },
                    { new Guid("f78b2a14-fe48-41c1-9e9c-48266b44affd"), "admin2@example.com", "Admin2", "User", null, "$2a$11$fGDDYUgGGqw0fM36y14iD.bT.YEyjzwYjhix37qsqDG8358UwbGZa", null, "Offline", "Admin", "admin2" }
                });

            migrationBuilder.InsertData(
                table: "LentItems",
                columns: new[] { "Id", "Barcode", "BarcodeImage", "BorrowerFullName", "BorrowerRole", "IsHiddenFromUser", "ItemId", "ItemName", "LentAt", "Remarks", "ReturnedAt", "Room", "Status", "StudentIdNumber", "SubjectTimeSchedule", "TeacherFullName", "TeacherId", "UserId" },
                values: new object[,]
                {
                    { new Guid("3761f142-9704-4aec-a3c6-7f5f091754b9"), null, null, "Bob Brown", "Teacher", false, new Guid("f8b9deca-0835-434e-b8b2-f2c54731e8c5"), "Apple iPad Pro", new DateTime(2025, 9, 29, 8, 1, 57, 351, DateTimeKind.Utc).AddTicks(2835), null, new DateTime(2025, 10, 14, 8, 1, 57, 351, DateTimeKind.Utc).AddTicks(2837), "", "Returned", null, "", "", null, new Guid("988e52ee-574d-4661-9e51-5e652e466928") },
                    { new Guid("7d43f205-93ac-444c-b668-833882a209af"), null, null, "Alice Williams", "Teacher", false, new Guid("0bf74594-7fa6-4e7f-82ca-5a24df3368a6"), "Shure SM7B Microphone", new DateTime(2025, 10, 26, 8, 1, 57, 351, DateTimeKind.Utc).AddTicks(2812), null, null, "", "Borrowed", null, "", "", null, new Guid("433e1f2f-b6db-4b3c-ab81-6da03575c7ba") }
                });

            migrationBuilder.InsertData(
                table: "Staff",
                columns: new[] { "Id", "Position" },
                values: new object[,]
                {
                    { new Guid("075ce85c-f3d1-45c1-931d-ac3be6514c9c"), "Lab Technician" },
                    { new Guid("45d7b3aa-c6a2-44ee-a9d8-c7f18e1db8d2"), "Lab Technician" },
                    { new Guid("4c7f39d0-17b4-4ebe-8f3d-e81f300f0d8b"), "Lab Technician" },
                    { new Guid("b34df533-7e58-43cb-9a63-c465e0e62a2b"), "Lab Technician" },
                    { new Guid("c162c052-f4fc-4791-bab3-aefdfb42bc04"), "Lab Technician" }
                });

            migrationBuilder.InsertData(
                table: "Students",
                columns: new[] { "Id", "BackStudentIdPicture", "CityMunicipality", "Course", "FrontStudentIdPicture", "PostalCode", "ProfilePicture", "Province", "Section", "Street", "StudentIdNumber", "Year" },
                values: new object[,]
                {
                    { new Guid("3f721c0d-475e-4544-869a-53ba10f10bbd"), null, "", "Computer Science", null, "", null, "", "A", "", "2023-0005", "3" },
                    { new Guid("3f8137d2-73c6-447c-b317-2004d639b8ca"), null, "", "Computer Science", null, "", null, "", "A", "", "S2021-003", "3" },
                    { new Guid("52dd452d-0178-4f21-a4a0-3224a2f50534"), null, "", "Computer Science", null, "", null, "", "A", "", "S2021-001", "3" },
                    { new Guid("7bc04053-3d3f-4d82-9822-e37e3aeda613"), null, "", "Computer Science", null, "", null, "", "A", "", "2023-0004", "3" },
                    { new Guid("8b8e92c9-b084-466a-b73a-b8cbd2a69cfb"), null, "", "Computer Science", null, "", null, "", "A", "", "S2021-002", "3" }
                });

            migrationBuilder.InsertData(
                table: "Teachers",
                columns: new[] { "Id", "Department" },
                values: new object[,]
                {
                    { new Guid("433e1f2f-b6db-4b3c-ab81-6da03575c7ba"), "Information Technology" },
                    { new Guid("6659dd70-13f9-426e-b64b-dee3705a1a1f"), "Information Technology" },
                    { new Guid("988e52ee-574d-4661-9e51-5e652e466928"), "Information Technology" },
                    { new Guid("9d922d34-1875-4c6a-b477-0a172848879e"), "Information Technology" },
                    { new Guid("b2c0d79c-4424-48a3-afe1-bba5b4d08079"), "Information Technology" }
                });

            migrationBuilder.InsertData(
                table: "LentItems",
                columns: new[] { "Id", "Barcode", "BarcodeImage", "BorrowerFullName", "BorrowerRole", "IsHiddenFromUser", "ItemId", "ItemName", "LentAt", "Remarks", "ReturnedAt", "Room", "Status", "StudentIdNumber", "SubjectTimeSchedule", "TeacherFullName", "TeacherId", "UserId" },
                values: new object[,]
                {
                    { new Guid("5925d154-d0dd-4aea-8657-9e88ab1163b5"), null, null, "Jane Smith", "Student", false, new Guid("6411d0c0-e592-4007-b55d-050042a89929"), "Canon EOS R6 Camera", new DateTime(2025, 10, 19, 8, 1, 57, 351, DateTimeKind.Utc).AddTicks(2784), null, new DateTime(2025, 10, 27, 8, 1, 57, 351, DateTimeKind.Utc).AddTicks(2786), "", "Returned", "S2021-002", "", "Bob Brown", new Guid("988e52ee-574d-4661-9e51-5e652e466928"), new Guid("8b8e92c9-b084-466a-b73a-b8cbd2a69cfb") },
                    { new Guid("5b926c00-434e-4c4b-abe1-c85a128efd38"), null, null, "Peter Jones", "Student", false, new Guid("aed4af47-3898-4ec1-b098-f0cb312c71f9"), "Epson Home Cinema Projector", new DateTime(2025, 10, 28, 8, 1, 57, 351, DateTimeKind.Utc).AddTicks(2803), null, null, "", "Borrowed", "S2021-003", "", "Alice Williams", new Guid("433e1f2f-b6db-4b3c-ab81-6da03575c7ba"), new Guid("3f8137d2-73c6-447c-b317-2004d639b8ca") },
                    { new Guid("8e5e4e7d-ba18-45ba-8a5c-24adbe1ee902"), null, null, "John Doe", "Student", false, new Guid("9065d5eb-5671-4f18-858e-cee5ec3ede26"), "Dell XPS 15 Laptop", new DateTime(2025, 10, 24, 8, 1, 57, 351, DateTimeKind.Utc).AddTicks(2755), null, null, "", "Borrowed", "S2021-001", "", "Alice Williams", new Guid("433e1f2f-b6db-4b3c-ab81-6da03575c7ba"), new Guid("52dd452d-0178-4f21-a4a0-3224a2f50534") }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "LentItems",
                keyColumn: "Id",
                keyValue: new Guid("3761f142-9704-4aec-a3c6-7f5f091754b9"));

            migrationBuilder.DeleteData(
                table: "LentItems",
                keyColumn: "Id",
                keyValue: new Guid("5925d154-d0dd-4aea-8657-9e88ab1163b5"));

            migrationBuilder.DeleteData(
                table: "LentItems",
                keyColumn: "Id",
                keyValue: new Guid("5b926c00-434e-4c4b-abe1-c85a128efd38"));

            migrationBuilder.DeleteData(
                table: "LentItems",
                keyColumn: "Id",
                keyValue: new Guid("7d43f205-93ac-444c-b668-833882a209af"));

            migrationBuilder.DeleteData(
                table: "LentItems",
                keyColumn: "Id",
                keyValue: new Guid("8e5e4e7d-ba18-45ba-8a5c-24adbe1ee902"));

            migrationBuilder.DeleteData(
                table: "Staff",
                keyColumn: "Id",
                keyValue: new Guid("075ce85c-f3d1-45c1-931d-ac3be6514c9c"));

            migrationBuilder.DeleteData(
                table: "Staff",
                keyColumn: "Id",
                keyValue: new Guid("45d7b3aa-c6a2-44ee-a9d8-c7f18e1db8d2"));

            migrationBuilder.DeleteData(
                table: "Staff",
                keyColumn: "Id",
                keyValue: new Guid("4c7f39d0-17b4-4ebe-8f3d-e81f300f0d8b"));

            migrationBuilder.DeleteData(
                table: "Staff",
                keyColumn: "Id",
                keyValue: new Guid("b34df533-7e58-43cb-9a63-c465e0e62a2b"));

            migrationBuilder.DeleteData(
                table: "Staff",
                keyColumn: "Id",
                keyValue: new Guid("c162c052-f4fc-4791-bab3-aefdfb42bc04"));

            migrationBuilder.DeleteData(
                table: "Students",
                keyColumn: "Id",
                keyValue: new Guid("3f721c0d-475e-4544-869a-53ba10f10bbd"));

            migrationBuilder.DeleteData(
                table: "Students",
                keyColumn: "Id",
                keyValue: new Guid("3f8137d2-73c6-447c-b317-2004d639b8ca"));

            migrationBuilder.DeleteData(
                table: "Students",
                keyColumn: "Id",
                keyValue: new Guid("52dd452d-0178-4f21-a4a0-3224a2f50534"));

            migrationBuilder.DeleteData(
                table: "Students",
                keyColumn: "Id",
                keyValue: new Guid("7bc04053-3d3f-4d82-9822-e37e3aeda613"));

            migrationBuilder.DeleteData(
                table: "Students",
                keyColumn: "Id",
                keyValue: new Guid("8b8e92c9-b084-466a-b73a-b8cbd2a69cfb"));

            migrationBuilder.DeleteData(
                table: "Teachers",
                keyColumn: "Id",
                keyValue: new Guid("6659dd70-13f9-426e-b64b-dee3705a1a1f"));

            migrationBuilder.DeleteData(
                table: "Teachers",
                keyColumn: "Id",
                keyValue: new Guid("9d922d34-1875-4c6a-b477-0a172848879e"));

            migrationBuilder.DeleteData(
                table: "Teachers",
                keyColumn: "Id",
                keyValue: new Guid("b2c0d79c-4424-48a3-afe1-bba5b4d08079"));

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("2652755c-dcc0-4620-b473-e6e949c65801"));

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("5b7f652c-b7fc-4688-8e1a-067e46545faa"));

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("7bdb44cc-8856-44ef-a1cb-f077b45ddc96"));

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("c370b9e1-857e-4037-bbf4-43752a965a6c"));

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("ce1c6a6c-c656-4ec6-8f33-52f088af6a5d"));

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("f78b2a14-fe48-41c1-9e9c-48266b44affd"));

            migrationBuilder.DeleteData(
                table: "Items",
                keyColumn: "Id",
                keyValue: new Guid("0bf74594-7fa6-4e7f-82ca-5a24df3368a6"));

            migrationBuilder.DeleteData(
                table: "Items",
                keyColumn: "Id",
                keyValue: new Guid("6411d0c0-e592-4007-b55d-050042a89929"));

            migrationBuilder.DeleteData(
                table: "Items",
                keyColumn: "Id",
                keyValue: new Guid("9065d5eb-5671-4f18-858e-cee5ec3ede26"));

            migrationBuilder.DeleteData(
                table: "Items",
                keyColumn: "Id",
                keyValue: new Guid("aed4af47-3898-4ec1-b098-f0cb312c71f9"));

            migrationBuilder.DeleteData(
                table: "Items",
                keyColumn: "Id",
                keyValue: new Guid("f8b9deca-0835-434e-b8b2-f2c54731e8c5"));

            migrationBuilder.DeleteData(
                table: "Teachers",
                keyColumn: "Id",
                keyValue: new Guid("433e1f2f-b6db-4b3c-ab81-6da03575c7ba"));

            migrationBuilder.DeleteData(
                table: "Teachers",
                keyColumn: "Id",
                keyValue: new Guid("988e52ee-574d-4661-9e51-5e652e466928"));

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("075ce85c-f3d1-45c1-931d-ac3be6514c9c"));

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("3f721c0d-475e-4544-869a-53ba10f10bbd"));

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("3f8137d2-73c6-447c-b317-2004d639b8ca"));

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("45d7b3aa-c6a2-44ee-a9d8-c7f18e1db8d2"));

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("4c7f39d0-17b4-4ebe-8f3d-e81f300f0d8b"));

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("52dd452d-0178-4f21-a4a0-3224a2f50534"));

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("6659dd70-13f9-426e-b64b-dee3705a1a1f"));

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("7bc04053-3d3f-4d82-9822-e37e3aeda613"));

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("8b8e92c9-b084-466a-b73a-b8cbd2a69cfb"));

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("9d922d34-1875-4c6a-b477-0a172848879e"));

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("b2c0d79c-4424-48a3-afe1-bba5b4d08079"));

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("b34df533-7e58-43cb-9a63-c465e0e62a2b"));

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("c162c052-f4fc-4791-bab3-aefdfb42bc04"));

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("433e1f2f-b6db-4b3c-ab81-6da03575c7ba"));

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("988e52ee-574d-4661-9e51-5e652e466928"));

            migrationBuilder.DropColumn(
                name: "Status",
                table: "Items");

            migrationBuilder.InsertData(
                table: "Items",
                columns: new[] { "Id", "Barcode", "BarcodeImage", "Category", "Condition", "CreatedAt", "Description", "Image", "ImageMimeType", "ItemMake", "ItemModel", "ItemName", "ItemType", "SerialNumber", "UpdatedAt" },
                values: new object[,]
                {
                    { new Guid("05ed6e12-5eb6-41cf-b35e-60a5caa33e98"), null, null, "Electronics", "Good", new DateTime(2025, 10, 23, 6, 29, 39, 133, DateTimeKind.Utc).AddTicks(8099), "Tablet for digital art and design classes.", null, null, "Apple", "iPad Pro 12.9-inch", "Apple iPad Pro", "Tablet", "SN-TB-APL-005", new DateTime(2025, 10, 23, 6, 29, 39, 133, DateTimeKind.Utc).AddTicks(8101) },
                    { new Guid("52c44875-1e75-4d8e-8179-0c6121e2a5d3"), null, null, "MediaEquipment", "Good", new DateTime(2025, 10, 23, 6, 29, 39, 133, DateTimeKind.Utc).AddTicks(8085), "Full-frame mirrorless camera with 4K video.", null, null, "Canon", "EOS R6", "Canon EOS R6 Camera", "Camera", "SN-CM-CAN-003", new DateTime(2025, 10, 23, 6, 29, 39, 133, DateTimeKind.Utc).AddTicks(8087) },
                    { new Guid("a276a41a-70ea-4224-9357-4369eda33618"), null, null, "MediaEquipment", "New", new DateTime(2025, 10, 23, 6, 29, 39, 133, DateTimeKind.Utc).AddTicks(8078), "1080p projector for classroom presentations.", null, null, "Epson", "HC 2250", "Epson Home Cinema Projector", "Projector", "SN-PR-EPS-002", new DateTime(2025, 10, 23, 6, 29, 39, 133, DateTimeKind.Utc).AddTicks(8079) },
                    { new Guid("f44075e4-ac7c-4920-b2bf-26779f6e5cd2"), null, null, "MediaEquipment", "Good", new DateTime(2025, 10, 23, 6, 29, 39, 133, DateTimeKind.Utc).AddTicks(8093), "Dynamic microphone for vocal recording.", null, null, "Shure", "SM7B", "Shure SM7B Microphone", "Microphone", "SN-MC-SHR-004", new DateTime(2025, 10, 23, 6, 29, 39, 133, DateTimeKind.Utc).AddTicks(8094) },
                    { new Guid("fd51ee56-38e8-4711-841c-f5bbcc361af3"), null, null, "Electronics", "Good", new DateTime(2025, 10, 23, 6, 29, 39, 133, DateTimeKind.Utc).AddTicks(8068), "High-performance laptop for video editing.", null, null, "Dell", "XPS 15 9510", "Dell XPS 15 Laptop", "Laptop", "SN-LP-DELL-001", new DateTime(2025, 10, 23, 6, 29, 39, 133, DateTimeKind.Utc).AddTicks(8070) }
                });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "Email", "FirstName", "LastName", "MiddleName", "PasswordHash", "PhoneNumber", "Status", "UserRole", "Username" },
                values: new object[,]
                {
                    { new Guid("08c6f64d-f286-47dd-9d0c-00ab54a33fc3"), "john.doe@example.com", "John", "Doe", null, "$2a$11$Eqf3VLWysyqoKCwwluHBDeeTEc7q2HCsULu51jluRU0YWdXvsbv6a", "0912134567", "Offline", "Student", "johndoe" },
                    { new Guid("0d05cec5-e7ac-40d8-ba84-3e86705625f9"), "staff3@example.com", "Staff3", "Doe", null, "$2a$11$RoZ07bC95w0/Ekm5rk277O8/uC9Sqb.BqWauN5rL2U3jdyrQyedyi", "0998376543", "Offline", "Staff", "staff3" },
                    { new Guid("2315dbf4-f226-45bd-a110-fefc778286a8"), "teacher4@example.com", "Teacher4", "Smith", null, "$2a$11$LrNKfE0km6DKtJeBZX8jVuu89Kyvw0if3ahBVkVPne2zSm8R.5qE.", "0917423456", "Offline", "Teacher", "teacher4" },
                    { new Guid("28a911a2-812e-497d-9c9a-edcee261e06c"), "teacher5@example.com", "Teacher5", "Smith", null, "$2a$11$D526BE9UxxO/EYIT7wvHfeLWgWZMJrsmF3oRGOP1jcQKP3TOhTlMW", "0917523456", "Offline", "Teacher", "teacher5" },
                    { new Guid("33e38d82-7028-4891-bcec-6fe60f48cdc6"), "bob.brown@example.com", "Bob", "Brown", null, "$2a$11$PqYZ9lxdm19N6SxK/rQ1IuVQvFH1L11VOEHzHLtkVdW2hDjT1RFWy", "0917223456", "Offline", "Teacher", "bobbrown" },
                    { new Guid("3d5fb3ce-08f8-485c-824b-f2b68c7a98b8"), "peter.jones@example.com", "Peter", "Jones", null, "$2a$11$DCEx/Wfc7nxkMmnh62lBCuE0DB0.O8o9tvdJLMynNbxk9yK1HHGXi", "0912334567", "Offline", "Student", "peterjones" },
                    { new Guid("420df9ac-3b37-4c43-8318-4083025d1409"), "student4@example.com", "Student4", "Jones", null, "$2a$11$YT/AJVnzFXnxZtKUF/ZF4.gUViYY/bDtUNV06qt4l0nG9k8wlqlRi", "0912434567", "Offline", "Student", "student4" },
                    { new Guid("586a3811-312d-494d-82d4-0a13365c75ea"), "admin1@example.com", "Admin1", "User", null, "$2a$11$HQ.QHRHuyBlLillRKWEv8uhivHiLKHtjlHqFy.BOggkU6rkZirGge", null, "Offline", "Admin", "admin1" },
                    { new Guid("6b493779-85c1-4bdb-99d7-e6a796dcb07e"), "staff2@example.com", "Staff2", "Doe", null, "$2a$11$YQBezuR7kTQW2d2NZBTCiOtwN8uCkrn1jxbQPrY721eSow0x9f58i", "0998276543", "Offline", "Staff", "staff2" },
                    { new Guid("76637bfa-93a8-4eca-9c6b-17ff70a5fc47"), "admin3@example.com", "Admin3", "User", null, "$2a$11$4o5A853Gykgoz5BcoZRfLedv9jT1/4cy/dZDez.1WNDjl66cXHVjS", null, "Offline", "Admin", "admin3" },
                    { new Guid("784301f4-2faa-44f5-8de4-3fa534cd306f"), "superadmin@example.com", "Super", "Admin", null, "$2a$11$ipo62fdZkIcyczYNEF653O6ZNZw0hLRycNTEOpA7egCckjNV9Fvwy", null, "Offline", "SuperAdmin", "superadmin" },
                    { new Guid("8e37afc4-d422-4c2d-a54b-6244a6e3ebaa"), "staff5@example.com", "Staff5", "Doe", null, "$2a$11$.Dhg74MUauBFPwsvr4PFHOkmd39dbvB.9L8uOynQsp6UH6ijPc02q", "0998576543", "Offline", "Staff", "staff5" },
                    { new Guid("927a9650-a8bd-4e00-b1d3-c80d75e5fb2c"), "staff4@example.com", "Staff4", "Doe", null, "$2a$11$gQqSNGB5QKriRvzGJLTU1.rG2JzlUjoNFXSPsE7NJk6LiCvOmEYG6", "0998476543", "Offline", "Staff", "staff4" },
                    { new Guid("93884b89-9910-440a-be14-89180ce5336e"), "staff1@example.com", "Staff1", "Doe", null, "$2a$11$5MLZz1FbVQoxryOyvdIqHO9HVV.dPqRggVmyT1gM6CGCbDyCc.Ty6", "0998176543", "Offline", "Staff", "staff1" },
                    { new Guid("98e0c707-9383-484c-bb66-31a0823b0936"), "alice.williams@example.com", "Alice", "Williams", null, "$2a$11$niVcueTBgpWJ7X9GvaVyjePqaT1ahsntcLdEW0zJYRefOlOjWZ/RS", "0917123456", "Offline", "Teacher", "alicewilliams" },
                    { new Guid("b1f64a63-6509-4f8b-aa0d-c3e0136f38ad"), "teacher3@example.com", "Teacher3", "Smith", null, "$2a$11$aK.YIjQYecPweNQAtPEO8eAjgxWzaQ7hyjaOESGnSmDf1BTus3Zpm", "0917323456", "Offline", "Teacher", "teacher3" },
                    { new Guid("bf82ec20-545a-4caa-92a4-32e4172abf87"), "jane.smith@example.com", "Jane", "Smith", null, "$2a$11$vgQsatKX8scKa.4UWJFUj.3dKQg38QuNo7fLj.DIzRQYIR9q94.Re", "0912234567", "Offline", "Student", "janesmith" },
                    { new Guid("c0d30f61-9b9f-4425-b0c9-d51e7fe87b5c"), "student5@example.com", "Student5", "Jones", null, "$2a$11$itLBZ0qMRRRo/uFVXxyWpOYe0crzCDYZirE/yFGxW/uthJTAMMzxi", "0912534567", "Offline", "Student", "student5" },
                    { new Guid("d5db98c4-5556-4587-b12e-f58cc6193358"), "admin4@example.com", "Admin4", "User", null, "$2a$11$DVBtZSabd1l55eiXiEERX.NR735r2.GVKdQfLDglx.c/iZ4wQYMBO", null, "Offline", "Admin", "admin4" },
                    { new Guid("df2e3eca-0646-4dd3-96bb-520f87268676"), "admin2@example.com", "Admin2", "User", null, "$2a$11$hzpHtQSZUrE90tvP8a3TUOnWSapaqspB/XO993De.UaWliGdSIXfm", null, "Offline", "Admin", "admin2" },
                    { new Guid("e0cb2e0e-2d55-47cc-987e-e220339a7f3b"), "admin5@example.com", "Admin5", "User", null, "$2a$11$D0GxbXsMbfWL3/Lh0w6YG.Cp/589Wdxf2bGfArheuWdQWa7q78iY.", null, "Offline", "Admin", "admin5" }
                });

            migrationBuilder.InsertData(
                table: "LentItems",
                columns: new[] { "Id", "Barcode", "BarcodeImage", "BorrowerFullName", "BorrowerRole", "IsHiddenFromUser", "ItemId", "ItemName", "LentAt", "Remarks", "ReturnedAt", "Room", "Status", "StudentIdNumber", "SubjectTimeSchedule", "TeacherFullName", "TeacherId", "UserId" },
                values: new object[,]
                {
                    { new Guid("606c96be-6019-4f1f-9f9c-f2eb0f1bbfdc"), null, null, "Alice Williams", "Teacher", false, new Guid("f44075e4-ac7c-4920-b2bf-26779f6e5cd2"), "Shure SM7B Microphone", new DateTime(2025, 10, 20, 6, 29, 39, 133, DateTimeKind.Utc).AddTicks(8966), null, null, "", "Borrowed", null, "", "", null, new Guid("98e0c707-9383-484c-bb66-31a0823b0936") },
                    { new Guid("d7ef5226-6204-4cc3-936a-21e70f72b92b"), null, null, "Bob Brown", "Teacher", false, new Guid("05ed6e12-5eb6-41cf-b35e-60a5caa33e98"), "Apple iPad Pro", new DateTime(2025, 9, 23, 6, 29, 39, 133, DateTimeKind.Utc).AddTicks(8973), null, new DateTime(2025, 10, 8, 6, 29, 39, 133, DateTimeKind.Utc).AddTicks(8974), "", "Returned", null, "", "", null, new Guid("33e38d82-7028-4891-bcec-6fe60f48cdc6") }
                });

            migrationBuilder.InsertData(
                table: "Staff",
                columns: new[] { "Id", "Position" },
                values: new object[,]
                {
                    { new Guid("0d05cec5-e7ac-40d8-ba84-3e86705625f9"), "Lab Technician" },
                    { new Guid("6b493779-85c1-4bdb-99d7-e6a796dcb07e"), "Lab Technician" },
                    { new Guid("8e37afc4-d422-4c2d-a54b-6244a6e3ebaa"), "Lab Technician" },
                    { new Guid("927a9650-a8bd-4e00-b1d3-c80d75e5fb2c"), "Lab Technician" },
                    { new Guid("93884b89-9910-440a-be14-89180ce5336e"), "Lab Technician" }
                });

            migrationBuilder.InsertData(
                table: "Students",
                columns: new[] { "Id", "BackStudentIdPicture", "CityMunicipality", "Course", "FrontStudentIdPicture", "PostalCode", "ProfilePicture", "Province", "Section", "Street", "StudentIdNumber", "Year" },
                values: new object[,]
                {
                    { new Guid("08c6f64d-f286-47dd-9d0c-00ab54a33fc3"), null, "", "Computer Science", null, "", null, "", "A", "", "S2021-001", "3" },
                    { new Guid("3d5fb3ce-08f8-485c-824b-f2b68c7a98b8"), null, "", "Computer Science", null, "", null, "", "A", "", "S2021-003", "3" },
                    { new Guid("420df9ac-3b37-4c43-8318-4083025d1409"), null, "", "Computer Science", null, "", null, "", "A", "", "2023-0004", "3" },
                    { new Guid("bf82ec20-545a-4caa-92a4-32e4172abf87"), null, "", "Computer Science", null, "", null, "", "A", "", "S2021-002", "3" },
                    { new Guid("c0d30f61-9b9f-4425-b0c9-d51e7fe87b5c"), null, "", "Computer Science", null, "", null, "", "A", "", "2023-0005", "3" }
                });

            migrationBuilder.InsertData(
                table: "Teachers",
                columns: new[] { "Id", "Department" },
                values: new object[,]
                {
                    { new Guid("2315dbf4-f226-45bd-a110-fefc778286a8"), "Information Technology" },
                    { new Guid("28a911a2-812e-497d-9c9a-edcee261e06c"), "Information Technology" },
                    { new Guid("33e38d82-7028-4891-bcec-6fe60f48cdc6"), "Information Technology" },
                    { new Guid("98e0c707-9383-484c-bb66-31a0823b0936"), "Information Technology" },
                    { new Guid("b1f64a63-6509-4f8b-aa0d-c3e0136f38ad"), "Information Technology" }
                });

            migrationBuilder.InsertData(
                table: "LentItems",
                columns: new[] { "Id", "Barcode", "BarcodeImage", "BorrowerFullName", "BorrowerRole", "IsHiddenFromUser", "ItemId", "ItemName", "LentAt", "Remarks", "ReturnedAt", "Room", "Status", "StudentIdNumber", "SubjectTimeSchedule", "TeacherFullName", "TeacherId", "UserId" },
                values: new object[,]
                {
                    { new Guid("753ddf13-a470-4d54-924c-46b6eb22a7f6"), null, null, "Jane Smith", "Student", false, new Guid("52c44875-1e75-4d8e-8179-0c6121e2a5d3"), "Canon EOS R6 Camera", new DateTime(2025, 10, 13, 6, 29, 39, 133, DateTimeKind.Utc).AddTicks(8944), null, new DateTime(2025, 10, 21, 6, 29, 39, 133, DateTimeKind.Utc).AddTicks(8945), "", "Returned", "S2021-002", "", "Bob Brown", new Guid("33e38d82-7028-4891-bcec-6fe60f48cdc6"), new Guid("bf82ec20-545a-4caa-92a4-32e4172abf87") },
                    { new Guid("c7aef9c8-6fac-4fa0-90a1-2ebc0f7d44f2"), null, null, "Peter Jones", "Student", false, new Guid("a276a41a-70ea-4224-9357-4369eda33618"), "Epson Home Cinema Projector", new DateTime(2025, 10, 22, 6, 29, 39, 133, DateTimeKind.Utc).AddTicks(8959), null, null, "", "Borrowed", "S2021-003", "", "Alice Williams", new Guid("98e0c707-9383-484c-bb66-31a0823b0936"), new Guid("3d5fb3ce-08f8-485c-824b-f2b68c7a98b8") },
                    { new Guid("ecbedfef-95b4-4ae7-bd48-b19e9e877bcc"), null, null, "John Doe", "Student", false, new Guid("fd51ee56-38e8-4711-841c-f5bbcc361af3"), "Dell XPS 15 Laptop", new DateTime(2025, 10, 18, 6, 29, 39, 133, DateTimeKind.Utc).AddTicks(8908), null, null, "", "Borrowed", "S2021-001", "", "Alice Williams", new Guid("98e0c707-9383-484c-bb66-31a0823b0936"), new Guid("08c6f64d-f286-47dd-9d0c-00ab54a33fc3") }
                });
        }
    }
}
