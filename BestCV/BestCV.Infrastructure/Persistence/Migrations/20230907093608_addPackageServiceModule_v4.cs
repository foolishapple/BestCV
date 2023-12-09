using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Jobi.Infrastructure.Persistence.Migrations
{
    public partial class addPackageServiceModule_v4 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "EmployerServicePackage",
                keyColumn: "Id",
                keyValue: 1001);

            migrationBuilder.UpdateData(
                table: "AccountStatus",
                keyColumn: "Id",
                keyValue: 1001,
                column: "CreatedTime",
                value: new DateTime(2023, 9, 7, 16, 36, 2, 755, DateTimeKind.Local).AddTicks(7047));

            migrationBuilder.UpdateData(
                table: "AccountStatus",
                keyColumn: "Id",
                keyValue: 1002,
                column: "CreatedTime",
                value: new DateTime(2023, 9, 7, 16, 36, 2, 755, DateTimeKind.Local).AddTicks(7142));

            migrationBuilder.UpdateData(
                table: "AdminAccount",
                keyColumn: "Id",
                keyValue: 1001L,
                column: "CreatedTime",
                value: new DateTime(2023, 9, 7, 16, 36, 2, 755, DateTimeKind.Local).AddTicks(7172));

            migrationBuilder.UpdateData(
                table: "CandidateLevel",
                keyColumn: "Id",
                keyValue: 1001,
                column: "CreatedTime",
                value: new DateTime(2023, 9, 7, 16, 36, 2, 755, DateTimeKind.Local).AddTicks(7221));

            migrationBuilder.UpdateData(
                table: "Position",
                keyColumn: "Id",
                keyValue: 1001,
                column: "CreatedTime",
                value: new DateTime(2023, 9, 7, 16, 36, 2, 755, DateTimeKind.Local).AddTicks(7261));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AccountStatus",
                keyColumn: "Id",
                keyValue: 1001,
                column: "CreatedTime",
                value: new DateTime(2023, 9, 7, 16, 22, 41, 283, DateTimeKind.Local).AddTicks(3852));

            migrationBuilder.UpdateData(
                table: "AccountStatus",
                keyColumn: "Id",
                keyValue: 1002,
                column: "CreatedTime",
                value: new DateTime(2023, 9, 7, 16, 22, 41, 283, DateTimeKind.Local).AddTicks(3931));

            migrationBuilder.UpdateData(
                table: "AdminAccount",
                keyColumn: "Id",
                keyValue: 1001L,
                column: "CreatedTime",
                value: new DateTime(2023, 9, 7, 16, 22, 41, 283, DateTimeKind.Local).AddTicks(3959));

            migrationBuilder.UpdateData(
                table: "CandidateLevel",
                keyColumn: "Id",
                keyValue: 1001,
                column: "CreatedTime",
                value: new DateTime(2023, 9, 7, 16, 22, 41, 283, DateTimeKind.Local).AddTicks(3997));

            migrationBuilder.InsertData(
                table: "EmployerServicePackage",
                columns: new[] { "Id", "Active", "CreatedTime", "Description", "DiscountPrice", "Name", "Price", "ServicePackageGroupId", "ServicePackageTypeId" },
                values: new object[] { 1001, true, new DateTime(2023, 9, 7, 16, 22, 41, 283, DateTimeKind.Local).AddTicks(4060), null, 0, "Thường", 0, null, null });

            migrationBuilder.UpdateData(
                table: "Position",
                keyColumn: "Id",
                keyValue: 1001,
                column: "CreatedTime",
                value: new DateTime(2023, 9, 7, 16, 22, 41, 283, DateTimeKind.Local).AddTicks(4029));
        }
    }
}
