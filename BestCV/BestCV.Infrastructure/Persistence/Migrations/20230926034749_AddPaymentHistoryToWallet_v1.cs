using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Jobi.Infrastructure.Persistence.Migrations
{
    public partial class AddPaymentHistoryToWallet_v1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "WalletHistoryType",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false, comment: "Mã")
                        .Annotation("SqlServer:Identity", "1001, 1"),
                    Color = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true, comment: "Màu"),
                    Active = table.Column<bool>(type: "bit", nullable: false, defaultValue: true, comment: "Đánh dấu bị xóa"),
                    CreatedTime = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "(getdate())", comment: "Ngày tạo"),
                    Name = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false, comment: "Tên"),
                    Description = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true, comment: "Mô tả")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WalletHistoryType", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "EmployerWalletHistories",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false, comment: "Mã")
                        .Annotation("SqlServer:Identity", "1001, 1"),
                    Amount = table.Column<int>(type: "int", nullable: false, comment: "Giá trị"),
                    EmploEmployerWalletId = table.Column<long>(type: "bigint", nullable: false, comment: "Mã ví nhà tuyển dụng"),
                    WalletHistoryTypeId = table.Column<int>(type: "int", nullable: false, comment: "Mã loại lịch sử ví"),
                    Active = table.Column<bool>(type: "bit", nullable: false, defaultValue: true, comment: "Đánh dấu đã bị xóa"),
                    CreatedTime = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "(getdate())", comment: "Ngày tạo"),
                    Name = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false, comment: "Tên"),
                    Description = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true, comment: "Mô tả")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EmployerWalletHistory", x => x.Id);
                    table.ForeignKey(
                        name: "FK_EmployerWalletHistory_EmployerWallet",
                        column: x => x.EmploEmployerWalletId,
                        principalTable: "EmployerWallet",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_EmployerWalletHistory_WalletHistoryType",
                        column: x => x.WalletHistoryTypeId,
                        principalTable: "WalletHistoryType",
                        principalColumn: "Id");
                });

            migrationBuilder.UpdateData(
                table: "AccountStatus",
                keyColumn: "Id",
                keyValue: 1001,
                column: "CreatedTime",
                value: new DateTime(2023, 9, 26, 10, 47, 45, 88, DateTimeKind.Local).AddTicks(7647));

            migrationBuilder.UpdateData(
                table: "AccountStatus",
                keyColumn: "Id",
                keyValue: 1002,
                column: "CreatedTime",
                value: new DateTime(2023, 9, 26, 10, 47, 45, 88, DateTimeKind.Local).AddTicks(7738));

            migrationBuilder.UpdateData(
                table: "AdminAccount",
                keyColumn: "Id",
                keyValue: 1001L,
                column: "CreatedTime",
                value: new DateTime(2023, 9, 26, 10, 47, 45, 88, DateTimeKind.Local).AddTicks(7764));

            migrationBuilder.UpdateData(
                table: "CVTemplateStatus",
                keyColumn: "Id",
                keyValue: 1001L,
                column: "CreatedTime",
                value: new DateTime(2023, 9, 26, 10, 47, 45, 88, DateTimeKind.Local).AddTicks(7875));

            migrationBuilder.UpdateData(
                table: "CVTemplateStatus",
                keyColumn: "Id",
                keyValue: 1002L,
                column: "CreatedTime",
                value: new DateTime(2023, 9, 26, 10, 47, 45, 88, DateTimeKind.Local).AddTicks(7887));

            migrationBuilder.UpdateData(
                table: "CandidateLevel",
                keyColumn: "Id",
                keyValue: 1001,
                column: "CreatedTime",
                value: new DateTime(2023, 9, 26, 10, 47, 45, 88, DateTimeKind.Local).AddTicks(7803));

            migrationBuilder.UpdateData(
                table: "Position",
                keyColumn: "Id",
                keyValue: 1001,
                column: "CreatedTime",
                value: new DateTime(2023, 9, 26, 10, 47, 45, 88, DateTimeKind.Local).AddTicks(7845));

            migrationBuilder.CreateIndex(
                name: "IX_EmployerWalletHistories_EmploEmployerWalletId",
                table: "EmployerWalletHistories",
                column: "EmploEmployerWalletId");

            migrationBuilder.CreateIndex(
                name: "IX_EmployerWalletHistories_WalletHistoryTypeId",
                table: "EmployerWalletHistories",
                column: "WalletHistoryTypeId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "EmployerWalletHistories");

            migrationBuilder.DropTable(
                name: "WalletHistoryType");

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
        }
    }
}
