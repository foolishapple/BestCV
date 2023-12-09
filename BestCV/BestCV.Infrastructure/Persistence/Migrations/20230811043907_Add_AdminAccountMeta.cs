using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BestCV.Infrastructure.Persistence.Migrations
{
    public partial class Add_AdminAccountMeta : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AdminAccountMeta",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1001, 1"),
                    AdminAccountId = table.Column<long>(type: "bigint", nullable: false, comment: "Mã Admin"),
                    Key = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false, comment: "Tên khóa"),
                    Value = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false, comment: "Giá trị"),
                    Active = table.Column<bool>(type: "bit", nullable: false, defaultValueSql: "((1))", comment: "Đánh dấu bị xóa"),
                    CreatedTime = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "(getdate())", comment: "Ngày tạo"),
                    Name = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false, comment: "Tên dữ liệu bổ sung của Admin"),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true, comment: "Mô tả")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AdminAccountMeta", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AdminAccountMeta_AdminAccount",
                        column: x => x.AdminAccountId,
                        principalTable: "AdminAccount",
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
                name: "IX_AdminAccountMeta_AdminAccountId",
                table: "AdminAccountMeta",
                column: "AdminAccountId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AdminAccountMeta");

            migrationBuilder.UpdateData(
                table: "AccountStatus",
                keyColumn: "Id",
                keyValue: 1001,
                column: "CreatedTime",
                value: new DateTime(2023, 8, 10, 17, 6, 33, 660, DateTimeKind.Local).AddTicks(395));

            migrationBuilder.UpdateData(
                table: "AccountStatus",
                keyColumn: "Id",
                keyValue: 1002,
                column: "CreatedTime",
                value: new DateTime(2023, 8, 10, 17, 6, 33, 660, DateTimeKind.Local).AddTicks(441));

            migrationBuilder.UpdateData(
                table: "AdminAccount",
                keyColumn: "Id",
                keyValue: 1001L,
                column: "CreatedTime",
                value: new DateTime(2023, 8, 10, 17, 6, 33, 660, DateTimeKind.Local).AddTicks(463));

            migrationBuilder.UpdateData(
                table: "CandidateLevel",
                keyColumn: "Id",
                keyValue: 1001,
                column: "CreatedTime",
                value: new DateTime(2023, 8, 10, 17, 6, 33, 660, DateTimeKind.Local).AddTicks(504));

            migrationBuilder.UpdateData(
                table: "EmployerServicePackage",
                keyColumn: "Id",
                keyValue: 1001,
                column: "CreatedTime",
                value: new DateTime(2023, 8, 10, 17, 6, 33, 660, DateTimeKind.Local).AddTicks(561));

            migrationBuilder.UpdateData(
                table: "Position",
                keyColumn: "Id",
                keyValue: 1001,
                column: "CreatedTime",
                value: new DateTime(2023, 8, 10, 17, 6, 33, 660, DateTimeKind.Local).AddTicks(527));
        }
    }
}
