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
    public class CandidateSkillConfiguration : IEntityTypeConfiguration<CandidateSkill>
    {
        public void Configure(EntityTypeBuilder<CandidateSkill> builder)
        {
            builder.ToTable("CandidateSkill");
            builder.HasKey(x => x.Id);
            builder.Property(e => e.Id)
                .ValueGeneratedOnAdd()
                .UseIdentityColumn(1001, 1)
                .HasComment("Mã kỹ năng ứng viên");
            builder.Property(e => e.CreatedTime)
                .HasDefaultValueSql("(getdate())")
                .HasComment("Ngày tạo");
            builder.Property(e => e.Active)
                .IsRequired()
                .HasDefaultValueSql("((1))")
                .HasComment("Đánh dấu bị xóa");
            builder.Property(e => e.Name).HasMaxLength(500);
            builder.Property(e => e.Description).HasMaxLength(500);
            builder.HasOne(e => e.SkillLevel).WithMany(x => x.CandidateSkills)
                .HasForeignKey(e => e.SkillLevelId)
                .OnDelete(DeleteBehavior.ClientSetNull);
            builder.HasOne(e => e.Skill).WithMany(x => x.CandidateSkills)
                .HasForeignKey(e => e.SkillId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_CandidateSkill_Skill");
            builder.HasOne(e => e.Candidate).WithMany(x => x.CandidateSkills)
                .HasForeignKey(e => e.CandidateId)
                .OnDelete(DeleteBehavior.ClientSetNull);
        }
    }
}
