using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BackendTechnicalAssetsManagement.Migrations
{
    /// <inheritdoc />
    public partial class AddRfidRegistrationSessions : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "RfidRegistrationSessions",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    ItemId = table.Column<Guid>(type: "uuid", nullable: false),
                    Status = table.Column<string>(type: "text", nullable: false),
                    ScannedRfidUid = table.Column<string>(type: "text", nullable: true),
                    ErrorMessage = table.Column<string>(type: "text", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    ExpiresAt = table.Column<DateTime>(type: "timestamp without time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RfidRegistrationSessions", x => x.Id);
                    table.ForeignKey(
                        name: "FK_RfidRegistrationSessions_Items_ItemId",
                        column: x => x.ItemId,
                        principalTable: "Items",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.UpdateData(
                table: "ArchiveItems",
                keyColumn: "Id",
                keyValue: new Guid("00000007-0000-0000-0000-000000000005"),
                column: "ImageUrl",
                value: "temp/items/item_06_hdmi_short.png");

            migrationBuilder.UpdateData(
                table: "ArchiveItems",
                keyColumn: "Id",
                keyValue: new Guid("00000007-0000-0000-0000-000000000006"),
                column: "ImageUrl",
                value: "temp/items/item_07_usb_mic.png");

            migrationBuilder.UpdateData(
                table: "ArchiveItems",
                keyColumn: "Id",
                keyValue: new Guid("00000007-0000-0000-0000-000000000007"),
                column: "ImageUrl",
                value: "temp/items/item_08_mouse.png");

            migrationBuilder.UpdateData(
                table: "ArchiveItems",
                keyColumn: "Id",
                keyValue: new Guid("00000007-0000-0000-0000-000000000008"),
                column: "ImageUrl",
                value: "temp/items/item_09_extension.png");

            migrationBuilder.UpdateData(
                table: "ArchiveItems",
                keyColumn: "Id",
                keyValue: new Guid("00000007-0000-0000-0000-000000000009"),
                column: "ImageUrl",
                value: "temp/items/item_10_usb_hub.png");

            migrationBuilder.UpdateData(
                table: "ArchiveUsers",
                keyColumn: "Id",
                keyValue: new Guid("00000007-0000-0000-0000-000000000001"),
                column: "PasswordHash",
                value: "$2a$04$i/Go7v4OgkdC/pmdKY4QDOoOtTQHFwktVc6E7fH.N97Y2cpgj0Ljm");

            migrationBuilder.UpdateData(
                table: "ArchiveUsers",
                keyColumn: "Id",
                keyValue: new Guid("00000007-0000-0000-0000-000000000002"),
                column: "PasswordHash",
                value: "$2a$04$i/Go7v4OgkdC/pmdKY4QDOoOtTQHFwktVc6E7fH.N97Y2cpgj0Ljm");

            migrationBuilder.UpdateData(
                table: "ArchiveUsers",
                keyColumn: "Id",
                keyValue: new Guid("00000007-0000-0000-0000-000000000003"),
                column: "PasswordHash",
                value: "$2a$04$i/Go7v4OgkdC/pmdKY4QDOoOtTQHFwktVc6E7fH.N97Y2cpgj0Ljm");

            migrationBuilder.UpdateData(
                table: "ArchiveUsers",
                keyColumn: "Id",
                keyValue: new Guid("00000007-0000-0000-0000-000000000004"),
                column: "PasswordHash",
                value: "$2a$04$i/Go7v4OgkdC/pmdKY4QDOoOtTQHFwktVc6E7fH.N97Y2cpgj0Ljm");

            migrationBuilder.UpdateData(
                table: "Items",
                keyColumn: "Id",
                keyValue: new Guid("00000005-0000-0000-0000-000000000001"),
                column: "ImageUrl",
                value: "temp/items/item_01_hdmi_cable.png");

            migrationBuilder.UpdateData(
                table: "Items",
                keyColumn: "Id",
                keyValue: new Guid("00000005-0000-0000-0000-000000000002"),
                column: "ImageUrl",
                value: "temp/items/item_02_wireless_mic.png");

            migrationBuilder.UpdateData(
                table: "Items",
                keyColumn: "Id",
                keyValue: new Guid("00000005-0000-0000-0000-000000000003"),
                column: "ImageUrl",
                value: "temp/items/item_03_projector.png");

            migrationBuilder.UpdateData(
                table: "Items",
                keyColumn: "Id",
                keyValue: new Guid("00000005-0000-0000-0000-000000000004"),
                column: "ImageUrl",
                value: "temp/items/item_04_speaker.png");

            migrationBuilder.UpdateData(
                table: "Items",
                keyColumn: "Id",
                keyValue: new Guid("00000005-0000-0000-0000-000000000005"),
                column: "ImageUrl",
                value: "temp/items/item_05_keyboard.png");

            migrationBuilder.UpdateData(
                table: "Items",
                keyColumn: "Id",
                keyValue: new Guid("00000005-0000-0000-0000-000000000006"),
                column: "ImageUrl",
                value: "temp/items/item_06_hdmi_short.png");

            migrationBuilder.UpdateData(
                table: "Items",
                keyColumn: "Id",
                keyValue: new Guid("00000005-0000-0000-0000-000000000007"),
                column: "ImageUrl",
                value: "temp/items/item_07_usb_mic.png");

            migrationBuilder.UpdateData(
                table: "Items",
                keyColumn: "Id",
                keyValue: new Guid("00000005-0000-0000-0000-000000000008"),
                column: "ImageUrl",
                value: "temp/items/item_08_mouse.png");

            migrationBuilder.UpdateData(
                table: "Items",
                keyColumn: "Id",
                keyValue: new Guid("00000005-0000-0000-0000-000000000009"),
                column: "ImageUrl",
                value: "temp/items/item_09_extension.png");

            migrationBuilder.UpdateData(
                table: "Items",
                keyColumn: "Id",
                keyValue: new Guid("00000005-0000-0000-0000-000000000010"),
                column: "ImageUrl",
                value: "temp/items/item_10_usb_hub.png");

            migrationBuilder.UpdateData(
                table: "LentItems",
                keyColumn: "Id",
                keyValue: new Guid("00000006-0000-0000-0000-000000000001"),
                column: "FrontStudentIdPictureUrl",
                value: "temp/students/id-front/student_01_id_front.png");

            migrationBuilder.UpdateData(
                table: "LentItems",
                keyColumn: "Id",
                keyValue: new Guid("00000006-0000-0000-0000-000000000002"),
                column: "FrontStudentIdPictureUrl",
                value: "temp/students/id-front/student_02_id_front.png");

            migrationBuilder.UpdateData(
                table: "LentItems",
                keyColumn: "Id",
                keyValue: new Guid("00000006-0000-0000-0000-000000000004"),
                column: "FrontStudentIdPictureUrl",
                value: "temp/students/id-front/student_03_id_front.png");

            migrationBuilder.UpdateData(
                table: "Students",
                keyColumn: "Id",
                keyValue: new Guid("00000004-0000-0000-0000-000000000001"),
                columns: new[] { "BackStudentIdPictureUrl", "FrontStudentIdPictureUrl", "ProfilePictureUrl" },
                values: new object[] { "temp/students/id-back/student_01_id_back.png", "temp/students/id-front/student_01_id_front.png", "temp/students/profile/student_01_profile.png" });

            migrationBuilder.UpdateData(
                table: "Students",
                keyColumn: "Id",
                keyValue: new Guid("00000004-0000-0000-0000-000000000002"),
                columns: new[] { "BackStudentIdPictureUrl", "FrontStudentIdPictureUrl", "ProfilePictureUrl" },
                values: new object[] { "temp/students/id-back/student_02_id_back.png", "temp/students/id-front/student_02_id_front.png", "temp/students/profile/student_02_profile.png" });

            migrationBuilder.UpdateData(
                table: "Students",
                keyColumn: "Id",
                keyValue: new Guid("00000004-0000-0000-0000-000000000003"),
                columns: new[] { "BackStudentIdPictureUrl", "FrontStudentIdPictureUrl", "ProfilePictureUrl" },
                values: new object[] { "temp/students/id-back/student_03_id_back.png", "temp/students/id-front/student_03_id_front.png", "temp/students/profile/student_03_profile.png" });

            migrationBuilder.UpdateData(
                table: "Students",
                keyColumn: "Id",
                keyValue: new Guid("00000004-0000-0000-0000-000000000004"),
                columns: new[] { "BackStudentIdPictureUrl", "FrontStudentIdPictureUrl", "ProfilePictureUrl" },
                values: new object[] { "temp/students/id-back/student_04_id_back.png", "temp/students/id-front/student_04_id_front.png", "temp/students/profile/student_04_profile.png" });

            migrationBuilder.UpdateData(
                table: "Students",
                keyColumn: "Id",
                keyValue: new Guid("00000004-0000-0000-0000-000000000005"),
                columns: new[] { "BackStudentIdPictureUrl", "FrontStudentIdPictureUrl", "ProfilePictureUrl" },
                values: new object[] { "temp/students/id-back/student_05_id_back.png", "temp/students/id-front/student_05_id_front.png", "temp/students/profile/student_05_profile.png" });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("00000001-0000-0000-0000-000000000001"),
                column: "PasswordHash",
                value: "$2a$04$i/Go7v4OgkdC/pmdKY4QDOoOtTQHFwktVc6E7fH.N97Y2cpgj0Ljm");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("00000001-0000-0000-0000-000000000002"),
                column: "PasswordHash",
                value: "$2a$04$i/Go7v4OgkdC/pmdKY4QDOoOtTQHFwktVc6E7fH.N97Y2cpgj0Ljm");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("00000001-0000-0000-0000-000000000003"),
                column: "PasswordHash",
                value: "$2a$04$i/Go7v4OgkdC/pmdKY4QDOoOtTQHFwktVc6E7fH.N97Y2cpgj0Ljm");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("00000001-0000-0000-0000-000000000004"),
                column: "PasswordHash",
                value: "$2a$04$i/Go7v4OgkdC/pmdKY4QDOoOtTQHFwktVc6E7fH.N97Y2cpgj0Ljm");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("00000001-0000-0000-0000-000000000005"),
                column: "PasswordHash",
                value: "$2a$04$i/Go7v4OgkdC/pmdKY4QDOoOtTQHFwktVc6E7fH.N97Y2cpgj0Ljm");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("00000002-0000-0000-0000-000000000001"),
                column: "PasswordHash",
                value: "$2a$04$i/Go7v4OgkdC/pmdKY4QDOoOtTQHFwktVc6E7fH.N97Y2cpgj0Ljm");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("00000002-0000-0000-0000-000000000002"),
                column: "PasswordHash",
                value: "$2a$04$i/Go7v4OgkdC/pmdKY4QDOoOtTQHFwktVc6E7fH.N97Y2cpgj0Ljm");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("00000002-0000-0000-0000-000000000003"),
                column: "PasswordHash",
                value: "$2a$04$i/Go7v4OgkdC/pmdKY4QDOoOtTQHFwktVc6E7fH.N97Y2cpgj0Ljm");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("00000002-0000-0000-0000-000000000004"),
                column: "PasswordHash",
                value: "$2a$04$i/Go7v4OgkdC/pmdKY4QDOoOtTQHFwktVc6E7fH.N97Y2cpgj0Ljm");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("00000002-0000-0000-0000-000000000005"),
                column: "PasswordHash",
                value: "$2a$04$i/Go7v4OgkdC/pmdKY4QDOoOtTQHFwktVc6E7fH.N97Y2cpgj0Ljm");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("00000003-0000-0000-0000-000000000001"),
                column: "PasswordHash",
                value: "$2a$04$i/Go7v4OgkdC/pmdKY4QDOoOtTQHFwktVc6E7fH.N97Y2cpgj0Ljm");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("00000003-0000-0000-0000-000000000002"),
                column: "PasswordHash",
                value: "$2a$04$i/Go7v4OgkdC/pmdKY4QDOoOtTQHFwktVc6E7fH.N97Y2cpgj0Ljm");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("00000003-0000-0000-0000-000000000003"),
                column: "PasswordHash",
                value: "$2a$04$i/Go7v4OgkdC/pmdKY4QDOoOtTQHFwktVc6E7fH.N97Y2cpgj0Ljm");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("00000003-0000-0000-0000-000000000004"),
                column: "PasswordHash",
                value: "$2a$04$i/Go7v4OgkdC/pmdKY4QDOoOtTQHFwktVc6E7fH.N97Y2cpgj0Ljm");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("00000003-0000-0000-0000-000000000005"),
                column: "PasswordHash",
                value: "$2a$04$i/Go7v4OgkdC/pmdKY4QDOoOtTQHFwktVc6E7fH.N97Y2cpgj0Ljm");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("00000004-0000-0000-0000-000000000001"),
                column: "PasswordHash",
                value: "$2a$04$i/Go7v4OgkdC/pmdKY4QDOoOtTQHFwktVc6E7fH.N97Y2cpgj0Ljm");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("00000004-0000-0000-0000-000000000002"),
                column: "PasswordHash",
                value: "$2a$04$i/Go7v4OgkdC/pmdKY4QDOoOtTQHFwktVc6E7fH.N97Y2cpgj0Ljm");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("00000004-0000-0000-0000-000000000003"),
                column: "PasswordHash",
                value: "$2a$04$i/Go7v4OgkdC/pmdKY4QDOoOtTQHFwktVc6E7fH.N97Y2cpgj0Ljm");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("00000004-0000-0000-0000-000000000004"),
                column: "PasswordHash",
                value: "$2a$04$i/Go7v4OgkdC/pmdKY4QDOoOtTQHFwktVc6E7fH.N97Y2cpgj0Ljm");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("00000004-0000-0000-0000-000000000005"),
                column: "PasswordHash",
                value: "$2a$04$i/Go7v4OgkdC/pmdKY4QDOoOtTQHFwktVc6E7fH.N97Y2cpgj0Ljm");

            migrationBuilder.CreateIndex(
                name: "IX_RfidRegistrationSessions_ItemId",
                table: "RfidRegistrationSessions",
                column: "ItemId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "RfidRegistrationSessions");

            migrationBuilder.UpdateData(
                table: "ArchiveItems",
                keyColumn: "Id",
                keyValue: new Guid("00000007-0000-0000-0000-000000000005"),
                column: "ImageUrl",
                value: "https://oeyoyyxeluzaeckwpcsa.supabase.co/storage/v1/object/public/technical-bucket/items/item_06_hdmi_short.png");

            migrationBuilder.UpdateData(
                table: "ArchiveItems",
                keyColumn: "Id",
                keyValue: new Guid("00000007-0000-0000-0000-000000000006"),
                column: "ImageUrl",
                value: "https://oeyoyyxeluzaeckwpcsa.supabase.co/storage/v1/object/public/technical-bucket/items/item_07_usb_mic.png");

            migrationBuilder.UpdateData(
                table: "ArchiveItems",
                keyColumn: "Id",
                keyValue: new Guid("00000007-0000-0000-0000-000000000007"),
                column: "ImageUrl",
                value: "https://oeyoyyxeluzaeckwpcsa.supabase.co/storage/v1/object/public/technical-bucket/items/item_08_mouse.png");

            migrationBuilder.UpdateData(
                table: "ArchiveItems",
                keyColumn: "Id",
                keyValue: new Guid("00000007-0000-0000-0000-000000000008"),
                column: "ImageUrl",
                value: "https://oeyoyyxeluzaeckwpcsa.supabase.co/storage/v1/object/public/technical-bucket/items/item_09_extension.png");

            migrationBuilder.UpdateData(
                table: "ArchiveItems",
                keyColumn: "Id",
                keyValue: new Guid("00000007-0000-0000-0000-000000000009"),
                column: "ImageUrl",
                value: "https://oeyoyyxeluzaeckwpcsa.supabase.co/storage/v1/object/public/technical-bucket/items/item_10_usb_hub.png");

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
                table: "Items",
                keyColumn: "Id",
                keyValue: new Guid("00000005-0000-0000-0000-000000000001"),
                column: "ImageUrl",
                value: "https://oeyoyyxeluzaeckwpcsa.supabase.co/storage/v1/object/public/technical-bucket/items/item_01_hdmi_cable.png");

            migrationBuilder.UpdateData(
                table: "Items",
                keyColumn: "Id",
                keyValue: new Guid("00000005-0000-0000-0000-000000000002"),
                column: "ImageUrl",
                value: "https://oeyoyyxeluzaeckwpcsa.supabase.co/storage/v1/object/public/technical-bucket/items/item_02_wireless_mic.png");

            migrationBuilder.UpdateData(
                table: "Items",
                keyColumn: "Id",
                keyValue: new Guid("00000005-0000-0000-0000-000000000003"),
                column: "ImageUrl",
                value: "https://oeyoyyxeluzaeckwpcsa.supabase.co/storage/v1/object/public/technical-bucket/items/item_03_projector.png");

            migrationBuilder.UpdateData(
                table: "Items",
                keyColumn: "Id",
                keyValue: new Guid("00000005-0000-0000-0000-000000000004"),
                column: "ImageUrl",
                value: "https://oeyoyyxeluzaeckwpcsa.supabase.co/storage/v1/object/public/technical-bucket/items/item_04_speaker.png");

            migrationBuilder.UpdateData(
                table: "Items",
                keyColumn: "Id",
                keyValue: new Guid("00000005-0000-0000-0000-000000000005"),
                column: "ImageUrl",
                value: "https://oeyoyyxeluzaeckwpcsa.supabase.co/storage/v1/object/public/technical-bucket/items/item_05_keyboard.png");

            migrationBuilder.UpdateData(
                table: "Items",
                keyColumn: "Id",
                keyValue: new Guid("00000005-0000-0000-0000-000000000006"),
                column: "ImageUrl",
                value: "https://oeyoyyxeluzaeckwpcsa.supabase.co/storage/v1/object/public/technical-bucket/items/item_06_hdmi_short.png");

            migrationBuilder.UpdateData(
                table: "Items",
                keyColumn: "Id",
                keyValue: new Guid("00000005-0000-0000-0000-000000000007"),
                column: "ImageUrl",
                value: "https://oeyoyyxeluzaeckwpcsa.supabase.co/storage/v1/object/public/technical-bucket/items/item_07_usb_mic.png");

            migrationBuilder.UpdateData(
                table: "Items",
                keyColumn: "Id",
                keyValue: new Guid("00000005-0000-0000-0000-000000000008"),
                column: "ImageUrl",
                value: "https://oeyoyyxeluzaeckwpcsa.supabase.co/storage/v1/object/public/technical-bucket/items/item_08_mouse.png");

            migrationBuilder.UpdateData(
                table: "Items",
                keyColumn: "Id",
                keyValue: new Guid("00000005-0000-0000-0000-000000000009"),
                column: "ImageUrl",
                value: "https://oeyoyyxeluzaeckwpcsa.supabase.co/storage/v1/object/public/technical-bucket/items/item_09_extension.png");

            migrationBuilder.UpdateData(
                table: "Items",
                keyColumn: "Id",
                keyValue: new Guid("00000005-0000-0000-0000-000000000010"),
                column: "ImageUrl",
                value: "https://oeyoyyxeluzaeckwpcsa.supabase.co/storage/v1/object/public/technical-bucket/items/item_10_usb_hub.png");

            migrationBuilder.UpdateData(
                table: "LentItems",
                keyColumn: "Id",
                keyValue: new Guid("00000006-0000-0000-0000-000000000001"),
                column: "FrontStudentIdPictureUrl",
                value: "https://oeyoyyxeluzaeckwpcsa.supabase.co/storage/v1/object/public/technical-bucket/students/id-front/student_01_id_front.png");

            migrationBuilder.UpdateData(
                table: "LentItems",
                keyColumn: "Id",
                keyValue: new Guid("00000006-0000-0000-0000-000000000002"),
                column: "FrontStudentIdPictureUrl",
                value: "https://oeyoyyxeluzaeckwpcsa.supabase.co/storage/v1/object/public/technical-bucket/students/id-front/student_02_id_front.png");

            migrationBuilder.UpdateData(
                table: "LentItems",
                keyColumn: "Id",
                keyValue: new Guid("00000006-0000-0000-0000-000000000004"),
                column: "FrontStudentIdPictureUrl",
                value: "https://oeyoyyxeluzaeckwpcsa.supabase.co/storage/v1/object/public/technical-bucket/students/id-front/student_03_id_front.png");

            migrationBuilder.UpdateData(
                table: "Students",
                keyColumn: "Id",
                keyValue: new Guid("00000004-0000-0000-0000-000000000001"),
                columns: new[] { "BackStudentIdPictureUrl", "FrontStudentIdPictureUrl", "ProfilePictureUrl" },
                values: new object[] { "https://oeyoyyxeluzaeckwpcsa.supabase.co/storage/v1/object/public/technical-bucket/students/id-back/student_01_id_back.png", "https://oeyoyyxeluzaeckwpcsa.supabase.co/storage/v1/object/public/technical-bucket/students/id-front/student_01_id_front.png", "https://oeyoyyxeluzaeckwpcsa.supabase.co/storage/v1/object/public/technical-bucket/students/profile/student_01_profile.png" });

            migrationBuilder.UpdateData(
                table: "Students",
                keyColumn: "Id",
                keyValue: new Guid("00000004-0000-0000-0000-000000000002"),
                columns: new[] { "BackStudentIdPictureUrl", "FrontStudentIdPictureUrl", "ProfilePictureUrl" },
                values: new object[] { "https://oeyoyyxeluzaeckwpcsa.supabase.co/storage/v1/object/public/technical-bucket/students/id-back/student_02_id_back.png", "https://oeyoyyxeluzaeckwpcsa.supabase.co/storage/v1/object/public/technical-bucket/students/id-front/student_02_id_front.png", "https://oeyoyyxeluzaeckwpcsa.supabase.co/storage/v1/object/public/technical-bucket/students/profile/student_02_profile.png" });

            migrationBuilder.UpdateData(
                table: "Students",
                keyColumn: "Id",
                keyValue: new Guid("00000004-0000-0000-0000-000000000003"),
                columns: new[] { "BackStudentIdPictureUrl", "FrontStudentIdPictureUrl", "ProfilePictureUrl" },
                values: new object[] { "https://oeyoyyxeluzaeckwpcsa.supabase.co/storage/v1/object/public/technical-bucket/students/id-back/student_03_id_back.png", "https://oeyoyyxeluzaeckwpcsa.supabase.co/storage/v1/object/public/technical-bucket/students/id-front/student_03_id_front.png", "https://oeyoyyxeluzaeckwpcsa.supabase.co/storage/v1/object/public/technical-bucket/students/profile/student_03_profile.png" });

            migrationBuilder.UpdateData(
                table: "Students",
                keyColumn: "Id",
                keyValue: new Guid("00000004-0000-0000-0000-000000000004"),
                columns: new[] { "BackStudentIdPictureUrl", "FrontStudentIdPictureUrl", "ProfilePictureUrl" },
                values: new object[] { "https://oeyoyyxeluzaeckwpcsa.supabase.co/storage/v1/object/public/technical-bucket/students/id-back/student_04_id_back.png", "https://oeyoyyxeluzaeckwpcsa.supabase.co/storage/v1/object/public/technical-bucket/students/id-front/student_04_id_front.png", "https://oeyoyyxeluzaeckwpcsa.supabase.co/storage/v1/object/public/technical-bucket/students/profile/student_04_profile.png" });

            migrationBuilder.UpdateData(
                table: "Students",
                keyColumn: "Id",
                keyValue: new Guid("00000004-0000-0000-0000-000000000005"),
                columns: new[] { "BackStudentIdPictureUrl", "FrontStudentIdPictureUrl", "ProfilePictureUrl" },
                values: new object[] { "https://oeyoyyxeluzaeckwpcsa.supabase.co/storage/v1/object/public/technical-bucket/students/id-back/student_05_id_back.png", "https://oeyoyyxeluzaeckwpcsa.supabase.co/storage/v1/object/public/technical-bucket/students/id-front/student_05_id_front.png", "https://oeyoyyxeluzaeckwpcsa.supabase.co/storage/v1/object/public/technical-bucket/students/profile/student_05_profile.png" });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("00000001-0000-0000-0000-000000000001"),
                column: "PasswordHash",
                value: "$2a$04$l4RFhj2r1EkTTA20v450e.1aclI7uUyvFi23CeMQLZxY9PcID6fpW");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("00000001-0000-0000-0000-000000000002"),
                column: "PasswordHash",
                value: "$2a$04$l4RFhj2r1EkTTA20v450e.1aclI7uUyvFi23CeMQLZxY9PcID6fpW");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("00000001-0000-0000-0000-000000000003"),
                column: "PasswordHash",
                value: "$2a$04$l4RFhj2r1EkTTA20v450e.1aclI7uUyvFi23CeMQLZxY9PcID6fpW");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("00000001-0000-0000-0000-000000000004"),
                column: "PasswordHash",
                value: "$2a$04$l4RFhj2r1EkTTA20v450e.1aclI7uUyvFi23CeMQLZxY9PcID6fpW");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("00000001-0000-0000-0000-000000000005"),
                column: "PasswordHash",
                value: "$2a$04$l4RFhj2r1EkTTA20v450e.1aclI7uUyvFi23CeMQLZxY9PcID6fpW");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("00000002-0000-0000-0000-000000000001"),
                column: "PasswordHash",
                value: "$2a$04$l4RFhj2r1EkTTA20v450e.1aclI7uUyvFi23CeMQLZxY9PcID6fpW");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("00000002-0000-0000-0000-000000000002"),
                column: "PasswordHash",
                value: "$2a$04$l4RFhj2r1EkTTA20v450e.1aclI7uUyvFi23CeMQLZxY9PcID6fpW");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("00000002-0000-0000-0000-000000000003"),
                column: "PasswordHash",
                value: "$2a$04$l4RFhj2r1EkTTA20v450e.1aclI7uUyvFi23CeMQLZxY9PcID6fpW");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("00000002-0000-0000-0000-000000000004"),
                column: "PasswordHash",
                value: "$2a$04$l4RFhj2r1EkTTA20v450e.1aclI7uUyvFi23CeMQLZxY9PcID6fpW");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("00000002-0000-0000-0000-000000000005"),
                column: "PasswordHash",
                value: "$2a$04$l4RFhj2r1EkTTA20v450e.1aclI7uUyvFi23CeMQLZxY9PcID6fpW");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("00000003-0000-0000-0000-000000000001"),
                column: "PasswordHash",
                value: "$2a$04$l4RFhj2r1EkTTA20v450e.1aclI7uUyvFi23CeMQLZxY9PcID6fpW");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("00000003-0000-0000-0000-000000000002"),
                column: "PasswordHash",
                value: "$2a$04$l4RFhj2r1EkTTA20v450e.1aclI7uUyvFi23CeMQLZxY9PcID6fpW");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("00000003-0000-0000-0000-000000000003"),
                column: "PasswordHash",
                value: "$2a$04$l4RFhj2r1EkTTA20v450e.1aclI7uUyvFi23CeMQLZxY9PcID6fpW");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("00000003-0000-0000-0000-000000000004"),
                column: "PasswordHash",
                value: "$2a$04$l4RFhj2r1EkTTA20v450e.1aclI7uUyvFi23CeMQLZxY9PcID6fpW");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("00000003-0000-0000-0000-000000000005"),
                column: "PasswordHash",
                value: "$2a$04$l4RFhj2r1EkTTA20v450e.1aclI7uUyvFi23CeMQLZxY9PcID6fpW");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("00000004-0000-0000-0000-000000000001"),
                column: "PasswordHash",
                value: "$2a$04$l4RFhj2r1EkTTA20v450e.1aclI7uUyvFi23CeMQLZxY9PcID6fpW");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("00000004-0000-0000-0000-000000000002"),
                column: "PasswordHash",
                value: "$2a$04$l4RFhj2r1EkTTA20v450e.1aclI7uUyvFi23CeMQLZxY9PcID6fpW");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("00000004-0000-0000-0000-000000000003"),
                column: "PasswordHash",
                value: "$2a$04$l4RFhj2r1EkTTA20v450e.1aclI7uUyvFi23CeMQLZxY9PcID6fpW");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("00000004-0000-0000-0000-000000000004"),
                column: "PasswordHash",
                value: "$2a$04$l4RFhj2r1EkTTA20v450e.1aclI7uUyvFi23CeMQLZxY9PcID6fpW");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("00000004-0000-0000-0000-000000000005"),
                column: "PasswordHash",
                value: "$2a$04$l4RFhj2r1EkTTA20v450e.1aclI7uUyvFi23CeMQLZxY9PcID6fpW");
        }
    }
}
