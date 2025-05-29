using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Data.Migrations
{
    /// <inheritdoc />
    public partial class editentity : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "SupplierID",
                table: "Materials",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Materials_SupplierID",
                table: "Materials",
                column: "SupplierID");

            migrationBuilder.AddForeignKey(
                name: "FK_Materials_Suppliers_SupplierID",
                table: "Materials",
                column: "SupplierID",
                principalTable: "Suppliers",
                principalColumn: "SupplierID",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Materials_Suppliers_SupplierID",
                table: "Materials");

            migrationBuilder.DropIndex(
                name: "IX_Materials_SupplierID",
                table: "Materials");

            migrationBuilder.DropColumn(
                name: "SupplierID",
                table: "Materials");
        }
    }
}
