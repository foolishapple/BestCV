using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Jobi.Infrastructure.Persistence.Migrations
{
    public partial class updateCompanyMustInterestedAndJobCategory : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_MustBeInterestedCompany_Job",
                table: "MustBeInterestedCompany");

            migrationBuilder.DropIndex(
                name: "IX_MustBeInterestedCompany_JobId",
                table: "MustBeInterestedCompany");

            migrationBuilder.DropColumn(
                name: "JobId",
                table: "MustBeInterestedCompany");

            migrationBuilder.AddColumn<int>(
                name: "CompanyId",
                table: "MustBeInterestedCompany",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "ReferenceCategory",
                table: "JobCategory",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: true,
                comment: "Ngành liên quan");

            migrationBuilder.UpdateData(
                table: "AccountStatus",
                keyColumn: "Id",
                keyValue: 1001,
                column: "CreatedTime",
                value: new DateTime(2023, 9, 20, 9, 30, 49, 200, DateTimeKind.Local).AddTicks(6381));

            migrationBuilder.UpdateData(
                table: "AccountStatus",
                keyColumn: "Id",
                keyValue: 1002,
                column: "CreatedTime",
                value: new DateTime(2023, 9, 20, 9, 30, 49, 200, DateTimeKind.Local).AddTicks(6434));

            migrationBuilder.UpdateData(
                table: "AdminAccount",
                keyColumn: "Id",
                keyValue: 1001L,
                column: "CreatedTime",
                value: new DateTime(2023, 9, 20, 9, 30, 49, 200, DateTimeKind.Local).AddTicks(6456));

            migrationBuilder.UpdateData(
                table: "CVTemplateStatus",
                keyColumn: "Id",
                keyValue: 1001L,
                column: "CreatedTime",
                value: new DateTime(2023, 9, 20, 9, 30, 49, 200, DateTimeKind.Local).AddTicks(6528));

            migrationBuilder.UpdateData(
                table: "CVTemplateStatus",
                keyColumn: "Id",
                keyValue: 1002L,
                column: "CreatedTime",
                value: new DateTime(2023, 9, 20, 9, 30, 49, 200, DateTimeKind.Local).AddTicks(6539));

            migrationBuilder.UpdateData(
                table: "CandidateLevel",
                keyColumn: "Id",
                keyValue: 1001,
                column: "CreatedTime",
                value: new DateTime(2023, 9, 20, 9, 30, 49, 200, DateTimeKind.Local).AddTicks(6485));

            migrationBuilder.UpdateData(
                table: "Position",
                keyColumn: "Id",
                keyValue: 1001,
                column: "CreatedTime",
                value: new DateTime(2023, 9, 20, 9, 30, 49, 200, DateTimeKind.Local).AddTicks(6506));

            migrationBuilder.CreateIndex(
                name: "IX_MustBeInterestedCompany_CompanyId",
                table: "MustBeInterestedCompany",
                column: "CompanyId",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_MustBeInterestedCompany_Company",
                table: "MustBeInterestedCompany",
                column: "CompanyId",
                principalTable: "Company",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_MustBeInterestedCompany_Company",
                table: "MustBeInterestedCompany");

            migrationBuilder.DropIndex(
                name: "IX_MustBeInterestedCompany_CompanyId",
                table: "MustBeInterestedCompany");

            migrationBuilder.DropColumn(
                name: "CompanyId",
                table: "MustBeInterestedCompany");

            migrationBuilder.DropColumn(
                name: "ReferenceCategory",
                table: "JobCategory");

            migrationBuilder.AddColumn<long>(
                name: "JobId",
                table: "MustBeInterestedCompany",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.UpdateData(
                table: "AccountStatus",
                keyColumn: "Id",
                keyValue: 1001,
                column: "CreatedTime",
                value: new DateTime(2023, 9, 18, 9, 19, 39, 932, DateTimeKind.Local).AddTicks(2273));

            migrationBuilder.UpdateData(
                table: "AccountStatus",
                keyColumn: "Id",
                keyValue: 1002,
                column: "CreatedTime",
                value: new DateTime(2023, 9, 18, 9, 19, 39, 932, DateTimeKind.Local).AddTicks(2341));

            migrationBuilder.UpdateData(
                table: "AdminAccount",
                keyColumn: "Id",
                keyValue: 1001L,
                column: "CreatedTime",
                value: new DateTime(2023, 9, 18, 9, 19, 39, 932, DateTimeKind.Local).AddTicks(2362));

            migrationBuilder.UpdateData(
                table: "CVTemplateStatus",
                keyColumn: "Id",
                keyValue: 1001L,
                column: "CreatedTime",
                value: new DateTime(2023, 9, 18, 9, 19, 39, 932, DateTimeKind.Local).AddTicks(2455));

            migrationBuilder.UpdateData(
                table: "CVTemplateStatus",
                keyColumn: "Id",
                keyValue: 1002L,
                column: "CreatedTime",
                value: new DateTime(2023, 9, 18, 9, 19, 39, 932, DateTimeKind.Local).AddTicks(2467));

            migrationBuilder.UpdateData(
                table: "CandidateLevel",
                keyColumn: "Id",
                keyValue: 1001,
                column: "CreatedTime",
                value: new DateTime(2023, 9, 18, 9, 19, 39, 932, DateTimeKind.Local).AddTicks(2395));

            migrationBuilder.UpdateData(
                table: "Position",
                keyColumn: "Id",
                keyValue: 1001,
                column: "CreatedTime",
                value: new DateTime(2023, 9, 18, 9, 19, 39, 932, DateTimeKind.Local).AddTicks(2426));

            migrationBuilder.CreateIndex(
                name: "IX_MustBeInterestedCompany_JobId",
                table: "MustBeInterestedCompany",
                column: "JobId",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_MustBeInterestedCompany_Job",
                table: "MustBeInterestedCompany",
                column: "JobId",
                principalTable: "Job",
                principalColumn: "Id");
        }
    }
}
