using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using BestCV.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BestCV.Infrastructure.Persistence.Configurations
{
    public class CandidateHonorAndAwardConfiguration : IEntityTypeConfiguration<CandidateHonorAndAward>
    {
        public void Configure(EntityTypeBuilder<CandidateHonorAndAward> builder)
        {
            builder.ToTable("CandidateHonorAndAward");

            builder.HasKey(t => t.Id);

            builder.Property(e => e.Id)
                .HasComment("Mã vinh danh và giải thưởng")
                .ValueGeneratedOnAdd()
                .UseIdentityColumn(1001, 1);
            builder.Property(e => e.Active)
                .IsRequired()
                .HasDefaultValueSql("((1))")
                .HasComment("Đánh dấu bị xóa");
            builder.Property(e => e.CreatedTime)
                .HasDefaultValueSql("(getdate())")
                .HasComment("Ngày tạo");
            builder.Property(e => e.Description).HasMaxLength(500).HasComment("Mô tả");
            builder.Property(e => e.TimePeriod).HasMaxLength(50).HasComment("Khoảng thời gian");
            builder.Property(e => e.Name)
                .HasMaxLength(500)
                .HasComment("Tên vinh danh và giải thưởng");

            builder.HasOne(d => d.Candidate).WithMany(p => p.CandidateHonorAndAwards)
                .HasForeignKey(d => d.CandidateId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_CandidateHonorAndAward_Candidate");
        }
    }
}
