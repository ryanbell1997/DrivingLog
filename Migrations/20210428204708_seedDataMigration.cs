using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace DrivingLog.Migrations
{
    public partial class seedDataMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "LogEntries",
                columns: new[] { "Id", "FinishDateTime", "StartDateTime" },
                values: new object[] { 1, new DateTime(2020, 2, 24, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2020, 2, 23, 0, 0, 0, 0, DateTimeKind.Unspecified) });

            migrationBuilder.InsertData(
                table: "LogEntries",
                columns: new[] { "Id", "FinishDateTime", "StartDateTime" },
                values: new object[] { 2, new DateTime(2020, 2, 26, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2020, 2, 25, 0, 0, 0, 0, DateTimeKind.Unspecified) });

            migrationBuilder.InsertData(
                table: "LogEntries",
                columns: new[] { "Id", "FinishDateTime", "StartDateTime" },
                values: new object[] { 3, new DateTime(2020, 2, 28, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2020, 2, 27, 0, 0, 0, 0, DateTimeKind.Unspecified) });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "LogEntries",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "LogEntries",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "LogEntries",
                keyColumn: "Id",
                keyValue: 3);
        }
    }
}
