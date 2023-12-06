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
    public class SlideConfiguration : IEntityTypeConfiguration<Slide>
    {
        public void Configure(EntityTypeBuilder<Slide> builder)
        {
            builder.ToTable("Slide");

            builder.HasKey(t => t.Id);

            builder.Property(e => e.Id)
                .HasComment("Mã slide")
                .ValueGeneratedOnAdd()
                .UseIdentityColumn(1001, 1);
            builder.Property(e => e.Active)
                .IsRequired()
                .HasDefaultValueSql("((1))")
                .HasComment("Đánh dấu bị xóa");
            builder.Property(e => e.CreatedTime)
                .HasDefaultValueSql("(getdate())")
                .HasComment("Ngày tạo");
            builder.Property(e => e.Description)
                .HasComment("Mô tả")
                .HasMaxLength(500);
            builder.Property(e => e.Image)
                .HasComment("Đường dẫn ảnh")
                .HasMaxLength(500);
            builder.Property(e => e.CandidateOrderSort)
               .HasComment("Thứ tự sắp xếp ở màn hình ứng viên");
            builder.Property(e => e.SubOrderSort)
               .HasComment("Thứ tự sắp xếp phụ giữa các order sort cùng bậc");
            builder.Property(e => e.Name)
                .HasMaxLength(255)
                .HasComment("Tên slide");
        }
    }
}
