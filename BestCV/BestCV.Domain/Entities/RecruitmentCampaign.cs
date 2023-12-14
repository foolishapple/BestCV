using BestCV.Core.Entities;
using BestCV.Core.Entities.Interfaces;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BestCV.Domain.Entities
{
    public class RecruitmentCampaign : EntityCommon<long>, IFullTextSearch
    {
        /// <summary>
        /// Mã trạng thái chiến dịch tuyển dụng
        /// </summary>
        public int RecruitmentCampaignStatusId { get; set; }

        /// <summary>
        /// mã nhà tuyển dụng
        /// </summary>
        public long? EmployerId { get; set; }

        /// <summary>
        /// Chiến dịch tuyển dụng đã được duyệt chưa
        /// </summary>
        public bool IsAprroved { get; set; }

        /// <summary>
        /// Thời điểm duyệt chiến dịch tuyển dụng
        /// </summary>
        public DateTime? ApprovalDate { get; set; }

        /// <summary>
        /// Tìm kiếm
        /// </summary>
        public string Search { get; set; } = null!;
        [JsonIgnore]
        public virtual RecruitmentCampaignStatus RecruitmentCampaignStatus { get; } = null!;
        [JsonIgnore]
        public virtual ICollection<RecruitmentCampaignRequireWorkPlace> RecruitmentCampaignRequireWorkPlaces { get; } = new List<RecruitmentCampaignRequireWorkPlace>();
        [JsonIgnore]
        public virtual ICollection<RecruitmentCampaignRequireJobPosition> RecruitmentCampaignRequireJobPositions { get; } = new  List<RecruitmentCampaignRequireJobPosition>();
        [JsonIgnore]
        public virtual ICollection<Job> Jobs { get; } = new List<Job>();
        [JsonIgnore]
        public virtual Employer? Employer { get; } = null!;
    }
}
