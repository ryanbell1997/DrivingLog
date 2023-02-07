using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace DrivingLog.Migrations
{
    public partial class timeSpanUpdated : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
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

            migrationBuilder.AlterColumn<TimeSpan>(
                name: "StartDateTime",
                table: "LogEntries",
                type: "time",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "datetime2");

            migrationBuilder.AlterColumn<TimeSpan>(
                name: "FinishDateTime",
                table: "LogEntries",
                type: "time",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "datetime2");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTime>(
                name: "StartDateTime",
                table: "LogEntries",
                type: "datetime2",
                nullable: false,
                oldClrType: typeof(TimeSpan),
                oldType: "time");

            migrationBuilder.AlterColumn<DateTime>(
                name: "FinishDateTime",
                table: "LogEntries",
                type: "datetime2",
                nullable: false,
                oldClrType: typeof(TimeSpan),
                oldType: "time");

            migrationBuilder.InsertData(
                table: "LogEntries",
                columns: new[] { "Id", "Date", "FinishDateTime", "QuantityCharged", "StartDateTime", "TotalTime" },
                values: new object[] { 1, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2020, 2, 24, 0, 0, 0, 0, DateTimeKind.Unspecified), 0m, new DateTime(2020, 2, 23, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0) });

            migrationBuilder.InsertData(
                table: "LogEntries",
                columns: new[] { "Id", "Date", "FinishDateTime", "QuantityCharged", "StartDateTime", "TotalTime" },
                values: new object[] { 2, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2020, 2, 26, 0, 0, 0, 0, DateTimeKind.Unspecified), 0m, new DateTime(2020, 2, 25, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0) });

            migrationBuilder.InsertData(
                table: "LogEntries",
                columns: new[] { "Id", "Date", "FinishDateTime", "QuantityCharged", "StartDateTime", "TotalTime" },
                values: new object[] { 3, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2020, 2, 28, 0, 0, 0, 0, DateTimeKind.Unspecified), 0m, new DateTime(2020, 2, 27, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0) });
        }
    }
}
