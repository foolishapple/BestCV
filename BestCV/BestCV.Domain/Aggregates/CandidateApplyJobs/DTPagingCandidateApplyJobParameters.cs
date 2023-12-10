using BestCV.Core.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BestCV.Domain.Aggregates.CandidateApplyJobs
{
    public class DTPagingCandidateApplyJobParameters : DTParameters
    {
        public long? EmployerId { get; set; }

        public bool IsViewUnread { get; set; }

        public int? CandidateApplyJobSourceId { get; set; }

        public long? RecruitmentCampaignId { get; set; }
        public long? JobId  { get; set; }
        public int[] CandidateApplyJobStatusIds { get; set; } = new int [] {};
        public int[] CandidateApplyJobSourceIds { get; set; } = new int[] { };
        public long[] RecruitmentCampaignIds { get; set; } = new long[] { };
        public long? CandidateId { get; set; }
    }
}
