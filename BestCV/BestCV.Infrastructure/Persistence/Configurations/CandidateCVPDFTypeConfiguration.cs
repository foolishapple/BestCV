using BestCV.Domain.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BestCV.Infrastructure.Persistence.Configurations
{
    public class CandidateCVPDFTypeConfiguration : IEntityTypeConfiguration<CandidateCVPDFType>
    {
        public void Configure(EntityTypeBuilder<CandidateCVPDFType> builder)
        {
            builder.ToTable("CandidateCVPDFTypes");

            builder.HasKey(t => t.Id);

            builder.Property(e => e.Id)
                .HasComment("Mã loại CV PDF của ứng viên")
                .ValueGeneratedOnAdd()
                .UseIdentityColumn(1001, 1);
            builder.Property(e => e.Active)
                .IsRequired()
                .HasDefaultValueSql("((1))")
                .HasComment("Đánh dấu bị xóa");
            builder.Property(e => e.CreatedTime)
                .HasDefaultValueSql("(getdate())")
                .HasComment("Ngày tạo");
            builder.Property(e => e.Description).HasMaxLength(500).HasComment("Mô tả");
            builder.Property(e => e.Name)
                .HasMaxLength(500)
                .HasComment("Tên loại CV PDF");
        }
    }
}