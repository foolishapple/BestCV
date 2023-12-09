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
    public class EmployerWalletConfiguration : IEntityTypeConfiguration<EmployerWallet>
    {
        public void Configure(EntityTypeBuilder<EmployerWallet> builder)
        {
            builder.ToTable("EmployerWallet");
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
            builder.Property(e => e.Value).HasComment("Số dư");
            builder.HasKey(c => c.Id);
            builder.HasOne(c => c.Employer)
                .WithMany(c => c.EmployerWallets)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasForeignKey(c => c.EmployerId)
                .HasConstraintName("FK_EmployerWallet_Employer");
            builder.HasOne(c => c.WalletType)
                .WithMany(c => c.EmployerWallets)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasForeignKey(c => c.WalletTypeId)
                .HasConstraintName("FK_EmployerWallet_WalletType");
            builder.HasIndex(c => new {c.EmployerId,c.WalletTypeId})
                .IsUnique();
        }
    }
}
