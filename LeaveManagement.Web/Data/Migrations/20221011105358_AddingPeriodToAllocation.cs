using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LeaveManagement.Web.Data.Migrations
{
    public partial class AddingPeriodToAllocation : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Period",
                table: "LeaveAllocations",
                type: "int",
                nullable: false,
                defaultValue: 0);

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

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Period",
                table: "LeaveAllocations");

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
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "df4f3d74-41d2-4f1f-bff0-cbdc98678d4b", "AQAAAAEAACcQAAAAELRsMPluZFYi7hkeDZOHmHVTQMIrDo/ixiY2dGtJfPUl3ytNI8e/SHojJpDf65mNNg==", "5a75d6ce-6c67-425e-84d3-9f6c507444dd" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "82f7a62a-832a-418b-b6ae-340d98f3e40b",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "b22fcf06-564e-4288-8fd2-400ce2ff82b4", "AQAAAAEAACcQAAAAEOLXYWZG6V07uzMBp9tGYmiQmRDLoVTpxxKo2qjIxryE7IqbnXLDWG5zFarJZlGC2g==", "d56819bc-e17c-45ad-a0a9-5e697e926ba4" });
        }
    }
}
