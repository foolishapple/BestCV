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
    public class EmployerServicePackageEmployerConfiguration : IEntityTypeConfiguration<EmployerServicePackageEmployer>
    {
        public void Configure(EntityTypeBuilder<EmployerServicePackageEmployer> builder)
        {
            builder.ToTable("EmployerServicePackageEmployer");
            builder.Property(c => c.Id).
                UseIdentityColumn(1001, 1)
                .ValueGeneratedOnAdd()
                .HasComment("Mã");
            builder.Property(c => c.CreatedTime).HasDefaultValueSql("(getdate())").HasComment("Ngày tạo");
            builder.Property(c => c.Active).HasDefaultValue(true).HasComment("Đánh dấu đã bị xóa");
            builder.Property(c => c.EmployerOrderDetailId).HasComment("Mã chi tiết đơn hàng");

            builder.HasKey(c => c.Id);
            builder.HasOne(c => c.EmployerOrderDetail).WithMany(c => c.EmployerServicePackageEmployers).HasForeignKey(c => c.EmployerOrderDetailId).OnDelete(DeleteBehavior.ClientSetNull).HasConstraintName("FK_EmployerServicePackageEmployer_EmployerOrderDetail");
        }
    }
}
