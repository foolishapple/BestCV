using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BestCV.Domain.Aggregates.Candidate
{
    public class FindCandidateParameters : CandidateDTParameters
    {
        public long? EmployerId { get; set; }
        public string FilterCV { get; set; }
        public string SearchAll { get; set; }
    }
}
