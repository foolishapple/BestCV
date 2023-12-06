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
    public class CandidateOrderDetailConfiguration : IEntityTypeConfiguration<CandidateOrderDetail>
    {
        public void Configure(EntityTypeBuilder<CandidateOrderDetail> builder)
        {
            builder.ToTable("CandidateOrderDetail");
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id)
                .ValueGeneratedOnAdd()
                .UseIdentityColumn(1001, 1);

            builder.Property(e => e.CandidateOrderId).HasComment("Mã đơn hàng ứng viên");
            builder.Property(e => e.CandidateLevelId).HasComment("Mã cấp độ ứng viên ");
            builder.Property(e => e.Quantity).HasComment("Số lượng");
            builder.Property(e => e.Price).HasComment("Giá");
            builder.Property(e => e.DiscountPrice).HasComment("Giảm giá");
            builder.Property(e => e.FinalPrice).HasComment("Giá cuối");

            builder.Property(e => e.Active)
                 .IsRequired()
                 .HasDefaultValueSql("((1))")
                 .HasComment("Đánh dấu bị xóa");

            builder.Property(e => e.CreatedTime)
                .HasDefaultValueSql("(getdate())")
                .HasComment("Ngày tạo");

            builder.HasOne(d => d.CandidateOrders).WithMany(p => p.CandidateOrderDetails)
               .HasForeignKey(d => d.CandidateOrderId)
               .OnDelete(DeleteBehavior.ClientSetNull)
               .HasConstraintName("FK_CandidateOrderDetails_CandidateOrders");

            builder.HasOne(d => d.CandidateLevel).WithMany(p => p.CandidateOrderDetails)
               .HasForeignKey(d => d.CandidateLevelId)
               .OnDelete(DeleteBehavior.ClientSetNull)
               .HasConstraintName("FK_CandidateOrderDetails_CandidateLevel");

        }
    }
}
