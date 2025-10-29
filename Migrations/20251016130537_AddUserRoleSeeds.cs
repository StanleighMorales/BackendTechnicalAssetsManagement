using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace BackendTechnicalAssetsManagement.Migrations
{
    /// <inheritdoc />
    public partial class AddUserRoleSeeds : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "Email", "FirstName", "LastName", "MiddleName", "PasswordHash", "PhoneNumber", "Status", "UserRole", "Username" },
                values: new object[,]
                {
                    { new Guid("0f1e0268-a800-4800-9b4c-36fab9398ecb"), "student1@example.com", "Student1", "Jones", null, "AQAAAAIAAYagAAAAEAQl1GYxLGI0UJuu7SkBMgm1m2PszK89YBWBiKnLZ8po6PBFC/6yuc5f4qrFw31qVQ==", "0912134567", "Offline", "Student", "student1" },
                    { new Guid("17dd384c-e54f-4981-acca-025d93b36560"), "staff3@example.com", "Staff3", "Doe", null, "AQAAAAIAAYagAAAAEI613a2m2wC5xay2mryiNN2TaXX/SYDt59DGmkP6wLwcFLNBT86XmdXXku0pG68oyA==", "0998376543", "Offline", "Staff", "staff3" },
                    { new Guid("1b759e23-ade6-417a-b253-175a4f0f8ae6"), "admin3@example.com", "Admin3", "User", null, "AQAAAAIAAYagAAAAEA/Ntn+PAGAtFKRJIj6VGG8XL/6sDJrflRYvB2wPRjCp9wquQw7MuLeoDFS029fPBQ==", null, "Offline", "Admin", "admin3" },
                    { new Guid("1ed05039-5195-4847-ab11-a399bde07314"), "student3@example.com", "Student3", "Jones", null, "AQAAAAIAAYagAAAAEE7M56prYxT2PJXnKRhN1tzVjeGSFPAtobvhXYCA+cHdxvC0jmLEkERx+ShjeeZ1og==", "0912334567", "Offline", "Student", "student3" },
                    { new Guid("1fea876e-f3f8-4369-9623-2890b8022eb3"), "student2@example.com", "Student2", "Jones", null, "AQAAAAIAAYagAAAAEPgMgeU1qDtnoMYm1uZgjJrqtTIu/5XlI0l2/VFeS/onTg1d6uG0d1QU6cfnmjKqMQ==", "0912234567", "Offline", "Student", "student2" },
                    { new Guid("386af524-dae4-4c56-a87f-d8069bde52bf"), "admin2@example.com", "Admin2", "User", null, "AQAAAAIAAYagAAAAEDPz2bA10swqRu4EM6ICQe5Rx2rkF5ZRYXkr2lhVddfGI5h8WRIFGKR8db93XdxIXg==", null, "Offline", "Admin", "admin2" },
                    { new Guid("426b7e7d-a6ea-49a6-9248-1d9397741646"), "teacher1@example.com", "Teacher1", "Smith", null, "AQAAAAIAAYagAAAAEPLW0K/INgF7WIGpugMMcnyKhID1YOZG2qrSl9HMI+Y+l/q8sQ7vllH91x+Jg8xQpw==", "0917123456", "Offline", "Teacher", "teacher1" },
                    { new Guid("458fc8b1-8f35-4bd8-bfca-c8e3e1f07e22"), "teacher5@example.com", "Teacher5", "Smith", null, "AQAAAAIAAYagAAAAEHH4FBFgJjNYT+rHKHQKBECi9eCFbvZgBYVPSt8QZLHW1NANdtEU/jFMRuspaY0K2Q==", "0917523456", "Offline", "Teacher", "teacher5" },
                    { new Guid("47112c52-d7b0-4724-865c-3507b9b06b1f"), "staff1@example.com", "Staff1", "Doe", null, "AQAAAAIAAYagAAAAEAqGHT4+NxkVPKSbYrCUvtanm/2/t02SEdPb/sReqVg2svMyDIOTo6W4UShzJ+cwcQ==", "0998176543", "Offline", "Staff", "staff1" },
                    { new Guid("4a36aec4-7b85-4c6f-a243-40433f8ce0f7"), "admin5@example.com", "Admin5", "User", null, "AQAAAAIAAYagAAAAEN49yxefeXE901kZzPm++hy3wIiETf16fS+2n/b6pj4qx6/aNf6Ko0O7phN6aY0eLQ==", null, "Offline", "Admin", "admin5" },
                    { new Guid("4b1d35b2-75b0-4e59-b67d-49dfb8061053"), "staff5@example.com", "Staff5", "Doe", null, "AQAAAAIAAYagAAAAEBatC5xXtet4DMmSKDquf56dJh+ZwwVAjd/j1XJ0mMKulZBT7tuwvQaP7OXedrOmbA==", "0998576543", "Offline", "Staff", "staff5" },
                    { new Guid("54a28d24-0c4b-4696-93ff-196f1d922c7a"), "teacher3@example.com", "Teacher3", "Smith", null, "AQAAAAIAAYagAAAAEKmc2QPxO4PXCIY/qWitv6NLqUNpWBpbM5084WRFORZot3qAC4WOOCVPITNlFRq3fw==", "0917323456", "Offline", "Teacher", "teacher3" },
                    { new Guid("6de9eb5c-0d29-41ad-ac42-e292bc2ea689"), "student4@example.com", "Student4", "Jones", null, "AQAAAAIAAYagAAAAEKkCV1l0Jqo0haTtJhDgR/u3oQm4yFb3dOPMt8XKa84SOp/86+jONFYgG/0NbxxGHA==", "0912434567", "Offline", "Student", "student4" },
                    { new Guid("988c222d-6c56-443f-bdf9-927cf443a2fa"), "admin4@example.com", "Admin4", "User", null, "AQAAAAIAAYagAAAAEEinYoyo5ljI5CHVyEFr6FFHbDBcPIpFF49LZWvSjwwhagnJTL5zVfa0Dsxf+cz9Rg==", null, "Offline", "Admin", "admin4" },
                    { new Guid("9b8e7cb2-dace-4bb0-91c2-2fcb539bf40c"), "admin1@example.com", "Admin1", "User", null, "AQAAAAIAAYagAAAAEDKsTlotsJUi7SPau78m89HX25DB7aa3aNk9EvPor0TlMSvEI7lP/OCvrwpuy/IEaA==", null, "Offline", "Admin", "admin1" },
                    { new Guid("c86e8899-dd93-4af4-805b-c15676f0b1ca"), "superadmin@example.com", "Super", "Admin", null, "AQAAAAIAAYagAAAAEJhthNcN8l/2xQI1VZZDApn5y96PyKaTi8ZZtzQHxuBRwtn/Bw5iribGYbPU0vWGBw==", null, "Offline", "SuperAdmin", "superadmin" },
                    { new Guid("d2993d16-a66a-4f7e-aed5-42a4636ccf5d"), "teacher2@example.com", "Teacher2", "Smith", null, "AQAAAAIAAYagAAAAED+teb+FvJTKvPbLt8VWva4QyJKPyGrd8QEKMcs5ddMs51JAkPqWVhcWJ+WPl6vWKQ==", "0917223456", "Offline", "Teacher", "teacher2" },
                    { new Guid("d72165ec-a7f7-4925-9063-15a15f7db606"), "staff4@example.com", "Staff4", "Doe", null, "AQAAAAIAAYagAAAAEEn7+152TmNPrwIgN7YxNPhHXuL1OoL7AOWPUFWFy0bDayV/MVQCvdWqAuHoOLRbMQ==", "0998476543", "Offline", "Staff", "staff4" },
                    { new Guid("e387f4da-284c-4756-9469-357772cd67ea"), "staff2@example.com", "Staff2", "Doe", null, "AQAAAAIAAYagAAAAEOsGfBBuu6D+X2Ozruvms8Zu717mTQMtAJqqstc8LvcyQZISW3NbyNvHIZwx7MXKcA==", "0998276543", "Offline", "Staff", "staff2" },
                    { new Guid("f6d7128e-8eba-400e-81fb-cfaa1d3f3e1c"), "student5@example.com", "Student5", "Jones", null, "AQAAAAIAAYagAAAAENeV4diWFAuYKQX1pqHTVlaVSBtjferwtS1QJlBBiKO22H4jnpvLLejtkwDRTT7/Xw==", "0912534567", "Offline", "Student", "student5" },
                    { new Guid("fdfbb9e1-307e-4528-8124-a2e2cc747cb4"), "teacher4@example.com", "Teacher4", "Smith", null, "AQAAAAIAAYagAAAAEL14QpjXKxBGPjMOgC0kXD+bhe5U4SnyJe66LL13zsb3E0d7ZRdH6sgOXlC/6/Slnw==", "0917423456", "Offline", "Teacher", "teacher4" }
                });

            migrationBuilder.InsertData(
                table: "Staff",
                columns: new[] { "Id", "Position" },
                values: new object[,]
                {
                    { new Guid("17dd384c-e54f-4981-acca-025d93b36560"), "Lab Technician" },
                    { new Guid("47112c52-d7b0-4724-865c-3507b9b06b1f"), "Lab Technician" },
                    { new Guid("4b1d35b2-75b0-4e59-b67d-49dfb8061053"), "Lab Technician" },
                    { new Guid("d72165ec-a7f7-4925-9063-15a15f7db606"), "Lab Technician" },
                    { new Guid("e387f4da-284c-4756-9469-357772cd67ea"), "Lab Technician" }
                });

            migrationBuilder.InsertData(
                table: "Students",
                columns: new[] { "Id", "BackStudentIdPicture", "CityMunicipality", "Course", "FrontStudentIdPicture", "PostalCode", "ProfilePicture", "Province", "Section", "Street", "StudentIdNumber", "Year" },
                values: new object[,]
                {
                    { new Guid("0f1e0268-a800-4800-9b4c-36fab9398ecb"), null, "Sample City", "Computer Science", null, "12345", null, "Sample Province", "A", "123 Main St #1", "2023-0001", "3" },
                    { new Guid("1ed05039-5195-4847-ab11-a399bde07314"), null, "Sample City", "Computer Science", null, "12345", null, "Sample Province", "A", "123 Main St #3", "2023-0003", "3" },
                    { new Guid("1fea876e-f3f8-4369-9623-2890b8022eb3"), null, "Sample City", "Computer Science", null, "12345", null, "Sample Province", "A", "123 Main St #2", "2023-0002", "3" },
                    { new Guid("6de9eb5c-0d29-41ad-ac42-e292bc2ea689"), null, "Sample City", "Computer Science", null, "12345", null, "Sample Province", "A", "123 Main St #4", "2023-0004", "3" },
                    { new Guid("f6d7128e-8eba-400e-81fb-cfaa1d3f3e1c"), null, "Sample City", "Computer Science", null, "12345", null, "Sample Province", "A", "123 Main St #5", "2023-0005", "3" }
                });

            migrationBuilder.InsertData(
                table: "Teachers",
                columns: new[] { "Id", "Department" },
                values: new object[,]
                {
                    { new Guid("426b7e7d-a6ea-49a6-9248-1d9397741646"), "Information Technology" },
                    { new Guid("458fc8b1-8f35-4bd8-bfca-c8e3e1f07e22"), "Information Technology" },
                    { new Guid("54a28d24-0c4b-4696-93ff-196f1d922c7a"), "Information Technology" },
                    { new Guid("d2993d16-a66a-4f7e-aed5-42a4636ccf5d"), "Information Technology" },
                    { new Guid("fdfbb9e1-307e-4528-8124-a2e2cc747cb4"), "Information Technology" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Staff",
                keyColumn: "Id",
                keyValue: new Guid("17dd384c-e54f-4981-acca-025d93b36560"));

            migrationBuilder.DeleteData(
                table: "Staff",
                keyColumn: "Id",
                keyValue: new Guid("47112c52-d7b0-4724-865c-3507b9b06b1f"));

            migrationBuilder.DeleteData(
                table: "Staff",
                keyColumn: "Id",
                keyValue: new Guid("4b1d35b2-75b0-4e59-b67d-49dfb8061053"));

            migrationBuilder.DeleteData(
                table: "Staff",
                keyColumn: "Id",
                keyValue: new Guid("d72165ec-a7f7-4925-9063-15a15f7db606"));

            migrationBuilder.DeleteData(
                table: "Staff",
                keyColumn: "Id",
                keyValue: new Guid("e387f4da-284c-4756-9469-357772cd67ea"));

            migrationBuilder.DeleteData(
                table: "Students",
                keyColumn: "Id",
                keyValue: new Guid("0f1e0268-a800-4800-9b4c-36fab9398ecb"));

            migrationBuilder.DeleteData(
                table: "Students",
                keyColumn: "Id",
                keyValue: new Guid("1ed05039-5195-4847-ab11-a399bde07314"));

            migrationBuilder.DeleteData(
                table: "Students",
                keyColumn: "Id",
                keyValue: new Guid("1fea876e-f3f8-4369-9623-2890b8022eb3"));

            migrationBuilder.DeleteData(
                table: "Students",
                keyColumn: "Id",
                keyValue: new Guid("6de9eb5c-0d29-41ad-ac42-e292bc2ea689"));

            migrationBuilder.DeleteData(
                table: "Students",
                keyColumn: "Id",
                keyValue: new Guid("f6d7128e-8eba-400e-81fb-cfaa1d3f3e1c"));

            migrationBuilder.DeleteData(
                table: "Teachers",
                keyColumn: "Id",
                keyValue: new Guid("426b7e7d-a6ea-49a6-9248-1d9397741646"));

            migrationBuilder.DeleteData(
                table: "Teachers",
                keyColumn: "Id",
                keyValue: new Guid("458fc8b1-8f35-4bd8-bfca-c8e3e1f07e22"));

            migrationBuilder.DeleteData(
                table: "Teachers",
                keyColumn: "Id",
                keyValue: new Guid("54a28d24-0c4b-4696-93ff-196f1d922c7a"));

            migrationBuilder.DeleteData(
                table: "Teachers",
                keyColumn: "Id",
                keyValue: new Guid("d2993d16-a66a-4f7e-aed5-42a4636ccf5d"));

            migrationBuilder.DeleteData(
                table: "Teachers",
                keyColumn: "Id",
                keyValue: new Guid("fdfbb9e1-307e-4528-8124-a2e2cc747cb4"));

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("1b759e23-ade6-417a-b253-175a4f0f8ae6"));

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("386af524-dae4-4c56-a87f-d8069bde52bf"));

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("4a36aec4-7b85-4c6f-a243-40433f8ce0f7"));

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("988c222d-6c56-443f-bdf9-927cf443a2fa"));

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("9b8e7cb2-dace-4bb0-91c2-2fcb539bf40c"));

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("c86e8899-dd93-4af4-805b-c15676f0b1ca"));

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("0f1e0268-a800-4800-9b4c-36fab9398ecb"));

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("17dd384c-e54f-4981-acca-025d93b36560"));

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("1ed05039-5195-4847-ab11-a399bde07314"));

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("1fea876e-f3f8-4369-9623-2890b8022eb3"));

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("426b7e7d-a6ea-49a6-9248-1d9397741646"));

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("458fc8b1-8f35-4bd8-bfca-c8e3e1f07e22"));

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("47112c52-d7b0-4724-865c-3507b9b06b1f"));

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("4b1d35b2-75b0-4e59-b67d-49dfb8061053"));

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("54a28d24-0c4b-4696-93ff-196f1d922c7a"));

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("6de9eb5c-0d29-41ad-ac42-e292bc2ea689"));

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("d2993d16-a66a-4f7e-aed5-42a4636ccf5d"));

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("d72165ec-a7f7-4925-9063-15a15f7db606"));

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("e387f4da-284c-4756-9469-357772cd67ea"));

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("f6d7128e-8eba-400e-81fb-cfaa1d3f3e1c"));

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("fdfbb9e1-307e-4528-8124-a2e2cc747cb4"));
        }
    }
}
