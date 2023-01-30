using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FootballWesbshop.Migrations
{
    public partial class sequence : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Sequence",
                table: "Shoes",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Sequence",
                table: "Shoes");
        }
    }
}
