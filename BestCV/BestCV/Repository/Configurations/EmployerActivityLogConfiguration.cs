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
    public class EmployerActivityLogConfiguration : IEntityTypeConfiguration<EmployerActivityLog>
    {
        public void Configure(EntityTypeBuilder<EmployerActivityLog> builder)
        {
            builder.ToTable("EmployerActivityLog");

            builder.Property(e => e.Id)
                .HasComment("Mã lịch sử hoạt động của nhà tuyển dụng")
                .ValueGeneratedOnAdd()
                .UseIdentityColumn(1001, 1);
            builder.Property(c => c.Description)
                .HasComment("Mô tả");
            builder.Property(c => c.OldValue)
                .HasComment("Giá trị cũ");
            builder.Property(c => c.NewValue)
                .HasComment("Giá trị mới");
            builder.Property(c => c.OS)
                .HasComment("Hệ điều hành")
                .HasMaxLength(255);
            builder.Property(c => c.UserAgent)
                .HasComment("Giao thức người dùng")
                .HasMaxLength(255);
            builder.Property(c => c.Browser)
                .HasComment("Trình duyệt")
                .HasMaxLength(255);
            builder.Property(c => c.IpAddress)
                .HasComment("Địa chỉ IP");
            builder.Property(e => e.Active)
                .IsRequired()
                .HasDefaultValueSql("((1))")
                .HasComment("Đánh dấu bị xóa");
            builder.Property(e => e.CreatedTime)
                .HasDefaultValueSql("(getdate())")
                .HasComment("Ngày tạo");
            //Constrair
            builder.HasKey(t => t.Id);
            builder.HasOne(d => d.EmployerActivityLogType).WithMany(p => p.EmployerActivityLogs)
                .HasForeignKey(d => d.EmployerActivityLogTypeId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_EmployerActivityLog_EmployerActivityLogType");
        }
    }
}
