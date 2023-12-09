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
    public class PostConfiguration : IEntityTypeConfiguration<Post>
    {
        public void Configure(EntityTypeBuilder<Post> builder)
        {
            builder.ToTable("Post");
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id)
                .ValueGeneratedOnAdd()
                .UseIdentityColumn(1001, 1);

            builder.Property(x => x.Active).HasDefaultValueSql("((1))").HasComment("Đánh dấu bị xóa");
            builder.Property(x => x.CreatedTime).HasDefaultValueSql("(getdate())").HasComment("Ngày tạo");
            builder.Property(x => x.Name).HasMaxLength(500).HasComment("Tiêu đề bài viết");
            builder.Property(x => x.Overview).HasMaxLength(500).HasComment("Mô tả ngắn");
            builder.Property(x => x.Photo).HasMaxLength(500).HasComment("Đường dẫn ảnh bài viết");
            builder.Property(x => x.AuthorId).HasComment("Mã tác giả");
            builder.Property(x => x.PublishedTime).HasComment("Thời gian xuất bản");
            builder.Property(x => x.PublishedTime).HasComment("Thời gian xuất bản");
            builder.Property(x => x.IsApproved).HasComment("Trạng thái duyệt bài viết");
            builder.Property(x => x.ApprovalDate).HasComment("Thời gian duyệt bài viết");
            builder.Property(x => x.IsPublish).HasComment("Trạng thái xuất bản bài viết");
            builder.Property(x => x.PublishedTime).HasComment("Thời gian xuất bản bài viết");
            builder.Property(x => x.Description).HasComment("Mô tả");
            builder.Property(c => c.Search)
                .HasComment("Tìm kiếm")
                .HasColumnType("varchar(MAX)");

            builder.HasOne(d => d.PostType).WithMany(p => p.Posts)
                .HasForeignKey(d => d.PostTypeId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Post_PostType");
            builder.HasOne(d => d.PostCategory).WithMany(p => p.Posts)
                .HasForeignKey(d => d.PostCategoryId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Post_PostCategory");  
            builder.HasOne(d => d.PostLayout).WithMany(p => p.Posts)
                .HasForeignKey(d => d.PostLayoutId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Post_PostLayout");       
            builder.HasOne(d => d.PostStatus).WithMany(p => p.Posts)
                .HasForeignKey(d => d.PostStatusId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Post_PostStatus");  
            builder.HasOne(d => d.AdminAccount).WithMany(p => p.Posts)
                .HasForeignKey(d => d.AuthorId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Post_AdminAccount");

            
        }
    }
}
