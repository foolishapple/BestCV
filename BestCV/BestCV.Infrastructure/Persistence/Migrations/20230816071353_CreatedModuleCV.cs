using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Jobi.Infrastructure.Persistence.Migrations
{
    public partial class CreatedModuleCV : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "CVTemplateStatus",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false, comment: "Mã trạng thái template")
                        .Annotation("SqlServer:Identity", "1001, 1"),
                    Active = table.Column<bool>(type: "bit", nullable: false, defaultValueSql: "((1))", comment: "Đánh dấu bị xóa"),
                    CreatedTime = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "(getdate())", comment: "Ngày tạo"),
                    Name = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false, comment: "Tên trạng thái CV"),
                    Description = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true, comment: "Mô tả")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CVTemplateStatus", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "CVTemplate",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false, comment: "Mã trạng thái template")
                        .Annotation("SqlServer:Identity", "1001, 1"),
                    CVTemplateStatusId = table.Column<long>(type: "bigint", nullable: false, comment: "Mã trạng thái template"),
                    Version = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true, comment: "Phiên bản template"),
                    Content = table.Column<string>(type: "nvarchar(max)", nullable: false, comment: "Nội dung HTML của template"),
                    AdditionalCSS = table.Column<string>(type: "nvarchar(max)", nullable: false, comment: "CSS bổ sung của Template"),
                    Active = table.Column<bool>(type: "bit", nullable: false, defaultValueSql: "((1))", comment: "Đánh dấu bị xóa"),
                    CreatedTime = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "(getdate())", comment: "Ngày tạo"),
                    Name = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false, comment: "Tên trạng thái CV"),
                    Description = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true, comment: "Mô tả")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CVTemplate", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CVTemplate_CVTemplateStatus",
                        column: x => x.CVTemplateStatusId,
                        principalTable: "CVTemplateStatus",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "CandidateCV",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false, comment: "Mã trạng thái template")
                        .Annotation("SqlServer:Identity", "1001, 1"),
                    Name = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false, comment: "Tên trạng thái CV"),
                    CandidateId = table.Column<long>(type: "bigint", nullable: false, comment: "Mã ứng viên"),
                    CVTemplateId = table.Column<long>(type: "bigint", nullable: false, comment: "Mã template"),
                    Content = table.Column<string>(type: "nvarchar(max)", nullable: false, comment: "Nội dung HTML của CV"),
                    Active = table.Column<bool>(type: "bit", nullable: false, defaultValueSql: "((1))", comment: "Đánh dấu bị xóa"),
                    CreatedTime = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "(getdate())", comment: "Ngày tạo")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CandidateCV", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CandidateCV_Candidate",
                        column: x => x.CandidateId,
                        principalTable: "Candidate",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_CandidateCV_CVTemplate",
                        column: x => x.CVTemplateId,
                        principalTable: "CVTemplate",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "CandidateCVPDF",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false, comment: "Mã trạng thái template")
                        .Annotation("SqlServer:Identity", "1001, 1"),
                    CandidateCvId = table.Column<long>(type: "bigint", nullable: false, comment: "Mã CV"),
                    Url = table.Column<string>(type: "nvarchar(max)", nullable: false, comment: "Đường dẫn của file CV PDF trên Server"),
                    Active = table.Column<bool>(type: "bit", nullable: false, defaultValueSql: "((1))", comment: "Đánh dấu bị xóa"),
                    CreatedTime = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "(getdate())", comment: "Ngày tạo")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CandidateCVPDF", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CandidateCVPDF_CandidateCV",
                        column: x => x.CandidateCvId,
                        principalTable: "CandidateCV",
                        principalColumn: "Id");
                });

            migrationBuilder.UpdateData(
                table: "AccountStatus",
                keyColumn: "Id",
                keyValue: 1001,
                column: "CreatedTime",
                value: new DateTime(2023, 8, 16, 14, 13, 48, 354, DateTimeKind.Local).AddTicks(9130));

            migrationBuilder.UpdateData(
                table: "AccountStatus",
                keyColumn: "Id",
                keyValue: 1002,
                column: "CreatedTime",
                value: new DateTime(2023, 8, 16, 14, 13, 48, 354, DateTimeKind.Local).AddTicks(9206));

            migrationBuilder.UpdateData(
                table: "AdminAccount",
                keyColumn: "Id",
                keyValue: 1001L,
                column: "CreatedTime",
                value: new DateTime(2023, 8, 16, 14, 13, 48, 354, DateTimeKind.Local).AddTicks(9277));

            migrationBuilder.UpdateData(
                table: "CandidateLevel",
                keyColumn: "Id",
                keyValue: 1001,
                column: "CreatedTime",
                value: new DateTime(2023, 8, 16, 14, 13, 48, 354, DateTimeKind.Local).AddTicks(9345));

            migrationBuilder.UpdateData(
                table: "EmployerServicePackage",
                keyColumn: "Id",
                keyValue: 1001,
                column: "CreatedTime",
                value: new DateTime(2023, 8, 16, 14, 13, 48, 354, DateTimeKind.Local).AddTicks(9426));

            migrationBuilder.UpdateData(
                table: "Position",
                keyColumn: "Id",
                keyValue: 1001,
                column: "CreatedTime",
                value: new DateTime(2023, 8, 16, 14, 13, 48, 354, DateTimeKind.Local).AddTicks(9377));

            migrationBuilder.CreateIndex(
                name: "IX_CandidateCV_CandidateId",
                table: "CandidateCV",
                column: "CandidateId");

            migrationBuilder.CreateIndex(
                name: "IX_CandidateCV_CVTemplateId",
                table: "CandidateCV",
                column: "CVTemplateId");

            migrationBuilder.CreateIndex(
                name: "IX_CandidateCVPDF_CandidateCvId",
                table: "CandidateCVPDF",
                column: "CandidateCvId");

            migrationBuilder.CreateIndex(
                name: "IX_CVTemplate_CVTemplateStatusId",
                table: "CVTemplate",
                column: "CVTemplateStatusId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CandidateCVPDF");

            migrationBuilder.DropTable(
                name: "CandidateCV");

            migrationBuilder.DropTable(
                name: "CVTemplate");

            migrationBuilder.DropTable(
                name: "CVTemplateStatus");

            migrationBuilder.UpdateData(
                table: "AccountStatus",
                keyColumn: "Id",
                keyValue: 1001,
                column: "CreatedTime",
                value: new DateTime(2023, 8, 15, 14, 35, 4, 662, DateTimeKind.Local).AddTicks(5448));

            migrationBuilder.UpdateData(
                table: "AccountStatus",
                keyColumn: "Id",
                keyValue: 1002,
                column: "CreatedTime",
                value: new DateTime(2023, 8, 15, 14, 35, 4, 662, DateTimeKind.Local).AddTicks(5484));

            migrationBuilder.UpdateData(
                table: "AdminAccount",
                keyColumn: "Id",
                keyValue: 1001L,
                column: "CreatedTime",
                value: new DateTime(2023, 8, 15, 14, 35, 4, 662, DateTimeKind.Local).AddTicks(5508));

            migrationBuilder.UpdateData(
                table: "CandidateLevel",
                keyColumn: "Id",
                keyValue: 1001,
                column: "CreatedTime",
                value: new DateTime(2023, 8, 15, 14, 35, 4, 662, DateTimeKind.Local).AddTicks(5553));

            migrationBuilder.UpdateData(
                table: "EmployerServicePackage",
                keyColumn: "Id",
                keyValue: 1001,
                column: "CreatedTime",
                value: new DateTime(2023, 8, 15, 14, 35, 4, 662, DateTimeKind.Local).AddTicks(5608));

            migrationBuilder.UpdateData(
                table: "Position",
                keyColumn: "Id",
                keyValue: 1001,
                column: "CreatedTime",
                value: new DateTime(2023, 8, 15, 14, 35, 4, 662, DateTimeKind.Local).AddTicks(5576));
        }
    }
}
