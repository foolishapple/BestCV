using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Jobi.Infrastructure.Persistence.Migrations
{
    public partial class RemoveUniqueNameInJobReason_v1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_JobReasonApply_Name",
                table: "JobReasonApply");

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "JobReasonApply",
                type: "nvarchar(max)",
                nullable: false,
                comment: "Lý do nên ứng tuyển",
                oldClrType: typeof(string),
                oldType: "nvarchar(450)",
                oldComment: "Lý do nên ứng tuyển");

            migrationBuilder.UpdateData(
                table: "AccountStatus",
                keyColumn: "Id",
                keyValue: 1001,
                column: "CreatedTime",
                value: new DateTime(2023, 8, 25, 15, 53, 46, 691, DateTimeKind.Local).AddTicks(2776));

            migrationBuilder.UpdateData(
                table: "AccountStatus",
                keyColumn: "Id",
                keyValue: 1002,
                column: "CreatedTime",
                value: new DateTime(2023, 8, 25, 15, 53, 46, 691, DateTimeKind.Local).AddTicks(2819));

            migrationBuilder.UpdateData(
                table: "AdminAccount",
                keyColumn: "Id",
                keyValue: 1001L,
                column: "CreatedTime",
                value: new DateTime(2023, 8, 25, 15, 53, 46, 691, DateTimeKind.Local).AddTicks(2847));

            migrationBuilder.UpdateData(
                table: "CandidateLevel",
                keyColumn: "Id",
                keyValue: 1001,
                column: "CreatedTime",
                value: new DateTime(2023, 8, 25, 15, 53, 46, 691, DateTimeKind.Local).AddTicks(2885));

            migrationBuilder.UpdateData(
                table: "EmployerServicePackage",
                keyColumn: "Id",
                keyValue: 1001,
                column: "CreatedTime",
                value: new DateTime(2023, 8, 25, 15, 53, 46, 691, DateTimeKind.Local).AddTicks(2948));

            migrationBuilder.UpdateData(
                table: "Position",
                keyColumn: "Id",
                keyValue: 1001,
                column: "CreatedTime",
                value: new DateTime(2023, 8, 25, 15, 53, 46, 691, DateTimeKind.Local).AddTicks(2917));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "JobReasonApply",
                type: "nvarchar(450)",
                nullable: false,
                comment: "Lý do nên ứng tuyển",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldComment: "Lý do nên ứng tuyển");

            migrationBuilder.UpdateData(
                table: "AccountStatus",
                keyColumn: "Id",
                keyValue: 1001,
                column: "CreatedTime",
                value: new DateTime(2023, 8, 22, 18, 30, 52, 95, DateTimeKind.Local).AddTicks(7015));

            migrationBuilder.UpdateData(
                table: "AccountStatus",
                keyColumn: "Id",
                keyValue: 1002,
                column: "CreatedTime",
                value: new DateTime(2023, 8, 22, 18, 30, 52, 95, DateTimeKind.Local).AddTicks(7083));

            migrationBuilder.UpdateData(
                table: "AdminAccount",
                keyColumn: "Id",
                keyValue: 1001L,
                column: "CreatedTime",
                value: new DateTime(2023, 8, 22, 18, 30, 52, 95, DateTimeKind.Local).AddTicks(7103));

            migrationBuilder.UpdateData(
                table: "CandidateLevel",
                keyColumn: "Id",
                keyValue: 1001,
                column: "CreatedTime",
                value: new DateTime(2023, 8, 22, 18, 30, 52, 95, DateTimeKind.Local).AddTicks(7139));

            migrationBuilder.UpdateData(
                table: "EmployerServicePackage",
                keyColumn: "Id",
                keyValue: 1001,
                column: "CreatedTime",
                value: new DateTime(2023, 8, 22, 18, 30, 52, 95, DateTimeKind.Local).AddTicks(7201));

            migrationBuilder.UpdateData(
                table: "Position",
                keyColumn: "Id",
                keyValue: 1001,
                column: "CreatedTime",
                value: new DateTime(2023, 8, 22, 18, 30, 52, 95, DateTimeKind.Local).AddTicks(7171));

            migrationBuilder.CreateIndex(
                name: "IX_JobReasonApply_Name",
                table: "JobReasonApply",
                column: "Name",
                unique: true);
        }
    }
}
