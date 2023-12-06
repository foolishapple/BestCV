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
    public class EmployerNotificationConfiguration : IEntityTypeConfiguration<EmployerNotification>
    {
        public void Configure(EntityTypeBuilder<EmployerNotification> builder)
        {
            builder.ToTable("EmployerNotification");
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id)
                .ValueGeneratedOnAdd()
                .UseIdentityColumn(1001, 1);
            builder.Property(e => e.Active)
                .IsRequired()
                .HasDefaultValueSql("((1))")
                .HasComment("Đánh dấu bị xóa");
            builder.Property(e => e.CreatedTime)
                .HasDefaultValueSql("(getdate())")
                .HasComment("Ngày tạo");
            builder.Property(e => e.NotificationStatusId).IsRequired().HasComment("Mã trạng thái thông báo");
            builder.Property(e => e.NotificationTypeId).IsRequired().HasComment("Mã loại thông báo");
            builder.Property(e => e.EmployerId).IsRequired().HasComment("Mã nhà tuyển dụng");
            builder.Property(e => e.Name).HasMaxLength(255).HasComment("Tên thông báo");
            builder.Property(e => e.Description).HasMaxLength(500).HasComment("Mô tả");
            builder.Property(e => e.Link).HasMaxLength(500).HasComment("Đường dẫn");

            builder.HasOne(d => d.NotificationType).WithMany(p => p.EmployerNotifications)
                .HasForeignKey(d => d.NotificationTypeId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_NotificationType_EmployerNotification");

            builder.HasOne(d => d.NotificationStatus).WithMany(p => p.EmployerNotifications)
                .HasForeignKey(d => d.NotificationStatusId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_NotificationStatus_EmployerNotification");

            builder.HasOne(d => d.Employer).WithMany(p => p.EmployerNotifications)
                .HasForeignKey(d => d.EmployerId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Employer_EmployerNotification");
        }
    }
}
