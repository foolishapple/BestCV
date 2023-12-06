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
    public class CandidateApplyJobStatusConfiguration : IEntityTypeConfiguration<CandidateApplyJobStatus>
    {
        public void Configure(EntityTypeBuilder<CandidateApplyJobStatus> builder)
        {
            builder.ToTable("CandidateApplyJobStatus");

            builder.HasKey(t => t.Id);

            builder.Property(e => e.Id)
                .HasComment("Mã tình trạng ứng viên ứng tuyển công việc")
                .ValueGeneratedOnAdd()
                .UseIdentityColumn(1001, 1);
            builder.Property(e => e.Active)
                .IsRequired()
                .HasDefaultValueSql("((1))")
                .HasComment("Đánh dấu bị xóa");
            builder.Property(e => e.CreatedTime)
                .HasDefaultValueSql("(getdate())")
                .HasComment("Ngày tạo");
            builder.Property(e => e.Description).HasComment("Mô tả");
            builder.Property(e => e.Name)
                .HasMaxLength(255)
                .HasComment("Tên tình trạng ứng viên ứng tuyển công việc");
            builder.Property(c => c.Color)
                .HasColumnType("varchar(12)")
                .HasComment("Màu");
            //builder.HasIndex(c => c.Color).IsUnique();
        }
    }
}
