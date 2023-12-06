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
    public class RecruitmentCampaignStatusConfiguration : IEntityTypeConfiguration<RecruitmentCampaignStatus>
    {
        public void Configure(EntityTypeBuilder<RecruitmentCampaignStatus> builder)
        {
            builder.ToTable("RecruitmentCampaignStatus");

            builder.HasKey(t => t.Id);

            builder.Property(e => e.Id)
                .HasComment("Mã tình trạng chiến dịch tuyển dụng")
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
                .HasComment("Tên tình trạng chiến dịch tuyển dụng");
            builder.Property(e => e.Color)
               .HasColumnType("varchar(12)")
               .HasComment("Màu");
            //Constrair
            //builder.HasIndex(e => e.Color).IsUnique();
        }
    }
}
