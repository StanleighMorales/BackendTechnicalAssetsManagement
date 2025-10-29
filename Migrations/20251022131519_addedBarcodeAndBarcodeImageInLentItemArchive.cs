using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace BackendTechnicalAssetsManagement.Migrations
{
    /// <inheritdoc />
    public partial class addedBarcodeAndBarcodeImageInLentItemArchive : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
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

            migrationBuilder.AddColumn<string>(
                name: "Barcode",
                table: "ArchiveLentItems",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<byte[]>(
                name: "BarcodeImage",
                table: "ArchiveLentItems",
                type: "varbinary(max)",
                nullable: true);

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

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
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
                name: "Barcode",
                table: "ArchiveLentItems");

            migrationBuilder.DropColumn(
                name: "BarcodeImage",
                table: "ArchiveLentItems");

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
    }
}
