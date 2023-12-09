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
    public class EmployerOrderVoucherConfiguration : IEntityTypeConfiguration<EmployerOrderVoucher>
    {
        public void Configure(EntityTypeBuilder<EmployerOrderVoucher> builder)
        {
            builder.ToTable("EmployerOrderVoucher");
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
            builder.Property(e => e.EmployerOrderId).IsRequired().HasComment("Mã đơn hàng nhà tuyển dụng");
            builder.Property(e => e.VoucherId).IsRequired().HasComment("Mã khuyến mãi");

            builder.HasOne(d => d.EmployerOrder).WithMany(p => p.EmployerOrderVouchers)
                .HasForeignKey(d => d.EmployerOrderId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_EmployerOrder_EmployerOrderVoucher");

            builder.HasOne(d => d.EmployerVoucher).WithMany(p => p.EmployerOrderVouchers)
                .HasForeignKey(d => d.VoucherId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_EmployerVoucher_EmployerOrderVoucher");
        }
    }
}
