using BestCV.Application.Models.CandidateApplyJobStatuses;
using BestCV.Application.Services.Interfaces;
using BestCV.Domain.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.ComponentModel.DataAnnotations;

namespace BestCV.API.Controllers
{
    [ApiController]
    [Route("api/candidate-apply-job-status")]
    public class CandidateApplyJobStatusController : BaseController, IBaseController<CandidateApplyJobStatus, InsertCandidateApplyJobStatusDTO, UpdateCandidateApplyJobStatusDTO,CandidateApplyJobStatusDTO, int>
    {
        private readonly ICandidateApplyJobStatusService _service;
        private readonly ILogger _logger;
        public CandidateApplyJobStatusController(ICandidateApplyJobStatusService service, ILoggerFactory loggerFactory)
        {
            _service = service;
            _logger = loggerFactory.CreateLogger<CandidateApplyJobStatusController>();
        }
        [HttpPost("add")]
        public async Task<IActionResult> Add([FromBody] InsertCandidateApplyJobStatusDTO obj)
        {
            try
            {
                var response = await _service.CreateAsync(obj);
                return Ok(response);
            }
            catch (Exception e)
            {
                _logger.LogError(e, "Failed to add new candidate apply job status");
                return BadRequest();
            }
        }
        [HttpPost("add-many")]
        public async Task<IActionResult> AddMany([FromBody] IEnumerable<InsertCandidateApplyJobStatusDTO> objs)
        {
            try
            {
                var response = await _service.CreateListAsync(objs);
                return Ok(response);
            }
            catch (Exception e)
            {
                _logger.LogError(e, "Failed to add new candidate apply job status");
                return BadRequest();
            }
        }
        [HttpDelete("delete/{id}")]
        public async Task<IActionResult> Delete([Required(ErrorMessage = "Id không được để trống.")] int id)
        {
            try
            {
                var response = await _service.SoftDeleteAsync(id);
                return Ok(response);
            }
            catch (Exception e)
            {
                _logger.LogError(e, "Failed to delete candidate apply job status");
                return BadRequest();
            }
        }
        [HttpGet("detail/{id}")]
        public async Task<IActionResult> Detail([Required(ErrorMessage = "Id không được để trống.")] int id)
        {
            try
            {
                var response = await _service.GetByIdAsync(id);
                return Ok(response);
            }
            catch (Exception e)
            {
                _logger.LogError(e, "Failed to get candidate apply job status");
                return BadRequest();
            }
        }
        [HttpGet("list-all")]
        public async Task<IActionResult> ListAll()
        {
            try
            {
                var response = await _service.GetAllAsync();
                return Ok(response);
            }
            catch (Exception e)
            {
                _logger.LogError(e, "Failed to get list all candidate apply job status");
                return BadRequest();
            }
        }
        [HttpPut("update")]
        public async Task<IActionResult> Update([FromBody] UpdateCandidateApplyJobStatusDTO obj)
        {
            try
            {
                var response = await _service.UpdateAsync(obj);
                return Ok(response);
            }
            catch (Exception e)
            {
                _logger.LogError(e, "Failed to get list all candidate apply job status");
                return BadRequest();
            }
        }
        [HttpPut("update-many")]
        public async Task<IActionResult> UpdateMany([FromBody] IEnumerable<UpdateCandidateApplyJobStatusDTO> objs)
        {
            try
            {
                var response = await _service.UpdateListAsync(objs);
                return Ok(response);
            }
            catch (Exception e)
            {
                _logger.LogError(e, "Failed to get list all candidate apply job status");
                return BadRequest();
            }
        }
    }
}
