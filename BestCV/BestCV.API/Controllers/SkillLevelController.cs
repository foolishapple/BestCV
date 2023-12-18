using BestCV.Application.Models.SkillLevel;
using BestCV.Application.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace BestCV.API.Controllers
{
    [ApiController]
    [Route("api/skill-level")]
    public class SkillLevelController : BaseController
    {
        private readonly ISkillLevelService service;
        private readonly ILogger<SkillLevelController> logger;

        public SkillLevelController(ISkillLevelService _service, ILoggerFactory loggerFactory)
        {
            logger = loggerFactory.CreateLogger<SkillLevelController>();
            service = _service;
        }

        #region CRUD
 
        [HttpGet("list")]
        public async Task<IActionResult> GetAll()
        {
            try
            {
                var res = await service.GetAllAsync();
                if (res != null)
                {
                    return Ok(res);
                }
                return BadRequest();
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Failed to get all skill-level");
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
                logger.LogError(ex, "Failed to detail skill-level ");
                return BadRequest(ex);
            }
        }

        [HttpPost("add")]
        public async Task<IActionResult> Add([FromBody] InsertSkillLevelDTO obj)
        {
            try
            {
                var res = await service.CreateAsync(obj);
                return Ok(res);
            }
            catch (Exception ex) 
            {
                logger.LogError(ex, "Failed to add skill-level");
                return BadRequest();
            }
        }

        [HttpDelete("delete/{id}")]
        public async Task<IActionResult> Delete([Required] int id)
        {
            try
            {
                var data = await service.SoftDeleteAsync(id);
                return Ok(data);
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Failed to delete skill-level");
                return BadRequest();
            }
        }


        [HttpPut("update")]
        public async Task<IActionResult> Update([FromBody] UpdateSkillLevelDTO obj)
        {
            try
            {
                var data = await service.UpdateAsync(obj);
                return Ok(data);
            }
            catch(Exception ex) 
            {
                logger.LogError(ex, "Failed to update skill-level");
                return BadRequest();
            }
        }
        #endregion
        #region ADDITIONAL_RESOURCES
        #endregion
    }
}
