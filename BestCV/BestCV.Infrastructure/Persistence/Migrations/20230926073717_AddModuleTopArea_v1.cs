using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Jobi.Infrastructure.Persistence.Migrations
{
    public partial class AddModuleTopArea_v1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "TopAreaJob",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false, comment: "Mã")
                        .Annotation("SqlServer:Identity", "1001, 1"),
                    JobId = table.Column<long>(type: "bigint", nullable: false),
                    OrderSort = table.Column<int>(type: "int", nullable: false, comment: "Thứ tự sắp xếp"),
                    SubOrderSort = table.Column<int>(type: "int", nullable: false, comment: "Thứ tự sắp xếp phụ"),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Active = table.Column<bool>(type: "bit", nullable: false, defaultValue: true, comment: "Đánh dấu đã bị xóa"),
                    CreatedTime = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "(getdate())", comment: "Ngày tạo")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TopAreaJob", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TopAreaJob_Job",
                        column: x => x.JobId,
                        principalTable: "Job",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

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

            migrationBuilder.CreateIndex(
                name: "IX_TopAreaJob_JobId",
                table: "TopAreaJob",
                column: "JobId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "TopAreaJob");

            migrationBuilder.UpdateData(
                table: "AccountStatus",
                keyColumn: "Id",
                keyValue: 1001,
                column: "CreatedTime",
                value: new DateTime(2023, 9, 26, 10, 47, 45, 88, DateTimeKind.Local).AddTicks(7647));

            migrationBuilder.UpdateData(
                table: "AccountStatus",
                keyColumn: "Id",
                keyValue: 1002,
                column: "CreatedTime",
                value: new DateTime(2023, 9, 26, 10, 47, 45, 88, DateTimeKind.Local).AddTicks(7738));

            migrationBuilder.UpdateData(
                table: "AdminAccount",
                keyColumn: "Id",
                keyValue: 1001L,
                column: "CreatedTime",
                value: new DateTime(2023, 9, 26, 10, 47, 45, 88, DateTimeKind.Local).AddTicks(7764));

            migrationBuilder.UpdateData(
                table: "CVTemplateStatus",
                keyColumn: "Id",
                keyValue: 1001L,
                column: "CreatedTime",
                value: new DateTime(2023, 9, 26, 10, 47, 45, 88, DateTimeKind.Local).AddTicks(7875));

            migrationBuilder.UpdateData(
                table: "CVTemplateStatus",
                keyColumn: "Id",
                keyValue: 1002L,
                column: "CreatedTime",
                value: new DateTime(2023, 9, 26, 10, 47, 45, 88, DateTimeKind.Local).AddTicks(7887));

            migrationBuilder.UpdateData(
                table: "CandidateLevel",
                keyColumn: "Id",
                keyValue: 1001,
                column: "CreatedTime",
                value: new DateTime(2023, 9, 26, 10, 47, 45, 88, DateTimeKind.Local).AddTicks(7803));

            migrationBuilder.UpdateData(
                table: "Position",
                keyColumn: "Id",
                keyValue: 1001,
                column: "CreatedTime",
                value: new DateTime(2023, 9, 26, 10, 47, 45, 88, DateTimeKind.Local).AddTicks(7845));
        }
    }
}
