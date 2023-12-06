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
    public class MenuConfiguration : IEntityTypeConfiguration<Menu>
    {
        public void Configure(EntityTypeBuilder<Menu> builder)
        {
            builder.ToTable("Menu");
            builder.HasKey(e => e.Id);
            builder.Property(x => x.Id)
                .ValueGeneratedOnAdd()
                .UseIdentityColumn(1001, 1)
                .HasComment("Mã menu");
            builder.Property(e => e.Active)
                .IsRequired()
                .HasDefaultValueSql("((1))")
                .HasComment("Đánh dấu bị xóa");
            builder.Property(e => e.CreatedTime)
                .HasDefaultValueSql("(getdate())")
                .HasComment("Ngày tạo");
            builder.Property(e => e.Name)
                .HasMaxLength(255)
                .HasComment("Tên");
            builder.Property(e => e.TreeIds)
                .HasMaxLength(255)
                .HasComment("Để truy cập và quản lý các nút trong cây thư mục");
            builder.Property(e => e.ParentId).HasComment("Mã cấp danh mục");
            builder.Property(c => c.Description).HasComment("Mô tả");

            builder.Property(c => c.Icon).HasComment("Icon");
            builder.Property(c => c.Link).HasComment("Đường dẫn");

            builder.HasOne(c => c.MenuType).WithMany(p => p.Menus)
                .HasForeignKey(d => d.MenuTypeId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Menu_MenuType");
        }
    }
}
