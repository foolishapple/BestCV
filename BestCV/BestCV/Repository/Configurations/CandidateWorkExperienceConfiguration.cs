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
    internal class CandidateWorkExperienceConfiguration : IEntityTypeConfiguration<CandidateWorkExperience>
    {
        public void Configure(EntityTypeBuilder<CandidateWorkExperience> builder)
        {
            builder.ToTable("CandidateWorkExperience");
            builder.HasKey(x => x.Id);
            builder.Property(e => e.Id)
                .ValueGeneratedOnAdd()
                .UseIdentityColumn(1001, 1)
                .HasComment("Mã kinh nghiệm làm việc của ứng viên");
            builder.Property(e => e.Active)
                .IsRequired()
                .HasDefaultValueSql("((1))")
                .HasComment("Đánh dấu bị xóa");
            builder.Property(e => e.CreatedTime)
                .HasDefaultValueSql("(getdate())")
                .HasComment("Ngày tạo");
            builder.Property(e => e.JobTitle)
                .HasMaxLength(500)
                .HasComment("Tiêu đề công việc");
            builder.Property(e => e.Company)
                .HasMaxLength(500)
                .HasComment("Công ty");
            builder.Property(e => e.TimePeriod)
                .HasMaxLength(50)
                .HasComment("Khoảng thời gian");
            builder.Property(e => e.Description)
                .HasMaxLength(1000)
                .HasComment("Mô tả");

            builder.HasOne(d => d.Candidate).WithMany(p => p.CandidateWorkExperiences)
                .HasForeignKey(d => d.CandidateId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("CandidateWorkExperience_Candidate");
        }
    }
}
