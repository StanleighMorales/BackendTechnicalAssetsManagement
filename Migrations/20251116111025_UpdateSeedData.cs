using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace BackendTechnicalAssetsManagement.Migrations
{
    /// <inheritdoc />
    public partial class UpdateSeedData : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "LentItems",
                keyColumn: "Id",
                keyValue: new Guid("16096275-55ce-4b63-9df7-b117234a53e2"));

            migrationBuilder.DeleteData(
                table: "LentItems",
                keyColumn: "Id",
                keyValue: new Guid("233e8791-588f-4645-9e2d-22d0b5a7bfdc"));

            migrationBuilder.DeleteData(
                table: "LentItems",
                keyColumn: "Id",
                keyValue: new Guid("42554f4d-a6b9-4fb1-be12-4e37b7c54a5c"));

            migrationBuilder.DeleteData(
                table: "LentItems",
                keyColumn: "Id",
                keyValue: new Guid("a604b8e2-26aa-45ee-b586-2e72172edee6"));

            migrationBuilder.DeleteData(
                table: "LentItems",
                keyColumn: "Id",
                keyValue: new Guid("b184cf02-4ec0-4aba-9b14-184620f2044a"));

            migrationBuilder.DeleteData(
                table: "Staff",
                keyColumn: "Id",
                keyValue: new Guid("13bec163-b287-4e70-b68c-4b1c2e07fb30"));

            migrationBuilder.DeleteData(
                table: "Staff",
                keyColumn: "Id",
                keyValue: new Guid("5ac6a324-25fc-468b-9988-b9acd07c7982"));

            migrationBuilder.DeleteData(
                table: "Staff",
                keyColumn: "Id",
                keyValue: new Guid("67230c4b-d9f2-4be5-a9a3-92d5fab82a56"));

            migrationBuilder.DeleteData(
                table: "Staff",
                keyColumn: "Id",
                keyValue: new Guid("75e86d5e-10e4-4211-871d-06ef2266e3a9"));

            migrationBuilder.DeleteData(
                table: "Staff",
                keyColumn: "Id",
                keyValue: new Guid("dec461b7-7687-46ae-a1f6-5ffa64f501af"));

            migrationBuilder.DeleteData(
                table: "Students",
                keyColumn: "Id",
                keyValue: new Guid("1106d7a8-eb7b-42cf-8b64-74286fea84ba"));

            migrationBuilder.DeleteData(
                table: "Students",
                keyColumn: "Id",
                keyValue: new Guid("1e9106d3-73ec-4438-b08a-000eec5798f6"));

            migrationBuilder.DeleteData(
                table: "Students",
                keyColumn: "Id",
                keyValue: new Guid("70667c61-1f72-431c-a9be-c2208295fefb"));

            migrationBuilder.DeleteData(
                table: "Students",
                keyColumn: "Id",
                keyValue: new Guid("89f1142c-4425-4c99-a592-720a0bf785c5"));

            migrationBuilder.DeleteData(
                table: "Students",
                keyColumn: "Id",
                keyValue: new Guid("a402a76b-3b93-4b2d-a075-1ccbf3da568d"));

            migrationBuilder.DeleteData(
                table: "Teachers",
                keyColumn: "Id",
                keyValue: new Guid("8159e8a2-f024-403d-8dd0-d23e8c4ce083"));

            migrationBuilder.DeleteData(
                table: "Teachers",
                keyColumn: "Id",
                keyValue: new Guid("a45b3918-3621-4a3c-b6b3-9e05849d3c73"));

            migrationBuilder.DeleteData(
                table: "Teachers",
                keyColumn: "Id",
                keyValue: new Guid("d2db5d22-8988-4b7c-b9e7-d130d0e0886f"));

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("3bfb3538-d973-4edb-ae64-a82672f3ae0e"));

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("3d16bf60-b629-492e-8e43-9b80339d05e0"));

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("76a4a5d3-1913-4733-bf32-eeef7ac38d15"));

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("b8ca4420-e8f5-40c2-a3c2-b24c34d76e76"));

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("c1798bec-b230-4e6c-88d9-55daaa72e564"));

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("e46888af-9474-472d-b15c-b7a1a83b0da1"));

            migrationBuilder.DeleteData(
                table: "Items",
                keyColumn: "Id",
                keyValue: new Guid("1808a478-ee4c-41d4-bcd9-442969c5f037"));

            migrationBuilder.DeleteData(
                table: "Items",
                keyColumn: "Id",
                keyValue: new Guid("5cb4780d-812c-48cf-aa78-a03fd3a6a47c"));

            migrationBuilder.DeleteData(
                table: "Items",
                keyColumn: "Id",
                keyValue: new Guid("98007650-218a-4baf-b731-8cdbc76d6651"));

            migrationBuilder.DeleteData(
                table: "Items",
                keyColumn: "Id",
                keyValue: new Guid("abffe3b6-04fa-489c-ac3e-e9ae39b853b0"));

            migrationBuilder.DeleteData(
                table: "Items",
                keyColumn: "Id",
                keyValue: new Guid("b45c779f-4099-457d-9604-3e4cc6969948"));

            migrationBuilder.DeleteData(
                table: "Teachers",
                keyColumn: "Id",
                keyValue: new Guid("a3d80db9-70ba-4069-b505-5e6cc5358b5b"));

            migrationBuilder.DeleteData(
                table: "Teachers",
                keyColumn: "Id",
                keyValue: new Guid("b6d116ae-df63-4284-a0aa-2b1fc46b31ae"));

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("1106d7a8-eb7b-42cf-8b64-74286fea84ba"));

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("13bec163-b287-4e70-b68c-4b1c2e07fb30"));

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("1e9106d3-73ec-4438-b08a-000eec5798f6"));

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("5ac6a324-25fc-468b-9988-b9acd07c7982"));

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("67230c4b-d9f2-4be5-a9a3-92d5fab82a56"));

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("70667c61-1f72-431c-a9be-c2208295fefb"));

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("75e86d5e-10e4-4211-871d-06ef2266e3a9"));

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("8159e8a2-f024-403d-8dd0-d23e8c4ce083"));

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("89f1142c-4425-4c99-a592-720a0bf785c5"));

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("a402a76b-3b93-4b2d-a075-1ccbf3da568d"));

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("a45b3918-3621-4a3c-b6b3-9e05849d3c73"));

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("d2db5d22-8988-4b7c-b9e7-d130d0e0886f"));

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("dec461b7-7687-46ae-a1f6-5ffa64f501af"));

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("a3d80db9-70ba-4069-b505-5e6cc5358b5b"));

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("b6d116ae-df63-4284-a0aa-2b1fc46b31ae"));

            migrationBuilder.InsertData(
                table: "Items",
                columns: new[] { "Id", "Barcode", "BarcodeImage", "Category", "Condition", "CreatedAt", "Description", "Image", "ImageMimeType", "ItemMake", "ItemModel", "ItemName", "ItemType", "SerialNumber", "Status", "UpdatedAt" },
                values: new object[,]
                {
                    { new Guid("433d4153-802a-4889-a936-cc40152cf428"), "ITEM-SN-EXT-006", null, "Electronics", "Good", new DateTime(2025, 11, 16, 11, 10, 24, 58, DateTimeKind.Utc).AddTicks(9247), "15-foot extension cord with surge protection.", null, null, "Belkin", "Heavy Duty", "Extension Wire 15ft", "Extension Wire", "SN-EXT-006", "Available", new DateTime(2025, 11, 16, 11, 10, 24, 58, DateTimeKind.Utc).AddTicks(9247) },
                    { new Guid("50891c08-7605-4b4b-889e-7fd014a8f9c4"), "ITEM-SN-SPK-003", null, "MediaEquipment", "Good", new DateTime(2025, 11, 16, 11, 10, 24, 58, DateTimeKind.Utc).AddTicks(9247), "Portable speaker for classroom audio.", null, null, "JBL", "Flip 6", "Portable Bluetooth Speaker", "Speaker", "SN-SPK-003", "Available", new DateTime(2025, 11, 16, 11, 10, 24, 58, DateTimeKind.Utc).AddTicks(9247) },
                    { new Guid("5b027da2-8573-4da9-88ab-645fc5f1b71a"), "ITEM-SN-HDMI-007", null, "Electronics", "New", new DateTime(2025, 11, 16, 11, 10, 24, 58, DateTimeKind.Utc).AddTicks(9247), "6-foot HDMI cable for short connections.", null, null, "AmazonBasics", "HDMI 2.1", "HDMI Cable 6ft", "Cable", "SN-HDMI-007", "Available", new DateTime(2025, 11, 16, 11, 10, 24, 58, DateTimeKind.Utc).AddTicks(9247) },
                    { new Guid("66ebfa17-1c85-4dc2-9df4-a1651d4d517b"), "ITEM-SN-HDMI-001", null, "Electronics", "Good", new DateTime(2025, 11, 16, 11, 10, 24, 58, DateTimeKind.Utc).AddTicks(9247), "10-foot HDMI cable for display connections.", null, null, "Belkin", "HDMI 2.0", "HDMI Cable 10ft", "Cable", "SN-HDMI-001", "Unavailable", new DateTime(2025, 11, 16, 11, 10, 24, 58, DateTimeKind.Utc).AddTicks(9247) },
                    { new Guid("6a9fa638-3a30-4a3e-b6d6-5d7a462e7256"), "ITEM-SN-MIC-008", null, "MediaEquipment", "Good", new DateTime(2025, 11, 16, 11, 10, 24, 58, DateTimeKind.Utc).AddTicks(9247), "USB condenser microphone for recording.", null, null, "Blue", "Yeti", "USB Microphone", "Microphone", "SN-MIC-008", "Available", new DateTime(2025, 11, 16, 11, 10, 24, 58, DateTimeKind.Utc).AddTicks(9247) },
                    { new Guid("82ec5f2b-b7cd-4a39-a6a1-f733396afd4a"), "ITEM-SN-MIC-002", null, "MediaEquipment", "Good", new DateTime(2025, 11, 16, 11, 10, 24, 58, DateTimeKind.Utc).AddTicks(9247), "Wireless microphone for presentations.", null, null, "Shure", "WM-200", "Wireless Microphone", "Microphone", "SN-MIC-002", "Unavailable", new DateTime(2025, 11, 16, 11, 10, 24, 58, DateTimeKind.Utc).AddTicks(9247) },
                    { new Guid("9e6ee2b3-f65b-4eed-99d8-9487f4855a0f"), "ITEM-SN-MOUSE-004", null, "Electronics", "Good", new DateTime(2025, 11, 16, 11, 10, 24, 58, DateTimeKind.Utc).AddTicks(9247), "Ergonomic wireless mouse.", null, null, "Logitech", "MX Master 3", "Wireless Mouse", "Mouse", "SN-MOUSE-004", "Unavailable", new DateTime(2025, 11, 16, 11, 10, 24, 58, DateTimeKind.Utc).AddTicks(9247) },
                    { new Guid("b980bd3a-a23e-4e07-aa01-1da198d14f8d"), "ITEM-SN-KB-005", null, "Electronics", "Good", new DateTime(2025, 11, 16, 11, 10, 24, 58, DateTimeKind.Utc).AddTicks(9247), "Compact mechanical keyboard.", null, null, "Logitech", "K380", "Mechanical Keyboard", "Keyboard", "SN-KB-005", "Available", new DateTime(2025, 11, 16, 11, 10, 24, 58, DateTimeKind.Utc).AddTicks(9247) }
                });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "Email", "FirstName", "LastName", "MiddleName", "PasswordHash", "PhoneNumber", "Status", "UserRole", "Username" },
                values: new object[,]
                {
                    { new Guid("07d32fbb-bc4f-416d-8c25-2bd7f8147be3"), "staff1@example.com", "Staff1", "Doe", null, "$2a$11$oqWv6owuWypslmkBP6RHaOpiSM.majtq1GXTbLU8YLOa4cDA3CJ0a", "0998176543", "Offline", "Staff", "staff1" },
                    { new Guid("07f9fcfb-c770-423d-b725-d7a3ccc25a09"), "staff4@example.com", "Staff4", "Doe", null, "$2a$11$cAMG7Ez/zCNqsUNAkBXyG.SRlntsym3PlTugrAyqC7wXSagZyMzwC", "0998476543", "Offline", "Staff", "staff4" },
                    { new Guid("14bb9ebd-6e42-40c8-bde4-f5f63a80e3b8"), "jane.smith@example.com", "Jane", "Smith", null, "$2a$11$K8wjk6kI3nTS332OyLKakeoAAdipYgcy/mbXRD9lWqezybGRmfAcm", "0912234567", "Offline", "Student", "janesmith" },
                    { new Guid("1cc83eec-6e2c-44c0-83de-f8486657356c"), "staff5@example.com", "Staff5", "Doe", null, "$2a$11$zFUj4Pr3jGGen05cyr6my.fX8msuhZv98SkiB0LNifPcUL.iF7Vg.", "0998576543", "Offline", "Staff", "staff5" },
                    { new Guid("22716e25-502a-47c5-bd3a-8563abcd7fb1"), "peter.jones@example.com", "Peter", "Jones", null, "$2a$11$H5Ql3QGCoLO9O4fq2j7jEuNDElbdE1EqEzgszWlIWuyvhJ2Wjv2wG", "0912334567", "Offline", "Student", "peterjones" },
                    { new Guid("2535298c-1b8c-4039-9b4e-17cf84b36249"), "john.doe@example.com", "John", "Doe", null, "$2a$11$7CKvv5ub7pw8S3qL0EpKVeHuOJs1lKfFGmoOPMLC/jlcWSsYsLA62", "0912134567", "Offline", "Student", "johndoe" },
                    { new Guid("33c75f57-d1c2-47db-9856-ba5841875ae8"), "staff3@example.com", "Staff3", "Doe", null, "$2a$11$EZGEpECrVWAqj5li3kpltuSVJLDMI0EUWA6otbwXZwgtLPz8JMmry", "0998376543", "Offline", "Staff", "staff3" },
                    { new Guid("4138386d-26ff-478a-b036-5d5fa6b59847"), "alice.williams@example.com", "Alice", "Williams", null, "$2a$11$eKsLGVYwlQQdTFFUUYQvHe1Gli4kxBzyacBWpC8KqpohyHaDmR3hm", "0917123456", "Offline", "Teacher", "alicewilliams" },
                    { new Guid("470e3585-7444-4957-b745-777e2c824e76"), "admin3@example.com", "Admin3", "User", null, "$2a$11$8HdNGQ4ZNGiAAZIvSHDwwOFTQDf5cAPUSZW5gDmOvn7TkZNE.R2/S", null, "Offline", "Admin", "admin3" },
                    { new Guid("6102bd48-2531-4e41-94f4-22b8fe798438"), "student5@example.com", "Student5", "Jones", null, "$2a$11$ZeOkr6/Iu8s9AVq.Gmj8D.vO1SOtKnZ.OyF/EaYvM1By.ZE3H8SNi", "0912534567", "Offline", "Student", "student5" },
                    { new Guid("65febc6f-de3a-4346-9643-0f68e362895a"), "admin4@example.com", "Admin4", "User", null, "$2a$11$PyDvlw6Tw4n0QnQUCevtOeFUwV0pActiZuaK367kl5aZdzLXpUlzq", null, "Offline", "Admin", "admin4" },
                    { new Guid("6d46b16f-00e7-4d9e-93b5-7bb7c8a17a57"), "admin1@example.com", "Admin1", "User", null, "$2a$11$BemgpD/hHln/zTXZUaiHreoizYI8caZdgZ3aF9ZDvn0d7flilqnt.", null, "Offline", "Admin", "admin1" },
                    { new Guid("721a0113-f1e7-43e3-9ffd-3b8f04c8d4b3"), "superadmin@example.com", "Super", "Admin", null, "$2a$11$0L7rAS7/xzqPJMl7uFNdheWyQzTNEj5l3832dgMPWU2mojEEoPT2y", null, "Offline", "SuperAdmin", "superadmin" },
                    { new Guid("88a2c501-a016-41d0-b62b-c7375f6f5cc2"), "teacher4@example.com", "Teacher4", "Smith", null, "$2a$11$L/GJMBryC/4okqpW2fobNOfDLCZxg3Gy8mBKGB2w3mNR3UjtZgO26", "0917423456", "Offline", "Teacher", "teacher4" },
                    { new Guid("8e270c16-b686-46c6-811e-a1729e834625"), "bob.brown@example.com", "Bob", "Brown", null, "$2a$11$WD36frkqXzdnGvUVNkVoze7O4oM/dVkfEOLq6sN/arEWgk0eMmCyu", "0917223456", "Offline", "Teacher", "bobbrown" },
                    { new Guid("b13869ae-8afc-4c60-a34b-9a9d3898bce1"), "admin2@example.com", "Admin2", "User", null, "$2a$11$sLHV773WDO2pQfEaQYdDQOs6YwYysr67qACfpNsE7ok8yUVMQnwBe", null, "Offline", "Admin", "admin2" },
                    { new Guid("b3b23e69-0eea-48a6-850b-46896d2ba01a"), "student4@example.com", "Student4", "Jones", null, "$2a$11$loy07UpnNhv9O.brDLBpD.caa0UzDjNzAZK4lhp7m5mBrluOErKF6", "0912434567", "Offline", "Student", "student4" },
                    { new Guid("bddf357e-9c3d-486c-9ded-5fc75c0344ed"), "teacher5@example.com", "Teacher5", "Smith", null, "$2a$11$ItgFyyqu2r3xg5J3y5zvku1n6HlwQywAf3BZimzfqvOrM.WGV3aum", "0917523456", "Offline", "Teacher", "teacher5" },
                    { new Guid("dd4c3ad1-0473-41eb-8f2c-6b4c7181e192"), "teacher3@example.com", "Teacher3", "Smith", null, "$2a$11$WXbqkJgdbDXHLq9q1fR9POdMBTm4brS7x2ZbcQrBSItCO3EftvNp6", "0917323456", "Offline", "Teacher", "teacher3" },
                    { new Guid("e8a5ed89-943f-4fa4-a5fa-a707d6f7cb29"), "staff2@example.com", "Staff2", "Doe", null, "$2a$11$KaiAxtOFWlRzbD/n1ttL2emeTpUqeW3GM2oHfidufZhu66d.g80sK", "0998276543", "Offline", "Staff", "staff2" },
                    { new Guid("f2758c89-3553-41e4-9f90-bd35e8988750"), "admin5@example.com", "Admin5", "User", null, "$2a$11$ps/o2kXzsIeCQ/DXaOzWqONUkYW2LmxvS6GeWclIVNwOMm6Cu1dwG", null, "Offline", "Admin", "admin5" }
                });

            migrationBuilder.InsertData(
                table: "LentItems",
                columns: new[] { "Id", "Barcode", "BarcodeImage", "BorrowerFullName", "BorrowerRole", "IsHiddenFromUser", "ItemId", "ItemName", "LentAt", "Remarks", "ReturnedAt", "Room", "Status", "StudentIdNumber", "SubjectTimeSchedule", "TeacherFullName", "TeacherId", "UserId" },
                values: new object[,]
                {
                    { new Guid("631b7402-28dd-42c2-9ce9-2bdbbfcf0b82"), "LENT-20251017-001", null, "Bob Brown", "Teacher", false, new Guid("b980bd3a-a23e-4e07-aa01-1da198d14f8d"), "Mechanical Keyboard", new DateTime(2025, 10, 17, 11, 10, 24, 58, DateTimeKind.Utc).AddTicks(9247), null, new DateTime(2025, 11, 1, 11, 10, 24, 58, DateTimeKind.Utc).AddTicks(9247), "Room 302", "Returned", null, "IT Workshop - Daily", "", null, new Guid("8e270c16-b686-46c6-811e-a1729e834625") },
                    { new Guid("be57c8f6-7238-44f5-ae25-882ecbb56d08"), "LENT-20251113-001", null, "Alice Williams", "Teacher", false, new Guid("9e6ee2b3-f65b-4eed-99d8-9487f4855a0f"), "Wireless Mouse", new DateTime(2025, 11, 13, 11, 10, 24, 58, DateTimeKind.Utc).AddTicks(9247), null, null, "Room 101", "Borrowed", null, "Faculty Meeting - Friday 3:00 PM", "", null, new Guid("4138386d-26ff-478a-b036-5d5fa6b59847") }
                });

            migrationBuilder.InsertData(
                table: "Staff",
                columns: new[] { "Id", "Position" },
                values: new object[,]
                {
                    { new Guid("07d32fbb-bc4f-416d-8c25-2bd7f8147be3"), "Lab Technician" },
                    { new Guid("07f9fcfb-c770-423d-b725-d7a3ccc25a09"), "Lab Technician" },
                    { new Guid("1cc83eec-6e2c-44c0-83de-f8486657356c"), "Lab Technician" },
                    { new Guid("33c75f57-d1c2-47db-9856-ba5841875ae8"), "Lab Technician" },
                    { new Guid("e8a5ed89-943f-4fa4-a5fa-a707d6f7cb29"), "Lab Technician" }
                });

            migrationBuilder.InsertData(
                table: "Students",
                columns: new[] { "Id", "BackStudentIdPicture", "CityMunicipality", "Course", "FrontStudentIdPicture", "PostalCode", "ProfilePicture", "Province", "Section", "Street", "StudentIdNumber", "Year" },
                values: new object[,]
                {
                    { new Guid("14bb9ebd-6e42-40c8-bde4-f5f63a80e3b8"), null, "", "Computer Science", null, "", null, "", "A", "", "S2021-002", "3" },
                    { new Guid("22716e25-502a-47c5-bd3a-8563abcd7fb1"), null, "", "Computer Science", null, "", null, "", "A", "", "S2021-003", "3" },
                    { new Guid("2535298c-1b8c-4039-9b4e-17cf84b36249"), null, "", "Computer Science", null, "", null, "", "A", "", "S2021-001", "3" },
                    { new Guid("6102bd48-2531-4e41-94f4-22b8fe798438"), null, "", "Computer Science", null, "", null, "", "A", "", "2023-0005", "3" },
                    { new Guid("b3b23e69-0eea-48a6-850b-46896d2ba01a"), null, "", "Computer Science", null, "", null, "", "A", "", "2023-0004", "3" }
                });

            migrationBuilder.InsertData(
                table: "Teachers",
                columns: new[] { "Id", "Department" },
                values: new object[,]
                {
                    { new Guid("4138386d-26ff-478a-b036-5d5fa6b59847"), "Information Technology" },
                    { new Guid("88a2c501-a016-41d0-b62b-c7375f6f5cc2"), "Information Technology" },
                    { new Guid("8e270c16-b686-46c6-811e-a1729e834625"), "Information Technology" },
                    { new Guid("bddf357e-9c3d-486c-9ded-5fc75c0344ed"), "Information Technology" },
                    { new Guid("dd4c3ad1-0473-41eb-8f2c-6b4c7181e192"), "Information Technology" }
                });

            migrationBuilder.InsertData(
                table: "LentItems",
                columns: new[] { "Id", "Barcode", "BarcodeImage", "BorrowerFullName", "BorrowerRole", "IsHiddenFromUser", "ItemId", "ItemName", "LentAt", "Remarks", "ReturnedAt", "Room", "Status", "StudentIdNumber", "SubjectTimeSchedule", "TeacherFullName", "TeacherId", "UserId" },
                values: new object[,]
                {
                    { new Guid("0d921574-5c39-4156-8c11-8029174ca96c"), "LENT-20251115-001", null, "Peter Jones", "Student", false, new Guid("82ec5f2b-b7cd-4a39-a6a1-f733396afd4a"), "Wireless Microphone", new DateTime(2025, 11, 15, 11, 10, 24, 58, DateTimeKind.Utc).AddTicks(9247), null, null, "Room 401", "Borrowed", "S2021-003", "CS201 - MWF 2:00-3:30 PM", "Alice Williams", new Guid("4138386d-26ff-478a-b036-5d5fa6b59847"), new Guid("22716e25-502a-47c5-bd3a-8563abcd7fb1") },
                    { new Guid("444b95cf-0bc1-44ba-9079-1f0a5a6ee2e1"), "LENT-20251111-001", null, "John Doe", "Student", false, new Guid("66ebfa17-1c85-4dc2-9df4-a1651d4d517b"), "HDMI Cable 10ft", new DateTime(2025, 11, 11, 11, 10, 24, 58, DateTimeKind.Utc).AddTicks(9247), null, null, "Room 301", "Borrowed", "S2021-001", "CS101 - MWF 9:00-10:30 AM", "Alice Williams", new Guid("4138386d-26ff-478a-b036-5d5fa6b59847"), new Guid("2535298c-1b8c-4039-9b4e-17cf84b36249") },
                    { new Guid("afb07ab7-9d2c-4526-ae07-72d3f363ba8d"), "LENT-20251106-001", null, "Jane Smith", "Student", false, new Guid("50891c08-7605-4b4b-889e-7fd014a8f9c4"), "Portable Bluetooth Speaker", new DateTime(2025, 11, 6, 11, 10, 24, 58, DateTimeKind.Utc).AddTicks(9247), null, new DateTime(2025, 11, 14, 11, 10, 24, 58, DateTimeKind.Utc).AddTicks(9247), "Room 205", "Returned", "S2021-002", "MEDIA101 - TTH 1:00-2:30 PM", "Bob Brown", new Guid("8e270c16-b686-46c6-811e-a1729e834625"), new Guid("14bb9ebd-6e42-40c8-bde4-f5f63a80e3b8") }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Items",
                keyColumn: "Id",
                keyValue: new Guid("433d4153-802a-4889-a936-cc40152cf428"));

            migrationBuilder.DeleteData(
                table: "Items",
                keyColumn: "Id",
                keyValue: new Guid("5b027da2-8573-4da9-88ab-645fc5f1b71a"));

            migrationBuilder.DeleteData(
                table: "Items",
                keyColumn: "Id",
                keyValue: new Guid("6a9fa638-3a30-4a3e-b6d6-5d7a462e7256"));

            migrationBuilder.DeleteData(
                table: "LentItems",
                keyColumn: "Id",
                keyValue: new Guid("0d921574-5c39-4156-8c11-8029174ca96c"));

            migrationBuilder.DeleteData(
                table: "LentItems",
                keyColumn: "Id",
                keyValue: new Guid("444b95cf-0bc1-44ba-9079-1f0a5a6ee2e1"));

            migrationBuilder.DeleteData(
                table: "LentItems",
                keyColumn: "Id",
                keyValue: new Guid("631b7402-28dd-42c2-9ce9-2bdbbfcf0b82"));

            migrationBuilder.DeleteData(
                table: "LentItems",
                keyColumn: "Id",
                keyValue: new Guid("afb07ab7-9d2c-4526-ae07-72d3f363ba8d"));

            migrationBuilder.DeleteData(
                table: "LentItems",
                keyColumn: "Id",
                keyValue: new Guid("be57c8f6-7238-44f5-ae25-882ecbb56d08"));

            migrationBuilder.DeleteData(
                table: "Staff",
                keyColumn: "Id",
                keyValue: new Guid("07d32fbb-bc4f-416d-8c25-2bd7f8147be3"));

            migrationBuilder.DeleteData(
                table: "Staff",
                keyColumn: "Id",
                keyValue: new Guid("07f9fcfb-c770-423d-b725-d7a3ccc25a09"));

            migrationBuilder.DeleteData(
                table: "Staff",
                keyColumn: "Id",
                keyValue: new Guid("1cc83eec-6e2c-44c0-83de-f8486657356c"));

            migrationBuilder.DeleteData(
                table: "Staff",
                keyColumn: "Id",
                keyValue: new Guid("33c75f57-d1c2-47db-9856-ba5841875ae8"));

            migrationBuilder.DeleteData(
                table: "Staff",
                keyColumn: "Id",
                keyValue: new Guid("e8a5ed89-943f-4fa4-a5fa-a707d6f7cb29"));

            migrationBuilder.DeleteData(
                table: "Students",
                keyColumn: "Id",
                keyValue: new Guid("14bb9ebd-6e42-40c8-bde4-f5f63a80e3b8"));

            migrationBuilder.DeleteData(
                table: "Students",
                keyColumn: "Id",
                keyValue: new Guid("22716e25-502a-47c5-bd3a-8563abcd7fb1"));

            migrationBuilder.DeleteData(
                table: "Students",
                keyColumn: "Id",
                keyValue: new Guid("2535298c-1b8c-4039-9b4e-17cf84b36249"));

            migrationBuilder.DeleteData(
                table: "Students",
                keyColumn: "Id",
                keyValue: new Guid("6102bd48-2531-4e41-94f4-22b8fe798438"));

            migrationBuilder.DeleteData(
                table: "Students",
                keyColumn: "Id",
                keyValue: new Guid("b3b23e69-0eea-48a6-850b-46896d2ba01a"));

            migrationBuilder.DeleteData(
                table: "Teachers",
                keyColumn: "Id",
                keyValue: new Guid("88a2c501-a016-41d0-b62b-c7375f6f5cc2"));

            migrationBuilder.DeleteData(
                table: "Teachers",
                keyColumn: "Id",
                keyValue: new Guid("bddf357e-9c3d-486c-9ded-5fc75c0344ed"));

            migrationBuilder.DeleteData(
                table: "Teachers",
                keyColumn: "Id",
                keyValue: new Guid("dd4c3ad1-0473-41eb-8f2c-6b4c7181e192"));

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("470e3585-7444-4957-b745-777e2c824e76"));

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("65febc6f-de3a-4346-9643-0f68e362895a"));

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("6d46b16f-00e7-4d9e-93b5-7bb7c8a17a57"));

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("721a0113-f1e7-43e3-9ffd-3b8f04c8d4b3"));

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("b13869ae-8afc-4c60-a34b-9a9d3898bce1"));

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("f2758c89-3553-41e4-9f90-bd35e8988750"));

            migrationBuilder.DeleteData(
                table: "Items",
                keyColumn: "Id",
                keyValue: new Guid("50891c08-7605-4b4b-889e-7fd014a8f9c4"));

            migrationBuilder.DeleteData(
                table: "Items",
                keyColumn: "Id",
                keyValue: new Guid("66ebfa17-1c85-4dc2-9df4-a1651d4d517b"));

            migrationBuilder.DeleteData(
                table: "Items",
                keyColumn: "Id",
                keyValue: new Guid("82ec5f2b-b7cd-4a39-a6a1-f733396afd4a"));

            migrationBuilder.DeleteData(
                table: "Items",
                keyColumn: "Id",
                keyValue: new Guid("9e6ee2b3-f65b-4eed-99d8-9487f4855a0f"));

            migrationBuilder.DeleteData(
                table: "Items",
                keyColumn: "Id",
                keyValue: new Guid("b980bd3a-a23e-4e07-aa01-1da198d14f8d"));

            migrationBuilder.DeleteData(
                table: "Teachers",
                keyColumn: "Id",
                keyValue: new Guid("4138386d-26ff-478a-b036-5d5fa6b59847"));

            migrationBuilder.DeleteData(
                table: "Teachers",
                keyColumn: "Id",
                keyValue: new Guid("8e270c16-b686-46c6-811e-a1729e834625"));

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("07d32fbb-bc4f-416d-8c25-2bd7f8147be3"));

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("07f9fcfb-c770-423d-b725-d7a3ccc25a09"));

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("14bb9ebd-6e42-40c8-bde4-f5f63a80e3b8"));

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("1cc83eec-6e2c-44c0-83de-f8486657356c"));

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("22716e25-502a-47c5-bd3a-8563abcd7fb1"));

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("2535298c-1b8c-4039-9b4e-17cf84b36249"));

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("33c75f57-d1c2-47db-9856-ba5841875ae8"));

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("6102bd48-2531-4e41-94f4-22b8fe798438"));

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("88a2c501-a016-41d0-b62b-c7375f6f5cc2"));

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("b3b23e69-0eea-48a6-850b-46896d2ba01a"));

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("bddf357e-9c3d-486c-9ded-5fc75c0344ed"));

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("dd4c3ad1-0473-41eb-8f2c-6b4c7181e192"));

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("e8a5ed89-943f-4fa4-a5fa-a707d6f7cb29"));

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("4138386d-26ff-478a-b036-5d5fa6b59847"));

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("8e270c16-b686-46c6-811e-a1729e834625"));

            migrationBuilder.InsertData(
                table: "Items",
                columns: new[] { "Id", "Barcode", "BarcodeImage", "Category", "Condition", "CreatedAt", "Description", "Image", "ImageMimeType", "ItemMake", "ItemModel", "ItemName", "ItemType", "SerialNumber", "Status", "UpdatedAt" },
                values: new object[,]
                {
                    { new Guid("1808a478-ee4c-41d4-bcd9-442969c5f037"), null, null, "MediaEquipment", "New", new DateTime(2025, 10, 31, 5, 1, 55, 555, DateTimeKind.Utc).AddTicks(6917), "1080p projector for classroom presentations.", null, null, "Epson", "HC 2250", "Epson Home Cinema Projector", "Projector", "SN-PR-EPS-002", "Available", new DateTime(2025, 10, 31, 5, 1, 55, 555, DateTimeKind.Utc).AddTicks(6918) },
                    { new Guid("5cb4780d-812c-48cf-aa78-a03fd3a6a47c"), null, null, "Electronics", "Good", new DateTime(2025, 10, 31, 5, 1, 55, 555, DateTimeKind.Utc).AddTicks(6910), "High-performance laptop for video editing.", null, null, "Dell", "XPS 15 9510", "Dell XPS 15 Laptop", "Laptop", "SN-LP-DELL-001", "Available", new DateTime(2025, 10, 31, 5, 1, 55, 555, DateTimeKind.Utc).AddTicks(6911) },
                    { new Guid("98007650-218a-4baf-b731-8cdbc76d6651"), null, null, "MediaEquipment", "Good", new DateTime(2025, 10, 31, 5, 1, 55, 555, DateTimeKind.Utc).AddTicks(6926), "Dynamic microphone for vocal recording.", null, null, "Shure", "SM7B", "Shure SM7B Microphone", "Microphone", "SN-MC-SHR-004", "Available", new DateTime(2025, 10, 31, 5, 1, 55, 555, DateTimeKind.Utc).AddTicks(6929) },
                    { new Guid("abffe3b6-04fa-489c-ac3e-e9ae39b853b0"), null, null, "Electronics", "Good", new DateTime(2025, 10, 31, 5, 1, 55, 555, DateTimeKind.Utc).AddTicks(6933), "Tablet for digital art and design classes.", null, null, "Apple", "iPad Pro 12.9-inch", "Apple iPad Pro", "Tablet", "SN-TB-APL-005", "Available", new DateTime(2025, 10, 31, 5, 1, 55, 555, DateTimeKind.Utc).AddTicks(6934) },
                    { new Guid("b45c779f-4099-457d-9604-3e4cc6969948"), null, null, "MediaEquipment", "Good", new DateTime(2025, 10, 31, 5, 1, 55, 555, DateTimeKind.Utc).AddTicks(6922), "Full-frame mirrorless camera with 4K video.", null, null, "Canon", "EOS R6", "Canon EOS R6 Camera", "Camera", "SN-CM-CAN-003", "Available", new DateTime(2025, 10, 31, 5, 1, 55, 555, DateTimeKind.Utc).AddTicks(6922) }
                });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "Email", "FirstName", "LastName", "MiddleName", "PasswordHash", "PhoneNumber", "Status", "UserRole", "Username" },
                values: new object[,]
                {
                    { new Guid("1106d7a8-eb7b-42cf-8b64-74286fea84ba"), "student4@example.com", "Student4", "Jones", null, "$2a$11$xHjtm.9F7spPUvLqH8fG5.U2nQuCQnnJCsTkZ6V1Z/xuECX7HCwYG", "0912434567", "Offline", "Student", "student4" },
                    { new Guid("13bec163-b287-4e70-b68c-4b1c2e07fb30"), "staff1@example.com", "Staff1", "Doe", null, "$2a$11$.xK4Gw6v8Q7zlpDOEiydz.Wig05y4Pu2FW6uoKWMGNB4aYdLm2.Uy", "0998176543", "Offline", "Staff", "staff1" },
                    { new Guid("1e9106d3-73ec-4438-b08a-000eec5798f6"), "student5@example.com", "Student5", "Jones", null, "$2a$11$ey7RBAFcIiLMpTElBVXVZuKFQh09fIQpcalv2cWiCRPQeHLUJAxMS", "0912534567", "Offline", "Student", "student5" },
                    { new Guid("3bfb3538-d973-4edb-ae64-a82672f3ae0e"), "admin3@example.com", "Admin3", "User", null, "$2a$11$gKBOilsIkkh.WyPilmAsCuc1NgCZFiah/Xl.Q850hpx3mK6M4Bg/y", null, "Offline", "Admin", "admin3" },
                    { new Guid("3d16bf60-b629-492e-8e43-9b80339d05e0"), "admin1@example.com", "Admin1", "User", null, "$2a$11$h.ywu2763xUk9AGlzFh.1eIcMhZQHgODo9UgMLtQLNfD9FoTPO9o6", null, "Offline", "Admin", "admin1" },
                    { new Guid("5ac6a324-25fc-468b-9988-b9acd07c7982"), "staff3@example.com", "Staff3", "Doe", null, "$2a$11$T1Vi.nJrTa8JDB8PNs2X2uPoyB1VhwuRe8iosrI0sPgGBqoeoPk3q", "0998376543", "Offline", "Staff", "staff3" },
                    { new Guid("67230c4b-d9f2-4be5-a9a3-92d5fab82a56"), "staff5@example.com", "Staff5", "Doe", null, "$2a$11$hBQo9cOPUofHe2uC9RSxle9BQGZV6jOfzsQUJCU4WDDxVroedwFM6", "0998576543", "Offline", "Staff", "staff5" },
                    { new Guid("70667c61-1f72-431c-a9be-c2208295fefb"), "john.doe@example.com", "John", "Doe", null, "$2a$11$rH68BeMGWzWscHciQgPLsOyMvkLJC6rv/0mPkBCRpnWTZJ/abf962", "0912134567", "Offline", "Student", "johndoe" },
                    { new Guid("75e86d5e-10e4-4211-871d-06ef2266e3a9"), "staff4@example.com", "Staff4", "Doe", null, "$2a$11$LlgXuG0w7xQu1cziWFyuWe6qkdARtXdw/G.nkKF/.X6KOUkrw9PIC", "0998476543", "Offline", "Staff", "staff4" },
                    { new Guid("76a4a5d3-1913-4733-bf32-eeef7ac38d15"), "admin4@example.com", "Admin4", "User", null, "$2a$11$F6l.6Uq1s2nH6yaf2Ht6lOSIoHdkI4X39W0amsOqKLuanJPzqEcdG", null, "Offline", "Admin", "admin4" },
                    { new Guid("8159e8a2-f024-403d-8dd0-d23e8c4ce083"), "teacher5@example.com", "Teacher5", "Smith", null, "$2a$11$qkE.isCVU5kGvBckv.bPm.2BZvIcoBmIfBl/hOsBVLWZVLpN8OHqu", "0917523456", "Offline", "Teacher", "teacher5" },
                    { new Guid("89f1142c-4425-4c99-a592-720a0bf785c5"), "peter.jones@example.com", "Peter", "Jones", null, "$2a$11$lCF7yEGZhPDRVmR.JU/QAummwIKdbZkmaqe.PhZzeUwXgKofI4Gb6", "0912334567", "Offline", "Student", "peterjones" },
                    { new Guid("a3d80db9-70ba-4069-b505-5e6cc5358b5b"), "bob.brown@example.com", "Bob", "Brown", null, "$2a$11$05gXL3kf7Kpj/Adr4i1/aOfo9Y1sgxgA5N.As2Ank2G0zvJOZUrhS", "0917223456", "Offline", "Teacher", "bobbrown" },
                    { new Guid("a402a76b-3b93-4b2d-a075-1ccbf3da568d"), "jane.smith@example.com", "Jane", "Smith", null, "$2a$11$q9FSylUo0aBSTnNffCJQxOeDvaFJC1A.dO8/CFOaymN6Nqy7dFWv6", "0912234567", "Offline", "Student", "janesmith" },
                    { new Guid("a45b3918-3621-4a3c-b6b3-9e05849d3c73"), "teacher3@example.com", "Teacher3", "Smith", null, "$2a$11$U.nZGY/nVZze6qCOolHeAe7aH10NSIB1jsMkMdhI/mWalw4KVCzZK", "0917323456", "Offline", "Teacher", "teacher3" },
                    { new Guid("b6d116ae-df63-4284-a0aa-2b1fc46b31ae"), "alice.williams@example.com", "Alice", "Williams", null, "$2a$11$8PbcupGKpj8slWqJZcLDwuuyojztt9EhfqPzpm5GLJg8OrRIimyWO", "0917123456", "Offline", "Teacher", "alicewilliams" },
                    { new Guid("b8ca4420-e8f5-40c2-a3c2-b24c34d76e76"), "admin5@example.com", "Admin5", "User", null, "$2a$11$wbtX/lWZ5tezn1.xeyw8PeNkCxl.3QcuOl5T9AV0L3ORiYXoyxy.y", null, "Offline", "Admin", "admin5" },
                    { new Guid("c1798bec-b230-4e6c-88d9-55daaa72e564"), "superadmin@example.com", "Super", "Admin", null, "$2a$11$VR.RbIeqEi38HaH6gAOaWOEuvZpoKPtui9MCBXJT1F7UcAyzt5KWi", null, "Offline", "SuperAdmin", "superadmin" },
                    { new Guid("d2db5d22-8988-4b7c-b9e7-d130d0e0886f"), "teacher4@example.com", "Teacher4", "Smith", null, "$2a$11$kkRAYtAaVDX6El6qVK29euXNprNR.UfrLqx0LDZeImqUkkdhPqcci", "0917423456", "Offline", "Teacher", "teacher4" },
                    { new Guid("dec461b7-7687-46ae-a1f6-5ffa64f501af"), "staff2@example.com", "Staff2", "Doe", null, "$2a$11$XmLjJMmuEzws.YEZ7SxvOuM54RquBAPdcG9zxg4ZtbnAL5LE.Ne3i", "0998276543", "Offline", "Staff", "staff2" },
                    { new Guid("e46888af-9474-472d-b15c-b7a1a83b0da1"), "admin2@example.com", "Admin2", "User", null, "$2a$11$a.LLTihJ4peZnknMmYXusOoUIekMqud0p9DjN/B35wVTbnntDSyRq", null, "Offline", "Admin", "admin2" }
                });

            migrationBuilder.InsertData(
                table: "LentItems",
                columns: new[] { "Id", "Barcode", "BarcodeImage", "BorrowerFullName", "BorrowerRole", "IsHiddenFromUser", "ItemId", "ItemName", "LentAt", "Remarks", "ReturnedAt", "Room", "Status", "StudentIdNumber", "SubjectTimeSchedule", "TeacherFullName", "TeacherId", "UserId" },
                values: new object[,]
                {
                    { new Guid("233e8791-588f-4645-9e2d-22d0b5a7bfdc"), null, null, "Bob Brown", "Teacher", false, new Guid("abffe3b6-04fa-489c-ac3e-e9ae39b853b0"), "Apple iPad Pro", new DateTime(2025, 10, 1, 5, 1, 55, 555, DateTimeKind.Utc).AddTicks(7660), null, new DateTime(2025, 10, 16, 5, 1, 55, 555, DateTimeKind.Utc).AddTicks(7660), "", "Returned", null, "", "", null, new Guid("a3d80db9-70ba-4069-b505-5e6cc5358b5b") },
                    { new Guid("a604b8e2-26aa-45ee-b586-2e72172edee6"), null, null, "Alice Williams", "Teacher", false, new Guid("98007650-218a-4baf-b731-8cdbc76d6651"), "Shure SM7B Microphone", new DateTime(2025, 10, 28, 5, 1, 55, 555, DateTimeKind.Utc).AddTicks(7654), null, null, "", "Borrowed", null, "", "", null, new Guid("b6d116ae-df63-4284-a0aa-2b1fc46b31ae") }
                });

            migrationBuilder.InsertData(
                table: "Staff",
                columns: new[] { "Id", "Position" },
                values: new object[,]
                {
                    { new Guid("13bec163-b287-4e70-b68c-4b1c2e07fb30"), "Lab Technician" },
                    { new Guid("5ac6a324-25fc-468b-9988-b9acd07c7982"), "Lab Technician" },
                    { new Guid("67230c4b-d9f2-4be5-a9a3-92d5fab82a56"), "Lab Technician" },
                    { new Guid("75e86d5e-10e4-4211-871d-06ef2266e3a9"), "Lab Technician" },
                    { new Guid("dec461b7-7687-46ae-a1f6-5ffa64f501af"), "Lab Technician" }
                });

            migrationBuilder.InsertData(
                table: "Students",
                columns: new[] { "Id", "BackStudentIdPicture", "CityMunicipality", "Course", "FrontStudentIdPicture", "PostalCode", "ProfilePicture", "Province", "Section", "Street", "StudentIdNumber", "Year" },
                values: new object[,]
                {
                    { new Guid("1106d7a8-eb7b-42cf-8b64-74286fea84ba"), null, "", "Computer Science", null, "", null, "", "A", "", "2023-0004", "3" },
                    { new Guid("1e9106d3-73ec-4438-b08a-000eec5798f6"), null, "", "Computer Science", null, "", null, "", "A", "", "2023-0005", "3" },
                    { new Guid("70667c61-1f72-431c-a9be-c2208295fefb"), null, "", "Computer Science", null, "", null, "", "A", "", "S2021-001", "3" },
                    { new Guid("89f1142c-4425-4c99-a592-720a0bf785c5"), null, "", "Computer Science", null, "", null, "", "A", "", "S2021-003", "3" },
                    { new Guid("a402a76b-3b93-4b2d-a075-1ccbf3da568d"), null, "", "Computer Science", null, "", null, "", "A", "", "S2021-002", "3" }
                });

            migrationBuilder.InsertData(
                table: "Teachers",
                columns: new[] { "Id", "Department" },
                values: new object[,]
                {
                    { new Guid("8159e8a2-f024-403d-8dd0-d23e8c4ce083"), "Information Technology" },
                    { new Guid("a3d80db9-70ba-4069-b505-5e6cc5358b5b"), "Information Technology" },
                    { new Guid("a45b3918-3621-4a3c-b6b3-9e05849d3c73"), "Information Technology" },
                    { new Guid("b6d116ae-df63-4284-a0aa-2b1fc46b31ae"), "Information Technology" },
                    { new Guid("d2db5d22-8988-4b7c-b9e7-d130d0e0886f"), "Information Technology" }
                });

            migrationBuilder.InsertData(
                table: "LentItems",
                columns: new[] { "Id", "Barcode", "BarcodeImage", "BorrowerFullName", "BorrowerRole", "IsHiddenFromUser", "ItemId", "ItemName", "LentAt", "Remarks", "ReturnedAt", "Room", "Status", "StudentIdNumber", "SubjectTimeSchedule", "TeacherFullName", "TeacherId", "UserId" },
                values: new object[,]
                {
                    { new Guid("16096275-55ce-4b63-9df7-b117234a53e2"), null, null, "John Doe", "Student", false, new Guid("5cb4780d-812c-48cf-aa78-a03fd3a6a47c"), "Dell XPS 15 Laptop", new DateTime(2025, 10, 26, 5, 1, 55, 555, DateTimeKind.Utc).AddTicks(7624), null, null, "", "Borrowed", "S2021-001", "", "Alice Williams", new Guid("b6d116ae-df63-4284-a0aa-2b1fc46b31ae"), new Guid("70667c61-1f72-431c-a9be-c2208295fefb") },
                    { new Guid("42554f4d-a6b9-4fb1-be12-4e37b7c54a5c"), null, null, "Peter Jones", "Student", false, new Guid("1808a478-ee4c-41d4-bcd9-442969c5f037"), "Epson Home Cinema Projector", new DateTime(2025, 10, 30, 5, 1, 55, 555, DateTimeKind.Utc).AddTicks(7651), null, null, "", "Borrowed", "S2021-003", "", "Alice Williams", new Guid("b6d116ae-df63-4284-a0aa-2b1fc46b31ae"), new Guid("89f1142c-4425-4c99-a592-720a0bf785c5") },
                    { new Guid("b184cf02-4ec0-4aba-9b14-184620f2044a"), null, null, "Jane Smith", "Student", false, new Guid("b45c779f-4099-457d-9604-3e4cc6969948"), "Canon EOS R6 Camera", new DateTime(2025, 10, 21, 5, 1, 55, 555, DateTimeKind.Utc).AddTicks(7643), null, new DateTime(2025, 10, 29, 5, 1, 55, 555, DateTimeKind.Utc).AddTicks(7644), "", "Returned", "S2021-002", "", "Bob Brown", new Guid("a3d80db9-70ba-4069-b505-5e6cc5358b5b"), new Guid("a402a76b-3b93-4b2d-a075-1ccbf3da568d") }
                });
        }
    }
}
