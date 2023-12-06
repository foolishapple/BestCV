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
    public class JobConfiguration : IEntityTypeConfiguration<Job>
    {
        public void Configure(EntityTypeBuilder<Job> builder)
        {
            builder.ToTable("Job");
            builder.HasKey(e => e.Id);
            builder.Property(e => e.Id)
                .ValueGeneratedOnAdd()
                .UseIdentityColumn(1001, 1);

            builder.Property(e => e.RecruimentCampaignId)
                .IsRequired()
                .HasComment("Mã chiến dịch tuyển dụng");

            builder.Property(e => e.JobStatusId)
                .IsRequired()
                .HasComment("Mã trạng thái tin tuyển dụng");

            builder.Property(e => e.Name)
                .HasMaxLength(255)
                .HasComment("Title tin tuyển dụng");

            builder.Property(e => e.TotalRecruitment)
                .HasComment("Số lượng cần tuyển");

            builder.Property(e => e.GenderRequirement)
                .HasComment("Giới tính yêu cầu");

            builder.Property(e => e.JobTypeId)
                .IsRequired()
                .HasComment("Mã loại tin tuyển dụng");

            builder.Property(e => e.PrimaryJobCategoryId)
                .IsRequired()
                .HasComment("Mã vị trí ngành nghề tuyển dụng chính");
            builder.Property(e => e.JobPositionId)
                .IsRequired()
                .HasComment("Mã vị trí tuyển dụng");

            builder.Property(e => e.IsApproved)
                .IsRequired()
                .HasDefaultValueSql("((0))")
                .HasComment("Tin tuyển dụng có được duyệt hay không");

            builder.Property(e => e.ExperienceRangeId)
                .IsRequired()
                .HasComment("Mã khoảng kinh nghiệm");

            builder.Property(e => e.Currency)
                .IsRequired()
                .HasComment("Loại tiền tệ");

            builder.Property(e => e.SalaryTypeId)
                .IsRequired()
                .HasComment("Mã loại lương");

            builder.Property(e => e.SalaryFrom)
                .HasComment("Lương tối thiểu");

            builder.Property(e => e.SalaryTo)
                .HasComment("Lương tối đa");

            builder.Property(e => e.Overview)
                .HasComment("Tổng quan công việc");

            builder.Property(e => e.Requirement)
                .IsRequired()
                .HasComment("Yêu cầu công việc");

            builder.Property(e => e.Benefit)
                .IsRequired()
                .HasComment("Quyền lợi công việc");

            builder.Property(e => e.ReceiverName)
                .HasMaxLength(255)
                .HasComment("Tên người nhận");

            builder.Property(e => e.ReceiverPhone)
                .HasMaxLength(10)
                .HasComment("Điện thoại người nhận");

            builder.Property(e => e.ApplyEndDate)
                .HasComment("Thời hạn ứng tuyển");

            builder.Property(e => e.ApprovalDate)
                .HasComment("Thời diểm duyệt tin");

            builder.Property(e => e.Active)
                .IsRequired()
                .HasDefaultValueSql("((1))")
                .HasComment("Đánh dấu bị xóa");

            builder.Property(e => e.CreatedTime)
                .HasDefaultValueSql("(getdate())")
                .HasComment("Ngày tạo");

            builder.Property(e => e.Search)
                .IsRequired()
                .HasComment("Trường tìm kiếm");
            builder.Property(c => c.ViewCount)
                .HasDefaultValue(0)
                .HasComment("Tổng lượt xem");
            builder.Property(c => c.RefreshDate).HasDefaultValueSql("(getdate())").HasComment("Ngày làm mới");
            //Relationship
           
            builder.HasOne(d => d.RecruitmentCampaign).WithMany(p => p.Jobs)
               .HasForeignKey(d => d.RecruimentCampaignId)
               .OnDelete(DeleteBehavior.ClientSetNull)
               .HasConstraintName("FK_Job_RecruitmentCampaign");
            builder.HasOne(d => d.JobStatus).WithMany(p => p.Jobs)
               .HasForeignKey(d => d.JobStatusId)
               .OnDelete(DeleteBehavior.ClientSetNull)
               .HasConstraintName("FK_Job_JobStatus");
            builder.HasOne(d => d.JobType).WithMany(p => p.Jobs)
                .HasForeignKey(d => d.JobTypeId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Job_JobType");
            builder.HasOne(d => d.ExperienceRange).WithMany(p => p.Jobs)
               .HasForeignKey(d => d.ExperienceRangeId)
               .OnDelete(DeleteBehavior.ClientSetNull)
               .HasConstraintName("FK_Job_ExperienceRange");
            builder.HasOne(d => d.SalaryType).WithMany(p => p.Jobs)
               .HasForeignKey(d => d.SalaryTypeId)
               .OnDelete(DeleteBehavior.ClientSetNull)
               .HasConstraintName("FK_Job_SalaryType");
            builder.HasOne(d => d.PrimaryJobCategory).WithMany(p => p.Jobs)
              .HasForeignKey(d => d.PrimaryJobCategoryId)
              .OnDelete(DeleteBehavior.ClientSetNull)
              .HasConstraintName("FK_Job_PrimaryJobCategory");
             builder.HasOne(x => x.JobPosition)
                .WithMany(x => x.Jobs)
                .HasForeignKey(x => x.JobPositionId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Job_JobPosition");
        }
    }
}
