using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FootballWesbshop.Migrations
{
    public partial class repair : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Shoes_AspNetUsers_OwnerId",
                table: "Shoes");

            migrationBuilder.DropIndex(
                name: "IX_Shoes_OwnerId",
                table: "Shoes");

            migrationBuilder.DropColumn(
                name: "OwnerId",
                table: "Shoes");

            migrationBuilder.CreateTable(
                name: "ShoeSiteUser",
                columns: table => new
                {
                    CartUid = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    OwnersId = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ShoeSiteUser", x => new { x.CartUid, x.OwnersId });
                    table.ForeignKey(
                        name: "FK_ShoeSiteUser_AspNetUsers_OwnersId",
                        column: x => x.OwnersId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ShoeSiteUser_Shoes_CartUid",
                        column: x => x.CartUid,
                        principalTable: "Shoes",
                        principalColumn: "Uid",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ShoeSiteUser_OwnersId",
                table: "ShoeSiteUser",
                column: "OwnersId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ShoeSiteUser");

            migrationBuilder.AddColumn<string>(
                name: "OwnerId",
                table: "Shoes",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateIndex(
                name: "IX_Shoes_OwnerId",
                table: "Shoes",
                column: "OwnerId");

            migrationBuilder.AddForeignKey(
                name: "FK_Shoes_AspNetUsers_OwnerId",
                table: "Shoes",
                column: "OwnerId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
