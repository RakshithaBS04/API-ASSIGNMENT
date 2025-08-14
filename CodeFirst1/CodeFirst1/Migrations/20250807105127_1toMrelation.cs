using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CodeFirst1.Migrations
{
    /// <inheritdoc />
    public partial class _1toMrelation : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Category",
                table: "Products");

            migrationBuilder.AddColumn<int>(
                name: "catId",
                table: "Products",
                type: "int",
                nullable: true
                );

            migrationBuilder.CreateIndex(
                name: "IX_Products_catId",
                table: "Products",
                column: "catId");

            migrationBuilder.AddForeignKey(
                name: "FK_Products_Categories_catId",
                table: "Products",
                column: "catId",
                principalTable: "Categories",
                principalColumn: "CategoryId",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Products_Categories_catId",
                table: "Products");

            migrationBuilder.DropIndex(
                name: "IX_Products_catId",
                table: "Products");

            migrationBuilder.DropColumn(
                name: "catId",
                table: "Products");

            migrationBuilder.AddColumn<string>(
                name: "Category",
                table: "Products",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }
    }
}
