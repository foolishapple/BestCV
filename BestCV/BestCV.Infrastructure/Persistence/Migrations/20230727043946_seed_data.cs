using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Jobi.Infrastructure.Persistence.Migrations
{
    public partial class seed_data : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Mã chức vụ",
                table: "Position",
                newName: "Id");

            migrationBuilder.AlterColumn<int>(
                name: "Id",
                table: "Position",
                type: "int",
                nullable: false,
                comment: "Mã chức vụ",
                oldClrType: typeof(int),
                oldType: "int")
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

            migrationBuilder.InsertData(
                table: "CandidateLevel",
                columns: new[] { "Id", "Active", "CreatedTime", "Description", "DiscountEndDate", "DiscountPrice", "ExpiryTime", "Name", "Price" },
                values: new object[] { 1001, true, new DateTime(2023, 7, 27, 11, 39, 45, 794, DateTimeKind.Local).AddTicks(201), null, null, 0, 0, "Thường", 0 });

            migrationBuilder.InsertData(
                table: "EmployerServicePackage",
                columns: new[] { "Mã gói dịch vụ cho nhà tuyển dụng", "Active", "CreatedTime", "Description", "DiscountPrice", "ExpiryTime", "Name", "Price" },
                values: new object[] { 1001, true, new DateTime(2023, 7, 27, 11, 39, 45, 794, DateTimeKind.Local).AddTicks(269), null, 0, 0, "Thường", 0 });

            migrationBuilder.InsertData(
                table: "Position",
                columns: new[] { "Id", "Active", "CreatedTime", "Description", "Name" },
                values: new object[] { 1001, true, new DateTime(2023, 7, 27, 11, 39, 45, 794, DateTimeKind.Local).AddTicks(236), null, "Nhân viên" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "CandidateLevel",
                keyColumn: "Id",
                keyValue: 1001);

            migrationBuilder.DeleteData(
                table: "EmployerServicePackage",
                keyColumn: "Mã gói dịch vụ cho nhà tuyển dụng",
                keyValue: 1001);

            migrationBuilder.DeleteData(
                table: "Position",
                keyColumn: "Id",
                keyValue: 1001);

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "Position",
                newName: "Mã chức vụ");

            migrationBuilder.AlterColumn<int>(
                name: "Mã chức vụ",
                table: "Position",
                type: "int",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int",
                oldComment: "Mã chức vụ")
                .Annotation("SqlServer:Identity", "1001, 1")
                .OldAnnotation("SqlServer:Identity", "1001, 1");

            migrationBuilder.UpdateData(
                table: "AccountStatus",
                keyColumn: "Id",
                keyValue: 1001,
                column: "CreatedTime",
                value: new DateTime(2023, 7, 26, 17, 40, 2, 749, DateTimeKind.Local).AddTicks(3001));

            migrationBuilder.UpdateData(
                table: "AccountStatus",
                keyColumn: "Id",
                keyValue: 1002,
                column: "CreatedTime",
                value: new DateTime(2023, 7, 26, 17, 40, 2, 749, DateTimeKind.Local).AddTicks(3034));

            migrationBuilder.UpdateData(
                table: "AdminAccount",
                keyColumn: "Id",
                keyValue: 1001L,
                column: "CreatedTime",
                value: new DateTime(2023, 7, 26, 17, 40, 2, 749, DateTimeKind.Local).AddTicks(3053));
        }
    }
}
