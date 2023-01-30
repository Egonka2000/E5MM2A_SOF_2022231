using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FootballWesbshop.Migrations
{
    public partial class photo_blob : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ContentType",
                table: "Shoes");

            migrationBuilder.DropColumn(
                name: "Data",
                table: "Shoes");

            migrationBuilder.AddColumn<string>(
                name: "PhotoUrl",
                table: "Shoes",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PhotoUrl",
                table: "Shoes");

            migrationBuilder.AddColumn<string>(
                name: "ContentType",
                table: "Shoes",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<byte[]>(
                name: "Data",
                table: "Shoes",
                type: "varbinary(max)",
                nullable: true);
        }
    }
}
