using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BestCV.Infrastructure.Persistence.Migrations
{
    public partial class EmployerOrder_ver1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "ApplyEndDate",
                table: "EmployerOrders",
                type: "datetime2",
                nullable: true,
                comment: "Thời hạn ứng tuyển");

            migrationBuilder.AddColumn<DateTime>(
                name: "ApprovalDate",
                table: "EmployerOrders",
                type: "datetime2",
                nullable: true,
                comment: "Thời diểm duyệt tin");

            migrationBuilder.UpdateData(
                table: "AccountStatus",
                keyColumn: "Id",
                keyValue: 1001,
                column: "CreatedTime",
                value: new DateTime(2023, 9, 11, 11, 31, 36, 460, DateTimeKind.Local).AddTicks(4833));

            migrationBuilder.UpdateData(
                table: "AccountStatus",
                keyColumn: "Id",
                keyValue: 1002,
                column: "CreatedTime",
                value: new DateTime(2023, 9, 11, 11, 31, 36, 460, DateTimeKind.Local).AddTicks(4876));

            migrationBuilder.UpdateData(
                table: "AdminAccount",
                keyColumn: "Id",
                keyValue: 1001L,
                column: "CreatedTime",
                value: new DateTime(2023, 9, 11, 11, 31, 36, 460, DateTimeKind.Local).AddTicks(4900));

            migrationBuilder.UpdateData(
                table: "CandidateLevel",
                keyColumn: "Id",
                keyValue: 1001,
                column: "CreatedTime",
                value: new DateTime(2023, 9, 11, 11, 31, 36, 460, DateTimeKind.Local).AddTicks(4936));

            migrationBuilder.UpdateData(
                table: "Position",
                keyColumn: "Id",
                keyValue: 1001,
                column: "CreatedTime",
                value: new DateTime(2023, 9, 11, 11, 31, 36, 460, DateTimeKind.Local).AddTicks(4964));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ApplyEndDate",
                table: "EmployerOrders");

            migrationBuilder.DropColumn(
                name: "ApprovalDate",
                table: "EmployerOrders");

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
    }
}
