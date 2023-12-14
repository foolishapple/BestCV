using BestCV.Core.Entities;
using BestCV.Core.Entities.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BestCV.Domain.Entities
{
    public class RecruitmentCampaignRequireJobPosition : EntityBase<long>
    {
        /// <summary>
        /// Mã vị trí công việc
        /// </summary>
        public int JobPositionId { get; set; }

        /// <summary>
        /// Mã chiến dịch tuyển dụng
        /// </summary>
        public long RecruitmentCampaignId { get; set; }

        public virtual JobPosition JobPosition { get; } = null!;
        public virtual RecruitmentCampaign RecruitmentCampaign { get; } = null!;
    }
}
