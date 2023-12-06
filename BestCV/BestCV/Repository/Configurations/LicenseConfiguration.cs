using Jobi.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Jobi.Infrastructure.Persistence.Configurations
{
    public class LicenseConfiguration : IEntityTypeConfiguration<License>
    {
        public void Configure(EntityTypeBuilder<License> builder)
        {
            builder.ToTable("License");
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id)
                .ValueGeneratedOnAdd()
                .UseIdentityColumn(1001, 1)
                .HasComment("Mã giấy tờ");
            builder.Property(e => e.CompanyId).IsRequired();
            builder.Property(e => e.LicenseTypeId).IsRequired();
            builder.Property(e => e.Path).HasComment("Ảnh giấy tờ");
            builder.Property(e => e.IsApproved).HasComment("Chấp nhận");
            builder.Property(e => e.ApprovalDate).HasComment("Ngày chấp nhận");
            builder.Property(e => e.Active)
                .IsRequired()
                .HasDefaultValueSql("((1))")
                .HasComment("Đánh dấu bị xóa");
            builder.Property(e => e.CreatedTime)
                .HasDefaultValueSql("(getdate())")
                .HasComment("Ngày tạo");

            builder.HasOne(x=> x.Company).WithMany(x=> x.Licenses)
                .HasForeignKey(x => x.CompanyId)
                .HasConstraintName("FK_License_Company")
                .OnDelete(DeleteBehavior.ClientSetNull);

            builder.HasOne(x => x.LicenseType).WithMany(x => x.Licenses)
                .HasForeignKey(x => x.LicenseTypeId)
                .HasConstraintName("FK_License_LicenseType")
                .OnDelete(DeleteBehavior.ClientSetNull);
        }
    }
}
