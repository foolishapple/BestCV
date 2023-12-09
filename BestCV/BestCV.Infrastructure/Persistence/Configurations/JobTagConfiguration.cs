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
    public class JobTagConfiguration: IEntityTypeConfiguration<JobTag>
    {
        public void Configure(EntityTypeBuilder<JobTag> builder)
        {
            builder.ToTable(nameof(JobTag));
            builder.HasKey(x => x.Id);

            builder.Property(e => e.Id)
               .ValueGeneratedOnAdd()
               .UseIdentityColumn(1001, 1);

            builder.Property(e => e.JobId)
                .IsRequired()
                .HasComment("Mã tin tuyển dụng");

            builder.Property(e => e.TagId)
                .IsRequired()
                .HasComment("Mã thẻ");

            builder.Property(e => e.Active)
                .IsRequired()
                .HasDefaultValueSql("((1))")
                .HasComment("Đánh dấu bị xóa");

            builder.Property(e => e.CreatedTime)
                .HasDefaultValueSql("(getdate())")
                .HasComment("Ngày tạo");

            builder.HasOne(e => e.Job)
                .WithMany(e => e.JobTags)
                .HasForeignKey(e => e.JobId)
                .HasConstraintName("FK_JobTag_Job")
                .OnDelete(DeleteBehavior.ClientSetNull);

            builder.HasOne(e => e.Tag)
                .WithMany(e => e.JobTags)
                .HasForeignKey(e => e.TagId)
                .HasConstraintName("FK_JobTag_Tag")
                .OnDelete(DeleteBehavior.ClientSetNull);
        }
    }
}
