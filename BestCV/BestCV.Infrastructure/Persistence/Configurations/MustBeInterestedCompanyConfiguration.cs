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
    public class MustBeInterestedCompanyConfiguration : IEntityTypeConfiguration<MustBeInterestedCompany>
    {
        public void Configure(EntityTypeBuilder<MustBeInterestedCompany> builder)
        {
            builder.ToTable("MustBeInterestedCompany");
            builder.Property(c => c.Id).ValueGeneratedOnAdd()
                .UseIdentityColumn(1001, 1)
                .HasComment("Mã");
            builder.Property(e => e.Active)
                .IsRequired()
                .HasDefaultValueSql("((1))")
                .HasComment("Đánh dấu bị xóa");
            builder.Property(e => e.CreatedTime)
                .HasDefaultValueSql("(getdate())")
                .HasComment("Ngày tạo");
            builder.Property(e => e.Description).HasMaxLength(500).HasComment("Mô tả");
            builder.HasKey(c => c.Id);
            builder.HasOne(c => c.Company)
                .WithMany(c => c.MustBeInterestedCompanies)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasForeignKey(c => c.CompanyId)
                .HasConstraintName("FK_MustBeInterestedCompany_Company");
            builder.HasIndex(c => c.CompanyId)
                .IsUnique();
        }
    }
}
