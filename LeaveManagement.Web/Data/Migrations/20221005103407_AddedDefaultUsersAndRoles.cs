using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LeaveManagement.Web.Data.Migrations
{
    public partial class AddedDefaultUsersAndRoles : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "0e496d22-f74d-4bd4-9be2-f3b0436f29ed", "befd8fa0-2083-4b8c-8b7f-b4538d97f922", "Administrator", "ADMINISTRATOR" },
                    { "82f7772a-888a-418b-b6ae-340d98f3e40b", "637cf230-dc98-4086-945e-c3875611e96c", "User", "USER" }
                });

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "DateJoined", "DateOfBirth", "Email", "EmailConfirmed", "FirstName", "LastName", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TaxID", "TwoFactorEnabled", "UserName" },
                values: new object[,]
                {
                    { "0e496d22-f74d-4bd4-9be2-f3b0436f29ed", 0, "7e768d3c-a367-409f-9b66-7cfa40408030", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "user@localhost.com", false, "System", "User", false, null, "USER@LOCALHOST.COM", null, "AQAAAAEAACcQAAAAEPepwX/Ofu5FN9Ntkyxu6QHDNs5JoQjW64mvHeWr1G/y/a+QOji7ccH7Z4tm8GUtXw==", null, false, "a87ea25b-c689-4f0e-bc3b-970a8fb16c3b", null, false, null },
                    { "82f7a62a-832a-418b-b6ae-340d98f3e40b", 0, "278ee127-2856-45cb-9426-bfd802c80597", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "admin2@test.com", false, "System", "Admin", false, null, "ADMIN2@TEST.COM", null, "AQAAAAEAACcQAAAAEKauT3K+JTtIWCJdQhmGgpsbVpMpwPVV2w2vfp/VkwEadzCBqCc/bhybWVM5QjmSSg==", null, false, "bb9430bc-7f08-40aa-87c1-d4ea841eec49", null, false, null }
                });

            migrationBuilder.InsertData(
                table: "AspNetUserRoles",
                columns: new[] { "RoleId", "UserId" },
                values: new object[] { "82f7772a-888a-418b-b6ae-340d98f3e40b", "0e496d22-f74d-4bd4-9be2-f3b0436f29ed" });

            migrationBuilder.InsertData(
                table: "AspNetUserRoles",
                columns: new[] { "RoleId", "UserId" },
                values: new object[] { "0e496d22-f74d-4bd4-9be2-f3b0436f29ed", "82f7a62a-832a-418b-b6ae-340d98f3e40b" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { "82f7772a-888a-418b-b6ae-340d98f3e40b", "0e496d22-f74d-4bd4-9be2-f3b0436f29ed" });

            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { "0e496d22-f74d-4bd4-9be2-f3b0436f29ed", "82f7a62a-832a-418b-b6ae-340d98f3e40b" });

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "0e496d22-f74d-4bd4-9be2-f3b0436f29ed");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "82f7772a-888a-418b-b6ae-340d98f3e40b");

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "0e496d22-f74d-4bd4-9be2-f3b0436f29ed");

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "82f7a62a-832a-418b-b6ae-340d98f3e40b");
        }
    }
}
