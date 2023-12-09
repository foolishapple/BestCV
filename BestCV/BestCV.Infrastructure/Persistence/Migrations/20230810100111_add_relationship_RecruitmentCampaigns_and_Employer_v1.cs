using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Jobi.Infrastructure.Persistence.Migrations
{
    public partial class add_relationship_RecruitmentCampaigns_and_Employer_v1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<long>(
                name: "EmployerId",
                table: "RecruitmentCampaign",
                type: "bigint",
                nullable: true,
                comment: "Mã nhà tuyển dụng tạo chiến dịch");

            migrationBuilder.UpdateData(
                table: "AccountStatus",
                keyColumn: "Id",
                keyValue: 1001,
                column: "CreatedTime",
                value: new DateTime(2023, 8, 10, 17, 1, 6, 953, DateTimeKind.Local).AddTicks(1250));

            migrationBuilder.UpdateData(
                table: "AccountStatus",
                keyColumn: "Id",
                keyValue: 1002,
                column: "CreatedTime",
                value: new DateTime(2023, 8, 10, 17, 1, 6, 953, DateTimeKind.Local).AddTicks(1289));

            migrationBuilder.UpdateData(
                table: "AdminAccount",
                keyColumn: "Id",
                keyValue: 1001L,
                column: "CreatedTime",
                value: new DateTime(2023, 8, 10, 17, 1, 6, 953, DateTimeKind.Local).AddTicks(1308));

            migrationBuilder.UpdateData(
                table: "CandidateLevel",
                keyColumn: "Id",
                keyValue: 1001,
                column: "CreatedTime",
                value: new DateTime(2023, 8, 10, 17, 1, 6, 953, DateTimeKind.Local).AddTicks(1349));

            migrationBuilder.UpdateData(
                table: "EmployerServicePackage",
                keyColumn: "Id",
                keyValue: 1001,
                column: "CreatedTime",
                value: new DateTime(2023, 8, 10, 17, 1, 6, 953, DateTimeKind.Local).AddTicks(1410));

            migrationBuilder.UpdateData(
                table: "Position",
                keyColumn: "Id",
                keyValue: 1001,
                column: "CreatedTime",
                value: new DateTime(2023, 8, 10, 17, 1, 6, 953, DateTimeKind.Local).AddTicks(1376));

            migrationBuilder.CreateIndex(
                name: "IX_RecruitmentCampaign_EmployerId",
                table: "RecruitmentCampaign",
                column: "EmployerId");

            migrationBuilder.AddForeignKey(
                name: "FK_RecruitmentCampaigns_Employer",
                table: "RecruitmentCampaign",
                column: "EmployerId",
                principalTable: "Employer",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_RecruitmentCampaigns_Employer",
                table: "RecruitmentCampaign");

            migrationBuilder.DropIndex(
                name: "IX_RecruitmentCampaign_EmployerId",
                table: "RecruitmentCampaign");

            migrationBuilder.DropColumn(
                name: "EmployerId",
                table: "RecruitmentCampaign");

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
    }
}
