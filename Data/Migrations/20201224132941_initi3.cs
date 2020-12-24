using Microsoft.EntityFrameworkCore.Migrations;

namespace Data.Migrations
{
    public partial class initi3 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "BlogCategoryID",
                table: "Blogs",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Blogs_BlogCategoryID",
                table: "Blogs",
                column: "BlogCategoryID");

            migrationBuilder.AddForeignKey(
                name: "FK_Blogs_BlogCategories_BlogCategoryID",
                table: "Blogs",
                column: "BlogCategoryID",
                principalTable: "BlogCategories",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Blogs_BlogCategories_BlogCategoryID",
                table: "Blogs");

            migrationBuilder.DropIndex(
                name: "IX_Blogs_BlogCategoryID",
                table: "Blogs");

            migrationBuilder.DropColumn(
                name: "BlogCategoryID",
                table: "Blogs");
        }
    }
}
