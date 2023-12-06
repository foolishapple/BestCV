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
    public class CandidateCVPDFConfiguration : IEntityTypeConfiguration<CandidateCVPDF>
    {
        public void Configure(EntityTypeBuilder<CandidateCVPDF> builder)
        {
            builder.ToTable("CandidateCVPDF");
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

            builder.Property(e => e.CandidateId)
                .IsRequired()
                .HasComment("Mã CV");

            builder.Property(e => e.Url)
                .IsRequired()
                .HasComment("Đường dẫn của file CV PDF trên Server");

            builder.HasOne(d => d.Candidate).WithMany(p => p.CandidateCVPDFs)
                .HasForeignKey(d => d.CandidateId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_CandidateCVPDF_Candidate");
            builder.HasOne(d => d.CandidateCVPDFType).WithMany(p => p.CandidateCVPDFs)
                .HasForeignKey(d => d.CandidateCVPDFTypeId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_CandidateCVPDF_CandidateCVPDFType");
        }
    }
}
