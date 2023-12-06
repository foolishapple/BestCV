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
    public class CandidateOrdersConfiguration : IEntityTypeConfiguration<CandidateOrders>
    {
        public void Configure(EntityTypeBuilder<CandidateOrders> builder)
        {
            builder.ToTable("CandidateOrders");
            builder.HasKey(x => x.Id);
            builder.Property(e => e.Id)
                .ValueGeneratedOnAdd()
                .UseIdentityColumn(1001, 1)
                .HasComment("Mã đơn đặt hàng của ứng viên");
            builder.Property(e => e.Active)
            .IsRequired()
            .HasDefaultValueSql("((1))")
            .HasComment("Đánh dấu bị xóa");
            builder.Property(e => e.CreatedTime)
                .HasDefaultValueSql("(getdate())")
                .HasComment("Ngày tạo");
            builder.Property(e => e.Price).HasComment("Giá");
            builder.Property(e => e.DiscountPrice).HasComment("Giảm Giá");
            builder.Property(e => e.DiscountCounpon).HasComment("Phiếu giảm Giá");
            builder.Property(e => e.FinalPrice).HasComment("Giá niêm yết");
            builder.Property(e => e.TransactionCode)
                .HasMaxLength(255)
                .HasComment("Mã giao dịch");
            builder.Property(e => e.Info)             
                .HasComment("Thông tin giao dịch");

            builder.HasOne(d => d.Candidate).WithMany(p => p.CandidateOrderses)
               .HasForeignKey(d => d.CandidateId)
               .OnDelete(DeleteBehavior.ClientSetNull)
               .HasConstraintName("FK_CandidateOrders_Candidate");

            builder.HasOne(d => d.OrderStatus).WithMany(p => p.CandidateOrderses)
               .HasForeignKey(d => d.OrderStatusId)
               .OnDelete(DeleteBehavior.ClientSetNull)
               .HasConstraintName("FK_CandidateOrders_OrderStatus");

            builder.HasOne(d => d.PaymentMethod).WithMany(p => p.CandidateOrderses)
               .HasForeignKey(d => d.PaymentMethodId)
               .OnDelete(DeleteBehavior.ClientSetNull)
               .HasConstraintName("FK_CandidateOrders_PaymentMethod");
        }
    }
}
