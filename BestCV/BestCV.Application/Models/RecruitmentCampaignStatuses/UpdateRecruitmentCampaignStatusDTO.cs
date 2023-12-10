using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BestCV.Application.Models.RecruitmentCampaignStatuses
{
    public class UpdateRecruitmentCampaignStatusDTO : InsertRecruitmentCampaignStatusDTO
    {
        /// <summary>
        /// Mã tình trạng chiến dịch tuyển dụng
        /// </summary>
        public int Id { get; set; }
    }
}
