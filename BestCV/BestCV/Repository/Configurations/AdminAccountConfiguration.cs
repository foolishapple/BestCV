using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Jobi.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Jobi.Domain.Constants;

namespace Jobi.Infrastructure.Persistence.Configurations
{
    public class AdminAccountConfiguration : IEntityTypeConfiguration<AdminAccount>
    {
        public void Configure(EntityTypeBuilder<AdminAccount> builder)
        {
            builder.ToTable("AdminAccount");
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id)
                .HasComment("Mã tài khoản admin")
                .ValueGeneratedOnAdd()
                .UseIdentityColumn(1001, 1);
            builder.Property(c => c.Search)
                .HasComment("Trường search không dấu")
                .HasColumnType("varchar(MAX)");
            builder.Property(c => c.UserName)
                .HasComment("Tên đăng nhập")
                .HasMaxLength(255);
            builder.Property(c => c.FullName)
                .HasComment("Tên đầy đủ")
                .HasMaxLength(255);
            builder.Property(c => c.Password)
                .HasComment("Mật khẩu")
                .HasMaxLength(500);
            builder.Property(c => c.Photo)
               .HasComment("Đường dẫn ảnh")
               .HasMaxLength(500);
            builder.Property(c => c.UserName)
               .HasComment("Tên đăng nhập")
               .HasMaxLength(255);
            builder.Property(c => c.Description)
               .HasComment("Mô tả")
               .HasMaxLength(500);
            builder.Property(e => e.Active)
                .IsRequired()
                .HasDefaultValueSql("((1))")
                .HasComment("Đánh dấu bị xóa");
            builder.Property(e => e.CreatedTime)
                .HasDefaultValueSql("(getdate())")
                .HasComment("Ngày tạo");
            builder.Property(x => x.Phone)
                .HasMaxLength(25)
                .HasComment("Số điện thoại");
            builder.Property(x => x.FullName).HasMaxLength(255);
            builder.Property(x => x.Email).HasMaxLength(500);
            builder.Property(x => x.LockEnabled).HasDefaultValue(0);
            builder.Property(x => x.AccessFailedCount).HasDefaultValue(0);
        }
    }
}
