using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BestCV.Application.Models.Candidates
{
    public class SettingNotiEmailDTO
    {
        public long Id { get; set; } = 0;
        /// <summary>
        /// Nhận thông báo cập nhật quan trọng từ hệ thống
        /// </summary>
        public bool IsSubcribeEmailImportantSystemUpdate { get; set; }
        /// <summary>
        /// Cho nhà tuyển dụng view CV
        /// </summary>
        public bool IsSubcribeEmailEmployerViewCV { get; set; }
        /// <summary>
        /// Nhận thông báo email về cập nhật tính năng mới
        /// </summary>
        public bool IsSubcribeEmailNewFeatureUpdate { get; set; }
        /// <summary>
        /// Nhận thông báo qua email về hệ thống khác
        /// </summary>
        public bool IsSubcribeEmailOtherSystemNotification { get; set; }
        /// <summary>
        /// Nhận thông báo qua email về gợi ý công việc
        /// </summary>
        public bool IsSubcribeEmailJobSuggestion { get; set; }
        /// <summary>
        /// Nhận thông báo qua email về nhà tuyển dụng mời làm việc
        /// </summary>
        public bool IsSubcribeEmailEmployerInviteJob { get; set; }
        /// <summary>
        /// Nhận thông báo qua email giới thiệu về dịch vụ
        /// </summary>
        public bool IsSubcribeEmailServiceIntro { get; set; }
        /// <summary>
        /// Nhận thông báo qua email về chương trình sự kiện giới thiệu
        /// </summary>
        public bool IsSubcribeEmailProgramEventIntro { get; set; }
        /// <summary>
        /// Nhận thông báo qua email về gift phiếu giảm giá
        /// </summary>
        public bool IsSubcribeEmailGiftCoupon { get; set; }
    }
}
