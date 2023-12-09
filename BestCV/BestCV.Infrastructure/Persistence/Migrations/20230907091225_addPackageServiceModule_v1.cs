using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BestCV.Infrastructure.Persistence.Migrations
{
    public partial class addPackageServiceModule_v1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Employer_EmployerServicePackage",
                table: "Employer");

            migrationBuilder.DropIndex(
                name: "IX_TopFeatureJob_JobId",
                table: "TopFeatureJob");

            migrationBuilder.DropIndex(
                name: "IX_Employer_EmployerServicePackageId",
                table: "Employer");

            migrationBuilder.DropColumn(
                name: "ExpiryTime",
                table: "EmployerServicePackage");

            migrationBuilder.DropColumn(
                name: "EmployerServicePackageId",
                table: "Employer");

            migrationBuilder.AddColumn<int>(
                name: "SubOrderSort",
                table: "Slide",
                type: "int",
                nullable: false,
                defaultValue: 0,
                comment: "Thứ tự sắp xếp phụ giữa các order sort cùng bậc");

            migrationBuilder.AddColumn<int>(
                name: "ServicePackageGroupId",
                table: "EmployerServicePackage",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "ServicePackageTypeId",
                table: "EmployerServicePackage",
                type: "int",
                nullable: true);

            migrationBuilder.AlterColumn<long>(
                name: "CandidateId",
                table: "CandidateNotification",
                type: "bigint",
                nullable: true,
                comment: "Mã ứng viên",
                oldClrType: typeof(long),
                oldType: "bigint",
                oldComment: "Mã ứng viên");

            migrationBuilder.CreateTable(
                name: "Benefit",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false, comment: "Mã")
                        .Annotation("SqlServer:Identity", "1001, 1"),
                    Active = table.Column<bool>(type: "bit", nullable: false, defaultValueSql: "((1))", comment: "Đánh dấu bị xóa"),
                    CreatedTime = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "(getdate())", comment: "Ngày tạo"),
                    Name = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false, comment: "Tên"),
                    Description = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true, comment: "Mô tả")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Benefit", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "JobAggreable",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false, comment: "Mã")
                        .Annotation("SqlServer:Identity", "1001, 1"),
                    Description = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true, comment: "Mô tả"),
                    JobId = table.Column<long>(type: "bigint", nullable: false),
                    Active = table.Column<bool>(type: "bit", nullable: false, defaultValueSql: "((1))", comment: "Đánh dấu bị xóa"),
                    CreatedTime = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "(getdate())", comment: "Ngày tạo")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_JobAggreable", x => x.Id);
                    table.ForeignKey(
                        name: "FK_JobAggreable_Job",
                        column: x => x.JobId,
                        principalTable: "Job",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "JobReference",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false, comment: "Mã")
                        .Annotation("SqlServer:Identity", "1001, 1"),
                    Description = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true, comment: "Mô tả"),
                    JobId = table.Column<long>(type: "bigint", nullable: false),
                    Active = table.Column<bool>(type: "bit", nullable: false, defaultValueSql: "((1))", comment: "Đánh dấu bị xóa"),
                    CreatedTime = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "(getdate())", comment: "Ngày tạo")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_JobReference", x => x.Id);
                    table.ForeignKey(
                        name: "FK_JobReference_Job",
                        column: x => x.JobId,
                        principalTable: "Job",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "JobServicePackage",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false, comment: "Mã")
                        .Annotation("SqlServer:Identity", "1001, 1"),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    JobId = table.Column<long>(type: "bigint", nullable: false),
                    ExpireTime = table.Column<int>(type: "int", nullable: true, comment: "Hạn sử dụng"),
                    Value = table.Column<int>(type: "int", nullable: true, comment: "Giá trị"),
                    EmployerServicePackageId = table.Column<int>(type: "int", nullable: false),
                    Active = table.Column<bool>(type: "bit", nullable: false, defaultValueSql: "((1))", comment: "Đánh dấu bị xóa"),
                    CreatedTime = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "(getdate())", comment: "Ngày tạo")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_JobServicePackage", x => x.Id);
                    table.ForeignKey(
                        name: "FK_JobServicePackage_EmployerServicePackage",
                        column: x => x.EmployerServicePackageId,
                        principalTable: "EmployerServicePackage",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_JobServicePackage_Job",
                        column: x => x.JobId,
                        principalTable: "Job",
                        principalColumn: "Id");
                });

            //migrationBuilder.CreateTable(
            //    name: "MenuType",
            //    columns: table => new
            //    {
            //        Id = table.Column<int>(type: "int", nullable: false, comment: "Mã loại menu")
            //            .Annotation("SqlServer:Identity", "1001, 1"),
            //        Active = table.Column<bool>(type: "bit", nullable: false, defaultValueSql: "((1))", comment: "Đánh dấu bị xóa"),
            //        CreatedTime = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "(getdate())", comment: "Ngày tạo"),
            //        Name = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false, comment: "Tên"),
            //        Description = table.Column<string>(type: "nvarchar(max)", nullable: true, comment: "Mô tả")
            //    },
            //    constraints: table =>
            //    {
            //        table.PrimaryKey("PK_MenuType", x => x.Id);
            //    });

            migrationBuilder.CreateTable(
                name: "MustBeInterestedCompany",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false, comment: "Mã")
                        .Annotation("SqlServer:Identity", "1001, 1"),
                    Description = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true, comment: "Mô tả"),
                    JobId = table.Column<long>(type: "bigint", nullable: false),
                    Active = table.Column<bool>(type: "bit", nullable: false, defaultValueSql: "((1))", comment: "Đánh dấu bị xóa"),
                    CreatedTime = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "(getdate())", comment: "Ngày tạo")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MustBeInterestedCompany", x => x.Id);
                    table.ForeignKey(
                        name: "FK_MustBeInterestedCompany_Job",
                        column: x => x.JobId,
                        principalTable: "Job",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "ServicePackageGroup",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false, comment: "Mã")
                        .Annotation("SqlServer:Identity", "1001, 1"),
                    Active = table.Column<bool>(type: "bit", nullable: false, defaultValueSql: "((1))", comment: "Đánh dấu bị xóa"),
                    CreatedTime = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "(getdate())", comment: "Ngày tạo"),
                    Name = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false, comment: "Tên"),
                    Description = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true, comment: "Mô tả")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ServicePackageGroup", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ServicePackageType",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false, comment: "Mã")
                        .Annotation("SqlServer:Identity", "1001, 1"),
                    Active = table.Column<bool>(type: "bit", nullable: false, defaultValueSql: "((1))", comment: "Đánh dấu bị xóa"),
                    CreatedTime = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "(getdate())", comment: "Ngày tạo"),
                    Name = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false, comment: "Tên"),
                    Description = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true, comment: "Mô tả")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ServicePackageType", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "TopJobExtra",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false, comment: "Mã")
                        .Annotation("SqlServer:Identity", "1001, 1"),
                    Description = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true, comment: "Mô tả"),
                    JobId = table.Column<long>(type: "bigint", nullable: false),
                    OrderSort = table.Column<int>(type: "int", nullable: false, comment: "Thứ tự sắp xếp"),
                    Active = table.Column<bool>(type: "bit", nullable: false, defaultValueSql: "((1))", comment: "Đánh dấu bị xóa"),
                    CreatedTime = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "(getdate())", comment: "Ngày tạo")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TopJobExtra", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TopJobExtra_Job",
                        column: x => x.JobId,
                        principalTable: "Job",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "TopJobManagement",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false, comment: "Mã")
                        .Annotation("SqlServer:Identity", "1001, 1"),
                    Description = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true, comment: "Mô tả"),
                    JobId = table.Column<long>(type: "bigint", nullable: false),
                    Active = table.Column<bool>(type: "bit", nullable: false, defaultValueSql: "((1))", comment: "Đánh dấu bị xóa"),
                    CreatedTime = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "(getdate())", comment: "Ngày tạo")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TopJobManagement", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TopJobManagement_Job",
                        column: x => x.JobId,
                        principalTable: "Job",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "TopJobUrgent",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false, comment: "Mã")
                        .Annotation("SqlServer:Identity", "1001, 1"),
                    Description = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true, comment: "Mô tả"),
                    JobId = table.Column<long>(type: "bigint", nullable: false),
                    OrderSort = table.Column<int>(type: "int", nullable: false, comment: "Thứ tự sắp xếp"),
                    Active = table.Column<bool>(type: "bit", nullable: false, defaultValueSql: "((1))", comment: "Đánh dấu bị xóa"),
                    CreatedTime = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "(getdate())", comment: "Ngày tạo")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TopJobUrgent", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TopJobUrgent_Job",
                        column: x => x.JobId,
                        principalTable: "Job",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "WalletType",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false, comment: "Mã")
                        .Annotation("SqlServer:Identity", "1001, 1"),
                    Active = table.Column<bool>(type: "bit", nullable: false, defaultValueSql: "((1))", comment: "Đánh dấu bị xóa"),
                    CreatedTime = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "(getdate())", comment: "Ngày tạo"),
                    Name = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false, comment: "Tên"),
                    Description = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true, comment: "Mô tả")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WalletType", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ServicePackageBenefit",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false, comment: "Mã")
                        .Annotation("SqlServer:Identity", "1001, 1"),
                    EmployerServicePackageId = table.Column<int>(type: "int", nullable: false),
                    BenefitId = table.Column<int>(type: "int", nullable: false),
                    Value = table.Column<int>(type: "int", nullable: false, comment: "Giá trị"),
                    Description = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true, comment: "Mô tả"),
                    Active = table.Column<bool>(type: "bit", nullable: false, defaultValueSql: "((1))", comment: "Đánh dấu bị xóa"),
                    CreatedTime = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "(getdate())", comment: "Ngày tạo")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ServicePackageBenefit", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ServicePackageBenefit_Benefit",
                        column: x => x.BenefitId,
                        principalTable: "Benefit",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_ServicePackageBenefit_EmployerServicePackage",
                        column: x => x.EmployerServicePackageId,
                        principalTable: "EmployerServicePackage",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "EmployerWallet",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false, comment: "Mã")
                        .Annotation("SqlServer:Identity", "1001, 1"),
                    WalletTypeId = table.Column<int>(type: "int", nullable: false),
                    EmployerId = table.Column<long>(type: "bigint", nullable: false),
                    Value = table.Column<int>(type: "int", nullable: false, comment: "Số dư"),
                    Active = table.Column<bool>(type: "bit", nullable: false, defaultValueSql: "((1))", comment: "Đánh dấu bị xóa"),
                    CreatedTime = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "(getdate())", comment: "Ngày tạo")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EmployerWallet", x => x.Id);
                    table.ForeignKey(
                        name: "FK_EmployerWallet_Employer",
                        column: x => x.EmployerId,
                        principalTable: "Employer",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_EmployerWallet_WalletType",
                        column: x => x.WalletTypeId,
                        principalTable: "WalletType",
                        principalColumn: "Id");
                });

            migrationBuilder.UpdateData(
                table: "AccountStatus",
                keyColumn: "Id",
                keyValue: 1001,
                column: "CreatedTime",
                value: new DateTime(2023, 9, 7, 16, 12, 20, 352, DateTimeKind.Local).AddTicks(6171));

            migrationBuilder.UpdateData(
                table: "AccountStatus",
                keyColumn: "Id",
                keyValue: 1002,
                column: "CreatedTime",
                value: new DateTime(2023, 9, 7, 16, 12, 20, 352, DateTimeKind.Local).AddTicks(6226));

            migrationBuilder.UpdateData(
                table: "AdminAccount",
                keyColumn: "Id",
                keyValue: 1001L,
                column: "CreatedTime",
                value: new DateTime(2023, 9, 7, 16, 12, 20, 352, DateTimeKind.Local).AddTicks(6261));

            migrationBuilder.UpdateData(
                table: "CandidateLevel",
                keyColumn: "Id",
                keyValue: 1001,
                column: "CreatedTime",
                value: new DateTime(2023, 9, 7, 16, 12, 20, 352, DateTimeKind.Local).AddTicks(6301));

            migrationBuilder.UpdateData(
                table: "EmployerServicePackage",
                keyColumn: "Id",
                keyValue: 1001,
                column: "CreatedTime",
                value: new DateTime(2023, 9, 7, 16, 12, 20, 352, DateTimeKind.Local).AddTicks(6364));

            migrationBuilder.UpdateData(
                table: "Position",
                keyColumn: "Id",
                keyValue: 1001,
                column: "CreatedTime",
                value: new DateTime(2023, 9, 7, 16, 12, 20, 352, DateTimeKind.Local).AddTicks(6337));

            migrationBuilder.CreateIndex(
                name: "IX_TopFeatureJob_JobId",
                table: "TopFeatureJob",
                column: "JobId");

            //migrationBuilder.CreateIndex(
            //    name: "IX_Menu_MenuTypeId",
            //    table: "Menu",
            //    column: "MenuTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_EmployerServicePackage_ServicePackageGroupId",
                table: "EmployerServicePackage",
                column: "ServicePackageGroupId");

            migrationBuilder.CreateIndex(
                name: "IX_EmployerServicePackage_ServicePackageTypeId",
                table: "EmployerServicePackage",
                column: "ServicePackageTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_EmployerWallet_EmployerId_WalletTypeId",
                table: "EmployerWallet",
                columns: new[] { "EmployerId", "WalletTypeId" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_EmployerWallet_WalletTypeId",
                table: "EmployerWallet",
                column: "WalletTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_JobAggreable_JobId",
                table: "JobAggreable",
                column: "JobId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_JobReference_JobId",
                table: "JobReference",
                column: "JobId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_JobServicePackage_EmployerServicePackageId",
                table: "JobServicePackage",
                column: "EmployerServicePackageId");

            migrationBuilder.CreateIndex(
                name: "IX_JobServicePackage_JobId_EmployerServicePackageId",
                table: "JobServicePackage",
                columns: new[] { "JobId", "EmployerServicePackageId" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_MustBeInterestedCompany_JobId",
                table: "MustBeInterestedCompany",
                column: "JobId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_ServicePackageBenefit_BenefitId",
                table: "ServicePackageBenefit",
                column: "BenefitId");

            migrationBuilder.CreateIndex(
                name: "IX_ServicePackageBenefit_EmployerServicePackageId_BenefitId",
                table: "ServicePackageBenefit",
                columns: new[] { "EmployerServicePackageId", "BenefitId" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_TopJobExtra_JobId",
                table: "TopJobExtra",
                column: "JobId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_TopJobManagement_JobId",
                table: "TopJobManagement",
                column: "JobId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_TopJobUrgent_JobId",
                table: "TopJobUrgent",
                column: "JobId",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_EmployerServicePackage_ServicePackageGroup_ServicePackageGroupId",
                table: "EmployerServicePackage",
                column: "ServicePackageGroupId",
                principalTable: "ServicePackageGroup",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_EmployerServicePackage_ServicePackageType_ServicePackageTypeId",
                table: "EmployerServicePackage",
                column: "ServicePackageTypeId",
                principalTable: "ServicePackageType",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Menu_MenuType_MenuTypeId",
                table: "Menu",
                column: "MenuTypeId",
                principalTable: "MenuType",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_EmployerServicePackage_ServicePackageGroup_ServicePackageGroupId",
                table: "EmployerServicePackage");

            migrationBuilder.DropForeignKey(
                name: "FK_EmployerServicePackage_ServicePackageType_ServicePackageTypeId",
                table: "EmployerServicePackage");

            migrationBuilder.DropForeignKey(
                name: "FK_Menu_MenuType_MenuTypeId",
                table: "Menu");

            migrationBuilder.DropTable(
                name: "EmployerWallet");

            migrationBuilder.DropTable(
                name: "JobAggreable");

            migrationBuilder.DropTable(
                name: "JobReference");

            migrationBuilder.DropTable(
                name: "JobServicePackage");

            migrationBuilder.DropTable(
                name: "MenuType");

            migrationBuilder.DropTable(
                name: "MustBeInterestedCompany");

            migrationBuilder.DropTable(
                name: "ServicePackageBenefit");

            migrationBuilder.DropTable(
                name: "ServicePackageGroup");

            migrationBuilder.DropTable(
                name: "ServicePackageType");

            migrationBuilder.DropTable(
                name: "TopJobExtra");

            migrationBuilder.DropTable(
                name: "TopJobManagement");

            migrationBuilder.DropTable(
                name: "TopJobUrgent");

            migrationBuilder.DropTable(
                name: "WalletType");

            migrationBuilder.DropTable(
                name: "Benefit");

            migrationBuilder.DropIndex(
                name: "IX_TopFeatureJob_JobId",
                table: "TopFeatureJob");

            migrationBuilder.DropIndex(
                name: "IX_Menu_MenuTypeId",
                table: "Menu");

            migrationBuilder.DropIndex(
                name: "IX_EmployerServicePackage_ServicePackageGroupId",
                table: "EmployerServicePackage");

            migrationBuilder.DropIndex(
                name: "IX_EmployerServicePackage_ServicePackageTypeId",
                table: "EmployerServicePackage");

            migrationBuilder.DropColumn(
                name: "SubOrderSort",
                table: "Slide");

            migrationBuilder.DropColumn(
                name: "MenuTypeId",
                table: "Menu");

            migrationBuilder.DropColumn(
                name: "ServicePackageGroupId",
                table: "EmployerServicePackage");

            migrationBuilder.DropColumn(
                name: "ServicePackageTypeId",
                table: "EmployerServicePackage");

            migrationBuilder.AddColumn<int>(
                name: "ExpiryTime",
                table: "EmployerServicePackage",
                type: "int",
                nullable: false,
                defaultValue: 0,
                comment: "Thời gian hết hạn");

            migrationBuilder.AddColumn<int>(
                name: "EmployerServicePackageId",
                table: "Employer",
                type: "int",
                nullable: false,
                defaultValue: 0,
                comment: "Mã gói dịch vụ");

            migrationBuilder.AlterColumn<long>(
                name: "CandidateId",
                table: "CandidateNotification",
                type: "bigint",
                nullable: false,
                defaultValue: 0L,
                comment: "Mã ứng viên",
                oldClrType: typeof(long),
                oldType: "bigint",
                oldNullable: true,
                oldComment: "Mã ứng viên");

            migrationBuilder.UpdateData(
                table: "AccountStatus",
                keyColumn: "Id",
                keyValue: 1001,
                column: "CreatedTime",
                value: new DateTime(2023, 8, 31, 15, 3, 2, 737, DateTimeKind.Local).AddTicks(2898));

            migrationBuilder.UpdateData(
                table: "AccountStatus",
                keyColumn: "Id",
                keyValue: 1002,
                column: "CreatedTime",
                value: new DateTime(2023, 8, 31, 15, 3, 2, 737, DateTimeKind.Local).AddTicks(2936));

            migrationBuilder.UpdateData(
                table: "AdminAccount",
                keyColumn: "Id",
                keyValue: 1001L,
                column: "CreatedTime",
                value: new DateTime(2023, 8, 31, 15, 3, 2, 737, DateTimeKind.Local).AddTicks(2964));

            migrationBuilder.UpdateData(
                table: "CandidateLevel",
                keyColumn: "Id",
                keyValue: 1001,
                column: "CreatedTime",
                value: new DateTime(2023, 8, 31, 15, 3, 2, 737, DateTimeKind.Local).AddTicks(3004));

            migrationBuilder.UpdateData(
                table: "EmployerServicePackage",
                keyColumn: "Id",
                keyValue: 1001,
                column: "CreatedTime",
                value: new DateTime(2023, 8, 31, 15, 3, 2, 737, DateTimeKind.Local).AddTicks(3067));

            migrationBuilder.UpdateData(
                table: "Position",
                keyColumn: "Id",
                keyValue: 1001,
                column: "CreatedTime",
                value: new DateTime(2023, 8, 31, 15, 3, 2, 737, DateTimeKind.Local).AddTicks(3032));

            migrationBuilder.CreateIndex(
                name: "IX_TopFeatureJob_JobId",
                table: "TopFeatureJob",
                column: "JobId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Employer_EmployerServicePackageId",
                table: "Employer",
                column: "EmployerServicePackageId");

            migrationBuilder.AddForeignKey(
                name: "FK_Employer_EmployerServicePackage",
                table: "Employer",
                column: "EmployerServicePackageId",
                principalTable: "EmployerServicePackage",
                principalColumn: "Id");
        }
    }
}
