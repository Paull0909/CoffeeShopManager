using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Data.Migrations
{
    /// <inheritdoc />
    public partial class position : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Positions_Users_UserID",
                table: "Positions");

            migrationBuilder.RenameColumn(
                name: "UserID",
                table: "Positions",
                newName: "UserId");

            migrationBuilder.RenameIndex(
                name: "IX_Positions_UserID",
                table: "Positions",
                newName: "IX_Positions_UserId");

            migrationBuilder.AlterColumn<Guid>(
                name: "UserId",
                table: "Positions",
                type: "uniqueidentifier",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier");

            migrationBuilder.AddForeignKey(
                name: "FK_Positions_Users_UserId",
                table: "Positions",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Positions_Users_UserId",
                table: "Positions");

            migrationBuilder.RenameColumn(
                name: "UserId",
                table: "Positions",
                newName: "UserID");

            migrationBuilder.RenameIndex(
                name: "IX_Positions_UserId",
                table: "Positions",
                newName: "IX_Positions_UserID");

            migrationBuilder.AlterColumn<Guid>(
                name: "UserID",
                table: "Positions",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Positions_Users_UserID",
                table: "Positions",
                column: "UserID",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
