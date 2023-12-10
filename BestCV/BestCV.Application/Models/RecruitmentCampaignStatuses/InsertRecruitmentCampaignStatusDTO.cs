using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BestCV.Application.Models.RecruitmentCampaignStatuses
{
    public class InsertRecruitmentCampaignStatusDTO
    {
        /// <summary>
        /// Tên
        /// </summary>
        public string Name { get; set; } = null!;
        /// <summary>
        /// Mô tả
        /// </summary>
        public string? Description { get; set; }
        /// <summary>
        /// Màu
        /// </summary>
        public string Color { get; set; } = null!;
    }
}
