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
    public class CandidateEducationConfiguration : IEntityTypeConfiguration<CandidateEducation>
    {
        public void Configure(EntityTypeBuilder<CandidateEducation> builder)
        {
            builder.ToTable("CandidateEducation");
            builder.HasKey(x => x.Id);
            builder.Property(e => e.Id)
                .ValueGeneratedOnAdd()
                .UseIdentityColumn(1001, 1)
                .HasComment("Mã học vấn ứng viên");
            builder.Property(e => e.Title)
                .HasMaxLength(500)
                .HasComment("Tiêu đề");
            builder.Property(e => e.School)
                .HasMaxLength(500)
                .HasComment("Trường học");
            builder.Property(e => e.TimePeriod)
                .HasMaxLength(50)
                .HasComment("Khoảng thời gian");
            builder.Property(e => e.CreatedTime)
                .HasDefaultValueSql("(getdate())")
                .HasComment("Ngày tạo");
            builder.Property(e => e.Active)
                .IsRequired()
                .HasDefaultValueSql("((1))")
                .HasComment("Đánh dấu bị xóa");
            builder.Property(e => e.Description)
               .HasMaxLength(500)
               .HasComment("Mô tả");

            builder.HasOne(d => d.Candidate).WithMany(p => p.CandidateEducations)
                .HasForeignKey(d => d.CandidateId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("Fk_CandidateEducation_Candidate");
        }
    }
}
