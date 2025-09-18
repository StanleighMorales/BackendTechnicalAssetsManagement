using System;
using Microsoft.EntityFrameworkCore.Migrations;

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
                name: "Items",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    SerialNumber = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Image = table.Column<byte[]>(type: "varbinary(max)", nullable: true),
                    ItemName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ItemType = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ItemModel = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ItemMake = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Category = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Condition = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Items", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Username = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PasswordHash = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    UserRole = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    RefreshToken = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TokenCreated = table.Column<DateTime>(type: "datetime2", nullable: true),
                    TokenExpires = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Discriminator = table.Column<string>(type: "nvarchar(8)", maxLength: 8, nullable: false),
                    Admin_LastName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Admin_MiddleName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Admin_FirstName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Admin_PhoneNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Manager_LastName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Manager_MiddleName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Manager_FirstName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Manager_PhoneNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Staff_LastName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Staff_MiddleName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Staff_FirstName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Staff_PhoneNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Position = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ProfilePicture = table.Column<byte[]>(type: "varbinary(max)", nullable: true),
                    Student_LastName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Student_MiddleName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Student_FirstName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    StudentIdNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Student_PhoneNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Course = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Year = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Section = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Street = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CityMunicipality = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Province = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PostalCode = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    FrontStudentIdPicture = table.Column<byte[]>(type: "varbinary(max)", nullable: true),
                    BackStudentIdPicture = table.Column<byte[]>(type: "varbinary(max)", nullable: true),
                    LastName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    MiddleName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    FirstName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhoneNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Department = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Items");

            migrationBuilder.DropTable(
                name: "Users");
        }
    }
}
