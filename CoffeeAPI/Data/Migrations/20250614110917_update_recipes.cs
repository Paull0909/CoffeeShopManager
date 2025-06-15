using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Data.Migrations
{
    /// <inheritdoc />
    public partial class update_recipes : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Recipes_Materials_MaterialID",
                table: "Recipes");

            migrationBuilder.DropIndex(
                name: "IX_Recipes_MaterialID",
                table: "Recipes");

            migrationBuilder.DropColumn(
                name: "MaterialID",
                table: "Recipes");

            migrationBuilder.AddColumn<string>(
                name: "MaterialName",
                table: "Recipes",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

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

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Recipes_Materials_MaterialsMaterialID",
                table: "Recipes");

            migrationBuilder.DropIndex(
                name: "IX_Recipes_MaterialsMaterialID",
                table: "Recipes");

            migrationBuilder.DropColumn(
                name: "MaterialName",
                table: "Recipes");

            migrationBuilder.DropColumn(
                name: "MaterialsMaterialID",
                table: "Recipes");

            migrationBuilder.AddColumn<int>(
                name: "MaterialID",
                table: "Recipes",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Recipes_MaterialID",
                table: "Recipes",
                column: "MaterialID");

            migrationBuilder.AddForeignKey(
                name: "FK_Recipes_Materials_MaterialID",
                table: "Recipes",
                column: "MaterialID",
                principalTable: "Materials",
                principalColumn: "MaterialID",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
