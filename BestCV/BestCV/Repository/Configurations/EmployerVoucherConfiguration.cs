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
    public class EmployerVoucherConfiguration : IEntityTypeConfiguration<EmployerVoucher>
    {
        public void Configure(EntityTypeBuilder<EmployerVoucher> builder)
        {
            builder.ToTable("EmployerVoucher");
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id)
                .ValueGeneratedOnAdd()
                .UseIdentityColumn(1001, 1);
            builder.Property(e => e.EmployerId)
                .IsRequired()
                .HasComment("Mã nhà tuyển dụng");
            builder.Property(e => e.VoucherId)
                .IsRequired()
                .HasComment("Mã voucher");
            builder.Property(e => e.Active)
                .IsRequired()
                .HasDefaultValueSql("((1))")
                .HasComment("Đánh dấu bị xóa");
            builder.Property(e => e.CreatedTime)
                .HasDefaultValueSql("(getdate())")
                .HasComment("Ngày tạo");

            //builder.HasOne(d => d.Employer).WithMany(p => p.EmployerVouchers)
            //    .HasForeignKey(d => d.EmployerId)
            //    .OnDelete(DeleteBehavior.ClientSetNull)
            //    .HasConstraintName("FK_Employer_EmployerVoucher");

            builder.HasOne(d => d.Voucher).WithMany(p => p.EmployerVouchers)
                .HasForeignKey(d => d.VoucherId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Voucher_EmployerVoucher");
        }
    }
}
