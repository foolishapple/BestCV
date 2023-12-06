using Jobi.Core.Repositories;
using Jobi.Core.Utilities;
using Jobi.Domain.Aggregates.CandidateApplyJobs;
using Jobi.Domain.Aggregates.CandidateSaveJob;
using Jobi.Domain.Entities;
using Jobi.Infrastructure.Persistence;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Jobi.Infrastructure.Repositories.Interfaces
{
    public interface ICandidateSaveJobRepository : IRepositoryBaseAsync<CandidateSaveJob, long, JobiContext>
    {
        Task<bool> IsJobIdExist(long accountId, long jobId);
        Task<CandidateSaveJob> GetByCandidateIdIdAndJobIdAsync(long candidateId, long jobId);
        Task<List<CandidateSaveJobAggregates>> ListCandidateSaveJobByCandidateId(long candidateId);
        Task<DTResult<CandidateSaveJobAggregates>> PagingByCandidateId(DTPagingCandidateSaveJobParameters parameters);
    }
}
