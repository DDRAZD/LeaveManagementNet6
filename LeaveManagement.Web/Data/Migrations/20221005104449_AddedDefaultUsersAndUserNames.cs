using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LeaveManagement.Web.Data.Migrations
{
    public partial class AddedDefaultUsersAndUserNames : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "0e496d22-f74d-4bd4-9be2-f3b0436f29ed",
                column: "ConcurrencyStamp",
                value: "f7ed3934-6014-4121-8237-41785d9cb6f9");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "82f7772a-888a-418b-b6ae-340d98f3e40b",
                column: "ConcurrencyStamp",
                value: "69712874-4c34-4b9f-b22d-24a3969fdfc7");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "0e496d22-f74d-4bd4-9be2-f3b0436f29ed",
                columns: new[] { "ConcurrencyStamp", "EmailConfirmed", "NormalizedUserName", "PasswordHash", "SecurityStamp", "UserName" },
                values: new object[] { "df4f3d74-41d2-4f1f-bff0-cbdc98678d4b", true, "USER@LOCALHOST.COM", "AQAAAAEAACcQAAAAELRsMPluZFYi7hkeDZOHmHVTQMIrDo/ixiY2dGtJfPUl3ytNI8e/SHojJpDf65mNNg==", "5a75d6ce-6c67-425e-84d3-9f6c507444dd", "user@localhost.com" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "82f7a62a-832a-418b-b6ae-340d98f3e40b",
                columns: new[] { "ConcurrencyStamp", "EmailConfirmed", "NormalizedUserName", "PasswordHash", "SecurityStamp", "UserName" },
                values: new object[] { "b22fcf06-564e-4288-8fd2-400ce2ff82b4", true, "ADMIN2@TEST.COM", "AQAAAAEAACcQAAAAEOLXYWZG6V07uzMBp9tGYmiQmRDLoVTpxxKo2qjIxryE7IqbnXLDWG5zFarJZlGC2g==", "d56819bc-e17c-45ad-a0a9-5e697e926ba4", "admin2@test.com" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "0e496d22-f74d-4bd4-9be2-f3b0436f29ed",
                column: "ConcurrencyStamp",
                value: "befd8fa0-2083-4b8c-8b7f-b4538d97f922");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "82f7772a-888a-418b-b6ae-340d98f3e40b",
                column: "ConcurrencyStamp",
                value: "637cf230-dc98-4086-945e-c3875611e96c");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "0e496d22-f74d-4bd4-9be2-f3b0436f29ed",
                columns: new[] { "ConcurrencyStamp", "EmailConfirmed", "NormalizedUserName", "PasswordHash", "SecurityStamp", "UserName" },
                values: new object[] { "7e768d3c-a367-409f-9b66-7cfa40408030", false, null, "AQAAAAEAACcQAAAAEPepwX/Ofu5FN9Ntkyxu6QHDNs5JoQjW64mvHeWr1G/y/a+QOji7ccH7Z4tm8GUtXw==", "a87ea25b-c689-4f0e-bc3b-970a8fb16c3b", null });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "82f7a62a-832a-418b-b6ae-340d98f3e40b",
                columns: new[] { "ConcurrencyStamp", "EmailConfirmed", "NormalizedUserName", "PasswordHash", "SecurityStamp", "UserName" },
                values: new object[] { "278ee127-2856-45cb-9426-bfd802c80597", false, null, "AQAAAAEAACcQAAAAEKauT3K+JTtIWCJdQhmGgpsbVpMpwPVV2w2vfp/VkwEadzCBqCc/bhybWVM5QjmSSg==", "bb9430bc-7f08-40aa-87c1-d4ea841eec49", null });
        }
    }
}
