using BestCV.Domain.Constants;
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
    public class CandidateConfiguration : IEntityTypeConfiguration<Candidate>
    {
        public void Configure(EntityTypeBuilder<Candidate> builder)
        {
            builder.ToTable("Candidate");
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id)
                .ValueGeneratedOnAdd()
                .UseIdentityColumn(1001, 1)
                .HasComment("Mã ứng viên");
            builder.Property(e => e.Username)
                .HasMaxLength(255)
                .HasComment("Tên đăng nhập");
            builder.Property(e => e.FullName)
                .HasMaxLength(255)
                .HasComment("Tên đầy đủ");
            builder.Property(e => e.Email)
                .HasMaxLength(255)
                .HasComment("Địa chỉ email");
            builder.Property(e => e.FirstName)
                .HasMaxLength(255)
                .HasComment("Họ");
            builder.Property(e => e.MiddleName)
                .HasMaxLength(255)
                .HasComment("Tên đệm");
            builder.Property(e => e.LastName)
                .HasMaxLength(255)
                .HasComment("Tên");
            builder.Property(e => e.Password)
                .HasMaxLength(500)
                .HasComment("Mật khẩu đăng nhập");
            builder.Property(e => e.GoogleId)
                .HasMaxLength(255)
                .HasComment("Mã google");
            builder.Property(e => e.FacebookId)
                .HasMaxLength(255)
                .HasComment("Mã fb");
            builder.Property(e => e.LinkedinId)
                .HasMaxLength(255)
                .HasComment("Mã linked");
            builder.Property(e => e.Photo)
                .HasMaxLength(500)
                .HasComment("Ảnh đại diện");
            builder.Property(e => e.CoverPhoto)
                .HasMaxLength(500)
                .HasComment("Ảnh bìa");
            builder.Property(e => e.Gender)
                .HasDefaultValueSql("((3))")
                .HasComment("Giới tính");
            builder.Property(e => e.JobPosition)
                .HasMaxLength(255)
                .HasComment("Vị trí công việc");
            builder.Property(e => e.AddressDetail)
                .HasMaxLength(500)
                .HasComment("Địa chỉ cụ thể");
            builder.Property(e => e.Interests)
                .HasMaxLength(500)
                .HasComment("Sở thích");
            builder.Property(e => e.Objective)
                .HasMaxLength(500)
                .HasComment("Mục tiêu");
            builder.Property(e => e.Info)
                .HasMaxLength(500)
                .HasComment("Thông tin");
            builder.Property(e => e.DoB).HasComment("Ngày sinh");
            builder.Property(e => e.Phone)
                .HasMaxLength(10)
                .HasComment("Số điện thoại");
            builder.Property(e => e.Nationality)
                .HasMaxLength(255)
                .HasComment("Quốc tịch");
            builder.Property(e => e.MaritalStatus)
                .HasMaxLength(255)
                .HasComment("Tình trạng hôn nhân");
            builder.Property(e => e.References)
                .HasMaxLength(1000)
                .HasComment("Người tham khảo");
            builder.Property(e => e.IsSubcribeEmailImportantSystemUpdate).HasComment("Nhận thông báo quan trọng về cập nhật hệ thông");
            builder.Property(e => e.IsSubcribeEmailEmployerViewCV).HasComment("Nhận thông báo email về nhà tuyển dụng đã xem sơ yếu lí lịch ");
            builder.Property(e => e.IsSubcribeEmailNewFeatureUpdate).HasComment("Nhận thông báo email về cập nhật tính năng mới");
            builder.Property(e => e.IsSubcribeEmailOtherSystemNotification).HasComment("Nhận thông báo qua email về hệ thống khác");
            builder.Property(e => e.IsSubcribeEmailJobSuggestion).HasComment("Nhận thông báo qua email về gợi ý công việc");
            builder.Property(e => e.IsSubcribeEmailEmployerInviteJob).HasComment("Nhận thông báo qua email về nhà tuyển dụng mời làm việc");
            builder.Property(e => e.IsSubcribeEmailServiceIntro).HasComment("Nhận thông báo qua email giới thiệu về dịch vụ");
            builder.Property(e => e.IsSubcribeEmailProgramEventIntro).HasComment("Nhận thông báo qua email về chương trình sự kiện giới thiệu");
            builder.Property(e => e.IsSubcribeEmailGiftCoupon).HasComment("Nhận thông báo qua email về gift phiếu giảm giá");
            builder.Property(e => e.IsCheckOnJobWatting).HasComment("Kiểm tra công việc đang chờ");
            builder.Property(e => e.IsCheckJobOffers).HasComment("Kiểm tra lời mời làm việc");
            builder.Property(e => e.IsCheckViewCV).HasComment("Kiểm tra xem sơ yếu lí lịch");
            builder.Property(e => e.IsCheckTopCVReview).HasComment("Kiểm tra đánh giá TopCV");
            builder.Property(e => e.SuggestionExperienceRangeId).HasComment("Kinh nghiệm làm việc");
            builder.Property(e => e.SuggestionSalaryRangeId).HasComment("Mức lương");
            builder.Property(e => e.AccessFailedCount).HasComment("Số lần đăng nhập thất bại");
            builder.Property(e => e.LockEnabled).HasComment("Bị khoá tài khoản?");
            builder.Property(e => e.LockEndDate).HasComment("Bị khóa đến thời gian nào");
            builder.Property(e => e.CandidateLevelEfficiencyExpiry).HasComment("Hiệu lực của cấp độ ứng viên");
            builder.Property(e => e.IsActivated).HasComment("Đã xác thực chưa");
            builder.Property(e => e.Search).HasComment("Lưu ký tự không dấu của các trường muốn tìm kiếm");
            builder.Property(e => e.Active)
                .IsRequired()
                .HasDefaultValueSql("((1))")
                .HasComment("Đánh dấu bị xóa");
            builder.Property(e => e.CreatedTime)
               .HasDefaultValueSql("(getdate())")
               .HasComment("Ngày tạo");

            builder.HasOne(d => d.CandidateStatuses).WithMany(p => p.Candidates)
                .HasForeignKey(d => d.CandidateStatusId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Candidate_CandidateStatus");

            builder.HasOne(d => d.CandidateLevels).WithMany(p => p.Candidates)
                .HasForeignKey(d => d.CandidateLevelId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Candidate_CandidateLevel");
        }
    }
}
