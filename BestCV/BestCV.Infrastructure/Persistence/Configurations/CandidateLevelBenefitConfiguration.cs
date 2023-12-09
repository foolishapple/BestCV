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
    public class CandidateLevelBenefitConfiguration : IEntityTypeConfiguration<CandidateLevelBenefit>
    {
        public void Configure(EntityTypeBuilder<CandidateLevelBenefit> builder)
        {
            builder.ToTable("CandidateLevelBenefit");
            builder.HasKey(c => c.Id);
            builder.Property(e => e.Id)
                .HasComment("Mã mức độ lợi ích của ứng viên ")
                .ValueGeneratedOnAdd()
                .UseIdentityColumn(1001, 1);

            builder.Property(e => e.Active)
                .IsRequired()
                .HasDefaultValueSql("((1))")
                .HasComment("Đánh dấu bị xóa");
            builder.Property(e => e.Name)
                .HasMaxLength(255)
                .HasComment("Tên mức độ lợi ích của ứng viên");
            builder.Property(e => e.CreatedTime)
                .HasDefaultValueSql("(getdate())")
                .HasComment("Ngày tạo");
            builder.Property(e => e.Description)
                .HasMaxLength(500)
                .HasComment("Mô tả");
            
        }
    }
}

