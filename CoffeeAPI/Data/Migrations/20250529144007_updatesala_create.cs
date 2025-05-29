using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Data.Migrations
{
    /// <inheritdoc />
    public partial class updatesala_create : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            // Thêm cột tạm CreatedAt_temp kiểu date, cho phép NULL để copy dữ liệu
            migrationBuilder.Sql("ALTER TABLE Salaries ADD CreatedAt_temp date NULL");

            // Nếu bạn có cách chuyển dữ liệu decimal sang date, làm ở đây. 
            // Ví dụ giả định cột decimal lưu ngày dưới dạng số yyyymmdd (vd 20230529)
            // Bạn có thể convert bằng cách này:
            migrationBuilder.Sql(@"
        UPDATE Salaries 
        SET CreatedAt_temp = TRY_CONVERT(date, 
            CONCAT(
                SUBSTRING(CAST(CreatedAt AS varchar), 1, 4), '-', 
                SUBSTRING(CAST(CreatedAt AS varchar), 5, 2), '-', 
                SUBSTRING(CAST(CreatedAt AS varchar), 7, 2)
            )
        )
        WHERE CreatedAt IS NOT NULL
    ");

            // Xóa cột cũ CreatedAt
            migrationBuilder.DropColumn(name: "CreatedAt", table: "Salaries");

            // Đổi tên cột tạm thành CreatedAt
            migrationBuilder.Sql("EXEC sp_rename 'Salaries.CreatedAt_temp', 'CreatedAt', 'COLUMN'");

            // Thay đổi cột CreatedAt thành NOT NULL với default GETDATE()
            migrationBuilder.AlterColumn<DateOnly>(
                name: "CreatedAt",
                table: "Salaries",
                type: "date",
                nullable: false,
                defaultValueSql: "GETDATE()",
                oldClrType: typeof(DateOnly),
                oldType: "date");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            // Quay về kiểu cũ (DateTime hoặc decimal) tuỳ trường hợp bạn thiết kế lại migration Down
            // Có thể cần tương tự tạo cột tạm, đổi kiểu ngược lại
        }

    }
}
