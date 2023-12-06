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
    public class EmployerOrderConfiguration : IEntityTypeConfiguration<EmployerOrder>
    {
        public void Configure(EntityTypeBuilder<EmployerOrder> builder)
        {
            builder.ToTable("EmployerOrders");
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
            builder.Property(e => e.OrderStatusId)
               .IsRequired()
               .HasComment("Mã trạng thái đơn hàng");
            builder.Property(e => e.PaymentMethodId)
              .IsRequired()
              .HasComment("Mã loại thanh toán");
            builder.Property(e => e.EmployerId)
              .IsRequired()
              .HasComment("Mã nhà tuyển dụng");
            builder.Property(e => e.Price)
              .HasComment("Giá tiền");
            builder.Property(e => e.DiscountPrice)
              .HasComment("Giá khuyến mãi");
            builder.Property(e => e.DiscountVoucher)
              .HasComment("Phiếu giảm giá");
            builder.Property(e => e.FinalPrice)
              .HasComment("Giá cuối");
            builder.Property(e => e.TransactionCode)
                .HasMaxLength(255)
             .HasComment("Mã giao dịch");
            builder.Property(e => e.Info)
             .HasComment("Thông tin giao dịch");
            builder.Property(e => e.RequestId)
                .HasMaxLength(255)
            .HasComment("Mã yêu cầu");
            builder.Property(e => e.Search)
               .HasComment("Search tổng");
            builder.Property(e => e.ApplyEndDate)
                .HasComment("Thời hạn đơn hàng");
            builder.Property(e => e.IsApproved)
                .IsRequired()
                .HasDefaultValueSql("((0))")
                .HasComment("Đơn hàng có được duyệt hay không");
            builder.Property(e => e.ApprovalDate)
                .HasComment("Thời diểm duyệt tin");

            builder.HasOne(x => x.EmployerOrderStatus)
                .WithMany(x => x.EmployerOrders)
                .HasForeignKey(x => x.OrderStatusId);

            builder.HasOne(x => x.PaymentMethod)
                .WithMany(x => x.EmployerOrders)
                .HasForeignKey(x => x.PaymentMethodId);
            builder.HasOne(x => x.Employer)
                .WithMany(x => x.EmployerOrders)
                .HasForeignKey(x => x.EmployerId);
        }
    }
}
