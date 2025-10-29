using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace BackendTechnicalAssetsManagement.Migrations
{
    /// <inheritdoc />
    public partial class AddedUserArchiveEntity : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Staff",
                keyColumn: "Id",
                keyValue: new Guid("041ba377-9a30-4156-8c5f-a5961271c443"));

            migrationBuilder.DeleteData(
                table: "Staff",
                keyColumn: "Id",
                keyValue: new Guid("2dd39013-e7e1-4784-88d2-b6340a47d800"));

            migrationBuilder.DeleteData(
                table: "Staff",
                keyColumn: "Id",
                keyValue: new Guid("2e3f3cdc-1be7-4939-8d9d-1f3e16dcf576"));

            migrationBuilder.DeleteData(
                table: "Staff",
                keyColumn: "Id",
                keyValue: new Guid("a9a896bb-ac21-46f2-9c83-2cccc6f39423"));

            migrationBuilder.DeleteData(
                table: "Staff",
                keyColumn: "Id",
                keyValue: new Guid("f896793b-9552-48be-9498-8246865b228c"));

            migrationBuilder.DeleteData(
                table: "Students",
                keyColumn: "Id",
                keyValue: new Guid("17a82329-4351-4f2d-9476-8f3b7cf2c350"));

            migrationBuilder.DeleteData(
                table: "Students",
                keyColumn: "Id",
                keyValue: new Guid("84dd15ea-7e10-4b42-bdd6-540ece2bac42"));

            migrationBuilder.DeleteData(
                table: "Students",
                keyColumn: "Id",
                keyValue: new Guid("9ddb677f-63f4-4d70-92f3-8b49cec3e154"));

            migrationBuilder.DeleteData(
                table: "Students",
                keyColumn: "Id",
                keyValue: new Guid("e207933a-ffe7-459a-be4d-8890f6f0ba5c"));

            migrationBuilder.DeleteData(
                table: "Students",
                keyColumn: "Id",
                keyValue: new Guid("ea2955dd-aad9-4fb3-a783-ecd0e303c176"));

            migrationBuilder.DeleteData(
                table: "Teachers",
                keyColumn: "Id",
                keyValue: new Guid("3c319bd1-279b-47b5-82fb-cc9570f4b6ad"));

            migrationBuilder.DeleteData(
                table: "Teachers",
                keyColumn: "Id",
                keyValue: new Guid("7a8be34e-fdc7-4a14-983e-a1be16c15220"));

            migrationBuilder.DeleteData(
                table: "Teachers",
                keyColumn: "Id",
                keyValue: new Guid("b7949929-f21b-462b-8b60-dc182fea7372"));

            migrationBuilder.DeleteData(
                table: "Teachers",
                keyColumn: "Id",
                keyValue: new Guid("d4daa4ef-3efb-407b-8ef3-efe40b333ef9"));

            migrationBuilder.DeleteData(
                table: "Teachers",
                keyColumn: "Id",
                keyValue: new Guid("d8590fac-34bd-4151-994d-76184b5efdf4"));

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("1f523569-73bb-4330-a6c0-7d98c88e1e66"));

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("64ca0a8a-bce1-4f9f-8cfd-24db9e3b5c0c"));

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("a8a2ee16-838b-44f0-8700-b4145a1de921"));

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("c7053500-2b39-4610-834a-77e212c291b4"));

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("f66b19e9-e46d-4b69-a14e-b1de42691290"));

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("fd422f75-1b24-41ef-85b3-96cc8201d838"));

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("041ba377-9a30-4156-8c5f-a5961271c443"));

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("17a82329-4351-4f2d-9476-8f3b7cf2c350"));

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("2dd39013-e7e1-4784-88d2-b6340a47d800"));

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("2e3f3cdc-1be7-4939-8d9d-1f3e16dcf576"));

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("3c319bd1-279b-47b5-82fb-cc9570f4b6ad"));

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("7a8be34e-fdc7-4a14-983e-a1be16c15220"));

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("84dd15ea-7e10-4b42-bdd6-540ece2bac42"));

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("9ddb677f-63f4-4d70-92f3-8b49cec3e154"));

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("a9a896bb-ac21-46f2-9c83-2cccc6f39423"));

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("b7949929-f21b-462b-8b60-dc182fea7372"));

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("d4daa4ef-3efb-407b-8ef3-efe40b333ef9"));

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("d8590fac-34bd-4151-994d-76184b5efdf4"));

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("e207933a-ffe7-459a-be4d-8890f6f0ba5c"));

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("ea2955dd-aad9-4fb3-a783-ecd0e303c176"));

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("f896793b-9552-48be-9498-8246865b228c"));

            migrationBuilder.CreateTable(
                name: "ArchiveUsers",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    OriginalUserId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    Username = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PasswordHash = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UserRole = table.Column<int>(type: "int", nullable: true),
                    LastName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    MiddleName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    FirstName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhoneNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Status = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ArchivedAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ArchiveUsers", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ArchiveStaff",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Position = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ArchiveStaff", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ArchiveStaff_ArchiveUsers_Id",
                        column: x => x.Id,
                        principalTable: "ArchiveUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ArchiveStudents",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ProfilePicture = table.Column<byte[]>(type: "varbinary(max)", nullable: true),
                    StudentIdNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Course = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Year = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Section = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Street = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CityMunicipality = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Province = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PostalCode = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    FrontStudentIdPicture = table.Column<byte[]>(type: "varbinary(max)", nullable: true),
                    BackStudentIdPicture = table.Column<byte[]>(type: "varbinary(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ArchiveStudents", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ArchiveStudents_ArchiveUsers_Id",
                        column: x => x.Id,
                        principalTable: "ArchiveUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ArchiveTeachers",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Department = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ArchiveTeachers", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ArchiveTeachers_ArchiveUsers_Id",
                        column: x => x.Id,
                        principalTable: "ArchiveUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

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

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ArchiveStaff");

            migrationBuilder.DropTable(
                name: "ArchiveStudents");

            migrationBuilder.DropTable(
                name: "ArchiveTeachers");

            migrationBuilder.DropTable(
                name: "ArchiveUsers");

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

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "Email", "FirstName", "LastName", "MiddleName", "PasswordHash", "PhoneNumber", "Status", "UserRole", "Username" },
                values: new object[,]
                {
                    { new Guid("041ba377-9a30-4156-8c5f-a5961271c443"), "staff1@example.com", "Staff1", "Doe", null, "$2a$11$ucdYcjkZFbYRspHT3SBu1ODbAhtjqrWPuBCJG7mag700Dmj3jO4kC", "0998176543", "Offline", "Staff", "staff1" },
                    { new Guid("17a82329-4351-4f2d-9476-8f3b7cf2c350"), "student2@example.com", "Student2", "Jones", null, "$2a$11$8JgD0aOoYNgGAowYT7jmEOf1xxjV.KJiHbNKP55.xrVFbG4YRU1qq", "0912234567", "Offline", "Student", "student2" },
                    { new Guid("1f523569-73bb-4330-a6c0-7d98c88e1e66"), "admin4@example.com", "Admin4", "User", null, "$2a$11$NlGhHxyM.91v9a3La5wiweLNJhIsK5ewqssxEa8IHcz.gbLPDtrGW", null, "Offline", "Admin", "admin4" },
                    { new Guid("2dd39013-e7e1-4784-88d2-b6340a47d800"), "staff3@example.com", "Staff3", "Doe", null, "$2a$11$3VVdGWk9BVTm/NOSMK6sXOSEMNiGITyK3QFMM.Ujd7PP5AGMDZqVi", "0998376543", "Offline", "Staff", "staff3" },
                    { new Guid("2e3f3cdc-1be7-4939-8d9d-1f3e16dcf576"), "staff4@example.com", "Staff4", "Doe", null, "$2a$11$knF58DODVJJZue7TIUz7Cu7T8nNeGJW40X9kmGY6BzKdHREmxNb22", "0998476543", "Offline", "Staff", "staff4" },
                    { new Guid("3c319bd1-279b-47b5-82fb-cc9570f4b6ad"), "teacher5@example.com", "Teacher5", "Smith", null, "$2a$11$PG3zPTG6Z8SuZbqmWm6VQuWpq9DqPOgjZ3Qsd7Qzp2L1s/r0MZqlu", "0917523456", "Offline", "Teacher", "teacher5" },
                    { new Guid("64ca0a8a-bce1-4f9f-8cfd-24db9e3b5c0c"), "admin2@example.com", "Admin2", "User", null, "$2a$11$AbntOnxQ2PTkhBbU4vTrU.V5Om8AUsXdeWfGd78wMP9gIGxNn7NH6", null, "Offline", "Admin", "admin2" },
                    { new Guid("7a8be34e-fdc7-4a14-983e-a1be16c15220"), "teacher4@example.com", "Teacher4", "Smith", null, "$2a$11$NwXG5W7mAvGjCtziUku4JOO9Cxg.tC4R1jUgKyoEg/T8i0uKONSfa", "0917423456", "Offline", "Teacher", "teacher4" },
                    { new Guid("84dd15ea-7e10-4b42-bdd6-540ece2bac42"), "student1@example.com", "Student1", "Jones", null, "$2a$11$9UPyf.HIUYcFKJo8aFUjduMrq8vAdweL0h6idrFWQuM6.CIXQ1PU6", "0912134567", "Offline", "Student", "student1" },
                    { new Guid("9ddb677f-63f4-4d70-92f3-8b49cec3e154"), "student5@example.com", "Student5", "Jones", null, "$2a$11$2fyhzD.Xvz7kQ02W651jAeXEqUoWgKcmyqcsadc60pIVPq6ROXU9C", "0912534567", "Offline", "Student", "student5" },
                    { new Guid("a8a2ee16-838b-44f0-8700-b4145a1de921"), "superadmin@example.com", "Super", "Admin", null, "$2a$11$XFZ1b09hkpyVP.EH2Z3mzOM.1Z2MxsHxXVSEl4CnbNrZS.xMok50a", null, "Offline", "SuperAdmin", "superadmin" },
                    { new Guid("a9a896bb-ac21-46f2-9c83-2cccc6f39423"), "staff5@example.com", "Staff5", "Doe", null, "$2a$11$NOY38miIdmuFQ/6wkJZR.OmHe.mMdP1aEi.Gm6EK39Oxw6t.mqXNG", "0998576543", "Offline", "Staff", "staff5" },
                    { new Guid("b7949929-f21b-462b-8b60-dc182fea7372"), "teacher1@example.com", "Teacher1", "Smith", null, "$2a$11$pOFQgdNtmufgDnPheTvQqe.T.PywsSu1q/1Po3x0cqqS5IRJLHE06", "0917123456", "Offline", "Teacher", "teacher1" },
                    { new Guid("c7053500-2b39-4610-834a-77e212c291b4"), "admin3@example.com", "Admin3", "User", null, "$2a$11$tpXdZopd3CyZ/yrvFZTOyuEWukECoy7FS5p1wgA91ErhzxJhN8ylW", null, "Offline", "Admin", "admin3" },
                    { new Guid("d4daa4ef-3efb-407b-8ef3-efe40b333ef9"), "teacher3@example.com", "Teacher3", "Smith", null, "$2a$11$BhIu1MaiMMqIQPveWshSy.WHpbBnxWgf35BN4awssjHsErarJX4VO", "0917323456", "Offline", "Teacher", "teacher3" },
                    { new Guid("d8590fac-34bd-4151-994d-76184b5efdf4"), "teacher2@example.com", "Teacher2", "Smith", null, "$2a$11$gTsv2shfLnJQXvOT0Q3HSeZoHKj.BWrx9qNXdRalxi25cdvbv4Cdi", "0917223456", "Offline", "Teacher", "teacher2" },
                    { new Guid("e207933a-ffe7-459a-be4d-8890f6f0ba5c"), "student4@example.com", "Student4", "Jones", null, "$2a$11$s0s4u6iF4C6afsyzplpBsO2C3xj7yOyD5RDkR0246ogyQEiIHhM/K", "0912434567", "Offline", "Student", "student4" },
                    { new Guid("ea2955dd-aad9-4fb3-a783-ecd0e303c176"), "student3@example.com", "Student3", "Jones", null, "$2a$11$c2Icj.nVOJySoDL09D23Oe5ihPn63s3hICASeZC2sKjETuiSuJylq", "0912334567", "Offline", "Student", "student3" },
                    { new Guid("f66b19e9-e46d-4b69-a14e-b1de42691290"), "admin5@example.com", "Admin5", "User", null, "$2a$11$ZQVC4QtIB9uGMhic6E1MceRn3vLFw.FKGRQ4q5U7j0HJU28j37aAG", null, "Offline", "Admin", "admin5" },
                    { new Guid("f896793b-9552-48be-9498-8246865b228c"), "staff2@example.com", "Staff2", "Doe", null, "$2a$11$c.AShHNlN6srsjGEDAN8seYT5MLuEoFUp4Ni/fJzGzaywhX8swadO", "0998276543", "Offline", "Staff", "staff2" },
                    { new Guid("fd422f75-1b24-41ef-85b3-96cc8201d838"), "admin1@example.com", "Admin1", "User", null, "$2a$11$sIYgu2Et6NRw/lv9ieamZOuYRZL0ZSiKeXtnNDHGSOc8KuSh//G4a", null, "Offline", "Admin", "admin1" }
                });

            migrationBuilder.InsertData(
                table: "Staff",
                columns: new[] { "Id", "Position" },
                values: new object[,]
                {
                    { new Guid("041ba377-9a30-4156-8c5f-a5961271c443"), "Lab Technician" },
                    { new Guid("2dd39013-e7e1-4784-88d2-b6340a47d800"), "Lab Technician" },
                    { new Guid("2e3f3cdc-1be7-4939-8d9d-1f3e16dcf576"), "Lab Technician" },
                    { new Guid("a9a896bb-ac21-46f2-9c83-2cccc6f39423"), "Lab Technician" },
                    { new Guid("f896793b-9552-48be-9498-8246865b228c"), "Lab Technician" }
                });

            migrationBuilder.InsertData(
                table: "Students",
                columns: new[] { "Id", "BackStudentIdPicture", "CityMunicipality", "Course", "FrontStudentIdPicture", "PostalCode", "ProfilePicture", "Province", "Section", "Street", "StudentIdNumber", "Year" },
                values: new object[,]
                {
                    { new Guid("17a82329-4351-4f2d-9476-8f3b7cf2c350"), null, "Sample City", "Computer Science", null, "12345", null, "Sample Province", "A", "123 Main St #2", "2023-0002", "3" },
                    { new Guid("84dd15ea-7e10-4b42-bdd6-540ece2bac42"), null, "Sample City", "Computer Science", null, "12345", null, "Sample Province", "A", "123 Main St #1", "2023-0001", "3" },
                    { new Guid("9ddb677f-63f4-4d70-92f3-8b49cec3e154"), null, "Sample City", "Computer Science", null, "12345", null, "Sample Province", "A", "123 Main St #5", "2023-0005", "3" },
                    { new Guid("e207933a-ffe7-459a-be4d-8890f6f0ba5c"), null, "Sample City", "Computer Science", null, "12345", null, "Sample Province", "A", "123 Main St #4", "2023-0004", "3" },
                    { new Guid("ea2955dd-aad9-4fb3-a783-ecd0e303c176"), null, "Sample City", "Computer Science", null, "12345", null, "Sample Province", "A", "123 Main St #3", "2023-0003", "3" }
                });

            migrationBuilder.InsertData(
                table: "Teachers",
                columns: new[] { "Id", "Department" },
                values: new object[,]
                {
                    { new Guid("3c319bd1-279b-47b5-82fb-cc9570f4b6ad"), "Information Technology" },
                    { new Guid("7a8be34e-fdc7-4a14-983e-a1be16c15220"), "Information Technology" },
                    { new Guid("b7949929-f21b-462b-8b60-dc182fea7372"), "Information Technology" },
                    { new Guid("d4daa4ef-3efb-407b-8ef3-efe40b333ef9"), "Information Technology" },
                    { new Guid("d8590fac-34bd-4151-994d-76184b5efdf4"), "Information Technology" }
                });
        }
    }
}
