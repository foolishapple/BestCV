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
    public class CompanyImageConfiguration : IEntityTypeConfiguration<CompanyImage>
    {
        public void Configure(EntityTypeBuilder<CompanyImage> builder)
        {
            builder.ToTable("CompanyImage");
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
            builder.Property(e => e.Name).HasMaxLength(255).HasComment("Tên ");
            builder.Property(e => e.Description).HasMaxLength(500).HasComment("Mô tả ");
            builder.Property(e => e.Path).HasMaxLength(500).HasComment("Đường dẫn ảnh");
            builder.Property(e => e.OrderSort).HasComment("Sắp xếp");
            builder.Property(e => e.CompanyId).IsRequired().HasComment("Mã công ty ");
            builder.HasOne(d => d.Company).WithMany(p => p.CompanyImages)
               .HasForeignKey(d => d.CompanyId)
               .OnDelete(DeleteBehavior.ClientSetNull)
               .HasConstraintName("FK_CompanyImage_Company");


        }
    }
}
