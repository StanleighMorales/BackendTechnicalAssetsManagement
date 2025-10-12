using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BackendTechnicalAssetsManagement.Migrations
{
    /// <inheritdoc />
    public partial class madeSerialNumberNullable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Items_SerialNumber",
                table: "Items");

            migrationBuilder.AlterColumn<string>(
                name: "SerialNumber",
                table: "Items",
                type: "nvarchar(450)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.CreateIndex(
                name: "IX_Items_SerialNumber",
                table: "Items",
                column: "SerialNumber",
                unique: true,
                filter: "[SerialNumber] IS NOT NULL");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Items_SerialNumber",
                table: "Items");

            migrationBuilder.AlterColumn<string>(
                name: "SerialNumber",
                table: "Items",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(450)",
                oldNullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Items_SerialNumber",
                table: "Items",
                column: "SerialNumber",
                unique: true);
        }
    }
}
