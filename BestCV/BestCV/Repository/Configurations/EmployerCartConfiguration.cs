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
    public class EmployerCartConfiguration : IEntityTypeConfiguration<EmployerCart>
    {
        public void Configure(EntityTypeBuilder<EmployerCart> builder)
        {
            builder.ToTable("EmployerCart");
            builder.Property(c => c.Id).ValueGeneratedOnAdd()
                .UseIdentityColumn(1001, 1)
                .HasComment("Mã");
            builder.Property(e => e.Active)
                .IsRequired()
                .HasDefaultValue(true)
                .HasComment("Đánh dấu bị xóa");
            builder.Property(e => e.CreatedTime)
                .HasDefaultValueSql("(getdate())")
                .HasComment("Ngày tạo");
            builder.Property(e => e.Description).HasMaxLength(500).HasComment("Mô tả");
            builder.Property(c => c.Quantity).HasDefaultValue(1).HasComment("Số lượng");
            builder.HasKey(c => c.Id);
            builder.HasOne(c => c.Employer).WithMany(c => c.EmployerCarts).HasForeignKey(c => c.EmployerId).HasConstraintName("FK_EmployerCart_Employer");
            builder.HasOne(c => c.EmployerServicePackage).WithMany(c => c.EmployerCarts).HasForeignKey(c => c.EmployerServicePackageId).HasConstraintName("FK_EmployerCart_EmployerServicePackage");
        }
    }
}
