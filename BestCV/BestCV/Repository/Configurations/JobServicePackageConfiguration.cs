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
    public class JobServicePackageConfiguration : IEntityTypeConfiguration<JobServicePackage>
    {
        public void Configure(EntityTypeBuilder<JobServicePackage> builder)
        {
            builder.ToTable("JobServicePackage");
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
            builder.Property(e => e.Value).HasComment("Giá trị");
            builder.Property(e => e.ExpireTime).HasComment("Hạn sử dụng");
            builder.Property(c => c.Quantity).HasDefaultValue(1).HasComment("Số lượng");
            builder.HasKey(c => c.Id);
            builder.HasOne(e => e.Job)
                .WithMany(c => c.JobServicePackages)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasForeignKey(c => c.JobId)
                .HasConstraintName("FK_JobServicePackage_Job");
            builder.HasOne(e => e.EmployerServicePackage)
                .WithMany(c => c.JobServicePackages)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasForeignKey(c => c.EmployerServicePackageId)
                .HasConstraintName("FK_JobServicePackage_EmployerServicePackage");
            builder.HasIndex(c => new { c.JobId, c.EmployerServicePackageId })
                .IsUnique();
        }
    }
}
