using BestCV.API.Utilities;
using BestCV.Application.Models.AccountStatus;
using BestCV.Application.Models.CandidateCVs;
using BestCV.Application.Models.CandidateLevel;
using BestCV.Application.Services.Interfaces;
using BestCV.Domain.Entities;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace BestCV.API.Controllers
{
    [Route("api/candidate-cv")]
    [ApiController]
    public class CandidateCVController : BaseController
    {
        private readonly ICandidateCVService service;
        private readonly ILogger<CandidateCVController> logger;
        public CandidateCVController(ICandidateCVService _service, ILoggerFactory loggerFactory)
        {
            service = _service;
            logger = loggerFactory.CreateLogger<CandidateCVController>();
        }

        #region CRUD
        [HttpGet("detail/{id}")]
        public async Task<IActionResult> Detail([Required] long id)
        {
            try
            {
                var response = await service.GetByIdAsync(id);
                return Ok(response);
            }
            catch (Exception ex)
            {
                logger.LogError(ex, $"Failed to get CandidateCV by id: {id}");
                return BadRequest();
            }
        }

        [HttpPost("add")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public async Task<IActionResult> Add([FromBody] InsertOrUpdateCandidateCVDTO obj)
        {
            try
            {
                obj.CandidateId = this.GetLoggedInUserId();
                var response = await service.CreateAsync(obj);
                return Ok(response);
            }
            catch (Exception e)
            {
                logger.LogError(e, "Failed when insert new CandidateCV");
                return BadRequest();
            }
        }


        [HttpPut("update")]
        public async Task<IActionResult> Update([FromBody] InsertOrUpdateCandidateCVDTO obj)
        {
            try
            {
                var response = await service.UpdateAsync(obj);
                return Ok(response);
            }
            catch (Exception e)
            {
                logger.LogError(e, $"Failed when update new CandidateCV with id: {obj.Id}");
                return BadRequest();
            }
        }


        [HttpDelete("delete/{id}")]
        public async Task<IActionResult> Delete([Required] long id)
        {
            try
            {
                var response = await service.SoftDeleteAsync(id);
                return Ok(response);
            }
            catch (Exception e)
            {
                logger.LogError(e, $"Failed to delete CandidateCV by id: {id}");
                return BadRequest();
            }
        }
        #endregion

        #region Additional Resources

        [HttpGet("my-list")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public async Task<IActionResult> GetListAsyncByCandidateId()
        {
            try
            {
                var candidateId = this.GetLoggedInUserId();
                var response = await service.GetListAggregateAsyncByCandidateId(candidateId);
                return Ok(response);
            }
            catch (Exception e)
            {
                logger.LogError(e, $"Failed when get list CandidateCVAggregate by CandidateId");
                return BadRequest();
            }
        }
        #endregion
    }
}
