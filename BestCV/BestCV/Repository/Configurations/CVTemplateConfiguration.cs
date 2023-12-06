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
    public class CVTemplateConfiguration : IEntityTypeConfiguration<CVTemplate>
    {
        public void Configure(EntityTypeBuilder<CVTemplate> builder)
        {
            builder.ToTable("CVTemplate");
            builder.HasKey(x => x.Id);

            builder.Property(e => e.Id)
                .ValueGeneratedOnAdd()
                .UseIdentityColumn(1001, 1)
                .HasComment("Mã trạng thái template");

            builder.Property(e => e.Active)
                .IsRequired()
                .HasDefaultValueSql("((1))")
                .HasComment("Đánh dấu bị xóa");

            builder.Property(e => e.CreatedTime)
                .HasDefaultValueSql("(getdate())")
                .HasComment("Ngày tạo");

            builder.Property(e => e.Description)
                .HasMaxLength(1000)
                .HasComment("Mô tả");

            builder.Property(e => e.Name)
                .HasMaxLength(255)
                .HasComment("Tên trạng thái CV");

            builder.Property(e => e.Photo)
                .HasMaxLength(500)
                .HasComment("Ảnh Template");

            builder.Property(e => e.OrderSort)
                .IsRequired()
                .HasComment("Sắp xếp");

            builder.Property(e => e.CVTemplateStatusId)
                .IsRequired()
                .HasComment("Mã trạng thái template");

            builder.Property(e => e.Version)
                .HasMaxLength(50)
                .HasComment("Phiên bản template");

            builder.Property(e => e.Content)
                .IsRequired()
                .HasComment("Nội dung HTML của template");

            builder.Property(e => e.AdditionalCSS)
                .IsRequired()
                .HasComment("CSS bổ sung của Template");

            builder.HasOne(d => d.CVTemplateStatus).WithMany(p => p.CVTemplates)
                .HasForeignKey(d => d.CVTemplateStatusId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_CVTemplate_CVTemplateStatus");
        }
    }
}
