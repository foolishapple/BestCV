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
    public class CandidateSuggestionJobSkillConfiguration : IEntityTypeConfiguration<CandidateSuggestionJobSkill>
    {
        public void Configure(EntityTypeBuilder<CandidateSuggestionJobSkill> builder)
        {
            builder.ToTable("CandidateSuggestionJobSkill");
            builder.HasKey(x => x.Id);
            builder.Property(e => e.Id)
                .ValueGeneratedOnAdd()
                .UseIdentityColumn(1001, 1)
                .HasComment("Mã kỹ năng đề xuất cho ứng viên");
            builder.Property(e => e.Active)
                .IsRequired()
                .HasDefaultValueSql("((1))")
                .HasComment("Đánh dấu bị xóa");
            builder.Property(e => e.CreatedTime)
                .HasDefaultValueSql("(getdate())")
                .HasComment("Ngày tạo");

            builder.HasOne(d => d.Candidate).WithMany(p => p.CandidateSuggestionJobSkills)
               .HasForeignKey(d => d.CandidateId)
               .OnDelete(DeleteBehavior.ClientSetNull)
               .HasConstraintName("FK_CandidateSuggestionJobSkill_Candidate");

            builder.HasOne(d => d.JobSkill).WithMany(p => p.CandidateSuggestionJobSkills)
               .HasForeignKey(d => d.JobSkillId)
               .OnDelete(DeleteBehavior.ClientSetNull)
               .HasConstraintName("FK_CandidateSuggestionJobSkill_JobSkill");
        }
    }
}
