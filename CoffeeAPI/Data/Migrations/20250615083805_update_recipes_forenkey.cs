using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Data.Migrations
{
    /// <inheritdoc />
    public partial class update_recipes_forenkey : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Recipes_ProductSizeID",
                table: "Recipes");

            migrationBuilder.CreateIndex(
                name: "IX_Recipes_ProductSizeID",
                table: "Recipes",
                column: "ProductSizeID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Recipes_ProductSizeID",
                table: "Recipes");

            migrationBuilder.CreateIndex(
                name: "IX_Recipes_ProductSizeID",
                table: "Recipes",
                column: "ProductSizeID",
                unique: true);
        }
    }
}
