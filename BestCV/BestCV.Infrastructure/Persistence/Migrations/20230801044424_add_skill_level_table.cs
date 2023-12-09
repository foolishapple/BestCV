using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Jobi.Infrastructure.Persistence.Migrations
{
    public partial class add_skill_level_table : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "SkillId",
                table: "CandidateSkill",
                newName: "SkillLevelId");

            migrationBuilder.AddColumn<string>(
                name: "Description",
                table: "CandidateSkill",
                type: "nvarchar(500)",
                maxLength: 500,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Name",
                table: "CandidateSkill",
                type: "nvarchar(500)",
                maxLength: 500,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AlterColumn<string>(
                name: "FullName",
                table: "AdminAccount",
                type: "nvarchar(255)",
                maxLength: 255,
                nullable: false,
                comment: "Tên đầy đủ",
                oldClrType: typeof(string),
                oldType: "nvarchar(255)",
                oldMaxLength: 255);

            migrationBuilder.CreateTable(
                name: "SkillLevel",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1001, 1"),
                    Active = table.Column<bool>(type: "bit", nullable: false, defaultValueSql: "((1))", comment: "Đánh dấu bị xóa"),
                    CreatedTime = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "(getdate())", comment: "Ngày tạo"),
                    Name = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false, comment: "Tên level"),
                    Description = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true, comment: "Mô tả")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SkillLevel", x => x.Id);
                });

            migrationBuilder.UpdateData(
                table: "AccountStatus",
                keyColumn: "Id",
                keyValue: 1001,
                column: "CreatedTime",
                value: new DateTime(2023, 8, 1, 11, 44, 23, 852, DateTimeKind.Local).AddTicks(6447));

            migrationBuilder.UpdateData(
                table: "AccountStatus",
                keyColumn: "Id",
                keyValue: 1002,
                column: "CreatedTime",
                value: new DateTime(2023, 8, 1, 11, 44, 23, 852, DateTimeKind.Local).AddTicks(6510));

            migrationBuilder.UpdateData(
                table: "AdminAccount",
                keyColumn: "Id",
                keyValue: 1001L,
                column: "CreatedTime",
                value: new DateTime(2023, 8, 1, 11, 44, 23, 852, DateTimeKind.Local).AddTicks(6563));

            migrationBuilder.UpdateData(
                table: "CandidateLevel",
                keyColumn: "Id",
                keyValue: 1001,
                column: "CreatedTime",
                value: new DateTime(2023, 8, 1, 11, 44, 23, 852, DateTimeKind.Local).AddTicks(6629));

            migrationBuilder.UpdateData(
                table: "EmployerServicePackage",
                keyColumn: "Id",
                keyValue: 1001,
                column: "CreatedTime",
                value: new DateTime(2023, 8, 1, 11, 44, 23, 852, DateTimeKind.Local).AddTicks(6705));

            migrationBuilder.UpdateData(
                table: "Position",
                keyColumn: "Id",
                keyValue: 1001,
                column: "CreatedTime",
                value: new DateTime(2023, 8, 1, 11, 44, 23, 852, DateTimeKind.Local).AddTicks(6659));

            migrationBuilder.CreateIndex(
                name: "IX_CandidateSkill_CandidateId",
                table: "CandidateSkill",
                column: "CandidateId");

            migrationBuilder.CreateIndex(
                name: "IX_CandidateSkill_SkillLevelId",
                table: "CandidateSkill",
                column: "SkillLevelId");

            migrationBuilder.AddForeignKey(
                name: "FK_CandidateSkill_Candidate_CandidateId",
                table: "CandidateSkill",
                column: "CandidateId",
                principalTable: "Candidate",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_CandidateSkill_SkillLevel_SkillLevelId",
                table: "CandidateSkill",
                column: "SkillLevelId",
                principalTable: "SkillLevel",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CandidateSkill_Candidate_CandidateId",
                table: "CandidateSkill");

            migrationBuilder.DropForeignKey(
                name: "FK_CandidateSkill_SkillLevel_SkillLevelId",
                table: "CandidateSkill");

            migrationBuilder.DropTable(
                name: "SkillLevel");

            migrationBuilder.DropIndex(
                name: "IX_CandidateSkill_CandidateId",
                table: "CandidateSkill");

            migrationBuilder.DropIndex(
                name: "IX_CandidateSkill_SkillLevelId",
                table: "CandidateSkill");

            migrationBuilder.DropColumn(
                name: "Description",
                table: "CandidateSkill");

            migrationBuilder.DropColumn(
                name: "Name",
                table: "CandidateSkill");

            migrationBuilder.RenameColumn(
                name: "SkillLevelId",
                table: "CandidateSkill",
                newName: "SkillId");

            migrationBuilder.AlterColumn<string>(
                name: "FullName",
                table: "AdminAccount",
                type: "nvarchar(255)",
                maxLength: 255,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(255)",
                oldMaxLength: 255,
                oldComment: "Tên đầy đủ");

            migrationBuilder.UpdateData(
                table: "AccountStatus",
                keyColumn: "Id",
                keyValue: 1001,
                column: "CreatedTime",
                value: new DateTime(2023, 7, 31, 14, 29, 19, 326, DateTimeKind.Local).AddTicks(1138));

            migrationBuilder.UpdateData(
                table: "AccountStatus",
                keyColumn: "Id",
                keyValue: 1002,
                column: "CreatedTime",
                value: new DateTime(2023, 7, 31, 14, 29, 19, 326, DateTimeKind.Local).AddTicks(1172));

            migrationBuilder.UpdateData(
                table: "AdminAccount",
                keyColumn: "Id",
                keyValue: 1001L,
                column: "CreatedTime",
                value: new DateTime(2023, 7, 31, 14, 29, 19, 326, DateTimeKind.Local).AddTicks(1189));

            migrationBuilder.UpdateData(
                table: "CandidateLevel",
                keyColumn: "Id",
                keyValue: 1001,
                column: "CreatedTime",
                value: new DateTime(2023, 7, 31, 14, 29, 19, 326, DateTimeKind.Local).AddTicks(1271));

            migrationBuilder.UpdateData(
                table: "EmployerServicePackage",
                keyColumn: "Id",
                keyValue: 1001,
                column: "CreatedTime",
                value: new DateTime(2023, 7, 31, 14, 29, 19, 326, DateTimeKind.Local).AddTicks(1329));

            migrationBuilder.UpdateData(
                table: "Position",
                keyColumn: "Id",
                keyValue: 1001,
                column: "CreatedTime",
                value: new DateTime(2023, 7, 31, 14, 29, 19, 326, DateTimeKind.Local).AddTicks(1300));
        }
    }
}
