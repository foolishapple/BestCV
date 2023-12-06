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
    public class CompanyReviewConfiguration : IEntityTypeConfiguration<CompanyReview>
    {
        public void Configure(EntityTypeBuilder<CompanyReview> builder)
        {
            builder.ToTable("CompanyReview");
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id)
                .ValueGeneratedOnAdd()
                .UseIdentityColumn(1001, 1)
                .HasComment("Mã đánh giá công ty");
            builder.Property(e => e.CompanyId).IsRequired().HasComment("Mã công ty được đánh giá");
            builder.Property(e => e.CandidateId).IsRequired().HasComment("Mã ứng viên đánh giá");
            builder.Property(e => e.Review).HasMaxLength(500).HasComment("Đánh giá");
            builder.Property(e => e.Rating).HasComment("Điểm đánh giá");
            builder.Property(e => e.IsApproved).HasComment("Chấp thuận đánh giá");
            builder.Property(e => e.ApprovalDate).HasComment("Ngày chấp thuận đánh giá");
            builder.Property(e => e.Active)
                .IsRequired()
                .HasDefaultValueSql("((1))")
                .HasComment("Đánh dấu bị xóa");
            builder.Property(e => e.CreatedTime)
                .HasDefaultValueSql("(getdate())")
                .HasComment("Ngày tạo");

            builder.HasOne(x => x.Company).WithMany(x => x.CompanyReview)
                .HasForeignKey(x => x.CompanyId)
                .HasConstraintName("FK_CompanyReview_Company")
                .OnDelete(DeleteBehavior.ClientSetNull);

            builder.HasOne(x => x.Candidate).WithMany(x => x.CompanyReviews)
                .HasForeignKey(x => x.CandidateId)
                .HasConstraintName("FK_CompanyReview_Candidate")
                .OnDelete(DeleteBehavior.ClientSetNull);
        }
    }
}
