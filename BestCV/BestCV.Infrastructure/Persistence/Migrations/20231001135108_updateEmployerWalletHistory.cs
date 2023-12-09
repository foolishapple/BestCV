using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BestCV.Infrastructure.Persistence.Migrations
{
    public partial class updateEmployerWalletHistory : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<long>(
                name: "CandidateId",
                table: "EmployerWalletHistories",
                type: "bigint",
                nullable: false,
                defaultValue: 0L,
                comment: "Mã ứng viên");

            migrationBuilder.UpdateData(
                table: "AccountStatus",
                keyColumn: "Id",
                keyValue: 1001,
                column: "CreatedTime",
                value: new DateTime(2023, 10, 1, 20, 51, 6, 695, DateTimeKind.Local).AddTicks(8778));

            migrationBuilder.UpdateData(
                table: "AccountStatus",
                keyColumn: "Id",
                keyValue: 1002,
                column: "CreatedTime",
                value: new DateTime(2023, 10, 1, 20, 51, 6, 695, DateTimeKind.Local).AddTicks(8856));

            migrationBuilder.UpdateData(
                table: "AdminAccount",
                keyColumn: "Id",
                keyValue: 1001L,
                column: "CreatedTime",
                value: new DateTime(2023, 10, 1, 20, 51, 6, 695, DateTimeKind.Local).AddTicks(8887));

            migrationBuilder.UpdateData(
                table: "CVTemplateStatus",
                keyColumn: "Id",
                keyValue: 1001L,
                column: "CreatedTime",
                value: new DateTime(2023, 10, 1, 20, 51, 6, 695, DateTimeKind.Local).AddTicks(9022));

            migrationBuilder.UpdateData(
                table: "CVTemplateStatus",
                keyColumn: "Id",
                keyValue: 1002L,
                column: "CreatedTime",
                value: new DateTime(2023, 10, 1, 20, 51, 6, 695, DateTimeKind.Local).AddTicks(9036));

            migrationBuilder.UpdateData(
                table: "CandidateLevel",
                keyColumn: "Id",
                keyValue: 1001,
                column: "CreatedTime",
                value: new DateTime(2023, 10, 1, 20, 51, 6, 695, DateTimeKind.Local).AddTicks(8938));

            migrationBuilder.UpdateData(
                table: "Position",
                keyColumn: "Id",
                keyValue: 1001,
                column: "CreatedTime",
                value: new DateTime(2023, 10, 1, 20, 51, 6, 695, DateTimeKind.Local).AddTicks(8978));

            migrationBuilder.CreateIndex(
                name: "IX_EmployerWalletHistories_CandidateId",
                table: "EmployerWalletHistories",
                column: "CandidateId");

            migrationBuilder.AddForeignKey(
                name: "FK_EmployerWalletHistory_Candidate",
                table: "EmployerWalletHistories",
                column: "CandidateId",
                principalTable: "Candidate",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_EmployerWalletHistory_Candidate",
                table: "EmployerWalletHistories");

            migrationBuilder.DropIndex(
                name: "IX_EmployerWalletHistories_CandidateId",
                table: "EmployerWalletHistories");

            migrationBuilder.DropColumn(
                name: "CandidateId",
                table: "EmployerWalletHistories");

            migrationBuilder.UpdateData(
                table: "AccountStatus",
                keyColumn: "Id",
                keyValue: 1001,
                column: "CreatedTime",
                value: new DateTime(2023, 9, 26, 14, 37, 12, 469, DateTimeKind.Local).AddTicks(6231));

            migrationBuilder.UpdateData(
                table: "AccountStatus",
                keyColumn: "Id",
                keyValue: 1002,
                column: "CreatedTime",
                value: new DateTime(2023, 9, 26, 14, 37, 12, 469, DateTimeKind.Local).AddTicks(6290));

            migrationBuilder.UpdateData(
                table: "AdminAccount",
                keyColumn: "Id",
                keyValue: 1001L,
                column: "CreatedTime",
                value: new DateTime(2023, 9, 26, 14, 37, 12, 469, DateTimeKind.Local).AddTicks(6312));

            migrationBuilder.UpdateData(
                table: "CVTemplateStatus",
                keyColumn: "Id",
                keyValue: 1001L,
                column: "CreatedTime",
                value: new DateTime(2023, 9, 26, 14, 37, 12, 469, DateTimeKind.Local).AddTicks(6403));

            migrationBuilder.UpdateData(
                table: "CVTemplateStatus",
                keyColumn: "Id",
                keyValue: 1002L,
                column: "CreatedTime",
                value: new DateTime(2023, 9, 26, 14, 37, 12, 469, DateTimeKind.Local).AddTicks(6415));

            migrationBuilder.UpdateData(
                table: "CandidateLevel",
                keyColumn: "Id",
                keyValue: 1001,
                column: "CreatedTime",
                value: new DateTime(2023, 9, 26, 14, 37, 12, 469, DateTimeKind.Local).AddTicks(6347));

            migrationBuilder.UpdateData(
                table: "Position",
                keyColumn: "Id",
                keyValue: 1001,
                column: "CreatedTime",
                value: new DateTime(2023, 9, 26, 14, 37, 12, 469, DateTimeKind.Local).AddTicks(6372));
        }
    }
}
