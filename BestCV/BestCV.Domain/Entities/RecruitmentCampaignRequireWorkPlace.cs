using BestCV.Core.Entities;
using BestCV.Core.Entities.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BestCV.Domain.Entities
{
    public class RecruitmentCampaignRequireWorkPlace : EntityBase<long>
    {
        /// <summary>
        /// Mã địa điểm làm việc
        /// </summary>
        public int WorkPlaceId { get; set; }

        /// <summary>
        /// Mã chiến dịch tuyển dụng
        /// </summary>
        public long RecruitmentCampaignId { get; set; }

        public virtual RecruitmentCampaign RecruitmentCampaign { get; } = null!;
        public virtual WorkPlace WorkPlace { get; } = null!;
    }
}
