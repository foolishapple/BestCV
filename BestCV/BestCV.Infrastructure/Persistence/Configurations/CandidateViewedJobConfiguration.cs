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
    public class CandidateViewedJobConfiguration : IEntityTypeConfiguration<CandidateViewedJob>
    {
        public void Configure(EntityTypeBuilder<CandidateViewedJob> builder)
        {
            builder.ToTable("CandidateViewedJob");
            builder.HasKey(x => x.Id);
            builder.Property(e => e.Id)
                .ValueGeneratedOnAdd()
                .UseIdentityColumn(1001, 1)
                .HasComment("Mã ứng viên đã xem công việc");
            builder.Property(e => e.Active)
               .IsRequired()
               .HasDefaultValueSql("((1))")
               .HasComment("Đánh dấu bị xóa");
            builder.Property(e => e.CreatedTime)
                .HasDefaultValueSql("(getdate())")
                .HasComment("Ngày tạo");

            builder.HasOne(d => d.Candidate).WithMany(p => p.CandidateViewedJobs)
               .HasForeignKey(d => d.CandidateId)
               .OnDelete(DeleteBehavior.ClientSetNull)
               .HasConstraintName("FK_CandidateViewedJob_Candidate");
            builder.HasOne(d => d.Job).WithMany(p => p.CandidateViewedJobs)
              .HasForeignKey(d => d.JobId)
              .OnDelete(DeleteBehavior.ClientSetNull)
              .HasConstraintName("FK_CandidateViewedJob_Job");
        }
    }
}
