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
    public class CandidateLevelConfiguration : IEntityTypeConfiguration<CandidateLevel>
    {
        public void Configure(EntityTypeBuilder<CandidateLevel> builder)
        {
            builder.ToTable("CandidateLevel");
            builder.HasKey(x => x.Id);
            builder.Property(e => e.Id)
                .ValueGeneratedOnAdd()
                .UseIdentityColumn(1001, 1)
                .HasComment("Mã");
            builder.Property(e => e.Name)
                .HasMaxLength(255)
                .HasComment("Tên cấp ứng viên");
            builder.Property(e => e.Price).HasComment("Giá");
            builder.Property(e => e.DiscountPrice).HasComment("Mã giảm giá");
            builder.Property(e => e.DiscountEndDate).HasComment("Thời gian của mã giảm giá");
            builder.Property(e => e.ExpiryTime).HasComment("Thời gian trước khi hết hạn");
            builder.Property(e => e.Description)
                .HasMaxLength(500)
                .HasComment("Mô tả");
            builder.Property(e => e.Active)
                .IsRequired()
                .HasDefaultValueSql("((1))")
                .HasComment("Đánh dấu bị xóa");
            builder.Property(e => e.CreatedTime)
                .HasDefaultValueSql("(getdate())")
                .HasComment("Ngày tạo");
        }
    }
}
