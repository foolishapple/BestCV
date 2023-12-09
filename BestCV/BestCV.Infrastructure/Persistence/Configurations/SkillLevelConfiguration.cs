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
    public class SkillLevelConfiguration : IEntityTypeConfiguration<SkillLevel>
    {
        public void Configure(EntityTypeBuilder<SkillLevel> builder)
        {
            builder.ToTable("SkillLevel");
            builder.HasKey(x => x.Id);
            builder.Property(e => e.Id)
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
                .HasMaxLength(255)
                .HasComment("Tên level");

        }
    }
}
