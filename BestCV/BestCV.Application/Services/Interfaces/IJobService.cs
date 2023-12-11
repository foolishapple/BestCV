using BestCV.Application.Models.Job;
using BestCV.Application.Models.JobCategory;
using BestCV.Application.Models.JobPosition;
using BestCV.Application.Models.JobSecondaryJobPositions;
using BestCV.Application.Models.JobStatuses;
using BestCV.Core.Entities;
using BestCV.Core.Services;
using BestCV.Core.Utilities;
using BestCV.Domain.Aggregates.Job;
using BestCV.Domain.Entities;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BestCV.Application.Services.Interfaces
{
    public interface IJobService : IServiceQueryBase<long, InsertJobDTO, UpdateJobDTO, JobDetailDTO>
    {
        Task<DionResponse> SearchJobHomePageAsync(SearchingJobParameters parameter);
        Task<DionResponse> LoadDataFilterJobHomePageAsync();

        /// <summary>
        /// author: truongthieuhuyen
        /// created: 23.08.2023
        /// description: chi tiết job (và các trường phụ) theo id
        /// </summary>
        /// <param name="jobId"></param>
        /// <returns></returns>
        Task<DionResponse> GetDetalById(long jobId);

        /// <summary>
        /// Author: TUNGTD
        /// Created: 22/08/2023
        /// Description: List Job aggregate by recruitCampain Id
        /// </summary>
        /// <param name="id">recruitCampain id</param>
        /// <returns></returns>
        Task<DionResponse> ListByRecruitCampain(long id);
        Task<DionResponse> GetDetailJobOnHomePageAsync(long jobId, long candidateId);
        Task AddToListViewed(long jobId, long candidateId);
        Task<DionResponse> ListJobReference(int categoryId, int typeId);
        /// <summary>
        /// Author: TUNGTD
        /// Created: 23/08/2023
        /// Description: List job datatables paging
        /// </summary>
        /// <param name="parameters"></param>
        /// <returns></returns>
        Task<DTResult<EmployerJobAggregate>> ListDTPaging(DTJobPagingParameters parameters);

        /// <summary>
        /// Author: HuyDQ
        /// Created: 24/08/2023
        /// Description: List job by companyId
        /// </summary>
        /// <param name="companyId">Mã công ty</param>
        /// <param name="candidateId">Mã ứng viên</param>
        /// <param name="quantity">Số lượng bản ghi muốn lấy</param>
        /// <returns></returns>
        Task<DionResponse> ListJobByCompanyId(int companyId, long candidateId, int quantity);
        /// <summary>
        /// Author: TUNGTD
        /// Created: 27/08/2023
        /// Description: Count job by condition
        /// </summary>
        /// <param name="condition"></param>
        /// <returns></returns>
        Task<DionResponse> CountByCondition(CountJobCondition condition);

        Task<object> ListRecruitmentNewsAggregates(DTParameters parameters);
        Task<List<SelectListItem>> ListJobCategorySelected();
        Task<List<SelectListItem>> ListJobTypeSelected();
        Task<List<SelectListItem>> ListJobStatusSelected();
        Task<List<SelectListItem>> ListJobExperienceSelected();
        Task<List<SelectListItem>> ListCampaignSelected();
        Task<DionResponse> QuickIsApprovedAsync(long id);
        Task<DionResponse> AdminDetailAsync(long id);
        Task<IEnumerable<JobCategoryDTO>> GetAllPrimaryJobCategoryNamesAsync();
        Task<List<JobCategory>> GetSecondaryJobCategoriesForSelect();
        Task<List<Tag>> GetJobTagsAsync();
        Task<List<JobSkill>> GetJobSkillsAsync();
        /// <summary>
        /// Author: TUNGTD
        /// Created: 11/09/2023
        /// Description: Check privacy
        /// </summary>
        /// <param name="id">Job id</param>
        /// <param name="userId">Employer logged Id</param>
        /// <returns></returns>
        Task<DionResponse> Privacy(long id, long userId);
        /// <summary>
        /// Author: TUNGTD
        /// Created: 11/09/2023
        /// Description: Add view count by job id
        /// </summary>
        /// <param name="id">job id</param>
        /// <returns></returns>
        Task AddViewCount(long id);
        /// <summary>
        /// Author: TUNGTD
        /// Created: 19/09/2023
        /// Description: Get list job suggesstion 
        /// </summary>
        /// <returns></returns>
        Task<DionResponse> ListSuggestion();
        /// <summary>
        /// Author: TUNGTD
        /// Created: 19/09/2023
        /// Description: Get search list job suggesstion 
        /// </summary>
        /// <returns></returns>
        Task<DionResponse> SearchSuggestion(string keyword);
    }
}
