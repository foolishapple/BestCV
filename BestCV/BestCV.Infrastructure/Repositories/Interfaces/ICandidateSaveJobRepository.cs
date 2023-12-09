using BestCV.Core.Repositories;
using BestCV.Core.Utilities;
using BestCV.Domain.Aggregates.CandidateApplyJobs;
using BestCV.Domain.Aggregates.CandidateSaveJob;
using BestCV.Domain.Entities;
using BestCV.Infrastructure.Persistence;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BestCV.Infrastructure.Repositories.Interfaces
{
    public interface ICandidateSaveJobRepository : IRepositoryBaseAsync<CandidateSaveJob, long, JobiContext>
    {
        Task<bool> IsJobIdExist(long accountId, long jobId);
        Task<CandidateSaveJob> GetByCandidateIdIdAndJobIdAsync(long candidateId, long jobId);
        Task<List<CandidateSaveJobAggregates>> ListCandidateSaveJobByCandidateId(long candidateId);
        Task<DTResult<CandidateSaveJobAggregates>> PagingByCandidateId(DTPagingCandidateSaveJobParameters parameters);
    }
}
