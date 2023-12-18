using BestCV.Application.Models.AccountStatus;
using BestCV.Application.Models.CandidateLevel;
using BestCV.Application.Services.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace BestCV.API.Controllers
{
    [Route("api/cv-template")]
    [ApiController]
    public class CVTemplateController : BaseController
    {
        private readonly ICVTemplateService service;
        private readonly ILogger<CVTemplateController> logger;
        public CVTemplateController(ICVTemplateService _service, ILoggerFactory loggerFactory)
        {
            service = _service;
            logger = loggerFactory.CreateLogger<CVTemplateController>();
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
                logger.LogError(ex, $"Failed to get cv template by id: {id}");
                return BadRequest();
            }
        }
        #endregion

        #region Additional Resources
 
        [HttpGet("get-all-publish")]
        public async Task<IActionResult> GetAllPublish()
        {
            try
            {
                var data = await service.GetAllPublishAsync();
                return Ok(data);
            }
            catch (Exception ex)
            {
                logger.LogError(ex, $"Failed to get all publish CVTemplate");
                return BadRequest();
            }
        }
        #endregion
    }
}
