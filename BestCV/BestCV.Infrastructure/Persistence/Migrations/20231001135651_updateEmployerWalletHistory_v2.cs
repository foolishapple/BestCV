using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BestCV.Infrastructure.Persistence.Migrations
{
    public partial class updateEmployerWalletHistory_v2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "EmploEmployerWalletId",
                table: "EmployerWalletHistories",
                newName: "EmployerWalletId");

            migrationBuilder.RenameIndex(
                name: "IX_EmployerWalletHistories_EmploEmployerWalletId",
                table: "EmployerWalletHistories",
                newName: "IX_EmployerWalletHistories_EmployerWalletId");

            migrationBuilder.UpdateData(
                table: "AccountStatus",
                keyColumn: "Id",
                keyValue: 1001,
                column: "CreatedTime",
                value: new DateTime(2023, 10, 1, 20, 56, 50, 467, DateTimeKind.Local).AddTicks(8388));

            migrationBuilder.UpdateData(
                table: "AccountStatus",
                keyColumn: "Id",
                keyValue: 1002,
                column: "CreatedTime",
                value: new DateTime(2023, 10, 1, 20, 56, 50, 467, DateTimeKind.Local).AddTicks(8429));

            migrationBuilder.UpdateData(
                table: "AdminAccount",
                keyColumn: "Id",
                keyValue: 1001L,
                column: "CreatedTime",
                value: new DateTime(2023, 10, 1, 20, 56, 50, 467, DateTimeKind.Local).AddTicks(8451));

            migrationBuilder.UpdateData(
                table: "CVTemplateStatus",
                keyColumn: "Id",
                keyValue: 1001L,
                column: "CreatedTime",
                value: new DateTime(2023, 10, 1, 20, 56, 50, 467, DateTimeKind.Local).AddTicks(8544));

            migrationBuilder.UpdateData(
                table: "CVTemplateStatus",
                keyColumn: "Id",
                keyValue: 1002L,
                column: "CreatedTime",
                value: new DateTime(2023, 10, 1, 20, 56, 50, 467, DateTimeKind.Local).AddTicks(8551));

            migrationBuilder.UpdateData(
                table: "CandidateLevel",
                keyColumn: "Id",
                keyValue: 1001,
                column: "CreatedTime",
                value: new DateTime(2023, 10, 1, 20, 56, 50, 467, DateTimeKind.Local).AddTicks(8485));

            migrationBuilder.UpdateData(
                table: "Position",
                keyColumn: "Id",
                keyValue: 1001,
                column: "CreatedTime",
                value: new DateTime(2023, 10, 1, 20, 56, 50, 467, DateTimeKind.Local).AddTicks(8512));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "EmployerWalletId",
                table: "EmployerWalletHistories",
                newName: "EmploEmployerWalletId");

            migrationBuilder.RenameIndex(
                name: "IX_EmployerWalletHistories_EmployerWalletId",
                table: "EmployerWalletHistories",
                newName: "IX_EmployerWalletHistories_EmploEmployerWalletId");

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
        }
    }
}
