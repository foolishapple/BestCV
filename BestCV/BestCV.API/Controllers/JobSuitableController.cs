using BestCV.API.Utilities;
using BestCV.Application.Models.JobSuitable;
using BestCV.Application.Services.Interfaces;
using BestCV.Core.Entities;
using BestCV.Domain.Aggregates.Job;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace BestCV.API.Controllers
{
	[Route("api/job-suitable")]
	[ApiController]
	public class JobSuitableController : BaseController
	{
		private readonly IJobSuitableService service;
		private readonly ILogger<JobSuitableController> logger;
		public JobSuitableController(IJobSuitableService _service, ILoggerFactory loggerFactory)
		{
			service = _service;
			logger = loggerFactory.CreateLogger<JobSuitableController>();
		}


		#region CRUD
		/// <summary>
		/// Author : Nam Anh
		/// CreatedTime : 08/09/2023
		/// </summary>
		/// <param name="id"></param>
		/// <returns></returns>
		[HttpGet("detail/{id}")]
		public async Task<IActionResult> Detail([Required] int id)
		{
			try
			{
				var response = await service.GetByIdAsync(id);
				return Ok(response);
			}
			catch (Exception ex)
			{
				logger.LogError(ex, $"Failed to get job suitable by id: {id}");
				return BadRequest();
			}
		}

		/// <summary>
		/// Author : Nam Anh
		/// CreatedTime : 08/09/2023
		/// </summary>
		/// <param name="model"></param>
		/// <returns></returns>
		[HttpPost("add")]
		public async Task<IActionResult> Add([FromBody] InsertJobSuitableDTO model)
		{
			try
			{
				var data = await service.CreateAsync(model);
				return Ok(data);
			}
			catch (Exception ex)
			{
				logger.LogError(ex, $"Failed to add job suitable");
				return BadRequest();
			}
		}

		/// <summary>
		/// Author : Nam Anh
		/// CreatedTime : 08/09/2023
		/// </summary>
		/// <param name="model"></param>
		/// <returns></returns>
		[HttpPut("update")]
		public async Task<IActionResult> Update([FromBody] UpdateJobSuitableDTO model)
		{
			try
			{
				var data = await service.UpdateAsync(model);
				return Ok(data);
			}
			catch (Exception ex)
			{
				logger.LogError(ex, $"Failed to update job suitable");
				return BadRequest();
			}
		}

		/// <summary>
		/// Author : Nam Anh
		/// CreatedTime : 08/09/2023
		/// </summary>
		/// <param name="id"></param>
		/// <returns></returns>
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
				logger.LogError(ex, $"Failed to soft delete job suitable");
				return BadRequest();
			}
		}

		/// <summary>
		/// Author : Nam Anh
		/// CreatedTime : 08/09/2023
		/// </summary>
		/// <returns></returns>
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
				logger.LogError(ex, $"Failed to get list job suitable");
				return BadRequest();
			}
		}
        #endregion


        #region Additional Resources
		/// <summary>
		/// Author : Nam Anh
		/// CreatedTime : 8/09/2023
		/// </summary>
		/// <returns></returns>
        [HttpGet("list-aggregates")]
        public async Task<IActionResult> GetListAggregates()
        {
            try
            {
                var data = await service.ListAggregatesAsync();
                return Ok(data);
            }
            catch (Exception ex)
            {
                logger.LogError(ex, $"Failed to get list job suitable aggregates");
                return BadRequest();
            }
        }


        [HttpGet("list-job-selected")]
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


        [HttpGet("list-job-suitable-dashboard")]
        public async Task<IActionResult> ListJobSuitableDashboard()
        {
            try
            {
                var data = await service.ListJobSuitableDashboard();
                return Ok(data);
            }
            catch (Exception e)
            {
                logger.LogError(e, "Failed to get list job suitable dashboard");
                return BadRequest();
            }
        }

        [HttpPost]
        [Route("searching-top-job-suitable")]
        public async Task<IActionResult> SearchingJobSuitable([FromBody] SearchJobWithServiceParameters parameters)
        {
            try
            {
                parameters.CandidateId = this.GetLoggedInUserId();
                var data = await service.SearchingJobSuitable(parameters);
                return Ok(data);
            }
            catch (Exception ex)
            {
                logger.LogError("Fail when get search top job suitable", ex);
                return BadRequest();
            }
        }
        #endregion
    }
}
