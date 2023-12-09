using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Jobi.Infrastructure.Persistence.Migrations
{
    public partial class FixViewCount_V1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "Value",
                table: "ServicePackageBenefit",
                type: "int",
                nullable: true,
                comment: "Giá trị",
                oldClrType: typeof(int),
                oldType: "int",
                oldComment: "Giá trị");

            migrationBuilder.AddColumn<int>(
                name: "ViewCount",
                table: "Job",
                type: "int",
                nullable: false,
                defaultValue: 0,
                comment: "Tổng lượt xem");

            migrationBuilder.UpdateData(
                table: "AccountStatus",
                keyColumn: "Id",
                keyValue: 1001,
                column: "CreatedTime",
                value: new DateTime(2023, 9, 11, 16, 38, 31, 897, DateTimeKind.Local).AddTicks(2336));

            migrationBuilder.UpdateData(
                table: "AccountStatus",
                keyColumn: "Id",
                keyValue: 1002,
                column: "CreatedTime",
                value: new DateTime(2023, 9, 11, 16, 38, 31, 897, DateTimeKind.Local).AddTicks(2396));

            migrationBuilder.UpdateData(
                table: "AdminAccount",
                keyColumn: "Id",
                keyValue: 1001L,
                column: "CreatedTime",
                value: new DateTime(2023, 9, 11, 16, 38, 31, 897, DateTimeKind.Local).AddTicks(2419));

            migrationBuilder.UpdateData(
                table: "CandidateLevel",
                keyColumn: "Id",
                keyValue: 1001,
                column: "CreatedTime",
                value: new DateTime(2023, 9, 11, 16, 38, 31, 897, DateTimeKind.Local).AddTicks(2452));

            migrationBuilder.UpdateData(
                table: "Position",
                keyColumn: "Id",
                keyValue: 1001,
                column: "CreatedTime",
                value: new DateTime(2023, 9, 11, 16, 38, 31, 897, DateTimeKind.Local).AddTicks(2478));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ViewCount",
                table: "Job");

            migrationBuilder.AlterColumn<int>(
                name: "Value",
                table: "ServicePackageBenefit",
                type: "int",
                nullable: false,
                defaultValue: 0,
                comment: "Giá trị",
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true,
                oldComment: "Giá trị");

            migrationBuilder.UpdateData(
                table: "AccountStatus",
                keyColumn: "Id",
                keyValue: 1001,
                column: "CreatedTime",
                value: new DateTime(2023, 9, 11, 11, 58, 18, 82, DateTimeKind.Local).AddTicks(5693));

            migrationBuilder.UpdateData(
                table: "AccountStatus",
                keyColumn: "Id",
                keyValue: 1002,
                column: "CreatedTime",
                value: new DateTime(2023, 9, 11, 11, 58, 18, 82, DateTimeKind.Local).AddTicks(5753));

            migrationBuilder.UpdateData(
                table: "AdminAccount",
                keyColumn: "Id",
                keyValue: 1001L,
                column: "CreatedTime",
                value: new DateTime(2023, 9, 11, 11, 58, 18, 82, DateTimeKind.Local).AddTicks(5787));

            migrationBuilder.UpdateData(
                table: "CandidateLevel",
                keyColumn: "Id",
                keyValue: 1001,
                column: "CreatedTime",
                value: new DateTime(2023, 9, 11, 11, 58, 18, 82, DateTimeKind.Local).AddTicks(5835));

            migrationBuilder.UpdateData(
                table: "Position",
                keyColumn: "Id",
                keyValue: 1001,
                column: "CreatedTime",
                value: new DateTime(2023, 9, 11, 11, 58, 18, 82, DateTimeKind.Local).AddTicks(5871));
        }
    }
}
