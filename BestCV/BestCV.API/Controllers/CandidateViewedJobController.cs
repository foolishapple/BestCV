using BestCV.API.Utilities;
using BestCV.Application.Services.Interfaces;
using BestCV.Core.Entities;
using BestCV.Core.Utilities;
using BestCV.Domain.Aggregates.CandidateSaveJob;
using BestCV.Domain.Aggregates.CandidateViewedJob;
using BestCV.Domain.Aggregates.CandidateViewJobs;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BestCV.API.Controllers
{
    [Route("api/candidate-viewed-job")]
    [ApiController]
    public class CandidateViewedJobController : BaseController
    {
        private readonly ICandidateViewedJobService service;
        private readonly ILogger<CandidateViewedJobController> logger;

        public CandidateViewedJobController(ICandidateViewedJobService _service, ILoggerFactory loggerFactory)
        {
            service = _service;
            logger = loggerFactory.CreateLogger<CandidateViewedJobController>();
        }

        #region Additional Resources
        [HttpPost("get-list-viewed-job")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public async Task<IActionResult> GetListJobByCandidateId()
        {
            try
            {
                var candidateId = this.GetLoggedInUserId();
                var data = await service.GetListViewedJobByCanddiateId(candidateId);
                
                return Ok(data);
            }
            catch (Exception ex)
            {
                logger.LogError(ex, ex.Message);
                return BadRequest(ex.Message + " - " + ex.InnerException);
            }
        }

        [HttpDelete("delete/{id}")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public async Task<IActionResult> DeleteCandidateViewedJob(long id)
        {
            try
            {
                var result = await service.SoftDeleteAsync(id);
                if (result.IsSucceeded)
                {
                    return Ok(BestCVResponse.Success());
                }
                else
                {
                    return Ok(BestCVResponse.Error());
                }
            }
            catch (Exception e)
            {
                logger.LogError(e, $"Failed to delete candidate save job id: {id}");
                return BadRequest(e);
            }
        }
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        [HttpPost("manage-list-job-viewed")]
        public async Task<IActionResult> ListByCandidate([FromBody] DTPagingCandidateViewedJobParameters parameters)
        {
            try
            {
                long candidateId = this.GetLoggedInUserId();
                parameters.CandidateId = candidateId;
                var result = await service.PagingByCandidateId(parameters);
                return Ok(result);
            }
            catch (Exception e)
            {
                logger.LogError(e, "Failed to get list paging candidate viewed job by candidateId");
                return BadRequest();
            }
        }
        /// <summary>
        /// Author: TUNGTD
        /// Created: 13/09/2023
        /// Description: datatable paging candidate viewed job parameter
        /// </summary>
        /// <param name="parameters"></param>
        /// <returns></returns>
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        [HttpPost("dt-paging")]
        public async Task<IActionResult> DTPaging([FromBody]DTCandidateViewedJobParameters parameters)
        {
            try
            {
                var result = await service.DTPaging(parameters);
                return Ok(result);
            }
            catch (Exception e)
            {
                logger.LogError(e, "Failed to get list candidate viewed job by datatables paging");
                return BadRequest();
            }
        }
        #endregion
    }
}
