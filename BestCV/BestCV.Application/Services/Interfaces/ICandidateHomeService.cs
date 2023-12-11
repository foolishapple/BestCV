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
        Task<DionResponse> GetListTopCompanyShowonHomepage();
        Task<DionResponse> GetListTopJobShowonHomepage();
        //Task<DionResponse> GetDetailJobAsync(long jobId);
        Task<DionResponse> GetListJobCategoryShowonHomepage();
        Task<DionResponse> GetListTopPostShowonHomepage();
        int CountJob();
        /// <summary>
        /// Author: TUNGTD
        /// Created: 23/09/2023
        /// Description: Check job is actived
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<bool> JobIsActive(long id);
        /// <summary>
        /// Author: TUNGTD
        /// Created: 23/09/2023
        /// Description: Check candidate is actived
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<bool> CandidateIsActive(long id);
        /// <summary>
        /// Author: TUNGTD
        /// Created: 23/09/2023
        /// Description: Check company is actived
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<bool> CompanyIsActive(int id);
    }
}
