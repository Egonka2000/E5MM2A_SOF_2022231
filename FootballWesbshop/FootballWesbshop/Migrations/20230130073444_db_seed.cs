using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FootballWesbshop.Migrations
{
    public partial class db_seed : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "Type",
                table: "Brands",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.UpdateData(
                table: "Brands",
                keyColumn: "Uid",
                keyValue: "2",
                columns: new[] { "Name", "Type" },
                values: new object[] { "Adidas", 1 });

            migrationBuilder.InsertData(
                table: "Brands",
                columns: new[] { "Uid", "Name", "Type" },
                values: new object[,]
                {
                    { "3", "Adidas", 2 },
                    { "4", "Nike", 3 },
                    { "5", "Nike", 4 }
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Brands",
                keyColumn: "Uid",
                keyValue: "3");

            migrationBuilder.DeleteData(
                table: "Brands",
                keyColumn: "Uid",
                keyValue: "4");

            migrationBuilder.DeleteData(
                table: "Brands",
                keyColumn: "Uid",
                keyValue: "5");

            migrationBuilder.AlterColumn<int>(
                name: "Type",
                table: "Brands",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.UpdateData(
                table: "Brands",
                keyColumn: "Uid",
                keyValue: "2",
                columns: new[] { "Name", "Type" },
                values: new object[] { "Nike", 0 });
        }
    }
}
