using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BackendTechnicalAssetsManagement.Migrations
{
    /// <inheritdoc />
    public partial class FixStudentIdNumberUniqueIndex : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
            name: "IX_Students_StudentIdNumber",
            table: "Students"); // <-- THIS MUST BE FIRST

            migrationBuilder.AlterColumn<string>(
                name: "StudentIdNumber",
                table: "Students",
                type: "nvarchar(450)",
                nullable: true, // <-- This is the change to nullable
                oldClrType: typeof(string),
                oldType: "nvarchar(450)"); // <-- THIS MUST BE SECOND

            // ... (other AlterColumn/CreateTable commands) ...

            migrationBuilder.CreateIndex(
                name: "IX_Students_StudentIdNumber",
                table: "Students",
                column: "StudentIdNumber",
                unique: true,
                filter: "[StudentIdNumber] IS NOT NULL AND [StudentIdNumber] <> ''"); // <-- THIS MUST BE LAST
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Students_StudentIdNumber",
                table: "Students");

            migrationBuilder.AlterColumn<string>(
                name: "StudentIdNumber",
                table: "Students",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)",
                oldNullable: true);
        }
    }
}
