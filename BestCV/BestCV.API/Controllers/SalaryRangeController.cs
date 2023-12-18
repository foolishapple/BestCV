using BestCV.Application.Models.SalaryRange;
using BestCV.Application.Models.SalaryType;
using BestCV.Application.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace BestCV.API.Controllers
{
    [Route("api/salary-range")]
    [ApiController]
    public class SalaryRangeController : BaseController
    {
        private readonly ISalaryRangeService service;
        private readonly ILogger<SalaryRangeController> logger;
        public SalaryRangeController(ISalaryRangeService _service, ILoggerFactory loggerFactory)
        {
            service = _service;
            logger = loggerFactory.CreateLogger<SalaryRangeController>();
        }


        #region CRUD
        /// <summary>
        /// Author : ThanhND
        /// CreatedTime : 02/08/2023
        /// </summary>
        /// <param name="id">salaryRangeId</param>
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
                logger.LogError(ex, $"Failed to get salary range by id: {id}");
                return BadRequest();
            }
        }

        /// <summary>
        /// Author : ThanhND
        /// CreatedTime : 02/08/2023
        /// </summary>
        /// <param name="model">InsertSalaryRangeDTO</param>
        /// <returns></returns>
        [HttpPost("add")]
        public async Task<IActionResult> Add([FromBody] InsertSalaryRangeDTO model)
        {
            try
            {
                var data = await service.CreateAsync(model);
                return Ok(data);
            }
            catch (Exception ex)
            {
                logger.LogError(ex, $"Failed to add salary range");
                return BadRequest();
            }
        }

        /// <summary>
        /// Author : ThanhND
        /// CreatedTime : 02/08/2023
        /// </summary>
        /// <param name="model">UpdateSalaryRangeDTO</param>
        /// <returns></returns>
        [HttpPut("update")]
        public async Task<IActionResult> Update([FromBody] UpdateSalaryRangeDTO model)
        {
            try
            {
                var data = await service.UpdateAsync(model);
                return Ok(data);
            }
            catch (Exception ex)
            {
                logger.LogError(ex, $"Failed to update salary range");
                return BadRequest();
            }
        }

        /// <summary>
        /// Author : ThanhND
        /// CreatedTime : 02/08/2023
        /// </summary>
        /// <param name="id">salaryRangeId</param>
        /// <returns></returns>
        [HttpDelete("delete/{id}")]
        public async Task<IActionResult> Delete([Required] int id)
        {
            try
            {
                var data = await service.SoftDeleteAsync(id);
                return Ok(data);
            }
            catch (Exception ex)
            {
                logger.LogError(ex, $"Failed to soft delete salary range");
                return BadRequest();
            }
        }

        /// <summary>
        /// Author : ThanhND
        /// CreatedTime : 02/08/2023
        /// </summary>
        /// <returns>List SalaryRange</returns>
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
                logger.LogError(ex, $"Failed to get list salary range");
                return BadRequest();
            }
        }
        #endregion


        #region Additional Resources

        #endregion
    }
}
