using BestCV.Application.Models.JobCategory;
using BestCV.Application.Services.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace BestCV.API.Controllers
{
    [Route("api/job-category")]
    [ApiController]
    public class JobCategoryController : BaseController
    {
        private IJobCategoryService jobCategoryService;
        private ILogger<JobCategoryController> logger;
        public JobCategoryController(IJobCategoryService jobCategoryService, ILoggerFactory loggerFactory)
        {
            this.jobCategoryService = jobCategoryService;
            this.logger = loggerFactory.CreateLogger<JobCategoryController>();
        }

        #region CRUD

        [HttpGet("list")]
        public async Task<IActionResult> GetList()
        {
            try
            {
                var response = await jobCategoryService.GetAllAsync();
                return Ok(response);
            }
            catch (Exception e)
            {
                logger.LogError(e, $"Failed to get job category");
                return BadRequest();
            }
        }


        [HttpGet("detail/{id}")]
        public async Task<IActionResult> Detail([Required] int id)
        {
            try
            {
                var response = await jobCategoryService.GetByIdAsync(id);
                return Ok(response);
            }
            catch (Exception e)
            {

                logger.LogError(e, $"Failed to get job category by id: {id}");
                return BadRequest();
            }
        }

 
        [HttpPost("add")]
        public async Task<IActionResult> Add([FromBody] InsertJobCategoryDTO obj)
        {
            try
            {
                var response = await jobCategoryService.CreateAsync(obj);
                return Ok(response);
            }
            catch (Exception e)
            {

                logger.LogError(e, $"Failed to insert job category: {obj}");
                return BadRequest();
            }
        }


        [HttpPut("update")]
        public async Task<IActionResult> Update([FromBody] UpdateJobCategoryDTO obj)
        {
            try
            {

                var response = await jobCategoryService.UpdateAsync(obj);
                return Ok(response);

            }
            catch (Exception e)
            {

                logger.LogError(e, $"Failed to update job category: {obj}");
                return BadRequest();
            }
        }

        [HttpDelete("delete")]
        public async Task<IActionResult> Delete([Required] int id)
        {
            try
            {
                var response = await jobCategoryService.SoftDeleteAsync(id);
                return Ok(response);

            }
            catch (Exception e)
            {

                logger.LogError(e, $"Failed to delete job category by id: {id}");
                return BadRequest();
            }
        }


        #endregion
    }
}
