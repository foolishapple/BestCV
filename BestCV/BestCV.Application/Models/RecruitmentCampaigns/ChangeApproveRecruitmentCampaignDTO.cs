using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BestCV.Application.Models.RecruitmentCampaigns
{
    public class ChangeApproveRecruitmentCampaignDTO
    {
        /// <summary>
        /// Mã chiến dịch
        /// </summary>
        public long Id { get; set; }
        /// <summary>
        /// Chiến dịch đã được duyệt hay chưa
        /// </summary>
        public bool IsApproved { get; set; }
    }
}
