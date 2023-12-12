using Jobi.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Jobi.Domain.Entities
{
    public class CandidateApplyJobStatus : EntityCommon<int>
    {
        public string Color { get; set; } = null!;

        public ICollection<CandidateApplyJob> CandidateApplyJobs { get; } = new List<CandidateApplyJob>();
    }
}
