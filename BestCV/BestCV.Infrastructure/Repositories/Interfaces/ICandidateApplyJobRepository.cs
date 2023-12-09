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
    public interface ICandidateApplyJobRepository : IRepositoryBaseAsync<CandidateApplyJob,long,JobiContext>
    {

        /// Description: Datatables pagging candidate apply job
        /// </summary>
        /// <param name="parameters">Paging parameters</param>
        /// <returns></returns>
        Task<DTResult<CandidateApplyJobAggregate>> DTPaging(DTPagingCandidateApplyJobParameters parameters);
        Task<bool> IsJobIdExist(long accountId, long jobId);

        /// Description: count candidate apply job by condition
        /// <param name="condition"></param>
        /// <returns></returns>
        Task<int> CountByCondition(CountCandidateApplyJobCondition condition);

        /// Description : Paging DT in CandidateManageJob
        /// <param name="parameters">DTPagingCandidateApplyJobParameters</param>
        /// <returns></returns>
        Task<DTResult<CandidateApplyJobAggregate>> PagingByCandidateId(DTPagingCandidateApplyJobParameters parameters);
        Task<CandidateApplyJobAggregate> DetailById(long id);
        Task<List<CandidateApplyJobAggregate>> GetListCandidateApplyJobCompare(long jobId, long candidateApplyJobId);
        Task<CandidateApplyJobAggregate> DetailById(long jobId, long candidateApplyJobId);
    }
}
