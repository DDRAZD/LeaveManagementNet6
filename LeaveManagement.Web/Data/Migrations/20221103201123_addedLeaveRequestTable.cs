using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LeaveManagement.Web.Data.Migrations
{
    public partial class addedLeaveRequestTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "LeaveRequests",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    StartDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    EndDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    LeaveTypeId = table.Column<int>(type: "int", nullable: false),
                    DateRequested = table.Column<DateTime>(type: "datetime2", nullable: false),
                    RequestComments = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    RequestingEmployeeId = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Approved = table.Column<bool>(type: "bit", nullable: true),
                    Cancelled = table.Column<bool>(type: "bit", nullable: false),
                    DateCreated = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DateModified = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LeaveRequests", x => x.Id);
                    table.ForeignKey(
                        name: "FK_LeaveRequests_LeaveTypes_LeaveTypeId",
                        column: x => x.LeaveTypeId,
                        principalTable: "LeaveTypes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

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

            migrationBuilder.CreateIndex(
                name: "IX_LeaveRequests_LeaveTypeId",
                table: "LeaveRequests",
                column: "LeaveTypeId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "LeaveRequests");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "0e496d22-f74d-4bd4-9be2-f3b0436f29ed",
                column: "ConcurrencyStamp",
                value: "aa6740ca-5561-4780-b61e-34b2584766ed");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "82f7772a-888a-418b-b6ae-340d98f3e40b",
                column: "ConcurrencyStamp",
                value: "20f5b68e-7dde-45d5-bce1-2d1e864ea92e");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "0e496d22-f74d-4bd4-9be2-f3b0436f29ed",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "76b153a4-87bd-47e9-96e7-ea8d05d20e2d", "AQAAAAEAACcQAAAAEPg5OshINWmOD1YpUsQYRG0tj8NhBPUBdAUAvA4XlM4+QDhAAO1H0TKZpD9zCprRvw==", "fe1b326a-1965-4525-af5d-1115b96289eb" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "82f7a62a-832a-418b-b6ae-340d98f3e40b",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "117ceb17-dadc-477a-8890-87ecbb579adc", "AQAAAAEAACcQAAAAEMPRo8LbCHmx6flfJH22D19RDfs1YgYARYTVG2Jn3AZ2YtvMdkhIK2nGYsQyYvZFyw==", "02b28d2e-7838-4db2-96ba-7e4c7d210baf" });
        }
    }
}
