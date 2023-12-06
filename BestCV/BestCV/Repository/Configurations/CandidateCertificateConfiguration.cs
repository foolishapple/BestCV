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
    public class CandidateCertificateConfiguration : IEntityTypeConfiguration<CandidateCertificate>
    {
        public void Configure(EntityTypeBuilder<CandidateCertificate> builder)
        {
            builder.ToTable("CandidateCertificate");

            builder.HasKey(t => t.Id);

            builder.Property(e => e.Id)
                .HasComment("Mã chứng chỉ ứng viên ")
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
            builder.Property(e => e.IssueBy).HasComment("Cấp bởi");
            builder.Property(e => e.TimePeriod).HasMaxLength(50).HasComment("Khoảng thời gian");
            builder.Property(e => e.Name)
                .HasMaxLength(500)
                .HasComment("Tên chứng chỉ ứng viên");

            builder.HasOne(d => d.Candidate).WithMany(p => p.CandidateCertificates)
                .HasForeignKey(d => d.CandidateId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_CandidateCertificate_Candidate");
        }
    }
}
