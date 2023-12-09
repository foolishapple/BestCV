using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BestCV.Infrastructure.Persistence.Migrations
{
    public partial class edit_OrderSort_not_null_CVTemplate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "OrderSort",
                table: "CVTemplate",
                type: "int",
                nullable: false,
                defaultValue: 0,
                comment: "Sắp xếp",
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.UpdateData(
                table: "AccountStatus",
                keyColumn: "Id",
                keyValue: 1001,
                column: "CreatedTime",
                value: new DateTime(2023, 9, 11, 16, 22, 22, 676, DateTimeKind.Local).AddTicks(1443));

            migrationBuilder.UpdateData(
                table: "AccountStatus",
                keyColumn: "Id",
                keyValue: 1002,
                column: "CreatedTime",
                value: new DateTime(2023, 9, 11, 16, 22, 22, 676, DateTimeKind.Local).AddTicks(1502));

            migrationBuilder.UpdateData(
                table: "AdminAccount",
                keyColumn: "Id",
                keyValue: 1001L,
                column: "CreatedTime",
                value: new DateTime(2023, 9, 11, 16, 22, 22, 676, DateTimeKind.Local).AddTicks(1622));

            migrationBuilder.UpdateData(
                table: "CVTemplateStatus",
                keyColumn: "Id",
                keyValue: 1001L,
                column: "CreatedTime",
                value: new DateTime(2023, 9, 11, 16, 22, 22, 676, DateTimeKind.Local).AddTicks(1762));

            migrationBuilder.UpdateData(
                table: "CVTemplateStatus",
                keyColumn: "Id",
                keyValue: 1002L,
                column: "CreatedTime",
                value: new DateTime(2023, 9, 11, 16, 22, 22, 676, DateTimeKind.Local).AddTicks(1782));

            migrationBuilder.UpdateData(
                table: "CandidateLevel",
                keyColumn: "Id",
                keyValue: 1001,
                column: "CreatedTime",
                value: new DateTime(2023, 9, 11, 16, 22, 22, 676, DateTimeKind.Local).AddTicks(1683));

            migrationBuilder.UpdateData(
                table: "Position",
                keyColumn: "Id",
                keyValue: 1001,
                column: "CreatedTime",
                value: new DateTime(2023, 9, 11, 16, 22, 22, 676, DateTimeKind.Local).AddTicks(1718));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "OrderSort",
                table: "CVTemplate",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int",
                oldComment: "Sắp xếp");

            migrationBuilder.UpdateData(
                table: "AccountStatus",
                keyColumn: "Id",
                keyValue: 1001,
                column: "CreatedTime",
                value: new DateTime(2023, 9, 11, 16, 14, 31, 244, DateTimeKind.Local).AddTicks(2953));

            migrationBuilder.UpdateData(
                table: "AccountStatus",
                keyColumn: "Id",
                keyValue: 1002,
                column: "CreatedTime",
                value: new DateTime(2023, 9, 11, 16, 14, 31, 244, DateTimeKind.Local).AddTicks(3053));

            migrationBuilder.UpdateData(
                table: "AdminAccount",
                keyColumn: "Id",
                keyValue: 1001L,
                column: "CreatedTime",
                value: new DateTime(2023, 9, 11, 16, 14, 31, 244, DateTimeKind.Local).AddTicks(3173));

            migrationBuilder.UpdateData(
                table: "CVTemplateStatus",
                keyColumn: "Id",
                keyValue: 1001L,
                column: "CreatedTime",
                value: new DateTime(2023, 9, 11, 16, 14, 31, 244, DateTimeKind.Local).AddTicks(3392));

            migrationBuilder.UpdateData(
                table: "CVTemplateStatus",
                keyColumn: "Id",
                keyValue: 1002L,
                column: "CreatedTime",
                value: new DateTime(2023, 9, 11, 16, 14, 31, 244, DateTimeKind.Local).AddTicks(3428));

            migrationBuilder.UpdateData(
                table: "CandidateLevel",
                keyColumn: "Id",
                keyValue: 1001,
                column: "CreatedTime",
                value: new DateTime(2023, 9, 11, 16, 14, 31, 244, DateTimeKind.Local).AddTicks(3265));

            migrationBuilder.UpdateData(
                table: "Position",
                keyColumn: "Id",
                keyValue: 1001,
                column: "CreatedTime",
                value: new DateTime(2023, 9, 11, 16, 14, 31, 244, DateTimeKind.Local).AddTicks(3320));
        }
    }
}
