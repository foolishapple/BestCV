using BestCV.API.Utilities;
using BestCV.Application.Services.Interfaces;
using BestCV.Core.Entities;
using BestCV.Domain.Aggregates.CandidateApplyJobs;
using BestCV.Domain.Aggregates.EmployerNotification;
using BestCV.Domain.Entities;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BestCV.API.Controllers
{
    [ApiController]
    [Route("api/employer-notification")]
    public class EmployerNotificationController : BaseController
    {
        private readonly IEmployerNotificationService service;
        private readonly ILogger<EmployerNotificationController> logger;

        public EmployerNotificationController(ILoggerFactory loggerFactory, IEmployerNotificationService _notificationService)
        {
            service = _notificationService;
            logger = loggerFactory.CreateLogger<EmployerNotificationController>();
        }


        #region CRUD


        [HttpGet("list/{id}")]
        public async Task<IActionResult> GetAllNotificationByEmployer(long id)
        {
            try
            {
                var data = await service.GetAllByEmployerId(id);
                if (data.Count > 0)
                {
                    return Ok(BestCVResponse.Success(data));
                }
                else
                {
                    return Ok(BestCVResponse.NotFound($"Failed to get list notification of employer id:{id}", id));
                }
            }
            catch (Exception e)
            {
                logger.LogError(e, $"Failed to get list notification of employer id:{id}");
                return BadRequest(e);
            }
        }




        [HttpDelete("delete/{id}")]
        public async Task<IActionResult> DeleteNotification(long id)
        {
            try
            {
                var result = await service.SoftDeleteAsync(id);
                return Ok(result);
             }
            catch (Exception e)
            {
                logger.LogError(e, $"Failed to delete notification id: {id}");
                return BadRequest(e);
            }
        }
        [HttpGet("detail/{id}")]
        public async Task<IActionResult> Detail(long id)
        {
            try
            {
                var response = await service.GetByIdAsync(id);
                return Ok(response);
            }
            catch (Exception e)
            {
                logger.LogError(e, "Failed to get employer notification detail");
                return BadRequest();
            }
        }
        #endregion
        #region ADDITTIONAL_RESOURCES
 
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        [HttpPost("list-employer-notification")]
        public async Task<IActionResult> ListEmployerNotificationByEmployerIdAsync([FromBody] EmployerNotificationParameter parameters)
        {
            try
            {
                long employerId = this.GetLoggedInUserId();
                
                var result = await service.DTPaging(parameters, employerId);
                return Ok(result);
            }
            catch (Exception e)
            {
                logger.LogError(e, "Failed to get list paging emplpoyer notification");
                return BadRequest();
            }
        }

        [HttpPut("MakeAsRead/{id}")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public async Task<IActionResult> MakeAsRead(long id)
        {
            try
            {
                await service.MakeAsRead(id);
                return Ok(BestCVResponse.Success());
            }
            catch (Exception e)
            {
                logger.LogError(e, "Failed to make as read notification by id");
                return BadRequest();
            }
        }
        [HttpGet("GetUnreadTotal")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]

        public async Task<IActionResult> GetUnreadTotal()
        {
            try
            {
                var id = this.GetLoggedInUserId();
                var total = await service.CountUnreadByEmployerId(id);
                return Ok(total);
            }
            catch (Exception e)
            {
                logger.LogError(e, "Failed to get total unread notification by employer id");
                return BadRequest();
            }
        }
        [HttpGet("listRecent")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public async Task<IActionResult> ListRecented()
        {
            try
            {
                var id = this.GetLoggedInUserId();
                var data = await service.ListRecented(id);
                return Ok(data);
            }
            catch (Exception e)
            {
                logger.LogError(e, "Failed to get list recented notification by account id");
                return BadRequest();
            }
        }
        #endregion

    }
}
