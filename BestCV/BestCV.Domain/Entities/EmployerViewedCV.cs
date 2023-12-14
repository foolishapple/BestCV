using BestCV.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BestCV.Domain.Entities
{
    public class EmployerViewedCV : EntityBase<long>
    {
        public long CandidateId { get; set; }
        public long EmployerId { get; set; }

        public virtual Candidate Candidate { get; set; } = null!;
        public virtual Employer Employer { get; set; } = null!;
    }
}
