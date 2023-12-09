using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Jobi.Infrastructure.Persistence.Migrations
{
    public partial class Add_ModifiedTime_to_CandidateCV : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<long>(
                name: "CVTemplateId",
                table: "CandidateCV",
                type: "bigint",
                nullable: true,
                comment: "Mã template",
                oldClrType: typeof(long),
                oldType: "bigint",
                oldComment: "Mã template");

            migrationBuilder.AddColumn<DateTime>(
                name: "ModifiedTime",
                table: "CandidateCV",
                type: "datetime2",
                nullable: false,
                defaultValueSql: "(getdate())",
                comment: "Ngày sửa đổi");

            migrationBuilder.UpdateData(
                table: "AccountStatus",
                keyColumn: "Id",
                keyValue: 1001,
                column: "CreatedTime",
                value: new DateTime(2023, 9, 20, 17, 31, 22, 39, DateTimeKind.Local).AddTicks(7400));

            migrationBuilder.UpdateData(
                table: "AccountStatus",
                keyColumn: "Id",
                keyValue: 1002,
                column: "CreatedTime",
                value: new DateTime(2023, 9, 20, 17, 31, 22, 39, DateTimeKind.Local).AddTicks(7488));

            migrationBuilder.UpdateData(
                table: "AdminAccount",
                keyColumn: "Id",
                keyValue: 1001L,
                column: "CreatedTime",
                value: new DateTime(2023, 9, 20, 17, 31, 22, 39, DateTimeKind.Local).AddTicks(7560));

            migrationBuilder.UpdateData(
                table: "CVTemplateStatus",
                keyColumn: "Id",
                keyValue: 1001L,
                column: "CreatedTime",
                value: new DateTime(2023, 9, 20, 17, 31, 22, 39, DateTimeKind.Local).AddTicks(7708));

            migrationBuilder.UpdateData(
                table: "CVTemplateStatus",
                keyColumn: "Id",
                keyValue: 1002L,
                column: "CreatedTime",
                value: new DateTime(2023, 9, 20, 17, 31, 22, 39, DateTimeKind.Local).AddTicks(7725));

            migrationBuilder.UpdateData(
                table: "CandidateLevel",
                keyColumn: "Id",
                keyValue: 1001,
                column: "CreatedTime",
                value: new DateTime(2023, 9, 20, 17, 31, 22, 39, DateTimeKind.Local).AddTicks(7625));

            migrationBuilder.UpdateData(
                table: "Position",
                keyColumn: "Id",
                keyValue: 1001,
                column: "CreatedTime",
                value: new DateTime(2023, 9, 20, 17, 31, 22, 39, DateTimeKind.Local).AddTicks(7669));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ModifiedTime",
                table: "CandidateCV");

            migrationBuilder.AlterColumn<long>(
                name: "CVTemplateId",
                table: "CandidateCV",
                type: "bigint",
                nullable: false,
                defaultValue: 0L,
                comment: "Mã template",
                oldClrType: typeof(long),
                oldType: "bigint",
                oldNullable: true,
                oldComment: "Mã template");

            migrationBuilder.UpdateData(
                table: "AccountStatus",
                keyColumn: "Id",
                keyValue: 1001,
                column: "CreatedTime",
                value: new DateTime(2023, 9, 20, 9, 30, 49, 200, DateTimeKind.Local).AddTicks(6381));

            migrationBuilder.UpdateData(
                table: "AccountStatus",
                keyColumn: "Id",
                keyValue: 1002,
                column: "CreatedTime",
                value: new DateTime(2023, 9, 20, 9, 30, 49, 200, DateTimeKind.Local).AddTicks(6434));

            migrationBuilder.UpdateData(
                table: "AdminAccount",
                keyColumn: "Id",
                keyValue: 1001L,
                column: "CreatedTime",
                value: new DateTime(2023, 9, 20, 9, 30, 49, 200, DateTimeKind.Local).AddTicks(6456));

            migrationBuilder.UpdateData(
                table: "CVTemplateStatus",
                keyColumn: "Id",
                keyValue: 1001L,
                column: "CreatedTime",
                value: new DateTime(2023, 9, 20, 9, 30, 49, 200, DateTimeKind.Local).AddTicks(6528));

            migrationBuilder.UpdateData(
                table: "CVTemplateStatus",
                keyColumn: "Id",
                keyValue: 1002L,
                column: "CreatedTime",
                value: new DateTime(2023, 9, 20, 9, 30, 49, 200, DateTimeKind.Local).AddTicks(6539));

            migrationBuilder.UpdateData(
                table: "CandidateLevel",
                keyColumn: "Id",
                keyValue: 1001,
                column: "CreatedTime",
                value: new DateTime(2023, 9, 20, 9, 30, 49, 200, DateTimeKind.Local).AddTicks(6485));

            migrationBuilder.UpdateData(
                table: "Position",
                keyColumn: "Id",
                keyValue: 1001,
                column: "CreatedTime",
                value: new DateTime(2023, 9, 20, 9, 30, 49, 200, DateTimeKind.Local).AddTicks(6506));
        }
    }
}
