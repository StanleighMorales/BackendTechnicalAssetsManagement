using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace BackendTechnicalAssetsManagement.Migrations
{
    /// <summary>
    /// Updates lent items barcode format from LENT-{GUID} to LENT-YYYYMMDD-XXX
    /// where XXX is a 3-digit daily sequence number starting from 001
    /// </summary>
    public partial class UpdateLentItemsBarcodeFormat : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            // First, update existing lent items with old barcode format to new format
            migrationBuilder.Sql(@"
                DECLARE @Counter INT = 1;
                DECLARE @CurrentDate DATE = CAST(GETUTCDATE() AS DATE);
                DECLARE @DateString NVARCHAR(8) = FORMAT(@CurrentDate, 'yyyyMMdd');
                
                -- Update existing lent items with old GUID-based barcodes to new date-based format
                UPDATE LentItems 
                SET Barcode = 'LENT-' + @DateString + '-' + FORMAT(@Counter, '000'),
                    @Counter = @Counter + 1
                WHERE Barcode LIKE 'LENT-%' 
                AND LEN(Barcode) > 15  -- Old GUID format is longer
                AND Barcode NOT LIKE 'LENT-________-___';  -- Not already in new format
            ");
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

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
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
    }
}
