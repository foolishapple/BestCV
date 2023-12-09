using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Jobi.Infrastructure.Persistence.Migrations
{
    public partial class add_table_menutype_v1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "MenuTypeId",
                table: "Menu",
                type: "int",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "MenuType",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false, comment: "Mã loại menu")
                        .Annotation("SqlServer:Identity", "1001, 1"),
                    Active = table.Column<bool>(type: "bit", nullable: false, defaultValueSql: "((1))", comment: "Đánh dấu bị xóa"),
                    CreatedTime = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "(getdate())", comment: "Ngày tạo"),
                    Name = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false, comment: "Tên"),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true, comment: "Mô tả")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MenuType", x => x.Id);
                });

            migrationBuilder.UpdateData(
                table: "AccountStatus",
                keyColumn: "Id",
                keyValue: 1001,
                column: "CreatedTime",
                value: new DateTime(2023, 8, 31, 11, 6, 41, 591, DateTimeKind.Local).AddTicks(7986));

            migrationBuilder.UpdateData(
                table: "AccountStatus",
                keyColumn: "Id",
                keyValue: 1002,
                column: "CreatedTime",
                value: new DateTime(2023, 8, 31, 11, 6, 41, 591, DateTimeKind.Local).AddTicks(8018));

            migrationBuilder.UpdateData(
                table: "AdminAccount",
                keyColumn: "Id",
                keyValue: 1001L,
                column: "CreatedTime",
                value: new DateTime(2023, 8, 31, 11, 6, 41, 591, DateTimeKind.Local).AddTicks(8042));

            migrationBuilder.UpdateData(
                table: "CandidateLevel",
                keyColumn: "Id",
                keyValue: 1001,
                column: "CreatedTime",
                value: new DateTime(2023, 8, 31, 11, 6, 41, 591, DateTimeKind.Local).AddTicks(8078));

            migrationBuilder.UpdateData(
                table: "EmployerServicePackage",
                keyColumn: "Id",
                keyValue: 1001,
                column: "CreatedTime",
                value: new DateTime(2023, 8, 31, 11, 6, 41, 591, DateTimeKind.Local).AddTicks(8115));

            migrationBuilder.UpdateData(
                table: "Position",
                keyColumn: "Id",
                keyValue: 1001,
                column: "CreatedTime",
                value: new DateTime(2023, 8, 31, 11, 6, 41, 591, DateTimeKind.Local).AddTicks(8100));

            migrationBuilder.CreateIndex(
                name: "IX_Menu_MenuTypeId",
                table: "Menu",
                column: "MenuTypeId");

            migrationBuilder.AddForeignKey(
                name: "FK_Menu_MenuType",
                table: "Menu",
                column: "MenuTypeId",
                principalTable: "MenuType",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Menu_MenuType",
                table: "Menu");

            migrationBuilder.DropTable(
                name: "MenuType");

            migrationBuilder.DropIndex(
                name: "IX_Menu_MenuTypeId",
                table: "Menu");

            migrationBuilder.DropColumn(
                name: "MenuTypeId",
                table: "Menu");

            migrationBuilder.UpdateData(
                table: "AccountStatus",
                keyColumn: "Id",
                keyValue: 1001,
                column: "CreatedTime",
                value: new DateTime(2023, 8, 29, 17, 24, 29, 134, DateTimeKind.Local).AddTicks(7531));

            migrationBuilder.UpdateData(
                table: "AccountStatus",
                keyColumn: "Id",
                keyValue: 1002,
                column: "CreatedTime",
                value: new DateTime(2023, 8, 29, 17, 24, 29, 134, DateTimeKind.Local).AddTicks(7730));

            migrationBuilder.UpdateData(
                table: "AdminAccount",
                keyColumn: "Id",
                keyValue: 1001L,
                column: "CreatedTime",
                value: new DateTime(2023, 8, 29, 17, 24, 29, 134, DateTimeKind.Local).AddTicks(7838));

            migrationBuilder.UpdateData(
                table: "CandidateLevel",
                keyColumn: "Id",
                keyValue: 1001,
                column: "CreatedTime",
                value: new DateTime(2023, 8, 29, 17, 24, 29, 134, DateTimeKind.Local).AddTicks(7932));

            migrationBuilder.UpdateData(
                table: "EmployerServicePackage",
                keyColumn: "Id",
                keyValue: 1001,
                column: "CreatedTime",
                value: new DateTime(2023, 8, 29, 17, 24, 29, 134, DateTimeKind.Local).AddTicks(8291));

            migrationBuilder.UpdateData(
                table: "Position",
                keyColumn: "Id",
                keyValue: 1001,
                column: "CreatedTime",
                value: new DateTime(2023, 8, 29, 17, 24, 29, 134, DateTimeKind.Local).AddTicks(8200));
        }
    }
}
