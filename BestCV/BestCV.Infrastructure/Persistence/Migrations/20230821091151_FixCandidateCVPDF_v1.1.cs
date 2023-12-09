using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BestCV.Infrastructure.Persistence.Migrations
{
    public partial class FixCandidateCVPDF_v11 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CandidateCVPDF_CandidateCV",
                table: "CandidateCVPDF");

            migrationBuilder.RenameColumn(
                name: "CandidateCvId",
                table: "CandidateCVPDF",
                newName: "CandidateId");

            migrationBuilder.RenameIndex(
                name: "IX_CandidateCVPDF_CandidateCvId",
                table: "CandidateCVPDF",
                newName: "IX_CandidateCVPDF_CandidateId");

            migrationBuilder.AlterColumn<string>(
                name: "Description",
                table: "JobReasonApply",
                type: "nvarchar(max)",
                nullable: true,
                comment: "Chi tiết lý do nên ứng tuyển",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldComment: "Chi tiết lý do nên ứng tuyển");

            migrationBuilder.UpdateData(
                table: "AccountStatus",
                keyColumn: "Id",
                keyValue: 1001,
                column: "CreatedTime",
                value: new DateTime(2023, 8, 21, 16, 11, 47, 82, DateTimeKind.Local).AddTicks(9913));

            migrationBuilder.UpdateData(
                table: "AccountStatus",
                keyColumn: "Id",
                keyValue: 1002,
                column: "CreatedTime",
                value: new DateTime(2023, 8, 21, 16, 11, 47, 82, DateTimeKind.Local).AddTicks(9960));

            migrationBuilder.UpdateData(
                table: "AdminAccount",
                keyColumn: "Id",
                keyValue: 1001L,
                column: "CreatedTime",
                value: new DateTime(2023, 8, 21, 16, 11, 47, 82, DateTimeKind.Local).AddTicks(9992));

            migrationBuilder.UpdateData(
                table: "CandidateLevel",
                keyColumn: "Id",
                keyValue: 1001,
                column: "CreatedTime",
                value: new DateTime(2023, 8, 21, 16, 11, 47, 83, DateTimeKind.Local).AddTicks(29));

            migrationBuilder.UpdateData(
                table: "EmployerServicePackage",
                keyColumn: "Id",
                keyValue: 1001,
                column: "CreatedTime",
                value: new DateTime(2023, 8, 21, 16, 11, 47, 83, DateTimeKind.Local).AddTicks(100));

            migrationBuilder.UpdateData(
                table: "Position",
                keyColumn: "Id",
                keyValue: 1001,
                column: "CreatedTime",
                value: new DateTime(2023, 8, 21, 16, 11, 47, 83, DateTimeKind.Local).AddTicks(64));

            migrationBuilder.AddForeignKey(
                name: "FK_CandidateCVPDF_Candidate",
                table: "CandidateCVPDF",
                column: "CandidateId",
                principalTable: "Candidate",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CandidateCVPDF_Candidate",
                table: "CandidateCVPDF");

            migrationBuilder.RenameColumn(
                name: "CandidateId",
                table: "CandidateCVPDF",
                newName: "CandidateCvId");

            migrationBuilder.RenameIndex(
                name: "IX_CandidateCVPDF_CandidateId",
                table: "CandidateCVPDF",
                newName: "IX_CandidateCVPDF_CandidateCvId");

            migrationBuilder.AlterColumn<string>(
                name: "Description",
                table: "JobReasonApply",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                comment: "Chi tiết lý do nên ứng tuyển",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true,
                oldComment: "Chi tiết lý do nên ứng tuyển");

            migrationBuilder.UpdateData(
                table: "AccountStatus",
                keyColumn: "Id",
                keyValue: 1001,
                column: "CreatedTime",
                value: new DateTime(2023, 8, 21, 14, 18, 49, 106, DateTimeKind.Local).AddTicks(4199));

            migrationBuilder.UpdateData(
                table: "AccountStatus",
                keyColumn: "Id",
                keyValue: 1002,
                column: "CreatedTime",
                value: new DateTime(2023, 8, 21, 14, 18, 49, 106, DateTimeKind.Local).AddTicks(4239));

            migrationBuilder.UpdateData(
                table: "AdminAccount",
                keyColumn: "Id",
                keyValue: 1001L,
                column: "CreatedTime",
                value: new DateTime(2023, 8, 21, 14, 18, 49, 106, DateTimeKind.Local).AddTicks(4265));

            migrationBuilder.UpdateData(
                table: "CandidateLevel",
                keyColumn: "Id",
                keyValue: 1001,
                column: "CreatedTime",
                value: new DateTime(2023, 8, 21, 14, 18, 49, 106, DateTimeKind.Local).AddTicks(4305));

            migrationBuilder.UpdateData(
                table: "EmployerServicePackage",
                keyColumn: "Id",
                keyValue: 1001,
                column: "CreatedTime",
                value: new DateTime(2023, 8, 21, 14, 18, 49, 106, DateTimeKind.Local).AddTicks(4363));

            migrationBuilder.UpdateData(
                table: "Position",
                keyColumn: "Id",
                keyValue: 1001,
                column: "CreatedTime",
                value: new DateTime(2023, 8, 21, 14, 18, 49, 106, DateTimeKind.Local).AddTicks(4330));

            migrationBuilder.AddForeignKey(
                name: "FK_CandidateCVPDF_CandidateCV",
                table: "CandidateCVPDF",
                column: "CandidateCvId",
                principalTable: "CandidateCV",
                principalColumn: "Id");
        }
    }
}
