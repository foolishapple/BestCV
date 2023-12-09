using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BestCV.Infrastructure.Persistence.Migrations
{
    public partial class add_workplaceId_to_company : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "WorkPlaceId",
                table: "Company",
                type: "int",
                nullable: true,
                comment: "Mã nơi làm việc");

            migrationBuilder.UpdateData(
                table: "AccountStatus",
                keyColumn: "Id",
                keyValue: 1001,
                column: "CreatedTime",
                value: new DateTime(2023, 8, 14, 23, 35, 29, 178, DateTimeKind.Local).AddTicks(8642));

            migrationBuilder.UpdateData(
                table: "AccountStatus",
                keyColumn: "Id",
                keyValue: 1002,
                column: "CreatedTime",
                value: new DateTime(2023, 8, 14, 23, 35, 29, 178, DateTimeKind.Local).AddTicks(8678));

            migrationBuilder.UpdateData(
                table: "AdminAccount",
                keyColumn: "Id",
                keyValue: 1001L,
                column: "CreatedTime",
                value: new DateTime(2023, 8, 14, 23, 35, 29, 178, DateTimeKind.Local).AddTicks(8701));

            migrationBuilder.UpdateData(
                table: "CandidateLevel",
                keyColumn: "Id",
                keyValue: 1001,
                column: "CreatedTime",
                value: new DateTime(2023, 8, 14, 23, 35, 29, 178, DateTimeKind.Local).AddTicks(8742));

            migrationBuilder.UpdateData(
                table: "EmployerServicePackage",
                keyColumn: "Id",
                keyValue: 1001,
                column: "CreatedTime",
                value: new DateTime(2023, 8, 14, 23, 35, 29, 178, DateTimeKind.Local).AddTicks(8800));

            migrationBuilder.UpdateData(
                table: "Position",
                keyColumn: "Id",
                keyValue: 1001,
                column: "CreatedTime",
                value: new DateTime(2023, 8, 14, 23, 35, 29, 178, DateTimeKind.Local).AddTicks(8762));

            migrationBuilder.CreateIndex(
                name: "IX_Company_WorkPlaceId",
                table: "Company",
                column: "WorkPlaceId");

            migrationBuilder.AddForeignKey(
                name: "FK_Company_WorkPlace_WorkPlaceId",
                table: "Company",
                column: "WorkPlaceId",
                principalTable: "WorkPlace",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Company_WorkPlace_WorkPlaceId",
                table: "Company");

            migrationBuilder.DropIndex(
                name: "IX_Company_WorkPlaceId",
                table: "Company");

            migrationBuilder.DropColumn(
                name: "WorkPlaceId",
                table: "Company");

            migrationBuilder.UpdateData(
                table: "AccountStatus",
                keyColumn: "Id",
                keyValue: 1001,
                column: "CreatedTime",
                value: new DateTime(2023, 8, 11, 11, 39, 5, 729, DateTimeKind.Local).AddTicks(5294));

            migrationBuilder.UpdateData(
                table: "AccountStatus",
                keyColumn: "Id",
                keyValue: 1002,
                column: "CreatedTime",
                value: new DateTime(2023, 8, 11, 11, 39, 5, 729, DateTimeKind.Local).AddTicks(5395));

            migrationBuilder.UpdateData(
                table: "AdminAccount",
                keyColumn: "Id",
                keyValue: 1001L,
                column: "CreatedTime",
                value: new DateTime(2023, 8, 11, 11, 39, 5, 729, DateTimeKind.Local).AddTicks(5435));

            migrationBuilder.UpdateData(
                table: "CandidateLevel",
                keyColumn: "Id",
                keyValue: 1001,
                column: "CreatedTime",
                value: new DateTime(2023, 8, 11, 11, 39, 5, 729, DateTimeKind.Local).AddTicks(5483));

            migrationBuilder.UpdateData(
                table: "EmployerServicePackage",
                keyColumn: "Id",
                keyValue: 1001,
                column: "CreatedTime",
                value: new DateTime(2023, 8, 11, 11, 39, 5, 729, DateTimeKind.Local).AddTicks(5559));

            migrationBuilder.UpdateData(
                table: "Position",
                keyColumn: "Id",
                keyValue: 1001,
                column: "CreatedTime",
                value: new DateTime(2023, 8, 11, 11, 39, 5, 729, DateTimeKind.Local).AddTicks(5519));
        }
    }
}
