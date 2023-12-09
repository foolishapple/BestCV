using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Jobi.Infrastructure.Persistence.Migrations
{
    public partial class add_relationship_RecruitmentCampaigns_and_Employer_v2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<long>(
                name: "EmployerId",
                table: "RecruitmentCampaign",
                type: "bigint",
                nullable: false,
                defaultValue: 0L,
                comment: "Mã nhà tuyển dụng tạo chiến dịch",
                oldClrType: typeof(long),
                oldType: "bigint",
                oldNullable: true,
                oldComment: "Mã nhà tuyển dụng tạo chiến dịch");

            migrationBuilder.UpdateData(
                table: "AccountStatus",
                keyColumn: "Id",
                keyValue: 1001,
                column: "CreatedTime",
                value: new DateTime(2023, 8, 10, 17, 6, 33, 660, DateTimeKind.Local).AddTicks(395));

            migrationBuilder.UpdateData(
                table: "AccountStatus",
                keyColumn: "Id",
                keyValue: 1002,
                column: "CreatedTime",
                value: new DateTime(2023, 8, 10, 17, 6, 33, 660, DateTimeKind.Local).AddTicks(441));

            migrationBuilder.UpdateData(
                table: "AdminAccount",
                keyColumn: "Id",
                keyValue: 1001L,
                column: "CreatedTime",
                value: new DateTime(2023, 8, 10, 17, 6, 33, 660, DateTimeKind.Local).AddTicks(463));

            migrationBuilder.UpdateData(
                table: "CandidateLevel",
                keyColumn: "Id",
                keyValue: 1001,
                column: "CreatedTime",
                value: new DateTime(2023, 8, 10, 17, 6, 33, 660, DateTimeKind.Local).AddTicks(504));

            migrationBuilder.UpdateData(
                table: "EmployerServicePackage",
                keyColumn: "Id",
                keyValue: 1001,
                column: "CreatedTime",
                value: new DateTime(2023, 8, 10, 17, 6, 33, 660, DateTimeKind.Local).AddTicks(561));

            migrationBuilder.UpdateData(
                table: "Position",
                keyColumn: "Id",
                keyValue: 1001,
                column: "CreatedTime",
                value: new DateTime(2023, 8, 10, 17, 6, 33, 660, DateTimeKind.Local).AddTicks(527));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<long>(
                name: "EmployerId",
                table: "RecruitmentCampaign",
                type: "bigint",
                nullable: true,
                comment: "Mã nhà tuyển dụng tạo chiến dịch",
                oldClrType: typeof(long),
                oldType: "bigint",
                oldComment: "Mã nhà tuyển dụng tạo chiến dịch");

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
        }
    }
}
