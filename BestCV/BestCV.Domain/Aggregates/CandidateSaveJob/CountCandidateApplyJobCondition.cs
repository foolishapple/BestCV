using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BestCV.Domain.Aggregates.CandidateSaveJob
{
    public class CountCandidateApplyJobCondition
    {
        public List<long> RecruitmentCampaginIds { get; set; } =  new();

        public List<long> JobIds { get; set; } = new();

        public List<int> CandidateApplyJobSourceIds { get; set; } = new();
    }
}
