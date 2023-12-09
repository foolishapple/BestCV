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
    public class EmployerActivityLogTypeConfiguration : IEntityTypeConfiguration<EmployerActivityLogType>
    {
        public void Configure(EntityTypeBuilder<EmployerActivityLogType> builder)
        {
            builder.ToTable("EmployerActivityLogType");

            builder.HasKey(t => t.Id);

            builder.Property(e => e.Id)
                .HasComment("Mã loại lịch sử hoạt động của nhà tuyển dụng")
                .ValueGeneratedOnAdd()
                .UseIdentityColumn(1001, 1);
            builder.Property(e => e.Active)
                .IsRequired()
                .HasDefaultValueSql("((1))")
                .HasComment("Đánh dấu bị xóa");
            builder.Property(e => e.CreatedTime)
                .HasDefaultValueSql("(getdate())")
                .HasComment("Ngày tạo");
            builder.Property(e => e.Description).HasComment("Mô tả");
            builder.Property(e => e.Name)
                .HasMaxLength(255)
                .HasComment("Tên loại lịch sử hoạt động của nhà tuyển dụng");
        }
    }
}
