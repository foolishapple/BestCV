using BestCV.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BestCV.Domain.Entities
{
    public class CandidateViewedJob:EntityBase<long>
    {

        public long CandidateId { get; set; }

        public long JobId { get; set; }

        public virtual Candidate Candidate { get; } = null!;
        public virtual Job Job { get;  } = null!;
    }
}
