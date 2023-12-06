using Jobi.Core.Repositories;
using Jobi.Core.Utilities;
using Jobi.Domain.Aggregates.CandidateViewedJob;
using Jobi.Domain.Aggregates.CandidateViewJobs;
using Jobi.Domain.Entities;
using Jobi.Infrastructure.Persistence;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Jobi.Infrastructure.Repositories.Interfaces
{
    public interface ICandidateViewedJobRepository : IRepositoryBaseAsync<CandidateViewedJob, long, JobiContext>
    {
        Task<List<CandidateViewedJobAggregates>> ListCandidateViewedJobByCandidateId(long candidateId);
        Task<DTResult<CandidateViewedJobAggregates>> PagingByCandidateId(DTPagingCandidateViewedJobParameters parameters);
        /// <summary>
        /// Author: TUNGTD
        /// Created: 13/09/2023
        /// Description: datatable paging candidate viewed job parameter
        /// </summary>
        /// <param name="parameters"></param>
        /// <returns></returns>
        Task<DTResult<CandidateViewedJobAggreagate>> DTPaging(DTCandidateViewedJobParameters parameters);
    }
}
