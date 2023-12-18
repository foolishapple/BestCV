using BestCV.API.Utilities;
using BestCV.Application.Models.AccountStatus;
using BestCV.Application.Models.CandidateSaveJob;
using BestCV.Application.Services.Interfaces;
using BestCV.Core.Entities;
using BestCV.Domain.Aggregates.CandidateApplyJobs;
using BestCV.Domain.Aggregates.CandidateSaveJob;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace BestCV.API.Controllers
{
    [Route("api/candidate-save-job")]
    [ApiController]
    public class CandidateSaveJobController : BaseController
    {
        private readonly ICandidateSaveJobService service;
        private readonly ILogger<CandidateSaveJobController> logger;
        public CandidateSaveJobController(ICandidateSaveJobService _service, ILoggerFactory loggerFactory)
        {
            service = _service;
            logger = loggerFactory.CreateLogger<CandidateSaveJobController>();
        }

        


        #region Additional Resources
        [HttpPost("save-job/{BestCVd}")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public async Task<IActionResult> AddBookMark(long BestCVd)
        {
            try
            {
                var accountId = this.GetLoggedInUserId();
                if(accountId == 0)
                {
                    return BadRequest();
                }
                var data = await service.QuickSaveJob(BestCVd, accountId);
                return Ok(data);


            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Failed to insert wishlist.");
                return BadRequest();
            }
        }

        [HttpPost("get-list-job")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public async Task<IActionResult> GetListJobByCandidateId()
        {
            try
            {
                var candidateId = this.GetLoggedInUserId();
                var data = await service.GetListJobByCandidateId(candidateId);
                
                return Ok(data);
            }
            catch (Exception ex)
            {
                logger.LogError(ex, ex.Message);
                return BadRequest();
            }
        }

        [HttpDelete("delete/{id}")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public async Task<IActionResult> DeleteCandidateSaveJob(long id)
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
        [HttpPost("manage-list-job-save")]
        public async Task<IActionResult> ListByCandidate([FromBody] DTPagingCandidateSaveJobParameters parameters)
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
                logger.LogError(e, "Failed to get list paging candidate save job by candidateId");
                return BadRequest();
            }
        }
        #endregion
    }
}
