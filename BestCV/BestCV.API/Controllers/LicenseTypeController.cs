using BestCV.Application.Models.LicenseType;
using BestCV.Application.Models.Occupation;
using BestCV.Application.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace BestCV.API.Controllers
{
    [ApiController]
    [Route("api/license-type")]
    public class LicenseTypeController : BaseController
    {
        private readonly ILicenseTypeService service;
        private readonly ILogger<LicenseTypeController> logger;
        public LicenseTypeController(ILicenseTypeService _service, ILoggerFactory loggerFactory)
        {
            logger = loggerFactory.CreateLogger<LicenseTypeController>();
            service = _service;
        }

        #region CRUD
        /// <summary>
		/// Author: TrungHieuTr
		/// CreatedTime:07/08/2023
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
                logger.LogError(ex, "Failed to get all license type");
                return BadRequest();

            }
        }
        /// <summary>
        /// Author: TrungHieuTr
        /// CreatedTime:07/08/2023
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
                logger.LogError(ex, "Failed to detail license type");
                return BadRequest(ex);
            }
        }
        /// <summary>
        /// Author: TrungHieuTr
        /// CreatedTime:07/08/2023
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        [HttpPost("add")]
        public async Task<IActionResult> Add([FromBody] InsertLicenseTypeDTO obj)
        {
            try
            {
                var res = await service.CreateAsync(obj);
                return Ok(res);
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Failed to add license type");
                return BadRequest();
            }
        }
        /// <summary>
        /// Author: TrungHieuTr
        /// CreatedTime:07/08/2023
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
                logger.LogError(ex, "Failed to delete license type");
                return BadRequest();
            }
        }
        /// <summary>
        /// Author: TrungHieuTr
        /// CreatedTime:07/08/2023
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        [HttpPut("update")]
        public async Task<IActionResult> Update([FromBody] UpdateLicenseTypeDTO obj)
        {
            try
            {
                var data = await service.UpdateAsync(obj);
                return Ok(data);
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Failed to update license type");
                return BadRequest();
            }
        }
        #endregion
        #region ADDITIONAL_RESOURCES
        #endregion
    }
}
