using BestCV.Core.Entities;
using BestCV.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BestCV.Infrastructure.Persistence.Configurations
{
    public class EmployerConfiguration : IEntityTypeConfiguration<Employer>
    {
        public void Configure(EntityTypeBuilder<Employer> builder)
        {
            builder.ToTable("Employer");
            builder.HasKey(x => x.Id);

            builder.Property(e => e.Id)
                .ValueGeneratedOnAdd()
                .UseIdentityColumn(1001, 1)
                .HasComment("Mã nhà tuyển dụng");
            builder.Property(x => x.Active)
                .HasDefaultValueSql("((1))")
                .HasComment("Đánh dấu bị xóa");
            builder.Property(x => x.Description)
                .HasComment("Mô tả")
                .HasMaxLength(500);
            builder.Property(x => x.CreatedTime)
                .HasDefaultValueSql("getdate()")
                .HasComment("Ngày tạo");
            builder.Property(x => x.Search)
                .HasComment("Tìm kiếm tổng")
                .IsRequired();

            builder.Property(x => x.IsActivated)
                .HasComment("Đánh dấu đã kích hoạt tài khoản")
                .HasDefaultValueSql("((0))");

            builder.Property(x => x.EmployerStatusId)
                .HasComment("Mã trạng thái nhà tuyển dụng")
                .IsRequired()
                .HasDefaultValue(1001);

            builder.Property(x => x.EmployerServicePackageEfficiencyExpiry)
                .HasComment("Thời gian hết hạn gói dịch vụ");

            builder.Property(x => x.PositionId)
                .HasComment("Mã chức vụ nhà tuyển dụng")
                .IsRequired();

            builder.Property(x => x.Gender)
                .HasComment("Giới tính")
                .IsRequired();

            builder.Property(x => x.Phone)
                .HasComment("Số điện thoại")
                .HasMaxLength(10);

            builder.Property(x => x.Email)
                .HasComment("Email")
                .IsRequired()
                .HasMaxLength(255);

            builder.Property(x => x.Photo)
                .HasComment("Ảnh đại diện")
                .HasMaxLength(500);

            builder.Property(x => x.Username)
                .HasComment("Tên tài khoản")
                .IsRequired()
                .HasMaxLength(255);

            builder.Property(x => x.Password)
                .HasComment("Mật khẩu")
                .IsRequired()
                .HasMaxLength(500);

            builder.Property(x => x.Fullname)
                .HasComment("Tên đầy đủ của nhà tuyển dụng.")
                .IsRequired()
                .HasMaxLength(255);

            builder.Property(x => x.SkypeAccount)
                .HasComment("Tài khoản Skype");

            builder.Property(x => x.AccessFailedCount)
                .HasComment("Số lần đăng nhập thất bại");

            builder.Property(x => x.LockEnabled)
                .HasComment("Đánh dấu bị khóa");

            builder.Property(x => x.LockEndDate)
                .HasComment("Thời gian tài khoản được mở");

            builder.HasIndex(x => x.Username).IsUnique();
            builder.HasIndex(x => x.Email).IsUnique();
            builder.HasIndex(x => x.Phone).IsUnique();
            //builder.HasIndex(x => x.SkypeAccount).IsUnique();

            builder.HasOne(d => d.EmployerStatus).WithMany(p => p.Employers)
                .HasForeignKey(d => d.EmployerStatusId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Employer_EmployerStatus");

          

            builder.HasOne(d => d.Position).WithMany(p => p.Employers)
                .HasForeignKey(d => d.PositionId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Employer_Position");
        }
    }
}
