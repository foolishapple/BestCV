using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Jobi.Infrastructure.Persistence.Migrations
{
    public partial class add_workplaceId_to_company_02 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Company_WorkPlace_WorkPlaceId",
                table: "Company");

            migrationBuilder.AlterColumn<int>(
                name: "WorkPlaceId",
                table: "Company",
                type: "int",
                nullable: false,
                defaultValue: 0,
                comment: "Mã nơi làm việc",
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true,
                oldComment: "Mã nơi làm việc");

            migrationBuilder.UpdateData(
                table: "AccountStatus",
                keyColumn: "Id",
                keyValue: 1001,
                column: "CreatedTime",
                value: new DateTime(2023, 8, 14, 23, 37, 39, 672, DateTimeKind.Local).AddTicks(8129));

            migrationBuilder.UpdateData(
                table: "AccountStatus",
                keyColumn: "Id",
                keyValue: 1002,
                column: "CreatedTime",
                value: new DateTime(2023, 8, 14, 23, 37, 39, 672, DateTimeKind.Local).AddTicks(8162));

            migrationBuilder.UpdateData(
                table: "AdminAccount",
                keyColumn: "Id",
                keyValue: 1001L,
                column: "CreatedTime",
                value: new DateTime(2023, 8, 14, 23, 37, 39, 672, DateTimeKind.Local).AddTicks(8193));

            migrationBuilder.UpdateData(
                table: "CandidateLevel",
                keyColumn: "Id",
                keyValue: 1001,
                column: "CreatedTime",
                value: new DateTime(2023, 8, 14, 23, 37, 39, 672, DateTimeKind.Local).AddTicks(8237));

            migrationBuilder.UpdateData(
                table: "EmployerServicePackage",
                keyColumn: "Id",
                keyValue: 1001,
                column: "CreatedTime",
                value: new DateTime(2023, 8, 14, 23, 37, 39, 672, DateTimeKind.Local).AddTicks(8288));

            migrationBuilder.UpdateData(
                table: "Position",
                keyColumn: "Id",
                keyValue: 1001,
                column: "CreatedTime",
                value: new DateTime(2023, 8, 14, 23, 37, 39, 672, DateTimeKind.Local).AddTicks(8261));

            migrationBuilder.AddForeignKey(
                name: "FK_Company_WorkPlace",
                table: "Company",
                column: "WorkPlaceId",
                principalTable: "WorkPlace",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Company_WorkPlace",
                table: "Company");

            migrationBuilder.AlterColumn<int>(
                name: "WorkPlaceId",
                table: "Company",
                type: "int",
                nullable: true,
                comment: "Mã nơi làm việc",
                oldClrType: typeof(int),
                oldType: "int",
                oldComment: "Mã nơi làm việc");

            migrationBuilder.UpdateData(
                table: "AccountStatus",
                keyColumn: "Id",
                keyValue: 1001,
                column: "CreatedTime",
                value: new DateTime(2023, 8, 14, 23, 35, 29, 178, DateTimeKind.Local).AddTicks(8642));

            migrationBuilder.UpdateData(
                table: "AccountStatus",
                keyColumn: "Id",
                keyValue: 1002,
                column: "CreatedTime",
                value: new DateTime(2023, 8, 14, 23, 35, 29, 178, DateTimeKind.Local).AddTicks(8678));

            migrationBuilder.UpdateData(
                table: "AdminAccount",
                keyColumn: "Id",
                keyValue: 1001L,
                column: "CreatedTime",
                value: new DateTime(2023, 8, 14, 23, 35, 29, 178, DateTimeKind.Local).AddTicks(8701));

            migrationBuilder.UpdateData(
                table: "CandidateLevel",
                keyColumn: "Id",
                keyValue: 1001,
                column: "CreatedTime",
                value: new DateTime(2023, 8, 14, 23, 35, 29, 178, DateTimeKind.Local).AddTicks(8742));

            migrationBuilder.UpdateData(
                table: "EmployerServicePackage",
                keyColumn: "Id",
                keyValue: 1001,
                column: "CreatedTime",
                value: new DateTime(2023, 8, 14, 23, 35, 29, 178, DateTimeKind.Local).AddTicks(8800));

            migrationBuilder.UpdateData(
                table: "Position",
                keyColumn: "Id",
                keyValue: 1001,
                column: "CreatedTime",
                value: new DateTime(2023, 8, 14, 23, 35, 29, 178, DateTimeKind.Local).AddTicks(8762));

            migrationBuilder.AddForeignKey(
                name: "FK_Company_WorkPlace_WorkPlaceId",
                table: "Company",
                column: "WorkPlaceId",
                principalTable: "WorkPlace",
                principalColumn: "Id");
        }
    }
}
