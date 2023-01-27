using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FootballWesbshop.Migrations
{
    public partial class valami : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
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
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
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
    }
}
