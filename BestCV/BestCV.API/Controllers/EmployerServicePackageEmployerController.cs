using BestCV.API.Utilities;
using BestCV.Application.Services.Interfaces;
using BestCV.Domain.Aggregates.EmployerServicePackageEmployers;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BestCV.API.Controllers
{
    [Route("api/employer-service-package-employer")]
    [ApiController]
    public class EmployerServicePackageEmployerController : BaseController
    {
        private readonly IEmployerServicePackageEmployerService _service;
        private readonly ILogger _logger;
        public EmployerServicePackageEmployerController(IEmployerServicePackageEmployerService service, ILoggerFactory loggerFactory)
        {
            _service = service;
            _logger = loggerFactory.CreateLogger<EmployerServicePackageEmployerController>();
        }
        /// <summary>
        /// Author: TUNGTD
        /// Created: 17/09/2023
        /// Description: API Post get list service package group of employer
        /// </summary>
        /// <param name="parameters"></param>
        /// <returns></returns>
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        [HttpGet("list-service-of-employer")]
        public async Task<IActionResult> ListServiceOfEmployer()
        {
            try
            {
                var employerId = this.GetLoggedInUserId();
                var result = await _service.GroupEmployerService(new()
                {
                    EmployerId = employerId
                });
                return Ok(result);
            }
            catch (Exception e)
            {
                _logger.LogError(e, "Failed to get list service package of employer");
                return BadRequest();
            }
        }
        /// <summary>
        /// Author: TUNGTD
        /// Created: 17/09/2023
        /// Description: API Post get list service package group add on of employer
        /// </summary>
        /// <param name="parameters"></param>
        /// <returns></returns>
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        [HttpGet("list-service-of-employer-add-on")]
        public async Task<IActionResult> ListServiceOfEmployerAddOn()
        {
            try
            {
                var employerId = this.GetLoggedInUserId();
                var result = await _service.GroupEmployerServiceAddOn(new()
                {
                    EmployerId = employerId
                });
                return Ok(result);
            }
            catch (Exception e)
            {
                _logger.LogError(e, "Failed to get list service package of employer");
                return BadRequest();
            }
        }
    }
}
