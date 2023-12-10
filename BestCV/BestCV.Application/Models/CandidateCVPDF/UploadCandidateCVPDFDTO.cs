using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BestCV.Application.Models.CandidateCVPDF
{
    public class UploadCandidateCVPDFDTO
    {
        public string Url { get; set; }
        public int CandidateCVPDFTypeId { get; set; }
        public long CandidateId { get; set; }
    }
}
