using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Jobi.Infrastructure.Persistence.Migrations
{
    public partial class AddModuleRefreshJob_v1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "RefreshDate",
                table: "Job",
                type: "datetime2",
                nullable: true,
                defaultValueSql: "(getdate())",
                comment: "Ngày làm mới");

            migrationBuilder.CreateTable(
                name: "RefreshJob",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false, comment: "Mã")
                        .Annotation("SqlServer:Identity", "1001, 1"),
                    Description = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true, comment: "Mô tả"),
                    JobId = table.Column<long>(type: "bigint", nullable: false, comment: "Mã tin tuyển dụng"),
                    RefreshDate = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "(getdate())", comment: "Ngày làm mới"),
                    Active = table.Column<bool>(type: "bit", nullable: false, defaultValue: true, comment: "Đánh dấu bị xóa"),
                    CreatedTime = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "(getdate())", comment: "Ngày tạo")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RefreshJob", x => x.Id);
                    table.ForeignKey(
                        name: "FK_RefreshJob_Job",
                        column: x => x.JobId,
                        principalTable: "Job",
                        principalColumn: "Id");
                });

            migrationBuilder.UpdateData(
                table: "AccountStatus",
                keyColumn: "Id",
                keyValue: 1001,
                column: "CreatedTime",
                value: new DateTime(2023, 9, 29, 11, 10, 46, 259, DateTimeKind.Local).AddTicks(965));

            migrationBuilder.UpdateData(
                table: "AccountStatus",
                keyColumn: "Id",
                keyValue: 1002,
                column: "CreatedTime",
                value: new DateTime(2023, 9, 29, 11, 10, 46, 259, DateTimeKind.Local).AddTicks(1010));

            migrationBuilder.UpdateData(
                table: "AdminAccount",
                keyColumn: "Id",
                keyValue: 1001L,
                column: "CreatedTime",
                value: new DateTime(2023, 9, 29, 11, 10, 46, 259, DateTimeKind.Local).AddTicks(1042));

            migrationBuilder.UpdateData(
                table: "CVTemplateStatus",
                keyColumn: "Id",
                keyValue: 1001L,
                column: "CreatedTime",
                value: new DateTime(2023, 9, 29, 11, 10, 46, 259, DateTimeKind.Local).AddTicks(1142));

            migrationBuilder.UpdateData(
                table: "CVTemplateStatus",
                keyColumn: "Id",
                keyValue: 1002L,
                column: "CreatedTime",
                value: new DateTime(2023, 9, 29, 11, 10, 46, 259, DateTimeKind.Local).AddTicks(1159));

            migrationBuilder.UpdateData(
                table: "CandidateLevel",
                keyColumn: "Id",
                keyValue: 1001,
                column: "CreatedTime",
                value: new DateTime(2023, 9, 29, 11, 10, 46, 259, DateTimeKind.Local).AddTicks(1078));

            migrationBuilder.UpdateData(
                table: "Position",
                keyColumn: "Id",
                keyValue: 1001,
                column: "CreatedTime",
                value: new DateTime(2023, 9, 29, 11, 10, 46, 259, DateTimeKind.Local).AddTicks(1108));

            migrationBuilder.CreateIndex(
                name: "IX_RefreshJob_JobId",
                table: "RefreshJob",
                column: "JobId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "RefreshJob");

            migrationBuilder.DropColumn(
                name: "RefreshDate",
                table: "Job");

            migrationBuilder.UpdateData(
                table: "AccountStatus",
                keyColumn: "Id",
                keyValue: 1001,
                column: "CreatedTime",
                value: new DateTime(2023, 9, 26, 14, 37, 12, 469, DateTimeKind.Local).AddTicks(6231));

            migrationBuilder.UpdateData(
                table: "AccountStatus",
                keyColumn: "Id",
                keyValue: 1002,
                column: "CreatedTime",
                value: new DateTime(2023, 9, 26, 14, 37, 12, 469, DateTimeKind.Local).AddTicks(6290));

            migrationBuilder.UpdateData(
                table: "AdminAccount",
                keyColumn: "Id",
                keyValue: 1001L,
                column: "CreatedTime",
                value: new DateTime(2023, 9, 26, 14, 37, 12, 469, DateTimeKind.Local).AddTicks(6312));

            migrationBuilder.UpdateData(
                table: "CVTemplateStatus",
                keyColumn: "Id",
                keyValue: 1001L,
                column: "CreatedTime",
                value: new DateTime(2023, 9, 26, 14, 37, 12, 469, DateTimeKind.Local).AddTicks(6403));

            migrationBuilder.UpdateData(
                table: "CVTemplateStatus",
                keyColumn: "Id",
                keyValue: 1002L,
                column: "CreatedTime",
                value: new DateTime(2023, 9, 26, 14, 37, 12, 469, DateTimeKind.Local).AddTicks(6415));

            migrationBuilder.UpdateData(
                table: "CandidateLevel",
                keyColumn: "Id",
                keyValue: 1001,
                column: "CreatedTime",
                value: new DateTime(2023, 9, 26, 14, 37, 12, 469, DateTimeKind.Local).AddTicks(6347));

            migrationBuilder.UpdateData(
                table: "Position",
                keyColumn: "Id",
                keyValue: 1001,
                column: "CreatedTime",
                value: new DateTime(2023, 9, 26, 14, 37, 12, 469, DateTimeKind.Local).AddTicks(6372));
        }
    }
}
