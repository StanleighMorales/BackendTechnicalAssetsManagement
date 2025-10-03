using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BackendTechnicalAssetsManagement.Migrations
{
    /// <inheritdoc />
    public partial class AddedTeacherFullName : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DeletedAt",
                table: "LentItems");

            migrationBuilder.DropColumn(
                name: "IsDeleted",
                table: "LentItems");

            migrationBuilder.AddColumn<string>(
                name: "TeacherFullName",
                table: "LentItems",
                type: "nvarchar(max)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "TeacherFullName",
                table: "LentItems");

            migrationBuilder.AddColumn<DateTime>(
                name: "DeletedAt",
                table: "LentItems",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                table: "LentItems",
                type: "bit",
                nullable: true);
        }
    }
}
