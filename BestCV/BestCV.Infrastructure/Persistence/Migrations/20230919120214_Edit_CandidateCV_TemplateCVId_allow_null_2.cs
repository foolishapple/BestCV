using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BestCV.Infrastructure.Persistence.Migrations
{
    public partial class Edit_CandidateCV_TemplateCVId_allow_null_2 : Migration
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

            migrationBuilder.UpdateData(
                table: "AccountStatus",
                keyColumn: "Id",
                keyValue: 1001,
                column: "CreatedTime",
                value: new DateTime(2023, 9, 19, 19, 2, 9, 122, DateTimeKind.Local).AddTicks(4692));

            migrationBuilder.UpdateData(
                table: "AccountStatus",
                keyColumn: "Id",
                keyValue: 1002,
                column: "CreatedTime",
                value: new DateTime(2023, 9, 19, 19, 2, 9, 122, DateTimeKind.Local).AddTicks(4917));

            migrationBuilder.UpdateData(
                table: "AdminAccount",
                keyColumn: "Id",
                keyValue: 1001L,
                column: "CreatedTime",
                value: new DateTime(2023, 9, 19, 19, 2, 9, 122, DateTimeKind.Local).AddTicks(5016));

            migrationBuilder.UpdateData(
                table: "CVTemplateStatus",
                keyColumn: "Id",
                keyValue: 1001L,
                column: "CreatedTime",
                value: new DateTime(2023, 9, 19, 19, 2, 9, 122, DateTimeKind.Local).AddTicks(5207));

            migrationBuilder.UpdateData(
                table: "CVTemplateStatus",
                keyColumn: "Id",
                keyValue: 1002L,
                column: "CreatedTime",
                value: new DateTime(2023, 9, 19, 19, 2, 9, 122, DateTimeKind.Local).AddTicks(5231));

            migrationBuilder.UpdateData(
                table: "CandidateLevel",
                keyColumn: "Id",
                keyValue: 1001,
                column: "CreatedTime",
                value: new DateTime(2023, 9, 19, 19, 2, 9, 122, DateTimeKind.Local).AddTicks(5102));

            migrationBuilder.UpdateData(
                table: "Position",
                keyColumn: "Id",
                keyValue: 1001,
                column: "CreatedTime",
                value: new DateTime(2023, 9, 19, 19, 2, 9, 122, DateTimeKind.Local).AddTicks(5150));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
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
                value: new DateTime(2023, 9, 19, 18, 47, 18, 882, DateTimeKind.Local).AddTicks(1103));

            migrationBuilder.UpdateData(
                table: "AccountStatus",
                keyColumn: "Id",
                keyValue: 1002,
                column: "CreatedTime",
                value: new DateTime(2023, 9, 19, 18, 47, 18, 882, DateTimeKind.Local).AddTicks(1228));

            migrationBuilder.UpdateData(
                table: "AdminAccount",
                keyColumn: "Id",
                keyValue: 1001L,
                column: "CreatedTime",
                value: new DateTime(2023, 9, 19, 18, 47, 18, 882, DateTimeKind.Local).AddTicks(1262));

            migrationBuilder.UpdateData(
                table: "CVTemplateStatus",
                keyColumn: "Id",
                keyValue: 1001L,
                column: "CreatedTime",
                value: new DateTime(2023, 9, 19, 18, 47, 18, 882, DateTimeKind.Local).AddTicks(1382));

            migrationBuilder.UpdateData(
                table: "CVTemplateStatus",
                keyColumn: "Id",
                keyValue: 1002L,
                column: "CreatedTime",
                value: new DateTime(2023, 9, 19, 18, 47, 18, 882, DateTimeKind.Local).AddTicks(1399));

            migrationBuilder.UpdateData(
                table: "CandidateLevel",
                keyColumn: "Id",
                keyValue: 1001,
                column: "CreatedTime",
                value: new DateTime(2023, 9, 19, 18, 47, 18, 882, DateTimeKind.Local).AddTicks(1309));

            migrationBuilder.UpdateData(
                table: "Position",
                keyColumn: "Id",
                keyValue: 1001,
                column: "CreatedTime",
                value: new DateTime(2023, 9, 19, 18, 47, 18, 882, DateTimeKind.Local).AddTicks(1348));
        }
    }
}
