using BestCV.Application.Models.JobType;
using BestCV.Application.Models.Position;
using BestCV.Application.Services.Implement;
using BestCV.Application.Services.Interfaces;
using BestCV.Infrastructure.Repositories.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using System.Security.Principal;

namespace BestCV.API.Controllers
{
    [Route("api/position")]
    [ApiController]
    public class PositionController : BaseController
    {
        private readonly IPositionService service;
        private readonly ILogger<PositionController> logger;
        private readonly IConfiguration configuration;

        public PositionController(IPositionService _service, ILoggerFactory loggerFactory, IConfiguration _configuration)
        {
            service = _service;
            logger = loggerFactory.CreateLogger<PositionController>();
            configuration = _configuration;
        }

        #region CRUD

        [HttpGet("list")]
        public async Task<IActionResult> GetAll()
        {
            try
            {
                var data = await service.GetAllAsync();
                return Ok(data);
            }
            catch (Exception ex)
            {
                logger.LogError(ex, $"Failed to get all data of position");
                return BadRequest();
            }
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
                logger.LogError(ex, $"Failed to get detail position by id: {id}");
                return BadRequest();
            }
        }

        [HttpPost("add")]
        public async Task<IActionResult> Add([FromBody] InsertPositionDTO obj)
        {
            try
            {
                var res = await service.CreateAsync(obj);
                return Ok(res);
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Failed to add Position");
                return BadRequest();
            }
        }
        [HttpPut("update")]
        public async Task<IActionResult> Update([FromBody] UpdatePositionDTO obj)
        {
            try
            {
                var res = await service.UpdateAsync(obj);
                return Ok(res);
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Failed to update Position");
                return BadRequest();
            }
        } 
            [HttpDelete("delete/{id}")]
            public async Task<IActionResult> Delete([Required] int id)
            {
                try
                {
                    var res = await service.SoftDeleteAsync(id);
                    return Ok(res);
                }
                catch (Exception ex)
                {
                    logger.LogError(ex, "Failed to delete position");
                    return BadRequest();
                }
            }
            #endregion


            #region Additional Resources

            #endregion
        }
    }
