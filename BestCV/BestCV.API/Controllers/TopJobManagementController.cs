using BestCV.API.Utilities;
using BestCV.Application.Models.JobSuitable;
using BestCV.Application.Models.TopJobManagement;
using BestCV.Application.Services.Implement;
using BestCV.Application.Services.Interfaces;
using BestCV.Core.Entities;
using BestCV.Domain.Aggregates.Job;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace BestCV.API.Controllers
{
    [Route("api/top-job-management")]
    [ApiController]
    public class TopJobManagementController : BaseController
    {
        private readonly ITopJobManagementService service;
        private readonly ILogger<TopJobManagementService> logger;
        public TopJobManagementController(ITopJobManagementService _service, ILoggerFactory loggerFactory)
        {
            service = _service;
            logger = loggerFactory.CreateLogger<TopJobManagementService>();
        }

        #region CRUD

        [HttpGet("detail/{id}")]
        public async Task<IActionResult> Detail([Required] int id)
        {
            try
            {
                var response = await service.GetByIdAsync(id);
                return Ok(response);
            }
            catch (Exception ex)
            {
                logger.LogError(ex, $"Failed to get top job managemenet by id: {id}");
                return BadRequest();
            }
        }


        [HttpPost("add")]
        public async Task<IActionResult> Add([FromBody] InsertTopJobManagementDTO model)
        {
            try
            {
                var data = await service.CreateAsync(model);
                return Ok(data);
            }
            catch (Exception ex)
            {
                logger.LogError(ex, $"Failed to add top job managemenet");
                return BadRequest();
            }
        }


        [HttpPut("update")]
        public async Task<IActionResult> Update([FromBody] UpdateTopJobManagementDTO model)
        {
            try
            {
                var data = await service.UpdateAsync(model);
                return Ok(data);
            }
            catch (Exception ex)
            {
                logger.LogError(ex, $"Failed to update top job managemenet");
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
                logger.LogError(ex, $"Failed to soft delete top job managemenet");
                return BadRequest();
            }
        }


        [HttpGet("list")]
        public async Task<IActionResult> GetList()
        {
            try
            {
                var data = await service.GetAllAsync();
                return Ok(data);
            }
            catch (Exception ex)
            {
                logger.LogError(ex, $"Failed to get list top job managemenet");
                return BadRequest();
            }
        }
        #endregion


        [HttpGet("list-aggregates")]
        public async Task<IActionResult> GetListAggregates()
        {
            try
            {
                var data = await service.ListAggregatesAsync();
                return Ok(data);
            }
            catch (Exception ex)
            {
                logger.LogError(ex, $"Failed to get list top job management aggregates");
                return BadRequest();
            }
        }


        [HttpGet("list-job-selected")]
        public async Task<IActionResult> ListJobSelected()
        {
            try
            {
                var data = await service.ListJobSelected();
                return Ok(BestCVResponse.Success(data));
            }
            catch (Exception e)
            {
                logger.LogError(e, "Failed to get list selected job");
                return BadRequest();
            }
        }

        [HttpPost("search-top-management")]
        public async Task<IActionResult> SearchTopManagement([FromBody] SearchJobWithServiceParameters parameter)
        {
            try
            {
                parameter.CandidateId = this.GetLoggedInUserId();
                var data = await service.SearchingManagementJob(parameter);
                return Ok(data);
            }
            catch (Exception e)
            {
                logger.LogError(e, "Failed to search top management");
                return BadRequest();
            }
        }
    }
}
