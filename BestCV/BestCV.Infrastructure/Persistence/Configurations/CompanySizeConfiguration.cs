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
    public class CompanySizeConfiguration : IEntityTypeConfiguration<CompanySize>
    {
        public void Configure(EntityTypeBuilder<CompanySize> builder)
        {
            builder.ToTable("CompanySize");

            builder.HasKey(e => e.Id);

            builder.Property(e => e.Id)
                .ValueGeneratedOnAdd()
                .UseIdentityColumn(1001, 1)
                .HasComment("Mã quy mô công ty");

            builder.Property(e => e.Name)
                .HasMaxLength(255)
                .HasComment("Tên quy mô công ty");

            builder.Property(e => e.Active)
                .IsRequired()
                .HasDefaultValue(true)
                .HasComment("Đánh dấu bị xóa");

            builder.Property(e => e.CreatedTime)
                .HasDefaultValueSql("getdate()")
                .HasComment("Ngày tạo");

            builder.Property(e => e.Description)
                .HasComment("Mô tả")
                .HasMaxLength(500);

            builder.HasIndex(e => e.Name).IsUnique();
        }
    }
}
