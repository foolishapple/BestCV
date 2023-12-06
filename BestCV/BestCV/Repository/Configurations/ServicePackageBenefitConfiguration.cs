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
    public class ServicePackageBenefitConfiguration : IEntityTypeConfiguration<ServicePackageBenefit>
    {
        public void Configure(EntityTypeBuilder<ServicePackageBenefit> builder)
        {
            builder.ToTable("ServicePackageBenefit");
            builder.Property(c => c.Id).ValueGeneratedOnAdd()
                .UseIdentityColumn(1001, 1)
                .HasComment("Mã");
            builder.Property(e => e.Active)
                .IsRequired()
                .HasDefaultValueSql("((1))")
                .HasComment("Đánh dấu bị xóa");
            builder.Property(e => e.CreatedTime)
                .HasDefaultValueSql("(getdate())")
                .HasComment("Ngày tạo");
            builder.Property(e => e.Value).HasComment("Giá trị");
            builder.Property(e => e.Description).HasMaxLength(500).HasComment("Mô tả");
            builder.HasKey(c => c.Id);
            builder.HasOne(c => c.EmployerServicePackage)
                .WithMany(c => c.ServicePackageBenefits)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasForeignKey(c => c.EmployerServicePackageId)
                .HasConstraintName("FK_ServicePackageBenefit_EmployerServicePackage");
            builder.HasOne(c => c.Benefit)
                .WithMany(c => c.ServicePackageBenefits)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasForeignKey(c => c.BenefitId)
                .HasConstraintName("FK_ServicePackageBenefit_Benefit");
            builder.HasIndex(c => new {c.EmployerServicePackageId,c.BenefitId})
                .IsUnique();
        }
    }
}
