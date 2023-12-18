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
      
        Task<DTResult<CandidateApplyJobAggregate>> DTPaging(DTPagingCandidateApplyJobParameters parameters);
       
        Task<BestCVResponse> UpdateDescription(AddNoteCandidateApplyJobDTO obj);
       
        Task<BestCVResponse> ChangeStatus(ChangeStatusCandidateApplyJobDTO obj);

        Task<BestCVResponse> ApplyJob(long jobId, long candidateId);
      
        Task<BestCVResponse> CountTotalCVByEmployer(long id);
       
        Task<BestCVResponse> CountTotalCVCandidateApplyByEmployer(long id);
        
        Task<DTResult<CandidateApplyJobAggregate>> PagingByCandidateId(DTPagingCandidateApplyJobParameters parameters);
        
        Task<BestCVResponse> EmployerViewed(long id);
        
        Task<BestCVResponse> CountTotalToJob(long id);
        
        Task<BestCVResponse> CountTotalCVCandidateApplyToJob(long id);

        Task<BestCVResponse> GetListCandidateApplyToJob(long jobId, long candidateApplyJobId);
        Task<BestCVResponse> DetailById(long jobId, long candidateApplyJobId);
    }
}
