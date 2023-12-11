using BestCV.Application.Models.AccountStatus;
using BestCV.Application.Models.CandidateSaveJob;
using BestCV.Core.Entities;
using BestCV.Core.Services;
using BestCV.Core.Utilities;
using BestCV.Domain.Aggregates.CandidateSaveJob;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BestCV.Application.Services.Interfaces
{
    public interface ICandidateSaveJobService : IServiceQueryBase<long, InsertCandidateSaveJobDTO, UpdateCandidateSaveJobDTO, CandidateSaveJobDTO>
    {
        Task<DionResponse> QuickSaveJob(long id, long accountId);
        Task<DionResponse> GetListJobByCandidateId(long candidateId);
        Task<DTResult<CandidateSaveJobAggregates>> PagingByCandidateId(DTPagingCandidateSaveJobParameters parameters);
    }
}
