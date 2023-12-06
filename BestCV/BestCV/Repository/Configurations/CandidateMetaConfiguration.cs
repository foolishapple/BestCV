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
    public class CandidateMetaConfiguration : IEntityTypeConfiguration<CandidateMeta>
    {
        public void Configure(EntityTypeBuilder<CandidateMeta> builder)
        {
            builder.ToTable("CandidateMeta");
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id)
                .ValueGeneratedOnAdd()
                .UseIdentityColumn(1001, 1)
                .HasComment("Mã Meta ứng viên"); ;          
            builder.Property(e => e.Active)
                .IsRequired()
                .HasDefaultValueSql("((1))")
                .HasComment("Đánh dấu bị xóa");
            builder.Property(e => e.CreatedTime)
                .HasDefaultValueSql("(getdate())")
                .HasComment("Ngày tạo");
            builder.Property(e => e.Name)
                .HasMaxLength(255)
                .HasComment("Tên Meta");
            builder.Property(e => e.Value)
                .HasMaxLength(255)
                .HasComment("giá trị");
            builder.Property(e => e.Key)
                .HasMaxLength(255)
                .HasComment("key");
            builder.Property(e => e.Description)
                .HasMaxLength(500)
                .HasComment("Mô tả");

            builder.HasOne(d => d.Candidate).WithMany(p => p.CandidateMetas)
               .HasForeignKey(d => d.CandidateId)
               .OnDelete(DeleteBehavior.ClientSetNull)
               .HasConstraintName("FK_CandidateMeta_Candidate");
        }
    }
}
