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
    public class RefreshJobConfiguration : IEntityTypeConfiguration<RefreshJob>
    {
        public void Configure(EntityTypeBuilder<RefreshJob> builder)
        {
            #region Properties
            builder.ToTable("RefreshJob");
            builder.Property(c => c.Id).UseIdentityColumn(1001, 1).ValueGeneratedOnAdd().HasComment("Mã");
            builder.Property(c => c.Active).HasDefaultValue(true).HasComment("Đánh dấu bị xóa");
            builder.Property(c => c.CreatedTime).HasDefaultValueSql("(getdate())").HasComment("Ngày tạo");
            builder.Property(c => c.Description).HasMaxLength(500).HasComment("Mô tả");
            builder.Property(c => c.RefreshDate).HasDefaultValueSql("(getdate())").HasComment("Ngày làm mới");
            builder.Property(c => c.JobId).HasComment("Mã tin tuyển dụng");
            #endregion
            #region Relationship
            builder.HasKey(c => c.Id).HasName("PK_RefreshJob");
            builder.HasOne(c => c.Job).WithMany(c => c.RefreshJobs).HasForeignKey(c => c.JobId).OnDelete(DeleteBehavior.ClientSetNull).HasConstraintName("FK_RefreshJob_Job");
            #endregion
        }
    }
}
