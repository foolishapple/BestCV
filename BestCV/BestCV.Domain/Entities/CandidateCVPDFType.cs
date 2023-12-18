using BestCV.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BestCV.Domain.Entities
{
    public class CandidateCVPDFType : EntityCommon<int>
    {
        public virtual ICollection<CandidateCVPDF> CandidateCVPDFs { get; } = new List<CandidateCVPDF>();
    }
}
