using Jobi.Core.Entities;
using Jobi.Core.Repositories;
using Jobi.Core.Utilities;
using Jobi.Domain.Aggregates.CandidateApplyJobs;
using Jobi.Domain.Aggregates.Job;
using Jobi.Domain.Entities;
using Jobi.Infrastructure.Persistence;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Jobi.Infrastructure.Repositories.Interfaces
{
    public interface IJobRepository : IRepositoryBaseAsync<Job, long, JobiContext>
    {
        /// <summary>
        /// Author : ThanhNd
        /// CreatedTime : 11/08/2023
        /// Description : Tìm kiếm công việc trang chủ (server side)
        /// </summary>
        /// <param name="param">SearchingJobParameters</param>
        /// <returns></returns>
        Task<PagingData<List<SearchJobAggregates>>> SearchJobHomePageAsync(SearchingJobParameters parameter);

        Task<object> LoadDataFilterJobHomePageAsync();

        Task<JobDetailAggregates> GetDetailById(long jobId);
        /// <summary>
        /// Author: TUNGTD
        /// Created: 22/08/2023
        /// Description: List Job aggregate by recruitCampain Id
        /// </summary>
        /// <param name="id">recruitCampain id</param>
        /// <returns></returns>
        Task<List<RecruitCampainJobAggregate>> ListByRecruitCampain(long id);
        Task<DetailJobAggregates> GetDetailJobAsync(long jobId, long candidateId);
        int CountJob();
        Task<List<SearchJobAggregates>> ListJobReference(int categoryId, int typeId);
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
        Task<List<SearchJobAggregates>> ListJobByCompanyId(int companyId, long candidateId, int quantity);
        /// <summary>
        /// Author: TUNGTD
        /// Created: 27/08/2023
        /// Description: Count job by condition
        /// </summary>
        /// <param name="condition"></param>
        /// <returns></returns>
        Task<int> CountByCondition(CountJobCondition condition);

        Task<object> ListRecruitmentNewsAggregates(DTParameters parameters);
        Task<List<SelectListItem>> ListJobCategorySelected();
        Task<List<SelectListItem>> ListJobTypeSelected();
        Task<List<SelectListItem>> ListJobStatusSelected();
        Task<List<SelectListItem>> ListJobExperienceSelected();
        Task<List<SelectListItem>> ListCampaignSelected();
        Task<bool> QuickIsApprovedAsync(long id);
        Task<List<CandidateApplyJobAggregate>> GetCandidateApplyJobByJobIdAsync(long jobId);
        Task<List<JobDetailAggregates>> GetJobDetailByJobIdAsync(long jobId);
        Task<RecruitmentCampaign> GetRecruitmentCampaignByJobIdAsync(long jobId);
        Task<Company> GetCompanyByRecruitmentCampaignIdAsync(long recruitmentCampaignId);
        Task<IEnumerable<WorkPlace>> GetWorkPlacesByJobIdAsync(long jobId);
        Task<IEnumerable<Job>> GetAllPrimaryJobCategoriesAsync();
        //Task<IEnumerable<JobSecondaryJobPosition>> GetJobSecondaryJobPositionsByJobIdAsync(long jobId);
        Task<List<JobSecondaryJobCategory>> GetSecondaryJobCategoriesByJobIdAsync(long jobId);
        Task<List<int>> GetTagIdsByJobIdAsync(long jobId);
        Task<List<int>> GetJobSkillIdsByJobIdAsync(long jobId);
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
        Task<bool> Privacy(long id, long userId);
        /// <summary>
        /// Author: TUNGTD
        /// Created: 19/09/2023
        /// Description: Get list job suggesstion 
        /// </summary>
        /// <returns></returns>
        Task<List<JobSuggestionAggregate>> ListSuggestion();
        /// <summary>
        /// Author: TUNGTD
        /// Created: 19/09/2023
        /// Description: serach job suggestion by keyword
        /// </summary>
        /// <param name="keyword"></param>
        /// <returns></returns>
        Task<List<JobSuggestionAggregate>> SearchSuggestion(string keyword);
    }
}
