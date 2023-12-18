using BestCV.Application.Models.JobReference;
using BestCV.Application.Services.Interfaces;
using BestCV.Core.Entities;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace BestCV.API.Controllers
{
    [Route("api/job-reference")]
    [ApiController]
    public class JobReferenceController : BaseController
    {
        private readonly IJobReferenceService service;
        private readonly ILogger<JobReferenceController> logger;
        public JobReferenceController(IJobReferenceService _service, ILoggerFactory loggerFactory)
        {
            service = _service;
            logger = loggerFactory.CreateLogger<JobReferenceController>();
        }

        #region CRUD

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
                logger.LogError(ex, $"Failed to get job reference by id: {id}");
                return BadRequest();
            }
        }


        [HttpPost("add")]
        public async Task<IActionResult> Add([FromBody] InsertJobReferenceDTO model)
        {
            try
            {
                var data = await service.CreateAsync(model);
                return Ok(data);
            }
            catch (Exception ex)
            {
                logger.LogError(ex, $"Failed to add job reference");
                return BadRequest();
            }
        }


        [HttpPut("update")]
        public async Task<IActionResult> Update([FromBody] UpdateJobReferenceDTO model)
        {
            try
            {
                var data = await service.UpdateAsync(model);
                return Ok(data);
            }
            catch (Exception ex)
            {
                logger.LogError(ex, $"Failed to update job reference");
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
                logger.LogError(ex, $"Failed to soft delete job reference");
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
                logger.LogError(ex, $"Failed to get list job reference");
                return BadRequest();
            }
        }
        #endregion

        #region Additional Resources

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
                logger.LogError(ex, $"Failed to get list job reference aggregates");
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


        [HttpGet("list-job-reference-on-detail-job/{BestCVd}")]
        public async Task<IActionResult> ListJobReferenceOnDetailJob(long BestCVd)
        {
            try
            {
                var data = await service.ListJobReferenceOnDetailJob(BestCVd);
                return Ok(data);
            }
            catch (Exception e)
            {
                logger.LogError(e, "Failed to get list reference job");
                return BadRequest();
            }
        }
        #endregion
    }
}
