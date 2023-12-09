using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BestCV.Infrastructure.Persistence.Migrations
{
    public partial class EmployerOrder_ver2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_TopJobUrgent_OrderSort_SubOrderSort",
                table: "TopJobUrgent");

            migrationBuilder.DropIndex(
                name: "IX_TopJobExtra_OrderSort_SubOrderSort",
                table: "TopJobExtra");

            migrationBuilder.DropIndex(
                name: "IX_TopFeatureJob_OrderSort_SubOrderSort",
                table: "TopFeatureJob");

            migrationBuilder.UpdateData(
                table: "AccountStatus",
                keyColumn: "Id",
                keyValue: 1001,
                column: "CreatedTime",
                value: new DateTime(2023, 9, 11, 11, 40, 58, 879, DateTimeKind.Local).AddTicks(6467));

            migrationBuilder.UpdateData(
                table: "AccountStatus",
                keyColumn: "Id",
                keyValue: 1002,
                column: "CreatedTime",
                value: new DateTime(2023, 9, 11, 11, 40, 58, 879, DateTimeKind.Local).AddTicks(6516));

            migrationBuilder.UpdateData(
                table: "AdminAccount",
                keyColumn: "Id",
                keyValue: 1001L,
                column: "CreatedTime",
                value: new DateTime(2023, 9, 11, 11, 40, 58, 879, DateTimeKind.Local).AddTicks(6553));

            migrationBuilder.UpdateData(
                table: "CandidateLevel",
                keyColumn: "Id",
                keyValue: 1001,
                column: "CreatedTime",
                value: new DateTime(2023, 9, 11, 11, 40, 58, 879, DateTimeKind.Local).AddTicks(6591));

            migrationBuilder.UpdateData(
                table: "Position",
                keyColumn: "Id",
                keyValue: 1001,
                column: "CreatedTime",
                value: new DateTime(2023, 9, 11, 11, 40, 58, 879, DateTimeKind.Local).AddTicks(6619));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AccountStatus",
                keyColumn: "Id",
                keyValue: 1001,
                column: "CreatedTime",
                value: new DateTime(2023, 9, 11, 11, 31, 36, 460, DateTimeKind.Local).AddTicks(4833));

            migrationBuilder.UpdateData(
                table: "AccountStatus",
                keyColumn: "Id",
                keyValue: 1002,
                column: "CreatedTime",
                value: new DateTime(2023, 9, 11, 11, 31, 36, 460, DateTimeKind.Local).AddTicks(4876));

            migrationBuilder.UpdateData(
                table: "AdminAccount",
                keyColumn: "Id",
                keyValue: 1001L,
                column: "CreatedTime",
                value: new DateTime(2023, 9, 11, 11, 31, 36, 460, DateTimeKind.Local).AddTicks(4900));

            migrationBuilder.UpdateData(
                table: "CandidateLevel",
                keyColumn: "Id",
                keyValue: 1001,
                column: "CreatedTime",
                value: new DateTime(2023, 9, 11, 11, 31, 36, 460, DateTimeKind.Local).AddTicks(4936));

            migrationBuilder.UpdateData(
                table: "Position",
                keyColumn: "Id",
                keyValue: 1001,
                column: "CreatedTime",
                value: new DateTime(2023, 9, 11, 11, 31, 36, 460, DateTimeKind.Local).AddTicks(4964));

            migrationBuilder.CreateIndex(
                name: "IX_TopJobUrgent_OrderSort_SubOrderSort",
                table: "TopJobUrgent",
                columns: new[] { "OrderSort", "SubOrderSort" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_TopJobExtra_OrderSort_SubOrderSort",
                table: "TopJobExtra",
                columns: new[] { "OrderSort", "SubOrderSort" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_TopFeatureJob_OrderSort_SubOrderSort",
                table: "TopFeatureJob",
                columns: new[] { "OrderSort", "SubOrderSort" },
                unique: true);
        }
    }
}
