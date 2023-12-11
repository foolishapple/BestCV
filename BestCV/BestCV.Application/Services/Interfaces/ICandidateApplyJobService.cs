using BestCV.Application.Models.CandidateApplyJobs;
using BestCV.Core.Entities;
using BestCV.Core.Services;
using BestCV.Core.Utilities;
using BestCV.Domain.Aggregates.CandidateApplyJobs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BestCV.Application.Services.Interfaces
{
    public interface ICandidateApplyJobService : IServiceQueryBase<long,InsertCandidateApplyJobDTO,UpdateCandidateApplyJobDTO,CandidateApplyJobDTO>
    {
        /// <summary>
        /// Author: TUNGTD
        /// Created: 15/08/2023
        /// Description: Datatables paging candidate apply job 
        /// </summary>
        /// <param name="parameters">Paging parameters</param>
        /// <returns></returns>
        Task<DTResult<CandidateApplyJobAggregate>> DTPaging(DTPagingCandidateApplyJobParameters parameters);
        /// <summary>
        /// Author: TUNGTD
        /// Created: 15/08/2023
        /// Description:update description to candidate apply job 
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        Task<DionResponse> UpdateDescription(AddNoteCandidateApplyJobDTO obj);
        /// <summary>
        /// Author: TUNGTD
        /// Created: 15/08/2023
        /// Description:change candidate apply job status
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        Task<DionResponse> ChangeStatus(ChangeStatusCandidateApplyJobDTO obj);

        Task<DionResponse> ApplyJob(long jobId, long candidateId);
        /// <summary>
        /// Author: TUNGTD
        /// Created: 24/08/2023
        /// Description: count total cv by employer id
        /// </summary>
        /// <param name="id">employer id</param>
        /// <returns></returns>
        Task<DionResponse> CountTotalCVByEmployer(long id);
        /// <summary>
        /// Author: TUNGTD
        /// Created: 24/08/2023
        /// Description: count total cv by employer id
        /// </summary>
        /// <param name="id">employer id</param>
        /// <returns></returns>
        Task<DionResponse> CountTotalCVCandidateApplyByEmployer(long id);
        /// <summary>
        /// Author : ThanhNd
        /// CreatedTime : 23/08/2023
        /// Description : Paging by CandidateId in CandidateManageJob
        /// </summary>
        /// <param name="parameters"></param>
        /// <returns></returns>
        Task<DTResult<CandidateApplyJobAggregate>> PagingByCandidateId(DTPagingCandidateApplyJobParameters parameters);
        /// <summary>
        /// Author: TUNGTD
        /// Created: 08/09/2023
        /// Description: Update employer viewed CV Status
        /// </summary>
        /// <param name="id">Mã ứng viên ứng tuyển tin tuyển dụng</param>
        /// <returns></returns>
        Task<DionResponse> EmployerViewed(long id);
        /// <summary>
        /// Author: TUNGTD
        /// Created: 24/08/2023
        /// Description: count total cv by job id
        /// </summary>
        /// <param name="id">job id</param>
        /// <returns></returns>
        Task<DionResponse> CountTotalToJob(long id);
        /// <summary>
        /// Author: TUNGTD
        /// Created: 24/08/2023
        /// Description: count total cv by job id
        /// </summary>
        /// <param name="id">job id</param>
        /// <returns></returns>
        Task<DionResponse> CountTotalCVCandidateApplyToJob(long id);

        Task<DionResponse> GetListCandidateApplyToJob(long jobId, long candidateApplyJobId);
        Task<DionResponse> DetailById(long jobId, long candidateApplyJobId);
    }
}
