using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace DrivingLog.Migrations
{
    public partial class timeSpanAlteration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<TimeSpan>(
                name: "TotalTime",
                table: "LogEntries",
                type: "time",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "datetime2");

            migrationBuilder.UpdateData(
                table: "LogEntries",
                keyColumn: "Id",
                keyValue: 1,
                column: "TotalTime",
                value: new TimeSpan(0, 0, 0, 0, 0));

            migrationBuilder.UpdateData(
                table: "LogEntries",
                keyColumn: "Id",
                keyValue: 2,
                column: "TotalTime",
                value: new TimeSpan(0, 0, 0, 0, 0));

            migrationBuilder.UpdateData(
                table: "LogEntries",
                keyColumn: "Id",
                keyValue: 3,
                column: "TotalTime",
                value: new TimeSpan(0, 0, 0, 0, 0));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTime>(
                name: "TotalTime",
                table: "LogEntries",
                type: "datetime2",
                nullable: false,
                oldClrType: typeof(TimeSpan),
                oldType: "time");

            migrationBuilder.UpdateData(
                table: "LogEntries",
                keyColumn: "Id",
                keyValue: 1,
                column: "TotalTime",
                value: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.UpdateData(
                table: "LogEntries",
                keyColumn: "Id",
                keyValue: 2,
                column: "TotalTime",
                value: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.UpdateData(
                table: "LogEntries",
                keyColumn: "Id",
                keyValue: 3,
                column: "TotalTime",
                value: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }
    }
}
