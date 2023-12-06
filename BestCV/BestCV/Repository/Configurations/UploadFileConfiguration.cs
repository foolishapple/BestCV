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
    public class UploadFileConfiguration : IEntityTypeConfiguration<UploadFile>
    {
        public void Configure(EntityTypeBuilder<UploadFile> builder)
        {
            builder.ToTable("UploadFile");
            builder.HasKey(e => e.Id);
            builder.Property(e => e.Id)
                .ValueGeneratedOnAdd()
                .UseIdentityColumn(1001, 1)
                .HasComment("Mã file");
            builder.HasOne(g => g.FolderUpload)
                .WithMany(e => e.UploadFiles)
                .HasForeignKey(e => e.FolderUploadId)
                .HasConstraintName("FK_UploadFile_FolderUpload");
            builder.HasOne(g => g.AdminAccount)
                .WithMany(e => e.UploadFiles)
                .HasForeignKey(e => e.AdminAccountId)
                .HasConstraintName("FK_UploadFile_AdminAccount");
            builder.Property(e => e.Extension)
                .HasMaxLength(255)
                .HasComment("Đuôi tệp tin");
            builder.Property(e => e.Name)
                .HasMaxLength(500);
            builder.Property(e => e.Size);
            builder.Property(e => e.Path)
                .HasMaxLength(500);
            builder.Property(e => e.ThumbnailPath)
                .HasMaxLength(500);
            builder.Property(e => e.MimeType)
                .HasMaxLength(500);
            builder.Property(e => e.Active)
                .IsRequired()
                .HasDefaultValueSql("((1))")
                .HasComment("Đánh dấu bị xóa");
            builder.Property(e => e.CreatedTime).HasDefaultValueSql("(getdate())")
                .HasComment("Ngày tạo");
        }
    }
}
