using Jobi.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Jobi.Domain.Entities
{
    public class CandidateLevelBenefit:EntityCommon<int>
    {
        public ICollection<CandidateLevelCandidateLevelBenefit> CandidateLevelCandidateLevelBenefits { get; } = new List<CandidateLevelCandidateLevelBenefit>();
    }
}
