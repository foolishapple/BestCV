using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Jobi.Infrastructure.Persistence.Migrations
{
    public partial class addEmployerKeyInJob_v2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<long>(
                name: "EmployerId",
                table: "Job",
                type: "bigint",
                nullable: false,
                defaultValue: 0L,
                comment: "Mã nhà tuyển dụng",
                oldClrType: typeof(long),
                oldType: "bigint",
                oldNullable: true,
                oldComment: "Mã nhà tuyển dụng");

            migrationBuilder.UpdateData(
                table: "AccountStatus",
                keyColumn: "Id",
                keyValue: 1001,
                column: "CreatedTime",
                value: new DateTime(2023, 8, 9, 18, 44, 25, 387, DateTimeKind.Local).AddTicks(4663));

            migrationBuilder.UpdateData(
                table: "AccountStatus",
                keyColumn: "Id",
                keyValue: 1002,
                column: "CreatedTime",
                value: new DateTime(2023, 8, 9, 18, 44, 25, 387, DateTimeKind.Local).AddTicks(4711));

            migrationBuilder.UpdateData(
                table: "AdminAccount",
                keyColumn: "Id",
                keyValue: 1001L,
                column: "CreatedTime",
                value: new DateTime(2023, 8, 9, 18, 44, 25, 387, DateTimeKind.Local).AddTicks(4730));

            migrationBuilder.UpdateData(
                table: "CandidateLevel",
                keyColumn: "Id",
                keyValue: 1001,
                column: "CreatedTime",
                value: new DateTime(2023, 8, 9, 18, 44, 25, 387, DateTimeKind.Local).AddTicks(4768));

            migrationBuilder.UpdateData(
                table: "EmployerServicePackage",
                keyColumn: "Id",
                keyValue: 1001,
                column: "CreatedTime",
                value: new DateTime(2023, 8, 9, 18, 44, 25, 387, DateTimeKind.Local).AddTicks(4822));

            migrationBuilder.UpdateData(
                table: "Position",
                keyColumn: "Id",
                keyValue: 1001,
                column: "CreatedTime",
                value: new DateTime(2023, 8, 9, 18, 44, 25, 387, DateTimeKind.Local).AddTicks(4792));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<long>(
                name: "EmployerId",
                table: "Job",
                type: "bigint",
                nullable: true,
                comment: "Mã nhà tuyển dụng",
                oldClrType: typeof(long),
                oldType: "bigint",
                oldComment: "Mã nhà tuyển dụng");

            migrationBuilder.UpdateData(
                table: "AccountStatus",
                keyColumn: "Id",
                keyValue: 1001,
                column: "CreatedTime",
                value: new DateTime(2023, 8, 9, 18, 41, 34, 617, DateTimeKind.Local).AddTicks(9341));

            migrationBuilder.UpdateData(
                table: "AccountStatus",
                keyColumn: "Id",
                keyValue: 1002,
                column: "CreatedTime",
                value: new DateTime(2023, 8, 9, 18, 41, 34, 617, DateTimeKind.Local).AddTicks(9385));

            migrationBuilder.UpdateData(
                table: "AdminAccount",
                keyColumn: "Id",
                keyValue: 1001L,
                column: "CreatedTime",
                value: new DateTime(2023, 8, 9, 18, 41, 34, 617, DateTimeKind.Local).AddTicks(9407));

            migrationBuilder.UpdateData(
                table: "CandidateLevel",
                keyColumn: "Id",
                keyValue: 1001,
                column: "CreatedTime",
                value: new DateTime(2023, 8, 9, 18, 41, 34, 617, DateTimeKind.Local).AddTicks(9448));

            migrationBuilder.UpdateData(
                table: "EmployerServicePackage",
                keyColumn: "Id",
                keyValue: 1001,
                column: "CreatedTime",
                value: new DateTime(2023, 8, 9, 18, 41, 34, 617, DateTimeKind.Local).AddTicks(9504));

            migrationBuilder.UpdateData(
                table: "Position",
                keyColumn: "Id",
                keyValue: 1001,
                column: "CreatedTime",
                value: new DateTime(2023, 8, 9, 18, 41, 34, 617, DateTimeKind.Local).AddTicks(9474));
        }
    }
}
