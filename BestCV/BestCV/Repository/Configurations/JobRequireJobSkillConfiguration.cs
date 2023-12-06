using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Jobi.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Jobi.Infrastructure.Persistence.Configurations
{
    public class JobRequireJobSkillConfiguration : IEntityTypeConfiguration<JobRequireJobSkill>
    {
        public void Configure(EntityTypeBuilder<JobRequireJobSkill> builder)
        {
            builder.ToTable("JobRequireJobSkill");
            builder.HasKey(e => e.Id);

            builder.Property(e => e.Id)
                .ValueGeneratedOnAdd()
                .UseIdentityColumn(1001, 1);

            builder.Property(e => e.JobId)
                .IsRequired()
                .HasComment("Mã tin tuyển dụng");

            builder.Property(e => e.JobSkillId)
                .IsRequired()
                .HasComment("Mã kĩ năng yêu cầu");

            builder.Property(e => e.Active)
                .IsRequired()
                .HasDefaultValueSql("((1))")
                .HasComment("Đánh dấu bị xóa");

            builder.Property(e => e.CreatedTime)
                .HasDefaultValueSql("(getdate())")
                .HasComment("Ngày tạo");
            builder.HasOne(d => d.Job).WithMany(p => p.JobRequireJobSkills)
             .HasForeignKey(d => d.JobId)
             .OnDelete(DeleteBehavior.ClientSetNull)
             .HasConstraintName("FK_JobRequireJobSkill_Job");
            builder.HasOne(d => d.JobSkill).WithMany(p => p.JobRequireJobSkills)
            .HasForeignKey(d => d.JobSkillId)
            .OnDelete(DeleteBehavior.ClientSetNull)
            .HasConstraintName("FK_JobRequireJobSkill_JobSkill");
        }
    }
}
