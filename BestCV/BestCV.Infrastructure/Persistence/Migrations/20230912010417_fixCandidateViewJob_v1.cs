using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BestCV.Infrastructure.Persistence.Migrations
{
    public partial class fixCandidateViewJob_v1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AccountStatus",
                keyColumn: "Id",
                keyValue: 1001,
                column: "CreatedTime",
                value: new DateTime(2023, 9, 12, 8, 4, 12, 386, DateTimeKind.Local).AddTicks(786));

            migrationBuilder.UpdateData(
                table: "AccountStatus",
                keyColumn: "Id",
                keyValue: 1002,
                column: "CreatedTime",
                value: new DateTime(2023, 9, 12, 8, 4, 12, 386, DateTimeKind.Local).AddTicks(852));

            migrationBuilder.UpdateData(
                table: "AdminAccount",
                keyColumn: "Id",
                keyValue: 1001L,
                column: "CreatedTime",
                value: new DateTime(2023, 9, 12, 8, 4, 12, 386, DateTimeKind.Local).AddTicks(876));

            migrationBuilder.UpdateData(
                table: "CVTemplateStatus",
                keyColumn: "Id",
                keyValue: 1001L,
                column: "CreatedTime",
                value: new DateTime(2023, 9, 12, 8, 4, 12, 386, DateTimeKind.Local).AddTicks(971));

            migrationBuilder.UpdateData(
                table: "CVTemplateStatus",
                keyColumn: "Id",
                keyValue: 1002L,
                column: "CreatedTime",
                value: new DateTime(2023, 9, 12, 8, 4, 12, 386, DateTimeKind.Local).AddTicks(982));

            migrationBuilder.UpdateData(
                table: "CandidateLevel",
                keyColumn: "Id",
                keyValue: 1001,
                column: "CreatedTime",
                value: new DateTime(2023, 9, 12, 8, 4, 12, 386, DateTimeKind.Local).AddTicks(912));

            migrationBuilder.UpdateData(
                table: "Position",
                keyColumn: "Id",
                keyValue: 1001,
                column: "CreatedTime",
                value: new DateTime(2023, 9, 12, 8, 4, 12, 386, DateTimeKind.Local).AddTicks(944));

            migrationBuilder.CreateIndex(
                name: "IX_CandidateViewedJob_JobId",
                table: "CandidateViewedJob",
                column: "JobId");

            migrationBuilder.AddForeignKey(
                name: "FK_CandidateViewedJob_Job",
                table: "CandidateViewedJob",
                column: "JobId",
                principalTable: "Job",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CandidateViewedJob_Job",
                table: "CandidateViewedJob");

            migrationBuilder.DropIndex(
                name: "IX_CandidateViewedJob_JobId",
                table: "CandidateViewedJob");

            migrationBuilder.UpdateData(
                table: "AccountStatus",
                keyColumn: "Id",
                keyValue: 1001,
                column: "CreatedTime",
                value: new DateTime(2023, 9, 11, 16, 38, 31, 897, DateTimeKind.Local).AddTicks(2336));

            migrationBuilder.UpdateData(
                table: "AccountStatus",
                keyColumn: "Id",
                keyValue: 1002,
                column: "CreatedTime",
                value: new DateTime(2023, 9, 11, 16, 38, 31, 897, DateTimeKind.Local).AddTicks(2396));

            migrationBuilder.UpdateData(
                table: "AdminAccount",
                keyColumn: "Id",
                keyValue: 1001L,
                column: "CreatedTime",
                value: new DateTime(2023, 9, 11, 16, 38, 31, 897, DateTimeKind.Local).AddTicks(2419));

            migrationBuilder.UpdateData(
                table: "CVTemplateStatus",
                keyColumn: "Id",
                keyValue: 1001L,
                column: "CreatedTime",
                value: new DateTime(2023, 9, 11, 16, 22, 22, 676, DateTimeKind.Local).AddTicks(1762));

            migrationBuilder.UpdateData(
                table: "CVTemplateStatus",
                keyColumn: "Id",
                keyValue: 1002L,
                column: "CreatedTime",
                value: new DateTime(2023, 9, 11, 16, 22, 22, 676, DateTimeKind.Local).AddTicks(1782));

            migrationBuilder.UpdateData(
                table: "CandidateLevel",
                keyColumn: "Id",
                keyValue: 1001,
                column: "CreatedTime",
                value: new DateTime(2023, 9, 11, 16, 38, 31, 897, DateTimeKind.Local).AddTicks(2452));

            migrationBuilder.UpdateData(
                table: "Position",
                keyColumn: "Id",
                keyValue: 1001,
                column: "CreatedTime",
                value: new DateTime(2023, 9, 11, 16, 38, 31, 897, DateTimeKind.Local).AddTicks(2478));
        }
    }
}
