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
    public class FolderUploadConfiguration : IEntityTypeConfiguration<FolderUpload>
    {
        public void Configure(EntityTypeBuilder<FolderUpload> builder)
        {
            builder.ToTable("FolderUpload");
            builder.HasKey(c => c.Id);
            builder.Property(e => e.Id)
                .ValueGeneratedOnAdd()
                .UseIdentityColumn(1001, 1)
                .HasComment("Mã");

            builder.Property(e => e.Id).HasComment("Mã thư mục");
            builder.Property(e => e.Active)
                .IsRequired()
                .HasDefaultValueSql("((1))")
                .HasComment("Đánh dấu bị xóa");
            builder.Property(e => e.CreatedTime).HasComment("Ngày tạo");
            builder.Property(e => e.Name)
                .HasMaxLength(255)
                .HasComment("Tên");
            builder.Property(e => e.Path)
                .HasMaxLength(255)
                .HasComment("Đường dẫn");
            builder.Property(e => e.ParentId).HasComment("Mã thư mục cha");
            builder.Property(e => e.TreeIds)
                .HasMaxLength(255)
                .HasComment("Để truy cập và quản lý các nút trong cây thư mục");
            builder.Property(e => e.CreatedTime)
                .HasDefaultValueSql("(getdate())")
                .HasComment("Ngày tạo");

        }
    }
}
