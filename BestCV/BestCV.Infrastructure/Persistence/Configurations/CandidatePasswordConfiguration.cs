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
    public class CandidatePasswordConfiguration : IEntityTypeConfiguration<CandidatePassword>
    {
        public void Configure(EntityTypeBuilder<CandidatePassword> builder)
        {
            builder.ToTable("CandidatePassword");
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id)
                .ValueGeneratedOnAdd()
                .UseIdentityColumn(1001, 1);

            builder.Property(e => e.CandidateId).HasComment("Mã ứng viên");
            builder.Property(e => e.OldPassword)
               .HasMaxLength(500)
               .HasComment("Mật khẩu cũ");
            builder.Property(e => e.Active)
                 .IsRequired()
                 .HasDefaultValueSql("((1))")
                 .HasComment("Đánh dấu bị xóa");

            builder.Property(e => e.CreatedTime)
                .HasDefaultValueSql("(getdate())")
                .HasComment("Ngày tạo");

            builder.HasOne(d => d.Candidate).WithMany(p => p.CandidatePasswords)
                .HasForeignKey(d => d.CandidateId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_CandidatePassword_Candidate");
        }
    }
}
