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
    public class AdminAccountRoleConfiguration : IEntityTypeConfiguration<AdminAccountRole>
    {
        public void Configure(EntityTypeBuilder<AdminAccountRole> builder)
        {
            builder.ToTable("AdminAccountRole");
            builder.Property(e => e.Id)
                .ValueGeneratedOnAdd()
                .UseIdentityColumn(1001, 1)
                .HasComment("Mã vai trò tài khoản admin");

            builder.Property(e => e.Active)
                .IsRequired()
                .HasDefaultValueSql("((1))")
                .HasComment("Đánh dấu bị xóa");
            builder.Property(e => e.CreatedTime)
                .HasDefaultValueSql("(getdate())")
                .HasComment("Ngày tạo");

            //Constrair
            builder.HasKey(e => e.Id);
            builder.HasIndex(e => new { e.AdminAccountId, e.RoleId }).IsUnique();
            builder.HasOne(d => d.AdminAccount).WithMany(p => p.AdminAccountRoles)
                .HasForeignKey(d => d.AdminAccountId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_AdminAccountRole_AdminAccount");
            builder.HasOne(d => d.Role).WithMany(p => p.AdminAccountRoles)
                .HasForeignKey(d => d.RoleId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_AdminAccountRole_Role");
        }
    }
}
