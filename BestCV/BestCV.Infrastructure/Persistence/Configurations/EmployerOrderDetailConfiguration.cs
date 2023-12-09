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
    public class EmployerOrderDetailConfiguration : IEntityTypeConfiguration<EmployerOrderDetail>
    {
        public void Configure(EntityTypeBuilder<EmployerOrderDetail> builder)
        {
            builder.ToTable("EmployerOrderDetail");
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
            builder.Property(e => e.OrderId).IsRequired().HasComment("Mã đơn hàng");
            builder.Property(e => e.EmployerServicePackageId).IsRequired().HasComment("Mã gói dịch vụ nhà tuyển dụng");
            builder.Property(e => e.Quantity).IsRequired().HasComment("Số lượng");
            builder.Property(e => e.Price).IsRequired().HasComment("Giá tiền");
            builder.Property(e => e.DiscountPrice).IsRequired().HasComment("Giảm giá");
            builder.Property(e => e.FinalPrice).IsRequired().HasComment("Giá tiền thanh toán");
            builder.Property(e => e.Search).IsRequired().HasComment("Search tổng");

            builder.HasOne(d => d.EmployerServicePackage).WithMany(p => p.EmployerOrderDetails)
                .HasForeignKey(d => d.EmployerServicePackageId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_EmployerServicePackage_EmployerOrderDetail");

            builder.HasOne(d => d.EmployerOrder).WithMany(p => p.EmployerOrderDetails)
                .HasForeignKey(d => d.OrderId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_EmployerOrder_EmployerOrderDetail");
        }
    }
}
