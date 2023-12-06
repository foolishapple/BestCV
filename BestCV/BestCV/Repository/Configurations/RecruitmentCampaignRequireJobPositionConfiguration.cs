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
    public class RecruitmentCampaignRequireJobPositionConfiguration : IEntityTypeConfiguration<RecruitmentCampaignRequireJobPosition>
    {
        public void Configure(EntityTypeBuilder<RecruitmentCampaignRequireJobPosition> builder)
        {
            builder.ToTable("RecruitmentCampaignRequireJobPosition");
            builder.HasKey(e => e.Id);

            builder.Property(e => e.Id)
                .ValueGeneratedOnAdd()
                .UseIdentityColumn(1001, 1);

            builder.Property(e => e.JobPositionId)
                .IsRequired()
                .HasComment("Mã vị trí công việc");

            builder.Property(e => e.RecruitmentCampaignId)
                .IsRequired()
                .HasComment("Mã chiến dịch tuyển dụng");

            builder.Property(e => e.Active)
                .IsRequired()
                .HasDefaultValueSql("((1))")
                .HasComment("Đánh dấu bị xóa");

            builder.Property(e => e.CreatedTime)
                .HasDefaultValueSql("(getdate())")
                .HasComment("Ngày tạo");

            builder.HasOne(x=> x.JobPosition).WithMany(x=> x.RecruitmentCampaignRequireJobPositions)
                .HasForeignKey(x=> x.JobPositionId)
                .OnDelete(DeleteBehavior.ClientSetNull);

            builder.HasOne(x => x.RecruitmentCampaign).WithMany(x => x.RecruitmentCampaignRequireJobPositions)
                .HasForeignKey(x => x.RecruitmentCampaignId)
                .OnDelete(DeleteBehavior.ClientSetNull);
        }
    }
}
