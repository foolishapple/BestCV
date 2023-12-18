using BestCV.API.Utilities;
using BestCV.Application.Models.CandidateApplyJobs;
using BestCV.Application.Services.Interfaces;
using BestCV.Domain.Aggregates.CandidateApplyJobs;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BestCV.API.Controllers
{
    [Route("api/candidate-apply-job")]
    [ApiController]
    public class CandidateApplyJobController : ControllerBase
    {
        private readonly ICandidateApplyJobService _service;
        private readonly ILogger _logger;

        public CandidateApplyJobController(ILoggerFactory loggerFactory, ICandidateApplyJobService service)
        {
            _logger = loggerFactory.CreateLogger<CandidateApplyJobController>();
            _service = service;
        }

        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        [HttpPost("list-to-employer")]
        public async Task<IActionResult> ListToEmployer([FromBody]DTPagingCandidateApplyJobParameters parameters)
        {
            try
            {
                long employerId = this.GetLoggedInUserId();
                parameters.EmployerId = employerId;
                var result = await _service.DTPaging(parameters);
                return Ok(result);
            }
            catch (Exception e)
            {
                _logger.LogError(e, "Failed to get list paging candidate apply job to employer");
                return BadRequest();
            }
        }
        [HttpPost("add")]
        public async Task<IActionResult> Add([FromBody]InsertCandidateApplyJobDTO obj)
        {
            try
            {
                var result = await _service.CreateAsync(obj);
                return Ok(result);
            }
            catch (Exception e)
            {
                _logger.LogError(e, "Failed to create new candidate apply job");
                return BadRequest();
            }
        }
        [HttpPut("add-description")]
        public async Task<IActionResult> AddDescription([FromBody]AddNoteCandidateApplyJobDTO obj)
        {
            try
            {
                var result = await _service.UpdateDescription(obj);
                return Ok(result);
            }
            catch (Exception e)
            {
                _logger.LogError(e, "Failed to add description to candidate apply job");
                return BadRequest();
            }
        }
        [HttpPut("change-status")]
        public async Task<IActionResult> ChangeStatus([FromBody] ChangeStatusCandidateApplyJobDTO obj)
        {
            try
            {
                var result = await _service.ChangeStatus(obj);
                return Ok(result);
            }
            catch (Exception e)
            {
                _logger.LogError(e, "Failed to change candidate apply job status");
                return BadRequest();
            }
        }

        [HttpGet("apply-job/{BestCVd}")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public async Task<IActionResult> ApplyJob(long BestCVd)
        {
            try
            {
                var accountId = this.GetLoggedInUserId();
                if (accountId == 0)
                {
                    return BadRequest();
                }
                var data = await _service.ApplyJob(BestCVd, accountId);
                return Ok(data);
            }
            catch (Exception e)
            {
                _logger.LogError(e, "Failed to change candidate apply job status");
                return BadRequest();
            }
        }

        [HttpGet("get-total-cv-to-recruitment-campagin/{id}")]
        public async Task<IActionResult> GetTotalCVToEmployer(long id) {
            try
            {
                var response = await _service.CountTotalCVByEmployer(id);
                return Ok(response);
            }
            catch (Exception e)
            {
                _logger.LogError(e, "Failed to get total cv to recruiment campagin");
                return BadRequest();
            }
        }

        [HttpGet("get-total-cv-candidate-apply-to-recruitment-campagin/{id}")]
        public async Task<IActionResult> GetTotalCVCandidateApplyToEmployer(long id)
        {
            try
            {
                var response = await _service.CountTotalCVCandidateApplyByEmployer(id);
                return Ok(response);
            }
            catch (Exception e)
            {
                _logger.LogError(e, "Failed to get total cv candidate apply job to recruitment campagin");
                return BadRequest();
            }
        }

        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        [HttpPost("manage-list-job-apply")]
        public async Task<IActionResult> ListByCandidate([FromBody] DTPagingCandidateApplyJobParameters parameters)
        {
            try
            {
                long candidateId = this.GetLoggedInUserId();
                parameters.CandidateId = candidateId;
                var result = await _service.PagingByCandidateId(parameters);
                return Ok(result);
            }
            catch (Exception e)
            {
                _logger.LogError(e, "Failed to get list paging candidate apply job by candidateId");
                return BadRequest();
            }
        }
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        [HttpPut("employer-viewed/{id}")]
        public async Task<IActionResult> EmployerViewed(long id)
        {
            try
            {
                var result = await _service.EmployerViewed(id);
                return Ok(result);
            }
            catch (Exception e)
            {
                _logger.LogError(e, "Failed to update candidate apply job employer viewed status");
                return BadRequest();
            }
        }


        [HttpGet("get-total-cv-to-job/{id}")]
        public async Task<IActionResult> GetTotalCVToJob(long id)
        {
            try
            {
                var response = await _service.CountTotalToJob(id);
                return Ok(response);
            }
            catch (Exception e)
            {
                _logger.LogError(e, "Failed to get total cv to job");
                return BadRequest();
            }
        }

        [HttpGet("get-total-cv-candidate-apply-to-job/{id}")]
        public async Task<IActionResult> GetTotalCVCandidateApplyToJob(long id)
        {
            try
            {
                var response = await _service.CountTotalCVCandidateApplyToJob(id);
                return Ok(response);
            }
            catch (Exception e)
            {
                _logger.LogError(e, "Failed to get total cv candidate apply job to job");
                return BadRequest();
            }
        }

        [HttpGet("get-list-candidate-apply-to-job/{BestCVd}/{candidateApplyBestCVd}")]
        public async Task<IActionResult> GetListCandidateApplyToJob(long BestCVd, long candidateApplyBestCVd)
        {
            try
            {
                var response = await _service.GetListCandidateApplyToJob(BestCVd, candidateApplyBestCVd);
                return Ok(response);
            }
            catch (Exception e)
            {
                _logger.LogError(e, "Failed to get list cv candidate apply job");
                return BadRequest();
            }
        }

        [HttpGet("detail-by-id/{BestCVd}/{candidateApplyBestCVd}")]
        public async Task<IActionResult> DetailById(long BestCVd,long candidateApplyBestCVd)
        {
            try
            {
                var data = await _service.DetailById(BestCVd,candidateApplyBestCVd);    
                return Ok(data);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Failed to get detail cv candidate apply job");
                return BadRequest();
            }
        }
    }
}
