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
        Task<BestCVResponse> SearchJobHomePageAsync(SearchingJobParameters parameter);
        Task<BestCVResponse> LoadDataFilterJobHomePageAsync();


        Task<BestCVResponse> GetDetalById(long jobId);


        Task<BestCVResponse> ListByRecruitCampain(long id);
        Task<BestCVResponse> GetDetailJobOnHomePageAsync(long jobId, long candidateId);
        Task AddToListViewed(long jobId, long candidateId);
        Task<BestCVResponse> ListJobReference(int categoryId, int typeId);

        Task<DTResult<EmployerJobAggregate>> ListDTPaging(DTJobPagingParameters parameters);


        Task<BestCVResponse> ListJobByCompanyId(int companyId, long candidateId, int quantity);
   
        Task<BestCVResponse> CountByCondition(CountJobCondition condition);

        Task<object> ListRecruitmentNewsAggregates(DTParameters parameters);
        Task<List<SelectListItem>> ListJobCategorySelected();
        Task<List<SelectListItem>> ListJobTypeSelected();
        Task<List<SelectListItem>> ListJobStatusSelected();
        Task<List<SelectListItem>> ListJobExperienceSelected();
        Task<List<SelectListItem>> ListCampaignSelected();
        Task<BestCVResponse> QuickIsApprovedAsync(long id);
        Task<BestCVResponse> AdminDetailAsync(long id);
        Task<IEnumerable<JobCategoryDTO>> GetAllPrimaryJobCategoryNamesAsync();
        Task<List<JobCategory>> GetSecondaryJobCategoriesForSelect();
        Task<List<Tag>> GetJobTagsAsync();
        Task<List<JobSkill>> GetJobSkillsAsync();

        Task<BestCVResponse> Privacy(long id, long userId);

        Task AddViewCount(long id);

        Task<BestCVResponse> ListSuggestion();

        Task<BestCVResponse> SearchSuggestion(string keyword);
    }
}
