using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Jobi.Infrastructure.Persistence.Migrations
{
    public partial class fixDefaultValueEndDateInEmployerServicePackage_v1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTime>(
                name: "DiscountEndDate",
                table: "EmployerServicePackage",
                type: "datetime2",
                nullable: true,
                comment: "Ngày kết thúc mã giảm giá",
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldNullable: true,
                oldDefaultValueSql: "(getdate())",
                oldComment: "Ngày kết thúc mã giảm giá");

            migrationBuilder.UpdateData(
                table: "AccountStatus",
                keyColumn: "Id",
                keyValue: 1001,
                column: "CreatedTime",
                value: new DateTime(2023, 9, 8, 10, 13, 36, 822, DateTimeKind.Local).AddTicks(6234));

            migrationBuilder.UpdateData(
                table: "AccountStatus",
                keyColumn: "Id",
                keyValue: 1002,
                column: "CreatedTime",
                value: new DateTime(2023, 9, 8, 10, 13, 36, 822, DateTimeKind.Local).AddTicks(6278));

            migrationBuilder.UpdateData(
                table: "AdminAccount",
                keyColumn: "Id",
                keyValue: 1001L,
                column: "CreatedTime",
                value: new DateTime(2023, 9, 8, 10, 13, 36, 822, DateTimeKind.Local).AddTicks(6302));

            migrationBuilder.UpdateData(
                table: "CandidateLevel",
                keyColumn: "Id",
                keyValue: 1001,
                column: "CreatedTime",
                value: new DateTime(2023, 9, 8, 10, 13, 36, 822, DateTimeKind.Local).AddTicks(6341));

            migrationBuilder.UpdateData(
                table: "Position",
                keyColumn: "Id",
                keyValue: 1001,
                column: "CreatedTime",
                value: new DateTime(2023, 9, 8, 10, 13, 36, 822, DateTimeKind.Local).AddTicks(6370));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTime>(
                name: "DiscountEndDate",
                table: "EmployerServicePackage",
                type: "datetime2",
                nullable: true,
                defaultValueSql: "(getdate())",
                comment: "Ngày kết thúc mã giảm giá",
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldNullable: true,
                oldComment: "Ngày kết thúc mã giảm giá");

            migrationBuilder.UpdateData(
                table: "AccountStatus",
                keyColumn: "Id",
                keyValue: 1001,
                column: "CreatedTime",
                value: new DateTime(2023, 9, 7, 18, 12, 26, 383, DateTimeKind.Local).AddTicks(6456));

            migrationBuilder.UpdateData(
                table: "AccountStatus",
                keyColumn: "Id",
                keyValue: 1002,
                column: "CreatedTime",
                value: new DateTime(2023, 9, 7, 18, 12, 26, 383, DateTimeKind.Local).AddTicks(6504));

            migrationBuilder.UpdateData(
                table: "AdminAccount",
                keyColumn: "Id",
                keyValue: 1001L,
                column: "CreatedTime",
                value: new DateTime(2023, 9, 7, 18, 12, 26, 383, DateTimeKind.Local).AddTicks(6531));

            migrationBuilder.UpdateData(
                table: "CandidateLevel",
                keyColumn: "Id",
                keyValue: 1001,
                column: "CreatedTime",
                value: new DateTime(2023, 9, 7, 18, 12, 26, 383, DateTimeKind.Local).AddTicks(6569));

            migrationBuilder.UpdateData(
                table: "Position",
                keyColumn: "Id",
                keyValue: 1001,
                column: "CreatedTime",
                value: new DateTime(2023, 9, 7, 18, 12, 26, 383, DateTimeKind.Local).AddTicks(6608));
        }
    }
}
