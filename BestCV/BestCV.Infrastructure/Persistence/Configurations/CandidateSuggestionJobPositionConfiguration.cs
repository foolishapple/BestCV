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
    public class CandidateSuggestionJobPositionConfiguration : IEntityTypeConfiguration<CandidateSuggestionJobPosition>
    {
        public void Configure(EntityTypeBuilder<CandidateSuggestionJobPosition> builder)
        {
            builder.ToTable("CandidateSuggestionJobPosition");
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id)
                .ValueGeneratedOnAdd()
                .UseIdentityColumn(1001, 1);

            builder.Property(e => e.CandidateId).HasComment("Mã ứng viên");
            builder.Property(e => e.JobPositionId).HasComment("Mã vị trí công việc");
            builder.Property(e => e.Active)
                 .IsRequired()
                 .HasDefaultValueSql("((1))")
                 .HasComment("Đánh dấu bị xóa");

            builder.Property(e => e.CreatedTime)
                .HasDefaultValueSql("(getdate())")
                .HasComment("Ngày tạo");

            builder.HasOne(d => d.Candidate).WithMany(p => p.CandidateSuggestionJobPositions)
                .HasForeignKey(d => d.CandidateId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_CandidateSuggestionJobPosition_Candidate");

            builder.HasOne(d => d.JobPosition).WithMany(p => p.CandidateSuggestionJobPositions)
               .HasForeignKey(d => d.JobPositionId)
               .OnDelete(DeleteBehavior.ClientSetNull)
               .HasConstraintName("FK_CandidateSuggestionJobPosition_JobPosition");
        }
    }
}
