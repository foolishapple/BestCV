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
    public class CandidateNotificationConfiguration : IEntityTypeConfiguration<CandidateNotification>
    {
        public void Configure(EntityTypeBuilder<CandidateNotification> builder)
        {
            builder.ToTable("CandidateNotification");
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id)
                .ValueGeneratedOnAdd()
                .UseIdentityColumn(1001, 1);
            builder.Property(e => e.NotificationTypeId).HasComment("Mã loại thông báo");
            builder.Property(e => e.NotificationStatusId).HasComment("Mã trạng thái thông báo ");

            builder.Property(e => e.CandidateId).HasComment("Mã ứng viên");
            builder.Property(e => e.Name)
              .HasMaxLength(255)
              .HasComment("Tên thông báo cho ứng viên");
            builder.Property(e => e.Description)
              .HasMaxLength(500)
              .HasComment("Mô tả");
            builder.Property(e => e.Active)
                 .IsRequired()
                 .HasDefaultValueSql("((1))")
                 .HasComment("Đánh dấu bị xóa");
            builder.Property(e => e.Link)
              .HasMaxLength(500)
              .HasComment("Liên kết ");
            builder.Property(e => e.CreatedTime)
                .HasDefaultValueSql("(getdate())")
                .HasComment("Ngày tạo");
            builder.Property(e => e.Search).HasComment("Lưu ký tự không dấu của các trường muốn tìm kiếm");

            builder.HasOne(d => d.NotificationType).WithMany(p => p.CandidateNotifications)
              .HasForeignKey(d => d.NotificationTypeId)
              .OnDelete(DeleteBehavior.ClientSetNull)
              .HasConstraintName("FK_CandidateNotification_NotificationType");

            builder.HasOne(d => d.NotificationStatus).WithMany(p => p.CandidateNotifications)
              .HasForeignKey(d => d.NotificationStatusId)
              .OnDelete(DeleteBehavior.ClientSetNull)
              .HasConstraintName("FK_CandidateNotification_NotificationStatus");

            builder.HasOne(d => d.Candidate).WithMany(p => p.CandidateNotifications)
              .HasForeignKey(d => d.CandidateId)
              .OnDelete(DeleteBehavior.ClientSetNull)
              .HasConstraintName("FK_CandidateNotification_Candidate");
        }
    }
}
