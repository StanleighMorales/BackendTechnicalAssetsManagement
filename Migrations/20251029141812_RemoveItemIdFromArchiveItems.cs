using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace BackendTechnicalAssetsManagement.Migrations
{
    /// <inheritdoc />
    public partial class RemoveItemIdFromArchiveItems : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
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
                name: "ItemId",
                table: "ArchiveItems");

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

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
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

            migrationBuilder.AddColumn<Guid>(
                name: "ItemId",
                table: "ArchiveItems",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

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
    }
}
