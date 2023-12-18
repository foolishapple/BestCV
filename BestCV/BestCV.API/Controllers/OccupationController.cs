using BestCV.Application.Models.Occupation;
using BestCV.Application.Services.Interfaces;
using BestCV.Domain.Entities;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace BestCV.API.Controllers
{
    [ApiController]
    [Route("api/occupation")]
    public class OccupationController : BaseController
    {
        private readonly IOccupationService service;
        private readonly ILogger<OccupationController> logger;

        public OccupationController(IOccupationService _service, ILoggerFactory loggerFactory)
        {
            logger = loggerFactory.CreateLogger<OccupationController>();
            service = _service;
        }

		#region CRUD
		/// <summary>
		/// Author: TrungHieuTr
		/// CreatedTime:27/07/2023
		/// </summary>
		/// <returns></returns>
		[HttpGet("list")]
        public async Task<IActionResult> GetAll()
        {
            try
            {
                var res = await service.GetAllAsync();
                if (res != null)
                {
                    return Ok(res);
                }
                return BadRequest();
            }
            catch (Exception ex)
			{
                logger.LogError(ex, "Failed to get all occupation");
                return BadRequest();

			}
        }
        /// <summary>
        /// Author: TrungHieuTr
        /// CreatedTime:27/07/2023
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("detail/{id}")]
        public async Task<IActionResult> Detail([Required] int id) 
        {
            try 
            {
                var res = await service.GetByIdAsync(id);
                return Ok(res);
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Failed to detail occupation");
                return BadRequest(ex);
            }
        }
		/// <summary>
		/// Author: TrungHieuTr
		/// CreatedTime:27/07/2023
		/// </summary>
		/// <param name="obj"></param>
		/// <returns></returns>
		[HttpPost("add")]
        public async Task<IActionResult> Add([FromBody] InsertOccupationDTO obj)
        {
            try
            {
                var res = await service.CreateAsync(obj);
                return Ok(res);
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Failed to add occupation");
                return BadRequest();
            }
        }
		/// <summary>
		/// Author: TrungHieuTr
		/// CreatedTime:27/07/2023
		/// </summary>
		/// <param name="id"></param>
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
                logger.LogError(ex, "Failed to delete occupation");
                return BadRequest();
            }
        }
		/// <summary>
		/// Author: TrungHieuTr
		/// CreatedTime:27/07/2023
		/// </summary>
		/// <param name="obj"></param>
		/// <returns></returns>
		[HttpPut("update")]
        public async Task<IActionResult> Update([FromBody] UpdateOccupationDTO obj)
        {
            try
            {
                var data = await service.UpdateAsync(obj);
                return Ok(data);
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Failed to update occupation");
				return BadRequest();
            }
        }
        #endregion
        #region ADDITIONAL_RESOURCES
        #endregion
    }
}
