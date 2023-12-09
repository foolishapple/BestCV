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
    public class JobMultimediaConfiguration : IEntityTypeConfiguration<JobMultimedia>
    {
        public void Configure(EntityTypeBuilder<JobMultimedia> builder)
        {
            builder.ToTable("JobMultimedia");
            builder.HasKey(e => e.Id);

            builder.Property(e => e.Id)
                .ValueGeneratedOnAdd()
                .UseIdentityColumn(1001, 1);

            builder.Property(e => e.JobId)
                .IsRequired()
                .HasComment("Mã tin tuyển dụng");

            builder.Property(e => e.Path)
                .IsRequired()
                .HasComment("Mã tin tuyển dụng");

            builder.Property(e => e.MultimediaTypeId)
                .IsRequired()
                .HasComment("Mã loại tệp tin đa phương tiện");

            builder.Property(e => e.Active)
                .IsRequired()
                .HasDefaultValueSql("((1))")
                .HasComment("Đánh dấu bị xóa");

            builder.Property(e => e.CreatedTime)
                .HasDefaultValueSql("(getdate())")
                .HasComment("Ngày tạo");
            builder.HasOne(d => d.Job).WithMany(p => p.JobMultimedias)
              .HasForeignKey(d => d.JobId)
              .OnDelete(DeleteBehavior.ClientSetNull)
              .HasConstraintName("FK_JobMultimedia_Job");
            builder.HasOne(d => d.MultimediaType).WithMany(p => p.JobMultimedias)
            .HasForeignKey(d => d.MultimediaTypeId)
            .OnDelete(DeleteBehavior.ClientSetNull)
            .HasConstraintName("FK_JobMultimedia_MultimediaType");
        }
    }
}
