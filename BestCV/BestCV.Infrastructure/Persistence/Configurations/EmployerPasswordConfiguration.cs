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
    public class EmployerPasswordConfiguration : IEntityTypeConfiguration<EmployerPassword>
    {
        public void Configure(EntityTypeBuilder<EmployerPassword> builder)
        {
            builder.ToTable("EmployerPassword");
            builder.HasKey(x => x.Id);

            builder.Property(e => e.Id)
                .HasComment("Mã mật khẩu của nhà tuyển dụng")
                .ValueGeneratedOnAdd()
                .UseIdentityColumn(1001, 1);

            builder.Property(e => e.Active)
                .IsRequired()
                .HasDefaultValueSql("((1))")
                .HasComment("Đánh dấu bị xóa");

            builder.Property(e => e.CreatedTime)
                .HasDefaultValueSql("(getdate())")
                .HasComment("Ngày tạo");

            builder.HasOne(x => x.Employer).WithMany(x => x.EmployerPasswords)
                .HasForeignKey(x => x.EmployerId)
                .HasConstraintName("FK_EmployerPassword_Employer")
                .OnDelete(DeleteBehavior.ClientSetNull);
        }
    }
}
