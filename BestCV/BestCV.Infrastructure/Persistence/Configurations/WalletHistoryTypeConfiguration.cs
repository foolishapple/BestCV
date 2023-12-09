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
    public class WalletHistoryTypeConfiguration : IEntityTypeConfiguration<WalletHistoryType>
    {
        public void Configure(EntityTypeBuilder<WalletHistoryType> builder)
        {
            //Properties
            builder.ToTable("WalletHistoryType");
            builder.Property(c => c.Id).UseIdentityColumn(1001, 1).ValueGeneratedOnAdd().HasComment("Mã");
            builder.Property(c => c.Active).HasDefaultValue(true).HasComment("Đánh dấu bị xóa");
            builder.Property(c => c.CreatedTime).HasDefaultValueSql("(getdate())").HasComment("Ngày tạo");
            builder.Property(c => c.Color).HasMaxLength(50).HasComment("Màu");
            builder.Property(c => c.Name).HasMaxLength(255).HasComment("Tên");
            builder.Property(c => c.Description).HasMaxLength(500).HasComment("Mô tả");
            //Relationship
            builder.HasKey(c => c.Id).HasName("PK_WalletHistoryType");
        }
    }
}
