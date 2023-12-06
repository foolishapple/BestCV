using Jobi.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Jobi.Domain.Entities
{
    public class JobPosition : EntityCommon<int>
    {
        public virtual ICollection<RecruitmentCampaignRequireJobPosition> RecruitmentCampaignRequireJobPositions { get;} = new  List<RecruitmentCampaignRequireJobPosition>();
        public virtual ICollection<Job> Jobs { get; } = new List<Job>();
        public ICollection<CandidateSuggestionJobPosition> CandidateSuggestionJobPositions { get; } = new List<CandidateSuggestionJobPosition>();
    }
}
