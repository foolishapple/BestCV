using BestCV.Application.Models.CandidateCVPDFTypes;
using BestCV.Application.Models.CandidateLevel;
using BestCV.Core.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BestCV.Application.Services.Interfaces
{
    public interface ICandidateCVPDFTypesService : IServiceQueryBase<int, InsertCandidateCVPDFTypesDTO, UpdateCandidateCVPDFTypesDTO, CandidateCVPDFTypesDTO>
    {
    }
}
