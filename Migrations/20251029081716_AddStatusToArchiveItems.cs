using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace BackendTechnicalAssetsManagement.Migrations
{
    /// <inheritdoc />
    public partial class AddStatusToArchiveItems : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
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

            migrationBuilder.AddColumn<string>(
                name: "Status",
                table: "ArchiveItems",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.InsertData(
                table: "Items",
                columns: new[] { "Id", "Barcode", "BarcodeImage", "Category", "Condition", "CreatedAt", "Description", "Image", "ImageMimeType", "ItemMake", "ItemModel", "ItemName", "ItemType", "SerialNumber", "Status", "UpdatedAt" },
                values: new object[,]
                {
                    { new Guid("0aa32eec-ccae-4bdf-b196-ba61592a40e0"), null, null, "Electronics", "Good", new DateTime(2025, 10, 29, 8, 17, 16, 282, DateTimeKind.Utc).AddTicks(7377), "High-performance laptop for video editing.", null, null, "Dell", "XPS 15 9510", "Dell XPS 15 Laptop", "Laptop", "SN-LP-DELL-001", "Available", new DateTime(2025, 10, 29, 8, 17, 16, 282, DateTimeKind.Utc).AddTicks(7377) },
                    { new Guid("3a362320-4f1c-4e1e-9663-8dcf00a420be"), null, null, "Electronics", "Good", new DateTime(2025, 10, 29, 8, 17, 16, 282, DateTimeKind.Utc).AddTicks(7404), "Tablet for digital art and design classes.", null, null, "Apple", "iPad Pro 12.9-inch", "Apple iPad Pro", "Tablet", "SN-TB-APL-005", "Available", new DateTime(2025, 10, 29, 8, 17, 16, 282, DateTimeKind.Utc).AddTicks(7405) },
                    { new Guid("4297ba1a-bfa5-4797-bd20-338968e98cbc"), null, null, "MediaEquipment", "Good", new DateTime(2025, 10, 29, 8, 17, 16, 282, DateTimeKind.Utc).AddTicks(7401), "Dynamic microphone for vocal recording.", null, null, "Shure", "SM7B", "Shure SM7B Microphone", "Microphone", "SN-MC-SHR-004", "Available", new DateTime(2025, 10, 29, 8, 17, 16, 282, DateTimeKind.Utc).AddTicks(7401) },
                    { new Guid("824b66e7-c87d-40ea-8817-aa68fc511f34"), null, null, "MediaEquipment", "New", new DateTime(2025, 10, 29, 8, 17, 16, 282, DateTimeKind.Utc).AddTicks(7391), "1080p projector for classroom presentations.", null, null, "Epson", "HC 2250", "Epson Home Cinema Projector", "Projector", "SN-PR-EPS-002", "Available", new DateTime(2025, 10, 29, 8, 17, 16, 282, DateTimeKind.Utc).AddTicks(7392) },
                    { new Guid("aed5cfb0-c7ed-4605-8da7-9fe0844ebe18"), null, null, "MediaEquipment", "Good", new DateTime(2025, 10, 29, 8, 17, 16, 282, DateTimeKind.Utc).AddTicks(7397), "Full-frame mirrorless camera with 4K video.", null, null, "Canon", "EOS R6", "Canon EOS R6 Camera", "Camera", "SN-CM-CAN-003", "Available", new DateTime(2025, 10, 29, 8, 17, 16, 282, DateTimeKind.Utc).AddTicks(7398) }
                });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "Email", "FirstName", "LastName", "MiddleName", "PasswordHash", "PhoneNumber", "Status", "UserRole", "Username" },
                values: new object[,]
                {
                    { new Guid("096ec110-8aac-423b-ba61-84e7c7d7f223"), "admin4@example.com", "Admin4", "User", null, "$2a$11$oEgWWQN3T8qCkIb5q7Q3/e4UL/xt52lhDUiB0IiN80LojbTUShNw6", null, "Offline", "Admin", "admin4" },
                    { new Guid("0c54d6dc-fc42-4e58-b91b-a9d698ca0c45"), "admin5@example.com", "Admin5", "User", null, "$2a$11$05Vg5.d1zlkhSQEQvRiT7e5ExStubgyDt/YyW4YjPRx1Iy5ITCHvm", null, "Offline", "Admin", "admin5" },
                    { new Guid("1bbca5bc-adec-417a-aea2-0b6046fccb08"), "student4@example.com", "Student4", "Jones", null, "$2a$11$LD81DYBJ.hlzJOExa4U5cO2TjRiU5BDqnmKkxHetfXEgRCUXGIGHK", "0912434567", "Offline", "Student", "student4" },
                    { new Guid("2566b03b-481f-4218-a816-b160e2246b7b"), "teacher4@example.com", "Teacher4", "Smith", null, "$2a$11$kYQ6zeRIXuqllK6S8ezEcehQfJBhOLyrDu/p4dwJfwBIxn8rU5tEq", "0917423456", "Offline", "Teacher", "teacher4" },
                    { new Guid("34f28ca7-2fd2-474f-b79b-1138d9b3b9ed"), "admin2@example.com", "Admin2", "User", null, "$2a$11$MgWkYfE4yFAksG8axn29guSkbBLaLHBgBOkeyUvcT.OPMlXsV0h5.", null, "Offline", "Admin", "admin2" },
                    { new Guid("39ed3dca-ed06-40da-9218-d9f7e74d2c1b"), "bob.brown@example.com", "Bob", "Brown", null, "$2a$11$E..P1oE9qtd3yILx69FMx.NU.fMTV48jLJGfKYWDwLR7fj/gFjhRa", "0917223456", "Offline", "Teacher", "bobbrown" },
                    { new Guid("472077a8-57f9-4055-8484-2a991ec2474f"), "staff1@example.com", "Staff1", "Doe", null, "$2a$11$lk9L9pL/wd3OGjly.isF1.hIwwKFln7q/smAqp9AeVzUX8Ux/I5XK", "0998176543", "Offline", "Staff", "staff1" },
                    { new Guid("5545c15d-4c37-4e8c-a7b4-433c4e2296b1"), "jane.smith@example.com", "Jane", "Smith", null, "$2a$11$2Lcl8ZlwDzGoSfWfziD6H.G07V8iz8o3verDvgRwzKDyDoXc7vMUe", "0912234567", "Offline", "Student", "janesmith" },
                    { new Guid("575b93d2-f029-484c-a93c-14cedde82491"), "admin3@example.com", "Admin3", "User", null, "$2a$11$4xjkOJmUkrX/YfDxX5bWDu3aFOwRBdm548JPDwHSlT323Efv09cR6", null, "Offline", "Admin", "admin3" },
                    { new Guid("59c31cfe-094e-4610-87e1-b21b45710e22"), "teacher5@example.com", "Teacher5", "Smith", null, "$2a$11$A.hVMV0jQzNDcmaKlJGtg./rLiH5blZIVnIsxiX8pyGz/xIj/naby", "0917523456", "Offline", "Teacher", "teacher5" },
                    { new Guid("5efc9101-d4e8-4fd2-b596-931cb122d508"), "staff3@example.com", "Staff3", "Doe", null, "$2a$11$gF9o.3PH5AUUlZ2Duo3iausstjJD1.ypMgSCLOaVV.KRSHIJh1GLy", "0998376543", "Offline", "Staff", "staff3" },
                    { new Guid("761c0790-26ad-4831-acac-4482b7556c72"), "staff2@example.com", "Staff2", "Doe", null, "$2a$11$qGF17gV8OBDSXCreyj3XlONRAYAfzlEL3VzLE4B2V2V5w7kHV14o6", "0998276543", "Offline", "Staff", "staff2" },
                    { new Guid("8ae7fa41-e6b5-419c-808b-38939bc44bdc"), "staff5@example.com", "Staff5", "Doe", null, "$2a$11$epNS/vX2DO/1ZlHt3raAdeK3E1y5UzvRHNVxdDuw8LIYPOAz5eQEi", "0998576543", "Offline", "Staff", "staff5" },
                    { new Guid("8ea9bc1f-5958-4116-a9b1-f95a84247d70"), "john.doe@example.com", "John", "Doe", null, "$2a$11$OFmMAx4xfCuts1DEM3nWiOULMma3nt669xmWdjsj2oLW8cnWkfDJy", "0912134567", "Offline", "Student", "johndoe" },
                    { new Guid("97621ca9-3be8-42b2-8906-5fc04bfcc2ac"), "staff4@example.com", "Staff4", "Doe", null, "$2a$11$GjjLdw.GpdM1zp7XCkdHpOVtHMIttoPYcnVoIcEh3DfDXKw350t/6", "0998476543", "Offline", "Staff", "staff4" },
                    { new Guid("abc42ee5-6dc9-40b3-b0a7-b34dcea136dc"), "student5@example.com", "Student5", "Jones", null, "$2a$11$TLDKTh5LDvOBAXH6ky1.xucDCOPMlhLreM8mhPNaEcf6jVdUpN5ly", "0912534567", "Offline", "Student", "student5" },
                    { new Guid("b460ef0f-b121-4254-adc3-fe4b61e2069b"), "alice.williams@example.com", "Alice", "Williams", null, "$2a$11$qn/qfQ1vsykrAMN0BTMji.npSBt4HfCnpl1eMlaPQrx0xIyQ4n.l6", "0917123456", "Offline", "Teacher", "alicewilliams" },
                    { new Guid("b98d57dd-6c71-4041-be81-2e3496c109c8"), "teacher3@example.com", "Teacher3", "Smith", null, "$2a$11$0mtEUQNgzH9Fons9jBT/bOCBY02LQygTijXZ1oSeIbOf/pDbp9hWW", "0917323456", "Offline", "Teacher", "teacher3" },
                    { new Guid("c38ac878-fd3e-4e0e-98b7-e6e9633332d8"), "peter.jones@example.com", "Peter", "Jones", null, "$2a$11$/ZJ1qZWJAC2amV/RP/WFh.kpyymcfWCtjAVBKSo20XwiX7Q.7e0HK", "0912334567", "Offline", "Student", "peterjones" },
                    { new Guid("cbb71cad-8415-47e4-ad38-e82ec08d848e"), "admin1@example.com", "Admin1", "User", null, "$2a$11$0chqINaXyJ7iBoN04u6o3.Z6l3aAMNStYxs4NjFL31tvLO9YwKIX2", null, "Offline", "Admin", "admin1" },
                    { new Guid("f672bcd6-edbc-4f30-93a8-13e7212c8cb0"), "superadmin@example.com", "Super", "Admin", null, "$2a$11$y/VkxEowl/GwGC2rfwFdO.OrF3mCw82CsqWLDeiWvS6zbEPIr6ltS", null, "Offline", "SuperAdmin", "superadmin" }
                });

            migrationBuilder.InsertData(
                table: "LentItems",
                columns: new[] { "Id", "Barcode", "BarcodeImage", "BorrowerFullName", "BorrowerRole", "IsHiddenFromUser", "ItemId", "ItemName", "LentAt", "Remarks", "ReturnedAt", "Room", "Status", "StudentIdNumber", "SubjectTimeSchedule", "TeacherFullName", "TeacherId", "UserId" },
                values: new object[,]
                {
                    { new Guid("3659fa18-8fd9-4f81-be2f-56f43abc0f64"), null, null, "Bob Brown", "Teacher", false, new Guid("3a362320-4f1c-4e1e-9663-8dcf00a420be"), "Apple iPad Pro", new DateTime(2025, 9, 29, 8, 17, 16, 282, DateTimeKind.Utc).AddTicks(7922), null, new DateTime(2025, 10, 14, 8, 17, 16, 282, DateTimeKind.Utc).AddTicks(7923), "", "Returned", null, "", "", null, new Guid("39ed3dca-ed06-40da-9218-d9f7e74d2c1b") },
                    { new Guid("96983a71-0078-4344-bab7-38f3c26e539f"), null, null, "Alice Williams", "Teacher", false, new Guid("4297ba1a-bfa5-4797-bd20-338968e98cbc"), "Shure SM7B Microphone", new DateTime(2025, 10, 26, 8, 17, 16, 282, DateTimeKind.Utc).AddTicks(7911), null, null, "", "Borrowed", null, "", "", null, new Guid("b460ef0f-b121-4254-adc3-fe4b61e2069b") }
                });

            migrationBuilder.InsertData(
                table: "Staff",
                columns: new[] { "Id", "Position" },
                values: new object[,]
                {
                    { new Guid("472077a8-57f9-4055-8484-2a991ec2474f"), "Lab Technician" },
                    { new Guid("5efc9101-d4e8-4fd2-b596-931cb122d508"), "Lab Technician" },
                    { new Guid("761c0790-26ad-4831-acac-4482b7556c72"), "Lab Technician" },
                    { new Guid("8ae7fa41-e6b5-419c-808b-38939bc44bdc"), "Lab Technician" },
                    { new Guid("97621ca9-3be8-42b2-8906-5fc04bfcc2ac"), "Lab Technician" }
                });

            migrationBuilder.InsertData(
                table: "Students",
                columns: new[] { "Id", "BackStudentIdPicture", "CityMunicipality", "Course", "FrontStudentIdPicture", "PostalCode", "ProfilePicture", "Province", "Section", "Street", "StudentIdNumber", "Year" },
                values: new object[,]
                {
                    { new Guid("1bbca5bc-adec-417a-aea2-0b6046fccb08"), null, "", "Computer Science", null, "", null, "", "A", "", "2023-0004", "3" },
                    { new Guid("5545c15d-4c37-4e8c-a7b4-433c4e2296b1"), null, "", "Computer Science", null, "", null, "", "A", "", "S2021-002", "3" },
                    { new Guid("8ea9bc1f-5958-4116-a9b1-f95a84247d70"), null, "", "Computer Science", null, "", null, "", "A", "", "S2021-001", "3" },
                    { new Guid("abc42ee5-6dc9-40b3-b0a7-b34dcea136dc"), null, "", "Computer Science", null, "", null, "", "A", "", "2023-0005", "3" },
                    { new Guid("c38ac878-fd3e-4e0e-98b7-e6e9633332d8"), null, "", "Computer Science", null, "", null, "", "A", "", "S2021-003", "3" }
                });

            migrationBuilder.InsertData(
                table: "Teachers",
                columns: new[] { "Id", "Department" },
                values: new object[,]
                {
                    { new Guid("2566b03b-481f-4218-a816-b160e2246b7b"), "Information Technology" },
                    { new Guid("39ed3dca-ed06-40da-9218-d9f7e74d2c1b"), "Information Technology" },
                    { new Guid("59c31cfe-094e-4610-87e1-b21b45710e22"), "Information Technology" },
                    { new Guid("b460ef0f-b121-4254-adc3-fe4b61e2069b"), "Information Technology" },
                    { new Guid("b98d57dd-6c71-4041-be81-2e3496c109c8"), "Information Technology" }
                });

            migrationBuilder.InsertData(
                table: "LentItems",
                columns: new[] { "Id", "Barcode", "BarcodeImage", "BorrowerFullName", "BorrowerRole", "IsHiddenFromUser", "ItemId", "ItemName", "LentAt", "Remarks", "ReturnedAt", "Room", "Status", "StudentIdNumber", "SubjectTimeSchedule", "TeacherFullName", "TeacherId", "UserId" },
                values: new object[,]
                {
                    { new Guid("49c32820-b057-464d-b458-9af448cd935e"), null, null, "John Doe", "Student", false, new Guid("0aa32eec-ccae-4bdf-b196-ba61592a40e0"), "Dell XPS 15 Laptop", new DateTime(2025, 10, 24, 8, 17, 16, 282, DateTimeKind.Utc).AddTicks(7878), null, null, "", "Borrowed", "S2021-001", "", "Alice Williams", new Guid("b460ef0f-b121-4254-adc3-fe4b61e2069b"), new Guid("8ea9bc1f-5958-4116-a9b1-f95a84247d70") },
                    { new Guid("c49d4c58-a4c5-4c99-b007-41dda86c7b9d"), null, null, "Peter Jones", "Student", false, new Guid("824b66e7-c87d-40ea-8817-aa68fc511f34"), "Epson Home Cinema Projector", new DateTime(2025, 10, 28, 8, 17, 16, 282, DateTimeKind.Utc).AddTicks(7906), null, null, "", "Borrowed", "S2021-003", "", "Alice Williams", new Guid("b460ef0f-b121-4254-adc3-fe4b61e2069b"), new Guid("c38ac878-fd3e-4e0e-98b7-e6e9633332d8") },
                    { new Guid("e8e98c2c-6001-4e51-a191-2268f7a31b76"), null, null, "Jane Smith", "Student", false, new Guid("aed5cfb0-c7ed-4605-8da7-9fe0844ebe18"), "Canon EOS R6 Camera", new DateTime(2025, 10, 19, 8, 17, 16, 282, DateTimeKind.Utc).AddTicks(7892), null, new DateTime(2025, 10, 27, 8, 17, 16, 282, DateTimeKind.Utc).AddTicks(7892), "", "Returned", "S2021-002", "", "Bob Brown", new Guid("39ed3dca-ed06-40da-9218-d9f7e74d2c1b"), new Guid("5545c15d-4c37-4e8c-a7b4-433c4e2296b1") }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "LentItems",
                keyColumn: "Id",
                keyValue: new Guid("3659fa18-8fd9-4f81-be2f-56f43abc0f64"));

            migrationBuilder.DeleteData(
                table: "LentItems",
                keyColumn: "Id",
                keyValue: new Guid("49c32820-b057-464d-b458-9af448cd935e"));

            migrationBuilder.DeleteData(
                table: "LentItems",
                keyColumn: "Id",
                keyValue: new Guid("96983a71-0078-4344-bab7-38f3c26e539f"));

            migrationBuilder.DeleteData(
                table: "LentItems",
                keyColumn: "Id",
                keyValue: new Guid("c49d4c58-a4c5-4c99-b007-41dda86c7b9d"));

            migrationBuilder.DeleteData(
                table: "LentItems",
                keyColumn: "Id",
                keyValue: new Guid("e8e98c2c-6001-4e51-a191-2268f7a31b76"));

            migrationBuilder.DeleteData(
                table: "Staff",
                keyColumn: "Id",
                keyValue: new Guid("472077a8-57f9-4055-8484-2a991ec2474f"));

            migrationBuilder.DeleteData(
                table: "Staff",
                keyColumn: "Id",
                keyValue: new Guid("5efc9101-d4e8-4fd2-b596-931cb122d508"));

            migrationBuilder.DeleteData(
                table: "Staff",
                keyColumn: "Id",
                keyValue: new Guid("761c0790-26ad-4831-acac-4482b7556c72"));

            migrationBuilder.DeleteData(
                table: "Staff",
                keyColumn: "Id",
                keyValue: new Guid("8ae7fa41-e6b5-419c-808b-38939bc44bdc"));

            migrationBuilder.DeleteData(
                table: "Staff",
                keyColumn: "Id",
                keyValue: new Guid("97621ca9-3be8-42b2-8906-5fc04bfcc2ac"));

            migrationBuilder.DeleteData(
                table: "Students",
                keyColumn: "Id",
                keyValue: new Guid("1bbca5bc-adec-417a-aea2-0b6046fccb08"));

            migrationBuilder.DeleteData(
                table: "Students",
                keyColumn: "Id",
                keyValue: new Guid("5545c15d-4c37-4e8c-a7b4-433c4e2296b1"));

            migrationBuilder.DeleteData(
                table: "Students",
                keyColumn: "Id",
                keyValue: new Guid("8ea9bc1f-5958-4116-a9b1-f95a84247d70"));

            migrationBuilder.DeleteData(
                table: "Students",
                keyColumn: "Id",
                keyValue: new Guid("abc42ee5-6dc9-40b3-b0a7-b34dcea136dc"));

            migrationBuilder.DeleteData(
                table: "Students",
                keyColumn: "Id",
                keyValue: new Guid("c38ac878-fd3e-4e0e-98b7-e6e9633332d8"));

            migrationBuilder.DeleteData(
                table: "Teachers",
                keyColumn: "Id",
                keyValue: new Guid("2566b03b-481f-4218-a816-b160e2246b7b"));

            migrationBuilder.DeleteData(
                table: "Teachers",
                keyColumn: "Id",
                keyValue: new Guid("59c31cfe-094e-4610-87e1-b21b45710e22"));

            migrationBuilder.DeleteData(
                table: "Teachers",
                keyColumn: "Id",
                keyValue: new Guid("b98d57dd-6c71-4041-be81-2e3496c109c8"));

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("096ec110-8aac-423b-ba61-84e7c7d7f223"));

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("0c54d6dc-fc42-4e58-b91b-a9d698ca0c45"));

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("34f28ca7-2fd2-474f-b79b-1138d9b3b9ed"));

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("575b93d2-f029-484c-a93c-14cedde82491"));

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("cbb71cad-8415-47e4-ad38-e82ec08d848e"));

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("f672bcd6-edbc-4f30-93a8-13e7212c8cb0"));

            migrationBuilder.DeleteData(
                table: "Items",
                keyColumn: "Id",
                keyValue: new Guid("0aa32eec-ccae-4bdf-b196-ba61592a40e0"));

            migrationBuilder.DeleteData(
                table: "Items",
                keyColumn: "Id",
                keyValue: new Guid("3a362320-4f1c-4e1e-9663-8dcf00a420be"));

            migrationBuilder.DeleteData(
                table: "Items",
                keyColumn: "Id",
                keyValue: new Guid("4297ba1a-bfa5-4797-bd20-338968e98cbc"));

            migrationBuilder.DeleteData(
                table: "Items",
                keyColumn: "Id",
                keyValue: new Guid("824b66e7-c87d-40ea-8817-aa68fc511f34"));

            migrationBuilder.DeleteData(
                table: "Items",
                keyColumn: "Id",
                keyValue: new Guid("aed5cfb0-c7ed-4605-8da7-9fe0844ebe18"));

            migrationBuilder.DeleteData(
                table: "Teachers",
                keyColumn: "Id",
                keyValue: new Guid("39ed3dca-ed06-40da-9218-d9f7e74d2c1b"));

            migrationBuilder.DeleteData(
                table: "Teachers",
                keyColumn: "Id",
                keyValue: new Guid("b460ef0f-b121-4254-adc3-fe4b61e2069b"));

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("1bbca5bc-adec-417a-aea2-0b6046fccb08"));

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("2566b03b-481f-4218-a816-b160e2246b7b"));

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("472077a8-57f9-4055-8484-2a991ec2474f"));

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("5545c15d-4c37-4e8c-a7b4-433c4e2296b1"));

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("59c31cfe-094e-4610-87e1-b21b45710e22"));

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("5efc9101-d4e8-4fd2-b596-931cb122d508"));

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("761c0790-26ad-4831-acac-4482b7556c72"));

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("8ae7fa41-e6b5-419c-808b-38939bc44bdc"));

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("8ea9bc1f-5958-4116-a9b1-f95a84247d70"));

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("97621ca9-3be8-42b2-8906-5fc04bfcc2ac"));

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("abc42ee5-6dc9-40b3-b0a7-b34dcea136dc"));

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("b98d57dd-6c71-4041-be81-2e3496c109c8"));

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("c38ac878-fd3e-4e0e-98b7-e6e9633332d8"));

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("39ed3dca-ed06-40da-9218-d9f7e74d2c1b"));

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("b460ef0f-b121-4254-adc3-fe4b61e2069b"));

            migrationBuilder.DropColumn(
                name: "Status",
                table: "ArchiveItems");

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
    }
}
