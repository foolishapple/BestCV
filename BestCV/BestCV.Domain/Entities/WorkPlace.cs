using BestCV.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BestCV.Domain.Entities
{
    public class WorkPlace : EntityCommon<int>
    {
        public int? ParentId { get; set; }
        public int Code { get; set; }

        public string DivisionType { get; set; } = null!;

        public string CodeName { get; set; } = null!;

        public int? PhoneCode { get; set; }

        public int? ProvinceCode { get; set; }

        public virtual ICollection<RecruitmentCampaignRequireWorkPlace> CampaignRequireWorkPlaces { get; } = new List<RecruitmentCampaignRequireWorkPlace>();
        public virtual ICollection<CandidateSuggestionWorkPlace> CandidateSuggestionWorkPlaces { get; } = new List<CandidateSuggestionWorkPlace>();
        public virtual ICollection<JobRequireCity> JobRequireCities { get; } = new List<JobRequireCity>();
        public virtual ICollection<Company> Companies { get; } = new List<Company>();
    }
}