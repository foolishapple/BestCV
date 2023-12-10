using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BestCV.Application.Models.CandidateSaveJob
{
    public class UpdateCandidateSaveJobDTO : InsertCandidateSaveJobDTO
    {
        public long Id { get; set; }
    }
}
