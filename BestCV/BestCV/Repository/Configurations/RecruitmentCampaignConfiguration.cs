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
    public class RecruitmentCampaignConfiguration : IEntityTypeConfiguration<RecruitmentCampaign>
    {
        public void Configure(EntityTypeBuilder<RecruitmentCampaign> builder)
        {
            builder.ToTable("RecruitmentCampaign");
            builder.HasKey(e => e.Id);

            builder.Property(e => e.Id)
                .ValueGeneratedOnAdd()
                .UseIdentityColumn(1001, 1);

            builder.Property(e => e.RecruitmentCampaignStatusId)
                .IsRequired()
                .HasComment("Mã trạng thái chiến dịch tuyển dụng");

            builder.Property(e=> e.EmployerId)
                .IsRequired()
                .HasComment("Mã nhà tuyển dụng tạo chiến dịch");

            builder.Property(e => e.Name)
                .HasMaxLength(255)
                .HasComment("Tên chiến dịch tuyển dụng");

            builder.Property(e => e.IsAprroved)
                .IsRequired()
                .HasDefaultValueSql("((0))")
                .HasComment("Chiến dịch tuyển dụng có được duyệt không");

            builder.Property(e => e.ApprovalDate)
                .HasComment("Thời điểm duyệt");

            builder.Property(e => e.Active)
                .IsRequired()
                .HasDefaultValueSql("((1))")
                .HasComment("Đánh dấu bị xóa");

            builder.Property(e => e.CreatedTime)
                .HasDefaultValueSql("(getdate())")
                .HasComment("Ngày tạo");

            builder.HasIndex(x => x.Name).IsUnique();

            builder.HasOne(x => x.RecruitmentCampaignStatus).WithMany(x => x.RecruitmentCampaigns)
                .HasForeignKey(x => x.RecruitmentCampaignStatusId)
                .HasConstraintName("FK_RecruitmentCampaign_RecruitmentCampaignStatus")
                .OnDelete(DeleteBehavior.ClientSetNull);

            builder.HasOne(x => x.Employer)
                .WithMany(r => r.RecruitmentCampaigns)
                .HasForeignKey(x => x.EmployerId)
                .HasConstraintName("FK_RecruitmentCampaigns_Employer")
                .OnDelete(DeleteBehavior.ClientSetNull);
        }
    }
}
