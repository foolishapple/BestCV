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
    public class FieldOfActivityConfiguration : IEntityTypeConfiguration<FieldOfActivity>
    {
        public void Configure(EntityTypeBuilder<FieldOfActivity> builder)
        {
            builder.ToTable("FieldOfActivity");
            builder.HasKey(e => e.Id);

            builder.Property(e => e.Id)
                .HasComment("Mã lĩnh vực hoạt động")
                .ValueGeneratedOnAdd()
                .UseIdentityColumn(1001, 1);

            builder.Property(e => e.Active)
                .IsRequired()
                .HasDefaultValueSql("((1))")
                .HasComment("Đánh dấu bị xóa");

            builder.Property(e => e.CreatedTime)
                .HasDefaultValueSql("(getdate())")
                .HasComment("Ngày tạo");

            builder.Property(e=> e.Description)
                .HasComment("Mô tả")
                .HasMaxLength(500);

            builder.Property(e => e.Name)
                .HasComment("Tên lĩnh vực hoạt động")
                .HasMaxLength(255);

            builder.HasIndex(x=> x.Name).IsUnique();
        }
    }
}
