using BestCV.Application.Models.CandidateCVPDF;
using BestCV.Application.Models.CandidateCVPDFTypes;
using BestCV.Core.Entities;
using BestCV.Core.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BestCV.Application.Services.Interfaces
{
    public interface ICandidateCVPDFService : IServiceQueryBase<long, InsertCandidateCVPDFDTO, UpdateCandidateCVPDFDTO, CandidateCVPDFDTO>
    {
        Task<DionResponse> GetByCandidateId(long candidateId);
        Task<DionResponse> UploadCV(UploadCandidateCVPDFDTO model);
    }
}
