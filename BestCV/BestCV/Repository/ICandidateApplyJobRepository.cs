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
    public interface ICandidateApplyJobRepository : IRepositoryBaseAsync<CandidateApplyJob,long,JobiContext>
    {
        /// <summary>
        /// Author: TUNGTD
        /// Created: 15/08/2023
        /// Description: Datatables pagging candidate apply job
        /// </summary>
        /// <param name="parameters">Paging parameters</param>
        /// <returns></returns>
        Task<DTResult<CandidateApplyJobAggregate>> DTPaging(DTPagingCandidateApplyJobParameters parameters);
        Task<bool> IsJobIdExist(long accountId, long jobId);
        /// <summary>
        /// Author: TUNGTD
        /// Created: 24/08/2023
        /// Description: count candidate apply job by condition
        /// </summary>
        /// <param name="condition"></param>
        /// <returns></returns>
        Task<int> CountByCondition(CountCandidateApplyJobCondition condition);
        /// <summary>
        /// Author : ThanhNd
        /// CreatedTime : 23/08/2023
        /// Description : Paging DT in CandidateManageJob
        /// </summary>
        /// <param name="parameters">DTPagingCandidateApplyJobParameters</param>
        /// <returns></returns>
        Task<DTResult<CandidateApplyJobAggregate>> PagingByCandidateId(DTPagingCandidateApplyJobParameters parameters);
        Task<CandidateApplyJobAggregate> DetailById(long id);
        Task<List<CandidateApplyJobAggregate>> GetListCandidateApplyJobCompare(long jobId, long candidateApplyJobId);
        Task<CandidateApplyJobAggregate> DetailById(long jobId, long candidateApplyJobId);
    }
}
