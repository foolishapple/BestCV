using BestCV.Application.Models.CompanyFieldOfActivities;
using BestCV.Application.Services.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace BestCV.API.Controllers
{
	[Route("api/company-field-of-activity")]
	[ApiController]
	public class CompanyFieldOfActivityController : ControllerBase
	{
        private readonly ICompanyFieldOfActivityService service;
        private readonly ILogger<CompanyFieldOfActivityController> logger;
        public CompanyFieldOfActivityController(ICompanyFieldOfActivityService _service, ILoggerFactory loggerFactory)
        {
            service = _service;
            logger = loggerFactory.CreateLogger<CompanyFieldOfActivityController>();
        }

        #region CRUD
  
        [HttpGet("list-by-companyId/{id}")]
        public async Task<IActionResult> GetListByCompanyId(int id)
        {
            try
            {
                var data = await service.GetFieldActivityByCompanyId(id);
                return Ok(data);

            }
            catch (Exception ex)
            {
                logger.LogError(ex, $"Failed to get list company size");
                return BadRequest();
            }
        }


        [HttpPost("add")]
        public async Task<IActionResult> Add([FromBody] IEnumerable<InsertCompanyFieldOfActivityDTO> objs)
        {
            try
            {
                var res = await service.CreateListAsync(objs);
                return Ok(res);
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Failed to add Company Field Of Activity");
                return BadRequest();
            }
        }

 

        [HttpDelete("deleteByCompanyId/{companyId}")]
        public async Task<IActionResult> DeleteByCompanyId([Required] int companyId)
        {
            try
            {
                var res = await service.DeleteFieldActivityByCompanyId(companyId);
                return Ok(res);
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Failed to delete Company Field Of Activity");
                return BadRequest();
            }
        }

        #endregion
    }
}
