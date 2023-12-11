using BestCV.Application.Models.CandidateViewedJob;
using BestCV.Core.Entities;
using BestCV.Core.Services;
using BestCV.Core.Utilities;
using BestCV.Domain.Aggregates.CandidateViewedJob;
using BestCV.Domain.Aggregates.CandidateViewJobs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BestCV.Application.Services.Interfaces
{
    public interface ICandidateViewedJobService : IServiceQueryBase<long, InsertCandidateViewedJobDTO, UpdateCandidateViewedJobDTO, CandidateViewedJobDTO>
    {
        Task<DionResponse> GetListViewedJobByCanddiateId(long candidateId);

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
