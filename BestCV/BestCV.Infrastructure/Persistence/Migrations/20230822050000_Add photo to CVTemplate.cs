using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BestCV.Infrastructure.Persistence.Migrations
{
    public partial class AddphototoCVTemplate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Description",
                table: "CVTemplate",
                type: "nvarchar(1000)",
                maxLength: 1000,
                nullable: true,
                comment: "Mô tả",
                oldClrType: typeof(string),
                oldType: "nvarchar(500)",
                oldMaxLength: 500,
                oldNullable: true,
                oldComment: "Mô tả");

            migrationBuilder.AddColumn<string>(
                name: "Photo",
                table: "CVTemplate",
                type: "nvarchar(500)",
                maxLength: 500,
                nullable: true,
                comment: "Ảnh Template");

            migrationBuilder.UpdateData(
                table: "AccountStatus",
                keyColumn: "Id",
                keyValue: 1001,
                column: "CreatedTime",
                value: new DateTime(2023, 8, 22, 11, 59, 55, 552, DateTimeKind.Local).AddTicks(6382));

            migrationBuilder.UpdateData(
                table: "AccountStatus",
                keyColumn: "Id",
                keyValue: 1002,
                column: "CreatedTime",
                value: new DateTime(2023, 8, 22, 11, 59, 55, 552, DateTimeKind.Local).AddTicks(6419));

            migrationBuilder.UpdateData(
                table: "AdminAccount",
                keyColumn: "Id",
                keyValue: 1001L,
                column: "CreatedTime",
                value: new DateTime(2023, 8, 22, 11, 59, 55, 552, DateTimeKind.Local).AddTicks(6441));

            migrationBuilder.UpdateData(
                table: "CandidateLevel",
                keyColumn: "Id",
                keyValue: 1001,
                column: "CreatedTime",
                value: new DateTime(2023, 8, 22, 11, 59, 55, 552, DateTimeKind.Local).AddTicks(6475));

            migrationBuilder.UpdateData(
                table: "EmployerServicePackage",
                keyColumn: "Id",
                keyValue: 1001,
                column: "CreatedTime",
                value: new DateTime(2023, 8, 22, 11, 59, 55, 552, DateTimeKind.Local).AddTicks(6531));

            migrationBuilder.UpdateData(
                table: "Position",
                keyColumn: "Id",
                keyValue: 1001,
                column: "CreatedTime",
                value: new DateTime(2023, 8, 22, 11, 59, 55, 552, DateTimeKind.Local).AddTicks(6501));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Photo",
                table: "CVTemplate");

            migrationBuilder.AlterColumn<string>(
                name: "Description",
                table: "CVTemplate",
                type: "nvarchar(500)",
                maxLength: 500,
                nullable: true,
                comment: "Mô tả",
                oldClrType: typeof(string),
                oldType: "nvarchar(1000)",
                oldMaxLength: 1000,
                oldNullable: true,
                oldComment: "Mô tả");

            migrationBuilder.UpdateData(
                table: "AccountStatus",
                keyColumn: "Id",
                keyValue: 1001,
                column: "CreatedTime",
                value: new DateTime(2023, 8, 21, 16, 11, 47, 82, DateTimeKind.Local).AddTicks(9913));

            migrationBuilder.UpdateData(
                table: "AccountStatus",
                keyColumn: "Id",
                keyValue: 1002,
                column: "CreatedTime",
                value: new DateTime(2023, 8, 21, 16, 11, 47, 82, DateTimeKind.Local).AddTicks(9960));

            migrationBuilder.UpdateData(
                table: "AdminAccount",
                keyColumn: "Id",
                keyValue: 1001L,
                column: "CreatedTime",
                value: new DateTime(2023, 8, 21, 16, 11, 47, 82, DateTimeKind.Local).AddTicks(9992));

            migrationBuilder.UpdateData(
                table: "CandidateLevel",
                keyColumn: "Id",
                keyValue: 1001,
                column: "CreatedTime",
                value: new DateTime(2023, 8, 21, 16, 11, 47, 83, DateTimeKind.Local).AddTicks(29));

            migrationBuilder.UpdateData(
                table: "EmployerServicePackage",
                keyColumn: "Id",
                keyValue: 1001,
                column: "CreatedTime",
                value: new DateTime(2023, 8, 21, 16, 11, 47, 83, DateTimeKind.Local).AddTicks(100));

            migrationBuilder.UpdateData(
                table: "Position",
                keyColumn: "Id",
                keyValue: 1001,
                column: "CreatedTime",
                value: new DateTime(2023, 8, 21, 16, 11, 47, 83, DateTimeKind.Local).AddTicks(64));
        }
    }
}
