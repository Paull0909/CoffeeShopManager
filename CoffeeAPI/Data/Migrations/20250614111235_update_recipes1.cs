using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Data.Migrations
{
    /// <inheritdoc />
    public partial class update_recipes1 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Recipes_Materials_MaterialsMaterialID",
                table: "Recipes");

            migrationBuilder.DropIndex(
                name: "IX_Recipes_MaterialsMaterialID",
                table: "Recipes");

            migrationBuilder.DropColumn(
                name: "MaterialsMaterialID",
                table: "Recipes");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "MaterialsMaterialID",
                table: "Recipes",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Recipes_MaterialsMaterialID",
                table: "Recipes",
                column: "MaterialsMaterialID");

            migrationBuilder.AddForeignKey(
                name: "FK_Recipes_Materials_MaterialsMaterialID",
                table: "Recipes",
                column: "MaterialsMaterialID",
                principalTable: "Materials",
                principalColumn: "MaterialID");
        }
    }
}
