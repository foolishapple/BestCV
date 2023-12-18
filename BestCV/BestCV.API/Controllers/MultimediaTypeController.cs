using BestCV.Application.Models.MultimediaType;
using BestCV.Application.Models.RecruitmentStatus;
using BestCV.Application.Services.Implement;
using BestCV.Application.Services.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace BestCV.API.Controllers
{
    [Route("api/multimedia-type")]
    [ApiController]
    public class MultimediaTypeController : BaseController
    {
        private readonly IMultimediaTypeService multimediaTypeService;
        private readonly ILogger<MultimediaTypeController> logger;
        public MultimediaTypeController(IMultimediaTypeService multimediaTypeService, ILoggerFactory loggerFactory)
        {
            this.multimediaTypeService = multimediaTypeService;
            this.logger = loggerFactory.CreateLogger<MultimediaTypeController>();
        }
        #region CRUD

        [HttpGet("list")]
        public async Task<IActionResult> GetList()
        {
            try
            {
                var response = await multimediaTypeService.GetAllAsync();
                return Ok(response);
            }
            catch (Exception e)
            {
                logger.LogError(e, $"Failed to get multimedia type");
                return BadRequest();
            }
        }


        [HttpGet("detail/{id}")]
        public async Task<IActionResult> Detail([Required] int id)
        {
            try
            {
                var response = await multimediaTypeService.GetByIdAsync(id);
                return Ok(response);
            }
            catch (Exception e)
            {

                logger.LogError(e, $"Failed to get multimedia type by id: {id}");
                return BadRequest();
            }
        }


        [HttpPost("add")]
        public async Task<IActionResult> Add([FromBody] InsertMultimediaTypeDTO obj)
        {
            try
            {
                var response = await multimediaTypeService.CreateAsync(obj);
                return Ok(response);
            }
            catch (Exception e)
            {

                logger.LogError(e, $"Failed to insert multimedia type: {obj}");
                return BadRequest();
            }
        }


        [HttpPut("update")]
        public async Task<IActionResult> Update([FromBody] UpdateMultimediaTypeDTO obj)
        {
            try
            {

                var response = await multimediaTypeService.UpdateAsync(obj);
                return Ok(response);

            }
            catch (Exception e)
            {

                logger.LogError(e, $"Failed to update multimedia type: {obj}");
                return BadRequest();
            }
        }


        [HttpDelete("delete")]
        public async Task<IActionResult> Delete([Required] int id)
        {
            try
            {
                var response = await multimediaTypeService.SoftDeleteAsync(id);
                return Ok(response);

            }
            catch (Exception e)
            {

                logger.LogError(e, $"Failed to delete multimedia type by id: {id}");
                return BadRequest();
            }
        }


        #endregion
    }
}
