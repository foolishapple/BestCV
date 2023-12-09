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
    public class TopFeatureJobConfiguration : IEntityTypeConfiguration<TopFeatureJob>
    {

        public void Configure(EntityTypeBuilder<TopFeatureJob> builder)
        {
            builder.ToTable("TopFeatureJob");
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
            builder.Property(e => e.JobId).IsRequired().HasComment("Mã việc làm");
            builder.Property(e => e.OrderSort).IsRequired().HasComment("Sắp xếp");
            builder.Property(e => e.SubOrderSort)
               .HasComment("Thứ tự sắp xếp phụ giữa các order sort cùng bậc");
            builder.HasOne(d => d.Job).WithMany(p => p.TopFeatureJobs)
              .HasForeignKey(d => d.JobId)
              .OnDelete(DeleteBehavior.ClientSetNull)
              .HasConstraintName("FK_TopFeatureJob_Job");
            //builder.HasIndex(c => new { c.OrderSort, c.SubOrderSort })
            //    .IsUnique();
        }
    }
}
