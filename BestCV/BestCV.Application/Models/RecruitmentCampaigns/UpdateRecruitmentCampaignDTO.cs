using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BestCV.Application.Models.RecruitmentCampaigns
{
    public class UpdateRecruitmentCampaignDTO
    {
        /// <summary>
        /// Mã chiến dịch
        /// </summary>
        public long Id { get; set; }
        /// <summary>
        /// Tên
        /// </summary>
        public string Name { get; set; } = null!;
        /// <summary>
        /// Mổ tả
        /// </summary>
        public string? Description { get; set; }
        /// <summary>
        /// Mã trạng thái chiến dịch tuyển dụng
        /// </summary>
        public int RecruitmentCampaignStatusId { get; set; }
    }
}
