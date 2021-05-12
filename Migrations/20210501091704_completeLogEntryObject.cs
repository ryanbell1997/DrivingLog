using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace DrivingLog.Migrations
{
    public partial class completeLogEntryObject : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "Date",
                table: "LogEntries",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<decimal>(
                name: "QuantityCharged",
                table: "LogEntries",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<DateTime>(
                name: "TotalTime",
                table: "LogEntries",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Date",
                table: "LogEntries");

            migrationBuilder.DropColumn(
                name: "QuantityCharged",
                table: "LogEntries");

            migrationBuilder.DropColumn(
                name: "TotalTime",
                table: "LogEntries");
        }
    }
}
