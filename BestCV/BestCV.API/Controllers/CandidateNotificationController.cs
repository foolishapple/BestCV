 using BestCV.API.Controllers;
using BestCV.API.Utilities;
using BestCV.Application.Services.Interfaces;
using BestCV.Core.Entities;
using BestCV.Domain.Aggregates.CandidateNotification;
using BestCV.Domain.Entities;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Controllers;

namespace BestCV.Web.Controllers
{
    [ApiController]
    [Route("api/candidate-notification")]
    public class CandidateNotificationController : BaseController
    {
        private readonly ICandidateNotificationService service;
        private readonly ILogger<CandidateNotificationController> logger;
        public CandidateNotificationController(ICandidateNotificationService _service, ILoggerFactory loggerFactory)
        {
            logger = loggerFactory.CreateLogger<CandidateNotificationController>();
            service = _service;
        }
        #region CRUD
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
                logger.LogError(e, "Failed to get candidate notification detail");
                return BadRequest();
            }
        }
        #endregion

        #region ADDITIONAL_RESOURCES

        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        [HttpPost("list-candidate-notification")]
        public async Task<IActionResult> ListCandidateNotificationByCandidateIdAsync([FromBody] CandidateNotificationParameter parameters)
        {
            try
            {
                long candidateId = this.GetLoggedInUserId();

                var result = await service.DTPaging(parameters, candidateId);
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
                var total = await service.CountUnreadByCandidateId(id);
                return Ok(total);
            }
            catch (Exception e)
            {
                logger.LogError(e, "Failed to get total unread notification by candidate id");
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
