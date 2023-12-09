using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BestCV.Infrastructure.Persistence.Migrations
{
    public partial class delete_unique_key_Table_employerMeta2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_EmployerMeta_Name",
                table: "EmployerMeta");

            migrationBuilder.UpdateData(
                table: "AccountStatus",
                keyColumn: "Id",
                keyValue: 1001,
                column: "CreatedTime",
                value: new DateTime(2023, 7, 27, 17, 31, 48, 383, DateTimeKind.Local).AddTicks(783));

            migrationBuilder.UpdateData(
                table: "AccountStatus",
                keyColumn: "Id",
                keyValue: 1002,
                column: "CreatedTime",
                value: new DateTime(2023, 7, 27, 17, 31, 48, 383, DateTimeKind.Local).AddTicks(847));

            migrationBuilder.UpdateData(
                table: "AdminAccount",
                keyColumn: "Id",
                keyValue: 1001L,
                column: "CreatedTime",
                value: new DateTime(2023, 7, 27, 17, 31, 48, 383, DateTimeKind.Local).AddTicks(898));

            migrationBuilder.UpdateData(
                table: "CandidateLevel",
                keyColumn: "Id",
                keyValue: 1001,
                column: "CreatedTime",
                value: new DateTime(2023, 7, 27, 17, 31, 48, 383, DateTimeKind.Local).AddTicks(961));

            migrationBuilder.UpdateData(
                table: "EmployerServicePackage",
                keyColumn: "Id",
                keyValue: 1001,
                column: "CreatedTime",
                value: new DateTime(2023, 7, 27, 17, 31, 48, 383, DateTimeKind.Local).AddTicks(1074));

            migrationBuilder.UpdateData(
                table: "Position",
                keyColumn: "Id",
                keyValue: 1001,
                column: "CreatedTime",
                value: new DateTime(2023, 7, 27, 17, 31, 48, 383, DateTimeKind.Local).AddTicks(1014));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
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

            migrationBuilder.CreateIndex(
                name: "IX_EmployerMeta_Name",
                table: "EmployerMeta",
                column: "Name",
                unique: true);
        }
    }
}
