using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CareerGuidancePlatform.Migrations
{
    /// <inheritdoc />
    public partial class fixmentor : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "RequestedAt",
                table: "Mentors",
                type: "datetime(6)",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<int>(
                name: "Status",
                table: "Mentors",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "RequestedAt",
                table: "Mentors");

            migrationBuilder.DropColumn(
                name: "Status",
                table: "Mentors");
        }
    }
}
