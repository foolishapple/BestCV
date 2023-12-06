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
    public class JobRequireCityConfiguration : IEntityTypeConfiguration<JobRequireCity>
    {
        public void Configure(EntityTypeBuilder<JobRequireCity> builder)
        {
            builder.ToTable("JobRequireCity");
            builder.HasKey(e => e.Id);

            builder.Property(e => e.Id)
                .ValueGeneratedOnAdd()
                .UseIdentityColumn(1001, 1);

            builder.Property(e => e.JobId)
                .IsRequired()
                .HasComment("Mã tin tuyển dụng");

            builder.Property(e => e.CityId)
                .IsRequired()
                .HasComment("Mã thành phố");

            builder.Property(e => e.Active)
                .IsRequired()
                .HasDefaultValueSql("((1))")
                .HasComment("Đánh dấu bị xóa");

            builder.Property(e => e.CreatedTime)
                .HasDefaultValueSql("(getdate())")
                .HasComment("Ngày tạo");
            builder.HasOne(d => d.Job).WithMany(p => p.JobRequireCities)
           .HasForeignKey(d => d.JobId)
           .OnDelete(DeleteBehavior.ClientSetNull)
           .HasConstraintName("FK_JobRequireCity_Job");
           builder.HasOne(c=>c.WorkPlace).WithMany(c=>c.JobRequireCities).OnDelete(DeleteBehavior.ClientSetNull).HasForeignKey(c=>c.CityId).HasConstraintName("FK_JobRequireCity_WorkPlace");
        }
    }
}
