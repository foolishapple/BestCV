using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BestCV.Infrastructure.Persistence.Migrations
{
    public partial class Remove_Unique_Color_in_JobStatus : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_JobStatus_Color",
                table: "JobStatus");

            migrationBuilder.UpdateData(
                table: "AccountStatus",
                keyColumn: "Id",
                keyValue: 1001,
                column: "CreatedTime",
                value: new DateTime(2023, 8, 31, 15, 3, 2, 737, DateTimeKind.Local).AddTicks(2898));

            migrationBuilder.UpdateData(
                table: "AccountStatus",
                keyColumn: "Id",
                keyValue: 1002,
                column: "CreatedTime",
                value: new DateTime(2023, 8, 31, 15, 3, 2, 737, DateTimeKind.Local).AddTicks(2936));

            migrationBuilder.UpdateData(
                table: "AdminAccount",
                keyColumn: "Id",
                keyValue: 1001L,
                column: "CreatedTime",
                value: new DateTime(2023, 8, 31, 15, 3, 2, 737, DateTimeKind.Local).AddTicks(2964));

            migrationBuilder.UpdateData(
                table: "CandidateLevel",
                keyColumn: "Id",
                keyValue: 1001,
                column: "CreatedTime",
                value: new DateTime(2023, 8, 31, 15, 3, 2, 737, DateTimeKind.Local).AddTicks(3004));

            migrationBuilder.UpdateData(
                table: "EmployerServicePackage",
                keyColumn: "Id",
                keyValue: 1001,
                column: "CreatedTime",
                value: new DateTime(2023, 8, 31, 15, 3, 2, 737, DateTimeKind.Local).AddTicks(3067));

            migrationBuilder.UpdateData(
                table: "Position",
                keyColumn: "Id",
                keyValue: 1001,
                column: "CreatedTime",
                value: new DateTime(2023, 8, 31, 15, 3, 2, 737, DateTimeKind.Local).AddTicks(3032));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AccountStatus",
                keyColumn: "Id",
                keyValue: 1001,
                column: "CreatedTime",
                value: new DateTime(2023, 8, 29, 17, 24, 29, 134, DateTimeKind.Local).AddTicks(7531));

            migrationBuilder.UpdateData(
                table: "AccountStatus",
                keyColumn: "Id",
                keyValue: 1002,
                column: "CreatedTime",
                value: new DateTime(2023, 8, 29, 17, 24, 29, 134, DateTimeKind.Local).AddTicks(7730));

            migrationBuilder.UpdateData(
                table: "AdminAccount",
                keyColumn: "Id",
                keyValue: 1001L,
                column: "CreatedTime",
                value: new DateTime(2023, 8, 29, 17, 24, 29, 134, DateTimeKind.Local).AddTicks(7838));

            migrationBuilder.UpdateData(
                table: "CandidateLevel",
                keyColumn: "Id",
                keyValue: 1001,
                column: "CreatedTime",
                value: new DateTime(2023, 8, 29, 17, 24, 29, 134, DateTimeKind.Local).AddTicks(7932));

            migrationBuilder.UpdateData(
                table: "EmployerServicePackage",
                keyColumn: "Id",
                keyValue: 1001,
                column: "CreatedTime",
                value: new DateTime(2023, 8, 29, 17, 24, 29, 134, DateTimeKind.Local).AddTicks(8291));

            migrationBuilder.UpdateData(
                table: "Position",
                keyColumn: "Id",
                keyValue: 1001,
                column: "CreatedTime",
                value: new DateTime(2023, 8, 29, 17, 24, 29, 134, DateTimeKind.Local).AddTicks(8200));

            migrationBuilder.CreateIndex(
                name: "IX_JobStatus_Color",
                table: "JobStatus",
                column: "Color",
                unique: true);
        }
    }
}
