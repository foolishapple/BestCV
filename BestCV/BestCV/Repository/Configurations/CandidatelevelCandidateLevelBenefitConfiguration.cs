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
    public class CandidatelevelCandidateLevelBenefitConfiguration : IEntityTypeConfiguration<CandidateLevelCandidateLevelBenefit>
    {
        public void Configure(EntityTypeBuilder<CandidateLevelCandidateLevelBenefit> builder)
        {
            builder.ToTable("CandidateLevelCandidateLevelBenefit");
            builder.HasKey(c => c.Id);
            builder.Property(e => e.Id)
                .ValueGeneratedOnAdd()
                .UseIdentityColumn(1001, 1)
                .HasComment("Mã Cấp độ ứng viên và quyền lợi ứng viên");
            builder.Property(e => e.Id).HasComment("Mã thư mục");
            builder.Property(e => e.Active)
                .IsRequired()
                .HasDefaultValueSql("((1))")
                .HasComment("Đánh dấu bị xóa");
            builder.Property(e => e.CreatedTime)
                .HasDefaultValueSql("(getdate())")
                .HasComment("Ngày tạo");
            builder.Property(e => e.Value)
                .HasMaxLength(255)
                .HasComment("Giá trị");
            builder.Property(e => e.HasBenefit)
                .IsRequired()
                .HasDefaultValueSql("((1))")
                .HasComment("Quyền lợi được hưởng");

            builder.HasOne(d => d.CandidateLevels).WithMany(p => p.CandidateLevelCandidateLevelBenefits)
                .HasForeignKey(d => d.CandidateLevelId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_CandidateLevelCandidateLevelBenefit_CandidateLevel");

            builder.HasOne(d => d.CandidateLevelBenefits).WithMany(p => p.CandidateLevelCandidateLevelBenefits)
                .HasForeignKey(d => d.CandidateLevelBenefitId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_CandidateLevelCandidateLevelBenefit_CandidateLevelBenefit");
        }
    }
}
