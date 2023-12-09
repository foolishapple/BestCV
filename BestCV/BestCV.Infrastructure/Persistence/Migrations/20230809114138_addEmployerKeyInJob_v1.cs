using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BestCV.Infrastructure.Persistence.Migrations
{
    public partial class addEmployerKeyInJob_v1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<long>(
                name: "EmployerId",
                table: "Job",
                type: "bigint",
                nullable: true,
                comment: "Mã nhà tuyển dụng");

            migrationBuilder.UpdateData(
                table: "AccountStatus",
                keyColumn: "Id",
                keyValue: 1001,
                column: "CreatedTime",
                value: new DateTime(2023, 8, 9, 18, 41, 34, 617, DateTimeKind.Local).AddTicks(9341));

            migrationBuilder.UpdateData(
                table: "AccountStatus",
                keyColumn: "Id",
                keyValue: 1002,
                column: "CreatedTime",
                value: new DateTime(2023, 8, 9, 18, 41, 34, 617, DateTimeKind.Local).AddTicks(9385));

            migrationBuilder.UpdateData(
                table: "AdminAccount",
                keyColumn: "Id",
                keyValue: 1001L,
                column: "CreatedTime",
                value: new DateTime(2023, 8, 9, 18, 41, 34, 617, DateTimeKind.Local).AddTicks(9407));

            migrationBuilder.UpdateData(
                table: "CandidateLevel",
                keyColumn: "Id",
                keyValue: 1001,
                column: "CreatedTime",
                value: new DateTime(2023, 8, 9, 18, 41, 34, 617, DateTimeKind.Local).AddTicks(9448));

            migrationBuilder.UpdateData(
                table: "EmployerServicePackage",
                keyColumn: "Id",
                keyValue: 1001,
                column: "CreatedTime",
                value: new DateTime(2023, 8, 9, 18, 41, 34, 617, DateTimeKind.Local).AddTicks(9504));

            migrationBuilder.UpdateData(
                table: "Position",
                keyColumn: "Id",
                keyValue: 1001,
                column: "CreatedTime",
                value: new DateTime(2023, 8, 9, 18, 41, 34, 617, DateTimeKind.Local).AddTicks(9474));

            migrationBuilder.CreateIndex(
                name: "IX_Job_EmployerId",
                table: "Job",
                column: "EmployerId");

            migrationBuilder.AddForeignKey(
                name: "FK_Job_Employers",
                table: "Job",
                column: "EmployerId",
                principalTable: "Employer",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Job_Employers",
                table: "Job");

            migrationBuilder.DropIndex(
                name: "IX_Job_EmployerId",
                table: "Job");

            migrationBuilder.DropColumn(
                name: "EmployerId",
                table: "Job");

            migrationBuilder.UpdateData(
                table: "AccountStatus",
                keyColumn: "Id",
                keyValue: 1001,
                column: "CreatedTime",
                value: new DateTime(2023, 8, 9, 14, 19, 39, 355, DateTimeKind.Local).AddTicks(6543));

            migrationBuilder.UpdateData(
                table: "AccountStatus",
                keyColumn: "Id",
                keyValue: 1002,
                column: "CreatedTime",
                value: new DateTime(2023, 8, 9, 14, 19, 39, 355, DateTimeKind.Local).AddTicks(6575));

            migrationBuilder.UpdateData(
                table: "AdminAccount",
                keyColumn: "Id",
                keyValue: 1001L,
                column: "CreatedTime",
                value: new DateTime(2023, 8, 9, 14, 19, 39, 355, DateTimeKind.Local).AddTicks(6596));

            migrationBuilder.UpdateData(
                table: "CandidateLevel",
                keyColumn: "Id",
                keyValue: 1001,
                column: "CreatedTime",
                value: new DateTime(2023, 8, 9, 14, 19, 39, 355, DateTimeKind.Local).AddTicks(6629));

            migrationBuilder.UpdateData(
                table: "EmployerServicePackage",
                keyColumn: "Id",
                keyValue: 1001,
                column: "CreatedTime",
                value: new DateTime(2023, 8, 9, 14, 19, 39, 355, DateTimeKind.Local).AddTicks(6687));

            migrationBuilder.UpdateData(
                table: "Position",
                keyColumn: "Id",
                keyValue: 1001,
                column: "CreatedTime",
                value: new DateTime(2023, 8, 9, 14, 19, 39, 355, DateTimeKind.Local).AddTicks(6654));
        }
    }
}
