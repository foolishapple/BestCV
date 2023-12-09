using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Jobi.Infrastructure.Persistence.Migrations
{
    public partial class edit : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_RecruitmentStatus_Color",
                table: "RecruitmentStatus");

            migrationBuilder.DropIndex(
                name: "IX_RecruitmentCampaignStatus_Color",
                table: "RecruitmentCampaignStatus");

            migrationBuilder.DropIndex(
                name: "IX_PostStatus_Color",
                table: "PostStatus");

            migrationBuilder.DropIndex(
                name: "IX_OrderStatus_Color",
                table: "OrderStatus");

            migrationBuilder.DropIndex(
                name: "IX_InterviewStatus_Color",
                table: "InterviewStatus");

            migrationBuilder.DropIndex(
                name: "IX_CandidateApplyJobStatus_Color",
                table: "CandidateApplyJobStatus");

            migrationBuilder.UpdateData(
                table: "AccountStatus",
                keyColumn: "Id",
                keyValue: 1001,
                column: "CreatedTime",
                value: new DateTime(2023, 10, 9, 14, 28, 19, 85, DateTimeKind.Local).AddTicks(76));

            migrationBuilder.UpdateData(
                table: "AccountStatus",
                keyColumn: "Id",
                keyValue: 1002,
                column: "CreatedTime",
                value: new DateTime(2023, 10, 9, 14, 28, 19, 85, DateTimeKind.Local).AddTicks(114));

            migrationBuilder.UpdateData(
                table: "AdminAccount",
                keyColumn: "Id",
                keyValue: 1001L,
                column: "CreatedTime",
                value: new DateTime(2023, 10, 9, 14, 28, 19, 85, DateTimeKind.Local).AddTicks(135));

            migrationBuilder.UpdateData(
                table: "CVTemplateStatus",
                keyColumn: "Id",
                keyValue: 1001L,
                column: "CreatedTime",
                value: new DateTime(2023, 10, 9, 14, 28, 19, 85, DateTimeKind.Local).AddTicks(237));

            migrationBuilder.UpdateData(
                table: "CVTemplateStatus",
                keyColumn: "Id",
                keyValue: 1002L,
                column: "CreatedTime",
                value: new DateTime(2023, 10, 9, 14, 28, 19, 85, DateTimeKind.Local).AddTicks(248));

            migrationBuilder.UpdateData(
                table: "CandidateLevel",
                keyColumn: "Id",
                keyValue: 1001,
                column: "CreatedTime",
                value: new DateTime(2023, 10, 9, 14, 28, 19, 85, DateTimeKind.Local).AddTicks(173));

            migrationBuilder.UpdateData(
                table: "Position",
                keyColumn: "Id",
                keyValue: 1001,
                column: "CreatedTime",
                value: new DateTime(2023, 10, 9, 14, 28, 19, 85, DateTimeKind.Local).AddTicks(202));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AccountStatus",
                keyColumn: "Id",
                keyValue: 1001,
                column: "CreatedTime",
                value: new DateTime(2023, 10, 3, 14, 31, 4, 212, DateTimeKind.Local).AddTicks(4670));

            migrationBuilder.UpdateData(
                table: "AccountStatus",
                keyColumn: "Id",
                keyValue: 1002,
                column: "CreatedTime",
                value: new DateTime(2023, 10, 3, 14, 31, 4, 212, DateTimeKind.Local).AddTicks(4711));

            migrationBuilder.UpdateData(
                table: "AdminAccount",
                keyColumn: "Id",
                keyValue: 1001L,
                column: "CreatedTime",
                value: new DateTime(2023, 10, 3, 14, 31, 4, 212, DateTimeKind.Local).AddTicks(4761));

            migrationBuilder.UpdateData(
                table: "CVTemplateStatus",
                keyColumn: "Id",
                keyValue: 1001L,
                column: "CreatedTime",
                value: new DateTime(2023, 10, 3, 14, 31, 4, 212, DateTimeKind.Local).AddTicks(4873));

            migrationBuilder.UpdateData(
                table: "CVTemplateStatus",
                keyColumn: "Id",
                keyValue: 1002L,
                column: "CreatedTime",
                value: new DateTime(2023, 10, 3, 14, 31, 4, 212, DateTimeKind.Local).AddTicks(4882));

            migrationBuilder.UpdateData(
                table: "CandidateLevel",
                keyColumn: "Id",
                keyValue: 1001,
                column: "CreatedTime",
                value: new DateTime(2023, 10, 3, 14, 31, 4, 212, DateTimeKind.Local).AddTicks(4810));

            migrationBuilder.UpdateData(
                table: "Position",
                keyColumn: "Id",
                keyValue: 1001,
                column: "CreatedTime",
                value: new DateTime(2023, 10, 3, 14, 31, 4, 212, DateTimeKind.Local).AddTicks(4839));

            migrationBuilder.CreateIndex(
                name: "IX_RecruitmentStatus_Color",
                table: "RecruitmentStatus",
                column: "Color",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_RecruitmentCampaignStatus_Color",
                table: "RecruitmentCampaignStatus",
                column: "Color",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_PostStatus_Color",
                table: "PostStatus",
                column: "Color",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_OrderStatus_Color",
                table: "OrderStatus",
                column: "Color",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_InterviewStatus_Color",
                table: "InterviewStatus",
                column: "Color",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_CandidateApplyJobStatus_Color",
                table: "CandidateApplyJobStatus",
                column: "Color",
                unique: true);
        }
    }
}
