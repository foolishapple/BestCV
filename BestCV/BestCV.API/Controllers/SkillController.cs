using BestCV.Application.Models.Skill;
using BestCV.Application.Models.SkillLevel;
using BestCV.Application.Services.Interfaces;
using BestCV.Domain.Aggregates.TopFeatureJob;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace BestCV.API.Controllers
{
    [ApiController]
    [Route("api/skill")]
    public class SkillController : BaseController
    {
        private readonly ISkillService service;
        private readonly ILogger<SkillController> logger;

        public SkillController(ISkillService _service, ILoggerFactory loggerFactory)
        {
            logger = loggerFactory.CreateLogger<SkillController>();
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
                logger.LogError(ex, "Failed to get all skill");
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
                logger.LogError(ex, "Failed to detail level ");
                return BadRequest(ex);
            }
        }

        [HttpPost("add")]
        public async Task<IActionResult> Add([FromBody] InsertSkillDTO obj)
        {
            try
            {
                var res = await service.CreateAsync(obj);
                return Ok(res);
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Failed to add skill");
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
                logger.LogError(ex, "Failed to delete skill");
                return BadRequest();
            }
        }

        [HttpPut("update")]
        public async Task<IActionResult> Update([FromBody] UpdateSkillDTO obj)
        {
            try
            {
                var data = await service.UpdateAsync(obj);
                return Ok(data);
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Failed to update skill");
                return BadRequest();
            }
        }


        [HttpPost]
        [Route("search-skills")]
        public async Task<IActionResult> Saerch([FromBody] Select2Aggregates select2Aggregates)
        {
            try
            {
                var data = await service.searchSkills(select2Aggregates);
                return Ok(data);
            }catch(Exception ex)
            {
                logger.LogError(ex, "Failed to get list for jobs");
                return BadRequest(ex.Message);
            }
        }
        #endregion
    }
}
