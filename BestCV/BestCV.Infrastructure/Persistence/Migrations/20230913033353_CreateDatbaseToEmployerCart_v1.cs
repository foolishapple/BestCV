using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BestCV.Infrastructure.Persistence.Migrations
{
    public partial class CreateDatbaseToEmployerCart_v1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "EmployerCart",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false, comment: "Mã")
                        .Annotation("SqlServer:Identity", "1001, 1"),
                    EmployerId = table.Column<long>(type: "bigint", nullable: false),
                    EmployerServicePackageId = table.Column<int>(type: "int", nullable: false),
                    Quantity = table.Column<int>(type: "int", nullable: false, defaultValue: 1, comment: "Số lượng"),
                    Description = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true, comment: "Mô tả"),
                    Active = table.Column<bool>(type: "bit", nullable: false, defaultValue: true, comment: "Đánh dấu bị xóa"),
                    CreatedTime = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "(getdate())", comment: "Ngày tạo")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EmployerCart", x => x.Id);
                    table.ForeignKey(
                        name: "FK_EmployerCart_Employer",
                        column: x => x.EmployerId,
                        principalTable: "Employer",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_EmployerCart_EmployerServicePackage",
                        column: x => x.EmployerServicePackageId,
                        principalTable: "EmployerServicePackage",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.UpdateData(
                table: "AccountStatus",
                keyColumn: "Id",
                keyValue: 1001,
                column: "CreatedTime",
                value: new DateTime(2023, 9, 13, 10, 33, 48, 754, DateTimeKind.Local).AddTicks(1926));

            migrationBuilder.UpdateData(
                table: "AccountStatus",
                keyColumn: "Id",
                keyValue: 1002,
                column: "CreatedTime",
                value: new DateTime(2023, 9, 13, 10, 33, 48, 754, DateTimeKind.Local).AddTicks(1965));

            migrationBuilder.UpdateData(
                table: "AdminAccount",
                keyColumn: "Id",
                keyValue: 1001L,
                column: "CreatedTime",
                value: new DateTime(2023, 9, 13, 10, 33, 48, 754, DateTimeKind.Local).AddTicks(1990));

            migrationBuilder.UpdateData(
                table: "CVTemplateStatus",
                keyColumn: "Id",
                keyValue: 1001L,
                column: "CreatedTime",
                value: new DateTime(2023, 9, 13, 10, 33, 48, 754, DateTimeKind.Local).AddTicks(2086));

            migrationBuilder.UpdateData(
                table: "CVTemplateStatus",
                keyColumn: "Id",
                keyValue: 1002L,
                column: "CreatedTime",
                value: new DateTime(2023, 9, 13, 10, 33, 48, 754, DateTimeKind.Local).AddTicks(2098));

            migrationBuilder.UpdateData(
                table: "CandidateLevel",
                keyColumn: "Id",
                keyValue: 1001,
                column: "CreatedTime",
                value: new DateTime(2023, 9, 13, 10, 33, 48, 754, DateTimeKind.Local).AddTicks(2028));

            migrationBuilder.UpdateData(
                table: "Position",
                keyColumn: "Id",
                keyValue: 1001,
                column: "CreatedTime",
                value: new DateTime(2023, 9, 13, 10, 33, 48, 754, DateTimeKind.Local).AddTicks(2054));

            migrationBuilder.CreateIndex(
                name: "IX_EmployerCart_EmployerId",
                table: "EmployerCart",
                column: "EmployerId");

            migrationBuilder.CreateIndex(
                name: "IX_EmployerCart_EmployerServicePackageId",
                table: "EmployerCart",
                column: "EmployerServicePackageId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "EmployerCart");

            migrationBuilder.UpdateData(
                table: "AccountStatus",
                keyColumn: "Id",
                keyValue: 1001,
                column: "CreatedTime",
                value: new DateTime(2023, 9, 12, 8, 4, 12, 386, DateTimeKind.Local).AddTicks(786));

            migrationBuilder.UpdateData(
                table: "AccountStatus",
                keyColumn: "Id",
                keyValue: 1002,
                column: "CreatedTime",
                value: new DateTime(2023, 9, 12, 8, 4, 12, 386, DateTimeKind.Local).AddTicks(852));

            migrationBuilder.UpdateData(
                table: "AdminAccount",
                keyColumn: "Id",
                keyValue: 1001L,
                column: "CreatedTime",
                value: new DateTime(2023, 9, 12, 8, 4, 12, 386, DateTimeKind.Local).AddTicks(876));

            migrationBuilder.UpdateData(
                table: "CVTemplateStatus",
                keyColumn: "Id",
                keyValue: 1001L,
                column: "CreatedTime",
                value: new DateTime(2023, 9, 12, 8, 4, 12, 386, DateTimeKind.Local).AddTicks(971));

            migrationBuilder.UpdateData(
                table: "CVTemplateStatus",
                keyColumn: "Id",
                keyValue: 1002L,
                column: "CreatedTime",
                value: new DateTime(2023, 9, 12, 8, 4, 12, 386, DateTimeKind.Local).AddTicks(982));

            migrationBuilder.UpdateData(
                table: "CandidateLevel",
                keyColumn: "Id",
                keyValue: 1001,
                column: "CreatedTime",
                value: new DateTime(2023, 9, 12, 8, 4, 12, 386, DateTimeKind.Local).AddTicks(912));

            migrationBuilder.UpdateData(
                table: "Position",
                keyColumn: "Id",
                keyValue: 1001,
                column: "CreatedTime",
                value: new DateTime(2023, 9, 12, 8, 4, 12, 386, DateTimeKind.Local).AddTicks(944));
        }
    }
}
