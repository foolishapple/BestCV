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
    public class CandidateProjectsConfiguration : IEntityTypeConfiguration<CandidateProjects>
    {
        public void Configure(EntityTypeBuilder<CandidateProjects> builder)
        {
            builder.ToTable("CandidateProjects");

            builder.HasKey(t => t.Id);

            builder.Property(e => e.Id)
                .HasComment("Mã dự án ứng viên")
                .ValueGeneratedOnAdd()
                .UseIdentityColumn(1001, 1);
            builder.Property(e => e.Active)
                .IsRequired()
                .HasDefaultValueSql("((1))")
                .HasComment("Đánh dấu bị xóa");
            builder.Property(e => e.CreatedTime)
                .HasDefaultValueSql("(getdate())")
                .HasComment("Ngày tạo");
            //builder.Property(e => e.Description).HasComment("Mô tả");
            builder.Property(e => e.TimePeriod).HasMaxLength(50).HasComment("Khoảng thời gian");
            builder.Property(e => e.ProjectName).HasMaxLength(500).HasComment("Tên dự án");
            builder.Property(e => e.Customer).HasMaxLength(500).HasComment("Khách hàng");
            builder.Property(e => e.TeamSize).HasComment("Số người trong dự án");
            builder.Property(e => e.Position).HasMaxLength(255).HasComment("Chức vụ");
            builder.Property(e => e.Responsibilities).HasMaxLength(500).HasComment("Trách nhiệm");
            builder.Property(e => e.Info).HasMaxLength(500).HasComment("Thông tin");

            builder.HasOne(d => d.Candidate).WithMany(p => p.CandidateProjectses)
               .HasForeignKey(d => d.CandidateId)
               .OnDelete(DeleteBehavior.ClientSetNull)
               .HasConstraintName("FK_CandidateProjects_Candidate");
        }
    }
}
