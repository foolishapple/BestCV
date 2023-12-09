using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BestCV.Infrastructure.Persistence.Migrations
{
    public partial class AddseedforCVTemplateStatus : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AccountStatus",
                keyColumn: "Id",
                keyValue: 1001,
                column: "CreatedTime",
                value: new DateTime(2023, 8, 22, 14, 47, 26, 98, DateTimeKind.Local).AddTicks(2953));

            migrationBuilder.UpdateData(
                table: "AccountStatus",
                keyColumn: "Id",
                keyValue: 1002,
                column: "CreatedTime",
                value: new DateTime(2023, 8, 22, 14, 47, 26, 98, DateTimeKind.Local).AddTicks(3026));

            migrationBuilder.UpdateData(
                table: "AdminAccount",
                keyColumn: "Id",
                keyValue: 1001L,
                column: "CreatedTime",
                value: new DateTime(2023, 8, 22, 14, 47, 26, 98, DateTimeKind.Local).AddTicks(3051));

            migrationBuilder.InsertData(
                table: "CVTemplateStatus",
                columns: new[] { "Id", "Active", "CreatedTime", "Description", "Name" },
                values: new object[,]
                {
                    { 1001L, true, new DateTime(2023, 8, 22, 14, 47, 26, 98, DateTimeKind.Local).AddTicks(3173), "Template đi vào hoạt động chính thức", "Publish" },
                    { 1002L, true, new DateTime(2023, 8, 22, 14, 47, 26, 98, DateTimeKind.Local).AddTicks(3185), "Template nháp. Vì một lý do nào đó nên chưa được đưa vào chính thức", "Draft" }
                });

            migrationBuilder.UpdateData(
                table: "CandidateLevel",
                keyColumn: "Id",
                keyValue: 1001,
                column: "CreatedTime",
                value: new DateTime(2023, 8, 22, 14, 47, 26, 98, DateTimeKind.Local).AddTicks(3085));

            migrationBuilder.UpdateData(
                table: "EmployerServicePackage",
                keyColumn: "Id",
                keyValue: 1001,
                column: "CreatedTime",
                value: new DateTime(2023, 8, 22, 14, 47, 26, 98, DateTimeKind.Local).AddTicks(3144));

            migrationBuilder.UpdateData(
                table: "Position",
                keyColumn: "Id",
                keyValue: 1001,
                column: "CreatedTime",
                value: new DateTime(2023, 8, 22, 14, 47, 26, 98, DateTimeKind.Local).AddTicks(3116));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "CVTemplateStatus",
                keyColumn: "Id",
                keyValue: 1001L);

            migrationBuilder.DeleteData(
                table: "CVTemplateStatus",
                keyColumn: "Id",
                keyValue: 1002L);

            migrationBuilder.UpdateData(
                table: "AccountStatus",
                keyColumn: "Id",
                keyValue: 1001,
                column: "CreatedTime",
                value: new DateTime(2023, 8, 22, 14, 27, 39, 480, DateTimeKind.Local).AddTicks(4562));

            migrationBuilder.UpdateData(
                table: "AccountStatus",
                keyColumn: "Id",
                keyValue: 1002,
                column: "CreatedTime",
                value: new DateTime(2023, 8, 22, 14, 27, 39, 480, DateTimeKind.Local).AddTicks(4610));

            migrationBuilder.UpdateData(
                table: "AdminAccount",
                keyColumn: "Id",
                keyValue: 1001L,
                column: "CreatedTime",
                value: new DateTime(2023, 8, 22, 14, 27, 39, 480, DateTimeKind.Local).AddTicks(4637));

            migrationBuilder.UpdateData(
                table: "CandidateLevel",
                keyColumn: "Id",
                keyValue: 1001,
                column: "CreatedTime",
                value: new DateTime(2023, 8, 22, 14, 27, 39, 480, DateTimeKind.Local).AddTicks(4678));

            migrationBuilder.UpdateData(
                table: "EmployerServicePackage",
                keyColumn: "Id",
                keyValue: 1001,
                column: "CreatedTime",
                value: new DateTime(2023, 8, 22, 14, 27, 39, 480, DateTimeKind.Local).AddTicks(4739));

            migrationBuilder.UpdateData(
                table: "Position",
                keyColumn: "Id",
                keyValue: 1001,
                column: "CreatedTime",
                value: new DateTime(2023, 8, 22, 14, 27, 39, 480, DateTimeKind.Local).AddTicks(4707));
        }
    }
}
