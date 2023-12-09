using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BestCV.Infrastructure.Persistence.Migrations
{
    public partial class EmployerOrder_ver3 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTime>(
                name: "ApplyEndDate",
                table: "EmployerOrders",
                type: "datetime2",
                nullable: true,
                comment: "Thời hạn đơn hàng",
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldNullable: true,
                oldComment: "Thời hạn ứng tuyển");

            migrationBuilder.AddColumn<bool>(
                name: "IsApproved",
                table: "EmployerOrders",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.UpdateData(
                table: "AccountStatus",
                keyColumn: "Id",
                keyValue: 1001,
                column: "CreatedTime",
                value: new DateTime(2023, 9, 11, 11, 55, 37, 604, DateTimeKind.Local).AddTicks(1103));

            migrationBuilder.UpdateData(
                table: "AccountStatus",
                keyColumn: "Id",
                keyValue: 1002,
                column: "CreatedTime",
                value: new DateTime(2023, 9, 11, 11, 55, 37, 604, DateTimeKind.Local).AddTicks(1148));

            migrationBuilder.UpdateData(
                table: "AdminAccount",
                keyColumn: "Id",
                keyValue: 1001L,
                column: "CreatedTime",
                value: new DateTime(2023, 9, 11, 11, 55, 37, 604, DateTimeKind.Local).AddTicks(1173));

            migrationBuilder.UpdateData(
                table: "CandidateLevel",
                keyColumn: "Id",
                keyValue: 1001,
                column: "CreatedTime",
                value: new DateTime(2023, 9, 11, 11, 55, 37, 604, DateTimeKind.Local).AddTicks(1211));

            migrationBuilder.UpdateData(
                table: "Position",
                keyColumn: "Id",
                keyValue: 1001,
                column: "CreatedTime",
                value: new DateTime(2023, 9, 11, 11, 55, 37, 604, DateTimeKind.Local).AddTicks(1236));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsApproved",
                table: "EmployerOrders");

            migrationBuilder.AlterColumn<DateTime>(
                name: "ApplyEndDate",
                table: "EmployerOrders",
                type: "datetime2",
                nullable: true,
                comment: "Thời hạn ứng tuyển",
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldNullable: true,
                oldComment: "Thời hạn đơn hàng");

            migrationBuilder.UpdateData(
                table: "AccountStatus",
                keyColumn: "Id",
                keyValue: 1001,
                column: "CreatedTime",
                value: new DateTime(2023, 9, 11, 11, 40, 58, 879, DateTimeKind.Local).AddTicks(6467));

            migrationBuilder.UpdateData(
                table: "AccountStatus",
                keyColumn: "Id",
                keyValue: 1002,
                column: "CreatedTime",
                value: new DateTime(2023, 9, 11, 11, 40, 58, 879, DateTimeKind.Local).AddTicks(6516));

            migrationBuilder.UpdateData(
                table: "AdminAccount",
                keyColumn: "Id",
                keyValue: 1001L,
                column: "CreatedTime",
                value: new DateTime(2023, 9, 11, 11, 40, 58, 879, DateTimeKind.Local).AddTicks(6553));

            migrationBuilder.UpdateData(
                table: "CandidateLevel",
                keyColumn: "Id",
                keyValue: 1001,
                column: "CreatedTime",
                value: new DateTime(2023, 9, 11, 11, 40, 58, 879, DateTimeKind.Local).AddTicks(6591));

            migrationBuilder.UpdateData(
                table: "Position",
                keyColumn: "Id",
                keyValue: 1001,
                column: "CreatedTime",
                value: new DateTime(2023, 9, 11, 11, 40, 58, 879, DateTimeKind.Local).AddTicks(6619));
        }
    }
}
