using BestCV.Application.Models.InterviewStatsus;
using BestCV.Application.Models.OrderStatus;
using BestCV.Application.Services.Implement;
using BestCV.Application.Services.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace BestCV.API.Controllers
{
    [Route("api/interview-status")]
    [ApiController]
    public class InterviewStatusController : BaseController
    {
        private readonly IInterviewStatusService interviewStatusService;
        private readonly ILogger<InterviewStatusController> logger;
        public InterviewStatusController(IInterviewStatusService interviewStatusService, ILoggerFactory loggerFactory)
        {
            this.interviewStatusService = interviewStatusService;
            this.logger = loggerFactory.CreateLogger<InterviewStatusController>();
        }

        #region CRUD

        [HttpGet("list")]
        public async Task<IActionResult> GetList()
        {
            try
            {
                var response = await interviewStatusService.GetAllAsync();
                return Ok(response);
            }
            catch (Exception e)
            {
                logger.LogError(e, $"Failed to get interview status");
                return BadRequest();
            }
        }


        [HttpGet("detail/{id}")]
        public async Task<IActionResult> Detail(int id)
        {
            try
            {
                var response = await interviewStatusService.GetByIdAsync(id);
                return Ok(response);
            }
            catch (Exception e)
            {

                logger.LogError(e, $"Failed to get interview status by id: {id}");
                return BadRequest();
            }
        }


        [HttpPost("add")]
        public async Task<IActionResult> Add([FromBody] InsertInterviewStatusDTO obj)
        {
            try
            {
                var response = await interviewStatusService.CreateAsync(obj);
                return Ok(response);
            }
            catch (Exception e)
            {

                logger.LogError(e, $"Failed to insert interview status: {obj}");
                return BadRequest();
            }
        }


        [HttpPut("update")]
        public async Task<IActionResult> Update([FromBody] UpdateInterviewStatusDTO obj)
        {
            try
            {

                var response = await interviewStatusService.UpdateAsync(obj);
                return Ok(response);

            }
            catch (Exception e)
            {

                logger.LogError(e, $"Failed to update interview status: {obj}");
                return BadRequest();
            }
        }


        [HttpDelete("delete")]
        public async Task<IActionResult> Delete([Required] int id)
        {
            try
            {
                var response = await interviewStatusService.SoftDeleteAsync(id);
                return Ok(response);

            }
            catch (Exception e)
            {

                logger.LogError(e, $"Failed to delete interview status by id: {id}");
                return BadRequest();
            }
        }


        #endregion
    }
}
