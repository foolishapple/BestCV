using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Jobi.Infrastructure.Persistence.Migrations
{
    public partial class updateJobSecondaryPositisionToJobSecondaryJobCategory_v2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "PrimaryJobCategoryId",
                table: "Job",
                type: "int",
                nullable: false,
                comment: "Mã vị trí ngành nghề tuyển dụng chính",
                oldClrType: typeof(int),
                oldType: "int",
                oldDefaultValue: 1001,
                oldComment: "Mã vị trí ngành nghề tuyển dụng chính");

            migrationBuilder.AlterColumn<int>(
                name: "JobPositionId",
                table: "Job",
                type: "int",
                nullable: false,
                comment: "Mã vị trí tuyển dụng",
                oldClrType: typeof(int),
                oldType: "int",
                oldDefaultValue: 1001,
                oldComment: "Mã vị trí tuyển dụng");

            migrationBuilder.UpdateData(
                table: "AccountStatus",
                keyColumn: "Id",
                keyValue: 1001,
                column: "CreatedTime",
                value: new DateTime(2023, 10, 27, 11, 9, 15, 263, DateTimeKind.Local).AddTicks(8828));

            migrationBuilder.UpdateData(
                table: "AccountStatus",
                keyColumn: "Id",
                keyValue: 1002,
                column: "CreatedTime",
                value: new DateTime(2023, 10, 27, 11, 9, 15, 263, DateTimeKind.Local).AddTicks(8871));

            migrationBuilder.UpdateData(
                table: "AdminAccount",
                keyColumn: "Id",
                keyValue: 1001L,
                column: "CreatedTime",
                value: new DateTime(2023, 10, 27, 11, 9, 15, 263, DateTimeKind.Local).AddTicks(8897));

            migrationBuilder.UpdateData(
                table: "CVTemplateStatus",
                keyColumn: "Id",
                keyValue: 1001L,
                column: "CreatedTime",
                value: new DateTime(2023, 10, 27, 11, 9, 15, 263, DateTimeKind.Local).AddTicks(8992));

            migrationBuilder.UpdateData(
                table: "CVTemplateStatus",
                keyColumn: "Id",
                keyValue: 1002L,
                column: "CreatedTime",
                value: new DateTime(2023, 10, 27, 11, 9, 15, 263, DateTimeKind.Local).AddTicks(9002));

            migrationBuilder.UpdateData(
                table: "CandidateLevel",
                keyColumn: "Id",
                keyValue: 1001,
                column: "CreatedTime",
                value: new DateTime(2023, 10, 27, 11, 9, 15, 263, DateTimeKind.Local).AddTicks(8933));

            migrationBuilder.UpdateData(
                table: "Position",
                keyColumn: "Id",
                keyValue: 1001,
                column: "CreatedTime",
                value: new DateTime(2023, 10, 27, 11, 9, 15, 263, DateTimeKind.Local).AddTicks(8964));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "PrimaryJobCategoryId",
                table: "Job",
                type: "int",
                nullable: false,
                defaultValue: 1001,
                comment: "Mã vị trí ngành nghề tuyển dụng chính",
                oldClrType: typeof(int),
                oldType: "int",
                oldComment: "Mã vị trí ngành nghề tuyển dụng chính");

            migrationBuilder.AlterColumn<int>(
                name: "JobPositionId",
                table: "Job",
                type: "int",
                nullable: false,
                defaultValue: 1001,
                comment: "Mã vị trí tuyển dụng",
                oldClrType: typeof(int),
                oldType: "int",
                oldComment: "Mã vị trí tuyển dụng");

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
        }
    }
}
