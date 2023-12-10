using BestCV.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BestCV.Application.Models.CandidateCVPDF
{
    public class CandidateCVPDFDTO : EntityBase<long>
    {
        public long CandidateId { get; set; }
        public string Url { get; set; } = null!;
        public int CandidateCVPDFTypeId { get; set; }
    }
}
