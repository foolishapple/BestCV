using BestCV.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BestCV.Application.Services.Interfaces
{
    public interface ICandidateHomeService
    {
        Task<BestCVResponse> GetListTopCompanyShowonHomepage();
        Task<BestCVResponse> GetListTopJobShowonHomepage();
        //Task<BestCVResponse> GetDetailJobAsync(long jobId);
        Task<BestCVResponse> GetListJobCategoryShowonHomepage();
        Task<BestCVResponse> GetListTopPostShowonHomepage();
        int CountJob();

        Task<bool> JobIsActive(long id);

        Task<bool> CandidateIsActive(long id);

        Task<bool> CompanyIsActive(int id);
    }
}
