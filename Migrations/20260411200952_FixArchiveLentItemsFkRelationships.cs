using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BackendTechnicalAssetsManagement.Migrations
{
    /// <inheritdoc />
    public partial class FixArchiveLentItemsFkRelationships : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ArchiveLentItems_Items_ItemId",
                table: "ArchiveLentItems");

            migrationBuilder.DropForeignKey(
                name: "FK_ArchiveLentItems_Teachers_TeacherId",
                table: "ArchiveLentItems");

            migrationBuilder.DropForeignKey(
                name: "FK_ArchiveLentItems_Users_UserId",
                table: "ArchiveLentItems");

            migrationBuilder.AddForeignKey(
                name: "FK_ArchiveLentItems_Items_ItemId",
                table: "ArchiveLentItems",
                column: "ItemId",
                principalTable: "Items",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_ArchiveLentItems_Teachers_TeacherId",
                table: "ArchiveLentItems",
                column: "TeacherId",
                principalTable: "Teachers",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_ArchiveLentItems_Users_UserId",
                table: "ArchiveLentItems",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ArchiveLentItems_Items_ItemId",
                table: "ArchiveLentItems");

            migrationBuilder.DropForeignKey(
                name: "FK_ArchiveLentItems_Teachers_TeacherId",
                table: "ArchiveLentItems");

            migrationBuilder.DropForeignKey(
                name: "FK_ArchiveLentItems_Users_UserId",
                table: "ArchiveLentItems");

            migrationBuilder.AddForeignKey(
                name: "FK_ArchiveLentItems_Items_ItemId",
                table: "ArchiveLentItems",
                column: "ItemId",
                principalTable: "Items",
                principalColumn: "Id",
                onDelete: ReferentialAction.SetNull);

            migrationBuilder.AddForeignKey(
                name: "FK_ArchiveLentItems_Teachers_TeacherId",
                table: "ArchiveLentItems",
                column: "TeacherId",
                principalTable: "Teachers",
                principalColumn: "Id",
                onDelete: ReferentialAction.SetNull);

            migrationBuilder.AddForeignKey(
                name: "FK_ArchiveLentItems_Users_UserId",
                table: "ArchiveLentItems",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.SetNull);
        }
    }
}
