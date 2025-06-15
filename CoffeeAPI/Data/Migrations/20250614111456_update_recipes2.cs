using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Data.Migrations
{
    /// <inheritdoc />
    public partial class update_recipes2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Recipes_Products_ProductsProductID",
                table: "Recipes");

            migrationBuilder.DropIndex(
                name: "IX_Recipes_ProductsProductID",
                table: "Recipes");

            migrationBuilder.DropColumn(
                name: "ProductsProductID",
                table: "Recipes");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ProductsProductID",
                table: "Recipes",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Recipes_ProductsProductID",
                table: "Recipes",
                column: "ProductsProductID");

            migrationBuilder.AddForeignKey(
                name: "FK_Recipes_Products_ProductsProductID",
                table: "Recipes",
                column: "ProductsProductID",
                principalTable: "Products",
                principalColumn: "ProductID");
        }
    }
}
