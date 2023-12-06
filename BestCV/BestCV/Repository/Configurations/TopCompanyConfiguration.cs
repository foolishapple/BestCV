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
    internal class TopCompanyConfiguration : IEntityTypeConfiguration<TopCompany>
    {
        public void Configure(EntityTypeBuilder<TopCompany> builder)
        {
            builder.ToTable("TopCompany");
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id)
                .ValueGeneratedOnAdd()
                .UseIdentityColumn(1001, 1)
                .HasComment("Mã công ty hàng đầu");
            builder.Property(e => e.Active)
                .IsRequired()
                .HasDefaultValueSql("((1))")
                .HasComment("Đánh dấu bị xóa");
            builder.Property(e => e.CreatedTime)
                .HasDefaultValueSql("(getdate())")
                .HasComment("Ngày tạo");
            builder.Property(e => e.CompanyId)
                .IsRequired()
                .HasComment("Mã công ty");
            builder.Property(e => e.OrderSort)
                .IsRequired()
                .HasComment("Sắp xếp");
            builder.Property(e => e.SubOrderSort)
               .HasComment("Thứ tự sắp xếp phụ giữa các order sort cùng bậc");
            builder.HasOne(x => x.Company).WithOne(x => x.TopCompany)
                .HasForeignKey<TopCompany>(x => x.CompanyId)
                .HasConstraintName("FK_TopCompany_Company")
                .OnDelete(DeleteBehavior.ClientSetNull);
        }
    }
}
