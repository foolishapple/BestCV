using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Jobi.Infrastructure.Persistence.Migrations
{
    public partial class addPackageServiceModule_v2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            //migrationBuilder.DropForeignKey(
            //    name: "FK_Menu_MenuType_MenuTypeId",
            //    table: "Menu");

            migrationBuilder.UpdateData(
                table: "AccountStatus",
                keyColumn: "Id",
                keyValue: 1001,
                column: "CreatedTime",
                value: new DateTime(2023, 9, 7, 16, 19, 57, 21, DateTimeKind.Local).AddTicks(8286));

            migrationBuilder.UpdateData(
                table: "AccountStatus",
                keyColumn: "Id",
                keyValue: 1002,
                column: "CreatedTime",
                value: new DateTime(2023, 9, 7, 16, 19, 57, 21, DateTimeKind.Local).AddTicks(8352));

            migrationBuilder.UpdateData(
                table: "AdminAccount",
                keyColumn: "Id",
                keyValue: 1001L,
                column: "CreatedTime",
                value: new DateTime(2023, 9, 7, 16, 19, 57, 21, DateTimeKind.Local).AddTicks(8387));

            migrationBuilder.UpdateData(
                table: "CandidateLevel",
                keyColumn: "Id",
                keyValue: 1001,
                column: "CreatedTime",
                value: new DateTime(2023, 9, 7, 16, 19, 57, 21, DateTimeKind.Local).AddTicks(8428));

            migrationBuilder.UpdateData(
                table: "EmployerServicePackage",
                keyColumn: "Id",
                keyValue: 1001,
                column: "CreatedTime",
                value: new DateTime(2023, 9, 7, 16, 19, 57, 21, DateTimeKind.Local).AddTicks(8491));

            migrationBuilder.UpdateData(
                table: "Position",
                keyColumn: "Id",
                keyValue: 1001,
                column: "CreatedTime",
                value: new DateTime(2023, 9, 7, 16, 19, 57, 21, DateTimeKind.Local).AddTicks(8463));

            //migrationBuilder.AddForeignKey(
            //    name: "FK_Menu_MenuType",
            //    table: "Menu",
            //    column: "MenuTypeId",
            //    principalTable: "MenuType",
            //    principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Menu_MenuType",
                table: "Menu");

            migrationBuilder.UpdateData(
                table: "AccountStatus",
                keyColumn: "Id",
                keyValue: 1001,
                column: "CreatedTime",
                value: new DateTime(2023, 9, 7, 16, 12, 20, 352, DateTimeKind.Local).AddTicks(6171));

            migrationBuilder.UpdateData(
                table: "AccountStatus",
                keyColumn: "Id",
                keyValue: 1002,
                column: "CreatedTime",
                value: new DateTime(2023, 9, 7, 16, 12, 20, 352, DateTimeKind.Local).AddTicks(6226));

            migrationBuilder.UpdateData(
                table: "AdminAccount",
                keyColumn: "Id",
                keyValue: 1001L,
                column: "CreatedTime",
                value: new DateTime(2023, 9, 7, 16, 12, 20, 352, DateTimeKind.Local).AddTicks(6261));

            migrationBuilder.UpdateData(
                table: "CandidateLevel",
                keyColumn: "Id",
                keyValue: 1001,
                column: "CreatedTime",
                value: new DateTime(2023, 9, 7, 16, 12, 20, 352, DateTimeKind.Local).AddTicks(6301));

            migrationBuilder.UpdateData(
                table: "EmployerServicePackage",
                keyColumn: "Id",
                keyValue: 1001,
                column: "CreatedTime",
                value: new DateTime(2023, 9, 7, 16, 12, 20, 352, DateTimeKind.Local).AddTicks(6364));

            migrationBuilder.UpdateData(
                table: "Position",
                keyColumn: "Id",
                keyValue: 1001,
                column: "CreatedTime",
                value: new DateTime(2023, 9, 7, 16, 12, 20, 352, DateTimeKind.Local).AddTicks(6337));

            migrationBuilder.AddForeignKey(
                name: "FK_Menu_MenuType_MenuTypeId",
                table: "Menu",
                column: "MenuTypeId",
                principalTable: "MenuType",
                principalColumn: "Id");
        }
    }
}
