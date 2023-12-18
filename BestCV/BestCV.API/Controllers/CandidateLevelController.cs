using BestCV.Application.Models.CandidateLevel;
using BestCV.Application.Models.CouponType;
using BestCV.Application.Services.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace BestCV.API.Controllers
{
    [Route("api/candidate-level")]
    [ApiController]
    public class CandidateLevelController : BaseController
    {
        private readonly ICandidateLevelService service;
        private readonly ILogger<CandidateLevelController> logger;
        public CandidateLevelController(ICandidateLevelService _service, ILoggerFactory loggerFactory)
        {
            service = _service;
            logger = loggerFactory.CreateLogger<CandidateLevelController>();
        }


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
                logger.LogError(ex, $"Failed to get candidate level by id: {id}");
                return BadRequest();
            }
        }


        [HttpPost("add")]
        public async Task<IActionResult> Add([FromBody] InsertCandidateLevelDTO model)
        {
            try
            {
                var data = await service.CreateAsync(model);
                return Ok(data);
            }
            catch (Exception ex)
            {
                logger.LogError(ex, $"Failed to add candidate level");
                return BadRequest();
            }
        }


        [HttpPut("update")]
        public async Task<IActionResult> Update([FromBody] UpdateCandidateLevelDTO model)
        {
            try
            {
                var data = await service.UpdateAsync(model);
                return Ok(data);
            }
            catch (Exception ex)
            {
                logger.LogError(ex, $"Failed to update candidate level");
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
                logger.LogError(ex, $"Failed to soft delete candidate level");
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
                logger.LogError(ex, $"Failed to get list candidate level");
                return BadRequest();
            }
        }


        #region Additional Resources

        #endregion
    }
}
