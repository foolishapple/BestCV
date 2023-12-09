using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BestCV.Infrastructure.Persistence.Migrations
{
    public partial class add_relationship_between_Job_and_Tag_v1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "JobTagId",
                table: "Job",
                type: "int",
                nullable: false,
                defaultValue: 0,
                comment: "Mã thẻ");

            migrationBuilder.CreateTable(
                name: "JobTag",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1001, 1"),
                    TagId = table.Column<int>(type: "int", nullable: false, comment: "Mã thẻ"),
                    JobId = table.Column<long>(type: "bigint", nullable: false, comment: "Mã tin tuyển dụng"),
                    Active = table.Column<bool>(type: "bit", nullable: false, defaultValueSql: "((1))", comment: "Đánh dấu bị xóa"),
                    CreatedTime = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "(getdate())", comment: "Ngày tạo")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_JobTag", x => x.Id);
                    table.ForeignKey(
                        name: "FK_JobTag_Job",
                        column: x => x.JobId,
                        principalTable: "Job",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_JobTag_Tag",
                        column: x => x.TagId,
                        principalTable: "Tag",
                        principalColumn: "Id");
                });

            migrationBuilder.UpdateData(
                table: "AccountStatus",
                keyColumn: "Id",
                keyValue: 1001,
                column: "CreatedTime",
                value: new DateTime(2023, 8, 18, 9, 33, 52, 18, DateTimeKind.Local).AddTicks(8200));

            migrationBuilder.UpdateData(
                table: "AccountStatus",
                keyColumn: "Id",
                keyValue: 1002,
                column: "CreatedTime",
                value: new DateTime(2023, 8, 18, 9, 33, 52, 18, DateTimeKind.Local).AddTicks(8244));

            migrationBuilder.UpdateData(
                table: "AdminAccount",
                keyColumn: "Id",
                keyValue: 1001L,
                column: "CreatedTime",
                value: new DateTime(2023, 8, 18, 9, 33, 52, 18, DateTimeKind.Local).AddTicks(8266));

            migrationBuilder.UpdateData(
                table: "CandidateLevel",
                keyColumn: "Id",
                keyValue: 1001,
                column: "CreatedTime",
                value: new DateTime(2023, 8, 18, 9, 33, 52, 18, DateTimeKind.Local).AddTicks(8300));

            migrationBuilder.UpdateData(
                table: "EmployerServicePackage",
                keyColumn: "Id",
                keyValue: 1001,
                column: "CreatedTime",
                value: new DateTime(2023, 8, 18, 9, 33, 52, 18, DateTimeKind.Local).AddTicks(8354));

            migrationBuilder.UpdateData(
                table: "Position",
                keyColumn: "Id",
                keyValue: 1001,
                column: "CreatedTime",
                value: new DateTime(2023, 8, 18, 9, 33, 52, 18, DateTimeKind.Local).AddTicks(8326));

            migrationBuilder.CreateIndex(
                name: "IX_JobTag_JobId",
                table: "JobTag",
                column: "JobId");

            migrationBuilder.CreateIndex(
                name: "IX_JobTag_TagId",
                table: "JobTag",
                column: "TagId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "JobTag");

            migrationBuilder.DropColumn(
                name: "JobTagId",
                table: "Job");

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
