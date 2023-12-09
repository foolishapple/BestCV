using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Jobi.Infrastructure.Persistence.Migrations
{
    public partial class rename_table_companyShowOnHomePage_v1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CompanyShowOnHomePage");

            migrationBuilder.CreateTable(
                name: "TopCompany",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false, comment: "Mã công ty hàng đầu")
                        .Annotation("SqlServer:Identity", "1001, 1"),
                    CompanyId = table.Column<int>(type: "int", nullable: false, comment: "Mã công ty"),
                    OrderSort = table.Column<int>(type: "int", nullable: false, comment: "Sắp xếp"),
                    Active = table.Column<bool>(type: "bit", nullable: false, defaultValueSql: "((1))", comment: "Đánh dấu bị xóa"),
                    CreatedTime = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "(getdate())", comment: "Ngày tạo")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TopCompany", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CompanyShowOnHomePage_Company",
                        column: x => x.CompanyId,
                        principalTable: "Company",
                        principalColumn: "Id");
                });

            migrationBuilder.UpdateData(
                table: "AccountStatus",
                keyColumn: "Id",
                keyValue: 1001,
                column: "CreatedTime",
                value: new DateTime(2023, 8, 14, 17, 10, 2, 569, DateTimeKind.Local).AddTicks(6479));

            migrationBuilder.UpdateData(
                table: "AccountStatus",
                keyColumn: "Id",
                keyValue: 1002,
                column: "CreatedTime",
                value: new DateTime(2023, 8, 14, 17, 10, 2, 569, DateTimeKind.Local).AddTicks(6526));

            migrationBuilder.UpdateData(
                table: "AdminAccount",
                keyColumn: "Id",
                keyValue: 1001L,
                column: "CreatedTime",
                value: new DateTime(2023, 8, 14, 17, 10, 2, 569, DateTimeKind.Local).AddTicks(6552));

            migrationBuilder.UpdateData(
                table: "CandidateLevel",
                keyColumn: "Id",
                keyValue: 1001,
                column: "CreatedTime",
                value: new DateTime(2023, 8, 14, 17, 10, 2, 569, DateTimeKind.Local).AddTicks(6587));

            migrationBuilder.UpdateData(
                table: "EmployerServicePackage",
                keyColumn: "Id",
                keyValue: 1001,
                column: "CreatedTime",
                value: new DateTime(2023, 8, 14, 17, 10, 2, 569, DateTimeKind.Local).AddTicks(6646));

            migrationBuilder.UpdateData(
                table: "Position",
                keyColumn: "Id",
                keyValue: 1001,
                column: "CreatedTime",
                value: new DateTime(2023, 8, 14, 17, 10, 2, 569, DateTimeKind.Local).AddTicks(6617));

            migrationBuilder.CreateIndex(
                name: "IX_TopCompany_CompanyId",
                table: "TopCompany",
                column: "CompanyId",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "TopCompany");

            migrationBuilder.CreateTable(
                name: "CompanyShowOnHomePage",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1001, 1"),
                    CompanyId = table.Column<int>(type: "int", nullable: false, comment: "Mã công ty"),
                    Active = table.Column<bool>(type: "bit", nullable: false, defaultValueSql: "((1))", comment: "Đánh dấu bị xóa"),
                    CreatedTime = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "(getdate())", comment: "Ngày tạo"),
                    OrderSort = table.Column<int>(type: "int", nullable: false, comment: "Sắp xếp")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CompanyShowOnHomePage", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CompanyShowOnHomePage_Company",
                        column: x => x.CompanyId,
                        principalTable: "Company",
                        principalColumn: "Id");
                });

            migrationBuilder.UpdateData(
                table: "AccountStatus",
                keyColumn: "Id",
                keyValue: 1001,
                column: "CreatedTime",
                value: new DateTime(2023, 8, 11, 11, 39, 5, 729, DateTimeKind.Local).AddTicks(5294));

            migrationBuilder.UpdateData(
                table: "AccountStatus",
                keyColumn: "Id",
                keyValue: 1002,
                column: "CreatedTime",
                value: new DateTime(2023, 8, 11, 11, 39, 5, 729, DateTimeKind.Local).AddTicks(5395));

            migrationBuilder.UpdateData(
                table: "AdminAccount",
                keyColumn: "Id",
                keyValue: 1001L,
                column: "CreatedTime",
                value: new DateTime(2023, 8, 11, 11, 39, 5, 729, DateTimeKind.Local).AddTicks(5435));

            migrationBuilder.UpdateData(
                table: "CandidateLevel",
                keyColumn: "Id",
                keyValue: 1001,
                column: "CreatedTime",
                value: new DateTime(2023, 8, 11, 11, 39, 5, 729, DateTimeKind.Local).AddTicks(5483));

            migrationBuilder.UpdateData(
                table: "EmployerServicePackage",
                keyColumn: "Id",
                keyValue: 1001,
                column: "CreatedTime",
                value: new DateTime(2023, 8, 11, 11, 39, 5, 729, DateTimeKind.Local).AddTicks(5559));

            migrationBuilder.UpdateData(
                table: "Position",
                keyColumn: "Id",
                keyValue: 1001,
                column: "CreatedTime",
                value: new DateTime(2023, 8, 11, 11, 39, 5, 729, DateTimeKind.Local).AddTicks(5519));

            migrationBuilder.CreateIndex(
                name: "IX_CompanyShowOnHomePage_CompanyId",
                table: "CompanyShowOnHomePage",
                column: "CompanyId",
                unique: true);
        }
    }
}
