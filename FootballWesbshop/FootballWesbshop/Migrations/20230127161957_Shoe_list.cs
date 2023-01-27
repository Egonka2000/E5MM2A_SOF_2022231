using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FootballWesbshop.Migrations
{
    public partial class Shoe_list : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<byte[]>(
                name: "Data",
                table: "Shoes",
                type: "varbinary(max)",
                nullable: true,
                oldClrType: typeof(byte[]),
                oldType: "varbinary(max)");

            migrationBuilder.AlterColumn<string>(
                name: "ContentType",
                table: "Shoes",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AddColumn<string>(
                name: "SiteUserId",
                table: "Shoes",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Shoes_SiteUserId",
                table: "Shoes",
                column: "SiteUserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Shoes_AspNetUsers_SiteUserId",
                table: "Shoes",
                column: "SiteUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Shoes_AspNetUsers_SiteUserId",
                table: "Shoes");

            migrationBuilder.DropIndex(
                name: "IX_Shoes_SiteUserId",
                table: "Shoes");

            migrationBuilder.DropColumn(
                name: "SiteUserId",
                table: "Shoes");

            migrationBuilder.AlterColumn<byte[]>(
                name: "Data",
                table: "Shoes",
                type: "varbinary(max)",
                nullable: false,
                defaultValue: new byte[0],
                oldClrType: typeof(byte[]),
                oldType: "varbinary(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "ContentType",
                table: "Shoes",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);
        }
    }
}
