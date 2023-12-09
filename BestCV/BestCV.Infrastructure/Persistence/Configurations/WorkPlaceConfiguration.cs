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
    public class WorkPlaceConfiguration : IEntityTypeConfiguration<WorkPlace>
    {
        public void Configure(EntityTypeBuilder<WorkPlace> builder)
        {
            builder.ToTable("WorkPlace");
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
            builder.Property(e => e.ParentId).HasComment("Mã cha");
            builder.Property(e => e.Name).HasMaxLength(255).HasComment("Tên thành phố, quận , huyện");
            builder.Property(e => e.Code).IsRequired().HasMaxLength(255).HasComment("Mã đơn vị hành chính");
            builder.Property(e => e.Description).HasMaxLength(500).HasComment("Mô tả ");
            builder.Property(e => e.DivisionType).HasMaxLength(255).HasComment("Loại đơn vị hành chính");
            builder.Property(e => e.CodeName).HasMaxLength(255).HasComment("Tên đơn vị hành chính viết ở dạng snake_case và bỏ dấu");
            builder.Property(e => e.PhoneCode).HasComment("Mã vùng điện thoại");
            builder.Property(e => e.ProvinceCode).HasComment("Mã tỉnh thành");
        }
    }
}