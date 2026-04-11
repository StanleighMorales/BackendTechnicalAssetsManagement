using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BackendTechnicalAssetsManagement.Migrations
{
    /// <inheritdoc />
    public partial class RemoveBarcodeColumns : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Barcode",
                table: "Items");

            migrationBuilder.DropColumn(
                name: "BarcodeImage",
                table: "Items");

            migrationBuilder.DropColumn(
                name: "Barcode",
                table: "LentItems");

            migrationBuilder.DropColumn(
                name: "BarcodeImage",
                table: "LentItems");

            migrationBuilder.DropColumn(
                name: "Barcode",
                table: "ArchiveItems");

            migrationBuilder.DropColumn(
                name: "BarcodeImage",
                table: "ArchiveItems");

            migrationBuilder.DropColumn(
                name: "Barcode",
                table: "ArchiveLentItems");

            migrationBuilder.DropColumn(
                name: "BarcodeImage",
                table: "ArchiveLentItems");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Barcode",
                table: "Items",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<byte[]>(
                name: "BarcodeImage",
                table: "Items",
                type: "varbinary(max)",
                nullable: true);

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

            migrationBuilder.AddColumn<string>(
                name: "Barcode",
                table: "ArchiveItems",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<byte[]>(
                name: "BarcodeImage",
                table: "ArchiveItems",
                type: "varbinary(max)",
                nullable: true);

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
        }
    }
}
