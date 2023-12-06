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
    public class CandidateActivitiesConfiguration : IEntityTypeConfiguration<CandidateActivities>
    {
        public void Configure(EntityTypeBuilder<CandidateActivities> builder)
        {
            builder.ToTable("CandidateActivities");

            builder.HasKey(t => t.Id);

            builder.Property(e => e.Id)
                .HasComment("Mã hoạt động ứng viên")
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
            builder.Property(e => e.TimePeriod).HasMaxLength(50).HasComment("Mô tả");
            builder.Property(e => e.Name)
                .HasMaxLength(500)
                .HasComment("Tên hoạt động ứng viên");

            builder.HasOne(d => d.Candidate).WithMany(p => p.CandidateActivities)
                .HasForeignKey(d => d.CandidateId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_CandidateActivities_Candidate");
        }
    }
}
