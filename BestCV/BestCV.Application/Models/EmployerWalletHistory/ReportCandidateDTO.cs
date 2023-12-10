using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BestCV.Application.Models.EmployerWalletHistory
{
    public class ReportCandidateDTO
    {
        public long CandidateId { get; set; }
        public string Description { get; set; }
        public long? EmployerId { get; set; }
    }
}
