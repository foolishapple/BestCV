using BestCV.Application.Models.JobStatuses;
using BestCV.Application.Services.Interfaces;
using BestCV.Infrastructure.Repositories.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using System.Security.Principal;

namespace BestCV.API.Controllers
{
    [Route("api/job-status")]
    [ApiController]
    public class JobStatusController : BaseController
    {
        private readonly IJobStatusService _service;
        private readonly ILogger<JobStatusController> _logger;
        public JobStatusController(IJobStatusService service, ILoggerFactory loggerFactory)
        {
            _service = service;
            _logger = loggerFactory.CreateLogger<JobStatusController>();
        }

        #region CRUD
        /// <summary>
        ///  Author: HuyDQ
        /// CreatedTime : 27/07/2023
        /// Description : get All
        /// </summary>
        /// <returns></returns>
        [HttpGet("list")]
        public async Task<IActionResult> GetAll()
        {
            try
            {
                var res = await _service.GetAllAsync();
                return Ok(res);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Failed to get all JobStatus");
                return BadRequest();
            }
        }
        /// <summary>
        ///  Author: HuyDQ
        /// CreatedTime : 27/07/2023
        /// Description : Detail by id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("detail/{id}")]
        public async Task<IActionResult> Detail([Required] int id)
        {
            try
            {
                var response = await _service.GetByIdAsync(id);
                return Ok(response);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Failed to get detail JobStatus by id: {id}");
                return BadRequest();
            }
        }
        /// <summary>
        /// Author: HuyDQ
        /// CreatedTime : 27/07/2023
        /// Description : add JobStatus 
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        [HttpPost("add")]
        public async Task<IActionResult> Add([FromBody] InsertJobStatusDTO obj)
        {
            try
            {
                var res = await _service.CreateAsync(obj);
                return Ok(res);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Failed to add JobStatus");
                return BadRequest();
            }
        }
        /// <summary>
        /// Author: HuyDQ
        /// CreatedTime : 27/07/2023
        /// Description : update JobStatus 
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        [HttpPut("update")]
        public async Task<IActionResult> Update([FromBody] UpdateJobStatusDTO obj)
        {
            try
            {
                var res = await _service.UpdateAsync(obj);
                return Ok(res);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Failed to update JobStatus");
                return BadRequest();
            }
        }
        /// <summary>
        /// Author: HuyDQ
        /// CreatedTime : 27/07/2023
        /// Description : delete JobStatus 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>

        [HttpDelete("delete/{id}")]
        public async Task<IActionResult> Delete([Required] int id)
        {
            try
            {
                var res = await _service.SoftDeleteAsync(id);
                return Ok(res);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Failed to delete JobStatus");
                return BadRequest();
            }
        }
        #endregion


        #region Additional Resources

        #endregion
    }
}
