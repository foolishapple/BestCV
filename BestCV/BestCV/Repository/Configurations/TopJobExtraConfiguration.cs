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
    public class TopJobExtraConfiguration : IEntityTypeConfiguration<TopJobExtra>
    {
        public void Configure(EntityTypeBuilder<TopJobExtra> builder)
        {
            builder.ToTable("TopJobExtra");
            builder.Property(c => c.Id).ValueGeneratedOnAdd()
                .UseIdentityColumn(1001, 1)
                .HasComment("Mã");
            builder.Property(e => e.Active)
                .IsRequired()
                .HasDefaultValueSql("((1))")
                .HasComment("Đánh dấu bị xóa");
            builder.Property(e => e.CreatedTime)
                .HasDefaultValueSql("(getdate())")
                .HasComment("Ngày tạo");
            builder.Property(e => e.Description).HasMaxLength(500).HasComment("Mô tả");
            builder.Property(c => c.OrderSort)
                .HasComment("Thứ tự sắp xếp");
            builder.Property(e => e.SubOrderSort)
               .HasComment("Thứ tự sắp xếp phụ giữa các order sort cùng bậc");
            builder.HasKey(c => c.Id);
            builder.HasOne(c => c.Job)
                .WithMany(c => c.TopJobExtras)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasForeignKey(c => c.JobId)
                .HasConstraintName("FK_TopJobExtra_Job");
            builder.HasIndex(c => c.JobId)
                .IsUnique();
            //builder.HasIndex(c => new { c.OrderSort, c.SubOrderSort })
            //   .IsUnique();
        }
    }
}
