using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BestCV.Infrastructure.Persistence.Migrations
{
    public partial class delete_unique_key_Table_employerMeta : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_EmployerMeta_Key",
                table: "EmployerMeta");

            migrationBuilder.UpdateData(
                table: "AccountStatus",
                keyColumn: "Id",
                keyValue: 1001,
                column: "CreatedTime",
                value: new DateTime(2023, 7, 27, 17, 29, 38, 937, DateTimeKind.Local).AddTicks(2606));

            migrationBuilder.UpdateData(
                table: "AccountStatus",
                keyColumn: "Id",
                keyValue: 1002,
                column: "CreatedTime",
                value: new DateTime(2023, 7, 27, 17, 29, 38, 937, DateTimeKind.Local).AddTicks(2687));

            migrationBuilder.UpdateData(
                table: "AdminAccount",
                keyColumn: "Id",
                keyValue: 1001L,
                column: "CreatedTime",
                value: new DateTime(2023, 7, 27, 17, 29, 38, 937, DateTimeKind.Local).AddTicks(2707));

            migrationBuilder.UpdateData(
                table: "CandidateLevel",
                keyColumn: "Id",
                keyValue: 1001,
                column: "CreatedTime",
                value: new DateTime(2023, 7, 27, 17, 29, 38, 937, DateTimeKind.Local).AddTicks(2745));

            migrationBuilder.UpdateData(
                table: "EmployerServicePackage",
                keyColumn: "Id",
                keyValue: 1001,
                column: "CreatedTime",
                value: new DateTime(2023, 7, 27, 17, 29, 38, 937, DateTimeKind.Local).AddTicks(2819));

            migrationBuilder.UpdateData(
                table: "Position",
                keyColumn: "Id",
                keyValue: 1001,
                column: "CreatedTime",
                value: new DateTime(2023, 7, 27, 17, 29, 38, 937, DateTimeKind.Local).AddTicks(2779));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AccountStatus",
                keyColumn: "Id",
                keyValue: 1001,
                column: "CreatedTime",
                value: new DateTime(2023, 7, 27, 11, 52, 44, 117, DateTimeKind.Local).AddTicks(2327));

            migrationBuilder.UpdateData(
                table: "AccountStatus",
                keyColumn: "Id",
                keyValue: 1002,
                column: "CreatedTime",
                value: new DateTime(2023, 7, 27, 11, 52, 44, 117, DateTimeKind.Local).AddTicks(2410));

            migrationBuilder.UpdateData(
                table: "AdminAccount",
                keyColumn: "Id",
                keyValue: 1001L,
                column: "CreatedTime",
                value: new DateTime(2023, 7, 27, 11, 52, 44, 117, DateTimeKind.Local).AddTicks(2466));

            migrationBuilder.UpdateData(
                table: "CandidateLevel",
                keyColumn: "Id",
                keyValue: 1001,
                column: "CreatedTime",
                value: new DateTime(2023, 7, 27, 11, 52, 44, 117, DateTimeKind.Local).AddTicks(2564));

            migrationBuilder.UpdateData(
                table: "EmployerServicePackage",
                keyColumn: "Id",
                keyValue: 1001,
                column: "CreatedTime",
                value: new DateTime(2023, 7, 27, 11, 52, 44, 117, DateTimeKind.Local).AddTicks(2715));

            migrationBuilder.UpdateData(
                table: "Position",
                keyColumn: "Id",
                keyValue: 1001,
                column: "CreatedTime",
                value: new DateTime(2023, 7, 27, 11, 52, 44, 117, DateTimeKind.Local).AddTicks(2642));

            migrationBuilder.CreateIndex(
                name: "IX_EmployerMeta_Key",
                table: "EmployerMeta",
                column: "Key",
                unique: true);
        }
    }
}
