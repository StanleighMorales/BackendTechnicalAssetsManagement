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
                name: "Rfids",
                columns: table => new
                {
                    RfidUid = table.Column<string>(type: "text", nullable: false),
                    RfidCode = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Rfids", x => x.RfidUid);
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
                    Status = table.Column<string>(type: "text", nullable: true),
                    IsBlocked = table.Column<bool>(type: "boolean", nullable: false),
                    BlockReason = table.Column<string>(type: "text", nullable: true),
                    BlockedAt = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    BlockedUntil = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    BlockedById = table.Column<Guid>(type: "uuid", nullable: true)
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
                    RfidUid = table.Column<string>(type: "text", nullable: true),
                    RfidCode = table.Column<string>(type: "text", nullable: true)
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
                    Room = table.Column<string>(type: "text", nullable: true),
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
                    Room = table.Column<string>(type: "text", nullable: true),
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
                    { new Guid("00000007-0000-0000-0000-000000000005"), "Electronics", "Good", new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "6-foot HDMI cable, retired from service.", "https://oeyoyyxeluzaeckwpcsa.supabase.co/storage/v1/object/public/technical-bucket/items/item_06_hdmi_short.png", "Generic", null, "HDMI Cable 6ft", "Cable", "Storage Room", "RFID-ITEM-006", "SN-HDMI-006", "Archived", new DateTime(2025, 1, 15, 10, 0, 0, 0, DateTimeKind.Utc) },
                    { new Guid("00000007-0000-0000-0000-000000000006"), "MediaEquipment", "Defective", new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "USB condenser microphone, defective.", "https://oeyoyyxeluzaeckwpcsa.supabase.co/storage/v1/object/public/technical-bucket/items/item_07_usb_mic.png", "Blue", null, "USB Microphone", "Microphone", "Storage Room", "RFID-ITEM-007", "SN-MIC-007", "Archived", new DateTime(2025, 1, 15, 10, 0, 0, 0, DateTimeKind.Utc) },
                    { new Guid("00000007-0000-0000-0000-000000000007"), "Electronics", "NeedRepair", new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "Wireless optical mouse, needs repair.", "https://oeyoyyxeluzaeckwpcsa.supabase.co/storage/v1/object/public/technical-bucket/items/item_08_mouse.png", "Logitech", null, "Wireless Mouse", "Peripheral", "Storage Room", "RFID-ITEM-008", "SN-MOUSE-008", "Archived", new DateTime(2025, 1, 15, 10, 0, 0, 0, DateTimeKind.Utc) },
                    { new Guid("00000007-0000-0000-0000-000000000008"), "Electronics", "Good", new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "15-foot extension wire, retired.", "https://oeyoyyxeluzaeckwpcsa.supabase.co/storage/v1/object/public/technical-bucket/items/item_09_extension.png", "Generic", null, "Extension Wire 15ft", "Cable", "Storage Room", "RFID-ITEM-009", "SN-EXT-009", "Archived", new DateTime(2025, 1, 15, 10, 0, 0, 0, DateTimeKind.Utc) },
                    { new Guid("00000007-0000-0000-0000-000000000009"), "Electronics", "Refurbished", new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "7-port USB 3.0 hub, refurbished.", "https://oeyoyyxeluzaeckwpcsa.supabase.co/storage/v1/object/public/technical-bucket/items/item_10_usb_hub.png", "Anker", null, "USB Hub 7-Port", "Peripheral", "Storage Room", "RFID-ITEM-010", "SN-HUB-010", "Archived", new DateTime(2025, 1, 15, 10, 0, 0, 0, DateTimeKind.Utc) }
                });

            migrationBuilder.InsertData(
                table: "ArchiveUsers",
                columns: new[] { "Id", "ArchivedAt", "Email", "FirstName", "LastName", "MiddleName", "OriginalUserId", "PasswordHash", "PhoneNumber", "Status", "UserRole", "Username" },
                values: new object[,]
                {
                    { new Guid("00000007-0000-0000-0000-000000000001"), new DateTime(2025, 1, 15, 10, 0, 0, 0, DateTimeKind.Utc), "archived.admin@school.edu.ph", "Archived", "Admin", null, new Guid("00000001-0000-0000-0000-000000000005"), "$2a$04$/xsMayMOudu2SeAkPeObC.k7YMiQcG/1J0zz0.EmpICuCJ9kQVNh.", null, "Inactive", 1, "archived.admin" },
                    { new Guid("00000007-0000-0000-0000-000000000002"), new DateTime(2025, 1, 15, 10, 0, 0, 0, DateTimeKind.Utc), "archived.staff@school.edu.ph", "Archived", "Staff", null, new Guid("00000002-0000-0000-0000-000000000005"), "$2a$04$/xsMayMOudu2SeAkPeObC.k7YMiQcG/1J0zz0.EmpICuCJ9kQVNh.", null, "Inactive", 2, "archived.staff" },
                    { new Guid("00000007-0000-0000-0000-000000000003"), new DateTime(2025, 1, 15, 10, 0, 0, 0, DateTimeKind.Utc), "archived.teacher@school.edu.ph", "Archived", "Teacher", null, new Guid("00000003-0000-0000-0000-000000000005"), "$2a$04$/xsMayMOudu2SeAkPeObC.k7YMiQcG/1J0zz0.EmpICuCJ9kQVNh.", null, "Inactive", 3, "archived.teacher" },
                    { new Guid("00000007-0000-0000-0000-000000000004"), new DateTime(2025, 1, 15, 10, 0, 0, 0, DateTimeKind.Utc), "archived.student@school.edu.ph", "Archived", "Student", null, new Guid("00000004-0000-0000-0000-000000000005"), "$2a$04$/xsMayMOudu2SeAkPeObC.k7YMiQcG/1J0zz0.EmpICuCJ9kQVNh.", null, "Inactive", 4, "archived.student" }
                });

            migrationBuilder.InsertData(
                table: "Items",
                columns: new[] { "Id", "Category", "Condition", "CreatedAt", "Description", "ImageUrl", "ItemMake", "ItemModel", "ItemName", "ItemType", "Location", "RfidUid", "SerialNumber", "Status", "UpdatedAt" },
                values: new object[,]
                {
                    { new Guid("00000005-0000-0000-0000-000000000001"), "Electronics", "Good", new DateTime(2024, 6, 1, 0, 0, 0, 0, DateTimeKind.Utc), "10-foot HDMI 2.0 cable for display connections.", "https://oeyoyyxeluzaeckwpcsa.supabase.co/storage/v1/object/public/technical-bucket/items/item_01_hdmi_cable.png", "Generic", "Standard HDMI 2.0", "HDMI Cable 10ft", "Cable", "Lab Cabinet A", "RFID-ITEM-001", "SN-HDMI-001", "Borrowed", new DateTime(2025, 4, 11, 8, 0, 0, 0, DateTimeKind.Utc) },
                    { new Guid("00000005-0000-0000-0000-000000000002"), "MediaEquipment", "Good", new DateTime(2024, 6, 1, 0, 0, 0, 0, DateTimeKind.Utc), "Dual-channel wireless microphone system.", "https://oeyoyyxeluzaeckwpcsa.supabase.co/storage/v1/object/public/technical-bucket/items/item_02_wireless_mic.png", "Shure", "BLX288/PG58", "Wireless Microphone", "Microphone", "Media Room Shelf", "RFID-ITEM-002", "SN-MIC-002", "Unavailable", new DateTime(2025, 4, 20, 9, 0, 0, 0, DateTimeKind.Utc) },
                    { new Guid("00000005-0000-0000-0000-000000000003"), "MediaEquipment", "Good", new DateTime(2024, 6, 1, 0, 0, 0, 0, DateTimeKind.Utc), "3600-lumen portable LCD projector.", "https://oeyoyyxeluzaeckwpcsa.supabase.co/storage/v1/object/public/technical-bucket/items/item_03_projector.png", "Epson", "EB-X41", "Portable Projector", "Projector", "AV Room Cabinet", "RFID-ITEM-003", "SN-PROJ-003", "Reserved", new DateTime(2025, 4, 19, 10, 0, 0, 0, DateTimeKind.Utc) },
                    { new Guid("00000005-0000-0000-0000-000000000004"), "MediaEquipment", "Good", new DateTime(2024, 6, 1, 0, 0, 0, 0, DateTimeKind.Utc), "Portable waterproof Bluetooth speaker.", "https://oeyoyyxeluzaeckwpcsa.supabase.co/storage/v1/object/public/technical-bucket/items/item_04_speaker.png", "JBL", "Charge 5", "Bluetooth Speaker", "Speaker", "Lab Cabinet B", "RFID-ITEM-004", "SN-SPK-004", "Available", new DateTime(2025, 4, 10, 14, 0, 0, 0, DateTimeKind.Utc) },
                    { new Guid("00000005-0000-0000-0000-000000000005"), "Electronics", "Good", new DateTime(2024, 6, 1, 0, 0, 0, 0, DateTimeKind.Utc), "Compact TKL mechanical keyboard with RGB.", "https://oeyoyyxeluzaeckwpcsa.supabase.co/storage/v1/object/public/technical-bucket/items/item_05_keyboard.png", "Keychron", "K2 Pro", "Mechanical Keyboard", "Peripheral", "Lab Cabinet A", "RFID-ITEM-005", "SN-KB-005", "Available", new DateTime(2025, 3, 25, 9, 0, 0, 0, DateTimeKind.Utc) },
                    { new Guid("00000005-0000-0000-0000-000000000006"), "Electronics", "Good", new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "6-foot HDMI cable, retired from service.", "https://oeyoyyxeluzaeckwpcsa.supabase.co/storage/v1/object/public/technical-bucket/items/item_06_hdmi_short.png", "Generic", null, "HDMI Cable 6ft", "Cable", "Storage Room", "RFID-ITEM-006", "SN-HDMI-006", "Archived", new DateTime(2025, 1, 15, 10, 0, 0, 0, DateTimeKind.Utc) },
                    { new Guid("00000005-0000-0000-0000-000000000007"), "MediaEquipment", "Defective", new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "USB condenser microphone, defective.", "https://oeyoyyxeluzaeckwpcsa.supabase.co/storage/v1/object/public/technical-bucket/items/item_07_usb_mic.png", "Blue", null, "USB Microphone", "Microphone", "Storage Room", "RFID-ITEM-007", "SN-MIC-007", "Archived", new DateTime(2025, 1, 15, 10, 0, 0, 0, DateTimeKind.Utc) },
                    { new Guid("00000005-0000-0000-0000-000000000008"), "Electronics", "NeedRepair", new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "Wireless optical mouse, needs repair.", "https://oeyoyyxeluzaeckwpcsa.supabase.co/storage/v1/object/public/technical-bucket/items/item_08_mouse.png", "Logitech", null, "Wireless Mouse", "Peripheral", "Storage Room", "RFID-ITEM-008", "SN-MOUSE-008", "Archived", new DateTime(2025, 1, 15, 10, 0, 0, 0, DateTimeKind.Utc) },
                    { new Guid("00000005-0000-0000-0000-000000000009"), "Electronics", "Good", new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "15-foot extension wire, retired.", "https://oeyoyyxeluzaeckwpcsa.supabase.co/storage/v1/object/public/technical-bucket/items/item_09_extension.png", "Generic", null, "Extension Wire 15ft", "Cable", "Storage Room", "RFID-ITEM-009", "SN-EXT-009", "Archived", new DateTime(2025, 1, 15, 10, 0, 0, 0, DateTimeKind.Utc) },
                    { new Guid("00000005-0000-0000-0000-000000000010"), "Electronics", "Refurbished", new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "7-port USB 3.0 hub, refurbished.", "https://oeyoyyxeluzaeckwpcsa.supabase.co/storage/v1/object/public/technical-bucket/items/item_10_usb_hub.png", "Anker", null, "USB Hub 7-Port", "Peripheral", "Storage Room", "RFID-ITEM-010", "SN-HUB-010", "Archived", new DateTime(2025, 1, 15, 10, 0, 0, 0, DateTimeKind.Utc) }
                });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "BlockReason", "BlockedAt", "BlockedById", "BlockedUntil", "Email", "FirstName", "IsBlocked", "LastName", "MiddleName", "PasswordHash", "PhoneNumber", "Status", "UserRole", "Username" },
                values: new object[,]
                {
                    { new Guid("00000001-0000-0000-0000-000000000001"), null, null, null, null, "superadmin@school.edu.ph", "Super", false, "Admin", null, "$2a$04$/xsMayMOudu2SeAkPeObC.k7YMiQcG/1J0zz0.EmpICuCJ9kQVNh.", null, "Offline", "SuperAdmin", "sadmin" },
                    { new Guid("00000001-0000-0000-0000-000000000002"), null, null, null, null, "christian.admin@school.edu.ph", "Christian", false, "Dela Cruz", null, "$2a$04$/xsMayMOudu2SeAkPeObC.k7YMiQcG/1J0zz0.EmpICuCJ9kQVNh.", null, "Offline", "Admin", "christian" },
                    { new Guid("00000001-0000-0000-0000-000000000003"), null, null, null, null, "ejay.admin@school.edu.ph", "Ejay", false, "Santos", null, "$2a$04$/xsMayMOudu2SeAkPeObC.k7YMiQcG/1J0zz0.EmpICuCJ9kQVNh.", null, "Offline", "Admin", "ejay" },
                    { new Guid("00000001-0000-0000-0000-000000000004"), null, null, null, null, "stan.admin@school.edu.ph", "Stan", false, "Reyes", null, "$2a$04$/xsMayMOudu2SeAkPeObC.k7YMiQcG/1J0zz0.EmpICuCJ9kQVNh.", null, "Offline", "Admin", "stan" },
                    { new Guid("00000001-0000-0000-0000-000000000005"), null, null, null, null, "archived.admin@school.edu.ph", "Archived", false, "Admin", null, "$2a$04$/xsMayMOudu2SeAkPeObC.k7YMiQcG/1J0zz0.EmpICuCJ9kQVNh.", null, "Inactive", "Admin", "archived.admin" },
                    { new Guid("00000002-0000-0000-0000-000000000001"), null, null, null, null, "carlos.mendoza@school.edu.ph", "Carlos", false, "Mendoza", null, "$2a$04$/xsMayMOudu2SeAkPeObC.k7YMiQcG/1J0zz0.EmpICuCJ9kQVNh.", null, "Offline", "Staff", "cmendoza" },
                    { new Guid("00000002-0000-0000-0000-000000000002"), null, null, null, null, "rosa.garcia@school.edu.ph", "Rosa", false, "Garcia", null, "$2a$04$/xsMayMOudu2SeAkPeObC.k7YMiQcG/1J0zz0.EmpICuCJ9kQVNh.", null, "Offline", "Staff", "rgarcia" },
                    { new Guid("00000002-0000-0000-0000-000000000003"), null, null, null, null, "ben.torres@school.edu.ph", "Ben", false, "Torres", null, "$2a$04$/xsMayMOudu2SeAkPeObC.k7YMiQcG/1J0zz0.EmpICuCJ9kQVNh.", null, "Offline", "Staff", "btorres" },
                    { new Guid("00000002-0000-0000-0000-000000000004"), null, null, null, null, "liza.villanueva@school.edu.ph", "Liza", false, "Villanueva", null, "$2a$04$/xsMayMOudu2SeAkPeObC.k7YMiQcG/1J0zz0.EmpICuCJ9kQVNh.", null, "Offline", "Staff", "lvillanueva" },
                    { new Guid("00000002-0000-0000-0000-000000000005"), null, null, null, null, "archived.staff@school.edu.ph", "Archived", false, "Staff", null, "$2a$04$/xsMayMOudu2SeAkPeObC.k7YMiQcG/1J0zz0.EmpICuCJ9kQVNh.", null, "Inactive", "Staff", "archived.staff" },
                    { new Guid("00000003-0000-0000-0000-000000000001"), null, null, null, null, "alice.williams@school.edu.ph", "Alice", false, "Williams", null, "$2a$04$/xsMayMOudu2SeAkPeObC.k7YMiQcG/1J0zz0.EmpICuCJ9kQVNh.", null, "Offline", "Teacher", "awilliams" },
                    { new Guid("00000003-0000-0000-0000-000000000002"), null, null, null, null, "roberto.cruz@school.edu.ph", "Roberto", false, "Cruz", null, "$2a$04$/xsMayMOudu2SeAkPeObC.k7YMiQcG/1J0zz0.EmpICuCJ9kQVNh.", null, "Offline", "Teacher", "rcruz" },
                    { new Guid("00000003-0000-0000-0000-000000000003"), null, null, null, null, "elena.fernandez@school.edu.ph", "Elena", false, "Fernandez", null, "$2a$04$/xsMayMOudu2SeAkPeObC.k7YMiQcG/1J0zz0.EmpICuCJ9kQVNh.", null, "Offline", "Teacher", "efernandez" },
                    { new Guid("00000003-0000-0000-0000-000000000004"), null, null, null, null, "jose.miranda@school.edu.ph", "Jose", false, "Miranda", null, "$2a$04$/xsMayMOudu2SeAkPeObC.k7YMiQcG/1J0zz0.EmpICuCJ9kQVNh.", null, "Offline", "Teacher", "jmiranda" },
                    { new Guid("00000003-0000-0000-0000-000000000005"), null, null, null, null, "archived.teacher@school.edu.ph", "Archived", false, "Teacher", null, "$2a$04$/xsMayMOudu2SeAkPeObC.k7YMiQcG/1J0zz0.EmpICuCJ9kQVNh.", null, "Inactive", "Teacher", "archived.teacher" },
                    { new Guid("00000004-0000-0000-0000-000000000001"), null, null, null, null, "juan.delacruz@school.edu.ph", "Juan", false, "Dela Cruz", null, "$2a$04$/xsMayMOudu2SeAkPeObC.k7YMiQcG/1J0zz0.EmpICuCJ9kQVNh.", "09171234501", "Offline", "Student", "jdelacruz" },
                    { new Guid("00000004-0000-0000-0000-000000000002"), null, null, null, null, "maria.santos@school.edu.ph", "Maria", false, "Santos", null, "$2a$04$/xsMayMOudu2SeAkPeObC.k7YMiQcG/1J0zz0.EmpICuCJ9kQVNh.", "09171234502", "Offline", "Student", "msantos" },
                    { new Guid("00000004-0000-0000-0000-000000000003"), null, null, null, null, "pedro.reyes@school.edu.ph", "Pedro", false, "Reyes", null, "$2a$04$/xsMayMOudu2SeAkPeObC.k7YMiQcG/1J0zz0.EmpICuCJ9kQVNh.", "09171234503", "Offline", "Student", "preyes" },
                    { new Guid("00000004-0000-0000-0000-000000000004"), null, null, null, null, "ana.garcia@school.edu.ph", "Ana", false, "Garcia", null, "$2a$04$/xsMayMOudu2SeAkPeObC.k7YMiQcG/1J0zz0.EmpICuCJ9kQVNh.", "09171234504", "Offline", "Student", "agarcia" },
                    { new Guid("00000004-0000-0000-0000-000000000005"), null, null, null, null, "archived.student@school.edu.ph", "Archived", false, "Student", null, "$2a$04$/xsMayMOudu2SeAkPeObC.k7YMiQcG/1J0zz0.EmpICuCJ9kQVNh.", "09170000000", "Inactive", "Student", "archived.student" }
                });

            migrationBuilder.InsertData(
                table: "ArchiveLentItems",
                columns: new[] { "Id", "BorrowerFullName", "BorrowerRole", "ContactNumber", "CreatedAt", "FrontStudentIdPictureUrl", "GuestImageUrl", "IsHiddenFromUser", "ItemId", "ItemName", "LentAt", "Organization", "Purpose", "Remarks", "ReservedFor", "ReturnedAt", "Room", "Status", "StudentIdNumber", "StudentRfid", "SubjectTimeSchedule", "TagUid", "TeacherFullName", "TeacherId", "UpdatedAt", "UserId" },
                values: new object[,]
                {
                    { new Guid("00000007-0000-0000-0000-000000000011"), "Archived Student", "Student", null, new DateTime(2024, 12, 5, 14, 0, 0, 0, DateTimeKind.Utc), null, null, false, new Guid("00000005-0000-0000-0000-000000000007"), "USB Microphone", new DateTime(2024, 12, 5, 14, 0, 0, 0, DateTimeKind.Utc), null, null, null, null, new DateTime(2024, 12, 7, 14, 0, 0, 0, DateTimeKind.Utc), "Lab 201", "Returned", "2022-0099", null, "IT301 - 2:00 PM", null, "", null, new DateTime(2024, 12, 7, 14, 0, 0, 0, DateTimeKind.Utc), new Guid("00000004-0000-0000-0000-000000000005") },
                    { new Guid("00000007-0000-0000-0000-000000000012"), "Juan Dela Cruz", "Student", null, new DateTime(2024, 11, 20, 8, 0, 0, 0, DateTimeKind.Utc), null, null, false, new Guid("00000005-0000-0000-0000-000000000008"), "Wireless Mouse", new DateTime(2024, 11, 20, 8, 0, 0, 0, DateTimeKind.Utc), null, null, null, null, new DateTime(2024, 11, 22, 8, 0, 0, 0, DateTimeKind.Utc), "Lab 101", "Returned", "2023-0001", null, "CS201 - 8:00 AM", null, "", null, new DateTime(2024, 11, 22, 8, 0, 0, 0, DateTimeKind.Utc), new Guid("00000004-0000-0000-0000-000000000001") },
                    { new Guid("00000007-0000-0000-0000-000000000014"), "Maria Santos", "Student", null, new DateTime(2024, 9, 9, 10, 0, 0, 0, DateTimeKind.Utc), null, null, false, new Guid("00000005-0000-0000-0000-000000000010"), "USB Hub 7-Port", null, null, null, null, new DateTime(2024, 9, 10, 10, 0, 0, 0, DateTimeKind.Utc), null, "Lab 102", "Expired", "2023-0002", null, "IT201 - 10:00 AM", null, "", null, new DateTime(2024, 9, 10, 10, 31, 0, 0, DateTimeKind.Utc), new Guid("00000004-0000-0000-0000-000000000002") }
                });

            migrationBuilder.InsertData(
                table: "ArchiveStaff",
                columns: new[] { "Id", "Position" },
                values: new object[] { new Guid("00000007-0000-0000-0000-000000000002"), "Former Technician" });

            migrationBuilder.InsertData(
                table: "ArchiveStudents",
                columns: new[] { "Id", "BackStudentIdPicture", "CityMunicipality", "Course", "FrontStudentIdPicture", "PostalCode", "ProfilePicture", "Province", "RfidUid", "Section", "Street", "StudentIdNumber", "Year" },
                values: new object[] { new Guid("00000007-0000-0000-0000-000000000004"), null, null, "Bachelor of Science in Information Technology", null, null, null, null, null, "D", null, "2022-0099", "4th Year" });

            migrationBuilder.InsertData(
                table: "ArchiveTeachers",
                columns: new[] { "Id", "Department" },
                values: new object[] { new Guid("00000007-0000-0000-0000-000000000003"), "Former Department" });

            migrationBuilder.InsertData(
                table: "LentItems",
                columns: new[] { "Id", "BorrowerFullName", "BorrowerRole", "ContactNumber", "CreatedAt", "FrontStudentIdPictureUrl", "GuestImageUrl", "IsHiddenFromUser", "IssuedById", "IssuedByLastName", "ItemId", "ItemName", "LentAt", "Organization", "Purpose", "Remarks", "ReservedFor", "ReturnedAt", "Room", "Status", "StudentIdNumber", "StudentRfid", "SubjectTimeSchedule", "TagUid", "TeacherFullName", "TeacherId", "UpdatedAt", "UserId" },
                values: new object[,]
                {
                    { new Guid("00000006-0000-0000-0000-000000000001"), "Juan Dela Cruz", "Student", null, new DateTime(2025, 4, 20, 8, 0, 0, 0, DateTimeKind.Utc), "https://oeyoyyxeluzaeckwpcsa.supabase.co/storage/v1/object/public/technical-bucket/students/id-front/student_01_id_front.png", null, false, null, null, new Guid("00000005-0000-0000-0000-000000000001"), "HDMI Cable 10ft", new DateTime(2025, 4, 20, 8, 0, 0, 0, DateTimeKind.Utc), null, null, null, null, null, "Lab 101", "Borrowed", "2023-0001", "RFID-STU-001", "CS301 - 8:00 AM", "RFID-ITEM-001", "", null, new DateTime(2025, 4, 20, 8, 0, 0, 0, DateTimeKind.Utc), new Guid("00000004-0000-0000-0000-000000000001") },
                    { new Guid("00000006-0000-0000-0000-000000000002"), "Maria Santos", "Student", null, new DateTime(2025, 4, 20, 9, 0, 0, 0, DateTimeKind.Utc), "https://oeyoyyxeluzaeckwpcsa.supabase.co/storage/v1/object/public/technical-bucket/students/id-front/student_02_id_front.png", null, false, null, null, new Guid("00000005-0000-0000-0000-000000000002"), "Wireless Microphone", null, null, null, null, new DateTime(2025, 4, 27, 10, 0, 0, 0, DateTimeKind.Utc), null, "Lab 102", "Pending", "2023-0002", "RFID-STU-002", "IT201 - 10:00 AM", "RFID-ITEM-002", "", null, new DateTime(2025, 4, 20, 9, 0, 0, 0, DateTimeKind.Utc), new Guid("00000004-0000-0000-0000-000000000002") },
                    { new Guid("00000006-0000-0000-0000-000000000004"), "Pedro Reyes", "Student", null, new DateTime(2025, 4, 10, 13, 0, 0, 0, DateTimeKind.Utc), "https://oeyoyyxeluzaeckwpcsa.supabase.co/storage/v1/object/public/technical-bucket/students/id-front/student_03_id_front.png", null, false, null, null, new Guid("00000005-0000-0000-0000-000000000004"), "Bluetooth Speaker", new DateTime(2025, 4, 10, 13, 0, 0, 0, DateTimeKind.Utc), null, null, null, null, new DateTime(2025, 4, 14, 15, 0, 0, 0, DateTimeKind.Utc), "Lab 103", "Returned", "2023-0003", "RFID-STU-003", "CS302 - 1:00 PM", "RFID-ITEM-004", "", null, new DateTime(2025, 4, 14, 15, 0, 0, 0, DateTimeKind.Utc), new Guid("00000004-0000-0000-0000-000000000003") }
                });

            migrationBuilder.InsertData(
                table: "Staff",
                columns: new[] { "Id", "Position" },
                values: new object[,]
                {
                    { new Guid("00000002-0000-0000-0000-000000000001"), "Lab Technician" },
                    { new Guid("00000002-0000-0000-0000-000000000002"), "Equipment Manager" },
                    { new Guid("00000002-0000-0000-0000-000000000003"), "IT Support" },
                    { new Guid("00000002-0000-0000-0000-000000000004"), "Asset Custodian" },
                    { new Guid("00000002-0000-0000-0000-000000000005"), "Former Technician" }
                });

            migrationBuilder.InsertData(
                table: "Students",
                columns: new[] { "Id", "BackStudentIdPictureUrl", "CityMunicipality", "Course", "FrontStudentIdPictureUrl", "GeneratedPassword", "PostalCode", "ProfilePictureUrl", "Province", "RfidCode", "RfidUid", "Section", "Street", "StudentIdNumber", "Year" },
                values: new object[,]
                {
                    { new Guid("00000004-0000-0000-0000-000000000001"), "https://oeyoyyxeluzaeckwpcsa.supabase.co/storage/v1/object/public/technical-bucket/students/id-back/student_01_id_back.png", "Quezon City", "Bachelor of Science in Computer Science", "https://oeyoyyxeluzaeckwpcsa.supabase.co/storage/v1/object/public/technical-bucket/students/id-front/student_01_id_front.png", null, "1100", "https://oeyoyyxeluzaeckwpcsa.supabase.co/storage/v1/object/public/technical-bucket/students/profile/student_01_profile.png", "Metro Manila", null, "RFID-STU-001", "A", "123 Rizal Street", "2023-0001", "3rd Year" },
                    { new Guid("00000004-0000-0000-0000-000000000002"), "https://oeyoyyxeluzaeckwpcsa.supabase.co/storage/v1/object/public/technical-bucket/students/id-back/student_02_id_back.png", "Makati City", "Bachelor of Science in Information Technology", "https://oeyoyyxeluzaeckwpcsa.supabase.co/storage/v1/object/public/technical-bucket/students/id-front/student_02_id_front.png", null, "1200", "https://oeyoyyxeluzaeckwpcsa.supabase.co/storage/v1/object/public/technical-bucket/students/profile/student_02_profile.png", "Metro Manila", null, "RFID-STU-002", "B", "456 Mabini Avenue", "2023-0002", "2nd Year" },
                    { new Guid("00000004-0000-0000-0000-000000000003"), "https://oeyoyyxeluzaeckwpcsa.supabase.co/storage/v1/object/public/technical-bucket/students/id-back/student_03_id_back.png", "Pasig City", "Bachelor of Science in Computer Science", "https://oeyoyyxeluzaeckwpcsa.supabase.co/storage/v1/object/public/technical-bucket/students/id-front/student_03_id_front.png", null, "1600", "https://oeyoyyxeluzaeckwpcsa.supabase.co/storage/v1/object/public/technical-bucket/students/profile/student_03_profile.png", "Metro Manila", null, "RFID-STU-003", "A", "789 Bonifacio Road", "2023-0003", "3rd Year" },
                    { new Guid("00000004-0000-0000-0000-000000000004"), "https://oeyoyyxeluzaeckwpcsa.supabase.co/storage/v1/object/public/technical-bucket/students/id-back/student_04_id_back.png", "Mandaluyong City", "Bachelor of Multimedia Arts", "https://oeyoyyxeluzaeckwpcsa.supabase.co/storage/v1/object/public/technical-bucket/students/id-front/student_04_id_front.png", null, "1550", "https://oeyoyyxeluzaeckwpcsa.supabase.co/storage/v1/object/public/technical-bucket/students/profile/student_04_profile.png", "Metro Manila", null, "RFID-STU-004", "C", "321 Luna Street", "2023-0004", "1st Year" },
                    { new Guid("00000004-0000-0000-0000-000000000005"), "https://oeyoyyxeluzaeckwpcsa.supabase.co/storage/v1/object/public/technical-bucket/students/id-back/student_05_id_back.png", "Taguig City", "Bachelor of Science in Information Technology", "https://oeyoyyxeluzaeckwpcsa.supabase.co/storage/v1/object/public/technical-bucket/students/id-front/student_05_id_front.png", null, "1630", "https://oeyoyyxeluzaeckwpcsa.supabase.co/storage/v1/object/public/technical-bucket/students/profile/student_05_profile.png", "Metro Manila", null, null, "D", "999 Old Street", "2022-0099", "4th Year" }
                });

            migrationBuilder.InsertData(
                table: "Teachers",
                columns: new[] { "Id", "Department" },
                values: new object[,]
                {
                    { new Guid("00000003-0000-0000-0000-000000000001"), "Information Technology" },
                    { new Guid("00000003-0000-0000-0000-000000000002"), "Computer Science" },
                    { new Guid("00000003-0000-0000-0000-000000000003"), "Information Technology" },
                    { new Guid("00000003-0000-0000-0000-000000000004"), "Multimedia Arts" },
                    { new Guid("00000003-0000-0000-0000-000000000005"), "Former Department" }
                });

            migrationBuilder.InsertData(
                table: "ArchiveLentItems",
                columns: new[] { "Id", "BorrowerFullName", "BorrowerRole", "ContactNumber", "CreatedAt", "FrontStudentIdPictureUrl", "GuestImageUrl", "IsHiddenFromUser", "ItemId", "ItemName", "LentAt", "Organization", "Purpose", "Remarks", "ReservedFor", "ReturnedAt", "Room", "Status", "StudentIdNumber", "StudentRfid", "SubjectTimeSchedule", "TagUid", "TeacherFullName", "TeacherId", "UpdatedAt", "UserId" },
                values: new object[,]
                {
                    { new Guid("00000007-0000-0000-0000-000000000010"), "Archived Teacher", "Teacher", null, new DateTime(2025, 1, 10, 11, 0, 0, 0, DateTimeKind.Utc), null, null, false, new Guid("00000005-0000-0000-0000-000000000006"), "HDMI Cable 6ft", new DateTime(2025, 1, 10, 11, 0, 0, 0, DateTimeKind.Utc), null, null, null, null, new DateTime(2025, 1, 14, 11, 0, 0, 0, DateTimeKind.Utc), "Room 301", "Returned", null, null, "MA101 - 11:00 AM", null, "Archived Teacher", new Guid("00000003-0000-0000-0000-000000000005"), new DateTime(2025, 1, 15, 10, 0, 0, 0, DateTimeKind.Utc), new Guid("00000003-0000-0000-0000-000000000005") },
                    { new Guid("00000007-0000-0000-0000-000000000013"), "Alice Williams", "Teacher", null, new DateTime(2024, 10, 15, 15, 0, 0, 0, DateTimeKind.Utc), null, null, false, new Guid("00000005-0000-0000-0000-000000000009"), "Extension Wire 15ft", new DateTime(2024, 10, 15, 15, 0, 0, 0, DateTimeKind.Utc), null, null, null, null, new DateTime(2024, 10, 17, 15, 0, 0, 0, DateTimeKind.Utc), "Room 201", "Returned", null, null, "IT401 - 3:00 PM", null, "Alice Williams", new Guid("00000003-0000-0000-0000-000000000001"), new DateTime(2024, 10, 17, 15, 0, 0, 0, DateTimeKind.Utc), new Guid("00000003-0000-0000-0000-000000000001") }
                });

            migrationBuilder.InsertData(
                table: "LentItems",
                columns: new[] { "Id", "BorrowerFullName", "BorrowerRole", "ContactNumber", "CreatedAt", "FrontStudentIdPictureUrl", "GuestImageUrl", "IsHiddenFromUser", "IssuedById", "IssuedByLastName", "ItemId", "ItemName", "LentAt", "Organization", "Purpose", "Remarks", "ReservedFor", "ReturnedAt", "Room", "Status", "StudentIdNumber", "StudentRfid", "SubjectTimeSchedule", "TagUid", "TeacherFullName", "TeacherId", "UpdatedAt", "UserId" },
                values: new object[,]
                {
                    { new Guid("00000006-0000-0000-0000-000000000003"), "Alice Williams", "Teacher", null, new DateTime(2025, 4, 19, 10, 0, 0, 0, DateTimeKind.Utc), null, null, false, null, null, new Guid("00000005-0000-0000-0000-000000000003"), "Portable Projector", null, null, null, null, new DateTime(2025, 4, 26, 13, 0, 0, 0, DateTimeKind.Utc), null, "Room 201", "Approved", null, null, "IT401 - 1:00 PM", "RFID-ITEM-003", "Alice Williams", new Guid("00000003-0000-0000-0000-000000000001"), new DateTime(2025, 4, 19, 11, 0, 0, 0, DateTimeKind.Utc), new Guid("00000003-0000-0000-0000-000000000001") },
                    { new Guid("00000006-0000-0000-0000-000000000005"), "Roberto Cruz", "Teacher", null, new DateTime(2025, 4, 14, 9, 0, 0, 0, DateTimeKind.Utc), null, null, false, null, null, new Guid("00000005-0000-0000-0000-000000000005"), "Mechanical Keyboard", null, null, null, null, new DateTime(2025, 4, 15, 9, 0, 0, 0, DateTimeKind.Utc), null, "Room 202", "Expired", null, null, "CS201 - 9:00 AM", "RFID-ITEM-005", "Roberto Cruz", new Guid("00000003-0000-0000-0000-000000000002"), new DateTime(2025, 4, 15, 9, 31, 0, 0, DateTimeKind.Utc), new Guid("00000003-0000-0000-0000-000000000002") }
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
                name: "IX_RefreshTokens_UserId_IsRevoked",
                table: "RefreshTokens",
                columns: new[] { "UserId", "IsRevoked" });

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
                name: "Rfids");

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
