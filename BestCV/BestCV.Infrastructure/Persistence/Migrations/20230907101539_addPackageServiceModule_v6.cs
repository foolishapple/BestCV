using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BestCV.Infrastructure.Persistence.Migrations
{
    public partial class addPackageServiceModule_v6 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "SubOrderSort",
                table: "TopJobUrgent",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "SubOrderSort",
                table: "TopJobExtra",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "SubOrderSort",
                table: "TopFeatureJob",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.UpdateData(
                table: "AccountStatus",
                keyColumn: "Id",
                keyValue: 1001,
                column: "CreatedTime",
                value: new DateTime(2023, 9, 7, 17, 15, 35, 84, DateTimeKind.Local).AddTicks(8889));

            migrationBuilder.UpdateData(
                table: "AccountStatus",
                keyColumn: "Id",
                keyValue: 1002,
                column: "CreatedTime",
                value: new DateTime(2023, 9, 7, 17, 15, 35, 84, DateTimeKind.Local).AddTicks(8965));

            migrationBuilder.UpdateData(
                table: "AdminAccount",
                keyColumn: "Id",
                keyValue: 1001L,
                column: "CreatedTime",
                value: new DateTime(2023, 9, 7, 17, 15, 35, 84, DateTimeKind.Local).AddTicks(8993));

            migrationBuilder.UpdateData(
                table: "CandidateLevel",
                keyColumn: "Id",
                keyValue: 1001,
                column: "CreatedTime",
                value: new DateTime(2023, 9, 7, 17, 15, 35, 84, DateTimeKind.Local).AddTicks(9033));

            migrationBuilder.UpdateData(
                table: "Position",
                keyColumn: "Id",
                keyValue: 1001,
                column: "CreatedTime",
                value: new DateTime(2023, 9, 7, 17, 15, 35, 84, DateTimeKind.Local).AddTicks(9070));

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

        protected override void Down(MigrationBuilder migrationBuilder)
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

            migrationBuilder.DropColumn(
                name: "SubOrderSort",
                table: "TopJobUrgent");

            migrationBuilder.DropColumn(
                name: "SubOrderSort",
                table: "TopJobExtra");

            migrationBuilder.DropColumn(
                name: "SubOrderSort",
                table: "TopFeatureJob");

            migrationBuilder.UpdateData(
                table: "AccountStatus",
                keyColumn: "Id",
                keyValue: 1001,
                column: "CreatedTime",
                value: new DateTime(2023, 9, 7, 16, 40, 11, 538, DateTimeKind.Local).AddTicks(9001));

            migrationBuilder.UpdateData(
                table: "AccountStatus",
                keyColumn: "Id",
                keyValue: 1002,
                column: "CreatedTime",
                value: new DateTime(2023, 9, 7, 16, 40, 11, 538, DateTimeKind.Local).AddTicks(9082));

            migrationBuilder.UpdateData(
                table: "AdminAccount",
                keyColumn: "Id",
                keyValue: 1001L,
                column: "CreatedTime",
                value: new DateTime(2023, 9, 7, 16, 40, 11, 538, DateTimeKind.Local).AddTicks(9109));

            migrationBuilder.UpdateData(
                table: "CandidateLevel",
                keyColumn: "Id",
                keyValue: 1001,
                column: "CreatedTime",
                value: new DateTime(2023, 9, 7, 16, 40, 11, 538, DateTimeKind.Local).AddTicks(9150));

            migrationBuilder.UpdateData(
                table: "Position",
                keyColumn: "Id",
                keyValue: 1001,
                column: "CreatedTime",
                value: new DateTime(2023, 9, 7, 16, 40, 11, 538, DateTimeKind.Local).AddTicks(9184));
        }
    }
}
