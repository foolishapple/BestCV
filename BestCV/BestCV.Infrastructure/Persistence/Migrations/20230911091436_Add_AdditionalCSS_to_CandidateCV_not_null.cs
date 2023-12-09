using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Jobi.Infrastructure.Persistence.Migrations
{
    public partial class Add_AdditionalCSS_to_CandidateCV_not_null : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "AdditionalCSS",
                table: "CandidateCV",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                comment: "CSS bổ sung của CV",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true,
                oldComment: "CSS bổ sung của CV");

            migrationBuilder.UpdateData(
                table: "AccountStatus",
                keyColumn: "Id",
                keyValue: 1001,
                column: "CreatedTime",
                value: new DateTime(2023, 9, 11, 16, 14, 31, 244, DateTimeKind.Local).AddTicks(2953));

            migrationBuilder.UpdateData(
                table: "AccountStatus",
                keyColumn: "Id",
                keyValue: 1002,
                column: "CreatedTime",
                value: new DateTime(2023, 9, 11, 16, 14, 31, 244, DateTimeKind.Local).AddTicks(3053));

            migrationBuilder.UpdateData(
                table: "AdminAccount",
                keyColumn: "Id",
                keyValue: 1001L,
                column: "CreatedTime",
                value: new DateTime(2023, 9, 11, 16, 14, 31, 244, DateTimeKind.Local).AddTicks(3173));

            migrationBuilder.UpdateData(
                table: "CVTemplateStatus",
                keyColumn: "Id",
                keyValue: 1001L,
                column: "CreatedTime",
                value: new DateTime(2023, 9, 11, 16, 14, 31, 244, DateTimeKind.Local).AddTicks(3392));

            migrationBuilder.UpdateData(
                table: "CVTemplateStatus",
                keyColumn: "Id",
                keyValue: 1002L,
                column: "CreatedTime",
                value: new DateTime(2023, 9, 11, 16, 14, 31, 244, DateTimeKind.Local).AddTicks(3428));

            migrationBuilder.UpdateData(
                table: "CandidateLevel",
                keyColumn: "Id",
                keyValue: 1001,
                column: "CreatedTime",
                value: new DateTime(2023, 9, 11, 16, 14, 31, 244, DateTimeKind.Local).AddTicks(3265));

            migrationBuilder.UpdateData(
                table: "Position",
                keyColumn: "Id",
                keyValue: 1001,
                column: "CreatedTime",
                value: new DateTime(2023, 9, 11, 16, 14, 31, 244, DateTimeKind.Local).AddTicks(3320));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "AdditionalCSS",
                table: "CandidateCV",
                type: "nvarchar(max)",
                nullable: true,
                comment: "CSS bổ sung của CV",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldComment: "CSS bổ sung của CV");

            migrationBuilder.UpdateData(
                table: "AccountStatus",
                keyColumn: "Id",
                keyValue: 1001,
                column: "CreatedTime",
                value: new DateTime(2023, 9, 11, 16, 9, 47, 666, DateTimeKind.Local).AddTicks(6309));

            migrationBuilder.UpdateData(
                table: "AccountStatus",
                keyColumn: "Id",
                keyValue: 1002,
                column: "CreatedTime",
                value: new DateTime(2023, 9, 11, 16, 9, 47, 666, DateTimeKind.Local).AddTicks(6386));

            migrationBuilder.UpdateData(
                table: "AdminAccount",
                keyColumn: "Id",
                keyValue: 1001L,
                column: "CreatedTime",
                value: new DateTime(2023, 9, 11, 16, 9, 47, 666, DateTimeKind.Local).AddTicks(6411));

            migrationBuilder.UpdateData(
                table: "CVTemplateStatus",
                keyColumn: "Id",
                keyValue: 1001L,
                column: "CreatedTime",
                value: new DateTime(2023, 9, 11, 16, 9, 47, 666, DateTimeKind.Local).AddTicks(6511));

            migrationBuilder.UpdateData(
                table: "CVTemplateStatus",
                keyColumn: "Id",
                keyValue: 1002L,
                column: "CreatedTime",
                value: new DateTime(2023, 9, 11, 16, 9, 47, 666, DateTimeKind.Local).AddTicks(6525));

            migrationBuilder.UpdateData(
                table: "CandidateLevel",
                keyColumn: "Id",
                keyValue: 1001,
                column: "CreatedTime",
                value: new DateTime(2023, 9, 11, 16, 9, 47, 666, DateTimeKind.Local).AddTicks(6452));

            migrationBuilder.UpdateData(
                table: "Position",
                keyColumn: "Id",
                keyValue: 1001,
                column: "CreatedTime",
                value: new DateTime(2023, 9, 11, 16, 9, 47, 666, DateTimeKind.Local).AddTicks(6481));
        }
    }
}
