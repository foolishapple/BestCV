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
    public class TopJobUrgentConfiguration : IEntityTypeConfiguration<TopJobUrgent>
    {
        public void Configure(EntityTypeBuilder<TopJobUrgent> builder)
        {
            builder.ToTable("TopJobUrgent");
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
                .WithMany(c => c.TopJobUrgents)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasForeignKey(c => c.JobId)
                .HasConstraintName("FK_TopJobUrgent_Job");
            builder.HasIndex(c => c.JobId)
                .IsUnique();
            //builder.HasIndex(c => new { c.OrderSort, c.SubOrderSort })
            //   .IsUnique();
        }
    }
}
