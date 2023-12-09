using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BestCV.Infrastructure.Persistence.Migrations
{
    public partial class addIonToJobCategory_v1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Icon",
                table: "JobCategory",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                comment: "Icon của danh mục công việc");

            migrationBuilder.UpdateData(
                table: "AccountStatus",
                keyColumn: "Id",
                keyValue: 1001,
                column: "CreatedTime",
                value: new DateTime(2023, 8, 19, 19, 42, 12, 949, DateTimeKind.Local).AddTicks(2882));

            migrationBuilder.UpdateData(
                table: "AccountStatus",
                keyColumn: "Id",
                keyValue: 1002,
                column: "CreatedTime",
                value: new DateTime(2023, 8, 19, 19, 42, 12, 949, DateTimeKind.Local).AddTicks(2914));

            migrationBuilder.UpdateData(
                table: "AdminAccount",
                keyColumn: "Id",
                keyValue: 1001L,
                column: "CreatedTime",
                value: new DateTime(2023, 8, 19, 19, 42, 12, 949, DateTimeKind.Local).AddTicks(2936));

            migrationBuilder.UpdateData(
                table: "CandidateLevel",
                keyColumn: "Id",
                keyValue: 1001,
                column: "CreatedTime",
                value: new DateTime(2023, 8, 19, 19, 42, 12, 949, DateTimeKind.Local).AddTicks(2974));

            migrationBuilder.UpdateData(
                table: "EmployerServicePackage",
                keyColumn: "Id",
                keyValue: 1001,
                column: "CreatedTime",
                value: new DateTime(2023, 8, 19, 19, 42, 12, 949, DateTimeKind.Local).AddTicks(3032));

            migrationBuilder.UpdateData(
                table: "Position",
                keyColumn: "Id",
                keyValue: 1001,
                column: "CreatedTime",
                value: new DateTime(2023, 8, 19, 19, 42, 12, 949, DateTimeKind.Local).AddTicks(3001));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Icon",
                table: "JobCategory");

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
