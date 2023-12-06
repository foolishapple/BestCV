using Jobi.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Jobi.Infrastructure.Persistence.Configurations
{
    public class JobSecondaryJobCategoryConfiguration : IEntityTypeConfiguration<JobSecondaryJobCategory>
    {
        public void Configure(EntityTypeBuilder<JobSecondaryJobCategory> builder)
        {
            builder.ToTable("JobSecondaryJobCategory");
            builder.HasKey(e => e.Id);

            builder.Property(e => e.Id)
                .ValueGeneratedOnAdd()
                .UseIdentityColumn(1001, 1);

            builder.Property(e => e.JobId)
                .IsRequired()
                .HasComment("Mã tin tuyển dụng");

            builder.Property(e => e.JobCategoryId)
                .IsRequired()
                .HasComment("Mã ngành nghề liên quan");

            builder.Property(e => e.Active)
                .IsRequired()
                .HasDefaultValueSql("((1))")
                .HasComment("Đánh dấu bị xóa");

            builder.Property(e => e.CreatedTime)
                .HasDefaultValueSql("(getdate())")
                .HasComment("Ngày tạo");
            builder.HasOne(d => d.Job).WithMany(p => p.JobSecondaryJobCategories)
              .HasForeignKey(d => d.JobId)
              .OnDelete(DeleteBehavior.ClientSetNull)
              .HasConstraintName("FK_JobSecondaryJobCategory_Job");
            builder.HasOne(d => d.JobCategory).WithMany(p => p.JobSecondaryJobCategories)
              .HasForeignKey(d => d.JobCategoryId)
              .OnDelete(DeleteBehavior.ClientSetNull)
              .HasConstraintName("FK_JobSecondaryJobCategory_JobCategory");
        }
    }
}
