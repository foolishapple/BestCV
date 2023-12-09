using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Jobi.Infrastructure.Persistence.Migrations
{
    public partial class updateJobSecondaryPositisionToJobSecondaryJobCategory_v1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Job_JobCategory",
                table: "Job");

            migrationBuilder.DropForeignKey(
                name: "FK_Job_PrimaryJobPosition",
                table: "Job");

            migrationBuilder.DropTable(
                name: "JobSecondaryJobPosition");

            migrationBuilder.DropIndex(
                name: "IX_Job_JobCategoryId",
                table: "Job");

            migrationBuilder.DropIndex(
                name: "IX_Job_PrimaryJobPositionId",
                table: "Job");

            migrationBuilder.DropColumn(
                name: "JobCategoryId",
                table: "Job");

            migrationBuilder.DropColumn(
                name: "PrimaryJobPositionId",
                table: "Job");

            migrationBuilder.AddColumn<int>(
                name: "JobPositionId",
                table: "Job",
                type: "int",
                nullable: false,
                defaultValue: 1001,
                comment: "Mã vị trí tuyển dụng");

            migrationBuilder.AddColumn<int>(
                name: "PrimaryJobCategoryId",
                table: "Job",
                type: "int",
                nullable: false,
                defaultValue: 1001,
                comment: "Mã vị trí ngành nghề tuyển dụng chính");

            migrationBuilder.CreateTable(
                name: "JobSecondaryJobCategory",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1001, 1"),
                    JobId = table.Column<long>(type: "bigint", nullable: false, comment: "Mã tin tuyển dụng"),
                    JobCategoryId = table.Column<int>(type: "int", nullable: false, comment: "Mã ngành nghề liên quan"),
                    Active = table.Column<bool>(type: "bit", nullable: false, defaultValueSql: "((1))", comment: "Đánh dấu bị xóa"),
                    CreatedTime = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "(getdate())", comment: "Ngày tạo")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_JobSecondaryJobCategory", x => x.Id);
                    table.ForeignKey(
                        name: "FK_JobSecondaryJobCategory_Job",
                        column: x => x.JobId,
                        principalTable: "Job",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_JobSecondaryJobCategory_JobCategory",
                        column: x => x.JobCategoryId,
                        principalTable: "JobCategory",
                        principalColumn: "Id");
                });

            migrationBuilder.UpdateData(
                table: "AccountStatus",
                keyColumn: "Id",
                keyValue: 1001,
                column: "CreatedTime",
                value: new DateTime(2023, 10, 27, 10, 51, 25, 575, DateTimeKind.Local).AddTicks(5174));

            migrationBuilder.UpdateData(
                table: "AccountStatus",
                keyColumn: "Id",
                keyValue: 1002,
                column: "CreatedTime",
                value: new DateTime(2023, 10, 27, 10, 51, 25, 575, DateTimeKind.Local).AddTicks(5237));

            migrationBuilder.UpdateData(
                table: "AdminAccount",
                keyColumn: "Id",
                keyValue: 1001L,
                column: "CreatedTime",
                value: new DateTime(2023, 10, 27, 10, 51, 25, 575, DateTimeKind.Local).AddTicks(5263));

            migrationBuilder.UpdateData(
                table: "CVTemplateStatus",
                keyColumn: "Id",
                keyValue: 1001L,
                column: "CreatedTime",
                value: new DateTime(2023, 10, 27, 10, 51, 25, 575, DateTimeKind.Local).AddTicks(5346));

            migrationBuilder.UpdateData(
                table: "CVTemplateStatus",
                keyColumn: "Id",
                keyValue: 1002L,
                column: "CreatedTime",
                value: new DateTime(2023, 10, 27, 10, 51, 25, 575, DateTimeKind.Local).AddTicks(5358));

            migrationBuilder.UpdateData(
                table: "CandidateLevel",
                keyColumn: "Id",
                keyValue: 1001,
                column: "CreatedTime",
                value: new DateTime(2023, 10, 27, 10, 51, 25, 575, DateTimeKind.Local).AddTicks(5299));

            migrationBuilder.UpdateData(
                table: "Position",
                keyColumn: "Id",
                keyValue: 1001,
                column: "CreatedTime",
                value: new DateTime(2023, 10, 27, 10, 51, 25, 575, DateTimeKind.Local).AddTicks(5324));

            migrationBuilder.CreateIndex(
                name: "IX_Job_JobPositionId",
                table: "Job",
                column: "JobPositionId");

            migrationBuilder.CreateIndex(
                name: "IX_Job_PrimaryJobCategoryId",
                table: "Job",
                column: "PrimaryJobCategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_JobSecondaryJobCategory_JobCategoryId",
                table: "JobSecondaryJobCategory",
                column: "JobCategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_JobSecondaryJobCategory_JobId",
                table: "JobSecondaryJobCategory",
                column: "JobId");

            migrationBuilder.AddForeignKey(
                name: "FK_Job_JobPosition",
                table: "Job",
                column: "JobPositionId",
                principalTable: "JobPosition",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Job_PrimaryJobCategory",
                table: "Job",
                column: "PrimaryJobCategoryId",
                principalTable: "JobCategory",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Job_JobPosition",
                table: "Job");

            migrationBuilder.DropForeignKey(
                name: "FK_Job_PrimaryJobCategory",
                table: "Job");

            migrationBuilder.DropTable(
                name: "JobSecondaryJobCategory");

            migrationBuilder.DropIndex(
                name: "IX_Job_JobPositionId",
                table: "Job");

            migrationBuilder.DropIndex(
                name: "IX_Job_PrimaryJobCategoryId",
                table: "Job");

            migrationBuilder.DropColumn(
                name: "JobPositionId",
                table: "Job");

            migrationBuilder.DropColumn(
                name: "PrimaryJobCategoryId",
                table: "Job");

            migrationBuilder.AddColumn<int>(
                name: "JobCategoryId",
                table: "Job",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "PrimaryJobPositionId",
                table: "Job",
                type: "int",
                nullable: false,
                defaultValue: 0,
                comment: "Mã vị trí tuyển dụng chính");

            migrationBuilder.CreateTable(
                name: "JobSecondaryJobPosition",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1001, 1"),
                    JobId = table.Column<long>(type: "bigint", nullable: false, comment: "Mã tin tuyển dụng"),
                    JobPositionId = table.Column<int>(type: "int", nullable: false, comment: "Mã vị trí tuyển dụng"),
                    Active = table.Column<bool>(type: "bit", nullable: false, defaultValueSql: "((1))", comment: "Đánh dấu bị xóa"),
                    CreatedTime = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "(getdate())", comment: "Ngày tạo")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_JobSecondaryJobPosition", x => x.Id);
                    table.ForeignKey(
                        name: "FK_JobSecondaryJobPosition_Job",
                        column: x => x.JobId,
                        principalTable: "Job",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_JobSecondaryJobPosition_JobPosition",
                        column: x => x.JobPositionId,
                        principalTable: "JobPosition",
                        principalColumn: "Id");
                });

            migrationBuilder.UpdateData(
                table: "AccountStatus",
                keyColumn: "Id",
                keyValue: 1001,
                column: "CreatedTime",
                value: new DateTime(2023, 10, 23, 10, 12, 58, 174, DateTimeKind.Local).AddTicks(5746));

            migrationBuilder.UpdateData(
                table: "AccountStatus",
                keyColumn: "Id",
                keyValue: 1002,
                column: "CreatedTime",
                value: new DateTime(2023, 10, 23, 10, 12, 58, 174, DateTimeKind.Local).AddTicks(5833));

            migrationBuilder.UpdateData(
                table: "AdminAccount",
                keyColumn: "Id",
                keyValue: 1001L,
                column: "CreatedTime",
                value: new DateTime(2023, 10, 23, 10, 12, 58, 174, DateTimeKind.Local).AddTicks(5865));

            migrationBuilder.UpdateData(
                table: "CVTemplateStatus",
                keyColumn: "Id",
                keyValue: 1001L,
                column: "CreatedTime",
                value: new DateTime(2023, 10, 23, 10, 12, 58, 174, DateTimeKind.Local).AddTicks(5967));

            migrationBuilder.UpdateData(
                table: "CVTemplateStatus",
                keyColumn: "Id",
                keyValue: 1002L,
                column: "CreatedTime",
                value: new DateTime(2023, 10, 23, 10, 12, 58, 174, DateTimeKind.Local).AddTicks(5981));

            migrationBuilder.UpdateData(
                table: "CandidateLevel",
                keyColumn: "Id",
                keyValue: 1001,
                column: "CreatedTime",
                value: new DateTime(2023, 10, 23, 10, 12, 58, 174, DateTimeKind.Local).AddTicks(5903));

            migrationBuilder.UpdateData(
                table: "Position",
                keyColumn: "Id",
                keyValue: 1001,
                column: "CreatedTime",
                value: new DateTime(2023, 10, 23, 10, 12, 58, 174, DateTimeKind.Local).AddTicks(5933));

            migrationBuilder.CreateIndex(
                name: "IX_Job_JobCategoryId",
                table: "Job",
                column: "JobCategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_Job_PrimaryJobPositionId",
                table: "Job",
                column: "PrimaryJobPositionId");

            migrationBuilder.CreateIndex(
                name: "IX_JobSecondaryJobPosition_JobId",
                table: "JobSecondaryJobPosition",
                column: "JobId");

            migrationBuilder.CreateIndex(
                name: "IX_JobSecondaryJobPosition_JobPositionId",
                table: "JobSecondaryJobPosition",
                column: "JobPositionId");

            migrationBuilder.AddForeignKey(
                name: "FK_Job_JobCategory",
                table: "Job",
                column: "JobCategoryId",
                principalTable: "JobCategory",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Job_PrimaryJobPosition",
                table: "Job",
                column: "PrimaryJobPositionId",
                principalTable: "JobPosition",
                principalColumn: "Id");
        }
    }
}
