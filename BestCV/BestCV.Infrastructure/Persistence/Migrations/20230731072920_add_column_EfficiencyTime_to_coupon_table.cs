using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Jobi.Infrastructure.Persistence.Migrations
{
    public partial class add_column_EfficiencyTime_to_coupon_table : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "EfficiencyTime",
                table: "Coupon",
                type: "int",
                nullable: false,
                defaultValue: 0,
                comment: "Hiệu lực của mã quà tặng (theo tháng)");

            migrationBuilder.UpdateData(
                table: "AccountStatus",
                keyColumn: "Id",
                keyValue: 1001,
                column: "CreatedTime",
                value: new DateTime(2023, 7, 31, 14, 29, 19, 326, DateTimeKind.Local).AddTicks(1138));

            migrationBuilder.UpdateData(
                table: "AccountStatus",
                keyColumn: "Id",
                keyValue: 1002,
                column: "CreatedTime",
                value: new DateTime(2023, 7, 31, 14, 29, 19, 326, DateTimeKind.Local).AddTicks(1172));

            migrationBuilder.UpdateData(
                table: "AdminAccount",
                keyColumn: "Id",
                keyValue: 1001L,
                column: "CreatedTime",
                value: new DateTime(2023, 7, 31, 14, 29, 19, 326, DateTimeKind.Local).AddTicks(1189));

            migrationBuilder.UpdateData(
                table: "CandidateLevel",
                keyColumn: "Id",
                keyValue: 1001,
                column: "CreatedTime",
                value: new DateTime(2023, 7, 31, 14, 29, 19, 326, DateTimeKind.Local).AddTicks(1271));

            migrationBuilder.UpdateData(
                table: "EmployerServicePackage",
                keyColumn: "Id",
                keyValue: 1001,
                column: "CreatedTime",
                value: new DateTime(2023, 7, 31, 14, 29, 19, 326, DateTimeKind.Local).AddTicks(1329));

            migrationBuilder.UpdateData(
                table: "Position",
                keyColumn: "Id",
                keyValue: 1001,
                column: "CreatedTime",
                value: new DateTime(2023, 7, 31, 14, 29, 19, 326, DateTimeKind.Local).AddTicks(1300));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "EfficiencyTime",
                table: "Coupon");

            migrationBuilder.UpdateData(
                table: "AccountStatus",
                keyColumn: "Id",
                keyValue: 1001,
                column: "CreatedTime",
                value: new DateTime(2023, 7, 27, 11, 52, 44, 117, DateTimeKind.Local).AddTicks(2327));

            migrationBuilder.UpdateData(
                table: "AccountStatus",
                keyColumn: "Id",
                keyValue: 1002,
                column: "CreatedTime",
                value: new DateTime(2023, 7, 27, 11, 52, 44, 117, DateTimeKind.Local).AddTicks(2410));

            migrationBuilder.UpdateData(
                table: "AdminAccount",
                keyColumn: "Id",
                keyValue: 1001L,
                column: "CreatedTime",
                value: new DateTime(2023, 7, 27, 11, 52, 44, 117, DateTimeKind.Local).AddTicks(2466));

            migrationBuilder.UpdateData(
                table: "CandidateLevel",
                keyColumn: "Id",
                keyValue: 1001,
                column: "CreatedTime",
                value: new DateTime(2023, 7, 27, 11, 52, 44, 117, DateTimeKind.Local).AddTicks(2564));

            migrationBuilder.UpdateData(
                table: "EmployerServicePackage",
                keyColumn: "Id",
                keyValue: 1001,
                column: "CreatedTime",
                value: new DateTime(2023, 7, 27, 11, 52, 44, 117, DateTimeKind.Local).AddTicks(2715));

            migrationBuilder.UpdateData(
                table: "Position",
                keyColumn: "Id",
                keyValue: 1001,
                column: "CreatedTime",
                value: new DateTime(2023, 7, 27, 11, 52, 44, 117, DateTimeKind.Local).AddTicks(2642));
        }
    }
}
