using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BestCV.Infrastructure.Persistence.Migrations
{
    public partial class add_reference_Job_JobCategory : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "JobCategoryId",
                table: "Job",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.UpdateData(
                table: "AccountStatus",
                keyColumn: "Id",
                keyValue: 1001,
                column: "CreatedTime",
                value: new DateTime(2023, 8, 7, 18, 4, 43, 913, DateTimeKind.Local).AddTicks(8408));

            migrationBuilder.UpdateData(
                table: "AccountStatus",
                keyColumn: "Id",
                keyValue: 1002,
                column: "CreatedTime",
                value: new DateTime(2023, 8, 7, 18, 4, 43, 913, DateTimeKind.Local).AddTicks(8490));

            migrationBuilder.UpdateData(
                table: "AdminAccount",
                keyColumn: "Id",
                keyValue: 1001L,
                column: "CreatedTime",
                value: new DateTime(2023, 8, 7, 18, 4, 43, 913, DateTimeKind.Local).AddTicks(8511));

            migrationBuilder.UpdateData(
                table: "CandidateLevel",
                keyColumn: "Id",
                keyValue: 1001,
                column: "CreatedTime",
                value: new DateTime(2023, 8, 7, 18, 4, 43, 913, DateTimeKind.Local).AddTicks(8547));

            migrationBuilder.UpdateData(
                table: "EmployerServicePackage",
                keyColumn: "Id",
                keyValue: 1001,
                column: "CreatedTime",
                value: new DateTime(2023, 8, 7, 18, 4, 43, 913, DateTimeKind.Local).AddTicks(8607));

            migrationBuilder.UpdateData(
                table: "Position",
                keyColumn: "Id",
                keyValue: 1001,
                column: "CreatedTime",
                value: new DateTime(2023, 8, 7, 18, 4, 43, 913, DateTimeKind.Local).AddTicks(8573));

            migrationBuilder.CreateIndex(
                name: "IX_Job_JobCategoryId",
                table: "Job",
                column: "JobCategoryId");

            migrationBuilder.AddForeignKey(
                name: "FK_Job_JobCategory",
                table: "Job",
                column: "JobCategoryId",
                principalTable: "JobCategory",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Job_JobCategory",
                table: "Job");

            migrationBuilder.DropIndex(
                name: "IX_Job_JobCategoryId",
                table: "Job");

            migrationBuilder.DropColumn(
                name: "JobCategoryId",
                table: "Job");

            migrationBuilder.UpdateData(
                table: "AccountStatus",
                keyColumn: "Id",
                keyValue: 1001,
                column: "CreatedTime",
                value: new DateTime(2023, 8, 4, 22, 28, 40, 174, DateTimeKind.Local).AddTicks(4525));

            migrationBuilder.UpdateData(
                table: "AccountStatus",
                keyColumn: "Id",
                keyValue: 1002,
                column: "CreatedTime",
                value: new DateTime(2023, 8, 4, 22, 28, 40, 174, DateTimeKind.Local).AddTicks(4560));

            migrationBuilder.UpdateData(
                table: "AdminAccount",
                keyColumn: "Id",
                keyValue: 1001L,
                column: "CreatedTime",
                value: new DateTime(2023, 8, 4, 22, 28, 40, 174, DateTimeKind.Local).AddTicks(4578));

            migrationBuilder.UpdateData(
                table: "CandidateLevel",
                keyColumn: "Id",
                keyValue: 1001,
                column: "CreatedTime",
                value: new DateTime(2023, 8, 4, 22, 28, 40, 174, DateTimeKind.Local).AddTicks(4612));

            migrationBuilder.UpdateData(
                table: "EmployerServicePackage",
                keyColumn: "Id",
                keyValue: 1001,
                column: "CreatedTime",
                value: new DateTime(2023, 8, 4, 22, 28, 40, 174, DateTimeKind.Local).AddTicks(4662));

            migrationBuilder.UpdateData(
                table: "Position",
                keyColumn: "Id",
                keyValue: 1001,
                column: "CreatedTime",
                value: new DateTime(2023, 8, 4, 22, 28, 40, 174, DateTimeKind.Local).AddTicks(4638));
        }
    }
}
