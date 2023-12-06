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
    public class CandidateCVConfiguration : IEntityTypeConfiguration<CandidateCV>
    {
        public void Configure(EntityTypeBuilder<CandidateCV> builder)
        {
            builder.ToTable("CandidateCV");
            builder.HasKey(x => x.Id);

            builder.Property(e => e.Id)
                .ValueGeneratedOnAdd()
                .UseIdentityColumn(1001, 1)
                .HasComment("Mã trạng thái template");

            builder.Property(e => e.Active)
                .IsRequired()
                .HasDefaultValueSql("((1))")
                .HasComment("Đánh dấu bị xóa");

            builder.Property(e => e.CreatedTime)
                .HasDefaultValueSql("(getdate())")
                .HasComment("Ngày tạo");

            builder.Property(e => e.ModifiedTime)
                .HasDefaultValueSql("(getdate())")
                .HasComment("Ngày sửa đổi");

            builder.Property(e => e.Name)
                .HasMaxLength(255)
                .HasComment("Tên trạng thái CV");

            builder.Property(e => e.CandidateId)
                .IsRequired()
                .HasComment("Mã ứng viên");

            builder.Property(e => e.CVTemplateId)
                .IsRequired(false)
                .HasComment("Mã template");

            builder.Property(e => e.Content)
                .IsRequired()
                .HasComment("Nội dung HTML của CV");

            builder.Property(e => e.AdditionalCSS)
                .IsRequired()
                .HasComment("CSS bổ sung của CV");

            builder.HasOne(d => d.Candidate).WithMany(p => p.CandidateCVs)
                .HasForeignKey(d => d.CandidateId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_CandidateCV_Candidate");

            builder.HasOne(d => d.CVTemplate).WithMany(p => p.CandidateCVs)
                .HasForeignKey(d => d.CVTemplateId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_CandidateCV_CVTemplate");
        }
    }
}
