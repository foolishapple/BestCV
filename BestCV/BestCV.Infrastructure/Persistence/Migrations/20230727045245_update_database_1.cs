using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Jobi.Infrastructure.Persistence.Migrations
{
    public partial class update_database_1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Mã loại giấy tờ",
                table: "LicenseType",
                newName: "Id");

            migrationBuilder.RenameColumn(
                name: "Mã giấy tờ",
                table: "License",
                newName: "Id");

            migrationBuilder.RenameColumn(
                name: "Mã quyền lợi gói dịch vụ cho nhà tuyển dụng",
                table: "EmployerServicePackageEmployerBenefit",
                newName: "Id");

            migrationBuilder.RenameColumn(
                name: "Mã gói dịch vụ cho nhà tuyển dụng",
                table: "EmployerServicePackage",
                newName: "Id");

            migrationBuilder.RenameColumn(
                name: "Mã lợi ích của nhà tuyển dụng",
                table: "EmployerBenefit",
                newName: "Id");

            migrationBuilder.RenameColumn(
                name: "Mã đánh giá công ty",
                table: "CompanyReview",
                newName: "Id");

            migrationBuilder.AlterColumn<int>(
                name: "Id",
                table: "LicenseType",
                type: "int",
                nullable: false,
                comment: "Mã loại giấy tờ",
                oldClrType: typeof(int),
                oldType: "int")
                .Annotation("SqlServer:Identity", "1001, 1")
                .OldAnnotation("SqlServer:Identity", "1001, 1");

            migrationBuilder.AlterColumn<long>(
                name: "Id",
                table: "License",
                type: "bigint",
                nullable: false,
                comment: "Mã giấy tờ",
                oldClrType: typeof(long),
                oldType: "bigint")
                .Annotation("SqlServer:Identity", "1001, 1")
                .OldAnnotation("SqlServer:Identity", "1001, 1");

            migrationBuilder.AlterColumn<int>(
                name: "Id",
                table: "EmployerServicePackageEmployerBenefit",
                type: "int",
                nullable: false,
                comment: "Mã quyền lợi gói dịch vụ cho nhà tuyển dụng",
                oldClrType: typeof(int),
                oldType: "int")
                .Annotation("SqlServer:Identity", "1001, 1")
                .OldAnnotation("SqlServer:Identity", "1001, 1");

            migrationBuilder.AlterColumn<int>(
                name: "Id",
                table: "EmployerServicePackage",
                type: "int",
                nullable: false,
                comment: "Mã gói dịch vụ cho nhà tuyển dụng",
                oldClrType: typeof(int),
                oldType: "int")
                .Annotation("SqlServer:Identity", "1001, 1")
                .OldAnnotation("SqlServer:Identity", "1001, 1");

            migrationBuilder.AlterColumn<int>(
                name: "Id",
                table: "EmployerBenefit",
                type: "int",
                nullable: false,
                comment: "Mã lợi ích của nhà tuyển dụng",
                oldClrType: typeof(int),
                oldType: "int")
                .Annotation("SqlServer:Identity", "1001, 1")
                .OldAnnotation("SqlServer:Identity", "1001, 1");

            migrationBuilder.AlterColumn<long>(
                name: "Id",
                table: "CompanyReview",
                type: "bigint",
                nullable: false,
                comment: "Mã đánh giá công ty",
                oldClrType: typeof(long),
                oldType: "bigint")
                .Annotation("SqlServer:Identity", "1001, 1")
                .OldAnnotation("SqlServer:Identity", "1001, 1");

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

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Id",
                table: "LicenseType",
                newName: "Mã loại giấy tờ");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "License",
                newName: "Mã giấy tờ");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "EmployerServicePackageEmployerBenefit",
                newName: "Mã quyền lợi gói dịch vụ cho nhà tuyển dụng");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "EmployerServicePackage",
                newName: "Mã gói dịch vụ cho nhà tuyển dụng");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "EmployerBenefit",
                newName: "Mã lợi ích của nhà tuyển dụng");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "CompanyReview",
                newName: "Mã đánh giá công ty");

            migrationBuilder.AlterColumn<int>(
                name: "Mã loại giấy tờ",
                table: "LicenseType",
                type: "int",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int",
                oldComment: "Mã loại giấy tờ")
                .Annotation("SqlServer:Identity", "1001, 1")
                .OldAnnotation("SqlServer:Identity", "1001, 1");

            migrationBuilder.AlterColumn<long>(
                name: "Mã giấy tờ",
                table: "License",
                type: "bigint",
                nullable: false,
                oldClrType: typeof(long),
                oldType: "bigint",
                oldComment: "Mã giấy tờ")
                .Annotation("SqlServer:Identity", "1001, 1")
                .OldAnnotation("SqlServer:Identity", "1001, 1");

            migrationBuilder.AlterColumn<int>(
                name: "Mã quyền lợi gói dịch vụ cho nhà tuyển dụng",
                table: "EmployerServicePackageEmployerBenefit",
                type: "int",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int",
                oldComment: "Mã quyền lợi gói dịch vụ cho nhà tuyển dụng")
                .Annotation("SqlServer:Identity", "1001, 1")
                .OldAnnotation("SqlServer:Identity", "1001, 1");

            migrationBuilder.AlterColumn<int>(
                name: "Mã gói dịch vụ cho nhà tuyển dụng",
                table: "EmployerServicePackage",
                type: "int",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int",
                oldComment: "Mã gói dịch vụ cho nhà tuyển dụng")
                .Annotation("SqlServer:Identity", "1001, 1")
                .OldAnnotation("SqlServer:Identity", "1001, 1");

            migrationBuilder.AlterColumn<int>(
                name: "Mã lợi ích của nhà tuyển dụng",
                table: "EmployerBenefit",
                type: "int",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int",
                oldComment: "Mã lợi ích của nhà tuyển dụng")
                .Annotation("SqlServer:Identity", "1001, 1")
                .OldAnnotation("SqlServer:Identity", "1001, 1");

            migrationBuilder.AlterColumn<long>(
                name: "Mã đánh giá công ty",
                table: "CompanyReview",
                type: "bigint",
                nullable: false,
                oldClrType: typeof(long),
                oldType: "bigint",
                oldComment: "Mã đánh giá công ty")
                .Annotation("SqlServer:Identity", "1001, 1")
                .OldAnnotation("SqlServer:Identity", "1001, 1");

            migrationBuilder.UpdateData(
                table: "AccountStatus",
                keyColumn: "Id",
                keyValue: 1001,
                column: "CreatedTime",
                value: new DateTime(2023, 7, 27, 11, 39, 45, 794, DateTimeKind.Local).AddTicks(99));

            migrationBuilder.UpdateData(
                table: "AccountStatus",
                keyColumn: "Id",
                keyValue: 1002,
                column: "CreatedTime",
                value: new DateTime(2023, 7, 27, 11, 39, 45, 794, DateTimeKind.Local).AddTicks(144));

            migrationBuilder.UpdateData(
                table: "AdminAccount",
                keyColumn: "Id",
                keyValue: 1001L,
                column: "CreatedTime",
                value: new DateTime(2023, 7, 27, 11, 39, 45, 794, DateTimeKind.Local).AddTicks(165));

            migrationBuilder.UpdateData(
                table: "CandidateLevel",
                keyColumn: "Id",
                keyValue: 1001,
                column: "CreatedTime",
                value: new DateTime(2023, 7, 27, 11, 39, 45, 794, DateTimeKind.Local).AddTicks(201));

            migrationBuilder.UpdateData(
                table: "EmployerServicePackage",
                keyColumn: "Mã gói dịch vụ cho nhà tuyển dụng",
                keyValue: 1001,
                column: "CreatedTime",
                value: new DateTime(2023, 7, 27, 11, 39, 45, 794, DateTimeKind.Local).AddTicks(269));

            migrationBuilder.UpdateData(
                table: "Position",
                keyColumn: "Id",
                keyValue: 1001,
                column: "CreatedTime",
                value: new DateTime(2023, 7, 27, 11, 39, 45, 794, DateTimeKind.Local).AddTicks(236));
        }
    }
}
