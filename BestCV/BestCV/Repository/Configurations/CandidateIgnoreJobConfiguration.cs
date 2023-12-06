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
    public class CandidateIgnoreJobConfiguration : IEntityTypeConfiguration<CandidateIgnoreJob>
    {
        public void Configure(EntityTypeBuilder<CandidateIgnoreJob> builder)
        {
            builder.ToTable("CandidateIgnoreJob");

            builder.HasKey(t => t.Id);

            builder.Property(e => e.Id)
                .HasComment("Mã công việc ứng viên bỏ qua")
                .ValueGeneratedOnAdd()
                .UseIdentityColumn(1001, 1);
            builder.Property(e => e.Active)
                .IsRequired()
                .HasDefaultValueSql("((1))")
                .HasComment("Đánh dấu bị xóa");
            builder.Property(e => e.CreatedTime)
                .HasDefaultValueSql("(getdate())")
                .HasComment("Ngày tạo");

            builder.HasOne(d => d.Candidate).WithMany(p => p.CandidateIgnoreJobs)
               .HasForeignKey(d => d.CandidateId)
               .OnDelete(DeleteBehavior.ClientSetNull)
               .HasConstraintName("FK_CandidateIgnoreJob_Candidate");

            builder.HasOne(d => d.Job).WithMany(p => p.CandidateIgnoreJobs)
               .HasForeignKey(d => d.JobId)
               .OnDelete(DeleteBehavior.ClientSetNull)
               .HasConstraintName("FK_CandidateIgnoreJob_Job");
        }
    }
}
