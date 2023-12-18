using BestCV.API.Utilities;
using BestCV.Application.Models.TopCompany;
using BestCV.Application.Models.TopFeatureJob;
using BestCV.Application.Services.Interfaces;
using BestCV.Core.Entities;
using BestCV.Domain.Aggregates.Job;
using BestCV.Domain.Aggregates.TopFeatureJob;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace BestCV.API.Controllers
{
    [Route("api/top-feature-job")]
    [ApiController]
    public class TopFeatureJobController : BaseController
    {
        private readonly ITopFeatureJobService service;
        private readonly ILogger<TopFeatureJobController> logger;
        public TopFeatureJobController(ITopFeatureJobService _service, ILoggerFactory loggerFactory)
        {
            service = _service;
            logger = loggerFactory.CreateLogger<TopFeatureJobController>();
        }

        #region CRUD
 
        [HttpGet("detail/{id}")]
        public async Task<IActionResult> Detail([Required] int id)
        {
            try
            {
                var reponse = await service.GetByIdAsync(id);
                return Ok(reponse);
            }
            catch (Exception ex)
            {
                logger.LogError(ex, $"Failed to get top feature job by id: {id}");
                return BadRequest();
            }
        }

        [HttpGet("list-by-orderSort")]
        public async Task<IActionResult> GetListbyOrderSort()
        {
            try
            {
                var data = await service.ListFeatureJob();
                return Ok(data);
            }
            catch (Exception ex)
            {
                logger.LogError(ex, $"Failed to get list top feature job");
                return BadRequest();
            }
        }
 
        [HttpPost]
        [Route("change-order-slide")]
        public async Task<IActionResult> ChangeOrderSort([FromBody] ChangeTopFeatureJobDTO model)
        {
            try
            {
                var isSuccess = await service.ChangeOrderSort(model);
                return Ok(BestCVResponse.Success(isSuccess));

            }
            catch (Exception ex)
            {
                logger.LogError("Fail when change order sort", ex);
                return BadRequest();
            }
        }

        [HttpPost("add")]
        public async Task<IActionResult> Add([FromBody] InsertTopFeatureJobDTO model)
        {
            try
            {
                var data = await service.CreateAsync(model);
                return Ok(data);
            }
            catch (Exception ex)
            {
                logger.LogError(ex, $"Failed to add top feature job");
                return BadRequest();
            }
        }

        [HttpPut("update")]
        public async Task<IActionResult> Update([FromBody] UpdateTopFeatureJobDTO model)
        {
            try
            {
                var data = await service.UpdateAsync(model);
                return Ok(data);
            }
            catch (Exception ex)
            {
                logger.LogError(ex, $"Failed to update top feature job");
                return BadRequest();
            }
        }


        [HttpDelete("delete")]
        public async Task<IActionResult> Delete([Required] int id)
        {
            try
            {
                var data = await service.SoftDeleteAsync(id);
                return Ok(data);
            }
            catch (Exception ex)
            {
                logger.LogError(ex, $"Failed to soft delete top feature job");
                return BadRequest();
            }
        }


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
                logger.LogError(ex, $"Failed to get list top feature job");
                return BadRequest();
            }
        }
        #endregion

        #region Additional Resources

		[HttpGet("List-Job-Selected")]
        public async Task<IActionResult> ListJobSelected()
        {
            try
            {
                var data = await service.ListJobSelected();
                return Ok(BestCVResponse.Success(data));
            }
            catch (Exception e)
            {
                logger.LogError(e, "Failed to get list selected job");
                return BadRequest();
            }
        }

        [HttpPost]
        [Route("search-jobs")]
        public async Task<IActionResult> Search([FromBody] Select2Aggregates select2Aggregates)
        {
            try
            {
                var result = await service.searchJobs(select2Aggregates);
                return Ok(result);
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Failed to get list for jobs");
                return BadRequest(ex.Message);
            }
        }

        [HttpGet]
        [Route("top-feature-job-home-page")]
        public async Task<IActionResult> GetTopFeatureJob()
        {
            try
            {
                var result = await service.ListTopFeatureJobShowOnHomePageAsync();
                return Ok(result);
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Failed to get top feature jobs");
                return BadRequest(ex.Message);
            }
        }

        [HttpPost]
        [Route("searching-feature-job")]
        public async Task<IActionResult> SearchingFeatureJob([FromBody] SearchJobWithServiceParameters parameters)
        {
            try
            {
                parameters.CandidateId = this.GetLoggedInUserId();
                var data = await service.SearchingFeatureJob(parameters);
                return Ok(data);
            }
            catch (Exception ex)
            {
                logger.LogError(ex, $"Failed to searching feature job");
                return BadRequest();
            }
        }
        #endregion
    }
}
