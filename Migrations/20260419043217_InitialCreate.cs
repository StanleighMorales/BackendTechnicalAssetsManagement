using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace BackendTechnicalAssetsManagement.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ArchiveItems",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    SerialNumber = table.Column<string>(type: "text", nullable: false),
                    RfidUid = table.Column<string>(type: "text", nullable: true),
                    ImageUrl = table.Column<string>(type: "text", nullable: true),
                    ItemName = table.Column<string>(type: "text", nullable: false),
                    ItemType = table.Column<string>(type: "text", nullable: false),
                    ItemModel = table.Column<string>(type: "text", nullable: true),
                    ItemMake = table.Column<string>(type: "text", nullable: false),
                    Description = table.Column<string>(type: "text", nullable: true),
                    Category = table.Column<string>(type: "text", nullable: false),
                    Condition = table.Column<string>(type: "text", nullable: false),
                    Status = table.Column<string>(type: "text", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    Location = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ArchiveItems", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ArchiveUsers",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    OriginalUserId = table.Column<Guid>(type: "uuid", nullable: true),
                    Username = table.Column<string>(type: "text", nullable: true),
                    PasswordHash = table.Column<string>(type: "text", nullable: true),
                    Email = table.Column<string>(type: "text", nullable: true),
                    UserRole = table.Column<int>(type: "integer", nullable: true),
                    LastName = table.Column<string>(type: "text", nullable: true),
                    MiddleName = table.Column<string>(type: "text", nullable: true),
                    FirstName = table.Column<string>(type: "text", nullable: true),
                    PhoneNumber = table.Column<string>(type: "text", nullable: true),
                    Status = table.Column<string>(type: "text", nullable: true),
                    ArchivedAt = table.Column<DateTime>(type: "timestamp without time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ArchiveUsers", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Items",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    SerialNumber = table.Column<string>(type: "text", nullable: true),
                    RfidUid = table.Column<string>(type: "text", nullable: true),
                    ImageUrl = table.Column<string>(type: "text", nullable: true),
                    ItemName = table.Column<string>(type: "text", nullable: false),
                    ItemType = table.Column<string>(type: "text", nullable: false),
                    ItemModel = table.Column<string>(type: "text", nullable: true),
                    ItemMake = table.Column<string>(type: "text", nullable: false),
                    Description = table.Column<string>(type: "text", nullable: true),
                    Category = table.Column<string>(type: "text", nullable: false),
                    Condition = table.Column<string>(type: "text", nullable: false),
                    Status = table.Column<string>(type: "text", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    Location = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Items", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Username = table.Column<string>(type: "text", nullable: false),
                    PasswordHash = table.Column<string>(type: "text", nullable: true),
                    Email = table.Column<string>(type: "text", nullable: false),
                    UserRole = table.Column<string>(type: "text", nullable: false),
                    LastName = table.Column<string>(type: "text", nullable: false),
                    MiddleName = table.Column<string>(type: "text", nullable: true),
                    FirstName = table.Column<string>(type: "text", nullable: false),
                    PhoneNumber = table.Column<string>(type: "text", nullable: true),
                    Status = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ArchiveStaff",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Position = table.Column<string>(type: "text", nullable: true)
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
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    ProfilePicture = table.Column<byte[]>(type: "bytea", nullable: true),
                    StudentIdNumber = table.Column<string>(type: "text", nullable: true),
                    Course = table.Column<string>(type: "text", nullable: true),
                    Year = table.Column<string>(type: "text", nullable: true),
                    Section = table.Column<string>(type: "text", nullable: true),
                    Street = table.Column<string>(type: "text", nullable: true),
                    CityMunicipality = table.Column<string>(type: "text", nullable: true),
                    Province = table.Column<string>(type: "text", nullable: true),
                    PostalCode = table.Column<string>(type: "text", nullable: true),
                    FrontStudentIdPicture = table.Column<byte[]>(type: "bytea", nullable: true),
                    BackStudentIdPicture = table.Column<byte[]>(type: "bytea", nullable: true),
                    RfidUid = table.Column<string>(type: "text", nullable: true)
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
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Department = table.Column<string>(type: "text", nullable: true)
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

            migrationBuilder.CreateTable(
                name: "RefreshTokens",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Token = table.Column<string>(type: "text", nullable: false),
                    ExpiresAt = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    IsRevoked = table.Column<bool>(type: "boolean", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    RevokedAt = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    UserId = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RefreshTokens", x => x.Id);
                    table.ForeignKey(
                        name: "FK_RefreshTokens_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Staff",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Position = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Staff", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Staff_Users_Id",
                        column: x => x.Id,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Students",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    ProfilePictureUrl = table.Column<string>(type: "text", nullable: true),
                    StudentIdNumber = table.Column<string>(type: "text", nullable: true),
                    Course = table.Column<string>(type: "text", nullable: false),
                    Year = table.Column<string>(type: "text", nullable: false),
                    Section = table.Column<string>(type: "text", nullable: false),
                    Street = table.Column<string>(type: "text", nullable: false),
                    CityMunicipality = table.Column<string>(type: "text", nullable: false),
                    Province = table.Column<string>(type: "text", nullable: false),
                    PostalCode = table.Column<string>(type: "text", nullable: false),
                    FrontStudentIdPictureUrl = table.Column<string>(type: "text", nullable: true),
                    BackStudentIdPictureUrl = table.Column<string>(type: "text", nullable: true),
                    GeneratedPassword = table.Column<string>(type: "text", nullable: true),
                    RfidUid = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Students", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Students_Users_Id",
                        column: x => x.Id,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Teachers",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Department = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Teachers", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Teachers_Users_Id",
                        column: x => x.Id,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ArchiveLentItems",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    ItemId = table.Column<Guid>(type: "uuid", nullable: true),
                    ItemName = table.Column<string>(type: "text", nullable: false),
                    UserId = table.Column<Guid>(type: "uuid", nullable: true),
                    TeacherId = table.Column<Guid>(type: "uuid", nullable: true),
                    BorrowerFullName = table.Column<string>(type: "text", nullable: false),
                    BorrowerRole = table.Column<string>(type: "text", nullable: false),
                    StudentIdNumber = table.Column<string>(type: "text", nullable: true),
                    TeacherFullName = table.Column<string>(type: "text", nullable: true),
                    Room = table.Column<string>(type: "text", nullable: false),
                    SubjectTimeSchedule = table.Column<string>(type: "text", nullable: false),
                    LentAt = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    ReturnedAt = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    Status = table.Column<string>(type: "text", nullable: true),
                    Remarks = table.Column<string>(type: "text", nullable: true),
                    IsHiddenFromUser = table.Column<bool>(type: "boolean", nullable: false),
                    TagUid = table.Column<string>(type: "text", nullable: true),
                    StudentRfid = table.Column<string>(type: "text", nullable: true),
                    FrontStudentIdPictureUrl = table.Column<string>(type: "text", nullable: true),
                    GuestImageUrl = table.Column<string>(type: "text", nullable: true),
                    Organization = table.Column<string>(type: "text", nullable: true),
                    ContactNumber = table.Column<string>(type: "text", nullable: true),
                    Purpose = table.Column<string>(type: "text", nullable: true),
                    ReservedFor = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "timestamp without time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ArchiveLentItems", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ArchiveLentItems_Items_ItemId",
                        column: x => x.ItemId,
                        principalTable: "Items",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_ArchiveLentItems_Teachers_TeacherId",
                        column: x => x.TeacherId,
                        principalTable: "Teachers",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_ArchiveLentItems_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "LentItems",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    ItemId = table.Column<Guid>(type: "uuid", nullable: false),
                    ItemName = table.Column<string>(type: "text", nullable: false),
                    UserId = table.Column<Guid>(type: "uuid", nullable: true),
                    TeacherId = table.Column<Guid>(type: "uuid", nullable: true),
                    BorrowerFullName = table.Column<string>(type: "text", nullable: false),
                    BorrowerRole = table.Column<string>(type: "text", nullable: false),
                    StudentIdNumber = table.Column<string>(type: "text", nullable: true),
                    TeacherFullName = table.Column<string>(type: "text", nullable: true),
                    Room = table.Column<string>(type: "text", nullable: false),
                    SubjectTimeSchedule = table.Column<string>(type: "text", nullable: false),
                    ReservedFor = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    LentAt = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    ReturnedAt = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    Status = table.Column<string>(type: "text", nullable: true),
                    Remarks = table.Column<string>(type: "text", nullable: true),
                    IsHiddenFromUser = table.Column<bool>(type: "boolean", nullable: false),
                    FrontStudentIdPictureUrl = table.Column<string>(type: "text", nullable: true),
                    TagUid = table.Column<string>(type: "text", nullable: true),
                    StudentRfid = table.Column<string>(type: "text", nullable: true),
                    GuestImageUrl = table.Column<string>(type: "text", nullable: true),
                    Organization = table.Column<string>(type: "text", nullable: true),
                    ContactNumber = table.Column<string>(type: "text", nullable: true),
                    Purpose = table.Column<string>(type: "text", nullable: true),
                    IssuedById = table.Column<Guid>(type: "uuid", nullable: true),
                    IssuedByLastName = table.Column<string>(type: "text", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "timestamp without time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LentItems", x => x.Id);
                    table.ForeignKey(
                        name: "FK_LentItems_Items_ItemId",
                        column: x => x.ItemId,
                        principalTable: "Items",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_LentItems_Teachers_TeacherId",
                        column: x => x.TeacherId,
                        principalTable: "Teachers",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_LentItems_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "ActivityLogs",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Category = table.Column<int>(type: "integer", nullable: false),
                    Action = table.Column<string>(type: "text", nullable: false),
                    ActorUserId = table.Column<Guid>(type: "uuid", nullable: true),
                    ActorName = table.Column<string>(type: "text", nullable: false),
                    ActorRole = table.Column<string>(type: "text", nullable: false),
                    ItemId = table.Column<Guid>(type: "uuid", nullable: true),
                    ItemName = table.Column<string>(type: "text", nullable: false),
                    ItemSerialNumber = table.Column<string>(type: "text", nullable: true),
                    LentItemId = table.Column<Guid>(type: "uuid", nullable: true),
                    PreviousStatus = table.Column<string>(type: "text", nullable: true),
                    NewStatus = table.Column<string>(type: "text", nullable: true),
                    BorrowedAt = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    ReturnedAt = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    ReservedFor = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    Remarks = table.Column<string>(type: "text", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "timestamp without time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ActivityLogs", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ActivityLogs_Items_ItemId",
                        column: x => x.ItemId,
                        principalTable: "Items",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_ActivityLogs_LentItems_LentItemId",
                        column: x => x.LentItemId,
                        principalTable: "LentItems",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_ActivityLogs_Users_ActorUserId",
                        column: x => x.ActorUserId,
                        principalTable: "Users",
                        principalColumn: "Id");
                });

            migrationBuilder.InsertData(
                table: "ArchiveItems",
                columns: new[] { "Id", "Category", "Condition", "CreatedAt", "Description", "ImageUrl", "ItemMake", "ItemModel", "ItemName", "ItemType", "Location", "RfidUid", "SerialNumber", "Status", "UpdatedAt" },
                values: new object[,]
                {
                    { new Guid("00000007-0000-0000-0000-000000000005"), "Electronics", "Good", new DateTime(2024, 6, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, null, "Generic", null, "HDMI Cable 6ft", "Cable", null, null, "SN-HDMI-007", "Archived", new DateTime(2025, 1, 15, 10, 0, 0, 0, DateTimeKind.Utc) },
                    { new Guid("00000007-0000-0000-0000-000000000006"), "MediaEquipment", "Defective", new DateTime(2024, 6, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, null, "Blue", null, "USB Microphone", "Microphone", null, null, "SN-MIC-008", "Archived", new DateTime(2025, 1, 15, 10, 0, 0, 0, DateTimeKind.Utc) }
                });

            migrationBuilder.InsertData(
                table: "ArchiveUsers",
                columns: new[] { "Id", "ArchivedAt", "Email", "FirstName", "LastName", "MiddleName", "OriginalUserId", "PasswordHash", "PhoneNumber", "Status", "UserRole", "Username" },
                values: new object[,]
                {
                    { new Guid("00000007-0000-0000-0000-000000000001"), new DateTime(2025, 1, 15, 10, 0, 0, 0, DateTimeKind.Utc), "ana.reyes@gmail.com", "Ana", "Reyes", null, new Guid("00000001-0000-0000-0000-000000000004"), "$2a$11$2MsPT1w1IrQKGjo7.ZZ6duUDE9aS0P6P5qxULhHe//cLWRgRBXw/i", null, "Inactive", 1, "areyes" },
                    { new Guid("00000007-0000-0000-0000-000000000002"), new DateTime(2025, 1, 15, 10, 0, 0, 0, DateTimeKind.Utc), "miguel.torres@gmail.com", "Miguel", "Torres", null, new Guid("00000002-0000-0000-0000-000000000003"), "$2a$11$2MsPT1w1IrQKGjo7.ZZ6duUDE9aS0P6P5qxULhHe//cLWRgRBXw/i", null, "Inactive", 2, "mtorres" },
                    { new Guid("00000007-0000-0000-0000-000000000003"), new DateTime(2025, 1, 15, 10, 0, 0, 0, DateTimeKind.Utc), "david.ramos@gmail.com", "David", "Ramos", null, new Guid("00000003-0000-0000-0000-000000000004"), "$2a$11$2MsPT1w1IrQKGjo7.ZZ6duUDE9aS0P6P5qxULhHe//cLWRgRBXw/i", null, "Inactive", 3, "dramos" },
                    { new Guid("00000007-0000-0000-0000-000000000004"), new DateTime(2025, 1, 15, 10, 0, 0, 0, DateTimeKind.Utc), "sofia.gonzales@gmail.com", "Sofia", "Gonzales", null, new Guid("00000004-0000-0000-0000-000000000006"), "$2a$11$2MsPT1w1IrQKGjo7.ZZ6duUDE9aS0P6P5qxULhHe//cLWRgRBXw/i", null, "Inactive", 4, "sgonzales" }
                });

            migrationBuilder.InsertData(
                table: "Items",
                columns: new[] { "Id", "Category", "Condition", "CreatedAt", "Description", "ImageUrl", "ItemMake", "ItemModel", "ItemName", "ItemType", "Location", "RfidUid", "SerialNumber", "Status", "UpdatedAt" },
                values: new object[,]
                {
                    { new Guid("00000005-0000-0000-0000-000000000001"), "Electronics", "Good", new DateTime(2024, 6, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, null, "Generic", null, "HDMI Cable 10ft", "Cable", null, null, "SN-HDMI-001", "Borrowed", new DateTime(2024, 6, 1, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { new Guid("00000005-0000-0000-0000-000000000002"), "MediaEquipment", "Good", new DateTime(2024, 6, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, null, "Shure", null, "Wireless Microphone", "Microphone", null, null, "SN-MIC-002", "Borrowed", new DateTime(2024, 6, 1, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { new Guid("00000005-0000-0000-0000-000000000003"), "MediaEquipment", "Good", new DateTime(2024, 6, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, null, "JBL", null, "Portable Bluetooth Speaker", "Speaker", null, "RFID-ITEM-003", "SN-SPK-003", "Available", new DateTime(2024, 6, 1, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { new Guid("00000005-0000-0000-0000-000000000004"), "Electronics", "Good", new DateTime(2024, 6, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, null, "Logitech", null, "Wireless Mouse", "Peripheral", null, null, "SN-MOUSE-004", "Borrowed", new DateTime(2024, 6, 1, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { new Guid("00000005-0000-0000-0000-000000000005"), "Electronics", "Good", new DateTime(2024, 6, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, null, "Keychron", null, "Mechanical Keyboard", "Peripheral", null, "RFID-ITEM-005", "SN-KB-005", "Available", new DateTime(2024, 6, 1, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { new Guid("00000005-0000-0000-0000-000000000006"), "Electronics", "Good", new DateTime(2024, 6, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, null, "Generic", null, "Extension Wire 15ft", "Cable", null, "RFID-ITEM-006", "SN-EXT-006", "Available", new DateTime(2024, 6, 1, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { new Guid("00000005-0000-0000-0000-000000000007"), "Electronics", "Good", new DateTime(2024, 6, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, null, "Generic", null, "HDMI Cable 6ft", "Cable", null, null, "SN-HDMI-007", "Archived", new DateTime(2024, 6, 1, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { new Guid("00000005-0000-0000-0000-000000000008"), "MediaEquipment", "Defective", new DateTime(2024, 6, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, null, "Blue", null, "USB Microphone", "Microphone", null, null, "SN-MIC-008", "Archived", new DateTime(2024, 6, 1, 0, 0, 0, 0, DateTimeKind.Utc) }
                });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "Email", "FirstName", "LastName", "MiddleName", "PasswordHash", "PhoneNumber", "Status", "UserRole", "Username" },
                values: new object[,]
                {
                    { new Guid("00000001-0000-0000-0000-000000000001"), "superadmin@gmail.com", "Super", "Admin", null, "$2a$11$2MsPT1w1IrQKGjo7.ZZ6duUDE9aS0P6P5qxULhHe//cLWRgRBXw/i", null, "Active", "SuperAdmin", "superadmin" },
                    { new Guid("00000001-0000-0000-0000-000000000002"), "maria.santos@gmail.com", "Maria", "Santos", null, "$2a$11$2MsPT1w1IrQKGjo7.ZZ6duUDE9aS0P6P5qxULhHe//cLWRgRBXw/i", null, "Active", "Admin", "msantos" },
                    { new Guid("00000001-0000-0000-0000-000000000003"), "juan.delacruz@gmail.com", "Juan", "Dela Cruz", null, "$2a$11$2MsPT1w1IrQKGjo7.ZZ6duUDE9aS0P6P5qxULhHe//cLWRgRBXw/i", null, "Active", "Admin", "jdelacruz" },
                    { new Guid("00000001-0000-0000-0000-000000000004"), "ana.reyes@gmail.com", "Ana", "Reyes", null, "$2a$11$2MsPT1w1IrQKGjo7.ZZ6duUDE9aS0P6P5qxULhHe//cLWRgRBXw/i", null, "Inactive", "Admin", "areyes" },
                    { new Guid("00000002-0000-0000-0000-000000000001"), "carlos.mendoza@gmail.com", "Carlos", "Mendoza", null, "$2a$11$2MsPT1w1IrQKGjo7.ZZ6duUDE9aS0P6P5qxULhHe//cLWRgRBXw/i", null, "Active", "Staff", "cmendoza" },
                    { new Guid("00000002-0000-0000-0000-000000000002"), "rosa.garcia@gmail.com", "Rosa", "Garcia", null, "$2a$11$2MsPT1w1IrQKGjo7.ZZ6duUDE9aS0P6P5qxULhHe//cLWRgRBXw/i", null, "Active", "Staff", "rgarcia" },
                    { new Guid("00000002-0000-0000-0000-000000000003"), "miguel.torres@gmail.com", "Miguel", "Torres", null, "$2a$11$2MsPT1w1IrQKGjo7.ZZ6duUDE9aS0P6P5qxULhHe//cLWRgRBXw/i", null, "Inactive", "Staff", "mtorres" },
                    { new Guid("00000003-0000-0000-0000-000000000001"), "alice.williams@gmail.com", "Alice", "Williams", null, "$2a$11$2MsPT1w1IrQKGjo7.ZZ6duUDE9aS0P6P5qxULhHe//cLWRgRBXw/i", null, "Active", "Teacher", "awilliams" },
                    { new Guid("00000003-0000-0000-0000-000000000002"), "roberto.cruz@gmail.com", "Roberto", "Cruz", null, "$2a$11$2MsPT1w1IrQKGjo7.ZZ6duUDE9aS0P6P5qxULhHe//cLWRgRBXw/i", null, "Active", "Teacher", "rcruz" },
                    { new Guid("00000003-0000-0000-0000-000000000003"), "elena.fernandez@gmail.com", "Elena", "Fernandez", null, "$2a$11$2MsPT1w1IrQKGjo7.ZZ6duUDE9aS0P6P5qxULhHe//cLWRgRBXw/i", null, "Active", "Teacher", "efernandez" },
                    { new Guid("00000003-0000-0000-0000-000000000004"), "david.ramos@gmail.com", "David", "Ramos", null, "$2a$11$2MsPT1w1IrQKGjo7.ZZ6duUDE9aS0P6P5qxULhHe//cLWRgRBXw/i", null, "Inactive", "Teacher", "dramos" },
                    { new Guid("00000004-0000-0000-0000-000000000001"), "john.doe@gmail.com", "John", "Doe", null, "$2a$11$2MsPT1w1IrQKGjo7.ZZ6duUDE9aS0P6P5qxULhHe//cLWRgRBXw/i", null, "Active", "Student", "jdoe" },
                    { new Guid("00000004-0000-0000-0000-000000000002"), "jane.smith@gmail.com", "Jane", "Smith", null, "$2a$11$2MsPT1w1IrQKGjo7.ZZ6duUDE9aS0P6P5qxULhHe//cLWRgRBXw/i", null, "Active", "Student", "jsmith" },
                    { new Guid("00000004-0000-0000-0000-000000000003"), "peter.jones@gmail.com", "Peter", "Jones", null, "$2a$11$2MsPT1w1IrQKGjo7.ZZ6duUDE9aS0P6P5qxULhHe//cLWRgRBXw/i", null, "Active", "Student", "pjones" },
                    { new Guid("00000004-0000-0000-0000-000000000004"), "maria.lopez@gmail.com", "Maria", "Lopez", null, "$2a$11$2MsPT1w1IrQKGjo7.ZZ6duUDE9aS0P6P5qxULhHe//cLWRgRBXw/i", null, "Active", "Student", "mlopez" },
                    { new Guid("00000004-0000-0000-0000-000000000005"), "carlos.rivera@gmail.com", "Carlos", "Rivera", null, "$2a$11$2MsPT1w1IrQKGjo7.ZZ6duUDE9aS0P6P5qxULhHe//cLWRgRBXw/i", null, "Active", "Student", "crivera" },
                    { new Guid("00000004-0000-0000-0000-000000000006"), "sofia.gonzales@gmail.com", "Sofia", "Gonzales", null, "$2a$11$2MsPT1w1IrQKGjo7.ZZ6duUDE9aS0P6P5qxULhHe//cLWRgRBXw/i", null, "Inactive", "Student", "sgonzales" }
                });

            migrationBuilder.InsertData(
                table: "ArchiveStaff",
                columns: new[] { "Id", "Position" },
                values: new object[] { new Guid("00000007-0000-0000-0000-000000000002"), "IT Support" });

            migrationBuilder.InsertData(
                table: "ArchiveStudents",
                columns: new[] { "Id", "BackStudentIdPicture", "CityMunicipality", "Course", "FrontStudentIdPicture", "PostalCode", "ProfilePicture", "Province", "RfidUid", "Section", "Street", "StudentIdNumber", "Year" },
                values: new object[] { new Guid("00000007-0000-0000-0000-000000000004"), null, null, "Computer Science", null, null, null, null, null, "B", null, "2024-0001", "1st Year" });

            migrationBuilder.InsertData(
                table: "ArchiveTeachers",
                columns: new[] { "Id", "Department" },
                values: new object[] { new Guid("00000007-0000-0000-0000-000000000003"), "Multimedia Arts" });

            migrationBuilder.InsertData(
                table: "LentItems",
                columns: new[] { "Id", "BorrowerFullName", "BorrowerRole", "ContactNumber", "CreatedAt", "FrontStudentIdPictureUrl", "GuestImageUrl", "IsHiddenFromUser", "IssuedById", "IssuedByLastName", "ItemId", "ItemName", "LentAt", "Organization", "Purpose", "Remarks", "ReservedFor", "ReturnedAt", "Room", "Status", "StudentIdNumber", "StudentRfid", "SubjectTimeSchedule", "TagUid", "TeacherFullName", "TeacherId", "UpdatedAt", "UserId" },
                values: new object[,]
                {
                    { new Guid("00000006-0000-0000-0000-000000000001"), "John Doe", "Student", null, new DateTime(2025, 4, 11, 8, 0, 0, 0, DateTimeKind.Utc), null, null, false, null, null, new Guid("00000005-0000-0000-0000-000000000001"), "HDMI Cable 10ft", new DateTime(2025, 4, 11, 8, 0, 0, 0, DateTimeKind.Utc), null, null, null, null, null, "Lab 101", "Borrowed", "2023-0001", null, "CS301 - 8:00 AM", null, "", null, new DateTime(2025, 4, 11, 8, 0, 0, 0, DateTimeKind.Utc), new Guid("00000004-0000-0000-0000-000000000001") },
                    { new Guid("00000006-0000-0000-0000-000000000002"), "Jane Smith", "Student", null, new DateTime(2025, 4, 6, 10, 0, 0, 0, DateTimeKind.Utc), null, null, false, null, null, new Guid("00000005-0000-0000-0000-000000000003"), "Portable Bluetooth Speaker", new DateTime(2025, 4, 6, 10, 0, 0, 0, DateTimeKind.Utc), null, null, null, null, new DateTime(2025, 4, 14, 10, 0, 0, 0, DateTimeKind.Utc), "Lab 102", "Returned", "2023-0002", null, "IT201 - 10:00 AM", null, "", null, new DateTime(2025, 4, 14, 10, 0, 0, 0, DateTimeKind.Utc), new Guid("00000004-0000-0000-0000-000000000002") },
                    { new Guid("00000006-0000-0000-0000-000000000003"), "Peter Jones", "Student", null, new DateTime(2025, 4, 15, 13, 0, 0, 0, DateTimeKind.Utc), null, null, false, null, null, new Guid("00000005-0000-0000-0000-000000000002"), "Wireless Microphone", new DateTime(2025, 4, 15, 13, 0, 0, 0, DateTimeKind.Utc), null, null, null, null, null, "Lab 103", "Borrowed", "2023-0003", null, "CS302 - 1:00 PM", null, "", null, new DateTime(2025, 4, 15, 13, 0, 0, 0, DateTimeKind.Utc), new Guid("00000004-0000-0000-0000-000000000003") }
                });

            migrationBuilder.InsertData(
                table: "Staff",
                columns: new[] { "Id", "Position" },
                values: new object[,]
                {
                    { new Guid("00000002-0000-0000-0000-000000000001"), "Lab Technician" },
                    { new Guid("00000002-0000-0000-0000-000000000002"), "Equipment Manager" },
                    { new Guid("00000002-0000-0000-0000-000000000003"), "IT Support" }
                });

            migrationBuilder.InsertData(
                table: "Students",
                columns: new[] { "Id", "BackStudentIdPictureUrl", "CityMunicipality", "Course", "FrontStudentIdPictureUrl", "GeneratedPassword", "PostalCode", "ProfilePictureUrl", "Province", "RfidUid", "Section", "Street", "StudentIdNumber", "Year" },
                values: new object[,]
                {
                    { new Guid("00000004-0000-0000-0000-000000000001"), null, "", "Computer Science", null, null, "", null, "", null, "A", "", "2023-0001", "3rd Year" },
                    { new Guid("00000004-0000-0000-0000-000000000002"), null, "", "Information Technology", null, null, "", null, "", null, "B", "", "2023-0002", "2nd Year" },
                    { new Guid("00000004-0000-0000-0000-000000000003"), null, "", "Computer Science", null, null, "", null, "", null, "A", "", "2023-0003", "3rd Year" },
                    { new Guid("00000004-0000-0000-0000-000000000004"), null, "", "Multimedia Arts", null, null, "", null, "", null, "C", "", "2023-0004", "1st Year" },
                    { new Guid("00000004-0000-0000-0000-000000000005"), null, "", "Information Technology", null, null, "", null, "", null, "A", "", "2023-0005", "2nd Year" },
                    { new Guid("00000004-0000-0000-0000-000000000006"), null, "", "Computer Science", null, null, "", null, "", null, "B", "", "2024-0001", "1st Year" }
                });

            migrationBuilder.InsertData(
                table: "Teachers",
                columns: new[] { "Id", "Department" },
                values: new object[,]
                {
                    { new Guid("00000003-0000-0000-0000-000000000001"), "Information Technology" },
                    { new Guid("00000003-0000-0000-0000-000000000002"), "Computer Science" },
                    { new Guid("00000003-0000-0000-0000-000000000003"), "Information Technology" },
                    { new Guid("00000003-0000-0000-0000-000000000004"), "Multimedia Arts" }
                });

            migrationBuilder.InsertData(
                table: "ArchiveLentItems",
                columns: new[] { "Id", "BorrowerFullName", "BorrowerRole", "ContactNumber", "CreatedAt", "FrontStudentIdPictureUrl", "GuestImageUrl", "IsHiddenFromUser", "ItemId", "ItemName", "LentAt", "Organization", "Purpose", "Remarks", "ReservedFor", "ReturnedAt", "Room", "Status", "StudentIdNumber", "StudentRfid", "SubjectTimeSchedule", "TagUid", "TeacherFullName", "TeacherId", "UpdatedAt", "UserId" },
                values: new object[] { new Guid("00000007-0000-0000-0000-000000000007"), "David Ramos", "Teacher", null, new DateTime(2025, 1, 10, 11, 0, 0, 0, DateTimeKind.Utc), null, null, false, new Guid("00000005-0000-0000-0000-000000000007"), "HDMI Cable 6ft", new DateTime(2025, 1, 10, 11, 0, 0, 0, DateTimeKind.Utc), null, null, null, null, new DateTime(2025, 1, 14, 11, 0, 0, 0, DateTimeKind.Utc), "Room 301", "Returned", null, null, "MA101 - 11:00 AM", null, "David Ramos", new Guid("00000003-0000-0000-0000-000000000004"), new DateTime(2025, 1, 15, 10, 0, 0, 0, DateTimeKind.Utc), new Guid("00000003-0000-0000-0000-000000000004") });

            migrationBuilder.InsertData(
                table: "LentItems",
                columns: new[] { "Id", "BorrowerFullName", "BorrowerRole", "ContactNumber", "CreatedAt", "FrontStudentIdPictureUrl", "GuestImageUrl", "IsHiddenFromUser", "IssuedById", "IssuedByLastName", "ItemId", "ItemName", "LentAt", "Organization", "Purpose", "Remarks", "ReservedFor", "ReturnedAt", "Room", "Status", "StudentIdNumber", "StudentRfid", "SubjectTimeSchedule", "TagUid", "TeacherFullName", "TeacherId", "UpdatedAt", "UserId" },
                values: new object[,]
                {
                    { new Guid("00000006-0000-0000-0000-000000000004"), "Alice Williams", "Teacher", null, new DateTime(2025, 4, 13, 15, 0, 0, 0, DateTimeKind.Utc), null, null, false, null, null, new Guid("00000005-0000-0000-0000-000000000004"), "Wireless Mouse", new DateTime(2025, 4, 13, 15, 0, 0, 0, DateTimeKind.Utc), null, null, null, null, null, "Room 201", "Borrowed", null, null, "IT401 - 3:00 PM", null, "Alice Williams", new Guid("00000003-0000-0000-0000-000000000001"), new DateTime(2025, 4, 13, 15, 0, 0, 0, DateTimeKind.Utc), new Guid("00000003-0000-0000-0000-000000000001") },
                    { new Guid("00000006-0000-0000-0000-000000000005"), "Roberto Cruz", "Teacher", null, new DateTime(2025, 3, 17, 9, 0, 0, 0, DateTimeKind.Utc), null, null, false, null, null, new Guid("00000005-0000-0000-0000-000000000005"), "Mechanical Keyboard", new DateTime(2025, 3, 17, 9, 0, 0, 0, DateTimeKind.Utc), null, null, null, null, new DateTime(2025, 4, 1, 9, 0, 0, 0, DateTimeKind.Utc), "Room 202", "Returned", null, null, "CS201 - 9:00 AM", null, "Roberto Cruz", new Guid("00000003-0000-0000-0000-000000000002"), new DateTime(2025, 4, 1, 9, 0, 0, 0, DateTimeKind.Utc), new Guid("00000003-0000-0000-0000-000000000002") }
                });

            migrationBuilder.CreateIndex(
                name: "IX_ActivityLogs_ActorUserId",
                table: "ActivityLogs",
                column: "ActorUserId");

            migrationBuilder.CreateIndex(
                name: "IX_ActivityLogs_ItemId",
                table: "ActivityLogs",
                column: "ItemId");

            migrationBuilder.CreateIndex(
                name: "IX_ActivityLogs_LentItemId",
                table: "ActivityLogs",
                column: "LentItemId");

            migrationBuilder.CreateIndex(
                name: "IX_ArchiveLentItems_ItemId",
                table: "ArchiveLentItems",
                column: "ItemId");

            migrationBuilder.CreateIndex(
                name: "IX_ArchiveLentItems_TeacherId",
                table: "ArchiveLentItems",
                column: "TeacherId");

            migrationBuilder.CreateIndex(
                name: "IX_ArchiveLentItems_UserId",
                table: "ArchiveLentItems",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Items_RfidUid",
                table: "Items",
                column: "RfidUid",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Items_SerialNumber",
                table: "Items",
                column: "SerialNumber",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_LentItems_ItemId",
                table: "LentItems",
                column: "ItemId");

            migrationBuilder.CreateIndex(
                name: "IX_LentItems_TeacherId",
                table: "LentItems",
                column: "TeacherId");

            migrationBuilder.CreateIndex(
                name: "IX_LentItems_UserId",
                table: "LentItems",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_RefreshTokens_UserId",
                table: "RefreshTokens",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Students_StudentIdNumber",
                table: "Students",
                column: "StudentIdNumber",
                unique: true,
                filter: "(\"StudentIdNumber\" IS NOT NULL AND \"StudentIdNumber\" <> '')");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ActivityLogs");

            migrationBuilder.DropTable(
                name: "ArchiveItems");

            migrationBuilder.DropTable(
                name: "ArchiveLentItems");

            migrationBuilder.DropTable(
                name: "ArchiveStaff");

            migrationBuilder.DropTable(
                name: "ArchiveStudents");

            migrationBuilder.DropTable(
                name: "ArchiveTeachers");

            migrationBuilder.DropTable(
                name: "RefreshTokens");

            migrationBuilder.DropTable(
                name: "Staff");

            migrationBuilder.DropTable(
                name: "Students");

            migrationBuilder.DropTable(
                name: "LentItems");

            migrationBuilder.DropTable(
                name: "ArchiveUsers");

            migrationBuilder.DropTable(
                name: "Items");

            migrationBuilder.DropTable(
                name: "Teachers");

            migrationBuilder.DropTable(
                name: "Users");
        }
    }
}
