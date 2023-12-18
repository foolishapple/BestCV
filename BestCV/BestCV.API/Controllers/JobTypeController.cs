using BestCV.Application.Models.JobType;
using BestCV.Application.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace BestCV.API.Controllers
{
    [ApiController]
    [Route("api/job-type")]
    public class JobTypeController : BaseController
    {
       private readonly IJobTypeService jobTypeService;
       private readonly ILogger<JobTypeController> logger;
        public JobTypeController (IJobTypeService _jobTypeService,ILoggerFactory _logger)
        {
            jobTypeService = _jobTypeService;
            logger = _logger.CreateLogger<JobTypeController>();
        }

        #region CRUD

        [HttpGet("list")]
        public async Task<IActionResult> GetAll() 
        {
            try
            {
                var res = await jobTypeService.GetAllAsync();
                if (res != null)
                {
                    return Ok(res);
                }
                return BadRequest();
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Failed to get all jobType");
                return BadRequest();
            }
        }

        [HttpGet("detail/{id}")]
        public async Task<IActionResult> Detail([Required] int id)
        {
            try
            {
                var response = await jobTypeService.GetByIdAsync(id);
                return Ok(response);
            }
            catch (Exception ex)
            {
                logger.LogError(ex, $"Failed to get detail jobType by id: {id}");
                return BadRequest();
            }
        }

        [HttpPost("add")]
        public async Task<IActionResult> Add([FromBody] InsertJobTypeDTO obj)
        {
            try
            {
                var res = await jobTypeService.CreateAsync(obj);
                return Ok(res);
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Failed to add jobType");
                return BadRequest();
            }
        }

        [HttpPut("update")]
        public async Task<IActionResult> Update([FromBody] UpdateJobTypeDTO obj)
        {
            try
            {
                var res = await jobTypeService.UpdateAsync(obj);
                return Ok(res);
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Failed to update jobType");
                return BadRequest();
            }
        }


        [HttpDelete("delete/{id}")]
        public async Task<IActionResult> Delete([Required] int id)
        {
            try
            {
                var res = await jobTypeService.SoftDeleteAsync(id);
                return Ok(res);
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Failed to delete jobType");
                return BadRequest();
            }
        }
        #endregion

    }
}
