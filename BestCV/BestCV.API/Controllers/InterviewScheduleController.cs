using BestCV.API.Utilities;
using BestCV.Application.Models.InterviewSchdule;
using BestCV.Application.Services.Interfaces;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BestCV.API.Controllers
{
    [Route("api/interview-schedule")]
    [ApiController]
    public class InterviewScheduleController : BaseController
    {
        private readonly IInterviewScheduleService interviewScheduleService;
        private readonly ILogger<InterviewScheduleController> logger;
        public InterviewScheduleController(IInterviewScheduleService interviewScheduleService, ILoggerFactory loggerFactory)
        {
            this.interviewScheduleService = interviewScheduleService;
            this.logger = loggerFactory.CreateLogger<InterviewScheduleController>();
        }

        #region CRUD

 
        [HttpGet("getListInterviewByCandiddateId")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public async Task<IActionResult> ListInterviewByCandidateId()
        {
            var candidateId = this.GetLoggedInUserId();
            try
            {
                var response = await interviewScheduleService.GetListByCandidateId(candidateId);
                return Ok(response);
            }
            catch (Exception e)
            {
                logger.LogError(e, $"Failed to get list interview Schedule by candidateId: {candidateId}");
                return BadRequest();
            }
        }


        [HttpGet("getListInterviewByEmployerId")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public async Task<IActionResult> ListInterviewByEmployerId()
        {
            var employerId = this.GetLoggedInUserId();
            try
            {
                var response = await interviewScheduleService.GetListByEmployerId(employerId);
                return Ok(response);
            }
            catch (Exception e)
            {
                logger.LogError(e, $"Failed to get list interview Schedule by employerId: {employerId}");
                return BadRequest();
            }
        }


        [HttpPost("add")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public async Task<IActionResult> Add([FromBody] InsertInterviewScheduleDTO obj)
        {
            try
            {
                var response = await interviewScheduleService.CreateAsync(obj);
                return Ok(response);
            }
            catch (Exception e)
            {
                logger.LogError(e, $"Failed to create interview Schedule");
                return BadRequest();
            }
        }

        #endregion
    }
}
