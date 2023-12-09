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
    public class CandidateSuggestionJobCategoryConfiguration : IEntityTypeConfiguration<CandidateSuggestionJobCategory>
    {
        public void Configure(EntityTypeBuilder<CandidateSuggestionJobCategory> builder)
        {
            builder.ToTable("CandidateSuggestionJobCategory");
            builder.HasKey(x => x.Id);
            builder.Property(e => e.Id)
                .ValueGeneratedOnAdd()
                .UseIdentityColumn(1001, 1)
                .HasComment("Mã việc làm đề xuất cho ứng viên");
            builder.Property(e => e.Active)
                .IsRequired()
                .HasDefaultValueSql("((1))")
                .HasComment("Đánh dấu bị xóa");
            builder.Property(e => e.CreatedTime)
                .HasDefaultValueSql("(getdate())")
                .HasComment("Ngày tạo");

            builder.HasOne(d => d.Candidate).WithMany(p => p.CandidateSuggestionJobCategories)
               .HasForeignKey(d => d.CandidateId)
               .OnDelete(DeleteBehavior.ClientSetNull)
               .HasConstraintName("FK_CandidateSuggestionJobCategory_Candidate");

            builder.HasOne(d => d.JobCategory).WithMany(p => p.CandidateSuggestionJobCategories)
               .HasForeignKey(d => d.JobCategoryId)
               .OnDelete(DeleteBehavior.ClientSetNull)
               .HasConstraintName("FK_CandidateSuggestionJobCategory_JobCategory");
        }
    }
}
