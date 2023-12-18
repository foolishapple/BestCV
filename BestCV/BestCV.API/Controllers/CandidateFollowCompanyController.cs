using BestCV.API.Utilities;
using BestCV.Application.Models.CandidateFollowCompany;
using BestCV.Application.Services.Interfaces;
using BestCV.Core.Entities;
using BestCV.Domain.Aggregates.CandidateFollowCompany;
using BestCV.Domain.Aggregates.CandidateSaveJob;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BestCV.API.Controllers
{
    [Route("api/candidate-follow-company")]
    [ApiController]
    public class CandidateFollowCompanyController : BaseController
    {
        private readonly ICandidateFollowCompanyService service;
        private readonly ILogger logger;

        public CandidateFollowCompanyController(ILoggerFactory loggerFactory, ICandidateFollowCompanyService _service)
        {
            service = _service;
            logger = loggerFactory.CreateLogger<CandidateFollowCompanyController>();
        }

        [HttpPost("get-list-candidate-follow-company")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public async Task<IActionResult> GetListCompanyByCandidateId()
        {
            try
            {
                var candidateId = this.GetLoggedInUserId();
                var data = await service.GetListCompanyByCandidateId(candidateId);
                
                return Ok(data);
            }
            catch (Exception ex)
            {
                logger.LogError(ex, ex.Message);
                return BadRequest(ex.Message + " - " + ex.InnerException);
            }
        }

        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        [HttpPost("add-candidate-follow-company")]
        public async Task<IActionResult> AddCandidateForCompany([FromBody] InsertCandidateFollowCompanyDTO obj)
        {
            try
            {
                var candidateId = this.GetLoggedInUserId();

                var data = await service.InsertCandidateWithViewModel(obj, candidateId);
                

                return Ok(data);
            }catch(Exception ex)
            {
                logger.LogError(ex, "Failed to add description to candidate apply job");
                return BadRequest();
            }
        }

        [HttpDelete("delete/{id}")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public async Task<IActionResult> DeleteFollowCompany(long id)
        {
            try
            {
                var result = await service.HardDeleteAsync(id);
                
                return Ok(result);
                
            }
            catch (Exception e)
            {
                logger.LogError(e, $"Failed to delete candidate save job id: {id}");
                return BadRequest(e);
            }
        }

        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        [HttpPost("list-candidate-follow-company")]
        public async Task<IActionResult> ListByCandidate([FromBody] DTPagingCandidateFollowCompanyParameters parameters)
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
                logger.LogError(e, "Failed to get list paging candidate follow company by candidateId");
                return BadRequest();
            }
        }

        [HttpGet("detail/{id}")]
        public async Task<IActionResult> Detail(long id)
        {
            try
            {
                var response = await service.GetByIdAsync(id);
                return Ok(response);
            }
            catch (Exception e)
            {
                logger.LogError(e, "Failed to get candidate notification detail");
                return BadRequest();
            }
        }
    }
}
