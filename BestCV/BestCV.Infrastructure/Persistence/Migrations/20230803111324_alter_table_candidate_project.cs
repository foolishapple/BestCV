using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BestCV.Infrastructure.Persistence.Migrations
{
    public partial class alter_table_candidate_project : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Description",
                table: "CandidateProjects");

            migrationBuilder.DropColumn(
                name: "Name",
                table: "CandidateProjects");

            migrationBuilder.UpdateData(
                table: "AccountStatus",
                keyColumn: "Id",
                keyValue: 1001,
                column: "CreatedTime",
                value: new DateTime(2023, 8, 3, 18, 13, 23, 668, DateTimeKind.Local).AddTicks(201));

            migrationBuilder.UpdateData(
                table: "AccountStatus",
                keyColumn: "Id",
                keyValue: 1002,
                column: "CreatedTime",
                value: new DateTime(2023, 8, 3, 18, 13, 23, 668, DateTimeKind.Local).AddTicks(244));

            migrationBuilder.UpdateData(
                table: "AdminAccount",
                keyColumn: "Id",
                keyValue: 1001L,
                column: "CreatedTime",
                value: new DateTime(2023, 8, 3, 18, 13, 23, 668, DateTimeKind.Local).AddTicks(265));

            migrationBuilder.UpdateData(
                table: "CandidateLevel",
                keyColumn: "Id",
                keyValue: 1001,
                column: "CreatedTime",
                value: new DateTime(2023, 8, 3, 18, 13, 23, 668, DateTimeKind.Local).AddTicks(302));

            migrationBuilder.UpdateData(
                table: "EmployerServicePackage",
                keyColumn: "Id",
                keyValue: 1001,
                column: "CreatedTime",
                value: new DateTime(2023, 8, 3, 18, 13, 23, 668, DateTimeKind.Local).AddTicks(355));

            migrationBuilder.UpdateData(
                table: "Position",
                keyColumn: "Id",
                keyValue: 1001,
                column: "CreatedTime",
                value: new DateTime(2023, 8, 3, 18, 13, 23, 668, DateTimeKind.Local).AddTicks(328));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Description",
                table: "CandidateProjects",
                type: "nvarchar(max)",
                nullable: true,
                comment: "Mô tả");

            migrationBuilder.AddColumn<string>(
                name: "Name",
                table: "CandidateProjects",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.UpdateData(
                table: "AccountStatus",
                keyColumn: "Id",
                keyValue: 1001,
                column: "CreatedTime",
                value: new DateTime(2023, 8, 2, 16, 1, 32, 775, DateTimeKind.Local).AddTicks(354));

            migrationBuilder.UpdateData(
                table: "AccountStatus",
                keyColumn: "Id",
                keyValue: 1002,
                column: "CreatedTime",
                value: new DateTime(2023, 8, 2, 16, 1, 32, 775, DateTimeKind.Local).AddTicks(519));

            migrationBuilder.UpdateData(
                table: "AdminAccount",
                keyColumn: "Id",
                keyValue: 1001L,
                column: "CreatedTime",
                value: new DateTime(2023, 8, 2, 16, 1, 32, 775, DateTimeKind.Local).AddTicks(570));

            migrationBuilder.UpdateData(
                table: "CandidateLevel",
                keyColumn: "Id",
                keyValue: 1001,
                column: "CreatedTime",
                value: new DateTime(2023, 8, 2, 16, 1, 32, 775, DateTimeKind.Local).AddTicks(666));

            migrationBuilder.UpdateData(
                table: "EmployerServicePackage",
                keyColumn: "Id",
                keyValue: 1001,
                column: "CreatedTime",
                value: new DateTime(2023, 8, 2, 16, 1, 32, 775, DateTimeKind.Local).AddTicks(792));

            migrationBuilder.UpdateData(
                table: "Position",
                keyColumn: "Id",
                keyValue: 1001,
                column: "CreatedTime",
                value: new DateTime(2023, 8, 2, 16, 1, 32, 775, DateTimeKind.Local).AddTicks(726));
        }
    }
}
