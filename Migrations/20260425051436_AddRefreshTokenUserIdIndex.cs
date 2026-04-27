using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BackendTechnicalAssetsManagement.Migrations
{
    /// <inheritdoc />
    public partial class AddRefreshTokenUserIdIndex : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_RefreshTokens_UserId",
                table: "RefreshTokens");

            migrationBuilder.UpdateData(
                table: "ArchiveUsers",
                keyColumn: "Id",
                keyValue: new Guid("00000007-0000-0000-0000-000000000001"),
                column: "PasswordHash",
                value: "$2a$04$XXYFHeKNDbLGqUzLB1yWjuPghWlA2zdvpbVQwQEa20ChcEN9hxUsO");

            migrationBuilder.UpdateData(
                table: "ArchiveUsers",
                keyColumn: "Id",
                keyValue: new Guid("00000007-0000-0000-0000-000000000002"),
                column: "PasswordHash",
                value: "$2a$04$XXYFHeKNDbLGqUzLB1yWjuPghWlA2zdvpbVQwQEa20ChcEN9hxUsO");

            migrationBuilder.UpdateData(
                table: "ArchiveUsers",
                keyColumn: "Id",
                keyValue: new Guid("00000007-0000-0000-0000-000000000003"),
                column: "PasswordHash",
                value: "$2a$04$XXYFHeKNDbLGqUzLB1yWjuPghWlA2zdvpbVQwQEa20ChcEN9hxUsO");

            migrationBuilder.UpdateData(
                table: "ArchiveUsers",
                keyColumn: "Id",
                keyValue: new Guid("00000007-0000-0000-0000-000000000004"),
                column: "PasswordHash",
                value: "$2a$04$XXYFHeKNDbLGqUzLB1yWjuPghWlA2zdvpbVQwQEa20ChcEN9hxUsO");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("00000001-0000-0000-0000-000000000001"),
                column: "PasswordHash",
                value: "$2a$04$XXYFHeKNDbLGqUzLB1yWjuPghWlA2zdvpbVQwQEa20ChcEN9hxUsO");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("00000001-0000-0000-0000-000000000002"),
                column: "PasswordHash",
                value: "$2a$04$XXYFHeKNDbLGqUzLB1yWjuPghWlA2zdvpbVQwQEa20ChcEN9hxUsO");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("00000001-0000-0000-0000-000000000003"),
                column: "PasswordHash",
                value: "$2a$04$XXYFHeKNDbLGqUzLB1yWjuPghWlA2zdvpbVQwQEa20ChcEN9hxUsO");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("00000001-0000-0000-0000-000000000004"),
                column: "PasswordHash",
                value: "$2a$04$XXYFHeKNDbLGqUzLB1yWjuPghWlA2zdvpbVQwQEa20ChcEN9hxUsO");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("00000002-0000-0000-0000-000000000001"),
                column: "PasswordHash",
                value: "$2a$04$XXYFHeKNDbLGqUzLB1yWjuPghWlA2zdvpbVQwQEa20ChcEN9hxUsO");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("00000002-0000-0000-0000-000000000002"),
                column: "PasswordHash",
                value: "$2a$04$XXYFHeKNDbLGqUzLB1yWjuPghWlA2zdvpbVQwQEa20ChcEN9hxUsO");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("00000002-0000-0000-0000-000000000003"),
                column: "PasswordHash",
                value: "$2a$04$XXYFHeKNDbLGqUzLB1yWjuPghWlA2zdvpbVQwQEa20ChcEN9hxUsO");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("00000003-0000-0000-0000-000000000001"),
                column: "PasswordHash",
                value: "$2a$04$XXYFHeKNDbLGqUzLB1yWjuPghWlA2zdvpbVQwQEa20ChcEN9hxUsO");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("00000003-0000-0000-0000-000000000002"),
                column: "PasswordHash",
                value: "$2a$04$XXYFHeKNDbLGqUzLB1yWjuPghWlA2zdvpbVQwQEa20ChcEN9hxUsO");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("00000003-0000-0000-0000-000000000003"),
                column: "PasswordHash",
                value: "$2a$04$XXYFHeKNDbLGqUzLB1yWjuPghWlA2zdvpbVQwQEa20ChcEN9hxUsO");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("00000003-0000-0000-0000-000000000004"),
                column: "PasswordHash",
                value: "$2a$04$XXYFHeKNDbLGqUzLB1yWjuPghWlA2zdvpbVQwQEa20ChcEN9hxUsO");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("00000004-0000-0000-0000-000000000001"),
                column: "PasswordHash",
                value: "$2a$04$XXYFHeKNDbLGqUzLB1yWjuPghWlA2zdvpbVQwQEa20ChcEN9hxUsO");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("00000004-0000-0000-0000-000000000002"),
                column: "PasswordHash",
                value: "$2a$04$XXYFHeKNDbLGqUzLB1yWjuPghWlA2zdvpbVQwQEa20ChcEN9hxUsO");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("00000004-0000-0000-0000-000000000003"),
                column: "PasswordHash",
                value: "$2a$04$XXYFHeKNDbLGqUzLB1yWjuPghWlA2zdvpbVQwQEa20ChcEN9hxUsO");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("00000004-0000-0000-0000-000000000004"),
                column: "PasswordHash",
                value: "$2a$04$XXYFHeKNDbLGqUzLB1yWjuPghWlA2zdvpbVQwQEa20ChcEN9hxUsO");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("00000004-0000-0000-0000-000000000005"),
                column: "PasswordHash",
                value: "$2a$04$XXYFHeKNDbLGqUzLB1yWjuPghWlA2zdvpbVQwQEa20ChcEN9hxUsO");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("00000004-0000-0000-0000-000000000006"),
                column: "PasswordHash",
                value: "$2a$04$XXYFHeKNDbLGqUzLB1yWjuPghWlA2zdvpbVQwQEa20ChcEN9hxUsO");

            migrationBuilder.CreateIndex(
                name: "IX_RefreshTokens_UserId_IsRevoked",
                table: "RefreshTokens",
                columns: new[] { "UserId", "IsRevoked" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_RefreshTokens_UserId_IsRevoked",
                table: "RefreshTokens");

            migrationBuilder.UpdateData(
                table: "ArchiveUsers",
                keyColumn: "Id",
                keyValue: new Guid("00000007-0000-0000-0000-000000000001"),
                column: "PasswordHash",
                value: "$2a$04$kxd5.NveUAredGIj3SwE/uurcRaoF1CVn9PaoYgm1b4dRBnk0sY9K");

            migrationBuilder.UpdateData(
                table: "ArchiveUsers",
                keyColumn: "Id",
                keyValue: new Guid("00000007-0000-0000-0000-000000000002"),
                column: "PasswordHash",
                value: "$2a$04$kxd5.NveUAredGIj3SwE/uurcRaoF1CVn9PaoYgm1b4dRBnk0sY9K");

            migrationBuilder.UpdateData(
                table: "ArchiveUsers",
                keyColumn: "Id",
                keyValue: new Guid("00000007-0000-0000-0000-000000000003"),
                column: "PasswordHash",
                value: "$2a$04$kxd5.NveUAredGIj3SwE/uurcRaoF1CVn9PaoYgm1b4dRBnk0sY9K");

            migrationBuilder.UpdateData(
                table: "ArchiveUsers",
                keyColumn: "Id",
                keyValue: new Guid("00000007-0000-0000-0000-000000000004"),
                column: "PasswordHash",
                value: "$2a$04$kxd5.NveUAredGIj3SwE/uurcRaoF1CVn9PaoYgm1b4dRBnk0sY9K");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("00000001-0000-0000-0000-000000000001"),
                column: "PasswordHash",
                value: "$2a$04$kxd5.NveUAredGIj3SwE/uurcRaoF1CVn9PaoYgm1b4dRBnk0sY9K");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("00000001-0000-0000-0000-000000000002"),
                column: "PasswordHash",
                value: "$2a$04$kxd5.NveUAredGIj3SwE/uurcRaoF1CVn9PaoYgm1b4dRBnk0sY9K");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("00000001-0000-0000-0000-000000000003"),
                column: "PasswordHash",
                value: "$2a$04$kxd5.NveUAredGIj3SwE/uurcRaoF1CVn9PaoYgm1b4dRBnk0sY9K");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("00000001-0000-0000-0000-000000000004"),
                column: "PasswordHash",
                value: "$2a$04$kxd5.NveUAredGIj3SwE/uurcRaoF1CVn9PaoYgm1b4dRBnk0sY9K");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("00000002-0000-0000-0000-000000000001"),
                column: "PasswordHash",
                value: "$2a$04$kxd5.NveUAredGIj3SwE/uurcRaoF1CVn9PaoYgm1b4dRBnk0sY9K");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("00000002-0000-0000-0000-000000000002"),
                column: "PasswordHash",
                value: "$2a$04$kxd5.NveUAredGIj3SwE/uurcRaoF1CVn9PaoYgm1b4dRBnk0sY9K");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("00000002-0000-0000-0000-000000000003"),
                column: "PasswordHash",
                value: "$2a$04$kxd5.NveUAredGIj3SwE/uurcRaoF1CVn9PaoYgm1b4dRBnk0sY9K");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("00000003-0000-0000-0000-000000000001"),
                column: "PasswordHash",
                value: "$2a$04$kxd5.NveUAredGIj3SwE/uurcRaoF1CVn9PaoYgm1b4dRBnk0sY9K");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("00000003-0000-0000-0000-000000000002"),
                column: "PasswordHash",
                value: "$2a$04$kxd5.NveUAredGIj3SwE/uurcRaoF1CVn9PaoYgm1b4dRBnk0sY9K");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("00000003-0000-0000-0000-000000000003"),
                column: "PasswordHash",
                value: "$2a$04$kxd5.NveUAredGIj3SwE/uurcRaoF1CVn9PaoYgm1b4dRBnk0sY9K");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("00000003-0000-0000-0000-000000000004"),
                column: "PasswordHash",
                value: "$2a$04$kxd5.NveUAredGIj3SwE/uurcRaoF1CVn9PaoYgm1b4dRBnk0sY9K");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("00000004-0000-0000-0000-000000000001"),
                column: "PasswordHash",
                value: "$2a$04$kxd5.NveUAredGIj3SwE/uurcRaoF1CVn9PaoYgm1b4dRBnk0sY9K");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("00000004-0000-0000-0000-000000000002"),
                column: "PasswordHash",
                value: "$2a$04$kxd5.NveUAredGIj3SwE/uurcRaoF1CVn9PaoYgm1b4dRBnk0sY9K");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("00000004-0000-0000-0000-000000000003"),
                column: "PasswordHash",
                value: "$2a$04$kxd5.NveUAredGIj3SwE/uurcRaoF1CVn9PaoYgm1b4dRBnk0sY9K");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("00000004-0000-0000-0000-000000000004"),
                column: "PasswordHash",
                value: "$2a$04$kxd5.NveUAredGIj3SwE/uurcRaoF1CVn9PaoYgm1b4dRBnk0sY9K");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("00000004-0000-0000-0000-000000000005"),
                column: "PasswordHash",
                value: "$2a$04$kxd5.NveUAredGIj3SwE/uurcRaoF1CVn9PaoYgm1b4dRBnk0sY9K");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("00000004-0000-0000-0000-000000000006"),
                column: "PasswordHash",
                value: "$2a$04$kxd5.NveUAredGIj3SwE/uurcRaoF1CVn9PaoYgm1b4dRBnk0sY9K");

            migrationBuilder.CreateIndex(
                name: "IX_RefreshTokens_UserId",
                table: "RefreshTokens",
                column: "UserId");
        }
    }
}
