using BestCV.API.Utilities;
using BestCV.Application.Models.TopJobExtra;
using BestCV.Application.Models.TopJobUrgent;
using BestCV.Application.Services.Interfaces;
using BestCV.Core.Entities;
using BestCV.Domain.Aggregates.Job;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace BestCV.API.Controllers
{
    [Route("api/top-job-urgent")]
    [ApiController]
    public class TopJobUrgentController : BaseController
    {
        private readonly ITopJobUrgentService service;
        private readonly ILogger<TopJobUrgentController> logger;
        public TopJobUrgentController(ITopJobUrgentService _service, ILoggerFactory loggerFactory)
        {
            service = _service;
            logger = loggerFactory.CreateLogger<TopJobUrgentController>();
        }


        [HttpGet("list")]
        public async Task<IActionResult> GetList()
        {
            try
            {
                var data = await service.ListTopJobUrgent();
                return Ok(data);

            }
            catch (Exception ex)
            {
                logger.LogError(ex, $"Failed to get list top job urgent");
                return BadRequest();
            }
        }


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
                logger.LogError(ex, $"Failed to get top job urgent by id: {id}");
                return BadRequest();
            }
        }


        [HttpPost("add")]
        public async Task<IActionResult> Add([FromBody] InsertTopJobUrgentDTO model)
        {
            try
            {
                var data = await service.CreateAsync(model);
                return Ok(data);
            }
            catch (Exception ex)
            {
                logger.LogError(ex, $"Failed to add top job urgent");
                return BadRequest();
            }
        }


        [HttpPut("update")]
        public async Task<IActionResult> Update([FromBody] UpdateTopJobUrgentDTO model)
        {
            try
            {
                var data = await service.UpdateAsync(model);
                return Ok(data);
            }
            catch (Exception ex)
            {
                logger.LogError(ex, $"Failed to update top job urgent");
                return BadRequest();
            }
        }

 
        [HttpDelete("delete")]
        public async Task<IActionResult> Delete([Required] int id)
        {
            try
            {
                var data = await service.SoftDeleteAsync(id);
                return Ok(data);
            }
            catch (Exception ex)
            {
                logger.LogError(ex, $"Failed to soft delete top job urgent");
                return BadRequest();
            }
        }


        [HttpPost]
        [Route("change-order-slide")]
        public async Task<IActionResult> ChangeOrderSort([FromBody] ChangeOrderSortTopJobUrgentDTO model)
        {
            try
            {
                var isSuccess = await service.ChangeOrderSort(model);
                return Ok(BestCVResponse.Success(isSuccess));

            }
            catch (Exception ex)
            {
                logger.LogError("Fail when change order sort", ex);
                return BadRequest();
            }
        }

        [HttpGet("get-top-urgent-job")]
        public async Task<IActionResult> GetTopUrgentJobHomePage()
        {
            try
            {
                var data = await service.ListTopJobUrgentShowOnHomePageAsync();
                return Ok(data);
            }
            catch (Exception ex)
            {
                logger.LogError(ex, $"Failed get top job urgent");
                return BadRequest();
            }
        }

        [HttpPost("search-urgent-job")]
        public async Task<IActionResult> SearchUrgentJob([FromBody] SearchJobWithServiceParameters parameter)
        {
            try
            {
                parameter.CandidateId = this.GetLoggedInUserId();
                var data = await service.SearchingUrgentJob(parameter);
                return Ok(data);
            }
            catch (Exception ex)
            {
                logger.LogError(ex, $"Failed search top job urgent");
                return BadRequest();
            }
        }
    }
}
