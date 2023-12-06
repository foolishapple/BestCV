using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Jobi.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Jobi.Infrastructure.Persistence.Configurations
{
    public class JobRequireDistrictConfiguration : IEntityTypeConfiguration<JobRequireDistrict>
    {
        public void Configure(EntityTypeBuilder<JobRequireDistrict> builder)
        {
            builder.ToTable("JobRequireDistrict");
            builder.HasKey(e => e.Id);

            builder.Property(e => e.Id)
                .ValueGeneratedOnAdd()
                .UseIdentityColumn(1001, 1);

            builder.Property(e => e.JobRequireCityId)
                .IsRequired()
                .HasComment("Mã tỉnh/thành phố");

            builder.Property(e => e.DistrictId)
                .IsRequired()
                .HasComment("Mã quận/huyện");

            builder.Property(e => e.AddressDetail)
                .IsRequired()
                .HasComment("Địa chỉ chi tiết");

            builder.Property(e => e.Active)
                .IsRequired()
                .HasDefaultValueSql("((1))")
                .HasComment("Đánh dấu bị xóa");

            builder.Property(e => e.CreatedTime)
                .HasDefaultValueSql("(getdate())")
                .HasComment("Ngày tạo");
            builder.HasOne(d => d.JobRequireCity).WithMany(p => p.JobRequireDistricts)
          .HasForeignKey(d => d.JobRequireCityId)
          .OnDelete(DeleteBehavior.ClientSetNull)
          .HasConstraintName("FK_JobRequireDistrict_JobRequireCity");
        }
    }
}
