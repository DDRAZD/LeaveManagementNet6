using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LeaveManagement.Web.Data.Migrations
{
    public partial class MadeRequestCommentsNullable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "RequestComments",
                table: "LeaveRequests",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "0e496d22-f74d-4bd4-9be2-f3b0436f29ed",
                column: "ConcurrencyStamp",
                value: "344bf864-dd74-41b4-8e1a-09987f647e94");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "82f7772a-888a-418b-b6ae-340d98f3e40b",
                column: "ConcurrencyStamp",
                value: "cc65ef66-2575-4474-af82-017ccaf620d7");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "0e496d22-f74d-4bd4-9be2-f3b0436f29ed",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "20784c81-1c16-4911-a5f6-e0e662ed9f26", "AQAAAAEAACcQAAAAECtnQkykEOAbC62dTEjfCwClsvVf6fRAJnVf8nPX2S1RnaFoq1aIWNwUsBoY42BEkg==", "bc05eba5-b2c4-495d-9c1a-f37253c0c493" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "82f7a62a-832a-418b-b6ae-340d98f3e40b",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "0eacf5b5-b815-4ad7-a9f8-8056c0ff498c", "AQAAAAEAACcQAAAAELMCc/PtLPPpOokszoz3sSkQiI6NrKRdW28EBfsG+i6VC2NVRfXk8nheGA7c9BjI7Q==", "58769992-f011-4796-9332-0c667173af41" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "RequestComments",
                table: "LeaveRequests",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "0e496d22-f74d-4bd4-9be2-f3b0436f29ed",
                column: "ConcurrencyStamp",
                value: "ec3c3e6f-8485-4bba-af90-62d8a1228148");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "82f7772a-888a-418b-b6ae-340d98f3e40b",
                column: "ConcurrencyStamp",
                value: "801d2189-5b87-43e6-aeb9-c64a61e8587b");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "0e496d22-f74d-4bd4-9be2-f3b0436f29ed",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "543ea473-a415-466d-a6cc-b4ab5fe3c766", "AQAAAAEAACcQAAAAEF2oSEPmopQM7udJJXp/71uusIxUJ8g/7o6aFlLZkzzFEzBr3zkiYT3Bjj0NztNrfg==", "fa854c4c-1c52-467d-a1bd-da68f098a30b" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "82f7a62a-832a-418b-b6ae-340d98f3e40b",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "a3f5a629-1dbc-4b32-8e46-f7472b27a371", "AQAAAAEAACcQAAAAEJDUBmEScMTkPZIkJfQZUv5RfcfWPxcWv8uaIzw14xWnUpNr6irLRhK685Nv7g1KMw==", "6b867ed5-9861-408f-b056-2936c0931342" });
        }
    }
}
