using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

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
                    Barcode = table.Column<string>(type: "text", nullable: true),
                    BarcodeImage = table.Column<byte[]>(type: "bytea", nullable: true),
                    Image = table.Column<byte[]>(type: "bytea", nullable: true),
                    ImageMimeType = table.Column<string>(type: "text", nullable: true),
                    ItemName = table.Column<string>(type: "text", nullable: false),
                    ItemType = table.Column<string>(type: "text", nullable: false),
                    ItemModel = table.Column<string>(type: "text", nullable: true),
                    ItemMake = table.Column<string>(type: "text", nullable: false),
                    Description = table.Column<string>(type: "text", nullable: true),
                    Category = table.Column<string>(type: "text", nullable: false),
                    Condition = table.Column<string>(type: "text", nullable: false),
                    Status = table.Column<string>(type: "text", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "timestamp without time zone", nullable: false)
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
                    Image = table.Column<byte[]>(type: "bytea", nullable: true),
                    ImageMimeType = table.Column<string>(type: "text", nullable: true),
                    Barcode = table.Column<string>(type: "text", nullable: true),
                    BarcodeImage = table.Column<byte[]>(type: "bytea", nullable: true),
                    ItemName = table.Column<string>(type: "text", nullable: false),
                    ItemType = table.Column<string>(type: "text", nullable: false),
                    ItemModel = table.Column<string>(type: "text", nullable: true),
                    ItemMake = table.Column<string>(type: "text", nullable: false),
                    Description = table.Column<string>(type: "text", nullable: true),
                    Category = table.Column<string>(type: "text", nullable: false),
                    Condition = table.Column<string>(type: "text", nullable: false),
                    Status = table.Column<string>(type: "text", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "timestamp without time zone", nullable: false)
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
                    BackStudentIdPicture = table.Column<byte[]>(type: "bytea", nullable: true)
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
                    ProfilePicture = table.Column<byte[]>(type: "bytea", nullable: true),
                    StudentIdNumber = table.Column<string>(type: "text", nullable: true),
                    Course = table.Column<string>(type: "text", nullable: false),
                    Year = table.Column<string>(type: "text", nullable: false),
                    Section = table.Column<string>(type: "text", nullable: false),
                    Street = table.Column<string>(type: "text", nullable: false),
                    CityMunicipality = table.Column<string>(type: "text", nullable: false),
                    Province = table.Column<string>(type: "text", nullable: false),
                    PostalCode = table.Column<string>(type: "text", nullable: false),
                    FrontStudentIdPicture = table.Column<byte[]>(type: "bytea", nullable: true),
                    BackStudentIdPicture = table.Column<byte[]>(type: "bytea", nullable: true),
                    GeneratedPassword = table.Column<string>(type: "text", nullable: true)
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
                    LentAt = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    ReturnedAt = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    Status = table.Column<string>(type: "text", nullable: true),
                    Remarks = table.Column<string>(type: "text", nullable: true),
                    IsHiddenFromUser = table.Column<bool>(type: "boolean", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    Barcode = table.Column<string>(type: "text", nullable: true),
                    BarcodeImage = table.Column<byte[]>(type: "bytea", nullable: true),
                    FrontStudentIdPicture = table.Column<byte[]>(type: "bytea", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ArchiveLentItems", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ArchiveLentItems_Items_ItemId",
                        column: x => x.ItemId,
                        principalTable: "Items",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
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
                    Barcode = table.Column<string>(type: "text", nullable: true),
                    BarcodeImage = table.Column<byte[]>(type: "bytea", nullable: true),
                    FrontStudentIdPicture = table.Column<byte[]>(type: "bytea", nullable: true),
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
                name: "LentItems");

            migrationBuilder.DropTable(
                name: "RefreshTokens");

            migrationBuilder.DropTable(
                name: "Staff");

            migrationBuilder.DropTable(
                name: "Students");

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
