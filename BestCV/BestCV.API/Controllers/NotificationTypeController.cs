using BestCV.Application.Models.MultimediaType;
using BestCV.Application.Models.NotificationType;
using BestCV.Application.Services.Implement;
using BestCV.Application.Services.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace BestCV.API.Controllers
{
    [Route("api/notification-type")]
    [ApiController]
    public class NotificationTypeController : BaseController
    {
        private readonly INotificationTypeService notificationService;
        private readonly ILogger<NotificationTypeController> logger;
        public NotificationTypeController(INotificationTypeService notificationService, ILoggerFactory loggerFactory)
        {
            this.notificationService = notificationService;
            this.logger = loggerFactory.CreateLogger<NotificationTypeController>();
        }

        #region CRUD

        [HttpGet("list")]
        public async Task<IActionResult> GetList()
        {
            try
            {
                var response = await notificationService.GetAllAsync();
                return Ok(response);
            }
            catch (Exception e)
            {
                logger.LogError(e, $"Failed to get notification type");
                return BadRequest();
            }
        }


        [HttpGet("detail/{id}")]
        public async Task<IActionResult> Detail([Required] int id)
        {
            try
            {
                var response = await notificationService.GetByIdAsync(id);
                return Ok(response);
            }
            catch (Exception e)
            {

                logger.LogError(e, $"Failed to get notification type by id: {id}");
                return BadRequest();
            }
        }


        [HttpPost("add")]
        public async Task<IActionResult> Add([FromBody] InsertNotificationTypeDTO obj)
        {
            try
            {
                var response = await notificationService.CreateAsync(obj);
                return Ok(response);
            }
            catch (Exception e)
            {

                logger.LogError(e, $"Failed to insert notification type: {obj}");
                return BadRequest();
            }
        }


        [HttpPut("update")]
        public async Task<IActionResult> Update([FromBody] UpdateNotificationTypeDTO obj)
        {
            try
            {

                var response = await notificationService.UpdateAsync(obj);
                return Ok(response);

            }
            catch (Exception e)
            {

                logger.LogError(e, $"Failed to update notification type: {obj}");
                return BadRequest();
            }
        }


        [HttpDelete("delete")]
        public async Task<IActionResult> Delete([Required] int id)
        {
            try
            {
                var response = await notificationService.SoftDeleteAsync(id);
                return Ok(response);

            }
            catch (Exception e)
            {

                logger.LogError(e, $"Failed to delete notification type by id: {id}");
                return BadRequest();
            }
        }


        #endregion
    }
}
