using BestCV.API.Utilities;
using BestCV.Application.Models.JobServicePackages;
using BestCV.Application.Services.Interfaces;
using BestCV.Core.Entities;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace BestCV.API.Controllers
{
    [Route("api/job-service-package")]
    [ApiController]
    public class JobServicePackageController : BaseController
    {
        private readonly IJobServicePackageService _service;
        private readonly ILogger _logger;
        public JobServicePackageController(IJobServicePackageService service, ILoggerFactory loggerFactory)
        {
            _service = service;
            _logger = loggerFactory.CreateLogger<JobServicePackageController>();
        }

        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        [HttpGet("list-all-add-on-of-job/{BestCVd}")]
        public async Task<IActionResult> ListAllAddOnOfJob(long BestCVd)
        {
            try
            {
                var result = await _service.ListAggregate(BestCVd);
                return Ok(result);
            }
            catch (Exception e)
            {
                _logger.LogError(e, $"Failed to get list all service add on job {BestCVd}");
                return BadRequest();
            }
        }

        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        [HttpPost("add-service-pack-to-job")]
        public async Task<IActionResult> AddServicePackToJob([FromBody] InsertJobServicePackageDTO obj)
        {
            try
            {
                var userId = this.GetLoggedInUserId();
                obj.EmployerId = userId;
                var result = await _service.AddServiceToJob(obj);
                return Ok(result);
            }
            catch (Exception e)
            {
                _logger.LogError(e, "Failed to add service package to job");
                return BadRequest();
            }
        }
    }
}
