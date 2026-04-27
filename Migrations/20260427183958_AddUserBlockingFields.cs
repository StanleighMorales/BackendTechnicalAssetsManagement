using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BackendTechnicalAssetsManagement.Migrations
{
    /// <inheritdoc />
    public partial class AddUserBlockingFields : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "BlockReason",
                table: "Users",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "BlockedAt",
                table: "Users",
                type: "timestamp without time zone",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "BlockedById",
                table: "Users",
                type: "uuid",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "BlockedUntil",
                table: "Users",
                type: "timestamp without time zone",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsBlocked",
                table: "Users",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.UpdateData(
                table: "ArchiveUsers",
                keyColumn: "Id",
                keyValue: new Guid("00000007-0000-0000-0000-000000000001"),
                column: "PasswordHash",
                value: "$2a$04$l4RFhj2r1EkTTA20v450e.1aclI7uUyvFi23CeMQLZxY9PcID6fpW");

            migrationBuilder.UpdateData(
                table: "ArchiveUsers",
                keyColumn: "Id",
                keyValue: new Guid("00000007-0000-0000-0000-000000000002"),
                column: "PasswordHash",
                value: "$2a$04$l4RFhj2r1EkTTA20v450e.1aclI7uUyvFi23CeMQLZxY9PcID6fpW");

            migrationBuilder.UpdateData(
                table: "ArchiveUsers",
                keyColumn: "Id",
                keyValue: new Guid("00000007-0000-0000-0000-000000000003"),
                column: "PasswordHash",
                value: "$2a$04$l4RFhj2r1EkTTA20v450e.1aclI7uUyvFi23CeMQLZxY9PcID6fpW");

            migrationBuilder.UpdateData(
                table: "ArchiveUsers",
                keyColumn: "Id",
                keyValue: new Guid("00000007-0000-0000-0000-000000000004"),
                column: "PasswordHash",
                value: "$2a$04$l4RFhj2r1EkTTA20v450e.1aclI7uUyvFi23CeMQLZxY9PcID6fpW");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("00000001-0000-0000-0000-000000000001"),
                columns: new[] { "BlockReason", "BlockedAt", "BlockedById", "BlockedUntil", "IsBlocked", "PasswordHash" },
                values: new object[] { null, null, null, null, false, "$2a$04$l4RFhj2r1EkTTA20v450e.1aclI7uUyvFi23CeMQLZxY9PcID6fpW" });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("00000001-0000-0000-0000-000000000002"),
                columns: new[] { "BlockReason", "BlockedAt", "BlockedById", "BlockedUntil", "IsBlocked", "PasswordHash" },
                values: new object[] { null, null, null, null, false, "$2a$04$l4RFhj2r1EkTTA20v450e.1aclI7uUyvFi23CeMQLZxY9PcID6fpW" });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("00000001-0000-0000-0000-000000000003"),
                columns: new[] { "BlockReason", "BlockedAt", "BlockedById", "BlockedUntil", "IsBlocked", "PasswordHash" },
                values: new object[] { null, null, null, null, false, "$2a$04$l4RFhj2r1EkTTA20v450e.1aclI7uUyvFi23CeMQLZxY9PcID6fpW" });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("00000001-0000-0000-0000-000000000004"),
                columns: new[] { "BlockReason", "BlockedAt", "BlockedById", "BlockedUntil", "IsBlocked", "PasswordHash" },
                values: new object[] { null, null, null, null, false, "$2a$04$l4RFhj2r1EkTTA20v450e.1aclI7uUyvFi23CeMQLZxY9PcID6fpW" });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("00000001-0000-0000-0000-000000000005"),
                columns: new[] { "BlockReason", "BlockedAt", "BlockedById", "BlockedUntil", "IsBlocked", "PasswordHash" },
                values: new object[] { null, null, null, null, false, "$2a$04$l4RFhj2r1EkTTA20v450e.1aclI7uUyvFi23CeMQLZxY9PcID6fpW" });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("00000002-0000-0000-0000-000000000001"),
                columns: new[] { "BlockReason", "BlockedAt", "BlockedById", "BlockedUntil", "IsBlocked", "PasswordHash" },
                values: new object[] { null, null, null, null, false, "$2a$04$l4RFhj2r1EkTTA20v450e.1aclI7uUyvFi23CeMQLZxY9PcID6fpW" });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("00000002-0000-0000-0000-000000000002"),
                columns: new[] { "BlockReason", "BlockedAt", "BlockedById", "BlockedUntil", "IsBlocked", "PasswordHash" },
                values: new object[] { null, null, null, null, false, "$2a$04$l4RFhj2r1EkTTA20v450e.1aclI7uUyvFi23CeMQLZxY9PcID6fpW" });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("00000002-0000-0000-0000-000000000003"),
                columns: new[] { "BlockReason", "BlockedAt", "BlockedById", "BlockedUntil", "IsBlocked", "PasswordHash" },
                values: new object[] { null, null, null, null, false, "$2a$04$l4RFhj2r1EkTTA20v450e.1aclI7uUyvFi23CeMQLZxY9PcID6fpW" });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("00000002-0000-0000-0000-000000000004"),
                columns: new[] { "BlockReason", "BlockedAt", "BlockedById", "BlockedUntil", "IsBlocked", "PasswordHash" },
                values: new object[] { null, null, null, null, false, "$2a$04$l4RFhj2r1EkTTA20v450e.1aclI7uUyvFi23CeMQLZxY9PcID6fpW" });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("00000002-0000-0000-0000-000000000005"),
                columns: new[] { "BlockReason", "BlockedAt", "BlockedById", "BlockedUntil", "IsBlocked", "PasswordHash" },
                values: new object[] { null, null, null, null, false, "$2a$04$l4RFhj2r1EkTTA20v450e.1aclI7uUyvFi23CeMQLZxY9PcID6fpW" });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("00000003-0000-0000-0000-000000000001"),
                columns: new[] { "BlockReason", "BlockedAt", "BlockedById", "BlockedUntil", "IsBlocked", "PasswordHash" },
                values: new object[] { null, null, null, null, false, "$2a$04$l4RFhj2r1EkTTA20v450e.1aclI7uUyvFi23CeMQLZxY9PcID6fpW" });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("00000003-0000-0000-0000-000000000002"),
                columns: new[] { "BlockReason", "BlockedAt", "BlockedById", "BlockedUntil", "IsBlocked", "PasswordHash" },
                values: new object[] { null, null, null, null, false, "$2a$04$l4RFhj2r1EkTTA20v450e.1aclI7uUyvFi23CeMQLZxY9PcID6fpW" });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("00000003-0000-0000-0000-000000000003"),
                columns: new[] { "BlockReason", "BlockedAt", "BlockedById", "BlockedUntil", "IsBlocked", "PasswordHash" },
                values: new object[] { null, null, null, null, false, "$2a$04$l4RFhj2r1EkTTA20v450e.1aclI7uUyvFi23CeMQLZxY9PcID6fpW" });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("00000003-0000-0000-0000-000000000004"),
                columns: new[] { "BlockReason", "BlockedAt", "BlockedById", "BlockedUntil", "IsBlocked", "PasswordHash" },
                values: new object[] { null, null, null, null, false, "$2a$04$l4RFhj2r1EkTTA20v450e.1aclI7uUyvFi23CeMQLZxY9PcID6fpW" });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("00000003-0000-0000-0000-000000000005"),
                columns: new[] { "BlockReason", "BlockedAt", "BlockedById", "BlockedUntil", "IsBlocked", "PasswordHash" },
                values: new object[] { null, null, null, null, false, "$2a$04$l4RFhj2r1EkTTA20v450e.1aclI7uUyvFi23CeMQLZxY9PcID6fpW" });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("00000004-0000-0000-0000-000000000001"),
                columns: new[] { "BlockReason", "BlockedAt", "BlockedById", "BlockedUntil", "IsBlocked", "PasswordHash" },
                values: new object[] { null, null, null, null, false, "$2a$04$l4RFhj2r1EkTTA20v450e.1aclI7uUyvFi23CeMQLZxY9PcID6fpW" });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("00000004-0000-0000-0000-000000000002"),
                columns: new[] { "BlockReason", "BlockedAt", "BlockedById", "BlockedUntil", "IsBlocked", "PasswordHash" },
                values: new object[] { null, null, null, null, false, "$2a$04$l4RFhj2r1EkTTA20v450e.1aclI7uUyvFi23CeMQLZxY9PcID6fpW" });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("00000004-0000-0000-0000-000000000003"),
                columns: new[] { "BlockReason", "BlockedAt", "BlockedById", "BlockedUntil", "IsBlocked", "PasswordHash" },
                values: new object[] { null, null, null, null, false, "$2a$04$l4RFhj2r1EkTTA20v450e.1aclI7uUyvFi23CeMQLZxY9PcID6fpW" });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("00000004-0000-0000-0000-000000000004"),
                columns: new[] { "BlockReason", "BlockedAt", "BlockedById", "BlockedUntil", "IsBlocked", "PasswordHash" },
                values: new object[] { null, null, null, null, false, "$2a$04$l4RFhj2r1EkTTA20v450e.1aclI7uUyvFi23CeMQLZxY9PcID6fpW" });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("00000004-0000-0000-0000-000000000005"),
                columns: new[] { "BlockReason", "BlockedAt", "BlockedById", "BlockedUntil", "IsBlocked", "PasswordHash" },
                values: new object[] { null, null, null, null, false, "$2a$04$l4RFhj2r1EkTTA20v450e.1aclI7uUyvFi23CeMQLZxY9PcID6fpW" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "BlockReason",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "BlockedAt",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "BlockedById",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "BlockedUntil",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "IsBlocked",
                table: "Users");

            migrationBuilder.UpdateData(
                table: "ArchiveUsers",
                keyColumn: "Id",
                keyValue: new Guid("00000007-0000-0000-0000-000000000001"),
                column: "PasswordHash",
                value: "$2a$04$WuSJZNktuI9FLcH.Az5HpeKJFhyNCxNzz7tR3jTVdzusa7qAJBBSK");

            migrationBuilder.UpdateData(
                table: "ArchiveUsers",
                keyColumn: "Id",
                keyValue: new Guid("00000007-0000-0000-0000-000000000002"),
                column: "PasswordHash",
                value: "$2a$04$WuSJZNktuI9FLcH.Az5HpeKJFhyNCxNzz7tR3jTVdzusa7qAJBBSK");

            migrationBuilder.UpdateData(
                table: "ArchiveUsers",
                keyColumn: "Id",
                keyValue: new Guid("00000007-0000-0000-0000-000000000003"),
                column: "PasswordHash",
                value: "$2a$04$WuSJZNktuI9FLcH.Az5HpeKJFhyNCxNzz7tR3jTVdzusa7qAJBBSK");

            migrationBuilder.UpdateData(
                table: "ArchiveUsers",
                keyColumn: "Id",
                keyValue: new Guid("00000007-0000-0000-0000-000000000004"),
                column: "PasswordHash",
                value: "$2a$04$WuSJZNktuI9FLcH.Az5HpeKJFhyNCxNzz7tR3jTVdzusa7qAJBBSK");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("00000001-0000-0000-0000-000000000001"),
                column: "PasswordHash",
                value: "$2a$04$WuSJZNktuI9FLcH.Az5HpeKJFhyNCxNzz7tR3jTVdzusa7qAJBBSK");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("00000001-0000-0000-0000-000000000002"),
                column: "PasswordHash",
                value: "$2a$04$WuSJZNktuI9FLcH.Az5HpeKJFhyNCxNzz7tR3jTVdzusa7qAJBBSK");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("00000001-0000-0000-0000-000000000003"),
                column: "PasswordHash",
                value: "$2a$04$WuSJZNktuI9FLcH.Az5HpeKJFhyNCxNzz7tR3jTVdzusa7qAJBBSK");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("00000001-0000-0000-0000-000000000004"),
                column: "PasswordHash",
                value: "$2a$04$WuSJZNktuI9FLcH.Az5HpeKJFhyNCxNzz7tR3jTVdzusa7qAJBBSK");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("00000001-0000-0000-0000-000000000005"),
                column: "PasswordHash",
                value: "$2a$04$WuSJZNktuI9FLcH.Az5HpeKJFhyNCxNzz7tR3jTVdzusa7qAJBBSK");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("00000002-0000-0000-0000-000000000001"),
                column: "PasswordHash",
                value: "$2a$04$WuSJZNktuI9FLcH.Az5HpeKJFhyNCxNzz7tR3jTVdzusa7qAJBBSK");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("00000002-0000-0000-0000-000000000002"),
                column: "PasswordHash",
                value: "$2a$04$WuSJZNktuI9FLcH.Az5HpeKJFhyNCxNzz7tR3jTVdzusa7qAJBBSK");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("00000002-0000-0000-0000-000000000003"),
                column: "PasswordHash",
                value: "$2a$04$WuSJZNktuI9FLcH.Az5HpeKJFhyNCxNzz7tR3jTVdzusa7qAJBBSK");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("00000002-0000-0000-0000-000000000004"),
                column: "PasswordHash",
                value: "$2a$04$WuSJZNktuI9FLcH.Az5HpeKJFhyNCxNzz7tR3jTVdzusa7qAJBBSK");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("00000002-0000-0000-0000-000000000005"),
                column: "PasswordHash",
                value: "$2a$04$WuSJZNktuI9FLcH.Az5HpeKJFhyNCxNzz7tR3jTVdzusa7qAJBBSK");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("00000003-0000-0000-0000-000000000001"),
                column: "PasswordHash",
                value: "$2a$04$WuSJZNktuI9FLcH.Az5HpeKJFhyNCxNzz7tR3jTVdzusa7qAJBBSK");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("00000003-0000-0000-0000-000000000002"),
                column: "PasswordHash",
                value: "$2a$04$WuSJZNktuI9FLcH.Az5HpeKJFhyNCxNzz7tR3jTVdzusa7qAJBBSK");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("00000003-0000-0000-0000-000000000003"),
                column: "PasswordHash",
                value: "$2a$04$WuSJZNktuI9FLcH.Az5HpeKJFhyNCxNzz7tR3jTVdzusa7qAJBBSK");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("00000003-0000-0000-0000-000000000004"),
                column: "PasswordHash",
                value: "$2a$04$WuSJZNktuI9FLcH.Az5HpeKJFhyNCxNzz7tR3jTVdzusa7qAJBBSK");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("00000003-0000-0000-0000-000000000005"),
                column: "PasswordHash",
                value: "$2a$04$WuSJZNktuI9FLcH.Az5HpeKJFhyNCxNzz7tR3jTVdzusa7qAJBBSK");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("00000004-0000-0000-0000-000000000001"),
                column: "PasswordHash",
                value: "$2a$04$WuSJZNktuI9FLcH.Az5HpeKJFhyNCxNzz7tR3jTVdzusa7qAJBBSK");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("00000004-0000-0000-0000-000000000002"),
                column: "PasswordHash",
                value: "$2a$04$WuSJZNktuI9FLcH.Az5HpeKJFhyNCxNzz7tR3jTVdzusa7qAJBBSK");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("00000004-0000-0000-0000-000000000003"),
                column: "PasswordHash",
                value: "$2a$04$WuSJZNktuI9FLcH.Az5HpeKJFhyNCxNzz7tR3jTVdzusa7qAJBBSK");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("00000004-0000-0000-0000-000000000004"),
                column: "PasswordHash",
                value: "$2a$04$WuSJZNktuI9FLcH.Az5HpeKJFhyNCxNzz7tR3jTVdzusa7qAJBBSK");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("00000004-0000-0000-0000-000000000005"),
                column: "PasswordHash",
                value: "$2a$04$WuSJZNktuI9FLcH.Az5HpeKJFhyNCxNzz7tR3jTVdzusa7qAJBBSK");
        }
    }
}
