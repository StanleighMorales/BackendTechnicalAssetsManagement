using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BackendTechnicalAssetsManagement.Migrations
{
    /// <inheritdoc />
    public partial class MadeItemnameFromStringToItem : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ItemName",
                table: "LentItems");

            migrationBuilder.CreateIndex(
                name: "IX_LentItems_ItemId",
                table: "LentItems",
                column: "ItemId");

            migrationBuilder.AddForeignKey(
                name: "FK_LentItems_Items_ItemId",
                table: "LentItems",
                column: "ItemId",
                principalTable: "Items",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_LentItems_Items_ItemId",
                table: "LentItems");

            migrationBuilder.DropIndex(
                name: "IX_LentItems_ItemId",
                table: "LentItems");

            migrationBuilder.AddColumn<string>(
                name: "ItemName",
                table: "LentItems",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}
