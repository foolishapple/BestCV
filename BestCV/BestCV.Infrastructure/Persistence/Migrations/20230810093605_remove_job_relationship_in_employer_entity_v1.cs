using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Jobi.Infrastructure.Persistence.Migrations
{
    public partial class remove_job_relationship_in_employer_entity_v1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Job_Employer_EmployerId",
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
                value: new DateTime(2023, 8, 10, 16, 36, 0, 953, DateTimeKind.Local).AddTicks(2291));

            migrationBuilder.UpdateData(
                table: "AccountStatus",
                keyColumn: "Id",
                keyValue: 1002,
                column: "CreatedTime",
                value: new DateTime(2023, 8, 10, 16, 36, 0, 953, DateTimeKind.Local).AddTicks(2342));

            migrationBuilder.UpdateData(
                table: "AdminAccount",
                keyColumn: "Id",
                keyValue: 1001L,
                column: "CreatedTime",
                value: new DateTime(2023, 8, 10, 16, 36, 0, 953, DateTimeKind.Local).AddTicks(2367));

            migrationBuilder.UpdateData(
                table: "CandidateLevel",
                keyColumn: "Id",
                keyValue: 1001,
                column: "CreatedTime",
                value: new DateTime(2023, 8, 10, 16, 36, 0, 953, DateTimeKind.Local).AddTicks(2412));

            migrationBuilder.UpdateData(
                table: "EmployerServicePackage",
                keyColumn: "Id",
                keyValue: 1001,
                column: "CreatedTime",
                value: new DateTime(2023, 8, 10, 16, 36, 0, 953, DateTimeKind.Local).AddTicks(2484));

            migrationBuilder.UpdateData(
                table: "Position",
                keyColumn: "Id",
                keyValue: 1001,
                column: "CreatedTime",
                value: new DateTime(2023, 8, 10, 16, 36, 0, 953, DateTimeKind.Local).AddTicks(2445));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<long>(
                name: "EmployerId",
                table: "Job",
                type: "bigint",
                nullable: true);

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

            migrationBuilder.CreateIndex(
                name: "IX_Job_EmployerId",
                table: "Job",
                column: "EmployerId");

            migrationBuilder.AddForeignKey(
                name: "FK_Job_Employer_EmployerId",
                table: "Job",
                column: "EmployerId",
                principalTable: "Employer",
                principalColumn: "Id");
        }
    }
}
