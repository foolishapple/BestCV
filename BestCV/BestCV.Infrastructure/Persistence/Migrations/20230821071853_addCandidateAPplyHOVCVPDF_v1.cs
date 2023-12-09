using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BestCV.Infrastructure.Persistence.Migrations
{
    public partial class addCandidateAPplyHOVCVPDF_v1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CandidateApplyJobs_Candidate",
                table: "CandidateApplyJob");

            migrationBuilder.RenameColumn(
                name: "CandidateId",
                table: "CandidateApplyJob",
                newName: "CandidateCVPDFId");

            migrationBuilder.RenameIndex(
                name: "IX_CandidateApplyJob_CandidateId",
                table: "CandidateApplyJob",
                newName: "IX_CandidateApplyJob_CandidateCVPDFId");

            migrationBuilder.AddColumn<int>(
                name: "CandidateCVPDFTypeId",
                table: "CandidateCVPDF",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "CandidateCVPDFTypes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false, comment: "Mã loại CV PDF của ứng viên")
                        .Annotation("SqlServer:Identity", "1001, 1"),
                    Active = table.Column<bool>(type: "bit", nullable: false, defaultValueSql: "((1))", comment: "Đánh dấu bị xóa"),
                    CreatedTime = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "(getdate())", comment: "Ngày tạo"),
                    Name = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false, comment: "Tên loại CV PDF"),
                    Description = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true, comment: "Mô tả")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CandidateCVPDFTypes", x => x.Id);
                });

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

            migrationBuilder.CreateIndex(
                name: "IX_CandidateCVPDF_CandidateCVPDFTypeId",
                table: "CandidateCVPDF",
                column: "CandidateCVPDFTypeId");

            migrationBuilder.AddForeignKey(
                name: "FK_CandidateApplyJobs_CandidateCVPDF",
                table: "CandidateApplyJob",
                column: "CandidateCVPDFId",
                principalTable: "CandidateCVPDF",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_CandidateCVPDF_CandidateCVPDFType",
                table: "CandidateCVPDF",
                column: "CandidateCVPDFTypeId",
                principalTable: "CandidateCVPDFTypes",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CandidateApplyJobs_CandidateCVPDF",
                table: "CandidateApplyJob");

            migrationBuilder.DropForeignKey(
                name: "FK_CandidateCVPDF_CandidateCVPDFType",
                table: "CandidateCVPDF");

            migrationBuilder.DropTable(
                name: "CandidateCVPDFTypes");

            migrationBuilder.DropIndex(
                name: "IX_CandidateCVPDF_CandidateCVPDFTypeId",
                table: "CandidateCVPDF");

            migrationBuilder.DropColumn(
                name: "CandidateCVPDFTypeId",
                table: "CandidateCVPDF");

            migrationBuilder.RenameColumn(
                name: "CandidateCVPDFId",
                table: "CandidateApplyJob",
                newName: "CandidateId");

            migrationBuilder.RenameIndex(
                name: "IX_CandidateApplyJob_CandidateCVPDFId",
                table: "CandidateApplyJob",
                newName: "IX_CandidateApplyJob_CandidateId");

            migrationBuilder.UpdateData(
                table: "AccountStatus",
                keyColumn: "Id",
                keyValue: 1001,
                column: "CreatedTime",
                value: new DateTime(2023, 8, 18, 9, 39, 22, 446, DateTimeKind.Local).AddTicks(4008));

            migrationBuilder.UpdateData(
                table: "AccountStatus",
                keyColumn: "Id",
                keyValue: 1002,
                column: "CreatedTime",
                value: new DateTime(2023, 8, 18, 9, 39, 22, 446, DateTimeKind.Local).AddTicks(4067));

            migrationBuilder.UpdateData(
                table: "AdminAccount",
                keyColumn: "Id",
                keyValue: 1001L,
                column: "CreatedTime",
                value: new DateTime(2023, 8, 18, 9, 39, 22, 446, DateTimeKind.Local).AddTicks(4089));

            migrationBuilder.UpdateData(
                table: "CandidateLevel",
                keyColumn: "Id",
                keyValue: 1001,
                column: "CreatedTime",
                value: new DateTime(2023, 8, 18, 9, 39, 22, 446, DateTimeKind.Local).AddTicks(4130));

            migrationBuilder.UpdateData(
                table: "EmployerServicePackage",
                keyColumn: "Id",
                keyValue: 1001,
                column: "CreatedTime",
                value: new DateTime(2023, 8, 18, 9, 39, 22, 446, DateTimeKind.Local).AddTicks(4192));

            migrationBuilder.UpdateData(
                table: "Position",
                keyColumn: "Id",
                keyValue: 1001,
                column: "CreatedTime",
                value: new DateTime(2023, 8, 18, 9, 39, 22, 446, DateTimeKind.Local).AddTicks(4157));

            migrationBuilder.AddForeignKey(
                name: "FK_CandidateApplyJobs_Candidate",
                table: "CandidateApplyJob",
                column: "CandidateId",
                principalTable: "Candidate",
                principalColumn: "Id");
        }
    }
}
