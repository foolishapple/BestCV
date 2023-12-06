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
    public class EmployerServicePackageEmployerBenefitConfiguration : IEntityTypeConfiguration<EmployerServicePackageEmployerBenefit>
    {
        public void Configure(EntityTypeBuilder<EmployerServicePackageEmployerBenefit> builder)
        {
            builder.ToTable("EmployerServicePackageEmployerBenefit");
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id)
                .ValueGeneratedOnAdd()
                .UseIdentityColumn(1001, 1)
                .HasComment("Mã quyền lợi gói dịch vụ cho nhà tuyển dụng");
            builder.Property(e => e.EmployerServicePackageId).IsRequired();
            builder.Property(e => e.EmployerBenefitId).IsRequired();
            builder.Property(e => e.Value).HasMaxLength(255).HasComment("Giá trị");
            builder.Property(e => e.HasBenefit).HasComment("Có quyền lợi hay không");
            builder.Property(e => e.Active)
                .IsRequired()
                .HasDefaultValueSql("((1))")
                .HasComment("Đánh dấu bị xóa");
            builder.Property(e => e.CreatedTime)
                .HasDefaultValueSql("(getdate())")
                .HasComment("Ngày tạo");

            builder.HasOne(d => d.EmployerBenefit).WithMany(p => p.EmployerServicePackageEmployerBenefits)
                .HasForeignKey(d => d.EmployerBenefitId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_EmployerBenefit_EmployerServicePackageEmployerBenefit");

            builder.HasOne(d => d.EmployerServicePackage).WithMany(p => p.EmployerServicePackageEmployerBenefits)
                .HasForeignKey(d => d.EmployerServicePackageId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_EmployerServicePackage_EmployerServicePackageEmployerBenefit");
        }
    }
}
