using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Jobi.Infrastructure.Persistence.Migrations
{
    public partial class remove_relationship_job_and_employer_v1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Job_Employers",
                table: "Job");

            migrationBuilder.AlterColumn<long>(
                name: "EmployerId",
                table: "Job",
                type: "bigint",
                nullable: true,
                oldClrType: typeof(long),
                oldType: "bigint",
                oldComment: "Mã nhà tuyển dụng");

            migrationBuilder.UpdateData(
                table: "AccountStatus",
                keyColumn: "Id",
                keyValue: 1001,
                column: "CreatedTime",
                value: new DateTime(2023, 8, 10, 16, 18, 57, 125, DateTimeKind.Local).AddTicks(358));

            migrationBuilder.UpdateData(
                table: "AccountStatus",
                keyColumn: "Id",
                keyValue: 1002,
                column: "CreatedTime",
                value: new DateTime(2023, 8, 10, 16, 18, 57, 125, DateTimeKind.Local).AddTicks(406));

            migrationBuilder.UpdateData(
                table: "AdminAccount",
                keyColumn: "Id",
                keyValue: 1001L,
                column: "CreatedTime",
                value: new DateTime(2023, 8, 10, 16, 18, 57, 125, DateTimeKind.Local).AddTicks(434));

            migrationBuilder.UpdateData(
                table: "CandidateLevel",
                keyColumn: "Id",
                keyValue: 1001,
                column: "CreatedTime",
                value: new DateTime(2023, 8, 10, 16, 18, 57, 125, DateTimeKind.Local).AddTicks(468));

            migrationBuilder.UpdateData(
                table: "EmployerServicePackage",
                keyColumn: "Id",
                keyValue: 1001,
                column: "CreatedTime",
                value: new DateTime(2023, 8, 10, 16, 18, 57, 125, DateTimeKind.Local).AddTicks(522));

            migrationBuilder.UpdateData(
                table: "Position",
                keyColumn: "Id",
                keyValue: 1001,
                column: "CreatedTime",
                value: new DateTime(2023, 8, 10, 16, 18, 57, 125, DateTimeKind.Local).AddTicks(488));

            migrationBuilder.AddForeignKey(
                name: "FK_Job_Employer_EmployerId",
                table: "Job",
                column: "EmployerId",
                principalTable: "Employer",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Job_Employer_EmployerId",
                table: "Job");

            migrationBuilder.AlterColumn<long>(
                name: "EmployerId",
                table: "Job",
                type: "bigint",
                nullable: false,
                defaultValue: 0L,
                comment: "Mã nhà tuyển dụng",
                oldClrType: typeof(long),
                oldType: "bigint",
                oldNullable: true);

            migrationBuilder.UpdateData(
                table: "AccountStatus",
                keyColumn: "Id",
                keyValue: 1001,
                column: "CreatedTime",
                value: new DateTime(2023, 8, 10, 14, 15, 20, 896, DateTimeKind.Local).AddTicks(6994));

            migrationBuilder.UpdateData(
                table: "AccountStatus",
                keyColumn: "Id",
                keyValue: 1002,
                column: "CreatedTime",
                value: new DateTime(2023, 8, 10, 14, 15, 20, 896, DateTimeKind.Local).AddTicks(7032));

            migrationBuilder.UpdateData(
                table: "AdminAccount",
                keyColumn: "Id",
                keyValue: 1001L,
                column: "CreatedTime",
                value: new DateTime(2023, 8, 10, 14, 15, 20, 896, DateTimeKind.Local).AddTicks(7053));

            migrationBuilder.UpdateData(
                table: "CandidateLevel",
                keyColumn: "Id",
                keyValue: 1001,
                column: "CreatedTime",
                value: new DateTime(2023, 8, 10, 14, 15, 20, 896, DateTimeKind.Local).AddTicks(7089));

            migrationBuilder.UpdateData(
                table: "EmployerServicePackage",
                keyColumn: "Id",
                keyValue: 1001,
                column: "CreatedTime",
                value: new DateTime(2023, 8, 10, 14, 15, 20, 896, DateTimeKind.Local).AddTicks(7136));

            migrationBuilder.UpdateData(
                table: "Position",
                keyColumn: "Id",
                keyValue: 1001,
                column: "CreatedTime",
                value: new DateTime(2023, 8, 10, 14, 15, 20, 896, DateTimeKind.Local).AddTicks(7110));

            migrationBuilder.AddForeignKey(
                name: "FK_Job_Employers",
                table: "Job",
                column: "EmployerId",
                principalTable: "Employer",
                principalColumn: "Id");
        }
    }
}
