using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BestCV.Infrastructure.Persistence.Migrations
{
    public partial class addPackageServiceModule_v5 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_EmployerServicePackage_ServicePackageGroup_ServicePackageGroupId",
                table: "EmployerServicePackage");

            migrationBuilder.DropForeignKey(
                name: "FK_EmployerServicePackage_ServicePackageType_ServicePackageTypeId",
                table: "EmployerServicePackage");

            migrationBuilder.DropIndex(
                name: "IX_EmployerServicePackage_ServicePackageTypeId",
                table: "EmployerServicePackage");

            migrationBuilder.AlterColumn<int>(
                name: "ServicePackageTypeId",
                table: "EmployerServicePackage",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "ServicePackageGroupId",
                table: "EmployerServicePackage",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.UpdateData(
                table: "AccountStatus",
                keyColumn: "Id",
                keyValue: 1001,
                column: "CreatedTime",
                value: new DateTime(2023, 9, 7, 16, 40, 11, 538, DateTimeKind.Local).AddTicks(9001));

            migrationBuilder.UpdateData(
                table: "AccountStatus",
                keyColumn: "Id",
                keyValue: 1002,
                column: "CreatedTime",
                value: new DateTime(2023, 9, 7, 16, 40, 11, 538, DateTimeKind.Local).AddTicks(9082));

            migrationBuilder.UpdateData(
                table: "AdminAccount",
                keyColumn: "Id",
                keyValue: 1001L,
                column: "CreatedTime",
                value: new DateTime(2023, 9, 7, 16, 40, 11, 538, DateTimeKind.Local).AddTicks(9109));

            migrationBuilder.UpdateData(
                table: "CandidateLevel",
                keyColumn: "Id",
                keyValue: 1001,
                column: "CreatedTime",
                value: new DateTime(2023, 9, 7, 16, 40, 11, 538, DateTimeKind.Local).AddTicks(9150));

            migrationBuilder.UpdateData(
                table: "Position",
                keyColumn: "Id",
                keyValue: 1001,
                column: "CreatedTime",
                value: new DateTime(2023, 9, 7, 16, 40, 11, 538, DateTimeKind.Local).AddTicks(9184));

            migrationBuilder.AddForeignKey(
                name: "FK_EmployerServicePackage_ServicePackageGroup",
                table: "EmployerServicePackage",
                column: "ServicePackageGroupId",
                principalTable: "ServicePackageGroup",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_EmployerServicePackage_ServicePackageType",
                table: "EmployerServicePackage",
                column: "ServicePackageGroupId",
                principalTable: "ServicePackageType",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_EmployerServicePackage_ServicePackageGroup",
                table: "EmployerServicePackage");

            migrationBuilder.DropForeignKey(
                name: "FK_EmployerServicePackage_ServicePackageType",
                table: "EmployerServicePackage");

            migrationBuilder.AlterColumn<int>(
                name: "ServicePackageTypeId",
                table: "EmployerServicePackage",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<int>(
                name: "ServicePackageGroupId",
                table: "EmployerServicePackage",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.UpdateData(
                table: "AccountStatus",
                keyColumn: "Id",
                keyValue: 1001,
                column: "CreatedTime",
                value: new DateTime(2023, 9, 7, 16, 36, 2, 755, DateTimeKind.Local).AddTicks(7047));

            migrationBuilder.UpdateData(
                table: "AccountStatus",
                keyColumn: "Id",
                keyValue: 1002,
                column: "CreatedTime",
                value: new DateTime(2023, 9, 7, 16, 36, 2, 755, DateTimeKind.Local).AddTicks(7142));

            migrationBuilder.UpdateData(
                table: "AdminAccount",
                keyColumn: "Id",
                keyValue: 1001L,
                column: "CreatedTime",
                value: new DateTime(2023, 9, 7, 16, 36, 2, 755, DateTimeKind.Local).AddTicks(7172));

            migrationBuilder.UpdateData(
                table: "CandidateLevel",
                keyColumn: "Id",
                keyValue: 1001,
                column: "CreatedTime",
                value: new DateTime(2023, 9, 7, 16, 36, 2, 755, DateTimeKind.Local).AddTicks(7221));

            migrationBuilder.UpdateData(
                table: "Position",
                keyColumn: "Id",
                keyValue: 1001,
                column: "CreatedTime",
                value: new DateTime(2023, 9, 7, 16, 36, 2, 755, DateTimeKind.Local).AddTicks(7261));

            migrationBuilder.CreateIndex(
                name: "IX_EmployerServicePackage_ServicePackageTypeId",
                table: "EmployerServicePackage",
                column: "ServicePackageTypeId");

            migrationBuilder.AddForeignKey(
                name: "FK_EmployerServicePackage_ServicePackageGroup_ServicePackageGroupId",
                table: "EmployerServicePackage",
                column: "ServicePackageGroupId",
                principalTable: "ServicePackageGroup",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_EmployerServicePackage_ServicePackageType_ServicePackageTypeId",
                table: "EmployerServicePackage",
                column: "ServicePackageTypeId",
                principalTable: "ServicePackageType",
                principalColumn: "Id");
        }
    }
}
