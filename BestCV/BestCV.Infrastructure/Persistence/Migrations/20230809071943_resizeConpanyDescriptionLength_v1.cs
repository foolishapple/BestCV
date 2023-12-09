using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Jobi.Infrastructure.Persistence.Migrations
{
    public partial class resizeConpanyDescriptionLength_v1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Description",
                table: "Company",
                type: "nvarchar(max)",
                maxLength: 5000,
                nullable: true,
                comment: "Mô tả",
                oldClrType: typeof(string),
                oldType: "nvarchar(500)",
                oldMaxLength: 500,
                oldNullable: true,
                oldComment: "Mô tả");

            migrationBuilder.UpdateData(
                table: "AccountStatus",
                keyColumn: "Id",
                keyValue: 1001,
                column: "CreatedTime",
                value: new DateTime(2023, 8, 9, 14, 19, 39, 355, DateTimeKind.Local).AddTicks(6543));

            migrationBuilder.UpdateData(
                table: "AccountStatus",
                keyColumn: "Id",
                keyValue: 1002,
                column: "CreatedTime",
                value: new DateTime(2023, 8, 9, 14, 19, 39, 355, DateTimeKind.Local).AddTicks(6575));

            migrationBuilder.UpdateData(
                table: "AdminAccount",
                keyColumn: "Id",
                keyValue: 1001L,
                column: "CreatedTime",
                value: new DateTime(2023, 8, 9, 14, 19, 39, 355, DateTimeKind.Local).AddTicks(6596));

            migrationBuilder.UpdateData(
                table: "CandidateLevel",
                keyColumn: "Id",
                keyValue: 1001,
                column: "CreatedTime",
                value: new DateTime(2023, 8, 9, 14, 19, 39, 355, DateTimeKind.Local).AddTicks(6629));

            migrationBuilder.UpdateData(
                table: "EmployerServicePackage",
                keyColumn: "Id",
                keyValue: 1001,
                column: "CreatedTime",
                value: new DateTime(2023, 8, 9, 14, 19, 39, 355, DateTimeKind.Local).AddTicks(6687));

            migrationBuilder.UpdateData(
                table: "Position",
                keyColumn: "Id",
                keyValue: 1001,
                column: "CreatedTime",
                value: new DateTime(2023, 8, 9, 14, 19, 39, 355, DateTimeKind.Local).AddTicks(6654));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Description",
                table: "Company",
                type: "nvarchar(500)",
                maxLength: 500,
                nullable: true,
                comment: "Mô tả",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldMaxLength: 5000,
                oldNullable: true,
                oldComment: "Mô tả");

            migrationBuilder.UpdateData(
                table: "AccountStatus",
                keyColumn: "Id",
                keyValue: 1001,
                column: "CreatedTime",
                value: new DateTime(2023, 8, 7, 18, 4, 43, 913, DateTimeKind.Local).AddTicks(8408));

            migrationBuilder.UpdateData(
                table: "AccountStatus",
                keyColumn: "Id",
                keyValue: 1002,
                column: "CreatedTime",
                value: new DateTime(2023, 8, 7, 18, 4, 43, 913, DateTimeKind.Local).AddTicks(8490));

            migrationBuilder.UpdateData(
                table: "AdminAccount",
                keyColumn: "Id",
                keyValue: 1001L,
                column: "CreatedTime",
                value: new DateTime(2023, 8, 7, 18, 4, 43, 913, DateTimeKind.Local).AddTicks(8511));

            migrationBuilder.UpdateData(
                table: "CandidateLevel",
                keyColumn: "Id",
                keyValue: 1001,
                column: "CreatedTime",
                value: new DateTime(2023, 8, 7, 18, 4, 43, 913, DateTimeKind.Local).AddTicks(8547));

            migrationBuilder.UpdateData(
                table: "EmployerServicePackage",
                keyColumn: "Id",
                keyValue: 1001,
                column: "CreatedTime",
                value: new DateTime(2023, 8, 7, 18, 4, 43, 913, DateTimeKind.Local).AddTicks(8607));

            migrationBuilder.UpdateData(
                table: "Position",
                keyColumn: "Id",
                keyValue: 1001,
                column: "CreatedTime",
                value: new DateTime(2023, 8, 7, 18, 4, 43, 913, DateTimeKind.Local).AddTicks(8573));
        }
    }
}
