using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace BackendTechnicalAssetsManagement.Migrations
{
    /// <inheritdoc />
    public partial class AddedBarcodeForLentItems : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Staff",
                keyColumn: "Id",
                keyValue: new Guid("029a8a52-5c48-45d7-924a-c95e9bbd6fa8"));

            migrationBuilder.DeleteData(
                table: "Staff",
                keyColumn: "Id",
                keyValue: new Guid("69e5c682-1836-4d67-85a7-8252f02b44e2"));

            migrationBuilder.DeleteData(
                table: "Staff",
                keyColumn: "Id",
                keyValue: new Guid("912035aa-7e50-45dd-a44b-e248f74f9073"));

            migrationBuilder.DeleteData(
                table: "Staff",
                keyColumn: "Id",
                keyValue: new Guid("915c6b5b-920f-4fae-a5d9-faca2b03bdb4"));

            migrationBuilder.DeleteData(
                table: "Staff",
                keyColumn: "Id",
                keyValue: new Guid("a9c6c964-bc81-40a3-bb23-e835f7a97b96"));

            migrationBuilder.DeleteData(
                table: "Students",
                keyColumn: "Id",
                keyValue: new Guid("0e25db68-5a92-4b44-8d13-bc486d834e36"));

            migrationBuilder.DeleteData(
                table: "Students",
                keyColumn: "Id",
                keyValue: new Guid("25207d58-9584-4509-9315-0edc55767d26"));

            migrationBuilder.DeleteData(
                table: "Students",
                keyColumn: "Id",
                keyValue: new Guid("62625b62-bbce-4215-8e7d-f0c3e3e7be12"));

            migrationBuilder.DeleteData(
                table: "Students",
                keyColumn: "Id",
                keyValue: new Guid("7ce13a83-09f6-402a-9f1b-ab34ab2c1cbe"));

            migrationBuilder.DeleteData(
                table: "Students",
                keyColumn: "Id",
                keyValue: new Guid("9424ba2f-744c-4e74-9e44-a82cf2492257"));

            migrationBuilder.DeleteData(
                table: "Teachers",
                keyColumn: "Id",
                keyValue: new Guid("01d8c593-93d2-4c41-b452-aefad0b7e580"));

            migrationBuilder.DeleteData(
                table: "Teachers",
                keyColumn: "Id",
                keyValue: new Guid("774b27ab-3283-4c1f-a613-4db341da56f1"));

            migrationBuilder.DeleteData(
                table: "Teachers",
                keyColumn: "Id",
                keyValue: new Guid("bcbb79eb-1ec3-4789-aac8-21fc370060b7"));

            migrationBuilder.DeleteData(
                table: "Teachers",
                keyColumn: "Id",
                keyValue: new Guid("d27a04a2-af24-4366-bb49-6ff428f16c8a"));

            migrationBuilder.DeleteData(
                table: "Teachers",
                keyColumn: "Id",
                keyValue: new Guid("ee79979c-9068-49e5-a486-dffdfee6a094"));

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("0366d205-1632-41d3-8fda-1214f9d09675"));

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("451289ca-c259-4a0b-b269-d16fbf6aef95"));

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("7606e4ad-efd5-4130-bff3-19d2da86e342"));

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("8e40d83d-5291-4f7d-bb48-57ce6f533443"));

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("9002f1ce-34d8-4a96-8588-8df7777b7802"));

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("f45d63b8-e00d-4624-a48d-4e8020770140"));

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("01d8c593-93d2-4c41-b452-aefad0b7e580"));

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("029a8a52-5c48-45d7-924a-c95e9bbd6fa8"));

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("0e25db68-5a92-4b44-8d13-bc486d834e36"));

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("25207d58-9584-4509-9315-0edc55767d26"));

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("62625b62-bbce-4215-8e7d-f0c3e3e7be12"));

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("69e5c682-1836-4d67-85a7-8252f02b44e2"));

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("774b27ab-3283-4c1f-a613-4db341da56f1"));

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("7ce13a83-09f6-402a-9f1b-ab34ab2c1cbe"));

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("912035aa-7e50-45dd-a44b-e248f74f9073"));

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("915c6b5b-920f-4fae-a5d9-faca2b03bdb4"));

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("9424ba2f-744c-4e74-9e44-a82cf2492257"));

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("a9c6c964-bc81-40a3-bb23-e835f7a97b96"));

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("bcbb79eb-1ec3-4789-aac8-21fc370060b7"));

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("d27a04a2-af24-4366-bb49-6ff428f16c8a"));

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("ee79979c-9068-49e5-a486-dffdfee6a094"));

            migrationBuilder.AddColumn<string>(
                name: "Barcode",
                table: "LentItems",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<byte[]>(
                name: "BarcodeImage",
                table: "LentItems",
                type: "varbinary(max)",
                nullable: true);

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "Email", "FirstName", "LastName", "MiddleName", "PasswordHash", "PhoneNumber", "Status", "UserRole", "Username" },
                values: new object[,]
                {
                    { new Guid("1012641e-4d0e-4f2f-9502-7a9141c9b4b3"), "teacher1@example.com", "Teacher1", "Smith", null, "$2a$11$PqCGAiHnbCOB4KkrlinKdeoUX1qmRZTURiKh0UNXaL5JGELy22k1G", "0917123456", "Offline", "Teacher", "teacher1" },
                    { new Guid("13fc8f77-45ff-4c24-9d29-27761171a5be"), "staff2@example.com", "Staff2", "Doe", null, "$2a$11$BYGU9yskFJipTAg555Mbu.H3TVgvhsZO09LuvU4Gx3IV.3Ic1nByW", "0998276543", "Offline", "Staff", "staff2" },
                    { new Guid("147c2993-6d8c-43c1-8243-2b4ad45e5abf"), "student4@example.com", "Student4", "Jones", null, "$2a$11$saMk2KEzgohlPa/VxcYDXOAJiGgTLzy7l0MZreew5HzsiCjNV53m2", "0912434567", "Offline", "Student", "student4" },
                    { new Guid("1b4bfd13-8365-402e-b46b-c67987f5ca36"), "admin1@example.com", "Admin1", "User", null, "$2a$11$NvfpO/a3DOnfxoi8IVsphO9gv2.PJ.059wufBO5PNUbwW5.aHLoBe", null, "Offline", "Admin", "admin1" },
                    { new Guid("283cb1a7-4005-44b1-9fee-5055bea2e2fd"), "student5@example.com", "Student5", "Jones", null, "$2a$11$QyDaiE1CWJwEnMIY.jOMhuooDEeqG2zbrrrA9s2VEBJqrkfgEnwwm", "0912534567", "Offline", "Student", "student5" },
                    { new Guid("3da83155-35a1-4263-bc33-173bdb8d0ece"), "admin2@example.com", "Admin2", "User", null, "$2a$11$lTO.L6mg4RXhlNsvg85kxOfqtHt1A0ixYkroSyKVHq5JUbLJzqsw.", null, "Offline", "Admin", "admin2" },
                    { new Guid("4474b8ea-84f8-4425-959b-ab1cd0a264bd"), "teacher5@example.com", "Teacher5", "Smith", null, "$2a$11$7NFTGKAiN/R7844xRwJq0O7CcyGGQa/IFVOfiz8V/ZbYLdDrOnGWq", "0917523456", "Offline", "Teacher", "teacher5" },
                    { new Guid("4ccccd17-75ab-4398-b137-4bd919ef1062"), "staff4@example.com", "Staff4", "Doe", null, "$2a$11$c3.V.S6wyG9YgvCtf5x4YuxBP159iAVhOxZS1w/Vn.M6cKTN.SUl2", "0998476543", "Offline", "Staff", "staff4" },
                    { new Guid("520ddfdd-fbd7-44ff-bcab-58306f4dcfa1"), "staff3@example.com", "Staff3", "Doe", null, "$2a$11$GmUTxfxR43YE12jHemjzgOH5VAgGgnlra1fmkCcD7Mdhg6ddYlxAK", "0998376543", "Offline", "Staff", "staff3" },
                    { new Guid("6575e8c1-566a-42d6-81d6-deb898b3b7c9"), "staff1@example.com", "Staff1", "Doe", null, "$2a$11$pB2.ycfAUnAaNTBgLGijgunHcVIaAtYOaH153p6.VKWNsMl0k/FSm", "0998176543", "Offline", "Staff", "staff1" },
                    { new Guid("738fda80-830f-4882-961d-e4282187ba8b"), "admin4@example.com", "Admin4", "User", null, "$2a$11$mt3puGsCeeAmBnOCIwUpeOkHEAdZraPFwg8FfrOvwydCWeoDP5m8G", null, "Offline", "Admin", "admin4" },
                    { new Guid("7aa16ca8-7f36-41bd-82b7-0d876c7137f7"), "student2@example.com", "Student2", "Jones", null, "$2a$11$1Kttl7moA2fB9bkVhF9.f.AeGG3nZf/ptmnMaPUHh4VmIJVsQIsG.", "0912234567", "Offline", "Student", "student2" },
                    { new Guid("7ea95571-0292-4532-9d77-b7e233292c27"), "admin5@example.com", "Admin5", "User", null, "$2a$11$vBiHbKYaC7QvfdWxfefkT.3nyAyep7zi29SakCaU79g./oIkVKUEa", null, "Offline", "Admin", "admin5" },
                    { new Guid("87bb2c9c-719a-4c63-a102-b1e05deb3725"), "superadmin@example.com", "Super", "Admin", null, "$2a$11$QxpktcYfXoAEG8a1pX3WKu8.6uz661SS557/h2S2J3.aLzpAQZwXW", null, "Offline", "SuperAdmin", "superadmin" },
                    { new Guid("95f38348-7993-44aa-92bb-5ab7b095fb59"), "student1@example.com", "Student1", "Jones", null, "$2a$11$rOI/UstMLTkbL8470Mnu5uVmoYHHpLv/EOMDGYf6HvaaYiuxb6Oku", "0912134567", "Offline", "Student", "student1" },
                    { new Guid("b5009a88-340c-4cee-95e8-e0653e6af8c3"), "teacher3@example.com", "Teacher3", "Smith", null, "$2a$11$5CYJIHRMOOwsO5LIdiMsTO5zoVTGY0prGb7moYTtw1dWVNIBTNMuK", "0917323456", "Offline", "Teacher", "teacher3" },
                    { new Guid("b5555ab8-9d04-4cfc-a490-a1a876dc36a9"), "student3@example.com", "Student3", "Jones", null, "$2a$11$M/R/Dk3h6IGfptdMad.il.GkyeEZc/AGRjP7tj5gVDEvad/hEYt/y", "0912334567", "Offline", "Student", "student3" },
                    { new Guid("e221c938-1475-4dc7-977b-dd0670651852"), "admin3@example.com", "Admin3", "User", null, "$2a$11$dcB1xHCEEJlpwDxRqXgDcul9uyhnf4mCUnzCf104.DYJwhQ95B9sS", null, "Offline", "Admin", "admin3" },
                    { new Guid("ebcf22fe-cefd-40b7-bec4-849b4b0b52f3"), "staff5@example.com", "Staff5", "Doe", null, "$2a$11$bUJVyLTQrNq.AA5qutDB..9j3IzqD5vx8v2vLZ.QgQXW9YfC9LGzO", "0998576543", "Offline", "Staff", "staff5" },
                    { new Guid("ec0691c2-c2f6-44c0-8f17-4dd8285b0777"), "teacher4@example.com", "Teacher4", "Smith", null, "$2a$11$lZYiaA0EzlnVBQRqTwdwzuf2ylPngzNO82U7dxeeuxWv7BDpptn/K", "0917423456", "Offline", "Teacher", "teacher4" },
                    { new Guid("f626ffa6-316f-440b-bbc9-4d1def8c0ff9"), "teacher2@example.com", "Teacher2", "Smith", null, "$2a$11$1f/n19O08WdtEF9/iDVs5uNQtJS3Qxy3UNXC8lPAC4YI4GNbtzuNC", "0917223456", "Offline", "Teacher", "teacher2" }
                });

            migrationBuilder.InsertData(
                table: "Staff",
                columns: new[] { "Id", "Position" },
                values: new object[,]
                {
                    { new Guid("13fc8f77-45ff-4c24-9d29-27761171a5be"), "Lab Technician" },
                    { new Guid("4ccccd17-75ab-4398-b137-4bd919ef1062"), "Lab Technician" },
                    { new Guid("520ddfdd-fbd7-44ff-bcab-58306f4dcfa1"), "Lab Technician" },
                    { new Guid("6575e8c1-566a-42d6-81d6-deb898b3b7c9"), "Lab Technician" },
                    { new Guid("ebcf22fe-cefd-40b7-bec4-849b4b0b52f3"), "Lab Technician" }
                });

            migrationBuilder.InsertData(
                table: "Students",
                columns: new[] { "Id", "BackStudentIdPicture", "CityMunicipality", "Course", "FrontStudentIdPicture", "PostalCode", "ProfilePicture", "Province", "Section", "Street", "StudentIdNumber", "Year" },
                values: new object[,]
                {
                    { new Guid("147c2993-6d8c-43c1-8243-2b4ad45e5abf"), null, "Sample City", "Computer Science", null, "12345", null, "Sample Province", "A", "123 Main St #4", "2023-0004", "3" },
                    { new Guid("283cb1a7-4005-44b1-9fee-5055bea2e2fd"), null, "Sample City", "Computer Science", null, "12345", null, "Sample Province", "A", "123 Main St #5", "2023-0005", "3" },
                    { new Guid("7aa16ca8-7f36-41bd-82b7-0d876c7137f7"), null, "Sample City", "Computer Science", null, "12345", null, "Sample Province", "A", "123 Main St #2", "2023-0002", "3" },
                    { new Guid("95f38348-7993-44aa-92bb-5ab7b095fb59"), null, "Sample City", "Computer Science", null, "12345", null, "Sample Province", "A", "123 Main St #1", "2023-0001", "3" },
                    { new Guid("b5555ab8-9d04-4cfc-a490-a1a876dc36a9"), null, "Sample City", "Computer Science", null, "12345", null, "Sample Province", "A", "123 Main St #3", "2023-0003", "3" }
                });

            migrationBuilder.InsertData(
                table: "Teachers",
                columns: new[] { "Id", "Department" },
                values: new object[,]
                {
                    { new Guid("1012641e-4d0e-4f2f-9502-7a9141c9b4b3"), "Information Technology" },
                    { new Guid("4474b8ea-84f8-4425-959b-ab1cd0a264bd"), "Information Technology" },
                    { new Guid("b5009a88-340c-4cee-95e8-e0653e6af8c3"), "Information Technology" },
                    { new Guid("ec0691c2-c2f6-44c0-8f17-4dd8285b0777"), "Information Technology" },
                    { new Guid("f626ffa6-316f-440b-bbc9-4d1def8c0ff9"), "Information Technology" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Staff",
                keyColumn: "Id",
                keyValue: new Guid("13fc8f77-45ff-4c24-9d29-27761171a5be"));

            migrationBuilder.DeleteData(
                table: "Staff",
                keyColumn: "Id",
                keyValue: new Guid("4ccccd17-75ab-4398-b137-4bd919ef1062"));

            migrationBuilder.DeleteData(
                table: "Staff",
                keyColumn: "Id",
                keyValue: new Guid("520ddfdd-fbd7-44ff-bcab-58306f4dcfa1"));

            migrationBuilder.DeleteData(
                table: "Staff",
                keyColumn: "Id",
                keyValue: new Guid("6575e8c1-566a-42d6-81d6-deb898b3b7c9"));

            migrationBuilder.DeleteData(
                table: "Staff",
                keyColumn: "Id",
                keyValue: new Guid("ebcf22fe-cefd-40b7-bec4-849b4b0b52f3"));

            migrationBuilder.DeleteData(
                table: "Students",
                keyColumn: "Id",
                keyValue: new Guid("147c2993-6d8c-43c1-8243-2b4ad45e5abf"));

            migrationBuilder.DeleteData(
                table: "Students",
                keyColumn: "Id",
                keyValue: new Guid("283cb1a7-4005-44b1-9fee-5055bea2e2fd"));

            migrationBuilder.DeleteData(
                table: "Students",
                keyColumn: "Id",
                keyValue: new Guid("7aa16ca8-7f36-41bd-82b7-0d876c7137f7"));

            migrationBuilder.DeleteData(
                table: "Students",
                keyColumn: "Id",
                keyValue: new Guid("95f38348-7993-44aa-92bb-5ab7b095fb59"));

            migrationBuilder.DeleteData(
                table: "Students",
                keyColumn: "Id",
                keyValue: new Guid("b5555ab8-9d04-4cfc-a490-a1a876dc36a9"));

            migrationBuilder.DeleteData(
                table: "Teachers",
                keyColumn: "Id",
                keyValue: new Guid("1012641e-4d0e-4f2f-9502-7a9141c9b4b3"));

            migrationBuilder.DeleteData(
                table: "Teachers",
                keyColumn: "Id",
                keyValue: new Guid("4474b8ea-84f8-4425-959b-ab1cd0a264bd"));

            migrationBuilder.DeleteData(
                table: "Teachers",
                keyColumn: "Id",
                keyValue: new Guid("b5009a88-340c-4cee-95e8-e0653e6af8c3"));

            migrationBuilder.DeleteData(
                table: "Teachers",
                keyColumn: "Id",
                keyValue: new Guid("ec0691c2-c2f6-44c0-8f17-4dd8285b0777"));

            migrationBuilder.DeleteData(
                table: "Teachers",
                keyColumn: "Id",
                keyValue: new Guid("f626ffa6-316f-440b-bbc9-4d1def8c0ff9"));

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("1b4bfd13-8365-402e-b46b-c67987f5ca36"));

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("3da83155-35a1-4263-bc33-173bdb8d0ece"));

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("738fda80-830f-4882-961d-e4282187ba8b"));

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("7ea95571-0292-4532-9d77-b7e233292c27"));

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("87bb2c9c-719a-4c63-a102-b1e05deb3725"));

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("e221c938-1475-4dc7-977b-dd0670651852"));

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("1012641e-4d0e-4f2f-9502-7a9141c9b4b3"));

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("13fc8f77-45ff-4c24-9d29-27761171a5be"));

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("147c2993-6d8c-43c1-8243-2b4ad45e5abf"));

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("283cb1a7-4005-44b1-9fee-5055bea2e2fd"));

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("4474b8ea-84f8-4425-959b-ab1cd0a264bd"));

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("4ccccd17-75ab-4398-b137-4bd919ef1062"));

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("520ddfdd-fbd7-44ff-bcab-58306f4dcfa1"));

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("6575e8c1-566a-42d6-81d6-deb898b3b7c9"));

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("7aa16ca8-7f36-41bd-82b7-0d876c7137f7"));

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("95f38348-7993-44aa-92bb-5ab7b095fb59"));

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("b5009a88-340c-4cee-95e8-e0653e6af8c3"));

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("b5555ab8-9d04-4cfc-a490-a1a876dc36a9"));

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("ebcf22fe-cefd-40b7-bec4-849b4b0b52f3"));

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("ec0691c2-c2f6-44c0-8f17-4dd8285b0777"));

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("f626ffa6-316f-440b-bbc9-4d1def8c0ff9"));

            migrationBuilder.DropColumn(
                name: "Barcode",
                table: "LentItems");

            migrationBuilder.DropColumn(
                name: "BarcodeImage",
                table: "LentItems");

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "Email", "FirstName", "LastName", "MiddleName", "PasswordHash", "PhoneNumber", "Status", "UserRole", "Username" },
                values: new object[,]
                {
                    { new Guid("01d8c593-93d2-4c41-b452-aefad0b7e580"), "teacher5@example.com", "Teacher5", "Smith", null, "$2a$11$PCmiYBYBmVmTrPgXovt4XuAu8NLOJASXPPrJ0t.edqFarOlelh5j2", "0917523456", "Offline", "Teacher", "teacher5" },
                    { new Guid("029a8a52-5c48-45d7-924a-c95e9bbd6fa8"), "staff1@example.com", "Staff1", "Doe", null, "$2a$11$EN8piMXz2mEgURlDVN57weX7nweN7lpY4St3Gsul9aLJigddRIQ06", "0998176543", "Offline", "Staff", "staff1" },
                    { new Guid("0366d205-1632-41d3-8fda-1214f9d09675"), "admin5@example.com", "Admin5", "User", null, "$2a$11$Tb0GNbfbCYzFSRjQyNxQhekhxygUmYxcN8QPUQhgG7n.q.TEy7NE.", null, "Offline", "Admin", "admin5" },
                    { new Guid("0e25db68-5a92-4b44-8d13-bc486d834e36"), "student3@example.com", "Student3", "Jones", null, "$2a$11$CKIzG.prPmMqP48oetKEz.twKbcSMHBHlefR4x26.nMRbflE7fjQO", "0912334567", "Offline", "Student", "student3" },
                    { new Guid("25207d58-9584-4509-9315-0edc55767d26"), "student4@example.com", "Student4", "Jones", null, "$2a$11$4NSZE4Kk69aCqHg107LGweISDTqd62QYpYdBywHdYbJuXNGn3XT/y", "0912434567", "Offline", "Student", "student4" },
                    { new Guid("451289ca-c259-4a0b-b269-d16fbf6aef95"), "admin1@example.com", "Admin1", "User", null, "$2a$11$JkPfiyyrfazD5JXPdy7w3.WS304BpYF9o4fvhiC6gdNeQfuGLe9mu", null, "Offline", "Admin", "admin1" },
                    { new Guid("62625b62-bbce-4215-8e7d-f0c3e3e7be12"), "student5@example.com", "Student5", "Jones", null, "$2a$11$k9vzmb8dyuS7XzNpwDm/AezaMeqXkqTYqVx0m4DdQF7R579/9rfKO", "0912534567", "Offline", "Student", "student5" },
                    { new Guid("69e5c682-1836-4d67-85a7-8252f02b44e2"), "staff2@example.com", "Staff2", "Doe", null, "$2a$11$hhB4BLghR0Pb8Vqn243wu.Bi.sxQEV8lKpHgQ5n.DW1NipC2k9qo6", "0998276543", "Offline", "Staff", "staff2" },
                    { new Guid("7606e4ad-efd5-4130-bff3-19d2da86e342"), "superadmin@example.com", "Super", "Admin", null, "$2a$11$Q292p9ufaIp0MhaiXlWIquMrdizEapgcVPS/RZ1Qn2QVeLz7DESKW", null, "Offline", "SuperAdmin", "superadmin" },
                    { new Guid("774b27ab-3283-4c1f-a613-4db341da56f1"), "teacher2@example.com", "Teacher2", "Smith", null, "$2a$11$qHN0Ll/CykxXyqjODCvAaOYM59XLD49w4ZMAd8aZkdP8/d4BRIjwG", "0917223456", "Offline", "Teacher", "teacher2" },
                    { new Guid("7ce13a83-09f6-402a-9f1b-ab34ab2c1cbe"), "student2@example.com", "Student2", "Jones", null, "$2a$11$fvzsWP318QhbIoupxIr0mefeF3pvKd62CV7NzWCL3z49vvCb0NM3a", "0912234567", "Offline", "Student", "student2" },
                    { new Guid("8e40d83d-5291-4f7d-bb48-57ce6f533443"), "admin3@example.com", "Admin3", "User", null, "$2a$11$gKUu8tqB60fzUWIdT5zDIuoBRfyL75IUVFrRcYIIQ5XEhKlOZoV5e", null, "Offline", "Admin", "admin3" },
                    { new Guid("9002f1ce-34d8-4a96-8588-8df7777b7802"), "admin4@example.com", "Admin4", "User", null, "$2a$11$bpRnlwUZ7h3kGT0yRLhZ1.UhOYJrALHNcXSt1mgxDJ8r.i06LsnJW", null, "Offline", "Admin", "admin4" },
                    { new Guid("912035aa-7e50-45dd-a44b-e248f74f9073"), "staff5@example.com", "Staff5", "Doe", null, "$2a$11$If24Oo444EjPNfZkmmfaj.7Jr1krtdybEJmxEEsDluVUsDrqyw.k6", "0998576543", "Offline", "Staff", "staff5" },
                    { new Guid("915c6b5b-920f-4fae-a5d9-faca2b03bdb4"), "staff3@example.com", "Staff3", "Doe", null, "$2a$11$wgoRrsp9U0DyFfY/r0uJGe0q16.ZQp42XUtxxEM9guf9aTBsT.ZH2", "0998376543", "Offline", "Staff", "staff3" },
                    { new Guid("9424ba2f-744c-4e74-9e44-a82cf2492257"), "student1@example.com", "Student1", "Jones", null, "$2a$11$O1RPoQSefwMscLIBoE6rWuBSBpE.lc0Dq4kCXVI16i5dUgSAvB0tG", "0912134567", "Offline", "Student", "student1" },
                    { new Guid("a9c6c964-bc81-40a3-bb23-e835f7a97b96"), "staff4@example.com", "Staff4", "Doe", null, "$2a$11$J.igY4DcMvMq7zpd/prk0OLMjHTTSRwr0PpkWRapCwu/OpeYOkBji", "0998476543", "Offline", "Staff", "staff4" },
                    { new Guid("bcbb79eb-1ec3-4789-aac8-21fc370060b7"), "teacher1@example.com", "Teacher1", "Smith", null, "$2a$11$5EYV.Cf4MpeUOUL37yG90OswUtxvSQ2dGZJgNSbPXy6B7mppHWOKC", "0917123456", "Offline", "Teacher", "teacher1" },
                    { new Guid("d27a04a2-af24-4366-bb49-6ff428f16c8a"), "teacher4@example.com", "Teacher4", "Smith", null, "$2a$11$T4BsA6EsdVizVBOf7Uc/zexpSLMXRA/EqZU/pjYwDVhgCozETPOti", "0917423456", "Offline", "Teacher", "teacher4" },
                    { new Guid("ee79979c-9068-49e5-a486-dffdfee6a094"), "teacher3@example.com", "Teacher3", "Smith", null, "$2a$11$KO.wIxoOEtv2mkaJSMsXRu3sYrX/3zzypWfBUo6dHamLn/iOkJAWS", "0917323456", "Offline", "Teacher", "teacher3" },
                    { new Guid("f45d63b8-e00d-4624-a48d-4e8020770140"), "admin2@example.com", "Admin2", "User", null, "$2a$11$RLW.vehAGfShXkgw9RlQQeSLUakbn4OtL098v8hddPmuBfSNefvge", null, "Offline", "Admin", "admin2" }
                });

            migrationBuilder.InsertData(
                table: "Staff",
                columns: new[] { "Id", "Position" },
                values: new object[,]
                {
                    { new Guid("029a8a52-5c48-45d7-924a-c95e9bbd6fa8"), "Lab Technician" },
                    { new Guid("69e5c682-1836-4d67-85a7-8252f02b44e2"), "Lab Technician" },
                    { new Guid("912035aa-7e50-45dd-a44b-e248f74f9073"), "Lab Technician" },
                    { new Guid("915c6b5b-920f-4fae-a5d9-faca2b03bdb4"), "Lab Technician" },
                    { new Guid("a9c6c964-bc81-40a3-bb23-e835f7a97b96"), "Lab Technician" }
                });

            migrationBuilder.InsertData(
                table: "Students",
                columns: new[] { "Id", "BackStudentIdPicture", "CityMunicipality", "Course", "FrontStudentIdPicture", "PostalCode", "ProfilePicture", "Province", "Section", "Street", "StudentIdNumber", "Year" },
                values: new object[,]
                {
                    { new Guid("0e25db68-5a92-4b44-8d13-bc486d834e36"), null, "Sample City", "Computer Science", null, "12345", null, "Sample Province", "A", "123 Main St #3", "2023-0003", "3" },
                    { new Guid("25207d58-9584-4509-9315-0edc55767d26"), null, "Sample City", "Computer Science", null, "12345", null, "Sample Province", "A", "123 Main St #4", "2023-0004", "3" },
                    { new Guid("62625b62-bbce-4215-8e7d-f0c3e3e7be12"), null, "Sample City", "Computer Science", null, "12345", null, "Sample Province", "A", "123 Main St #5", "2023-0005", "3" },
                    { new Guid("7ce13a83-09f6-402a-9f1b-ab34ab2c1cbe"), null, "Sample City", "Computer Science", null, "12345", null, "Sample Province", "A", "123 Main St #2", "2023-0002", "3" },
                    { new Guid("9424ba2f-744c-4e74-9e44-a82cf2492257"), null, "Sample City", "Computer Science", null, "12345", null, "Sample Province", "A", "123 Main St #1", "2023-0001", "3" }
                });

            migrationBuilder.InsertData(
                table: "Teachers",
                columns: new[] { "Id", "Department" },
                values: new object[,]
                {
                    { new Guid("01d8c593-93d2-4c41-b452-aefad0b7e580"), "Information Technology" },
                    { new Guid("774b27ab-3283-4c1f-a613-4db341da56f1"), "Information Technology" },
                    { new Guid("bcbb79eb-1ec3-4789-aac8-21fc370060b7"), "Information Technology" },
                    { new Guid("d27a04a2-af24-4366-bb49-6ff428f16c8a"), "Information Technology" },
                    { new Guid("ee79979c-9068-49e5-a486-dffdfee6a094"), "Information Technology" }
                });
        }
    }
}
