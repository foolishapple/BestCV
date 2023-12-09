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
    public class EmployerMetaConfiguration : IEntityTypeConfiguration<EmployerMeta>
    {
        public void Configure(EntityTypeBuilder<EmployerMeta> builder)
        {
            builder.ToTable("EmployerMeta");
            builder.HasKey(x => x.Id);

            builder.Property(x => x.Id)
                .IsRequired()
                .ValueGeneratedOnAdd()
                .UseIdentityColumn(1001, 1);
            builder.Property(x => x.Active)
                .HasComment("Đánh dấu bị xóa")
                .HasDefaultValueSql("((1))")
                .IsRequired();
            builder.Property(x => x.CreatedTime)
                .HasDefaultValueSql("(getdate())")
                .HasComment("Ngày tạo");
            builder.Property(x => x.Description)
                .HasComment("Mô tả");
            builder.Property(x => x.Name)
                .IsRequired()
                .HasMaxLength(255)
                .HasComment("Tên dữ liệu bổ sung của nhà tuyển dụng");

            builder.Property(x => x.EmployerId)
                .IsRequired()
                .HasComment("Mã nhà tuyển dụng");

            builder.Property(x => x.Key)
                .IsRequired()
                .HasMaxLength(255)
                .HasComment("Tên khóa");
            builder.Property(x => x.Value)
                .IsRequired()
                .HasMaxLength(255)
                .HasComment("Giá trị");

            builder.HasOne(x => x.Employer).WithMany(x => x.EmployerMetas)
                .HasForeignKey(x => x.EmployerId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_EmployerMeta_Employer");

        }
    }
}
