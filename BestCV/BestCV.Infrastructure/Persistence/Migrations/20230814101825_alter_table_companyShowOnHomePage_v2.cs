using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BestCV.Infrastructure.Persistence.Migrations
{
    public partial class alter_table_companyShowOnHomePage_v2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CompanyShowOnHomePage_Company",
                table: "TopCompany");

            migrationBuilder.UpdateData(
                table: "AccountStatus",
                keyColumn: "Id",
                keyValue: 1001,
                column: "CreatedTime",
                value: new DateTime(2023, 8, 14, 17, 18, 24, 586, DateTimeKind.Local).AddTicks(6373));

            migrationBuilder.UpdateData(
                table: "AccountStatus",
                keyColumn: "Id",
                keyValue: 1002,
                column: "CreatedTime",
                value: new DateTime(2023, 8, 14, 17, 18, 24, 586, DateTimeKind.Local).AddTicks(6498));

            migrationBuilder.UpdateData(
                table: "AdminAccount",
                keyColumn: "Id",
                keyValue: 1001L,
                column: "CreatedTime",
                value: new DateTime(2023, 8, 14, 17, 18, 24, 586, DateTimeKind.Local).AddTicks(6614));

            migrationBuilder.UpdateData(
                table: "CandidateLevel",
                keyColumn: "Id",
                keyValue: 1001,
                column: "CreatedTime",
                value: new DateTime(2023, 8, 14, 17, 18, 24, 586, DateTimeKind.Local).AddTicks(6707));

            migrationBuilder.UpdateData(
                table: "EmployerServicePackage",
                keyColumn: "Id",
                keyValue: 1001,
                column: "CreatedTime",
                value: new DateTime(2023, 8, 14, 17, 18, 24, 586, DateTimeKind.Local).AddTicks(6850));

            migrationBuilder.UpdateData(
                table: "Position",
                keyColumn: "Id",
                keyValue: 1001,
                column: "CreatedTime",
                value: new DateTime(2023, 8, 14, 17, 18, 24, 586, DateTimeKind.Local).AddTicks(6780));

            migrationBuilder.AddForeignKey(
                name: "FK_TopCompany_Company",
                table: "TopCompany",
                column: "CompanyId",
                principalTable: "Company",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TopCompany_Company",
                table: "TopCompany");

            migrationBuilder.UpdateData(
                table: "AccountStatus",
                keyColumn: "Id",
                keyValue: 1001,
                column: "CreatedTime",
                value: new DateTime(2023, 8, 14, 17, 10, 2, 569, DateTimeKind.Local).AddTicks(6479));

            migrationBuilder.UpdateData(
                table: "AccountStatus",
                keyColumn: "Id",
                keyValue: 1002,
                column: "CreatedTime",
                value: new DateTime(2023, 8, 14, 17, 10, 2, 569, DateTimeKind.Local).AddTicks(6526));

            migrationBuilder.UpdateData(
                table: "AdminAccount",
                keyColumn: "Id",
                keyValue: 1001L,
                column: "CreatedTime",
                value: new DateTime(2023, 8, 14, 17, 10, 2, 569, DateTimeKind.Local).AddTicks(6552));

            migrationBuilder.UpdateData(
                table: "CandidateLevel",
                keyColumn: "Id",
                keyValue: 1001,
                column: "CreatedTime",
                value: new DateTime(2023, 8, 14, 17, 10, 2, 569, DateTimeKind.Local).AddTicks(6587));

            migrationBuilder.UpdateData(
                table: "EmployerServicePackage",
                keyColumn: "Id",
                keyValue: 1001,
                column: "CreatedTime",
                value: new DateTime(2023, 8, 14, 17, 10, 2, 569, DateTimeKind.Local).AddTicks(6646));

            migrationBuilder.UpdateData(
                table: "Position",
                keyColumn: "Id",
                keyValue: 1001,
                column: "CreatedTime",
                value: new DateTime(2023, 8, 14, 17, 10, 2, 569, DateTimeKind.Local).AddTicks(6617));

            migrationBuilder.AddForeignKey(
                name: "FK_CompanyShowOnHomePage_Company",
                table: "TopCompany",
                column: "CompanyId",
                principalTable: "Company",
                principalColumn: "Id");
        }
    }
}
