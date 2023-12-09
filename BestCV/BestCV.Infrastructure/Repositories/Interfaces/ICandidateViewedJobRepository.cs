using BestCV.Core.Repositories;
using BestCV.Core.Utilities;
using BestCV.Domain.Aggregates.CandidateViewedJob;
using BestCV.Domain.Aggregates.CandidateViewJobs;
using BestCV.Domain.Entities;
using BestCV.Infrastructure.Persistence;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BestCV.Infrastructure.Repositories.Interfaces
{
    public interface ICandidateViewedJobRepository : IRepositoryBaseAsync<CandidateViewedJob, long, JobiContext>
    {
        Task<List<CandidateViewedJobAggregates>> ListCandidateViewedJobByCandidateId(long candidateId);
        Task<DTResult<CandidateViewedJobAggregates>> PagingByCandidateId(DTPagingCandidateViewedJobParameters parameters);

        /// Description: datatable paging candidate viewed job parameter
        /// </summary>
        /// <param name="parameters"></param>
        /// <returns></returns>
        Task<DTResult<CandidateViewedJobAggreagate>> DTPaging(DTCandidateViewedJobParameters parameters);
    }
}
