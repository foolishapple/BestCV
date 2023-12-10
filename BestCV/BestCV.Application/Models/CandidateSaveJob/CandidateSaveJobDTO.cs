using BestCV.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BestCV.Application.Models.CandidateSaveJob
{
    public class CandidateSaveJobDTO  : EntityBase<long>
    {
        public long CandidateId { get; set; }
        public long JobId { get; set; }
    }
}
