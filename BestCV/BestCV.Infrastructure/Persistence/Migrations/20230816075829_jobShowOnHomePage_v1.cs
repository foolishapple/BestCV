using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BestCV.Infrastructure.Persistence.Migrations
{
    public partial class jobShowOnHomePage_v1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "JobShowOnHomePage");

            migrationBuilder.CreateTable(
                name: "TopFeatureJob",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1001, 1"),
                    JobId = table.Column<long>(type: "bigint", nullable: false, comment: "Mã việc làm"),
                    OrderSort = table.Column<int>(type: "int", nullable: false, comment: "Sắp xếp"),
                    Active = table.Column<bool>(type: "bit", nullable: false, defaultValueSql: "((1))", comment: "Đánh dấu bị xóa"),
                    CreatedTime = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "(getdate())", comment: "Ngày tạo")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TopFeatureJob", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TopFeatureJob_Job",
                        column: x => x.JobId,
                        principalTable: "Job",
                        principalColumn: "Id");
                });

            migrationBuilder.UpdateData(
                table: "AccountStatus",
                keyColumn: "Id",
                keyValue: 1001,
                column: "CreatedTime",
                value: new DateTime(2023, 8, 16, 14, 58, 24, 640, DateTimeKind.Local).AddTicks(6621));

            migrationBuilder.UpdateData(
                table: "AccountStatus",
                keyColumn: "Id",
                keyValue: 1002,
                column: "CreatedTime",
                value: new DateTime(2023, 8, 16, 14, 58, 24, 640, DateTimeKind.Local).AddTicks(6771));

            migrationBuilder.UpdateData(
                table: "AdminAccount",
                keyColumn: "Id",
                keyValue: 1001L,
                column: "CreatedTime",
                value: new DateTime(2023, 8, 16, 14, 58, 24, 640, DateTimeKind.Local).AddTicks(6865));

            migrationBuilder.UpdateData(
                table: "CandidateLevel",
                keyColumn: "Id",
                keyValue: 1001,
                column: "CreatedTime",
                value: new DateTime(2023, 8, 16, 14, 58, 24, 640, DateTimeKind.Local).AddTicks(6984));

            migrationBuilder.UpdateData(
                table: "EmployerServicePackage",
                keyColumn: "Id",
                keyValue: 1001,
                column: "CreatedTime",
                value: new DateTime(2023, 8, 16, 14, 58, 24, 640, DateTimeKind.Local).AddTicks(7278));

            migrationBuilder.UpdateData(
                table: "Position",
                keyColumn: "Id",
                keyValue: 1001,
                column: "CreatedTime",
                value: new DateTime(2023, 8, 16, 14, 58, 24, 640, DateTimeKind.Local).AddTicks(7073));

            migrationBuilder.CreateIndex(
                name: "IX_TopFeatureJob_JobId",
                table: "TopFeatureJob",
                column: "JobId",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "TopFeatureJob");

            migrationBuilder.CreateTable(
                name: "JobShowOnHomePage",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1001, 1"),
                    JobId = table.Column<long>(type: "bigint", nullable: false, comment: "Mã việc làm"),
                    Active = table.Column<bool>(type: "bit", nullable: false, defaultValueSql: "((1))", comment: "Đánh dấu bị xóa"),
                    CreatedTime = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "(getdate())", comment: "Ngày tạo"),
                    OrderSort = table.Column<int>(type: "int", nullable: false, comment: "Sắp xếp")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_JobShowOnHomePage", x => x.Id);
                    table.ForeignKey(
                        name: "FK_JobShowOnHomePage_Job",
                        column: x => x.JobId,
                        principalTable: "Job",
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
                name: "IX_JobShowOnHomePage_JobId",
                table: "JobShowOnHomePage",
                column: "JobId",
                unique: true);
        }
    }
}
