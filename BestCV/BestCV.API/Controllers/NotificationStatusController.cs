using BestCV.Application.Models.NotificationStatuses;
using BestCV.Application.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace BestCV.API.Controllers
{
    
    [Route("api/notification-status")]
    [ApiController]
    public class NotificationStatusController : BaseController
    {
        private readonly INotificationStatusService _service;
        private readonly ILogger _logger;
        public NotificationStatusController(INotificationStatusService service, ILoggerFactory loggerFactory)
        {
            _service = service;
            _logger = loggerFactory.CreateLogger<NotificationStatusController>();
        }
        /// <summary>
        /// Author: TUNGTD
        /// Created: 08/08/2023
        /// Description: API GET get list all notification status
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
                _logger.LogError(e, "Failed to get list all notification status");
                return BadRequest();
            }
        }
        /// <summary>
        /// Author: TUNGTD
        /// Created: 08/08/2023
        /// Description: API GET get notification status detail by id
        /// </summary>
        /// <param name="id">notification status id</param>
        /// <returns></returns>
        [HttpGet("detail/{id}")]
        public async Task<IActionResult> Detail([Required(ErrorMessage = "Mã không được để trống")] int id)
        {
            try
            {
                var response = await _service.GetByIdAsync(id);
                return Ok(response);
            }
            catch (Exception e)
            {
                _logger.LogError(e, "Failed to get notification status detail");
                return BadRequest();
            }
        }
        /// <summary>
        /// Author: TUNGTD
        /// Created: 08/08/2023
        /// Description: API POST create new notification status
        /// </summary>
        /// <param name="obj">insert notification status DTO</param>
        /// <returns></returns>
        [HttpPost("add")]
        public async Task<IActionResult> Add([FromBody] InsertNotificationStatusDTO obj)
        {
            try
            {
                var response = await _service.CreateAsync(obj);
                return Ok(response);
            }
            catch (Exception e)
            {
                _logger.LogError(e, "Failed to create new notification status");
                return BadRequest();
            }
        }
        /// <summary>
        /// Author: TUNGTD
        /// Created: 08/08/2023
        /// Description: API PUT update notification status
        /// </summary>
        /// <param name="obj">update notification status DTO</param>
        /// <returns></returns>
        [HttpPut("update")]
        public async Task<IActionResult> Update([FromBody] UpdateNotificationStatusDTO obj)
        {
            try
            {
                var response = await _service.UpdateAsync(obj);
                return Ok(response);
            }
            catch (Exception e)
            {
                _logger.LogError(e, "Failed to update new notification status");
                return BadRequest();
            }
        }
        /// <summary>
        /// Author: TUNGTD
        /// Created: 08/08/2023
        /// Description: API DELETE soft delete notification status by id
        /// </summary>
        /// <param name="id">notification status id</param>
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
                _logger.LogError(e, "Failed to soft delete notification status");
                return BadRequest();
            }
        }
    }
}
