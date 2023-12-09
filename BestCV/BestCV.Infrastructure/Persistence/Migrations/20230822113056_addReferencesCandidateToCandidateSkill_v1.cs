using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BestCV.Infrastructure.Persistence.Migrations
{
    public partial class addReferencesCandidateToCandidateSkill_v1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "SkillId",
                table: "CandidateSkill",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.UpdateData(
                table: "AccountStatus",
                keyColumn: "Id",
                keyValue: 1001,
                column: "CreatedTime",
                value: new DateTime(2023, 8, 22, 18, 30, 52, 95, DateTimeKind.Local).AddTicks(7015));

            migrationBuilder.UpdateData(
                table: "AccountStatus",
                keyColumn: "Id",
                keyValue: 1002,
                column: "CreatedTime",
                value: new DateTime(2023, 8, 22, 18, 30, 52, 95, DateTimeKind.Local).AddTicks(7083));

            migrationBuilder.UpdateData(
                table: "AdminAccount",
                keyColumn: "Id",
                keyValue: 1001L,
                column: "CreatedTime",
                value: new DateTime(2023, 8, 22, 18, 30, 52, 95, DateTimeKind.Local).AddTicks(7103));

            migrationBuilder.UpdateData(
                table: "CandidateLevel",
                keyColumn: "Id",
                keyValue: 1001,
                column: "CreatedTime",
                value: new DateTime(2023, 8, 22, 18, 30, 52, 95, DateTimeKind.Local).AddTicks(7139));

            migrationBuilder.UpdateData(
                table: "EmployerServicePackage",
                keyColumn: "Id",
                keyValue: 1001,
                column: "CreatedTime",
                value: new DateTime(2023, 8, 22, 18, 30, 52, 95, DateTimeKind.Local).AddTicks(7201));

            migrationBuilder.UpdateData(
                table: "Position",
                keyColumn: "Id",
                keyValue: 1001,
                column: "CreatedTime",
                value: new DateTime(2023, 8, 22, 18, 30, 52, 95, DateTimeKind.Local).AddTicks(7171));

            migrationBuilder.CreateIndex(
                name: "IX_CandidateSkill_SkillId",
                table: "CandidateSkill",
                column: "SkillId");

            migrationBuilder.AddForeignKey(
                name: "FK_CandidateSkill_Skill",
                table: "CandidateSkill",
                column: "SkillId",
                principalTable: "Skill",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CandidateSkill_Skill",
                table: "CandidateSkill");

            migrationBuilder.DropIndex(
                name: "IX_CandidateSkill_SkillId",
                table: "CandidateSkill");

            migrationBuilder.DropColumn(
                name: "SkillId",
                table: "CandidateSkill");

            migrationBuilder.UpdateData(
                table: "AccountStatus",
                keyColumn: "Id",
                keyValue: 1001,
                column: "CreatedTime",
                value: new DateTime(2023, 8, 22, 11, 59, 55, 552, DateTimeKind.Local).AddTicks(6382));

            migrationBuilder.UpdateData(
                table: "AccountStatus",
                keyColumn: "Id",
                keyValue: 1002,
                column: "CreatedTime",
                value: new DateTime(2023, 8, 22, 11, 59, 55, 552, DateTimeKind.Local).AddTicks(6419));

            migrationBuilder.UpdateData(
                table: "AdminAccount",
                keyColumn: "Id",
                keyValue: 1001L,
                column: "CreatedTime",
                value: new DateTime(2023, 8, 22, 11, 59, 55, 552, DateTimeKind.Local).AddTicks(6441));

            migrationBuilder.UpdateData(
                table: "CandidateLevel",
                keyColumn: "Id",
                keyValue: 1001,
                column: "CreatedTime",
                value: new DateTime(2023, 8, 22, 11, 59, 55, 552, DateTimeKind.Local).AddTicks(6475));

            migrationBuilder.UpdateData(
                table: "EmployerServicePackage",
                keyColumn: "Id",
                keyValue: 1001,
                column: "CreatedTime",
                value: new DateTime(2023, 8, 22, 11, 59, 55, 552, DateTimeKind.Local).AddTicks(6531));

            migrationBuilder.UpdateData(
                table: "Position",
                keyColumn: "Id",
                keyValue: 1001,
                column: "CreatedTime",
                value: new DateTime(2023, 8, 22, 11, 59, 55, 552, DateTimeKind.Local).AddTicks(6501));
        }
    }
}
