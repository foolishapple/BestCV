using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Jobi.Infrastructure.Persistence.Migrations
{
    public partial class addReferencesJobRequireCityAndWorkPlave_v1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AccountStatus",
                keyColumn: "Id",
                keyValue: 1001,
                column: "CreatedTime",
                value: new DateTime(2023, 8, 10, 14, 15, 20, 896, DateTimeKind.Local).AddTicks(6994));

            migrationBuilder.UpdateData(
                table: "AccountStatus",
                keyColumn: "Id",
                keyValue: 1002,
                column: "CreatedTime",
                value: new DateTime(2023, 8, 10, 14, 15, 20, 896, DateTimeKind.Local).AddTicks(7032));

            migrationBuilder.UpdateData(
                table: "AdminAccount",
                keyColumn: "Id",
                keyValue: 1001L,
                column: "CreatedTime",
                value: new DateTime(2023, 8, 10, 14, 15, 20, 896, DateTimeKind.Local).AddTicks(7053));

            migrationBuilder.UpdateData(
                table: "CandidateLevel",
                keyColumn: "Id",
                keyValue: 1001,
                column: "CreatedTime",
                value: new DateTime(2023, 8, 10, 14, 15, 20, 896, DateTimeKind.Local).AddTicks(7089));

            migrationBuilder.UpdateData(
                table: "EmployerServicePackage",
                keyColumn: "Id",
                keyValue: 1001,
                column: "CreatedTime",
                value: new DateTime(2023, 8, 10, 14, 15, 20, 896, DateTimeKind.Local).AddTicks(7136));

            migrationBuilder.UpdateData(
                table: "Position",
                keyColumn: "Id",
                keyValue: 1001,
                column: "CreatedTime",
                value: new DateTime(2023, 8, 10, 14, 15, 20, 896, DateTimeKind.Local).AddTicks(7110));

            migrationBuilder.CreateIndex(
                name: "IX_JobRequireCity_CityId",
                table: "JobRequireCity",
                column: "CityId");

            migrationBuilder.AddForeignKey(
                name: "FK_JobRequireCity_WorkPlace",
                table: "JobRequireCity",
                column: "CityId",
                principalTable: "WorkPlace",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_JobRequireCity_WorkPlace",
                table: "JobRequireCity");

            migrationBuilder.DropIndex(
                name: "IX_JobRequireCity_CityId",
                table: "JobRequireCity");

            migrationBuilder.UpdateData(
                table: "AccountStatus",
                keyColumn: "Id",
                keyValue: 1001,
                column: "CreatedTime",
                value: new DateTime(2023, 8, 9, 18, 44, 25, 387, DateTimeKind.Local).AddTicks(4663));

            migrationBuilder.UpdateData(
                table: "AccountStatus",
                keyColumn: "Id",
                keyValue: 1002,
                column: "CreatedTime",
                value: new DateTime(2023, 8, 9, 18, 44, 25, 387, DateTimeKind.Local).AddTicks(4711));

            migrationBuilder.UpdateData(
                table: "AdminAccount",
                keyColumn: "Id",
                keyValue: 1001L,
                column: "CreatedTime",
                value: new DateTime(2023, 8, 9, 18, 44, 25, 387, DateTimeKind.Local).AddTicks(4730));

            migrationBuilder.UpdateData(
                table: "CandidateLevel",
                keyColumn: "Id",
                keyValue: 1001,
                column: "CreatedTime",
                value: new DateTime(2023, 8, 9, 18, 44, 25, 387, DateTimeKind.Local).AddTicks(4768));

            migrationBuilder.UpdateData(
                table: "EmployerServicePackage",
                keyColumn: "Id",
                keyValue: 1001,
                column: "CreatedTime",
                value: new DateTime(2023, 8, 9, 18, 44, 25, 387, DateTimeKind.Local).AddTicks(4822));

            migrationBuilder.UpdateData(
                table: "Position",
                keyColumn: "Id",
                keyValue: 1001,
                column: "CreatedTime",
                value: new DateTime(2023, 8, 9, 18, 44, 25, 387, DateTimeKind.Local).AddTicks(4792));
        }
    }
}
