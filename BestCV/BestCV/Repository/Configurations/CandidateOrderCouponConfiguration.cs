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
    public class CandidateOrderCouponConfiguration : IEntityTypeConfiguration<CandidateOrderCoupon>
    {
        public void Configure(EntityTypeBuilder<CandidateOrderCoupon> builder)
        {
            builder.ToTable("CandidateOrderCoupon");
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id)
                .ValueGeneratedOnAdd()
                .UseIdentityColumn(1001, 1);

            builder.Property(e => e.CandidateOrderId).HasComment("Mã đơn hàng ứng viên");
            builder.Property(e => e.CouponId).HasComment("Mã phiếu giảm giá");
            builder.Property(e => e.Active)
                 .IsRequired()
                 .HasDefaultValueSql("((1))")
                 .HasComment("Đánh dấu bị xóa");

            builder.Property(e => e.CreatedTime)
                .HasDefaultValueSql("(getdate())")
                .HasComment("Ngày tạo");

            builder.HasOne(d => d.CandidateOrders).WithMany(p => p.CandidateOrderCoupons)
               .HasForeignKey(d => d.CandidateOrderId)
               .OnDelete(DeleteBehavior.ClientSetNull)
               .HasConstraintName("FK_CandidateOrderCoupon_CandidateOrders");

            builder.HasOne(d => d.Coupon).WithMany(p => p.CandidateOrderCoupons)
               .HasForeignKey(d => d.CouponId)
               .OnDelete(DeleteBehavior.ClientSetNull)
               .HasConstraintName("FK_CandidateOrderCoupon_Coupon");
        }
    }
}
