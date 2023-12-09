using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Jobi.Infrastructure.Persistence.Migrations
{
    public partial class alter_table_candidate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "MaritalStatus",
                table: "Candidate",
                type: "nvarchar(255)",
                maxLength: 255,
                nullable: true,
                comment: "Tình trạng hôn nhân");

            migrationBuilder.AddColumn<string>(
                name: "Nationality",
                table: "Candidate",
                type: "nvarchar(255)",
                maxLength: 255,
                nullable: true,
                comment: "Quốc tịch");

            migrationBuilder.AddColumn<string>(
                name: "References",
                table: "Candidate",
                type: "nvarchar(1000)",
                maxLength: 1000,
                nullable: true,
                comment: "Người tham khảo");

            migrationBuilder.UpdateData(
                table: "AccountStatus",
                keyColumn: "Id",
                keyValue: 1001,
                column: "CreatedTime",
                value: new DateTime(2023, 8, 4, 22, 28, 40, 174, DateTimeKind.Local).AddTicks(4525));

            migrationBuilder.UpdateData(
                table: "AccountStatus",
                keyColumn: "Id",
                keyValue: 1002,
                column: "CreatedTime",
                value: new DateTime(2023, 8, 4, 22, 28, 40, 174, DateTimeKind.Local).AddTicks(4560));

            migrationBuilder.UpdateData(
                table: "AdminAccount",
                keyColumn: "Id",
                keyValue: 1001L,
                column: "CreatedTime",
                value: new DateTime(2023, 8, 4, 22, 28, 40, 174, DateTimeKind.Local).AddTicks(4578));

            migrationBuilder.UpdateData(
                table: "CandidateLevel",
                keyColumn: "Id",
                keyValue: 1001,
                column: "CreatedTime",
                value: new DateTime(2023, 8, 4, 22, 28, 40, 174, DateTimeKind.Local).AddTicks(4612));

            migrationBuilder.UpdateData(
                table: "EmployerServicePackage",
                keyColumn: "Id",
                keyValue: 1001,
                column: "CreatedTime",
                value: new DateTime(2023, 8, 4, 22, 28, 40, 174, DateTimeKind.Local).AddTicks(4662));

            migrationBuilder.UpdateData(
                table: "Position",
                keyColumn: "Id",
                keyValue: 1001,
                column: "CreatedTime",
                value: new DateTime(2023, 8, 4, 22, 28, 40, 174, DateTimeKind.Local).AddTicks(4638));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "MaritalStatus",
                table: "Candidate");

            migrationBuilder.DropColumn(
                name: "Nationality",
                table: "Candidate");

            migrationBuilder.DropColumn(
                name: "References",
                table: "Candidate");

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
    }
}
