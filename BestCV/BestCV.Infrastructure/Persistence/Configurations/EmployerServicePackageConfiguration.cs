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
    public class EmployerServicePackageConfiguration : IEntityTypeConfiguration<EmployerServicePackage>
    {
        public void Configure(EntityTypeBuilder<EmployerServicePackage> builder)
        {
            builder.ToTable("EmployerServicePackage");
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id)
                .ValueGeneratedOnAdd()
                .UseIdentityColumn(1001, 1)
                .HasComment("Mã gói dịch vụ cho nhà tuyển dụng");
            builder.Property(e => e.Name).HasMaxLength(255).HasComment("Tên gói lợi ích cho nhà tuyển dụng");
            builder.Property(e => e.Price).HasComment("Giá");
            builder.Property(e => e.DiscountPrice).HasComment("Giảm giá");
            builder.Property(e => e.DiscountEndDate)
                .HasComment("Ngày kết thúc mã giảm giá");
            //builder.Property(e => e.ExpiryTime).HasComment("Thời gian hết hạn");
            builder.Property(e => e.Description).HasMaxLength(500).HasComment("Mô tả");
            builder.Property(e => e.Active)
                .IsRequired()
                .HasDefaultValueSql("((1))")
                .HasComment("Đánh dấu bị xóa");
            builder.Property(e => e.CreatedTime)
                .HasDefaultValueSql("(getdate())")
                .HasComment("Ngày tạo");
            builder.HasOne(c => c.ServicePackageGroup)
                .WithMany(c => c.EmployerServicePackages)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasForeignKey(c => c.ServicePackageGroupId)
                .HasConstraintName("FK_EmployerServicePackage_ServicePackageGroup");
            builder.HasOne(c => c.ServicePackageType)
                .WithMany(c => c.EmployerServicePackages)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasForeignKey(c => c.ServicePackageGroupId)
                .HasConstraintName("FK_EmployerServicePackage_ServicePackageType");
        }
    }
}
