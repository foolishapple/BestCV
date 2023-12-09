using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using BestCV.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BestCV.Infrastructure.Persistence.Configurations
{
    //public class PasswordConfiguration : IEntityTypeConfiguration<Password>
    //{
    //    public void Configure(EntityTypeBuilder<Password> builder)
    //    {
    //        builder.ToTable("Password");
    //        builder.HasKey(x => x.Id);
    //        builder.Property(e => e.Id)
    //            .ValueGeneratedOnAdd()
    //            .UseIdentityColumn(1001, 1)
    //            .HasComment("Mã password");
    //        builder.Property(e => e.AccountId).HasComment("Mã tài khoản");
    //        builder.Property(e => e.Active)
    //            .IsRequired()
    //            .HasDefaultValueSql("((1))")
    //            .HasComment("Đánh dấu bị xóa");
    //        builder.Property(e => e.CreatedTime)
    //            .HasDefaultValueSql("(getdate())")
    //            .HasComment("Ngày tạo");
    //        builder.Property(e => e.Description).HasComment("Mô tả");
    //        builder.Property(e => e.OldPassword)
    //            .HasMaxLength(255)
    //            .HasComment("Password cũ");

    //        builder.HasOne(d => d.Account).WithMany(p => p.Passwords)
    //            .HasForeignKey(d => d.AccountId)
    //            .OnDelete(DeleteBehavior.ClientSetNull)
    //            .HasConstraintName("FK_Password_Account");
    //    }
    //}
}
