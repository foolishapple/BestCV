using Jobi.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Jobi.Domain.Entities
{
    public class CandidateApplyJobSource:EntityCommon<int>
    {
        public ICollection<CandidateApplyJob> CandidateApplyJobs { get; } = new List<CandidateApplyJob>();
    }
}
