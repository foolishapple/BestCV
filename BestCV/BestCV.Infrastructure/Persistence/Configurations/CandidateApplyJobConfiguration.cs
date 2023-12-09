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
    public class CandidateApplyJobConfiguration : IEntityTypeConfiguration<CandidateApplyJob>
    {
        public void Configure(EntityTypeBuilder<CandidateApplyJob> builder)
        {
            builder.ToTable("CandidateApplyJob");

            builder.HasKey(t => t.Id);

            builder.Property(e => e.Id)
                .HasComment("Mã ứng viên ứng tuyển việc làm")
                .ValueGeneratedOnAdd()
                .UseIdentityColumn(1001, 1);
            builder.Property(e => e.Active)
                .IsRequired()
                .HasDefaultValueSql("((1))")
                .HasComment("Đánh dấu bị xóa");
            builder.Property(e => e.CreatedTime)
                .HasDefaultValueSql("(getdate())")
                .HasComment("Ngày tạo");
            builder.Property(e => e.IsEmployerViewed).HasComment("Nhà tuyển dụng đã xem");
            builder.Property(c => c.Description).HasMaxLength(500).HasComment("Mô tả");
            builder.HasOne(d => d.CandidateCVPDF).WithMany(p => p.CandidateApplyJobs)
               .HasForeignKey(d => d.CandidateCVPDFId)
               .OnDelete(DeleteBehavior.ClientSetNull)
               .HasConstraintName("FK_CandidateApplyJobs_CandidateCVPDF");

            builder.HasOne(d => d.Job).WithMany(p => p.CandidateApplyJobs)
               .HasForeignKey(d => d.JobId)
               .OnDelete(DeleteBehavior.ClientSetNull)
               .HasConstraintName("FK_CandidateApplyJobs_Job");

            builder.HasOne(d => d.CandidateApplyJobStatus).WithMany(p => p.CandidateApplyJobs)
               .HasForeignKey(d => d.CandidateApplyJobStatusId)
               .OnDelete(DeleteBehavior.ClientSetNull)
               .HasConstraintName("FK_CandidateApplyJobs_CandidateApplyJobStatus");

            builder.HasOne(d => d.CandidateApplyJobSource).WithMany(p => p.CandidateApplyJobs)
               .HasForeignKey(d => d.CandidateApplyJobSourceId)
               .OnDelete(DeleteBehavior.ClientSetNull)
               .HasConstraintName("FK_CandidateApplyJobs_CandidateApplyJobSource");
        }
    }
}
