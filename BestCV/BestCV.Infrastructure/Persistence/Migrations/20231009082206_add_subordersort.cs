using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BestCV.Infrastructure.Persistence.Migrations
{
    public partial class add_subordersort : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "SubOrderSort",
                table: "TopJobUrgent",
                type: "int",
                nullable: false,
                comment: "Thứ tự sắp xếp phụ giữa các order sort cùng bậc",
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<int>(
                name: "SubOrderSort",
                table: "TopJobExtra",
                type: "int",
                nullable: false,
                comment: "Thứ tự sắp xếp phụ giữa các order sort cùng bậc",
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<int>(
                name: "SubOrderSort",
                table: "TopFeatureJob",
                type: "int",
                nullable: false,
                comment: "Thứ tự sắp xếp phụ giữa các order sort cùng bậc",
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddColumn<int>(
                name: "SubOrderSort",
                table: "TopCompany",
                type: "int",
                nullable: false,
                defaultValue: 0,
                comment: "Thứ tự sắp xếp phụ giữa các order sort cùng bậc");

            migrationBuilder.UpdateData(
                table: "AccountStatus",
                keyColumn: "Id",
                keyValue: 1001,
                column: "CreatedTime",
                value: new DateTime(2023, 10, 9, 15, 22, 1, 186, DateTimeKind.Local).AddTicks(5866));

            migrationBuilder.UpdateData(
                table: "AccountStatus",
                keyColumn: "Id",
                keyValue: 1002,
                column: "CreatedTime",
                value: new DateTime(2023, 10, 9, 15, 22, 1, 186, DateTimeKind.Local).AddTicks(5919));

            migrationBuilder.UpdateData(
                table: "AdminAccount",
                keyColumn: "Id",
                keyValue: 1001L,
                column: "CreatedTime",
                value: new DateTime(2023, 10, 9, 15, 22, 1, 186, DateTimeKind.Local).AddTicks(5947));

            migrationBuilder.UpdateData(
                table: "CVTemplateStatus",
                keyColumn: "Id",
                keyValue: 1001L,
                column: "CreatedTime",
                value: new DateTime(2023, 10, 9, 15, 22, 1, 186, DateTimeKind.Local).AddTicks(6061));

            migrationBuilder.UpdateData(
                table: "CVTemplateStatus",
                keyColumn: "Id",
                keyValue: 1002L,
                column: "CreatedTime",
                value: new DateTime(2023, 10, 9, 15, 22, 1, 186, DateTimeKind.Local).AddTicks(6074));

            migrationBuilder.UpdateData(
                table: "CandidateLevel",
                keyColumn: "Id",
                keyValue: 1001,
                column: "CreatedTime",
                value: new DateTime(2023, 10, 9, 15, 22, 1, 186, DateTimeKind.Local).AddTicks(5992));

            migrationBuilder.UpdateData(
                table: "Position",
                keyColumn: "Id",
                keyValue: 1001,
                column: "CreatedTime",
                value: new DateTime(2023, 10, 9, 15, 22, 1, 186, DateTimeKind.Local).AddTicks(6029));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "SubOrderSort",
                table: "TopCompany");

            migrationBuilder.AlterColumn<int>(
                name: "SubOrderSort",
                table: "TopJobUrgent",
                type: "int",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int",
                oldComment: "Thứ tự sắp xếp phụ giữa các order sort cùng bậc");

            migrationBuilder.AlterColumn<int>(
                name: "SubOrderSort",
                table: "TopJobExtra",
                type: "int",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int",
                oldComment: "Thứ tự sắp xếp phụ giữa các order sort cùng bậc");

            migrationBuilder.AlterColumn<int>(
                name: "SubOrderSort",
                table: "TopFeatureJob",
                type: "int",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int",
                oldComment: "Thứ tự sắp xếp phụ giữa các order sort cùng bậc");

            migrationBuilder.UpdateData(
                table: "AccountStatus",
                keyColumn: "Id",
                keyValue: 1001,
                column: "CreatedTime",
                value: new DateTime(2023, 10, 9, 14, 28, 19, 85, DateTimeKind.Local).AddTicks(76));

            migrationBuilder.UpdateData(
                table: "AccountStatus",
                keyColumn: "Id",
                keyValue: 1002,
                column: "CreatedTime",
                value: new DateTime(2023, 10, 9, 14, 28, 19, 85, DateTimeKind.Local).AddTicks(114));

            migrationBuilder.UpdateData(
                table: "AdminAccount",
                keyColumn: "Id",
                keyValue: 1001L,
                column: "CreatedTime",
                value: new DateTime(2023, 10, 9, 14, 28, 19, 85, DateTimeKind.Local).AddTicks(135));

            migrationBuilder.UpdateData(
                table: "CVTemplateStatus",
                keyColumn: "Id",
                keyValue: 1001L,
                column: "CreatedTime",
                value: new DateTime(2023, 10, 9, 14, 28, 19, 85, DateTimeKind.Local).AddTicks(237));

            migrationBuilder.UpdateData(
                table: "CVTemplateStatus",
                keyColumn: "Id",
                keyValue: 1002L,
                column: "CreatedTime",
                value: new DateTime(2023, 10, 9, 14, 28, 19, 85, DateTimeKind.Local).AddTicks(248));

            migrationBuilder.UpdateData(
                table: "CandidateLevel",
                keyColumn: "Id",
                keyValue: 1001,
                column: "CreatedTime",
                value: new DateTime(2023, 10, 9, 14, 28, 19, 85, DateTimeKind.Local).AddTicks(173));

            migrationBuilder.UpdateData(
                table: "Position",
                keyColumn: "Id",
                keyValue: 1001,
                column: "CreatedTime",
                value: new DateTime(2023, 10, 9, 14, 28, 19, 85, DateTimeKind.Local).AddTicks(202));
        }
    }
}
