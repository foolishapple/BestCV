using BestCV.Application.Models.CandidateApplyJobSources;
using BestCV.Application.Services.Implement;
using BestCV.Application.Services.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace BestCV.API.Controllers
{
    [Route("api/candidate-apply-job-source")]
    [ApiController]
    public class CandidateApplyJobSourceController : BaseController
    {
        private readonly ICandidateApplyJobSourceService _service;
        private readonly ILogger _logger;
        public CandidateApplyJobSourceController(ICandidateApplyJobSourceService service, ILoggerFactory loggerFactory)
        {
            _service = service;
            _logger = loggerFactory.CreateLogger<CandidateApplyJobSourceController>();
        }
        #region

        [HttpGet("list-all")]
        public async Task<IActionResult> ListAll()
        {
            try
            {
                var respone = await _service.GetAllAsync();
                return Ok(respone);
            }
            catch (Exception e)
            {
                _logger.LogError(e, "Failed to get list all candidate apply job source");
                return BadRequest();
            }
        }

        [HttpGet("detail/{id}")]
        public async Task<IActionResult> Detail([Required] int id)
        {
            try
            {
                var response = await _service.GetByIdAsync(id);
                return Ok(response);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Failed to get detail candidate apply job source by id: {id}");
                return BadRequest();
            }
        }

        [HttpPost("add")]
        public async Task<IActionResult> Add([FromBody] InsertCandidateApplyJobSourceDTO obj)
        {
            try
            {
                var res = await _service.CreateAsync(obj);
                return Ok(res);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Failed to add candidate apply job source");
                return BadRequest();
            }
        }

        [HttpPut("update")]
        public async Task<IActionResult> Update([FromBody] UpdateCandidateApplyJobSourceDTO obj)
        {
            try
            {
                var res = await _service.UpdateAsync(obj);
                return Ok(res);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Failed to update candidate apply job source");
                return BadRequest();
            }
        }


        [HttpDelete("delete/{id}")]
        public async Task<IActionResult> Delete([Required] int id)
        {
            try
            {
                var res = await _service.SoftDeleteAsync(id);
                return Ok(res);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Failed to delete candidate apply job source");
                return BadRequest();
            }
        }
        #endregion
    }
}
