using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SleepyFruitProject.Migrations
{
    public partial class UpdatedUserTime : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<TimeSpan>(
                name: "ElapsedTime",
                table: "OurUsers",
                type: "time",
                nullable: true,
                oldClrType: typeof(TimeSpan),
                oldType: "time");

            migrationBuilder.AddColumn<TimeSpan>(
                name: "BestTime",
                table: "OurUsers",
                type: "time",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "end_time",
                table: "OurUsers",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "start_time",
                table: "OurUsers",
                type: "datetime2",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "BestTime",
                table: "OurUsers");

            migrationBuilder.DropColumn(
                name: "end_time",
                table: "OurUsers");

            migrationBuilder.DropColumn(
                name: "start_time",
                table: "OurUsers");

            migrationBuilder.AlterColumn<TimeSpan>(
                name: "ElapsedTime",
                table: "OurUsers",
                type: "time",
                nullable: false,
                defaultValue: new TimeSpan(0, 0, 0, 0, 0),
                oldClrType: typeof(TimeSpan),
                oldType: "time",
                oldNullable: true);
        }
    }
}
