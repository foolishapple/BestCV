using BestCV.Application.Models.SystemConfigs;
using BestCV.Application.Services.Interfaces;
using BestCV.Core.Entities;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace BestCV.API.Controllers
{
    [ApiController]
    [Route("api/system-config")]
    public class SystemConfigController : BaseController
    {
        private readonly ISystemConfigService _service;
        private readonly ILogger _logger;
        public SystemConfigController(ISystemConfigService service, ILoggerFactory loggerFactory)
        {
            _service = service;
            _logger = loggerFactory.CreateLogger<SystemConfigController>();
        }
        /// <summary>
        /// Author: TUNGTD
        /// Created: 08/08/2023
        /// Description: API GET get list all system config
        /// </summary>
        /// <returns></returns>
        [HttpGet("list")]
        public async Task<IActionResult> List()
        {
            try
            {
                var response = await _service.GetAllAsync();
                return Ok(response);
            }
            catch (Exception e)
            {
                _logger.LogError(e, "Failed to get list all system config");
                return BadRequest();
            }
        }
        /// <summary>
        /// Author: TUNGTD
        /// Created: 08/08/2023
        /// Description: API GET get system config detail by id
        /// </summary>
        /// <param name="id">system config id</param>
        /// <returns></returns>
        [HttpGet("detail/{id}")]
        public async Task<IActionResult> Detail([Required(ErrorMessage = "Mã không được để trống")]int id)
        {
            try
            {
                var response = await _service.GetByIdAsync(id);
                return Ok(response);
            }
            catch (Exception e)
            {
                _logger.LogError(e, "Failed to get system config detail");
                return BadRequest();
            }
        }
        /// <summary>
        /// Author: TUNGTD
        /// Created: 08/08/2023
        /// Description: API POST create new system config
        /// </summary>
        /// <param name="obj">insert system config DTO</param>
        /// <returns></returns>
        [HttpPost("add")]
        public async Task<IActionResult> Add([FromBody] InsertSystemConfigDTO obj)
        {
            try
            {
                var response = await _service.CreateAsync(obj);
                return Ok(response);
            }
            catch (Exception e)
            {
                _logger.LogError(e, "Failed to create new system config");
                return BadRequest();
            }
        }
        /// <summary>
        /// Author: TUNGTD
        /// Created: 08/08/2023
        /// Description: API PUT update system config
        /// </summary>
        /// <param name="obj">update system config DTO</param>
        /// <returns></returns>
        [HttpPut("update")]
        public async Task<IActionResult> Update([FromBody] UpdateSystemConfigDTO obj)
        {
            try
            {
                var response = await _service.UpdateAsync(obj);
                return Ok(response);
            }
            catch (Exception e)
            {
                _logger.LogError(e, "Failed to update new system config");
                return BadRequest();
            }
        }
        /// <summary>
        /// Author: TUNGTD
        /// Created: 08/08/2023
        /// Description: API DELETE soft delete system config by id
        /// </summary>
        /// <param name="id">system config id</param>
        /// <returns></returns>
        [HttpDelete("delete/{id}")]
        public async Task<IActionResult> Delete([Required(ErrorMessage = "Mã không được để trống")] int id)
        {
            try
            {
                var response = await _service.SoftDeleteAsync(id);
                return Ok(response);
            }
            catch (Exception e)
            {
                _logger.LogError(e, "Failed to soft delete system config");
                return BadRequest();
            }
        }
    }
}
