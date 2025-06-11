using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Data.Migrations
{
    /// <inheritdoc />
    public partial class updateOrder : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "TableNumber",
                table: "Orders");

            migrationBuilder.AddColumn<int>(
                name: "TableNumberID",
                table: "Orders",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Orders_TableNumberID",
                table: "Orders",
                column: "TableNumberID");

            migrationBuilder.AddForeignKey(
                name: "FK_Orders_Tables_TableNumberID",
                table: "Orders",
                column: "TableNumberID",
                principalTable: "Tables",
                principalColumn: "TableID",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Orders_Tables_TableNumberID",
                table: "Orders");

            migrationBuilder.DropIndex(
                name: "IX_Orders_TableNumberID",
                table: "Orders");

            migrationBuilder.DropColumn(
                name: "TableNumberID",
                table: "Orders");

            migrationBuilder.AddColumn<string>(
                name: "TableNumber",
                table: "Orders",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }
    }
}
