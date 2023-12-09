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
    public class JobReasonApplyConfiguration : IEntityTypeConfiguration<JobReasonApply>
    {
        public void Configure(EntityTypeBuilder<JobReasonApply> builder)
        {
            builder.ToTable("JobReasonApply");
            builder.HasKey(e => e.Id);

            builder.Property(e => e.Id)
                .ValueGeneratedOnAdd()
                .UseIdentityColumn(1001, 1);

            builder.Property(e => e.JobId)
                .IsRequired()
                .HasComment("Mã tin tuyển dụng");

            builder.Property(e => e.Name)
                .IsRequired()
                .HasComment("Lý do nên ứng tuyển");

            builder.Property(e => e.Description)
                .HasComment("Chi tiết lý do nên ứng tuyển");

            builder.Property(e => e.Active)
                .IsRequired()
                .HasDefaultValueSql("((1))")
                .HasComment("Đánh dấu bị xóa");

            builder.Property(e => e.CreatedTime)
                .HasDefaultValueSql("(getdate())")
                .HasComment("Ngày tạo");
            builder.HasOne(d => d.Job).WithMany(p => p.JobReasonApplies)
             .HasForeignKey(d => d.JobId)
             .OnDelete(DeleteBehavior.ClientSetNull)
             .HasConstraintName("FK_JobReasonApply_Job");
        }
    }
}
