using BestCV.API.Utilities;
using BestCV.Application.Models.Job;
using BestCV.Application.Services.Interfaces;
using BestCV.Core.Utilities;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using BestCV.Core.Entities;
using Microsoft.AspNetCore.Mvc;
using NPOI.SS.UserModel.Charts;
using System.Collections;
using BestCV.Domain.Aggregates.Job;
using BestCV.Domain.Constants;
using NPOI.OpenXmlFormats.Wordprocessing;
using System.ComponentModel.DataAnnotations;
using BestCV.Application.Services.Implement;
using BestCV.Domain.Entities;
using NPOI.POIFS.Crypt.Dsig;

namespace BestCV.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class JobController : BaseController
    {

        private readonly IJobService service;
        private readonly ILogger<JobController> logger;

        public JobController(IJobService _service, ILoggerFactory loggerFactory)
        {
            service = _service;
            logger = loggerFactory.CreateLogger<JobController>();
        }

        #region CRUD

        /// <summary>
        /// author: truongthieuhuyen
        /// </summary>
        /// <param name="obj"></param>
        /// <returns>tạo mới job</returns>
        [HttpPost("add")]
        public async Task<IActionResult> AddJob(InsertJobDTO obj)
        {
            try
            {
                var res = await service.CreateAsync(obj);
                return Ok(res);
            }
            catch (Exception e)
            {
                logger.LogError(e, $"Failed to create new job");
                return BadRequest();
            }
        }


        /// <summary>
        /// author: truongthieuhuyen
        /// created: 24.08.2023
        /// 
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        [HttpPut("update")]
        public async Task<IActionResult> UpdateJob(UpdateJobDTO obj)
        {
            try
            {
                var res = await service.UpdateAsync(obj);
                return Ok(res);
            }
            catch (Exception e)
            {
                logger.LogError(e, $"Failed to update job[{obj.Id}]");
                return BadRequest();
            }
        }

        /// <summary>
        /// Author : HoanNK
        /// CreatedTime : 08/09/2023
        /// </summary>
		/// <returns>List MenuType</returns>
		[HttpGet("list")]
        public async Task<IActionResult> GetList()
        {
            try
            {
                var data = await service.GetAllAsync();
                return Ok(data);

            }
            catch (Exception ex)
            {
                logger.LogError(ex, $"Failed to get list job");
                return BadRequest();
            }
        }

        /// <summary>
        /// author: truongthieuhuyen
        /// created: 21.08.2023
        /// </summary>
        /// <param name="id"></param>
        /// <returns>chi tiết tin tuyển dụng</returns>
        [HttpGet("job-detail/{id}")]
        public async Task<IActionResult> GetDetailById(long id)
        {
            try
            {
                var res = await service.GetDetalById(id);
                return Ok(res);
            }
            catch (Exception e)
            {
                logger.LogError(e, $"Failed to get detail of job [{id}]");
                return BadRequest();
            }
        }

        /// <summary>
        /// Author: TUNGTD
        /// Created: 22/08/2023
        /// Description: API GET get list job aggregate by recruit campain id
        /// </summary>
        /// <param name="id">recruit campain id</param>
        /// <returns></returns>
        [HttpGet("list-to-recruit-campain/{id}")]
        public async Task<IActionResult> ListToRecruitCampain(long id)
        {
            try
            {
                var response = await service.ListByRecruitCampain(id);
                return Ok(response);
            }
            catch (Exception e)
            {
                logger.LogError(e, "Failed to get list job aggregate to recruit campain");
                return BadRequest();
            }
        }
        /// <summary>
        /// Author : Nam Anh 
        /// CreatedTime: 22/08/2023
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("detail-admin/{id}")]
        public async Task<IActionResult> DetailAdmin([Required] long id)
        {
            try
            {
                var response = await service.AdminDetailAsync(id);
                return Ok(response);
            }
            catch (Exception ex)
            {
                logger.LogError(ex, $"Failed to get detail job by id: {id}");
                return BadRequest();
            }
        }

        [HttpGet("detail/{id}")]
        public async Task<IActionResult> Detail([Required] long id)
        {
            try
            {
                var response = await service.GetByIdAsync(id);
                return Ok(response);
            }
            catch (Exception ex)
            {
                logger.LogError(ex, $"Failed to get detail job by id: {id}");
                return BadRequest();
            }
        }
        #endregion


        #region Additional Resources
        [HttpPost("searching-job")]
        public async Task<IActionResult> SearchingJob([FromBody] SearchingJobParameters parameters)
        {
            try
            {
                parameters.CandidateId = this.GetLoggedInUserId();
                var data = await service.SearchJobHomePageAsync(parameters);
                return Ok(data);
            }
            catch (Exception ex)
            {
                logger.LogError(ex, $"Failed to searching job");
                return BadRequest();
            }
        }

        [HttpGet("filter-job")]
        public async Task<IActionResult> filterJob()
        {
            try
            {
                var data = await service.LoadDataFilterJobHomePageAsync();
                return Ok(data);
            }
            catch (Exception ex)
            {
                logger.LogError(ex, $"Failed to searching job");
                return BadRequest();
            }
        }

        [HttpGet("detail-job-on-home-page/{BestCVd}")]
        public async Task<IActionResult> DetailJobOnHomepage(long BestCVd)
        {
            try
            {
                var candidateId = this.GetLoggedInUserId();
                if (candidateId != 0)
                {
                    await service.AddToListViewed(BestCVd, candidateId);
                }
                var data = await service.GetDetailJobOnHomePageAsync(BestCVd, candidateId);
                return Ok(data);
            }
            catch (Exception ex)
            {
                logger.LogError(ex, $"Failed to searching job");
                return BadRequest();
            }
        }

        [HttpGet("list-job-reference/{categoryId}/{typeId}")]
        public async Task<IActionResult> ListJobReference(int categoryId, int typeId)
        {
            try
            {

                var data = await service.ListJobReference(categoryId, typeId);
                return Ok(data);
            }
            catch (Exception ex)
            {
                logger.LogError(ex, $"Failed to searching job");
                return BadRequest();
            }
        }

        /// <summary>
        /// Author: TUNGTD
        /// Created: 23/08/2023
        /// Description: Get list job aggregate datatables paging
        /// </summary>
        /// <param name="parameters"></param>
        /// <returns></returns>
        [HttpPost("dt-paging-to-employer")]
        public async Task<IActionResult> DTPagingToEmployer([FromBody] DTJobPagingParameters parameters)
        {
            try
            {
                var userId = this.GetLoggedInUserId();
                parameters.EmployerId = userId;
                var result = await service.ListDTPaging(parameters);
                return Ok(result);
            }
            catch (Exception e)
            {
                logger.LogError(e, "Failed to get list job datatatabled paging");
                return BadRequest();
            }
        }

        [HttpGet("job-on-detail-company-page/{companyId}")]
        public async Task<IActionResult> JobOnDetailCompanyPage(int companyId)
        {
            try
            {
                var candidateId = this.GetLoggedInUserId();
                var data = await service.ListJobByCompanyId(companyId, candidateId, JobConst.JOB_QUANTITY_DETAIL_COMPANY_PAGE);
                return Ok(data);
            }
            catch (Exception ex)
            {
                logger.LogError(ex, $"Failed to searching job");
                return BadRequest();
            }
        }
        /// <summary>
        /// Author: TUNGTD
        /// Created: 27/08//2023
        /// Description: API POST count job by condition to employer
        /// </summary>
        /// <param name="condition">count job condition</param>
        /// <returns></returns>
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        [HttpPost("count-to-employer")]
        public async Task<IActionResult> CountToEmployer([FromBody] CountJobCondition condition)
        {
            try
            {
                var userId = this.GetLoggedInUserId();
                condition.EmployerIds = new long[] { userId };
                var response = await service.CountByCondition(condition);
                return Ok(response);
            }
            catch (Exception e)
            {
                logger.LogError(e, "Failed to count job to employer");
                return BadRequest();
            }
        }
        /// <summary>
        /// Author : Nam Anh
        /// CreatedTime : 22/08/2023
        /// </summary>
        /// <param name="parameters"></param>
        /// <returns></returns>
        [HttpPost("list-recruitment-news-aggregates")]
        public async Task<IActionResult> ListRecruitmentNewsAggregates(DTParameters parameters)
        {
            try
            {
                return Json(await service.ListRecruitmentNewsAggregates(parameters));
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Fail to load list recruitment news aggregates");
                return BadRequest();
            }
        }


		[HttpGet("list-job-category-selected")]
        public async Task<IActionResult> ListJobCategorySelected()
        {
            try
            {
                var data = await service.ListJobCategorySelected();
                return Ok(BestCVResponse.Success(data));
            }
            catch (Exception e)
            {
                logger.LogError(e, "Failed to get list selected job category");
                return BadRequest();
            }
        }

        [HttpGet("secondary-categories-select")]
        public async Task<ActionResult<List<JobPosition>>> GetSecondaryJobCategoriesForSelect()
        {
            try
            {
                var secondaryJobPositions = await service.GetSecondaryJobCategoriesForSelect();
                return Ok(BestCVResponse.Success(secondaryJobPositions));
            }
            catch (Exception e)
            {
                logger.LogError(e, "Failed to get selected secondary category");
                return BadRequest();
            }
            
        }
        
   
        [HttpGet("job-tag-select")]
        public async Task<ActionResult<List<Tag>>> GetJobTagsByBestCVdAsync()
        {
            try
            {
                var jobTag = await service.GetJobTagsAsync();
                return Ok(BestCVResponse.Success(jobTag));
            }
            catch (Exception e)
            {
                logger.LogError(e, "Failed to get selected job tag");
                return BadRequest();
            }
        }
        
   
        [HttpGet("job-skill-select")]
        public async Task<ActionResult<List<JobSkill>>> GetJobSkillsByBestCVdAsync()
        {
            try
            {
                var jobSkill = await service.GetJobSkillsAsync();
                return Ok(BestCVResponse.Success(jobSkill));
            }
            catch (Exception e)
            {
                logger.LogError(e, "Failed to get selected job skill");
                return BadRequest();
            }
        }

  
        [HttpGet("list-campaign-selected")]
        public async Task<IActionResult> ListCampaignSelected()
        {
            try
            {
                var data = await service.ListCampaignSelected();
                return Ok(BestCVResponse.Success(data));
            }
            catch (Exception e)
            {
                logger.LogError(e, "Failed to get list selected campaign category");
                return BadRequest();
            }
        }
        

		[HttpGet("list-job-type-selected")]
        public async Task<IActionResult> ListJobTypeSelected()
        {
            try
            {
                var data = await service.ListJobTypeSelected();
                return Ok(BestCVResponse.Success(data));
            }
            catch (Exception e)
            {
                logger.LogError(e, "Failed to get list selected job type");
                return BadRequest();
            }
        }
        

		[HttpGet("list-job-status-selected")]
        public async Task<IActionResult> ListJobStatusSelected()
        {
            try
            {
                var data = await service.ListJobStatusSelected();
                return Ok(BestCVResponse.Success(data));
            }
            catch (Exception e)
            {
                logger.LogError(e, "Failed to get list selected job status");
                return BadRequest();
            }
        }
        

		[HttpGet("list-job-experience-selected")]
        public async Task<IActionResult> ListJobExperienceSelected()
        {
            try
            {
                var data = await service.ListJobExperienceSelected();
                return Ok(BestCVResponse.Success(data));
            }
            catch (Exception e)
            {
                logger.LogError(e, "Failed to get list selected job experience");
                return BadRequest();
            }
        }

        [HttpGet("primary-categories-select")]
        public async Task<IActionResult> GetAllPrimaryJobPositionNames()
        {
            try
            {
                var positions = await service.GetAllPrimaryJobCategoryNamesAsync();
                return Ok(BestCVResponse.Success(positions));
            }
            catch (Exception e)
            {
                logger.LogError(e, "Failed to get list selected primary category");
                return BadRequest();
            }
            
        }

        [HttpPut("quick-isapproved")]
        public async Task<IActionResult> QuickIsApproved([Required] long id)
        {
            try
            {
                var response = await service.QuickIsApprovedAsync(id);
                return Ok(response);
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Fail to quick isapproved async");
                return BadRequest();
            }
        }

        [HttpGet("list-suggestion")]
        public async Task<IActionResult> ListSuggestion()
        {
            try
            {
                var result = await service.ListSuggestion();
                return Ok(result);
            }
            catch (Exception e)
            {
                logger.LogError(e, "Failed to get list job suggestion");
                return BadRequest();
            }
        }
 
        [HttpGet("search-suggestion")]
        public async Task<IActionResult> ListSuggestion(string keyword)
        {
            try
            {
                var result = await service.SearchSuggestion(keyword);
                return Ok(result);
            }
            catch (Exception e)
            {
                logger.LogError(e, "Failed to keyword get list job suggestion");
                return BadRequest();
            }
        }
        #endregion
    }
}
