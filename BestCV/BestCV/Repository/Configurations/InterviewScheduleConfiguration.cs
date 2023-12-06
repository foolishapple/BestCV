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
    public class InterviewScheduleConfiguration : IEntityTypeConfiguration<InterviewSchedule>
    {
        public void Configure(EntityTypeBuilder<InterviewSchedule> builder)
        {
            builder.ToTable("InterviewSchedule");
            builder.HasKey(x => x.Id);
            builder.Property(e => e.Id)
                .IsRequired()
                .UseIdentityColumn(1001, 1)
                .HasComment("Mã lịch phỏng vấn");
            builder.Property(e => e.Location)
                .HasMaxLength(500)
                .HasComment("Vị trí");
            builder.Property(e => e.Link)
                .HasMaxLength(500)
                .HasComment("Liên kết lịch phỏng vấn");
            builder.Property(e => e.StateDate).HasComment("Ngày bắt đầu");
            builder.Property(e => e.EndDate).HasComment("Ngày kết thúc");
            builder.Property(e => e.Description)
                .HasMaxLength(500)
                .HasComment("Mô tả");
            builder.Property(e => e.Active)
                .IsRequired()
                .HasDefaultValueSql("((1))")
                .HasComment("Đánh dấu bị xóa");
            builder.Property(e => e.CreatedTime)
                .HasDefaultValueSql("(getdate())")
                .HasComment("Ngày tạo");

            builder.HasOne(d => d.InterviewType).WithMany(p => p.InterviewSchedules)
               .HasForeignKey(d => d.InterviewscheduleTypeId)
               .OnDelete(DeleteBehavior.ClientSetNull)
               .HasConstraintName("FK_InterviewSchedule_InterviewType");

            builder.HasOne(d => d.InterviewStatus).WithMany(p => p.InterviewSchedules)
               .HasForeignKey(d => d.InterviewscheduleStatusId)
               .OnDelete(DeleteBehavior.ClientSetNull)
               .HasConstraintName("FK_InterviewSchedule_InterviewStatus");

            builder.HasOne(d => d.CandidateApplyJob).WithMany(p => p.InterviewSchedules)
              .HasForeignKey(d => d.CandidateApplyJobId)
              .OnDelete(DeleteBehavior.ClientSetNull)
              .HasConstraintName("FK_InterviewSchedule_CandidateApplyJob");
        }
    }
}
