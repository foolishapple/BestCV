using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BestCV.Infrastructure.Persistence.Migrations
{
    public partial class Add_AdditionalCSS_to_CandidateCV_and_remove_OrderSort_in_CVTemplate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "OrderSort",
                table: "CVTemplate");

            migrationBuilder.UpdateData(
                table: "AccountStatus",
                keyColumn: "Id",
                keyValue: 1001,
                column: "CreatedTime",
                value: new DateTime(2023, 9, 11, 15, 36, 6, 681, DateTimeKind.Local).AddTicks(8502));

            migrationBuilder.UpdateData(
                table: "AccountStatus",
                keyColumn: "Id",
                keyValue: 1002,
                column: "CreatedTime",
                value: new DateTime(2023, 9, 11, 15, 36, 6, 681, DateTimeKind.Local).AddTicks(8551));

            migrationBuilder.UpdateData(
                table: "AdminAccount",
                keyColumn: "Id",
                keyValue: 1001L,
                column: "CreatedTime",
                value: new DateTime(2023, 9, 11, 15, 36, 6, 681, DateTimeKind.Local).AddTicks(8580));

            migrationBuilder.UpdateData(
                table: "CVTemplateStatus",
                keyColumn: "Id",
                keyValue: 1001L,
                column: "CreatedTime",
                value: new DateTime(2023, 9, 11, 15, 36, 6, 681, DateTimeKind.Local).AddTicks(8680));

            migrationBuilder.UpdateData(
                table: "CVTemplateStatus",
                keyColumn: "Id",
                keyValue: 1002L,
                column: "CreatedTime",
                value: new DateTime(2023, 9, 11, 15, 36, 6, 681, DateTimeKind.Local).AddTicks(8694));

            migrationBuilder.UpdateData(
                table: "CandidateLevel",
                keyColumn: "Id",
                keyValue: 1001,
                column: "CreatedTime",
                value: new DateTime(2023, 9, 11, 15, 36, 6, 681, DateTimeKind.Local).AddTicks(8620));

            migrationBuilder.UpdateData(
                table: "Position",
                keyColumn: "Id",
                keyValue: 1001,
                column: "CreatedTime",
                value: new DateTime(2023, 9, 11, 15, 36, 6, 681, DateTimeKind.Local).AddTicks(8652));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "OrderSort",
                table: "CVTemplate",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                comment: "Sắp xếp");

            migrationBuilder.UpdateData(
                table: "AccountStatus",
                keyColumn: "Id",
                keyValue: 1001,
                column: "CreatedTime",
                value: new DateTime(2023, 9, 11, 15, 18, 11, 65, DateTimeKind.Local).AddTicks(67));

            migrationBuilder.UpdateData(
                table: "AccountStatus",
                keyColumn: "Id",
                keyValue: 1002,
                column: "CreatedTime",
                value: new DateTime(2023, 9, 11, 15, 18, 11, 65, DateTimeKind.Local).AddTicks(107));

            migrationBuilder.UpdateData(
                table: "AdminAccount",
                keyColumn: "Id",
                keyValue: 1001L,
                column: "CreatedTime",
                value: new DateTime(2023, 9, 11, 15, 18, 11, 65, DateTimeKind.Local).AddTicks(138));

            migrationBuilder.UpdateData(
                table: "CVTemplateStatus",
                keyColumn: "Id",
                keyValue: 1001L,
                column: "CreatedTime",
                value: new DateTime(2023, 9, 11, 15, 18, 11, 65, DateTimeKind.Local).AddTicks(237));

            migrationBuilder.UpdateData(
                table: "CVTemplateStatus",
                keyColumn: "Id",
                keyValue: 1002L,
                column: "CreatedTime",
                value: new DateTime(2023, 9, 11, 15, 18, 11, 65, DateTimeKind.Local).AddTicks(250));

            migrationBuilder.UpdateData(
                table: "CandidateLevel",
                keyColumn: "Id",
                keyValue: 1001,
                column: "CreatedTime",
                value: new DateTime(2023, 9, 11, 15, 18, 11, 65, DateTimeKind.Local).AddTicks(173));

            migrationBuilder.UpdateData(
                table: "Position",
                keyColumn: "Id",
                keyValue: 1001,
                column: "CreatedTime",
                value: new DateTime(2023, 9, 11, 15, 18, 11, 65, DateTimeKind.Local).AddTicks(202));
        }
    }
}
