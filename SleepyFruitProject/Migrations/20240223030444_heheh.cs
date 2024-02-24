using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SleepyFruitProject.Migrations
{
    public partial class heheh : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<TimeSpan>(
                name: "ElapsedTime",
                table: "OurUsers",
                type: "time",
                nullable: false,
                defaultValue: new TimeSpan(0, 0, 0, 0, 0));

            migrationBuilder.AddColumn<int>(
                name: "question",
                table: "OurUsers",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ElapsedTime",
                table: "OurUsers");

            migrationBuilder.DropColumn(
                name: "question",
                table: "OurUsers");
        }
    }
}
