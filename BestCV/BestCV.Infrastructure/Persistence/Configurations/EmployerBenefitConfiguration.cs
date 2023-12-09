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
    public class EmployerBenefitConfiguration : IEntityTypeConfiguration<EmployerBenefit>
    {
        public void Configure(EntityTypeBuilder<EmployerBenefit> builder)
        {
            builder.ToTable("EmployerBenefit");
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id)
                .ValueGeneratedOnAdd()
                .UseIdentityColumn(1001, 1)
                .HasComment("Mã lợi ích của nhà tuyển dụng");
            builder.Property(e => e.Name).HasMaxLength(255).HasComment("Tên lợi ích cho nhà tuyển dụng");
            builder.Property(e => e.Description).HasMaxLength(500).HasComment("Mô tả");
            builder.Property(e => e.Active)
                .IsRequired()
                .HasDefaultValueSql("((1))")
                .HasComment("Đánh dấu bị xóa");
            builder.Property(e => e.CreatedTime)
                .HasDefaultValueSql("(getdate())")
                .HasComment("Ngày tạo");
        }
    }
}
