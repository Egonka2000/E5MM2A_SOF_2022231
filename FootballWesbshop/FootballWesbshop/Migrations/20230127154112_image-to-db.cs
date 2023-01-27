using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FootballWesbshop.Migrations
{
    public partial class imagetodb : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ContentType",
                table: "Shoes",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<byte[]>(
                name: "Data",
                table: "Shoes",
                type: "varbinary(max)",
                nullable: false,
                defaultValue: new byte[0]);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ContentType",
                table: "Shoes");

            migrationBuilder.DropColumn(
                name: "Data",
                table: "Shoes");
        }
    }
}
