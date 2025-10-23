using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace BackendTechnicalAssetsManagement.Migrations
{
    /// <inheritdoc />
    public partial class addedItemsAndLentItemsSeeds : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Staff",
                keyColumn: "Id",
                keyValue: new Guid("3e0e2137-f559-4c4d-bfde-07376f6f4106"));

            migrationBuilder.DeleteData(
                table: "Staff",
                keyColumn: "Id",
                keyValue: new Guid("63ad9b40-5ea1-45a9-af13-5b605039102b"));

            migrationBuilder.DeleteData(
                table: "Staff",
                keyColumn: "Id",
                keyValue: new Guid("9721980c-d5df-4c3d-8364-f1ca34751c9b"));

            migrationBuilder.DeleteData(
                table: "Staff",
                keyColumn: "Id",
                keyValue: new Guid("a02b31f3-dec2-415b-8f4c-eb78f7975240"));

            migrationBuilder.DeleteData(
                table: "Staff",
                keyColumn: "Id",
                keyValue: new Guid("adc9eadd-3cce-4198-b96a-82cf861acc7f"));

            migrationBuilder.DeleteData(
                table: "Students",
                keyColumn: "Id",
                keyValue: new Guid("1a6660e2-4428-47c0-a1a6-65069d994383"));

            migrationBuilder.DeleteData(
                table: "Students",
                keyColumn: "Id",
                keyValue: new Guid("c3ee8e97-1393-4448-9580-111505ce902b"));

            migrationBuilder.DeleteData(
                table: "Students",
                keyColumn: "Id",
                keyValue: new Guid("de7d0e86-26eb-423f-a355-c6133bc00cc0"));

            migrationBuilder.DeleteData(
                table: "Students",
                keyColumn: "Id",
                keyValue: new Guid("eb61d1f4-edee-475e-a007-ab1999fb0e42"));

            migrationBuilder.DeleteData(
                table: "Students",
                keyColumn: "Id",
                keyValue: new Guid("f2974241-d89f-4369-9c4d-e001c98ba025"));

            migrationBuilder.DeleteData(
                table: "Teachers",
                keyColumn: "Id",
                keyValue: new Guid("008b540a-dbf1-4054-969a-f326edad2875"));

            migrationBuilder.DeleteData(
                table: "Teachers",
                keyColumn: "Id",
                keyValue: new Guid("13c06319-a102-493c-aee4-bfe34dec567a"));

            migrationBuilder.DeleteData(
                table: "Teachers",
                keyColumn: "Id",
                keyValue: new Guid("19a47ffe-cf0d-4ff2-93dc-36b200aada5d"));

            migrationBuilder.DeleteData(
                table: "Teachers",
                keyColumn: "Id",
                keyValue: new Guid("3fb46fb6-a5ae-405b-bcf9-16dae4f26edf"));

            migrationBuilder.DeleteData(
                table: "Teachers",
                keyColumn: "Id",
                keyValue: new Guid("ac7df1d5-bd01-4607-b021-eb25295294ef"));

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("1bce8c4d-3f07-4ba1-9313-21fcf8be3c95"));

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("34ffa072-78fc-40aa-b8a9-6317a621b47c"));

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("369131bb-eee4-43fd-9e5e-0144eabfecd6"));

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("5b8a219c-04cd-4fe9-9a65-4a70efb4f607"));

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("738c9e85-a89c-41bd-b80b-843bb764837c"));

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("85af73c6-1dfb-41c1-a40d-e00bd7b6eebd"));

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("008b540a-dbf1-4054-969a-f326edad2875"));

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("13c06319-a102-493c-aee4-bfe34dec567a"));

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("19a47ffe-cf0d-4ff2-93dc-36b200aada5d"));

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("1a6660e2-4428-47c0-a1a6-65069d994383"));

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("3e0e2137-f559-4c4d-bfde-07376f6f4106"));

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("3fb46fb6-a5ae-405b-bcf9-16dae4f26edf"));

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("63ad9b40-5ea1-45a9-af13-5b605039102b"));

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("9721980c-d5df-4c3d-8364-f1ca34751c9b"));

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("a02b31f3-dec2-415b-8f4c-eb78f7975240"));

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("ac7df1d5-bd01-4607-b021-eb25295294ef"));

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("adc9eadd-3cce-4198-b96a-82cf861acc7f"));

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("c3ee8e97-1393-4448-9580-111505ce902b"));

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("de7d0e86-26eb-423f-a355-c6133bc00cc0"));

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("eb61d1f4-edee-475e-a007-ab1999fb0e42"));

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("f2974241-d89f-4369-9c4d-e001c98ba025"));

            migrationBuilder.DropColumn(
                name: "LentItemId",
                table: "ArchiveLentItems");

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

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
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

            migrationBuilder.AddColumn<Guid>(
                name: "LentItemId",
                table: "ArchiveLentItems",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "Email", "FirstName", "LastName", "MiddleName", "PasswordHash", "PhoneNumber", "Status", "UserRole", "Username" },
                values: new object[,]
                {
                    { new Guid("008b540a-dbf1-4054-969a-f326edad2875"), "teacher3@example.com", "Teacher3", "Smith", null, "$2a$11$.7XGRCoEPe38K89aTuPdkeTHfppKpKuC7LiZUb34KWS4le6YKC3vW", "0917323456", "Offline", "Teacher", "teacher3" },
                    { new Guid("13c06319-a102-493c-aee4-bfe34dec567a"), "teacher4@example.com", "Teacher4", "Smith", null, "$2a$11$xXW.QuzPTfg7GlrilSiLPOiXeO.RC9O6FwkeSfteZs06sF05677Ku", "0917423456", "Offline", "Teacher", "teacher4" },
                    { new Guid("19a47ffe-cf0d-4ff2-93dc-36b200aada5d"), "teacher2@example.com", "Teacher2", "Smith", null, "$2a$11$VcCGZLkFv2Dm7p7Gajz.SOCZSKrF4IxA9x1hzl2Wd.1Kfx3/6tmCO", "0917223456", "Offline", "Teacher", "teacher2" },
                    { new Guid("1a6660e2-4428-47c0-a1a6-65069d994383"), "student5@example.com", "Student5", "Jones", null, "$2a$11$ikhsdTY34Tmm0tMn7fgPeeMs41SCdJY/WqOzfn9ABADLr9hlRPR1G", "0912534567", "Offline", "Student", "student5" },
                    { new Guid("1bce8c4d-3f07-4ba1-9313-21fcf8be3c95"), "admin3@example.com", "Admin3", "User", null, "$2a$11$0TMP.orPqEW/UhJl48mtJ..b5pPlRMIDD.OT2n9DJCk2KudXu8nSa", null, "Offline", "Admin", "admin3" },
                    { new Guid("34ffa072-78fc-40aa-b8a9-6317a621b47c"), "admin5@example.com", "Admin5", "User", null, "$2a$11$eoyWYrtj3/Ut.jASnIIgBucR4J5IiTusrwkMkZknRuxWOdh0YyIAC", null, "Offline", "Admin", "admin5" },
                    { new Guid("369131bb-eee4-43fd-9e5e-0144eabfecd6"), "superadmin@example.com", "Super", "Admin", null, "$2a$11$h7bK.ZZIfngDYXeOZPulFOuljSg/FIxLwUeMvQc9UfdAvzvg3xPv6", null, "Offline", "SuperAdmin", "superadmin" },
                    { new Guid("3e0e2137-f559-4c4d-bfde-07376f6f4106"), "staff2@example.com", "Staff2", "Doe", null, "$2a$11$D1AtYzkWwujQMzLRQ1wfgO7wJ895BmIs.0lYuYLQt1uI5Wdgqz.a2", "0998276543", "Offline", "Staff", "staff2" },
                    { new Guid("3fb46fb6-a5ae-405b-bcf9-16dae4f26edf"), "teacher1@example.com", "Teacher1", "Smith", null, "$2a$11$dzacivgDsXhmnvZlNlzqIuhAauyJVlsGVq2hmjK8kKTkPoITVhW4W", "0917123456", "Offline", "Teacher", "teacher1" },
                    { new Guid("5b8a219c-04cd-4fe9-9a65-4a70efb4f607"), "admin4@example.com", "Admin4", "User", null, "$2a$11$Z/fonb4dXIiTVfyFJDkV2ukZVfd.N5JdJV9ivLxMU00BTfPMLqFRK", null, "Offline", "Admin", "admin4" },
                    { new Guid("63ad9b40-5ea1-45a9-af13-5b605039102b"), "staff4@example.com", "Staff4", "Doe", null, "$2a$11$X25PsCtkAa8d1gkZ9GkDG.oy8nQosnyK3m.js/1X8muzy8coBbQ1y", "0998476543", "Offline", "Staff", "staff4" },
                    { new Guid("738c9e85-a89c-41bd-b80b-843bb764837c"), "admin2@example.com", "Admin2", "User", null, "$2a$11$sS3Sk.uA3mNjpbhxw/isJ.8/3ZfLcG6af0Sj3SDMP0qi4xJ0DMn0W", null, "Offline", "Admin", "admin2" },
                    { new Guid("85af73c6-1dfb-41c1-a40d-e00bd7b6eebd"), "admin1@example.com", "Admin1", "User", null, "$2a$11$kzZ1I4DHsy2yFtpQlpl6XuWudVobCx8r25C74tj.aCXn4RNAa7tOW", null, "Offline", "Admin", "admin1" },
                    { new Guid("9721980c-d5df-4c3d-8364-f1ca34751c9b"), "staff5@example.com", "Staff5", "Doe", null, "$2a$11$QznpRFeaU5CV0VeG6MJvn.njXbHdS8OKy6aHDL3/xbJw/bTFMgQ5e", "0998576543", "Offline", "Staff", "staff5" },
                    { new Guid("a02b31f3-dec2-415b-8f4c-eb78f7975240"), "staff3@example.com", "Staff3", "Doe", null, "$2a$11$cNOvVrTLSlUS5AYdKKicyuDme5pysJ0.uhV5KX8JYISMgKtzjwD4G", "0998376543", "Offline", "Staff", "staff3" },
                    { new Guid("ac7df1d5-bd01-4607-b021-eb25295294ef"), "teacher5@example.com", "Teacher5", "Smith", null, "$2a$11$3oA7j0PIgnDPQQKYf1W0eeuSo0mv/onJYQQwaa5sA6FDJJfRfMU16", "0917523456", "Offline", "Teacher", "teacher5" },
                    { new Guid("adc9eadd-3cce-4198-b96a-82cf861acc7f"), "staff1@example.com", "Staff1", "Doe", null, "$2a$11$2e28V4cY96gS14lxoIm/v.kh/3WlXdXTHa6Cd84QtmBz7PtfRNjpe", "0998176543", "Offline", "Staff", "staff1" },
                    { new Guid("c3ee8e97-1393-4448-9580-111505ce902b"), "student3@example.com", "Student3", "Jones", null, "$2a$11$Z4OcMH0/L0q5XhjjHDV8h.qAh002dfwk1jD8n0yI0AUaz6J/lgq0y", "0912334567", "Offline", "Student", "student3" },
                    { new Guid("de7d0e86-26eb-423f-a355-c6133bc00cc0"), "student4@example.com", "Student4", "Jones", null, "$2a$11$25aIO6llczo9sqw70xPQ7ek3eBJWJHvDOdYAF10Sl0HFzQQOWFMxK", "0912434567", "Offline", "Student", "student4" },
                    { new Guid("eb61d1f4-edee-475e-a007-ab1999fb0e42"), "student1@example.com", "Student1", "Jones", null, "$2a$11$qkG7jP1N2uzCgdVMs.sYq.9pRMeFnvoFWIc5jGtaz1FWZX54DHme6", "0912134567", "Offline", "Student", "student1" },
                    { new Guid("f2974241-d89f-4369-9c4d-e001c98ba025"), "student2@example.com", "Student2", "Jones", null, "$2a$11$cA1cDpa56DblWHPXmBqyxef94kWsJ0a7Mhg/S5pPftNyyCMekgbEC", "0912234567", "Offline", "Student", "student2" }
                });

            migrationBuilder.InsertData(
                table: "Staff",
                columns: new[] { "Id", "Position" },
                values: new object[,]
                {
                    { new Guid("3e0e2137-f559-4c4d-bfde-07376f6f4106"), "Lab Technician" },
                    { new Guid("63ad9b40-5ea1-45a9-af13-5b605039102b"), "Lab Technician" },
                    { new Guid("9721980c-d5df-4c3d-8364-f1ca34751c9b"), "Lab Technician" },
                    { new Guid("a02b31f3-dec2-415b-8f4c-eb78f7975240"), "Lab Technician" },
                    { new Guid("adc9eadd-3cce-4198-b96a-82cf861acc7f"), "Lab Technician" }
                });

            migrationBuilder.InsertData(
                table: "Students",
                columns: new[] { "Id", "BackStudentIdPicture", "CityMunicipality", "Course", "FrontStudentIdPicture", "PostalCode", "ProfilePicture", "Province", "Section", "Street", "StudentIdNumber", "Year" },
                values: new object[,]
                {
                    { new Guid("1a6660e2-4428-47c0-a1a6-65069d994383"), null, "Sample City", "Computer Science", null, "12345", null, "Sample Province", "A", "123 Main St #5", "2023-0005", "3" },
                    { new Guid("c3ee8e97-1393-4448-9580-111505ce902b"), null, "Sample City", "Computer Science", null, "12345", null, "Sample Province", "A", "123 Main St #3", "2023-0003", "3" },
                    { new Guid("de7d0e86-26eb-423f-a355-c6133bc00cc0"), null, "Sample City", "Computer Science", null, "12345", null, "Sample Province", "A", "123 Main St #4", "2023-0004", "3" },
                    { new Guid("eb61d1f4-edee-475e-a007-ab1999fb0e42"), null, "Sample City", "Computer Science", null, "12345", null, "Sample Province", "A", "123 Main St #1", "2023-0001", "3" },
                    { new Guid("f2974241-d89f-4369-9c4d-e001c98ba025"), null, "Sample City", "Computer Science", null, "12345", null, "Sample Province", "A", "123 Main St #2", "2023-0002", "3" }
                });

            migrationBuilder.InsertData(
                table: "Teachers",
                columns: new[] { "Id", "Department" },
                values: new object[,]
                {
                    { new Guid("008b540a-dbf1-4054-969a-f326edad2875"), "Information Technology" },
                    { new Guid("13c06319-a102-493c-aee4-bfe34dec567a"), "Information Technology" },
                    { new Guid("19a47ffe-cf0d-4ff2-93dc-36b200aada5d"), "Information Technology" },
                    { new Guid("3fb46fb6-a5ae-405b-bcf9-16dae4f26edf"), "Information Technology" },
                    { new Guid("ac7df1d5-bd01-4607-b021-eb25295294ef"), "Information Technology" }
                });
        }
    }
}
