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
    public class RecruitmentCampaignRequireWorkPlaceConfiguration : IEntityTypeConfiguration<RecruitmentCampaignRequireWorkPlace>
    {
        public void Configure(EntityTypeBuilder<RecruitmentCampaignRequireWorkPlace> builder)
        {
            builder.ToTable("RecruitmentCampaignRequireWorkPlace");
            builder.HasKey(e => e.Id);

            builder.Property(e => e.Id)
                .ValueGeneratedOnAdd()
                .UseIdentityColumn(1001, 1);

            builder.Property(e => e.WorkPlaceId)
                .IsRequired()
                .HasComment("Mã địa điểm làm việc");

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

            builder.HasOne(x=> x.WorkPlace).WithMany(x=>x.CampaignRequireWorkPlaces)
                .HasForeignKey(x=> x.WorkPlaceId)
                .OnDelete(DeleteBehavior.ClientSetNull);

            builder.HasOne(x => x.RecruitmentCampaign).WithMany(x => x.RecruitmentCampaignRequireWorkPlaces)
                .HasForeignKey(x => x.RecruitmentCampaignId)
                .OnDelete(DeleteBehavior.ClientSetNull);
        }
    }
}
