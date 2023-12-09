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
    public class TopAreaJobConfiguration : IEntityTypeConfiguration<TopAreaJob>
    {
        public void Configure(EntityTypeBuilder<TopAreaJob> builder)
        {
            builder.ToTable("TopAreaJob");
            #region Properties
            builder.Property(c => c.Id).UseIdentityColumn(1001, 1).ValueGeneratedOnAdd().HasComment("Mã");
            builder.Property(c => c.Active).HasDefaultValue(true).HasComment("Đánh dấu đã bị xóa");
            builder.Property(c => c.CreatedTime).HasDefaultValueSql("(getdate())").HasComment("Ngày tạo");
            builder.Property(c => c.OrderSort).HasComment("Thứ tự sắp xếp");
            builder.Property(c=>c.SubOrderSort).HasComment("Thứ tự sắp xếp phụ");
            #endregion
            #region Relationship
            builder.HasKey(c => c.Id).HasName("PK_TopAreaJob");
            builder.HasOne(c => c.Job).WithMany(c => c.TopAreaJobs).HasForeignKey(c => c.JobId).HasConstraintName("FK_TopAreaJob_Job");
            #endregion
        }
    }
}
