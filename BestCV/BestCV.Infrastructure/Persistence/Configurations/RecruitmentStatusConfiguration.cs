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
    public class RecruitmentStatusConfiguration : IEntityTypeConfiguration<RecruitmentStatus>
    {
        public void Configure(EntityTypeBuilder<RecruitmentStatus> builder)
        {
            builder.ToTable("RecruitmentStatus");
            builder.HasKey(x => x.Id);
            builder.Property(e => e.Id)
                .ValueGeneratedOnAdd()
                .UseIdentityColumn(1001, 1)
                .HasComment("Mã trạng thái chiến dịch tuyển dụng");
            builder.Property(e => e.Active)
                .IsRequired()
                .HasDefaultValueSql("((1))")
                .HasComment("Đánh dấu bị xóa");
            builder.Property(e => e.CreatedTime)
                .HasDefaultValueSql("(getdate())")
                .HasComment("Ngày tạo");
            builder.Property(e => e.Name)
                .HasMaxLength(255)
                .HasComment("Tên trạng thái chiến dịch tuyển dụng");
            builder.Property(e => e.Description)
                .HasMaxLength(500)
                .HasComment("Mô tả");

            //builder.HasIndex(e => e.Color).IsUnique();

            builder.Property(e => e.Color)
                .HasMaxLength(12)
                .HasComment("Màu trạng thái chiến dịch tuyển dụng");

        }
    }
}
