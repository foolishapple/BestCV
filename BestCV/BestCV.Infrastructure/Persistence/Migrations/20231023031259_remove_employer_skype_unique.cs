using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BestCV.Infrastructure.Persistence.Migrations
{
    public partial class remove_employer_skype_unique : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Employer_SkypeAccount",
                table: "Employer");

            migrationBuilder.AlterColumn<string>(
                name: "SkypeAccount",
                table: "Employer",
                type: "nvarchar(max)",
                nullable: true,
                comment: "Tài khoản Skype",
                oldClrType: typeof(string),
                oldType: "nvarchar(450)",
                oldNullable: true,
                oldComment: "Tài khoản Skype");

            migrationBuilder.UpdateData(
                table: "AccountStatus",
                keyColumn: "Id",
                keyValue: 1001,
                column: "CreatedTime",
                value: new DateTime(2023, 10, 23, 10, 12, 58, 174, DateTimeKind.Local).AddTicks(5746));

            migrationBuilder.UpdateData(
                table: "AccountStatus",
                keyColumn: "Id",
                keyValue: 1002,
                column: "CreatedTime",
                value: new DateTime(2023, 10, 23, 10, 12, 58, 174, DateTimeKind.Local).AddTicks(5833));

            migrationBuilder.UpdateData(
                table: "AdminAccount",
                keyColumn: "Id",
                keyValue: 1001L,
                column: "CreatedTime",
                value: new DateTime(2023, 10, 23, 10, 12, 58, 174, DateTimeKind.Local).AddTicks(5865));

            migrationBuilder.UpdateData(
                table: "CVTemplateStatus",
                keyColumn: "Id",
                keyValue: 1001L,
                column: "CreatedTime",
                value: new DateTime(2023, 10, 23, 10, 12, 58, 174, DateTimeKind.Local).AddTicks(5967));

            migrationBuilder.UpdateData(
                table: "CVTemplateStatus",
                keyColumn: "Id",
                keyValue: 1002L,
                column: "CreatedTime",
                value: new DateTime(2023, 10, 23, 10, 12, 58, 174, DateTimeKind.Local).AddTicks(5981));

            migrationBuilder.UpdateData(
                table: "CandidateLevel",
                keyColumn: "Id",
                keyValue: 1001,
                column: "CreatedTime",
                value: new DateTime(2023, 10, 23, 10, 12, 58, 174, DateTimeKind.Local).AddTicks(5903));

            migrationBuilder.UpdateData(
                table: "Position",
                keyColumn: "Id",
                keyValue: 1001,
                column: "CreatedTime",
                value: new DateTime(2023, 10, 23, 10, 12, 58, 174, DateTimeKind.Local).AddTicks(5933));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "SkypeAccount",
                table: "Employer",
                type: "nvarchar(450)",
                nullable: true,
                comment: "Tài khoản Skype",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true,
                oldComment: "Tài khoản Skype");

            migrationBuilder.UpdateData(
                table: "AccountStatus",
                keyColumn: "Id",
                keyValue: 1001,
                column: "CreatedTime",
                value: new DateTime(2023, 10, 19, 10, 0, 43, 282, DateTimeKind.Local).AddTicks(9251));

            migrationBuilder.UpdateData(
                table: "AccountStatus",
                keyColumn: "Id",
                keyValue: 1002,
                column: "CreatedTime",
                value: new DateTime(2023, 10, 19, 10, 0, 43, 282, DateTimeKind.Local).AddTicks(9283));

            migrationBuilder.UpdateData(
                table: "AdminAccount",
                keyColumn: "Id",
                keyValue: 1001L,
                column: "CreatedTime",
                value: new DateTime(2023, 10, 19, 10, 0, 43, 282, DateTimeKind.Local).AddTicks(9305));

            migrationBuilder.UpdateData(
                table: "CVTemplateStatus",
                keyColumn: "Id",
                keyValue: 1001L,
                column: "CreatedTime",
                value: new DateTime(2023, 10, 19, 10, 0, 43, 282, DateTimeKind.Local).AddTicks(9379));

            migrationBuilder.UpdateData(
                table: "CVTemplateStatus",
                keyColumn: "Id",
                keyValue: 1002L,
                column: "CreatedTime",
                value: new DateTime(2023, 10, 19, 10, 0, 43, 282, DateTimeKind.Local).AddTicks(9387));

            migrationBuilder.UpdateData(
                table: "CandidateLevel",
                keyColumn: "Id",
                keyValue: 1001,
                column: "CreatedTime",
                value: new DateTime(2023, 10, 19, 10, 0, 43, 282, DateTimeKind.Local).AddTicks(9332));

            migrationBuilder.UpdateData(
                table: "Position",
                keyColumn: "Id",
                keyValue: 1001,
                column: "CreatedTime",
                value: new DateTime(2023, 10, 19, 10, 0, 43, 282, DateTimeKind.Local).AddTicks(9353));

            migrationBuilder.CreateIndex(
                name: "IX_Employer_SkypeAccount",
                table: "Employer",
                column: "SkypeAccount",
                unique: true,
                filter: "[SkypeAccount] IS NOT NULL");
        }
    }
}
