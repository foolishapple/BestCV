using BestCV.API.Utilities;
using BestCV.Application.Models.Menu;
using BestCV.Application.Models.TopJobExtra;
using BestCV.Application.Services.Implement;
using BestCV.Application.Services.Interfaces;
using BestCV.Core.Entities;
using BestCV.Core.Utilities;
using BestCV.Domain.Aggregates.Job;
using Microsoft.AspNetCore.Mvc;
using System;
using System.ComponentModel.DataAnnotations;

namespace BestCV.API.Controllers
{
    [Route("api/top-job-extra")]
    [ApiController]
    public class TopJobExtraController : BaseController
    {
        private readonly ITopJobExtraService service;
        private readonly ILogger<TopJobExtraController> logger;
        public TopJobExtraController(ITopJobExtraService _service, ILoggerFactory loggerFactory)
        {
            service = _service;
            logger = loggerFactory.CreateLogger<TopJobExtraController>();
        }


        [HttpGet("list")]
        public async Task<IActionResult> GetList()
        {
            try
            {
                var data = await service.ListTopJobExtra();
                return Ok(data);

            }
            catch (Exception ex)
            {
                logger.LogError(ex, $"Failed to get list top job extra");
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
                logger.LogError(ex, $"Failed to get top job extra by id: {id}");
                return BadRequest();
            }
        }


        [HttpPost("add")]
        public async Task<IActionResult> Add([FromBody] InsertTopJobExtraDTO model)
        {
            try
            {
                var data = await service.CreateAsync(model);
                return Ok(data);
            }
            catch (Exception ex)
            {
                logger.LogError(ex, $"Failed to add top job extra");
                return BadRequest();
            }
        }


        [HttpPut("update")]
        public async Task<IActionResult> Update([FromBody] UpdateTopJobExtraDTO model)
        {
            try
            {
                var data = await service.UpdateAsync(model);
                return Ok(data);
            }
            catch (Exception ex)
            {
                logger.LogError(ex, $"Failed to update top job extra");
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
                logger.LogError(ex, $"Failed to soft delete top job extra");
                return BadRequest();
            }
        }

 
        [HttpPost]
        [Route("change-order-slide")]
        public async Task<IActionResult> ChangeOrderSort([FromBody] ChangeOrderSortDTO model)
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

        [HttpGet]
        [Route("get-list-top-job-extra")]
        public async Task<IActionResult> GetListTopJobExtra()
        {
            try
            {
                var data = await service.ListTopJobExtraShowOnHomePageAsync();
                return Ok(data);

            }
            catch (Exception ex)
            {
                logger.LogError("Fail when get list top job extra", ex);
                return BadRequest();
            }
        }

        [HttpPost]
        [Route("searching-top-extra")]
        public async Task<IActionResult> SearchingJobExtra([FromBody] SearchJobWithServiceParameters parameters)
        {
            try
            {
                parameters.CandidateId = this.GetLoggedInUserId();
                var data = await service.SearchingFeatureJob(parameters);
                return Ok(data);
            }
            catch (Exception ex)
            {
                logger.LogError("Fail when get search top job extra", ex);
                return BadRequest();
            }
        }
    }
}
