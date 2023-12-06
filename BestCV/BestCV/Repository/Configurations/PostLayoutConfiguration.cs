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
    public class PostLayoutConfiguration : IEntityTypeConfiguration<PostLayout>
    {
        public void Configure(EntityTypeBuilder<PostLayout> builder)
        {
            builder.ToTable("PostLayout");
            builder.HasKey(x => x.Id);
            builder.Property(e => e.Id)
                .ValueGeneratedOnAdd()
                .UseIdentityColumn(1001, 1)
                .HasComment("Mã layout bài viết");
            builder.Property(e => e.Active)
                .IsRequired()
                .HasDefaultValueSql("((1))")
                .HasComment("Đánh dấu bị xóa");
            builder.Property(e => e.CreatedTime)
                .HasDefaultValueSql("(getdate())")
                .HasComment("Ngày tạo");
            builder.Property(e => e.Name)
                .HasMaxLength(255)
                .HasComment("Tên layout bài viết");
            builder.Property(e => e.Description)
                .HasMaxLength(500)
                .HasComment("Mô tả");
        }
    }
}
