using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Data.Migrations
{
    /// <inheritdoc />
    public partial class edittablelot : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PurchasePrice",
                table: "Materials");

            migrationBuilder.DropColumn(
                name: "Quantity",
                table: "Materials");

            migrationBuilder.AddColumn<decimal>(
                name: "PurchasePrice",
                table: "Lot",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PurchasePrice",
                table: "Lot");

            migrationBuilder.AddColumn<decimal>(
                name: "PurchasePrice",
                table: "Materials",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<float>(
                name: "Quantity",
                table: "Materials",
                type: "real",
                nullable: false,
                defaultValue: 0f);
        }
    }
}
