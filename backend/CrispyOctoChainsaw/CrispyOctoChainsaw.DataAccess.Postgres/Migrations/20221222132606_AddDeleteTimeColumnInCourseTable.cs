using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CrispyOctoChainsaw.DataAccess.Postgres.Migrations
{
    public partial class AddDeleteTimeColumnInCourseTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTimeOffset>(
                name: "DeleteTime",
                table: "Courses",
                type: "timestamp with time zone",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DeleteTime",
                table: "Courses");
        }
    }
}
