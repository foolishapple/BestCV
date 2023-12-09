using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BestCV.Infrastructure.Persistence.Migrations
{
    public partial class init_table_1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AccountStatus",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false, comment: "Mã trạng thái tài khoản")
                        .Annotation("SqlServer:Identity", "1001, 1"),
                    Color = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Active = table.Column<bool>(type: "bit", nullable: false, defaultValueSql: "((1))", comment: "Đánh dấu bị xóa"),
                    CreatedTime = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "(getdate())", comment: "Ngày tạo"),
                    Name = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false, comment: "Tên trạng thái"),
                    Description = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true, comment: "Mô tả")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AccountStatus", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AdminAccount",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false, comment: "Mã tài khoản admin")
                        .Annotation("SqlServer:Identity", "1001, 1"),
                    Search = table.Column<string>(type: "varchar(MAX)", nullable: false, comment: "Trường search không dấu"),
                    FullName = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    Email = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    UserName = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false, comment: "Tên đăng nhập"),
                    Password = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false, comment: "Mật khẩu"),
                    Phone = table.Column<string>(type: "nvarchar(25)", maxLength: 25, nullable: false, comment: "Số điện thoại"),
                    Photo = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true, comment: "Đường dẫn ảnh"),
                    Description = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true, comment: "Mô tả"),
                    AccessFailedCount = table.Column<int>(type: "int", nullable: false, defaultValue: 0),
                    LockEnabled = table.Column<bool>(type: "bit", nullable: false, defaultValue: false),
                    LockEndDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Active = table.Column<bool>(type: "bit", nullable: false, defaultValueSql: "((1))", comment: "Đánh dấu bị xóa"),
                    CreatedTime = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "(getdate())", comment: "Ngày tạo")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AdminAccount", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "CandidateApplyJobSource",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false, comment: "Mã ứng viên ứng tuyền việc làm")
                        .Annotation("SqlServer:Identity", "1001, 1"),
                    Active = table.Column<bool>(type: "bit", nullable: false, defaultValueSql: "((1))", comment: "Đánh dấu bị xóa"),
                    CreatedTime = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "(getdate())", comment: "Ngày tạo"),
                    Name = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false, comment: "Tên ứng viên ứng tuyển việc làm"),
                    Description = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true, comment: "Mô tả")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CandidateApplyJobSource", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "CandidateApplyJobStatus",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false, comment: "Mã tình trạng ứng viên ứng tuyển công việc")
                        .Annotation("SqlServer:Identity", "1001, 1"),
                    Color = table.Column<string>(type: "varchar(12)", nullable: false, comment: "Màu"),
                    Active = table.Column<bool>(type: "bit", nullable: false, defaultValueSql: "((1))", comment: "Đánh dấu bị xóa"),
                    CreatedTime = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "(getdate())", comment: "Ngày tạo"),
                    Name = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false, comment: "Tên tình trạng ứng viên ứng tuyển công việc"),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true, comment: "Mô tả")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CandidateApplyJobStatus", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "CandidateLevel",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false, comment: "Mã")
                        .Annotation("SqlServer:Identity", "1001, 1"),
                    Price = table.Column<int>(type: "int", nullable: false, comment: "Giá"),
                    DiscountPrice = table.Column<int>(type: "int", nullable: false, comment: "Mã giảm giá"),
                    DiscountEndDate = table.Column<DateTime>(type: "datetime2", nullable: true, comment: "Thời gian của mã giảm giá"),
                    ExpiryTime = table.Column<int>(type: "int", nullable: false, comment: "Thời gian trước khi hết hạn"),
                    Active = table.Column<bool>(type: "bit", nullable: false, defaultValueSql: "((1))", comment: "Đánh dấu bị xóa"),
                    CreatedTime = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "(getdate())", comment: "Ngày tạo"),
                    Name = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false, comment: "Tên cấp ứng viên"),
                    Description = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true, comment: "Mô tả")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CandidateLevel", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "CandidateLevelBenefit",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false, comment: "Mã mức độ lợi ích của ứng viên ")
                        .Annotation("SqlServer:Identity", "1001, 1"),
                    Active = table.Column<bool>(type: "bit", nullable: false, defaultValueSql: "((1))", comment: "Đánh dấu bị xóa"),
                    CreatedTime = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "(getdate())", comment: "Ngày tạo"),
                    Name = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false, comment: "Tên mức độ lợi ích của ứng viên"),
                    Description = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true, comment: "Mô tả")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CandidateLevelBenefit", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "CandidateSkill",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false, comment: "Mã kỹ năng ứng viên")
                        .Annotation("SqlServer:Identity", "1001, 1"),
                    SkillId = table.Column<int>(type: "int", nullable: false),
                    CandidateId = table.Column<long>(type: "bigint", nullable: false),
                    Active = table.Column<bool>(type: "bit", nullable: false, defaultValueSql: "((1))", comment: "Đánh dấu bị xóa"),
                    CreatedTime = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "(getdate())", comment: "Ngày tạo")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CandidateSkill", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "CompanySize",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false, comment: "Mã quy mô công ty")
                        .Annotation("SqlServer:Identity", "1001, 1"),
                    Active = table.Column<bool>(type: "bit", nullable: false, defaultValue: true, comment: "Đánh dấu bị xóa"),
                    CreatedTime = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "getdate()", comment: "Ngày tạo"),
                    Name = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false, comment: "Tên quy mô công ty"),
                    Description = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true, comment: "Mô tả")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CompanySize", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "CouponType",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false, comment: "Mã loại coupon")
                        .Annotation("SqlServer:Identity", "1001, 1"),
                    Active = table.Column<bool>(type: "bit", nullable: false, defaultValueSql: "((1))", comment: "Đánh dấu bị xóa"),
                    CreatedTime = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "(getdate())", comment: "Ngày tạo"),
                    Name = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false, comment: "Tên loại coupon"),
                    Description = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true, comment: "Mô tả")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CouponType", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "EmployerActivityLogType",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false, comment: "Mã loại lịch sử hoạt động của nhà tuyển dụng")
                        .Annotation("SqlServer:Identity", "1001, 1"),
                    Active = table.Column<bool>(type: "bit", nullable: false, defaultValueSql: "((1))", comment: "Đánh dấu bị xóa"),
                    CreatedTime = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "(getdate())", comment: "Ngày tạo"),
                    Name = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false, comment: "Tên loại lịch sử hoạt động của nhà tuyển dụng"),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true, comment: "Mô tả")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EmployerActivityLogType", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "EmployerBenefit",
                columns: table => new
                {
                    Mãlợiíchcủanhàtuyểndụng = table.Column<int>(name: "Mã lợi ích của nhà tuyển dụng", type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1001, 1"),
                    Active = table.Column<bool>(type: "bit", nullable: false, defaultValueSql: "((1))", comment: "Đánh dấu bị xóa"),
                    CreatedTime = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "(getdate())", comment: "Ngày tạo"),
                    Name = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false, comment: "Tên lợi ích cho nhà tuyển dụng"),
                    Description = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true, comment: "Mô tả")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EmployerBenefit", x => x.Mãlợiíchcủanhàtuyểndụng);
                });

            migrationBuilder.CreateTable(
                name: "EmployerServicePackage",
                columns: table => new
                {
                    Mãgóidịchvụchonhàtuyểndụng = table.Column<int>(name: "Mã gói dịch vụ cho nhà tuyển dụng", type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1001, 1"),
                    Name = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false, comment: "Tên gói lợi ích cho nhà tuyển dụng"),
                    Price = table.Column<int>(type: "int", nullable: false, comment: "Giá"),
                    DiscountPrice = table.Column<int>(type: "int", nullable: false, comment: "Giảm giá"),
                    DiscountEndDate = table.Column<DateTime>(type: "datetime2", nullable: true, defaultValueSql: "(getdate())", comment: "Ngày kết thúc mã giảm giá"),
                    ExpiryTime = table.Column<int>(type: "int", nullable: false, comment: "Thời gian hết hạn"),
                    Description = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true, comment: "Mô tả"),
                    Active = table.Column<bool>(type: "bit", nullable: false, defaultValueSql: "((1))", comment: "Đánh dấu bị xóa"),
                    CreatedTime = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "(getdate())", comment: "Ngày tạo")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EmployerServicePackage", x => x.Mãgóidịchvụchonhàtuyểndụng);
                });

            migrationBuilder.CreateTable(
                name: "ExperienceRange",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false, comment: "Mã phạm vi kinh nghiệm")
                        .Annotation("SqlServer:Identity", "1001, 1"),
                    Active = table.Column<bool>(type: "bit", nullable: false, defaultValueSql: "((1))", comment: "Đánh dấu bị xóa"),
                    CreatedTime = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "(getdate())", comment: "Ngày tạo"),
                    Name = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false, comment: "Tên phạm vi kinh nghiệm"),
                    Description = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true, comment: "Mô tả")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ExperienceRange", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "FieldOfActivity",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false, comment: "Mã lĩnh vực hoạt động")
                        .Annotation("SqlServer:Identity", "1001, 1"),
                    Active = table.Column<bool>(type: "bit", nullable: false, defaultValueSql: "((1))", comment: "Đánh dấu bị xóa"),
                    CreatedTime = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "(getdate())", comment: "Ngày tạo"),
                    Name = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false, comment: "Tên lĩnh vực hoạt động"),
                    Description = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true, comment: "Mô tả")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FieldOfActivity", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "FolderUpload",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false, comment: "Mã thư mục")
                        .Annotation("SqlServer:Identity", "1001, 1"),
                    Path = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false, comment: "Đường dẫn"),
                    ParentId = table.Column<int>(type: "int", nullable: true, comment: "Mã thư mục cha"),
                    TreeIds = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false, comment: "Để truy cập và quản lý các nút trong cây thư mục"),
                    Active = table.Column<bool>(type: "bit", nullable: false, defaultValueSql: "((1))", comment: "Đánh dấu bị xóa"),
                    CreatedTime = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "(getdate())", comment: "Ngày tạo"),
                    Name = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false, comment: "Tên"),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FolderUpload", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "InterviewStatus",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false, comment: "Mã trạng thái phỏng vấn")
                        .Annotation("SqlServer:Identity", "1001, 1"),
                    Color = table.Column<string>(type: "nvarchar(12)", maxLength: 12, nullable: false, comment: "Màu trạng thái phỏng vấn"),
                    Active = table.Column<bool>(type: "bit", nullable: false, defaultValueSql: "((1))", comment: "Đánh dấu bị xóa"),
                    CreatedTime = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "(getdate())", comment: "Ngày tạo"),
                    Name = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false, comment: "Tên trạng thái phỏng vấn"),
                    Description = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true, comment: "Mô tả")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_InterviewStatus", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "InterviewType",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false, comment: "Mã loại phỏng vấn")
                        .Annotation("SqlServer:Identity", "1001, 1"),
                    Active = table.Column<bool>(type: "bit", nullable: false, defaultValueSql: "((1))", comment: "Đánh dấu bị xóa"),
                    CreatedTime = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "(getdate())", comment: "Ngày tạo"),
                    Name = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false, comment: "Tên loại phỏng vấn"),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true, comment: "Mô tả")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_InterviewType", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "JobCategory",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false, comment: "Mã danh mục công việc")
                        .Annotation("SqlServer:Identity", "1001, 1"),
                    Active = table.Column<bool>(type: "bit", nullable: false, defaultValueSql: "((1))", comment: "Đánh dấu bị xóa"),
                    CreatedTime = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "(getdate())", comment: "Ngày tạo"),
                    Name = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false, comment: "Tên danh mục công việc"),
                    Description = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true, comment: "Mô tả")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_JobCategory", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "JobPosition",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false, comment: "Mã vị trí công việc")
                        .Annotation("SqlServer:Identity", "1001, 1"),
                    Active = table.Column<bool>(type: "bit", nullable: false, defaultValueSql: "((1))", comment: "Đánh dấu bị xóa"),
                    CreatedTime = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "(getdate())", comment: "Ngày tạo"),
                    Name = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false, comment: "Tên vị trí công việc"),
                    Description = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true, comment: "Mô tả")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_JobPosition", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "JobSkill",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false, comment: "Mã kỹ năng")
                        .Annotation("SqlServer:Identity", "1001, 1"),
                    Active = table.Column<bool>(type: "bit", nullable: false, defaultValueSql: "((1))", comment: "Đánh dấu bị xóa"),
                    CreatedTime = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "(getdate())", comment: "Ngày tạo"),
                    Name = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false, comment: "Tên kỹ năng"),
                    Description = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true, comment: "Mô tả")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_JobSkill", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "JobStatus",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false, comment: "Mã trạng thái công việc")
                        .Annotation("SqlServer:Identity", "1001, 1"),
                    Color = table.Column<string>(type: "varchar(12)", nullable: false, comment: "Màu"),
                    Active = table.Column<bool>(type: "bit", nullable: false, defaultValueSql: "((1))", comment: "Đánh dấu bị xóa"),
                    CreatedTime = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "(getdate())", comment: "Ngày tạo"),
                    Name = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false, comment: "Tên trạng thái công việc"),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true, comment: "Mô tả")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_JobStatus", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "JobType",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false, comment: "Mã loại công việc")
                        .Annotation("SqlServer:Identity", "1001, 1"),
                    Active = table.Column<bool>(type: "bit", nullable: false, defaultValueSql: "((1))", comment: "Đánh dấu bị xóa"),
                    CreatedTime = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "(getdate())", comment: "Ngày tạo"),
                    Name = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false, comment: "Tên loại công việc"),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true, comment: "Mô tả")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_JobType", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "LicenseType",
                columns: table => new
                {
                    Mãloạigiấytờ = table.Column<int>(name: "Mã loại giấy tờ", type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1001, 1"),
                    Active = table.Column<bool>(type: "bit", nullable: false, defaultValueSql: "((1))", comment: "Đánh dấu bị xóa"),
                    CreatedTime = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "(getdate())", comment: "Ngày tạo"),
                    Name = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false, comment: "Tên loại giấy tờ"),
                    Description = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true, comment: "Mô tả")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LicenseType", x => x.Mãloạigiấytờ);
                });

            migrationBuilder.CreateTable(
                name: "Menu",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false, comment: "Mã menu")
                        .Annotation("SqlServer:Identity", "1001, 1"),
                    ParentId = table.Column<int>(type: "int", nullable: true, comment: "Mã cấp danh mục"),
                    Icon = table.Column<string>(type: "nvarchar(max)", nullable: true, comment: "Icon"),
                    Link = table.Column<string>(type: "nvarchar(max)", nullable: true, comment: "Đường dẫn"),
                    TreeIds = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false, comment: "Để truy cập và quản lý các nút trong cây thư mục"),
                    Active = table.Column<bool>(type: "bit", nullable: false, defaultValueSql: "((1))", comment: "Đánh dấu bị xóa"),
                    CreatedTime = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "(getdate())", comment: "Ngày tạo"),
                    Name = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false, comment: "Tên"),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true, comment: "Mô tả")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Menu", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "MultimediaType",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false, comment: "Mã loại đa phương tiện")
                        .Annotation("SqlServer:Identity", "1001, 1"),
                    Active = table.Column<bool>(type: "bit", nullable: false, defaultValueSql: "((1))", comment: "Đánh dấu bị xóa"),
                    CreatedTime = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "(getdate())", comment: "Ngày tạo"),
                    Name = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false, comment: "Tên loại đa phương tiện"),
                    Description = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true, comment: "Mô tả")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MultimediaType", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "NotificationStatus",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false, comment: "Mã trạng thái thông báo")
                        .Annotation("SqlServer:Identity", "1001, 1"),
                    Color = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Active = table.Column<bool>(type: "bit", nullable: false, defaultValueSql: "((1))", comment: "Đánh dấu bị xóa"),
                    CreatedTime = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "(getdate())", comment: "Ngày tạo"),
                    Name = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false, comment: "Tên trạng thái thông báo"),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true, comment: "Mô tả")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_NotificationStatus", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "NotificationType",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false, comment: "Mã loại thông báo")
                        .Annotation("SqlServer:Identity", "1001, 1"),
                    Active = table.Column<bool>(type: "bit", nullable: false, defaultValueSql: "((1))", comment: "Đánh dấu bị xóa"),
                    CreatedTime = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "(getdate())", comment: "Ngày tạo"),
                    Name = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false, comment: "Tên loại thông báo"),
                    Description = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true, comment: "Mô tả")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_NotificationType", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Occupation",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false, comment: "Mã nghề nghiệp")
                        .Annotation("SqlServer:Identity", "1001, 1"),
                    Active = table.Column<bool>(type: "bit", nullable: false, defaultValueSql: "((1))", comment: "Đánh dấu bị xóa"),
                    CreatedTime = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "(getdate())", comment: "Ngày tạo"),
                    Name = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false, comment: "Tên nghề nghiệp"),
                    Description = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true, comment: "Mô tả")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Occupation", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "OrderStatus",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false, comment: "Mã trạng thái đơn hàng")
                        .Annotation("SqlServer:Identity", "1001, 1"),
                    Color = table.Column<string>(type: "nvarchar(12)", maxLength: 12, nullable: false, comment: "Màu trạng thái đơn hàng"),
                    Active = table.Column<bool>(type: "bit", nullable: false, defaultValueSql: "((1))", comment: "Đánh dấu bị xóa"),
                    CreatedTime = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "(getdate())", comment: "Ngày tạo"),
                    Name = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false, comment: "Tên trạng thái đơn hàng"),
                    Description = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true, comment: "Mô tả")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OrderStatus", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "OrderType",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false, comment: "Mã loại hóa đơn")
                        .Annotation("SqlServer:Identity", "1001, 1"),
                    Active = table.Column<bool>(type: "bit", nullable: false, defaultValueSql: "((1))", comment: "Đánh dấu bị xóa"),
                    CreatedTime = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "(getdate())", comment: "Ngày tạo"),
                    Name = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false, comment: "Tên loại hóa đơn"),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true, comment: "Mô tả")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OrderType", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "PaymentMethod",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false, comment: "Mã phương thức thanh toán")
                        .Annotation("SqlServer:Identity", "1001, 1"),
                    Active = table.Column<bool>(type: "bit", nullable: false, defaultValueSql: "((1))", comment: "Đánh dấu bị xóa"),
                    CreatedTime = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "(getdate())", comment: "Ngày tạo"),
                    Name = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false, comment: "Tên phương thức thanh toán"),
                    Description = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true, comment: "Mô tả")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PaymentMethod", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Permission",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false, comment: "Mã quyền truy cập")
                        .Annotation("SqlServer:Identity", "1001, 1"),
                    Code = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false, comment: "Mã Code"),
                    Active = table.Column<bool>(type: "bit", nullable: false, defaultValueSql: "((1))", comment: "Đánh dấu bị xóa"),
                    CreatedTime = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "(getdate())", comment: "Ngày tạo"),
                    Name = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false, comment: "Tên quyền truy cập"),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Permission", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Position",
                columns: table => new
                {
                    Mãchứcvụ = table.Column<int>(name: "Mã chức vụ", type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1001, 1"),
                    Active = table.Column<bool>(type: "bit", nullable: false, defaultValueSql: "((1))", comment: "Đánh dấu bị xóa"),
                    CreatedTime = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "(getdate())", comment: "Ngày tạo"),
                    Name = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false, comment: "Tên chức vụ"),
                    Description = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true, comment: "Mô tả")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Position", x => x.Mãchứcvụ);
                });

            migrationBuilder.CreateTable(
                name: "PostCategory",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false, comment: "Mã danh mục bài viết")
                        .Annotation("SqlServer:Identity", "1001, 1"),
                    ParrentId = table.Column<int>(type: "int", nullable: false, comment: "Mã cấp danh mục bài viết"),
                    Color = table.Column<string>(type: "nvarchar(12)", maxLength: 12, nullable: false, comment: "Màu danh mục bài viết"),
                    Active = table.Column<bool>(type: "bit", nullable: false, defaultValueSql: "((1))", comment: "Đánh dấu bị xóa"),
                    CreatedTime = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "(getdate())", comment: "Ngày tạo"),
                    Name = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false, comment: "Tên danh mục bài viết"),
                    Description = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true, comment: "Mô tả")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PostCategory", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "PostLayout",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false, comment: "Mã layout bài viết")
                        .Annotation("SqlServer:Identity", "1001, 1"),
                    Active = table.Column<bool>(type: "bit", nullable: false, defaultValueSql: "((1))", comment: "Đánh dấu bị xóa"),
                    CreatedTime = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "(getdate())", comment: "Ngày tạo"),
                    Name = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false, comment: "Tên layout bài viết"),
                    Description = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true, comment: "Mô tả")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PostLayout", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "PostStatus",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false, comment: "Mã trạng thái bài viết")
                        .Annotation("SqlServer:Identity", "1001, 1"),
                    Color = table.Column<string>(type: "nvarchar(12)", maxLength: 12, nullable: false, comment: "Màu trạng thái bài viết"),
                    Active = table.Column<bool>(type: "bit", nullable: false, defaultValueSql: "((1))", comment: "Đánh dấu bị xóa"),
                    CreatedTime = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "(getdate())", comment: "Ngày tạo"),
                    Name = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false, comment: "Tên trạng thái bài viết"),
                    Description = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true, comment: "Mô tả")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PostStatus", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "PostType",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false, comment: "Mã loại bài viết")
                        .Annotation("SqlServer:Identity", "1001, 1"),
                    Active = table.Column<bool>(type: "bit", nullable: false, defaultValueSql: "((1))", comment: "Đánh dấu bị xóa"),
                    CreatedTime = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "(getdate())", comment: "Ngày tạo"),
                    Name = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false, comment: "Tên loại bài viết"),
                    Description = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true, comment: "Mô tả")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PostType", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "RecruitmentCampaignStatus",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false, comment: "Mã tình trạng chiến dịch tuyển dụng")
                        .Annotation("SqlServer:Identity", "1001, 1"),
                    Color = table.Column<string>(type: "varchar(12)", nullable: false, comment: "Màu"),
                    Active = table.Column<bool>(type: "bit", nullable: false, defaultValueSql: "((1))", comment: "Đánh dấu bị xóa"),
                    CreatedTime = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "(getdate())", comment: "Ngày tạo"),
                    Name = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false, comment: "Tên tình trạng chiến dịch tuyển dụng"),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true, comment: "Mô tả")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RecruitmentCampaignStatus", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "RecruitmentStatus",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false, comment: "Mã trạng thái chiến dịch tuyển dụng")
                        .Annotation("SqlServer:Identity", "1001, 1"),
                    Color = table.Column<string>(type: "nvarchar(12)", maxLength: 12, nullable: false, comment: "Màu trạng thái chiến dịch tuyển dụng"),
                    Active = table.Column<bool>(type: "bit", nullable: false, defaultValueSql: "((1))", comment: "Đánh dấu bị xóa"),
                    CreatedTime = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "(getdate())", comment: "Ngày tạo"),
                    Name = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false, comment: "Tên trạng thái chiến dịch tuyển dụng"),
                    Description = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true, comment: "Mô tả")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RecruitmentStatus", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Role",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false, comment: "Mã vai trò")
                        .Annotation("SqlServer:Identity", "1001, 1"),
                    Code = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false, comment: "Mã"),
                    Active = table.Column<bool>(type: "bit", nullable: false, defaultValueSql: "((1))", comment: "Đánh dấu bị xóa"),
                    CreatedTime = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "(getdate())", comment: "Ngày tạo"),
                    Name = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false, comment: "Tên"),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Role", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "SalaryRange",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false, comment: "Mã khoảng lương")
                        .Annotation("SqlServer:Identity", "1001, 1"),
                    Active = table.Column<bool>(type: "bit", nullable: false, defaultValueSql: "((1))", comment: "Đánh dấu bị xóa"),
                    CreatedTime = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "(getdate())", comment: "Ngày tạo"),
                    Name = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false, comment: "Tên khoảng lương"),
                    Description = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true, comment: "Mô tả")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SalaryRange", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "SalaryType",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false, comment: "Mã loại lương")
                        .Annotation("SqlServer:Identity", "1001, 1"),
                    Active = table.Column<bool>(type: "bit", nullable: false, defaultValueSql: "((1))", comment: "Đánh dấu bị xóa"),
                    CreatedTime = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "(getdate())", comment: "Ngày tạo"),
                    Name = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false, comment: "Tên loại lương"),
                    Description = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true, comment: "Mô tả")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SalaryType", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Slide",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false, comment: "Mã slide")
                        .Annotation("SqlServer:Identity", "1001, 1"),
                    Image = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false, comment: "Đường dẫn ảnh"),
                    CandidateOrderSort = table.Column<int>(type: "int", nullable: false, comment: "Thứ tự sắp xếp ở màn hình ứng viên"),
                    Active = table.Column<bool>(type: "bit", nullable: false, defaultValueSql: "((1))", comment: "Đánh dấu bị xóa"),
                    CreatedTime = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "(getdate())", comment: "Ngày tạo"),
                    Name = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false, comment: "Tên slide"),
                    Description = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true, comment: "Mô tả")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Slide", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "SystemConfig",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false, comment: "Mã")
                        .Annotation("SqlServer:Identity", "1001, 1"),
                    Key = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    Value = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true, comment: "Mô tả"),
                    Active = table.Column<bool>(type: "bit", nullable: false),
                    CreatedTime = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "(getdate())", comment: "Ngày tạo")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SystemConfig", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "TagType",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false, comment: "Mã loại thẻ")
                        .Annotation("SqlServer:Identity", "1001, 1"),
                    Active = table.Column<bool>(type: "bit", nullable: false, defaultValueSql: "((1))", comment: "Đánh dấu bị xóa"),
                    CreatedTime = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "(getdate())", comment: "Ngày tạo"),
                    Name = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false, comment: "Tên loại thẻ"),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true, comment: "Mô tả")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TagType", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "VoucherType",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false, comment: "Mã loại phiếu giảm giá")
                        .Annotation("SqlServer:Identity", "1001, 1"),
                    Active = table.Column<bool>(type: "bit", nullable: false, defaultValueSql: "((1))", comment: "Đánh dấu bị xóa"),
                    CreatedTime = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "(getdate())", comment: "Ngày tạo"),
                    Name = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false, comment: "Tên loại phiếu giảm giá"),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true, comment: "Mô tả")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_VoucherType", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "WorkPlace",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1001, 1"),
                    ParentId = table.Column<int>(type: "int", nullable: true, comment: "Mã cha"),
                    Code = table.Column<int>(type: "int", maxLength: 255, nullable: false, comment: "Mã đơn vị hành chính"),
                    DivisionType = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false, comment: "Loại đơn vị hành chính"),
                    CodeName = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false, comment: "Tên đơn vị hành chính viết ở dạng snake_case và bỏ dấu"),
                    PhoneCode = table.Column<int>(type: "int", nullable: true, comment: "Mã vùng điện thoại"),
                    ProvinceCode = table.Column<int>(type: "int", nullable: true, comment: "Mã tỉnh thành"),
                    Active = table.Column<bool>(type: "bit", nullable: false, defaultValueSql: "((1))", comment: "Đánh dấu bị xóa"),
                    CreatedTime = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "(getdate())", comment: "Ngày tạo"),
                    Name = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false, comment: "Tên thành phố, quận , huyện"),
                    Description = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true, comment: "Mô tả ")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WorkPlace", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Candidate",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false, comment: "Mã ứng viên")
                        .Annotation("SqlServer:Identity", "1001, 1"),
                    CandidateStatusId = table.Column<int>(type: "int", nullable: false),
                    CandidateLevelId = table.Column<int>(type: "int", nullable: false),
                    Username = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false, comment: "Tên đăng nhập"),
                    FullName = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false, comment: "Tên đầy đủ"),
                    FirstName = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true, comment: "Họ"),
                    MiddleName = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true, comment: "Tên đệm"),
                    LastName = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true, comment: "Tên"),
                    Email = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false, comment: "Địa chỉ email"),
                    Password = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false, comment: "Mật khẩu đăng nhập"),
                    GoogleId = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true, comment: "Mã google"),
                    FacebookId = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true, comment: "Mã fb"),
                    LinkedinId = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true, comment: "Mã linked"),
                    Photo = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true, comment: "Ảnh đại diện"),
                    CoverPhoto = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true, comment: "Ảnh bìa"),
                    Gender = table.Column<int>(type: "int", nullable: false, defaultValueSql: "((3))", comment: "Giới tính"),
                    JobPosition = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true, comment: "Vị trí công việc"),
                    AddressDetail = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true, comment: "Địa chỉ cụ thể"),
                    Interests = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true, comment: "Sở thích"),
                    Objective = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true, comment: "Mục tiêu"),
                    Info = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true, comment: "Thông tin"),
                    DoB = table.Column<DateTime>(type: "datetime2", nullable: true, comment: "Ngày sinh"),
                    Phone = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: false, comment: "Số điện thoại"),
                    IsSubcribeEmailImportantSystemUpdate = table.Column<bool>(type: "bit", nullable: false, comment: "Nhận thông báo quan trọng về cập nhật hệ thông"),
                    IsSubcribeEmailEmployerViewCV = table.Column<bool>(type: "bit", nullable: false, comment: "Nhận thông báo email về nhà tuyển dụng đã xem sơ yếu lí lịch "),
                    IsSubcribeEmailNewFeatureUpdate = table.Column<bool>(type: "bit", nullable: false, comment: "Nhận thông báo email về cập nhật tính năng mới"),
                    IsSubcribeEmailOtherSystemNotification = table.Column<bool>(type: "bit", nullable: false, comment: "Nhận thông báo qua email về hệ thống khác"),
                    IsSubcribeEmailJobSuggestion = table.Column<bool>(type: "bit", nullable: false, comment: "Nhận thông báo qua email về gợi ý công việc"),
                    IsSubcribeEmailEmployerInviteJob = table.Column<bool>(type: "bit", nullable: false, comment: "Nhận thông báo qua email về nhà tuyển dụng mời làm việc"),
                    IsSubcribeEmailServiceIntro = table.Column<bool>(type: "bit", nullable: false, comment: "Nhận thông báo qua email giới thiệu về dịch vụ"),
                    IsSubcribeEmailProgramEventIntro = table.Column<bool>(type: "bit", nullable: false, comment: "Nhận thông báo qua email về chương trình sự kiện giới thiệu"),
                    IsSubcribeEmailGiftCoupon = table.Column<bool>(type: "bit", nullable: false, comment: "Nhận thông báo qua email về gift phiếu giảm giá"),
                    IsCheckOnJobWatting = table.Column<bool>(type: "bit", nullable: false, comment: "Kiểm tra công việc đang chờ"),
                    IsCheckJobOffers = table.Column<bool>(type: "bit", nullable: false, comment: "Kiểm tra lời mời làm việc"),
                    IsCheckViewCV = table.Column<bool>(type: "bit", nullable: false, comment: "Kiểm tra xem sơ yếu lí lịch"),
                    IsCheckTopCVReview = table.Column<bool>(type: "bit", nullable: false, comment: "Kiểm tra đánh giá TopCV"),
                    SuggestionExperienceRangeId = table.Column<int>(type: "int", nullable: false, comment: "Kinh nghiệm làm việc"),
                    SuggestionSalaryRangeId = table.Column<int>(type: "int", nullable: false, comment: "Mức lương"),
                    AccessFailedCount = table.Column<int>(type: "int", nullable: false, comment: "Số lần đăng nhập thất bại"),
                    LockEnabled = table.Column<bool>(type: "bit", nullable: false, comment: "Bị khoá tài khoản?"),
                    LockEndDate = table.Column<DateTime>(type: "datetime2", nullable: true, comment: "Bị khóa đến thời gian nào"),
                    CandidateLevelEfficiencyExpiry = table.Column<DateTime>(type: "datetime2", nullable: true, comment: "Hiệu lực của cấp độ ứng viên"),
                    IsActivated = table.Column<bool>(type: "bit", nullable: false, comment: "Đã xác thực chưa"),
                    Search = table.Column<string>(type: "nvarchar(max)", nullable: false, comment: "Lưu ký tự không dấu của các trường muốn tìm kiếm"),
                    Active = table.Column<bool>(type: "bit", nullable: false, defaultValueSql: "((1))", comment: "Đánh dấu bị xóa"),
                    CreatedTime = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "(getdate())", comment: "Ngày tạo")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Candidate", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Candidate_CandidateLevel",
                        column: x => x.CandidateLevelId,
                        principalTable: "CandidateLevel",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Candidate_CandidateStatus",
                        column: x => x.CandidateStatusId,
                        principalTable: "AccountStatus",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "CandidateLevelCandidateLevelBenefit",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false, comment: "Mã thư mục")
                        .Annotation("SqlServer:Identity", "1001, 1"),
                    CandidateLevelId = table.Column<int>(type: "int", nullable: false),
                    CandidateLevelBenefitId = table.Column<int>(type: "int", nullable: false),
                    Value = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true, comment: "Giá trị"),
                    HasBenefit = table.Column<bool>(type: "bit", nullable: false, defaultValueSql: "((1))", comment: "Quyền lợi được hưởng"),
                    Active = table.Column<bool>(type: "bit", nullable: false, defaultValueSql: "((1))", comment: "Đánh dấu bị xóa"),
                    CreatedTime = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "(getdate())", comment: "Ngày tạo"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CandidateLevelCandidateLevelBenefit", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CandidateLevelCandidateLevelBenefit_CandidateLevel",
                        column: x => x.CandidateLevelId,
                        principalTable: "CandidateLevel",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_CandidateLevelCandidateLevelBenefit_CandidateLevelBenefit",
                        column: x => x.CandidateLevelBenefitId,
                        principalTable: "CandidateLevelBenefit",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Coupon",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false, comment: "Mã coupon")
                        .Annotation("SqlServer:Identity", "1001, 1"),
                    CouponTypeId = table.Column<int>(type: "int", nullable: false, comment: "Loại coupon"),
                    Code = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false, comment: "Mã"),
                    Active = table.Column<bool>(type: "bit", nullable: false, defaultValueSql: "((1))", comment: "Đánh dấu bị xóa"),
                    CreatedTime = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "(getdate())", comment: "Ngày tạo")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Coupon", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CouponType_Coupon",
                        column: x => x.CouponTypeId,
                        principalTable: "CouponType",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "EmployerActivityLog",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false, comment: "Mã lịch sử hoạt động của nhà tuyển dụng")
                        .Annotation("SqlServer:Identity", "1001, 1"),
                    EmployerId = table.Column<long>(type: "bigint", nullable: false),
                    EmployerActivityLogTypeId = table.Column<int>(type: "int", nullable: false),
                    OldValue = table.Column<string>(type: "nvarchar(max)", nullable: true, comment: "Giá trị cũ"),
                    NewValue = table.Column<string>(type: "nvarchar(max)", nullable: true, comment: "Giá trị mới"),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true, comment: "Mô tả"),
                    OS = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true, comment: "Hệ điều hành"),
                    UserAgent = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true, comment: "Giao thức người dùng"),
                    Browser = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true, comment: "Trình duyệt"),
                    IpAddress = table.Column<string>(type: "nvarchar(max)", nullable: true, comment: "Địa chỉ IP"),
                    Active = table.Column<bool>(type: "bit", nullable: false, defaultValueSql: "((1))", comment: "Đánh dấu bị xóa"),
                    CreatedTime = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "(getdate())", comment: "Ngày tạo")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EmployerActivityLog", x => x.Id);
                    table.ForeignKey(
                        name: "FK_EmployerActivityLog_EmployerActivityLogType",
                        column: x => x.EmployerActivityLogTypeId,
                        principalTable: "EmployerActivityLogType",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "EmployerServicePackageEmployerBenefit",
                columns: table => new
                {
                    Mãquyềnlợigóidịchvụchonhàtuyểndụng = table.Column<int>(name: "Mã quyền lợi gói dịch vụ cho nhà tuyển dụng", type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1001, 1"),
                    EmployerServicePackageId = table.Column<int>(type: "int", nullable: false),
                    EmployerBenefitId = table.Column<int>(type: "int", nullable: false),
                    Value = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true, comment: "Giá trị"),
                    HasBenefit = table.Column<bool>(type: "bit", nullable: false, comment: "Có quyền lợi hay không"),
                    Active = table.Column<bool>(type: "bit", nullable: false, defaultValueSql: "((1))", comment: "Đánh dấu bị xóa"),
                    CreatedTime = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "(getdate())", comment: "Ngày tạo")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EmployerServicePackageEmployerBenefit", x => x.Mãquyềnlợigóidịchvụchonhàtuyểndụng);
                    table.ForeignKey(
                        name: "FK_EmployerBenefit_EmployerServicePackageEmployerBenefit",
                        column: x => x.EmployerBenefitId,
                        principalTable: "EmployerBenefit",
                        principalColumn: "Mã lợi ích của nhà tuyển dụng");
                    table.ForeignKey(
                        name: "FK_EmployerServicePackage_EmployerServicePackageEmployerBenefit",
                        column: x => x.EmployerServicePackageId,
                        principalTable: "EmployerServicePackage",
                        principalColumn: "Mã gói dịch vụ cho nhà tuyển dụng");
                });

            migrationBuilder.CreateTable(
                name: "UploadFile",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false, comment: "Mã file")
                        .Annotation("SqlServer:Identity", "1001, 1"),
                    FolderUploadId = table.Column<int>(type: "int", nullable: false),
                    AdminAccountId = table.Column<long>(type: "bigint", nullable: false),
                    Size = table.Column<long>(type: "bigint", nullable: false),
                    Extension = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false, comment: "Đuôi tệp tin"),
                    Path = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    ThumbnailPath = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    MimeType = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    Active = table.Column<bool>(type: "bit", nullable: false, defaultValueSql: "((1))", comment: "Đánh dấu bị xóa"),
                    CreatedTime = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "(getdate())", comment: "Ngày tạo"),
                    Name = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UploadFile", x => x.Id);
                    table.ForeignKey(
                        name: "FK_UploadFile_AdminAccount",
                        column: x => x.AdminAccountId,
                        principalTable: "AdminAccount",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_UploadFile_FolderUpload",
                        column: x => x.FolderUploadId,
                        principalTable: "FolderUpload",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Employer",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false, comment: "Mã nhà tuyển dụng")
                        .Annotation("SqlServer:Identity", "1001, 1"),
                    IsActivated = table.Column<bool>(type: "bit", nullable: false, defaultValueSql: "((0))", comment: "Đánh dấu đã kích hoạt tài khoản"),
                    EmployerStatusId = table.Column<int>(type: "int", nullable: false, defaultValue: 1001, comment: "Mã trạng thái nhà tuyển dụng"),
                    EmployerServicePackageId = table.Column<int>(type: "int", nullable: false, comment: "Mã gói dịch vụ"),
                    EmployerServicePackageEfficiencyExpiry = table.Column<DateTime>(type: "datetime2", nullable: true, comment: "Thời gian hết hạn gói dịch vụ"),
                    PositionId = table.Column<int>(type: "int", nullable: false, comment: "Mã chức vụ nhà tuyển dụng"),
                    Email = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false, comment: "Email"),
                    Username = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false, comment: "Tên tài khoản"),
                    Password = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false, comment: "Mật khẩu"),
                    Fullname = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false, comment: "Tên đầy đủ của nhà tuyển dụng."),
                    Photo = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true, comment: "Ảnh đại diện"),
                    Gender = table.Column<int>(type: "int", nullable: false, comment: "Giới tính"),
                    Phone = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: true, comment: "Số điện thoại"),
                    AccessFailedCount = table.Column<int>(type: "int", nullable: true, comment: "Số lần đăng nhập thất bại"),
                    LockEnabled = table.Column<bool>(type: "bit", nullable: true, comment: "Đánh dấu bị khóa"),
                    LockEndDate = table.Column<DateTime>(type: "datetime2", nullable: true, comment: "Thời gian tài khoản được mở"),
                    SkypeAccount = table.Column<string>(type: "nvarchar(450)", nullable: true, comment: "Tài khoản Skype"),
                    Search = table.Column<string>(type: "nvarchar(max)", nullable: false, comment: "Tìm kiếm tổng"),
                    Description = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true, comment: "Mô tả"),
                    Active = table.Column<bool>(type: "bit", nullable: false, defaultValueSql: "((1))", comment: "Đánh dấu bị xóa"),
                    CreatedTime = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "getdate()", comment: "Ngày tạo")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Employer", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Employer_EmployerServicePackage",
                        column: x => x.EmployerServicePackageId,
                        principalTable: "EmployerServicePackage",
                        principalColumn: "Mã gói dịch vụ cho nhà tuyển dụng");
                    table.ForeignKey(
                        name: "FK_Employer_EmployerStatus",
                        column: x => x.EmployerStatusId,
                        principalTable: "AccountStatus",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Employer_Position",
                        column: x => x.PositionId,
                        principalTable: "Position",
                        principalColumn: "Mã chức vụ");
                });

            migrationBuilder.CreateTable(
                name: "Post",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1001, 1"),
                    PostTypeId = table.Column<int>(type: "int", nullable: false),
                    PostLayoutId = table.Column<int>(type: "int", nullable: false),
                    PostStatusId = table.Column<int>(type: "int", nullable: false),
                    PostCategoryId = table.Column<int>(type: "int", nullable: false),
                    Overview = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false, comment: "Mô tả ngắn"),
                    Photo = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false, comment: "Đường dẫn ảnh bài viết"),
                    AuthorId = table.Column<long>(type: "bigint", nullable: false, comment: "Mã tác giả"),
                    IsPublish = table.Column<bool>(type: "bit", nullable: false, comment: "Trạng thái xuất bản bài viết"),
                    IsDraft = table.Column<bool>(type: "bit", nullable: false),
                    IsApproved = table.Column<bool>(type: "bit", nullable: false, comment: "Trạng thái duyệt bài viết"),
                    ApprovalDate = table.Column<DateTime>(type: "datetime2", nullable: false, comment: "Thời gian duyệt bài viết"),
                    PublishedTime = table.Column<DateTime>(type: "datetime2", nullable: false, comment: "Thời gian xuất bản bài viết"),
                    Active = table.Column<bool>(type: "bit", nullable: false, defaultValueSql: "((1))", comment: "Đánh dấu bị xóa"),
                    CreatedTime = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "(getdate())", comment: "Ngày tạo"),
                    Name = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false, comment: "Tiêu đề bài viết"),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true, comment: "Mô tả"),
                    Search = table.Column<string>(type: "varchar(MAX)", nullable: false, comment: "Tìm kiếm")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Post", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Post_AdminAccount",
                        column: x => x.AuthorId,
                        principalTable: "AdminAccount",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Post_PostCategory",
                        column: x => x.PostCategoryId,
                        principalTable: "PostCategory",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Post_PostLayout",
                        column: x => x.PostLayoutId,
                        principalTable: "PostLayout",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Post_PostStatus",
                        column: x => x.PostStatusId,
                        principalTable: "PostStatus",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Post_PostType",
                        column: x => x.PostTypeId,
                        principalTable: "PostType",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "RecruitmentCampaign",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1001, 1"),
                    RecruitmentCampaignStatusId = table.Column<int>(type: "int", nullable: false, comment: "Mã trạng thái chiến dịch tuyển dụng"),
                    IsAprroved = table.Column<bool>(type: "bit", nullable: false, defaultValueSql: "((0))", comment: "Chiến dịch tuyển dụng có được duyệt không"),
                    ApprovalDate = table.Column<DateTime>(type: "datetime2", nullable: true, comment: "Thời điểm duyệt"),
                    Search = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Active = table.Column<bool>(type: "bit", nullable: false, defaultValueSql: "((1))", comment: "Đánh dấu bị xóa"),
                    CreatedTime = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "(getdate())", comment: "Ngày tạo"),
                    Name = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false, comment: "Tên chiến dịch tuyển dụng"),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RecruitmentCampaign", x => x.Id);
                    table.ForeignKey(
                        name: "FK_RecruitmentCampaign_RecruitmentCampaignStatus",
                        column: x => x.RecruitmentCampaignStatusId,
                        principalTable: "RecruitmentCampaignStatus",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "AdminAccountRole",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false, comment: "Mã vai trò tài khoản admin")
                        .Annotation("SqlServer:Identity", "1001, 1"),
                    AdminAccountId = table.Column<long>(type: "bigint", nullable: false),
                    RoleId = table.Column<int>(type: "int", nullable: false),
                    Active = table.Column<bool>(type: "bit", nullable: false, defaultValueSql: "((1))", comment: "Đánh dấu bị xóa"),
                    CreatedTime = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "(getdate())", comment: "Ngày tạo")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AdminAccountRole", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AdminAccountRole_AdminAccount",
                        column: x => x.AdminAccountId,
                        principalTable: "AdminAccount",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_AdminAccountRole_Role",
                        column: x => x.RoleId,
                        principalTable: "Role",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "RoleMenu",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false, comment: "Mã quyền menu")
                        .Annotation("SqlServer:Identity", "1001, 1"),
                    RoleId = table.Column<int>(type: "int", nullable: false),
                    MenuId = table.Column<int>(type: "int", nullable: false),
                    Active = table.Column<bool>(type: "bit", nullable: false, defaultValueSql: "((1))", comment: "Đánh dấu bị xóa"),
                    CreatedTime = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "(getdate())", comment: "Ngày tạo")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RoleMenu", x => x.Id);
                    table.ForeignKey(
                        name: "FK_RoleMenu_Menu",
                        column: x => x.MenuId,
                        principalTable: "Menu",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_RoleMenu_Role",
                        column: x => x.RoleId,
                        principalTable: "Role",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "RolePermissions",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false, comment: "Mã")
                        .Annotation("SqlServer:Identity", "1001, 1"),
                    RoleId = table.Column<int>(type: "int", nullable: false),
                    PermissionId = table.Column<int>(type: "int", nullable: false),
                    Active = table.Column<bool>(type: "bit", nullable: false, defaultValueSql: "((1))", comment: "Đánh dấu bị xóa"),
                    CreatedTime = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "(getdate())", comment: "Ngày tạo")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RolePermissions", x => x.Id);
                    table.ForeignKey(
                        name: "FK_RolePermissions_Permissions",
                        column: x => x.PermissionId,
                        principalTable: "Permission",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_RolePermissions_Roles",
                        column: x => x.RoleId,
                        principalTable: "Role",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Tag",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false, comment: "Mã thẻ")
                        .Annotation("SqlServer:Identity", "1001, 1"),
                    TagTypeId = table.Column<int>(type: "int", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false, comment: "Tên thẻ"),
                    Active = table.Column<bool>(type: "bit", nullable: false, defaultValueSql: "((1))", comment: "Đánh dấu bị xóa"),
                    CreatedTime = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "(getdate())", comment: "Ngày tạo")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tag", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Tag_TagType",
                        column: x => x.TagTypeId,
                        principalTable: "TagType",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Voucher",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false, comment: "Mã giảm giá")
                        .Annotation("SqlServer:Identity", "1001, 1"),
                    Code = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false, comment: "Mã"),
                    VoucherTypeId = table.Column<int>(type: "int", nullable: false, comment: "Mã loại mã giảm giá"),
                    Value = table.Column<int>(type: "int", nullable: false, comment: "Giá trị mã giảm giá"),
                    ExpiryTime = table.Column<DateTime>(type: "datetime2", nullable: false, comment: "Ngày hết hạn"),
                    Color = table.Column<string>(type: "nvarchar(12)", maxLength: 12, nullable: false, comment: "Màu mã giảm giá"),
                    Active = table.Column<bool>(type: "bit", nullable: false, defaultValueSql: "((1))", comment: "Đánh dấu bị xóa"),
                    CreatedTime = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "(getdate())", comment: "Ngày tạo")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Voucher", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Voucher_VoucherType",
                        column: x => x.VoucherTypeId,
                        principalTable: "VoucherType",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "CandidateActivities",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false, comment: "Mã hoạt động ứng viên")
                        .Annotation("SqlServer:Identity", "1001, 1"),
                    CandidateId = table.Column<long>(type: "bigint", nullable: false),
                    TimePeriod = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false, comment: "Mô tả"),
                    Active = table.Column<bool>(type: "bit", nullable: false, defaultValueSql: "((1))", comment: "Đánh dấu bị xóa"),
                    CreatedTime = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "(getdate())", comment: "Ngày tạo"),
                    Name = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false, comment: "Tên hoạt động ứng viên"),
                    Description = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true, comment: "Mô tả")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CandidateActivities", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CandidateActivities_Candidate",
                        column: x => x.CandidateId,
                        principalTable: "Candidate",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "CandidateCertificate",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false, comment: "Mã chứng chỉ ứng viên ")
                        .Annotation("SqlServer:Identity", "1001, 1"),
                    IssueBy = table.Column<string>(type: "nvarchar(max)", nullable: false, comment: "Cấp bởi"),
                    CandidateId = table.Column<long>(type: "bigint", nullable: false),
                    TimePeriod = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false, comment: "Khoảng thời gian"),
                    Active = table.Column<bool>(type: "bit", nullable: false, defaultValueSql: "((1))", comment: "Đánh dấu bị xóa"),
                    CreatedTime = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "(getdate())", comment: "Ngày tạo"),
                    Name = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false, comment: "Tên chứng chỉ ứng viên"),
                    Description = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true, comment: "Mô tả")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CandidateCertificate", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CandidateCertificate_Candidate",
                        column: x => x.CandidateId,
                        principalTable: "Candidate",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "CandidateEducation",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false, comment: "Mã học vấn ứng viên")
                        .Annotation("SqlServer:Identity", "1001, 1"),
                    Title = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false, comment: "Tiêu đề"),
                    School = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false, comment: "Trường học"),
                    CandidateId = table.Column<long>(type: "bigint", nullable: false),
                    TimePeriod = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false, comment: "Khoảng thời gian"),
                    Description = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true, comment: "Mô tả"),
                    Active = table.Column<bool>(type: "bit", nullable: false, defaultValueSql: "((1))", comment: "Đánh dấu bị xóa"),
                    CreatedTime = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "(getdate())", comment: "Ngày tạo")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CandidateEducation", x => x.Id);
                    table.ForeignKey(
                        name: "Fk_CandidateEducation_Candidate",
                        column: x => x.CandidateId,
                        principalTable: "Candidate",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "CandidateHonorAndAward",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false, comment: "Mã vinh danh và giải thưởng")
                        .Annotation("SqlServer:Identity", "1001, 1"),
                    CandidateId = table.Column<long>(type: "bigint", nullable: false),
                    TimePeriod = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false, comment: "Khoảng thời gian"),
                    Active = table.Column<bool>(type: "bit", nullable: false, defaultValueSql: "((1))", comment: "Đánh dấu bị xóa"),
                    CreatedTime = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "(getdate())", comment: "Ngày tạo"),
                    Name = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false, comment: "Tên vinh danh và giải thưởng"),
                    Description = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true, comment: "Mô tả")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CandidateHonorAndAward", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CandidateHonorAndAward_Candidate",
                        column: x => x.CandidateId,
                        principalTable: "Candidate",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "CandidateMeta",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false, comment: "Mã Meta ứng viên")
                        .Annotation("SqlServer:Identity", "1001, 1"),
                    CandidateId = table.Column<long>(type: "bigint", nullable: false),
                    Key = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false, comment: "key"),
                    Value = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false, comment: "giá trị"),
                    Active = table.Column<bool>(type: "bit", nullable: false, defaultValueSql: "((1))", comment: "Đánh dấu bị xóa"),
                    CreatedTime = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "(getdate())", comment: "Ngày tạo"),
                    Name = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false, comment: "Tên Meta"),
                    Description = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true, comment: "Mô tả")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CandidateMeta", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CandidateMeta_Candidate",
                        column: x => x.CandidateId,
                        principalTable: "Candidate",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "CandidateNotification",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1001, 1"),
                    NotificationTypeId = table.Column<int>(type: "int", nullable: false, comment: "Mã loại thông báo"),
                    NotificationStatusId = table.Column<int>(type: "int", nullable: false, comment: "Mã trạng thái thông báo "),
                    CandidateId = table.Column<long>(type: "bigint", nullable: false, comment: "Mã ứng viên"),
                    Link = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true, comment: "Liên kết "),
                    Search = table.Column<string>(type: "nvarchar(max)", nullable: false, comment: "Lưu ký tự không dấu của các trường muốn tìm kiếm"),
                    Active = table.Column<bool>(type: "bit", nullable: false, defaultValueSql: "((1))", comment: "Đánh dấu bị xóa"),
                    CreatedTime = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "(getdate())", comment: "Ngày tạo"),
                    Name = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false, comment: "Tên thông báo cho ứng viên"),
                    Description = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true, comment: "Mô tả")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CandidateNotification", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CandidateNotification_Candidate",
                        column: x => x.CandidateId,
                        principalTable: "Candidate",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_CandidateNotification_NotificationStatus",
                        column: x => x.NotificationStatusId,
                        principalTable: "NotificationStatus",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_CandidateNotification_NotificationType",
                        column: x => x.NotificationTypeId,
                        principalTable: "NotificationType",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "CandidateOrders",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false, comment: "Mã đơn đặt hàng của ứng viên")
                        .Annotation("SqlServer:Identity", "1001, 1"),
                    OrderStatusId = table.Column<int>(type: "int", nullable: false),
                    PaymentMethodId = table.Column<int>(type: "int", nullable: false),
                    CandidateId = table.Column<long>(type: "bigint", nullable: false),
                    Price = table.Column<int>(type: "int", nullable: false, comment: "Giá"),
                    DiscountPrice = table.Column<int>(type: "int", nullable: false, comment: "Giảm Giá"),
                    DiscountCounpon = table.Column<int>(type: "int", nullable: false, comment: "Phiếu giảm Giá"),
                    FinalPrice = table.Column<int>(type: "int", nullable: false, comment: "Giá niêm yết"),
                    TransactionCode = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true, comment: "Mã giao dịch"),
                    Info = table.Column<string>(type: "nvarchar(max)", nullable: true, comment: "Thông tin giao dịch"),
                    RequestId = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Active = table.Column<bool>(type: "bit", nullable: false, defaultValueSql: "((1))", comment: "Đánh dấu bị xóa"),
                    CreatedTime = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "(getdate())", comment: "Ngày tạo")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CandidateOrders", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CandidateOrders_Candidate",
                        column: x => x.CandidateId,
                        principalTable: "Candidate",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_CandidateOrders_OrderStatus",
                        column: x => x.OrderStatusId,
                        principalTable: "OrderStatus",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_CandidateOrders_PaymentMethod",
                        column: x => x.PaymentMethodId,
                        principalTable: "PaymentMethod",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "CandidatePassword",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1001, 1"),
                    CandidateId = table.Column<long>(type: "bigint", nullable: false, comment: "Mã ứng viên"),
                    OldPassword = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false, comment: "Mật khẩu cũ"),
                    Active = table.Column<bool>(type: "bit", nullable: false, defaultValueSql: "((1))", comment: "Đánh dấu bị xóa"),
                    CreatedTime = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "(getdate())", comment: "Ngày tạo")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CandidatePassword", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CandidatePassword_Candidate",
                        column: x => x.CandidateId,
                        principalTable: "Candidate",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "CandidateProjects",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false, comment: "Mã dự án ứng viên")
                        .Annotation("SqlServer:Identity", "1001, 1"),
                    CandidateId = table.Column<long>(type: "bigint", nullable: false),
                    ProjectName = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false, comment: "Tên dự án"),
                    Customer = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false, comment: "Khách hàng"),
                    TeamSize = table.Column<int>(type: "int", nullable: false, comment: "Số người trong dự án"),
                    Position = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false, comment: "Chức vụ"),
                    Responsibilities = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false, comment: "Trách nhiệm"),
                    Info = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false, comment: "Thông tin"),
                    TimePeriod = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false, comment: "Khoảng thời gian"),
                    Active = table.Column<bool>(type: "bit", nullable: false, defaultValueSql: "((1))", comment: "Đánh dấu bị xóa"),
                    CreatedTime = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "(getdate())", comment: "Ngày tạo"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true, comment: "Mô tả")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CandidateProjects", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CandidateProjects_Candidate",
                        column: x => x.CandidateId,
                        principalTable: "Candidate",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "CandidateSuggestionJobCategory",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false, comment: "Mã việc làm đề xuất cho ứng viên")
                        .Annotation("SqlServer:Identity", "1001, 1"),
                    CandidateId = table.Column<long>(type: "bigint", nullable: false),
                    JobCategoryId = table.Column<int>(type: "int", nullable: false),
                    Active = table.Column<bool>(type: "bit", nullable: false, defaultValueSql: "((1))", comment: "Đánh dấu bị xóa"),
                    CreatedTime = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "(getdate())", comment: "Ngày tạo")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CandidateSuggestionJobCategory", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CandidateSuggestionJobCategory_Candidate",
                        column: x => x.CandidateId,
                        principalTable: "Candidate",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_CandidateSuggestionJobCategory_JobCategory",
                        column: x => x.JobCategoryId,
                        principalTable: "JobCategory",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "CandidateSuggestionJobPosition",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1001, 1"),
                    CandidateId = table.Column<long>(type: "bigint", nullable: false, comment: "Mã ứng viên"),
                    JobPositionId = table.Column<int>(type: "int", nullable: false, comment: "Mã vị trí công việc"),
                    Active = table.Column<bool>(type: "bit", nullable: false, defaultValueSql: "((1))", comment: "Đánh dấu bị xóa"),
                    CreatedTime = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "(getdate())", comment: "Ngày tạo")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CandidateSuggestionJobPosition", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CandidateSuggestionJobPosition_Candidate",
                        column: x => x.CandidateId,
                        principalTable: "Candidate",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_CandidateSuggestionJobPosition_JobPosition",
                        column: x => x.JobPositionId,
                        principalTable: "JobPosition",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "CandidateSuggestionJobSkill",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false, comment: "Mã kỹ năng đề xuất cho ứng viên")
                        .Annotation("SqlServer:Identity", "1001, 1"),
                    CandidateId = table.Column<long>(type: "bigint", nullable: false),
                    JobSkillId = table.Column<int>(type: "int", nullable: false),
                    Active = table.Column<bool>(type: "bit", nullable: false, defaultValueSql: "((1))", comment: "Đánh dấu bị xóa"),
                    CreatedTime = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "(getdate())", comment: "Ngày tạo")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CandidateSuggestionJobSkill", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CandidateSuggestionJobSkill_Candidate",
                        column: x => x.CandidateId,
                        principalTable: "Candidate",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_CandidateSuggestionJobSkill_JobSkill",
                        column: x => x.JobSkillId,
                        principalTable: "JobSkill",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "CandidateSuggestionWorkPlace",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1001, 1"),
                    CandidateId = table.Column<long>(type: "bigint", nullable: false, comment: "Mã ứng viên"),
                    WorkPlaceId = table.Column<int>(type: "int", nullable: false, comment: "Mã nơi làm việc"),
                    Active = table.Column<bool>(type: "bit", nullable: false, defaultValueSql: "((1))", comment: "Đánh dấu bị xóa"),
                    CreatedTime = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "(getdate())", comment: "Ngày tạo")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CandidateSuggestionWorkPlace", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CandidateSuggestionWorkPlace_Candidate",
                        column: x => x.CandidateId,
                        principalTable: "Candidate",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_CandidateSuggestionWorkPlace_WorkPlace",
                        column: x => x.WorkPlaceId,
                        principalTable: "WorkPlace",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "CandidateViewedJob",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false, comment: "Mã ứng viên đã xem công việc")
                        .Annotation("SqlServer:Identity", "1001, 1"),
                    CandidateId = table.Column<long>(type: "bigint", nullable: false),
                    JobId = table.Column<long>(type: "bigint", nullable: false),
                    Active = table.Column<bool>(type: "bit", nullable: false, defaultValueSql: "((1))", comment: "Đánh dấu bị xóa"),
                    CreatedTime = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "(getdate())", comment: "Ngày tạo")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CandidateViewedJob", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CandidateViewedJob_Candidate",
                        column: x => x.CandidateId,
                        principalTable: "Candidate",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "CandidateWorkExperience",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false, comment: "Mã kinh nghiệm làm việc của ứng viên")
                        .Annotation("SqlServer:Identity", "1001, 1"),
                    JobTitle = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false, comment: "Tiêu đề công việc"),
                    Company = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false, comment: "Công ty"),
                    CandidateId = table.Column<long>(type: "bigint", nullable: false),
                    TimePeriod = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false, comment: "Khoảng thời gian"),
                    Description = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true, comment: "Mô tả"),
                    Active = table.Column<bool>(type: "bit", nullable: false, defaultValueSql: "((1))", comment: "Đánh dấu bị xóa"),
                    CreatedTime = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "(getdate())", comment: "Ngày tạo")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CandidateWorkExperience", x => x.Id);
                    table.ForeignKey(
                        name: "CandidateWorkExperience_Candidate",
                        column: x => x.CandidateId,
                        principalTable: "Candidate",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "CandidateCoupon",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1001, 1"),
                    CandidateId = table.Column<long>(type: "bigint", nullable: false, comment: "Mã ứng viên"),
                    CouponId = table.Column<int>(type: "int", nullable: false, comment: "Mã phiếu giảm giá"),
                    Active = table.Column<bool>(type: "bit", nullable: false, defaultValueSql: "((1))", comment: "Đánh dấu bị xóa"),
                    CreatedTime = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "(getdate())", comment: "Ngày tạo")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CandidateCoupon", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CandidateCoupon_Candidate",
                        column: x => x.CandidateId,
                        principalTable: "Candidate",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_CandidateCoupon_Coupon",
                        column: x => x.CouponId,
                        principalTable: "Coupon",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Company",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false, comment: "Mã công ty")
                        .Annotation("SqlServer:Identity", "1001, 1"),
                    EmployerId = table.Column<long>(type: "bigint", nullable: false, comment: "Mã nhà tuyển dụng"),
                    CompanySizeId = table.Column<int>(type: "int", nullable: false, comment: "Mã quy mô công ty"),
                    AddressDetail = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false, comment: "Địa chỉ công ty"),
                    Location = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true, comment: "Tọa độ trên bản đồ"),
                    Website = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false, comment: "Trang web của công ty"),
                    Phone = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: false, comment: "Số điện thoại của công ty"),
                    Logo = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false, comment: "Logo công ty"),
                    CoverPhoto = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false, comment: "Ảnh bìa"),
                    TaxCode = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false, comment: "Mã số thuế"),
                    FoundedIn = table.Column<int>(type: "int", nullable: false, comment: "Năm thành lập"),
                    TiktokLink = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true, comment: "Tài khoản Tiktok"),
                    YoutubeLink = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true, comment: "Tài khoản Youtube"),
                    FacebookLink = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true, comment: "Tài khoản Facebook"),
                    LinkedinLink = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true, comment: "Tài khoản Linkedin"),
                    TwitterLink = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true, comment: "Tài khoản Twitter"),
                    VideoIntro = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true, comment: "Link video giới thiệu"),
                    Active = table.Column<bool>(type: "bit", nullable: false, defaultValueSql: "((1))", comment: "Đánh dấu bị xóa"),
                    CreatedTime = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "getdate()", comment: "Ngày tạo"),
                    Name = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false, comment: "Tên công ty"),
                    Description = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true, comment: "Mô tả"),
                    Search = table.Column<string>(type: "nvarchar(max)", nullable: false, comment: "Tìm kiếm tổng")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Company", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Company_CompanySize",
                        column: x => x.CompanySizeId,
                        principalTable: "CompanySize",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Company_Employer",
                        column: x => x.EmployerId,
                        principalTable: "Employer",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "EmployerMeta",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1001, 1"),
                    EmployerId = table.Column<long>(type: "bigint", nullable: false, comment: "Mã nhà tuyển dụng"),
                    Key = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false, comment: "Tên khóa"),
                    Value = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false, comment: "Giá trị"),
                    Active = table.Column<bool>(type: "bit", nullable: false, defaultValueSql: "((1))", comment: "Đánh dấu bị xóa"),
                    CreatedTime = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "(getdate())", comment: "Ngày tạo"),
                    Name = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false, comment: "Tên dữ liệu bổ sung của nhà tuyển dụng"),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true, comment: "Mô tả")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EmployerMeta", x => x.Id);
                    table.ForeignKey(
                        name: "FK_EmployerMeta_Employer",
                        column: x => x.EmployerId,
                        principalTable: "Employer",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "EmployerNotification",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1001, 1"),
                    NotificationTypeId = table.Column<int>(type: "int", nullable: false, comment: "Mã loại thông báo"),
                    NotificationStatusId = table.Column<int>(type: "int", nullable: false, comment: "Mã trạng thái thông báo"),
                    EmployerId = table.Column<long>(type: "bigint", nullable: false, comment: "Mã nhà tuyển dụng"),
                    Link = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true, comment: "Đường dẫn"),
                    Search = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Active = table.Column<bool>(type: "bit", nullable: false, defaultValueSql: "((1))", comment: "Đánh dấu bị xóa"),
                    CreatedTime = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "(getdate())", comment: "Ngày tạo"),
                    Name = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false, comment: "Tên thông báo"),
                    Description = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true, comment: "Mô tả")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EmployerNotification", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Employer_EmployerNotification",
                        column: x => x.EmployerId,
                        principalTable: "Employer",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_NotificationStatus_EmployerNotification",
                        column: x => x.NotificationStatusId,
                        principalTable: "NotificationStatus",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_NotificationType_EmployerNotification",
                        column: x => x.NotificationTypeId,
                        principalTable: "NotificationType",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "EmployerOrders",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1001, 1"),
                    OrderStatusId = table.Column<int>(type: "int", nullable: false, comment: "Mã trạng thái đơn hàng"),
                    PaymentMethodId = table.Column<int>(type: "int", nullable: false, comment: "Mã loại thanh toán"),
                    EmployerId = table.Column<long>(type: "bigint", nullable: false, comment: "Mã nhà tuyển dụng"),
                    Price = table.Column<int>(type: "int", nullable: false, comment: "Giá tiền"),
                    DiscountPrice = table.Column<int>(type: "int", nullable: false, comment: "Giá khuyến mãi"),
                    DiscountVoucher = table.Column<int>(type: "int", nullable: false, comment: "Phiếu giảm giá"),
                    FinalPrice = table.Column<int>(type: "int", nullable: false, comment: "Giá cuối"),
                    TransactionCode = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true, comment: "Mã giao dịch"),
                    Info = table.Column<string>(type: "nvarchar(max)", nullable: true, comment: "Thông tin giao dịch"),
                    RequestId = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false, comment: "Mã yêu cầu"),
                    Search = table.Column<string>(type: "nvarchar(max)", nullable: false, comment: "Search tổng"),
                    Active = table.Column<bool>(type: "bit", nullable: false, defaultValueSql: "((1))", comment: "Đánh dấu bị xóa"),
                    CreatedTime = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "(getdate())", comment: "Ngày tạo")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EmployerOrders", x => x.Id);
                    table.ForeignKey(
                        name: "FK_EmployerOrders_Employer_EmployerId",
                        column: x => x.EmployerId,
                        principalTable: "Employer",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_EmployerOrders_OrderStatus_OrderStatusId",
                        column: x => x.OrderStatusId,
                        principalTable: "OrderStatus",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_EmployerOrders_PaymentMethod_PaymentMethodId",
                        column: x => x.PaymentMethodId,
                        principalTable: "PaymentMethod",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "EmployerPassword",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false, comment: "Mã mật khẩu của nhà tuyển dụng")
                        .Annotation("SqlServer:Identity", "1001, 1"),
                    EmployerId = table.Column<long>(type: "bigint", nullable: false),
                    OldPassword = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Active = table.Column<bool>(type: "bit", nullable: false, defaultValueSql: "((1))", comment: "Đánh dấu bị xóa"),
                    CreatedTime = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "(getdate())", comment: "Ngày tạo")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EmployerPassword", x => x.Id);
                    table.ForeignKey(
                        name: "FK_EmployerPassword_Employer",
                        column: x => x.EmployerId,
                        principalTable: "Employer",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "EmployerViewedCV",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1001, 1"),
                    CandidateId = table.Column<long>(type: "bigint", nullable: false, comment: "Mã ứng viên"),
                    EmployerId = table.Column<long>(type: "bigint", nullable: false, comment: "Mã nhà tuyển dụng"),
                    Active = table.Column<bool>(type: "bit", nullable: false, defaultValueSql: "((1))", comment: "Đánh dấu bị xóa"),
                    CreatedTime = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "(getdate())", comment: "Ngày tạo")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EmployerViewedCV", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Candidate_EmployerViewedCV",
                        column: x => x.CandidateId,
                        principalTable: "Candidate",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Employer_EmployerViewedCV",
                        column: x => x.EmployerId,
                        principalTable: "Employer",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Job",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1001, 1"),
                    RecruimentCampaignId = table.Column<long>(type: "bigint", nullable: false, comment: "Mã chiến dịch tuyển dụng"),
                    JobStatusId = table.Column<int>(type: "int", nullable: false, comment: "Mã trạng thái tin tuyển dụng"),
                    TotalRecruitment = table.Column<int>(type: "int", nullable: false, comment: "Số lượng cần tuyển"),
                    GenderRequirement = table.Column<int>(type: "int", nullable: false, comment: "Giới tính yêu cầu"),
                    JobTypeId = table.Column<int>(type: "int", nullable: false, comment: "Mã loại tin tuyển dụng"),
                    PrimaryJobPositionId = table.Column<int>(type: "int", nullable: false, comment: "Mã vị trí tuyển dụng chính"),
                    IsApproved = table.Column<bool>(type: "bit", nullable: false, defaultValueSql: "((0))", comment: "Tin tuyển dụng có được duyệt hay không"),
                    ExperienceRangeId = table.Column<int>(type: "int", nullable: false, comment: "Mã khoảng kinh nghiệm"),
                    Currency = table.Column<int>(type: "int", nullable: false, comment: "Loại tiền tệ"),
                    SalaryTypeId = table.Column<int>(type: "int", nullable: false, comment: "Mã loại lương"),
                    SalaryFrom = table.Column<int>(type: "int", nullable: true, comment: "Lương tối thiểu"),
                    SalaryTo = table.Column<int>(type: "int", nullable: true, comment: "Lương tối đa"),
                    Overview = table.Column<string>(type: "nvarchar(max)", nullable: true, comment: "Tổng quan công việc"),
                    Requirement = table.Column<string>(type: "nvarchar(max)", nullable: false, comment: "Yêu cầu công việc"),
                    Benefit = table.Column<string>(type: "nvarchar(max)", nullable: false, comment: "Quyền lợi công việc"),
                    ReceiverName = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true, comment: "Tên người nhận"),
                    ReceiverPhone = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: true, comment: "Điện thoại người nhận"),
                    ReceiverEmail = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ApplyEndDate = table.Column<DateTime>(type: "datetime2", nullable: true, comment: "Thời hạn ứng tuyển"),
                    ApprovalDate = table.Column<DateTime>(type: "datetime2", nullable: true, comment: "Thời diểm duyệt tin"),
                    Active = table.Column<bool>(type: "bit", nullable: false, defaultValueSql: "((1))", comment: "Đánh dấu bị xóa"),
                    CreatedTime = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "(getdate())", comment: "Ngày tạo"),
                    Name = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false, comment: "Title tin tuyển dụng"),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Search = table.Column<string>(type: "nvarchar(max)", nullable: false, comment: "Trường tìm kiếm")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Job", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Job_ExperienceRange",
                        column: x => x.ExperienceRangeId,
                        principalTable: "ExperienceRange",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Job_JobStatus",
                        column: x => x.JobStatusId,
                        principalTable: "JobStatus",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Job_JobType",
                        column: x => x.JobTypeId,
                        principalTable: "JobType",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Job_PrimaryJobPosition",
                        column: x => x.PrimaryJobPositionId,
                        principalTable: "JobPosition",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Job_RecruitmentCampaign",
                        column: x => x.RecruimentCampaignId,
                        principalTable: "RecruitmentCampaign",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Job_SalaryType",
                        column: x => x.SalaryTypeId,
                        principalTable: "SalaryType",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "RecruitmentCampaignRequireJobPosition",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1001, 1"),
                    JobPositionId = table.Column<int>(type: "int", nullable: false, comment: "Mã vị trí công việc"),
                    RecruitmentCampaignId = table.Column<long>(type: "bigint", nullable: false, comment: "Mã chiến dịch tuyển dụng"),
                    Active = table.Column<bool>(type: "bit", nullable: false, defaultValueSql: "((1))", comment: "Đánh dấu bị xóa"),
                    CreatedTime = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "(getdate())", comment: "Ngày tạo")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RecruitmentCampaignRequireJobPosition", x => x.Id);
                    table.ForeignKey(
                        name: "FK_RecruitmentCampaignRequireJobPosition_JobPosition_JobPositionId",
                        column: x => x.JobPositionId,
                        principalTable: "JobPosition",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_RecruitmentCampaignRequireJobPosition_RecruitmentCampaign_RecruitmentCampaignId",
                        column: x => x.RecruitmentCampaignId,
                        principalTable: "RecruitmentCampaign",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "RecruitmentCampaignRequireWorkPlace",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1001, 1"),
                    WorkPlaceId = table.Column<int>(type: "int", nullable: false, comment: "Mã địa điểm làm việc"),
                    RecruitmentCampaignId = table.Column<long>(type: "bigint", nullable: false, comment: "Mã chiến dịch tuyển dụng"),
                    Active = table.Column<bool>(type: "bit", nullable: false, defaultValueSql: "((1))", comment: "Đánh dấu bị xóa"),
                    CreatedTime = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "(getdate())", comment: "Ngày tạo")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RecruitmentCampaignRequireWorkPlace", x => x.Id);
                    table.ForeignKey(
                        name: "FK_RecruitmentCampaignRequireWorkPlace_RecruitmentCampaign_RecruitmentCampaignId",
                        column: x => x.RecruitmentCampaignId,
                        principalTable: "RecruitmentCampaign",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_RecruitmentCampaignRequireWorkPlace_WorkPlace_WorkPlaceId",
                        column: x => x.WorkPlaceId,
                        principalTable: "WorkPlace",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "PostTag",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false, comment: "Mã thẻ bài viết")
                        .Annotation("SqlServer:Identity", "1001, 1"),
                    PostId = table.Column<int>(type: "int", nullable: false, comment: "Mã bài viết"),
                    TagId = table.Column<int>(type: "int", nullable: false, comment: "Mã thẻ"),
                    Active = table.Column<bool>(type: "bit", nullable: false, defaultValueSql: "((1))", comment: "Đánh dấu bị xóa"),
                    CreatedTime = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "(getdate())", comment: "Ngày tạo")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PostTag", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PostTag_Post",
                        column: x => x.PostId,
                        principalTable: "Post",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_PostTag_Tag",
                        column: x => x.TagId,
                        principalTable: "Tag",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "EmployerVoucher",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1001, 1"),
                    EmployerId = table.Column<long>(type: "bigint", nullable: false, comment: "Mã nhà tuyển dụng"),
                    VoucherId = table.Column<int>(type: "int", nullable: false, comment: "Mã voucher"),
                    Active = table.Column<bool>(type: "bit", nullable: false, defaultValueSql: "((1))", comment: "Đánh dấu bị xóa"),
                    CreatedTime = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "(getdate())", comment: "Ngày tạo")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EmployerVoucher", x => x.Id);
                    table.ForeignKey(
                        name: "FK_EmployerVoucher_Employer_EmployerId",
                        column: x => x.EmployerId,
                        principalTable: "Employer",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Voucher_EmployerVoucher",
                        column: x => x.VoucherId,
                        principalTable: "Voucher",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "CandidateOrderCoupon",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1001, 1"),
                    CandidateOrderId = table.Column<long>(type: "bigint", nullable: false, comment: "Mã đơn hàng ứng viên"),
                    CouponId = table.Column<int>(type: "int", nullable: false, comment: "Mã phiếu giảm giá"),
                    Active = table.Column<bool>(type: "bit", nullable: false, defaultValueSql: "((1))", comment: "Đánh dấu bị xóa"),
                    CreatedTime = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "(getdate())", comment: "Ngày tạo")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CandidateOrderCoupon", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CandidateOrderCoupon_CandidateOrders",
                        column: x => x.CandidateOrderId,
                        principalTable: "CandidateOrders",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_CandidateOrderCoupon_Coupon",
                        column: x => x.CouponId,
                        principalTable: "Coupon",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "CandidateOrderDetail",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1001, 1"),
                    CandidateOrderId = table.Column<long>(type: "bigint", nullable: false, comment: "Mã đơn hàng ứng viên"),
                    CandidateLevelId = table.Column<int>(type: "int", nullable: false, comment: "Mã cấp độ ứng viên "),
                    Quantity = table.Column<int>(type: "int", nullable: false, comment: "Số lượng"),
                    Price = table.Column<int>(type: "int", nullable: false, comment: "Giá"),
                    DiscountPrice = table.Column<int>(type: "int", nullable: false, comment: "Giảm giá"),
                    FinalPrice = table.Column<int>(type: "int", nullable: false, comment: "Giá cuối"),
                    Active = table.Column<bool>(type: "bit", nullable: false, defaultValueSql: "((1))", comment: "Đánh dấu bị xóa"),
                    CreatedTime = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "(getdate())", comment: "Ngày tạo")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CandidateOrderDetail", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CandidateOrderDetails_CandidateLevel",
                        column: x => x.CandidateLevelId,
                        principalTable: "CandidateLevel",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_CandidateOrderDetails_CandidateOrders",
                        column: x => x.CandidateOrderId,
                        principalTable: "CandidateOrders",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "CandidateFollowCompany",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false, comment: "Mã ứng viên theo dỗi công ty")
                        .Annotation("SqlServer:Identity", "1001, 1"),
                    CandidateId = table.Column<long>(type: "bigint", nullable: false),
                    CompanyId = table.Column<int>(type: "int", nullable: false),
                    Active = table.Column<bool>(type: "bit", nullable: false, defaultValueSql: "((1))", comment: "Đánh dấu bị xóa"),
                    CreatedTime = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "(getdate())", comment: "Ngày tạo")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CandidateFollowCompany", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CandidateFollowCompany_Candidate",
                        column: x => x.CandidateId,
                        principalTable: "Candidate",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_CandidateFollowCompany_Company",
                        column: x => x.CompanyId,
                        principalTable: "Company",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "CompanyFieldOfActivity",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false, comment: "Mã lĩnh vực hoạt động của công ty")
                        .Annotation("SqlServer:Identity", "1001, 1"),
                    CompanyId = table.Column<int>(type: "int", nullable: false),
                    FieldOfActivityId = table.Column<int>(type: "int", nullable: false),
                    Active = table.Column<bool>(type: "bit", nullable: false, defaultValueSql: "((1))", comment: "Đánh dấu bị xóa"),
                    CreatedTime = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "(getdate())", comment: "Ngày tạo")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CompanyFieldOfActivity", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CompanyFieldOfActivity_Company",
                        column: x => x.CompanyId,
                        principalTable: "Company",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_CompanyFieldOfActivity_FieldOfActivity",
                        column: x => x.FieldOfActivityId,
                        principalTable: "FieldOfActivity",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "CompanyImage",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1001, 1"),
                    CompanyId = table.Column<int>(type: "int", nullable: false, comment: "Mã công ty "),
                    Path = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false, comment: "Đường dẫn ảnh"),
                    OrderSort = table.Column<int>(type: "int", nullable: false, comment: "Sắp xếp"),
                    Active = table.Column<bool>(type: "bit", nullable: false, defaultValueSql: "((1))", comment: "Đánh dấu bị xóa"),
                    CreatedTime = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "(getdate())", comment: "Ngày tạo"),
                    Name = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false, comment: "Tên "),
                    Description = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true, comment: "Mô tả ")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CompanyImage", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CompanyImage_Company",
                        column: x => x.CompanyId,
                        principalTable: "Company",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "CompanyReview",
                columns: table => new
                {
                    Mãđánhgiácôngty = table.Column<long>(name: "Mã đánh giá công ty", type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1001, 1"),
                    CompanyId = table.Column<int>(type: "int", nullable: false, comment: "Mã công ty được đánh giá"),
                    CandidateId = table.Column<long>(type: "bigint", nullable: false, comment: "Mã ứng viên đánh giá"),
                    Review = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false, comment: "Đánh giá"),
                    Rating = table.Column<float>(type: "real", nullable: false, comment: "Điểm đánh giá"),
                    IsApproved = table.Column<bool>(type: "bit", nullable: false, comment: "Chấp thuận đánh giá"),
                    ApprovalDate = table.Column<DateTime>(type: "datetime2", nullable: true, comment: "Ngày chấp thuận đánh giá"),
                    Active = table.Column<bool>(type: "bit", nullable: false, defaultValueSql: "((1))", comment: "Đánh dấu bị xóa"),
                    CreatedTime = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "(getdate())", comment: "Ngày tạo")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CompanyReview", x => x.Mãđánhgiácôngty);
                    table.ForeignKey(
                        name: "FK_CompanyReview_Candidate",
                        column: x => x.CandidateId,
                        principalTable: "Candidate",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_CompanyReview_Company",
                        column: x => x.CompanyId,
                        principalTable: "Company",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "CompanyShowOnHomePage",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1001, 1"),
                    CompanyId = table.Column<int>(type: "int", nullable: false, comment: "Mã công ty"),
                    OrderSort = table.Column<int>(type: "int", nullable: false, comment: "Sắp xếp"),
                    Active = table.Column<bool>(type: "bit", nullable: false, defaultValueSql: "((1))", comment: "Đánh dấu bị xóa"),
                    CreatedTime = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "(getdate())", comment: "Ngày tạo")
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

            migrationBuilder.CreateTable(
                name: "License",
                columns: table => new
                {
                    Mãgiấytờ = table.Column<long>(name: "Mã giấy tờ", type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1001, 1"),
                    CompanyId = table.Column<int>(type: "int", nullable: false),
                    LicenseTypeId = table.Column<int>(type: "int", nullable: false),
                    Path = table.Column<string>(type: "nvarchar(max)", nullable: false, comment: "Ảnh giấy tờ"),
                    IsApproved = table.Column<bool>(type: "bit", nullable: false, comment: "Chấp nhận"),
                    ApprovalDate = table.Column<DateTime>(type: "datetime2", nullable: true, comment: "Ngày chấp nhận"),
                    Search = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Active = table.Column<bool>(type: "bit", nullable: false, defaultValueSql: "((1))", comment: "Đánh dấu bị xóa"),
                    CreatedTime = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "(getdate())", comment: "Ngày tạo")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_License", x => x.Mãgiấytờ);
                    table.ForeignKey(
                        name: "FK_License_Company",
                        column: x => x.CompanyId,
                        principalTable: "Company",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_License_LicenseType",
                        column: x => x.LicenseTypeId,
                        principalTable: "LicenseType",
                        principalColumn: "Mã loại giấy tờ");
                });

            migrationBuilder.CreateTable(
                name: "EmployerOrderDetail",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1001, 1"),
                    OrderId = table.Column<long>(type: "bigint", nullable: false, comment: "Mã đơn hàng"),
                    EmployerServicePackageId = table.Column<int>(type: "int", nullable: false, comment: "Mã gói dịch vụ nhà tuyển dụng"),
                    Quantity = table.Column<int>(type: "int", nullable: false, comment: "Số lượng"),
                    Price = table.Column<int>(type: "int", nullable: false, comment: "Giá tiền"),
                    DiscountPrice = table.Column<int>(type: "int", nullable: false, comment: "Giảm giá"),
                    FinalPrice = table.Column<int>(type: "int", nullable: false, comment: "Giá tiền thanh toán"),
                    Search = table.Column<string>(type: "nvarchar(max)", nullable: false, comment: "Search tổng"),
                    Active = table.Column<bool>(type: "bit", nullable: false, defaultValueSql: "((1))", comment: "Đánh dấu bị xóa"),
                    CreatedTime = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "(getdate())", comment: "Ngày tạo")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EmployerOrderDetail", x => x.Id);
                    table.ForeignKey(
                        name: "FK_EmployerOrder_EmployerOrderDetail",
                        column: x => x.OrderId,
                        principalTable: "EmployerOrders",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_EmployerServicePackage_EmployerOrderDetail",
                        column: x => x.EmployerServicePackageId,
                        principalTable: "EmployerServicePackage",
                        principalColumn: "Mã gói dịch vụ cho nhà tuyển dụng");
                });

            migrationBuilder.CreateTable(
                name: "CandidateApplyJob",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false, comment: "Mã ứng viên ứng tuyển việc làm")
                        .Annotation("SqlServer:Identity", "1001, 1"),
                    CandidateId = table.Column<long>(type: "bigint", nullable: false),
                    JobId = table.Column<long>(type: "bigint", nullable: false),
                    CandidateApplyJobStatusId = table.Column<int>(type: "int", nullable: false),
                    CandidateApplyJobSourceId = table.Column<int>(type: "int", nullable: false),
                    IsEmployerViewed = table.Column<bool>(type: "bit", nullable: false, comment: "Nhà tuyển dụng đã xem"),
                    Active = table.Column<bool>(type: "bit", nullable: false, defaultValueSql: "((1))", comment: "Đánh dấu bị xóa"),
                    CreatedTime = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "(getdate())", comment: "Ngày tạo")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CandidateApplyJob", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CandidateApplyJobs_Candidate",
                        column: x => x.CandidateId,
                        principalTable: "Candidate",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_CandidateApplyJobs_CandidateApplyJobSource",
                        column: x => x.CandidateApplyJobSourceId,
                        principalTable: "CandidateApplyJobSource",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_CandidateApplyJobs_CandidateApplyJobStatus",
                        column: x => x.CandidateApplyJobStatusId,
                        principalTable: "CandidateApplyJobStatus",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_CandidateApplyJobs_Job",
                        column: x => x.JobId,
                        principalTable: "Job",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "CandidateIgnoreJob",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false, comment: "Mã công việc ứng viên bỏ qua")
                        .Annotation("SqlServer:Identity", "1001, 1"),
                    CandidateId = table.Column<long>(type: "bigint", nullable: false),
                    JobId = table.Column<long>(type: "bigint", nullable: false),
                    Active = table.Column<bool>(type: "bit", nullable: false, defaultValueSql: "((1))", comment: "Đánh dấu bị xóa"),
                    CreatedTime = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "(getdate())", comment: "Ngày tạo")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CandidateIgnoreJob", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CandidateIgnoreJob_Candidate",
                        column: x => x.CandidateId,
                        principalTable: "Candidate",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_CandidateIgnoreJob_Job",
                        column: x => x.JobId,
                        principalTable: "Job",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "CandidateSaveJob",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false, comment: "Mã công việc ứng viên đã lưu")
                        .Annotation("SqlServer:Identity", "1001, 1"),
                    CandidateId = table.Column<long>(type: "bigint", nullable: false),
                    JobId = table.Column<long>(type: "bigint", nullable: false),
                    Active = table.Column<bool>(type: "bit", nullable: false, defaultValueSql: "((1))", comment: "Đánh dấu bị xóa"),
                    CreatedTime = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "(getdate())", comment: "Ngày tạo")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CandidateSaveJob", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CandidateSaveJob_Candidate",
                        column: x => x.CandidateId,
                        principalTable: "Candidate",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_CandidateSaveJob_Job",
                        column: x => x.JobId,
                        principalTable: "Job",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "JobMultimedia",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1001, 1"),
                    JobId = table.Column<long>(type: "bigint", nullable: false, comment: "Mã tin tuyển dụng"),
                    Path = table.Column<string>(type: "nvarchar(max)", nullable: false, comment: "Mã tin tuyển dụng"),
                    MultimediaTypeId = table.Column<int>(type: "int", nullable: false, comment: "Mã loại tệp tin đa phương tiện"),
                    Search = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Active = table.Column<bool>(type: "bit", nullable: false, defaultValueSql: "((1))", comment: "Đánh dấu bị xóa"),
                    CreatedTime = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "(getdate())", comment: "Ngày tạo")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_JobMultimedia", x => x.Id);
                    table.ForeignKey(
                        name: "FK_JobMultimedia_Job",
                        column: x => x.JobId,
                        principalTable: "Job",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_JobMultimedia_MultimediaType",
                        column: x => x.MultimediaTypeId,
                        principalTable: "MultimediaType",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "JobReasonApply",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1001, 1"),
                    JobId = table.Column<long>(type: "bigint", nullable: false, comment: "Mã tin tuyển dụng"),
                    Active = table.Column<bool>(type: "bit", nullable: false, defaultValueSql: "((1))", comment: "Đánh dấu bị xóa"),
                    CreatedTime = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "(getdate())", comment: "Ngày tạo"),
                    Name = table.Column<string>(type: "nvarchar(450)", nullable: false, comment: "Lý do nên ứng tuyển"),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false, comment: "Chi tiết lý do nên ứng tuyển"),
                    Search = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_JobReasonApply", x => x.Id);
                    table.ForeignKey(
                        name: "FK_JobReasonApply_Job",
                        column: x => x.JobId,
                        principalTable: "Job",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "JobRequireCity",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1001, 1"),
                    JobId = table.Column<long>(type: "bigint", nullable: false, comment: "Mã tin tuyển dụng"),
                    CityId = table.Column<int>(type: "int", nullable: false, comment: "Mã thành phố"),
                    Search = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Active = table.Column<bool>(type: "bit", nullable: false, defaultValueSql: "((1))", comment: "Đánh dấu bị xóa"),
                    CreatedTime = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "(getdate())", comment: "Ngày tạo")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_JobRequireCity", x => x.Id);
                    table.ForeignKey(
                        name: "FK_JobRequireCity_Job",
                        column: x => x.JobId,
                        principalTable: "Job",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "JobRequireJobSkill",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1001, 1"),
                    JobId = table.Column<long>(type: "bigint", nullable: false, comment: "Mã tin tuyển dụng"),
                    JobSkillId = table.Column<int>(type: "int", nullable: false, comment: "Mã kĩ năng yêu cầu"),
                    Active = table.Column<bool>(type: "bit", nullable: false, defaultValueSql: "((1))", comment: "Đánh dấu bị xóa"),
                    CreatedTime = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "(getdate())", comment: "Ngày tạo")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_JobRequireJobSkill", x => x.Id);
                    table.ForeignKey(
                        name: "FK_JobRequireJobSkill_Job",
                        column: x => x.JobId,
                        principalTable: "Job",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_JobRequireJobSkill_JobSkill",
                        column: x => x.JobSkillId,
                        principalTable: "JobSkill",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "JobSecondaryJobPosition",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1001, 1"),
                    JobId = table.Column<long>(type: "bigint", nullable: false, comment: "Mã tin tuyển dụng"),
                    JobPositionId = table.Column<int>(type: "int", nullable: false, comment: "Mã vị trí tuyển dụng"),
                    Active = table.Column<bool>(type: "bit", nullable: false, defaultValueSql: "((1))", comment: "Đánh dấu bị xóa"),
                    CreatedTime = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "(getdate())", comment: "Ngày tạo")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_JobSecondaryJobPosition", x => x.Id);
                    table.ForeignKey(
                        name: "FK_JobSecondaryJobPosition_Job",
                        column: x => x.JobId,
                        principalTable: "Job",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_JobSecondaryJobPosition_JobPosition",
                        column: x => x.JobPositionId,
                        principalTable: "JobPosition",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "JobShowOnHomePage",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1001, 1"),
                    JobId = table.Column<long>(type: "bigint", nullable: false, comment: "Mã việc làm"),
                    OrderSort = table.Column<int>(type: "int", nullable: false, comment: "Sắp xếp"),
                    Active = table.Column<bool>(type: "bit", nullable: false, defaultValueSql: "((1))", comment: "Đánh dấu bị xóa"),
                    CreatedTime = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "(getdate())", comment: "Ngày tạo")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_JobShowOnHomePage", x => x.Id);
                    table.ForeignKey(
                        name: "FK_JobShowOnHomePage_Job",
                        column: x => x.JobId,
                        principalTable: "Job",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "EmployerOrderVoucher",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1001, 1"),
                    EmployerOrderId = table.Column<long>(type: "bigint", nullable: false, comment: "Mã đơn hàng nhà tuyển dụng"),
                    VoucherId = table.Column<long>(type: "bigint", nullable: false, comment: "Mã khuyến mãi"),
                    Active = table.Column<bool>(type: "bit", nullable: false, defaultValueSql: "((1))", comment: "Đánh dấu bị xóa"),
                    CreatedTime = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "(getdate())", comment: "Ngày tạo")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EmployerOrderVoucher", x => x.Id);
                    table.ForeignKey(
                        name: "FK_EmployerOrder_EmployerOrderVoucher",
                        column: x => x.EmployerOrderId,
                        principalTable: "EmployerOrders",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_EmployerVoucher_EmployerOrderVoucher",
                        column: x => x.VoucherId,
                        principalTable: "EmployerVoucher",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "InterviewSchedule",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false, comment: "Mã lịch phỏng vấn")
                        .Annotation("SqlServer:Identity", "1001, 1"),
                    InterviewscheduleTypeId = table.Column<int>(type: "int", nullable: false),
                    InterviewscheduleStatusId = table.Column<int>(type: "int", nullable: false),
                    CandidateApplyJobId = table.Column<long>(type: "bigint", nullable: false),
                    Link = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false, comment: "Liên kết lịch phỏng vấn"),
                    StateDate = table.Column<DateTime>(type: "datetime2", nullable: false, comment: "Ngày bắt đầu"),
                    EndDate = table.Column<DateTime>(type: "datetime2", nullable: false, comment: "Ngày kết thúc"),
                    Location = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false, comment: "Vị trí"),
                    Search = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Active = table.Column<bool>(type: "bit", nullable: false, defaultValueSql: "((1))", comment: "Đánh dấu bị xóa"),
                    CreatedTime = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "(getdate())", comment: "Ngày tạo"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true, comment: "Mô tả")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_InterviewSchedule", x => x.Id);
                    table.ForeignKey(
                        name: "FK_InterviewSchedule_CandidateApplyJob",
                        column: x => x.CandidateApplyJobId,
                        principalTable: "CandidateApplyJob",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_InterviewSchedule_InterviewStatus",
                        column: x => x.InterviewscheduleStatusId,
                        principalTable: "InterviewStatus",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_InterviewSchedule_InterviewType",
                        column: x => x.InterviewscheduleTypeId,
                        principalTable: "InterviewType",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "JobRequireDistrict",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1001, 1"),
                    JobRequireCityId = table.Column<long>(type: "bigint", nullable: false, comment: "Mã tỉnh/thành phố"),
                    DistrictId = table.Column<int>(type: "int", nullable: false, comment: "Mã quận/huyện"),
                    AddressDetail = table.Column<string>(type: "nvarchar(max)", nullable: false, comment: "Địa chỉ chi tiết"),
                    Search = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Active = table.Column<bool>(type: "bit", nullable: false, defaultValueSql: "((1))", comment: "Đánh dấu bị xóa"),
                    CreatedTime = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "(getdate())", comment: "Ngày tạo")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_JobRequireDistrict", x => x.Id);
                    table.ForeignKey(
                        name: "FK_JobRequireDistrict_JobRequireCity",
                        column: x => x.JobRequireCityId,
                        principalTable: "JobRequireCity",
                        principalColumn: "Id");
                });

            migrationBuilder.InsertData(
                table: "AccountStatus",
                columns: new[] { "Id", "Active", "Color", "CreatedTime", "Description", "Name" },
                values: new object[] { 1001, true, "green", new DateTime(2023, 7, 26, 17, 40, 2, 749, DateTimeKind.Local).AddTicks(3001), null, "Active" });

            migrationBuilder.InsertData(
                table: "AccountStatus",
                columns: new[] { "Id", "Active", "Color", "CreatedTime", "Description", "Name" },
                values: new object[] { 1002, true, "red", new DateTime(2023, 7, 26, 17, 40, 2, 749, DateTimeKind.Local).AddTicks(3034), null, "Block" });

            migrationBuilder.InsertData(
                table: "AdminAccount",
                columns: new[] { "Id", "Active", "CreatedTime", "Description", "Email", "FullName", "LockEndDate", "Password", "Phone", "Photo", "Search", "UserName" },
                values: new object[] { 1001L, true, new DateTime(2023, 7, 26, 17, 40, 2, 749, DateTimeKind.Local).AddTicks(3053), null, "dion@info.vn", "admin", null, "7828d7aa6efcf983b850025a6ceccad25905f5ecfa1758edbd1715d012747f2e", "0123456789", "/uploads/admin/default_avatar.jpg", "admin dion@info.vn", "admin" });

            migrationBuilder.CreateIndex(
                name: "IX_AdminAccountRole_AdminAccountId_RoleId",
                table: "AdminAccountRole",
                columns: new[] { "AdminAccountId", "RoleId" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_AdminAccountRole_RoleId",
                table: "AdminAccountRole",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "IX_Candidate_CandidateLevelId",
                table: "Candidate",
                column: "CandidateLevelId");

            migrationBuilder.CreateIndex(
                name: "IX_Candidate_CandidateStatusId",
                table: "Candidate",
                column: "CandidateStatusId");

            migrationBuilder.CreateIndex(
                name: "IX_CandidateActivities_CandidateId",
                table: "CandidateActivities",
                column: "CandidateId");

            migrationBuilder.CreateIndex(
                name: "IX_CandidateApplyJob_CandidateApplyJobSourceId",
                table: "CandidateApplyJob",
                column: "CandidateApplyJobSourceId");

            migrationBuilder.CreateIndex(
                name: "IX_CandidateApplyJob_CandidateApplyJobStatusId",
                table: "CandidateApplyJob",
                column: "CandidateApplyJobStatusId");

            migrationBuilder.CreateIndex(
                name: "IX_CandidateApplyJob_CandidateId",
                table: "CandidateApplyJob",
                column: "CandidateId");

            migrationBuilder.CreateIndex(
                name: "IX_CandidateApplyJob_JobId",
                table: "CandidateApplyJob",
                column: "JobId");

            migrationBuilder.CreateIndex(
                name: "IX_CandidateApplyJobStatus_Color",
                table: "CandidateApplyJobStatus",
                column: "Color",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_CandidateCertificate_CandidateId",
                table: "CandidateCertificate",
                column: "CandidateId");

            migrationBuilder.CreateIndex(
                name: "IX_CandidateCoupon_CandidateId",
                table: "CandidateCoupon",
                column: "CandidateId");

            migrationBuilder.CreateIndex(
                name: "IX_CandidateCoupon_CouponId",
                table: "CandidateCoupon",
                column: "CouponId");

            migrationBuilder.CreateIndex(
                name: "IX_CandidateEducation_CandidateId",
                table: "CandidateEducation",
                column: "CandidateId");

            migrationBuilder.CreateIndex(
                name: "IX_CandidateFollowCompany_CandidateId",
                table: "CandidateFollowCompany",
                column: "CandidateId");

            migrationBuilder.CreateIndex(
                name: "IX_CandidateFollowCompany_CompanyId",
                table: "CandidateFollowCompany",
                column: "CompanyId");

            migrationBuilder.CreateIndex(
                name: "IX_CandidateHonorAndAward_CandidateId",
                table: "CandidateHonorAndAward",
                column: "CandidateId");

            migrationBuilder.CreateIndex(
                name: "IX_CandidateIgnoreJob_CandidateId",
                table: "CandidateIgnoreJob",
                column: "CandidateId");

            migrationBuilder.CreateIndex(
                name: "IX_CandidateIgnoreJob_JobId",
                table: "CandidateIgnoreJob",
                column: "JobId");

            migrationBuilder.CreateIndex(
                name: "IX_CandidateLevelCandidateLevelBenefit_CandidateLevelBenefitId",
                table: "CandidateLevelCandidateLevelBenefit",
                column: "CandidateLevelBenefitId");

            migrationBuilder.CreateIndex(
                name: "IX_CandidateLevelCandidateLevelBenefit_CandidateLevelId",
                table: "CandidateLevelCandidateLevelBenefit",
                column: "CandidateLevelId");

            migrationBuilder.CreateIndex(
                name: "IX_CandidateMeta_CandidateId",
                table: "CandidateMeta",
                column: "CandidateId");

            migrationBuilder.CreateIndex(
                name: "IX_CandidateNotification_CandidateId",
                table: "CandidateNotification",
                column: "CandidateId");

            migrationBuilder.CreateIndex(
                name: "IX_CandidateNotification_NotificationStatusId",
                table: "CandidateNotification",
                column: "NotificationStatusId");

            migrationBuilder.CreateIndex(
                name: "IX_CandidateNotification_NotificationTypeId",
                table: "CandidateNotification",
                column: "NotificationTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_CandidateOrderCoupon_CandidateOrderId",
                table: "CandidateOrderCoupon",
                column: "CandidateOrderId");

            migrationBuilder.CreateIndex(
                name: "IX_CandidateOrderCoupon_CouponId",
                table: "CandidateOrderCoupon",
                column: "CouponId");

            migrationBuilder.CreateIndex(
                name: "IX_CandidateOrderDetail_CandidateLevelId",
                table: "CandidateOrderDetail",
                column: "CandidateLevelId");

            migrationBuilder.CreateIndex(
                name: "IX_CandidateOrderDetail_CandidateOrderId",
                table: "CandidateOrderDetail",
                column: "CandidateOrderId");

            migrationBuilder.CreateIndex(
                name: "IX_CandidateOrders_CandidateId",
                table: "CandidateOrders",
                column: "CandidateId");

            migrationBuilder.CreateIndex(
                name: "IX_CandidateOrders_OrderStatusId",
                table: "CandidateOrders",
                column: "OrderStatusId");

            migrationBuilder.CreateIndex(
                name: "IX_CandidateOrders_PaymentMethodId",
                table: "CandidateOrders",
                column: "PaymentMethodId");

            migrationBuilder.CreateIndex(
                name: "IX_CandidatePassword_CandidateId",
                table: "CandidatePassword",
                column: "CandidateId");

            migrationBuilder.CreateIndex(
                name: "IX_CandidateProjects_CandidateId",
                table: "CandidateProjects",
                column: "CandidateId");

            migrationBuilder.CreateIndex(
                name: "IX_CandidateSaveJob_CandidateId",
                table: "CandidateSaveJob",
                column: "CandidateId");

            migrationBuilder.CreateIndex(
                name: "IX_CandidateSaveJob_JobId",
                table: "CandidateSaveJob",
                column: "JobId");

            migrationBuilder.CreateIndex(
                name: "IX_CandidateSuggestionJobCategory_CandidateId",
                table: "CandidateSuggestionJobCategory",
                column: "CandidateId");

            migrationBuilder.CreateIndex(
                name: "IX_CandidateSuggestionJobCategory_JobCategoryId",
                table: "CandidateSuggestionJobCategory",
                column: "JobCategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_CandidateSuggestionJobPosition_CandidateId",
                table: "CandidateSuggestionJobPosition",
                column: "CandidateId");

            migrationBuilder.CreateIndex(
                name: "IX_CandidateSuggestionJobPosition_JobPositionId",
                table: "CandidateSuggestionJobPosition",
                column: "JobPositionId");

            migrationBuilder.CreateIndex(
                name: "IX_CandidateSuggestionJobSkill_CandidateId",
                table: "CandidateSuggestionJobSkill",
                column: "CandidateId");

            migrationBuilder.CreateIndex(
                name: "IX_CandidateSuggestionJobSkill_JobSkillId",
                table: "CandidateSuggestionJobSkill",
                column: "JobSkillId");

            migrationBuilder.CreateIndex(
                name: "IX_CandidateSuggestionWorkPlace_CandidateId",
                table: "CandidateSuggestionWorkPlace",
                column: "CandidateId");

            migrationBuilder.CreateIndex(
                name: "IX_CandidateSuggestionWorkPlace_WorkPlaceId",
                table: "CandidateSuggestionWorkPlace",
                column: "WorkPlaceId");

            migrationBuilder.CreateIndex(
                name: "IX_CandidateViewedJob_CandidateId",
                table: "CandidateViewedJob",
                column: "CandidateId");

            migrationBuilder.CreateIndex(
                name: "IX_CandidateWorkExperience_CandidateId",
                table: "CandidateWorkExperience",
                column: "CandidateId");

            migrationBuilder.CreateIndex(
                name: "IX_Company_CompanySizeId",
                table: "Company",
                column: "CompanySizeId");

            migrationBuilder.CreateIndex(
                name: "IX_Company_EmployerId",
                table: "Company",
                column: "EmployerId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Company_Name",
                table: "Company",
                column: "Name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Company_Phone",
                table: "Company",
                column: "Phone",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Company_TaxCode",
                table: "Company",
                column: "TaxCode",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Company_Website",
                table: "Company",
                column: "Website",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_CompanyFieldOfActivity_CompanyId",
                table: "CompanyFieldOfActivity",
                column: "CompanyId");

            migrationBuilder.CreateIndex(
                name: "IX_CompanyFieldOfActivity_FieldOfActivityId",
                table: "CompanyFieldOfActivity",
                column: "FieldOfActivityId");

            migrationBuilder.CreateIndex(
                name: "IX_CompanyImage_CompanyId",
                table: "CompanyImage",
                column: "CompanyId");

            migrationBuilder.CreateIndex(
                name: "IX_CompanyReview_CandidateId",
                table: "CompanyReview",
                column: "CandidateId");

            migrationBuilder.CreateIndex(
                name: "IX_CompanyReview_CompanyId",
                table: "CompanyReview",
                column: "CompanyId");

            migrationBuilder.CreateIndex(
                name: "IX_CompanyShowOnHomePage_CompanyId",
                table: "CompanyShowOnHomePage",
                column: "CompanyId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_CompanySize_Name",
                table: "CompanySize",
                column: "Name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Coupon_Code",
                table: "Coupon",
                column: "Code",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Coupon_CouponTypeId",
                table: "Coupon",
                column: "CouponTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_Employer_Email",
                table: "Employer",
                column: "Email",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Employer_EmployerServicePackageId",
                table: "Employer",
                column: "EmployerServicePackageId");

            migrationBuilder.CreateIndex(
                name: "IX_Employer_EmployerStatusId",
                table: "Employer",
                column: "EmployerStatusId");

            migrationBuilder.CreateIndex(
                name: "IX_Employer_Phone",
                table: "Employer",
                column: "Phone",
                unique: true,
                filter: "[Phone] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_Employer_PositionId",
                table: "Employer",
                column: "PositionId");

            migrationBuilder.CreateIndex(
                name: "IX_Employer_SkypeAccount",
                table: "Employer",
                column: "SkypeAccount",
                unique: true,
                filter: "[SkypeAccount] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_Employer_Username",
                table: "Employer",
                column: "Username",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_EmployerActivityLog_EmployerActivityLogTypeId",
                table: "EmployerActivityLog",
                column: "EmployerActivityLogTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_EmployerMeta_EmployerId",
                table: "EmployerMeta",
                column: "EmployerId");

            migrationBuilder.CreateIndex(
                name: "IX_EmployerMeta_Key",
                table: "EmployerMeta",
                column: "Key",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_EmployerMeta_Name",
                table: "EmployerMeta",
                column: "Name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_EmployerNotification_EmployerId",
                table: "EmployerNotification",
                column: "EmployerId");

            migrationBuilder.CreateIndex(
                name: "IX_EmployerNotification_NotificationStatusId",
                table: "EmployerNotification",
                column: "NotificationStatusId");

            migrationBuilder.CreateIndex(
                name: "IX_EmployerNotification_NotificationTypeId",
                table: "EmployerNotification",
                column: "NotificationTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_EmployerOrderDetail_EmployerServicePackageId",
                table: "EmployerOrderDetail",
                column: "EmployerServicePackageId");

            migrationBuilder.CreateIndex(
                name: "IX_EmployerOrderDetail_OrderId",
                table: "EmployerOrderDetail",
                column: "OrderId");

            migrationBuilder.CreateIndex(
                name: "IX_EmployerOrders_EmployerId",
                table: "EmployerOrders",
                column: "EmployerId");

            migrationBuilder.CreateIndex(
                name: "IX_EmployerOrders_OrderStatusId",
                table: "EmployerOrders",
                column: "OrderStatusId");

            migrationBuilder.CreateIndex(
                name: "IX_EmployerOrders_PaymentMethodId",
                table: "EmployerOrders",
                column: "PaymentMethodId");

            migrationBuilder.CreateIndex(
                name: "IX_EmployerOrderVoucher_EmployerOrderId",
                table: "EmployerOrderVoucher",
                column: "EmployerOrderId");

            migrationBuilder.CreateIndex(
                name: "IX_EmployerOrderVoucher_VoucherId",
                table: "EmployerOrderVoucher",
                column: "VoucherId");

            migrationBuilder.CreateIndex(
                name: "IX_EmployerPassword_EmployerId",
                table: "EmployerPassword",
                column: "EmployerId");

            migrationBuilder.CreateIndex(
                name: "IX_EmployerServicePackageEmployerBenefit_EmployerBenefitId",
                table: "EmployerServicePackageEmployerBenefit",
                column: "EmployerBenefitId");

            migrationBuilder.CreateIndex(
                name: "IX_EmployerServicePackageEmployerBenefit_EmployerServicePackageId",
                table: "EmployerServicePackageEmployerBenefit",
                column: "EmployerServicePackageId");

            migrationBuilder.CreateIndex(
                name: "IX_EmployerViewedCV_CandidateId",
                table: "EmployerViewedCV",
                column: "CandidateId");

            migrationBuilder.CreateIndex(
                name: "IX_EmployerViewedCV_EmployerId",
                table: "EmployerViewedCV",
                column: "EmployerId");

            migrationBuilder.CreateIndex(
                name: "IX_EmployerVoucher_EmployerId",
                table: "EmployerVoucher",
                column: "EmployerId");

            migrationBuilder.CreateIndex(
                name: "IX_EmployerVoucher_VoucherId",
                table: "EmployerVoucher",
                column: "VoucherId");

            migrationBuilder.CreateIndex(
                name: "IX_FieldOfActivity_Name",
                table: "FieldOfActivity",
                column: "Name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_InterviewSchedule_CandidateApplyJobId",
                table: "InterviewSchedule",
                column: "CandidateApplyJobId");

            migrationBuilder.CreateIndex(
                name: "IX_InterviewSchedule_InterviewscheduleStatusId",
                table: "InterviewSchedule",
                column: "InterviewscheduleStatusId");

            migrationBuilder.CreateIndex(
                name: "IX_InterviewSchedule_InterviewscheduleTypeId",
                table: "InterviewSchedule",
                column: "InterviewscheduleTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_InterviewStatus_Color",
                table: "InterviewStatus",
                column: "Color",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Job_ExperienceRangeId",
                table: "Job",
                column: "ExperienceRangeId");

            migrationBuilder.CreateIndex(
                name: "IX_Job_JobStatusId",
                table: "Job",
                column: "JobStatusId");

            migrationBuilder.CreateIndex(
                name: "IX_Job_JobTypeId",
                table: "Job",
                column: "JobTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_Job_PrimaryJobPositionId",
                table: "Job",
                column: "PrimaryJobPositionId");

            migrationBuilder.CreateIndex(
                name: "IX_Job_RecruimentCampaignId",
                table: "Job",
                column: "RecruimentCampaignId");

            migrationBuilder.CreateIndex(
                name: "IX_Job_SalaryTypeId",
                table: "Job",
                column: "SalaryTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_JobMultimedia_JobId",
                table: "JobMultimedia",
                column: "JobId");

            migrationBuilder.CreateIndex(
                name: "IX_JobMultimedia_MultimediaTypeId",
                table: "JobMultimedia",
                column: "MultimediaTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_JobReasonApply_JobId",
                table: "JobReasonApply",
                column: "JobId");

            migrationBuilder.CreateIndex(
                name: "IX_JobReasonApply_Name",
                table: "JobReasonApply",
                column: "Name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_JobRequireCity_JobId",
                table: "JobRequireCity",
                column: "JobId");

            migrationBuilder.CreateIndex(
                name: "IX_JobRequireDistrict_JobRequireCityId",
                table: "JobRequireDistrict",
                column: "JobRequireCityId");

            migrationBuilder.CreateIndex(
                name: "IX_JobRequireJobSkill_JobId",
                table: "JobRequireJobSkill",
                column: "JobId");

            migrationBuilder.CreateIndex(
                name: "IX_JobRequireJobSkill_JobSkillId",
                table: "JobRequireJobSkill",
                column: "JobSkillId");

            migrationBuilder.CreateIndex(
                name: "IX_JobSecondaryJobPosition_JobId",
                table: "JobSecondaryJobPosition",
                column: "JobId");

            migrationBuilder.CreateIndex(
                name: "IX_JobSecondaryJobPosition_JobPositionId",
                table: "JobSecondaryJobPosition",
                column: "JobPositionId");

            migrationBuilder.CreateIndex(
                name: "IX_JobShowOnHomePage_JobId",
                table: "JobShowOnHomePage",
                column: "JobId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_JobStatus_Color",
                table: "JobStatus",
                column: "Color",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_License_CompanyId",
                table: "License",
                column: "CompanyId");

            migrationBuilder.CreateIndex(
                name: "IX_License_LicenseTypeId",
                table: "License",
                column: "LicenseTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_OrderStatus_Color",
                table: "OrderStatus",
                column: "Color",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Post_AuthorId",
                table: "Post",
                column: "AuthorId");

            migrationBuilder.CreateIndex(
                name: "IX_Post_PostCategoryId",
                table: "Post",
                column: "PostCategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_Post_PostLayoutId",
                table: "Post",
                column: "PostLayoutId");

            migrationBuilder.CreateIndex(
                name: "IX_Post_PostStatusId",
                table: "Post",
                column: "PostStatusId");

            migrationBuilder.CreateIndex(
                name: "IX_Post_PostTypeId",
                table: "Post",
                column: "PostTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_PostStatus_Color",
                table: "PostStatus",
                column: "Color",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_PostTag_PostId_TagId",
                table: "PostTag",
                columns: new[] { "PostId", "TagId" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_PostTag_TagId",
                table: "PostTag",
                column: "TagId");

            migrationBuilder.CreateIndex(
                name: "IX_RecruitmentCampaign_Name",
                table: "RecruitmentCampaign",
                column: "Name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_RecruitmentCampaign_RecruitmentCampaignStatusId",
                table: "RecruitmentCampaign",
                column: "RecruitmentCampaignStatusId");

            migrationBuilder.CreateIndex(
                name: "IX_RecruitmentCampaignRequireJobPosition_JobPositionId",
                table: "RecruitmentCampaignRequireJobPosition",
                column: "JobPositionId");

            migrationBuilder.CreateIndex(
                name: "IX_RecruitmentCampaignRequireJobPosition_RecruitmentCampaignId",
                table: "RecruitmentCampaignRequireJobPosition",
                column: "RecruitmentCampaignId");

            migrationBuilder.CreateIndex(
                name: "IX_RecruitmentCampaignRequireWorkPlace_RecruitmentCampaignId",
                table: "RecruitmentCampaignRequireWorkPlace",
                column: "RecruitmentCampaignId");

            migrationBuilder.CreateIndex(
                name: "IX_RecruitmentCampaignRequireWorkPlace_WorkPlaceId",
                table: "RecruitmentCampaignRequireWorkPlace",
                column: "WorkPlaceId");

            migrationBuilder.CreateIndex(
                name: "IX_RecruitmentCampaignStatus_Color",
                table: "RecruitmentCampaignStatus",
                column: "Color",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_RecruitmentStatus_Color",
                table: "RecruitmentStatus",
                column: "Color",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_RoleMenu_MenuId",
                table: "RoleMenu",
                column: "MenuId");

            migrationBuilder.CreateIndex(
                name: "IX_RoleMenu_RoleId_MenuId",
                table: "RoleMenu",
                columns: new[] { "RoleId", "MenuId" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_RolePermissions_PermissionId",
                table: "RolePermissions",
                column: "PermissionId");

            migrationBuilder.CreateIndex(
                name: "IX_RolePermissions_RoleId_PermissionId",
                table: "RolePermissions",
                columns: new[] { "RoleId", "PermissionId" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Tag_TagTypeId",
                table: "Tag",
                column: "TagTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_UploadFile_AdminAccountId",
                table: "UploadFile",
                column: "AdminAccountId");

            migrationBuilder.CreateIndex(
                name: "IX_UploadFile_FolderUploadId",
                table: "UploadFile",
                column: "FolderUploadId");

            migrationBuilder.CreateIndex(
                name: "IX_Voucher_VoucherTypeId",
                table: "Voucher",
                column: "VoucherTypeId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AdminAccountRole");

            migrationBuilder.DropTable(
                name: "CandidateActivities");

            migrationBuilder.DropTable(
                name: "CandidateCertificate");

            migrationBuilder.DropTable(
                name: "CandidateCoupon");

            migrationBuilder.DropTable(
                name: "CandidateEducation");

            migrationBuilder.DropTable(
                name: "CandidateFollowCompany");

            migrationBuilder.DropTable(
                name: "CandidateHonorAndAward");

            migrationBuilder.DropTable(
                name: "CandidateIgnoreJob");

            migrationBuilder.DropTable(
                name: "CandidateLevelCandidateLevelBenefit");

            migrationBuilder.DropTable(
                name: "CandidateMeta");

            migrationBuilder.DropTable(
                name: "CandidateNotification");

            migrationBuilder.DropTable(
                name: "CandidateOrderCoupon");

            migrationBuilder.DropTable(
                name: "CandidateOrderDetail");

            migrationBuilder.DropTable(
                name: "CandidatePassword");

            migrationBuilder.DropTable(
                name: "CandidateProjects");

            migrationBuilder.DropTable(
                name: "CandidateSaveJob");

            migrationBuilder.DropTable(
                name: "CandidateSkill");

            migrationBuilder.DropTable(
                name: "CandidateSuggestionJobCategory");

            migrationBuilder.DropTable(
                name: "CandidateSuggestionJobPosition");

            migrationBuilder.DropTable(
                name: "CandidateSuggestionJobSkill");

            migrationBuilder.DropTable(
                name: "CandidateSuggestionWorkPlace");

            migrationBuilder.DropTable(
                name: "CandidateViewedJob");

            migrationBuilder.DropTable(
                name: "CandidateWorkExperience");

            migrationBuilder.DropTable(
                name: "CompanyFieldOfActivity");

            migrationBuilder.DropTable(
                name: "CompanyImage");

            migrationBuilder.DropTable(
                name: "CompanyReview");

            migrationBuilder.DropTable(
                name: "CompanyShowOnHomePage");

            migrationBuilder.DropTable(
                name: "EmployerActivityLog");

            migrationBuilder.DropTable(
                name: "EmployerMeta");

            migrationBuilder.DropTable(
                name: "EmployerNotification");

            migrationBuilder.DropTable(
                name: "EmployerOrderDetail");

            migrationBuilder.DropTable(
                name: "EmployerOrderVoucher");

            migrationBuilder.DropTable(
                name: "EmployerPassword");

            migrationBuilder.DropTable(
                name: "EmployerServicePackageEmployerBenefit");

            migrationBuilder.DropTable(
                name: "EmployerViewedCV");

            migrationBuilder.DropTable(
                name: "InterviewSchedule");

            migrationBuilder.DropTable(
                name: "JobMultimedia");

            migrationBuilder.DropTable(
                name: "JobReasonApply");

            migrationBuilder.DropTable(
                name: "JobRequireDistrict");

            migrationBuilder.DropTable(
                name: "JobRequireJobSkill");

            migrationBuilder.DropTable(
                name: "JobSecondaryJobPosition");

            migrationBuilder.DropTable(
                name: "JobShowOnHomePage");

            migrationBuilder.DropTable(
                name: "License");

            migrationBuilder.DropTable(
                name: "Occupation");

            migrationBuilder.DropTable(
                name: "OrderType");

            migrationBuilder.DropTable(
                name: "PostTag");

            migrationBuilder.DropTable(
                name: "RecruitmentCampaignRequireJobPosition");

            migrationBuilder.DropTable(
                name: "RecruitmentCampaignRequireWorkPlace");

            migrationBuilder.DropTable(
                name: "RecruitmentStatus");

            migrationBuilder.DropTable(
                name: "RoleMenu");

            migrationBuilder.DropTable(
                name: "RolePermissions");

            migrationBuilder.DropTable(
                name: "SalaryRange");

            migrationBuilder.DropTable(
                name: "Slide");

            migrationBuilder.DropTable(
                name: "SystemConfig");

            migrationBuilder.DropTable(
                name: "UploadFile");

            migrationBuilder.DropTable(
                name: "CandidateLevelBenefit");

            migrationBuilder.DropTable(
                name: "Coupon");

            migrationBuilder.DropTable(
                name: "CandidateOrders");

            migrationBuilder.DropTable(
                name: "JobCategory");

            migrationBuilder.DropTable(
                name: "FieldOfActivity");

            migrationBuilder.DropTable(
                name: "EmployerActivityLogType");

            migrationBuilder.DropTable(
                name: "NotificationStatus");

            migrationBuilder.DropTable(
                name: "NotificationType");

            migrationBuilder.DropTable(
                name: "EmployerOrders");

            migrationBuilder.DropTable(
                name: "EmployerVoucher");

            migrationBuilder.DropTable(
                name: "EmployerBenefit");

            migrationBuilder.DropTable(
                name: "CandidateApplyJob");

            migrationBuilder.DropTable(
                name: "InterviewStatus");

            migrationBuilder.DropTable(
                name: "InterviewType");

            migrationBuilder.DropTable(
                name: "MultimediaType");

            migrationBuilder.DropTable(
                name: "JobRequireCity");

            migrationBuilder.DropTable(
                name: "JobSkill");

            migrationBuilder.DropTable(
                name: "Company");

            migrationBuilder.DropTable(
                name: "LicenseType");

            migrationBuilder.DropTable(
                name: "Post");

            migrationBuilder.DropTable(
                name: "Tag");

            migrationBuilder.DropTable(
                name: "WorkPlace");

            migrationBuilder.DropTable(
                name: "Menu");

            migrationBuilder.DropTable(
                name: "Permission");

            migrationBuilder.DropTable(
                name: "Role");

            migrationBuilder.DropTable(
                name: "FolderUpload");

            migrationBuilder.DropTable(
                name: "CouponType");

            migrationBuilder.DropTable(
                name: "OrderStatus");

            migrationBuilder.DropTable(
                name: "PaymentMethod");

            migrationBuilder.DropTable(
                name: "Voucher");

            migrationBuilder.DropTable(
                name: "Candidate");

            migrationBuilder.DropTable(
                name: "CandidateApplyJobSource");

            migrationBuilder.DropTable(
                name: "CandidateApplyJobStatus");

            migrationBuilder.DropTable(
                name: "Job");

            migrationBuilder.DropTable(
                name: "CompanySize");

            migrationBuilder.DropTable(
                name: "Employer");

            migrationBuilder.DropTable(
                name: "AdminAccount");

            migrationBuilder.DropTable(
                name: "PostCategory");

            migrationBuilder.DropTable(
                name: "PostLayout");

            migrationBuilder.DropTable(
                name: "PostStatus");

            migrationBuilder.DropTable(
                name: "PostType");

            migrationBuilder.DropTable(
                name: "TagType");

            migrationBuilder.DropTable(
                name: "VoucherType");

            migrationBuilder.DropTable(
                name: "CandidateLevel");

            migrationBuilder.DropTable(
                name: "ExperienceRange");

            migrationBuilder.DropTable(
                name: "JobStatus");

            migrationBuilder.DropTable(
                name: "JobType");

            migrationBuilder.DropTable(
                name: "JobPosition");

            migrationBuilder.DropTable(
                name: "RecruitmentCampaign");

            migrationBuilder.DropTable(
                name: "SalaryType");

            migrationBuilder.DropTable(
                name: "EmployerServicePackage");

            migrationBuilder.DropTable(
                name: "AccountStatus");

            migrationBuilder.DropTable(
                name: "Position");

            migrationBuilder.DropTable(
                name: "RecruitmentCampaignStatus");
        }
    }
}
