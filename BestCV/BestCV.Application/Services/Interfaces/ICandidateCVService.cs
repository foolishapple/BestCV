using BestCV.Core.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BestCV.Domain.Entities;
using BestCV.Core.Entities;
using BestCV.Application.Models.CandidateCVs;
using BestCV.Domain.Aggregates.CandidateCVs;

namespace BestCV.Application.Services.Interfaces
{
    public interface ICandidateCVService : IServiceQueryBase<long, InsertOrUpdateCandidateCVDTO, InsertOrUpdateCandidateCVDTO, CandidateCV>
    {
        Task<BestCVResponse> GetListAsyncByCandidateId(long candidateId);

        Task<BestCVResponse> GetListAggregateAsyncByCandidateId(long candidateId);
    }
}
