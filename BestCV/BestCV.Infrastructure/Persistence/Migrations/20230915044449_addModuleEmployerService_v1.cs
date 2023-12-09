using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BestCV.Infrastructure.Persistence.Migrations
{
    public partial class addModuleEmployerService_v1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "EmployerServicePackageEmployer",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false, comment: "Mã")
                        .Annotation("SqlServer:Identity", "1001, 1"),
                    EmployerOrderDetailId = table.Column<long>(type: "bigint", nullable: false, comment: "Mã chi tiết đơn hàng"),
                    Active = table.Column<bool>(type: "bit", nullable: false, defaultValue: true, comment: "Đánh dấu đã bị xóa"),
                    CreatedTime = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "(getdate())", comment: "Ngày tạo")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EmployerServicePackageEmployer", x => x.Id);
                    table.ForeignKey(
                        name: "FK_EmployerServicePackageEmployer_EmployerOrderDetail",
                        column: x => x.EmployerOrderDetailId,
                        principalTable: "EmployerOrderDetail",
                        principalColumn: "Id");
                });

            migrationBuilder.UpdateData(
                table: "AccountStatus",
                keyColumn: "Id",
                keyValue: 1001,
                column: "CreatedTime",
                value: new DateTime(2023, 9, 15, 11, 44, 43, 943, DateTimeKind.Local).AddTicks(205));

            migrationBuilder.UpdateData(
                table: "AccountStatus",
                keyColumn: "Id",
                keyValue: 1002,
                column: "CreatedTime",
                value: new DateTime(2023, 9, 15, 11, 44, 43, 943, DateTimeKind.Local).AddTicks(253));

            migrationBuilder.UpdateData(
                table: "AdminAccount",
                keyColumn: "Id",
                keyValue: 1001L,
                column: "CreatedTime",
                value: new DateTime(2023, 9, 15, 11, 44, 43, 943, DateTimeKind.Local).AddTicks(276));

            migrationBuilder.UpdateData(
                table: "CVTemplateStatus",
                keyColumn: "Id",
                keyValue: 1001L,
                column: "CreatedTime",
                value: new DateTime(2023, 9, 15, 11, 44, 43, 943, DateTimeKind.Local).AddTicks(372));

            migrationBuilder.UpdateData(
                table: "CVTemplateStatus",
                keyColumn: "Id",
                keyValue: 1002L,
                column: "CreatedTime",
                value: new DateTime(2023, 9, 15, 11, 44, 43, 943, DateTimeKind.Local).AddTicks(384));

            migrationBuilder.UpdateData(
                table: "CandidateLevel",
                keyColumn: "Id",
                keyValue: 1001,
                column: "CreatedTime",
                value: new DateTime(2023, 9, 15, 11, 44, 43, 943, DateTimeKind.Local).AddTicks(311));

            migrationBuilder.UpdateData(
                table: "Position",
                keyColumn: "Id",
                keyValue: 1001,
                column: "CreatedTime",
                value: new DateTime(2023, 9, 15, 11, 44, 43, 943, DateTimeKind.Local).AddTicks(343));

            migrationBuilder.CreateIndex(
                name: "IX_EmployerServicePackageEmployer_EmployerOrderDetailId",
                table: "EmployerServicePackageEmployer",
                column: "EmployerOrderDetailId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "EmployerServicePackageEmployer");

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
        }
    }
}
