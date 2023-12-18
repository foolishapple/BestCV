using BestCV.Application.Models.CandidateLevel;
using BestCV.Application.Models.EmployerServicePackage;
using BestCV.Application.Services.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace BestCV.API.Controllers
{
    [Route("api/employer-service-package")]
    [ApiController]
    public class EmployerServicePackageController : BaseController
    {
        private readonly IEmployerServicePackageService service;
        private readonly ILogger<EmployerServicePackageController> logger;
        public EmployerServicePackageController(IEmployerServicePackageService _service, ILoggerFactory loggerFactory)
        {
            service = _service;
            logger = loggerFactory.CreateLogger<EmployerServicePackageController>();
        }

        #region CRUD
        /// <summary>
        /// Author : ThanhND
        /// CreatedTime : 03/08/2023
        /// </summary>
        /// <param name="id">employerServicePackageId</param>
        /// <returns></returns>
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
                logger.LogError(ex, $"Failed to get employer Service PackageId by id: {id}");
                return BadRequest();
            }
        }

        /// <summary>
        /// Author : ThanhND
        /// CreatedTime : 03/08/2023
        /// </summary>
        /// <param name="model">InsertEmployerServicePackageDTO</param>
        /// <returns></returns>
        [HttpPost("add")]
        public async Task<IActionResult> Add([FromBody] InsertEmployerServicePackageDTO model)
        {
            try
            {
                var data = await service.CreateAsync(model);
                return Ok(data);
            }
            catch (Exception ex)
            {
                logger.LogError(ex, $"Failed to add Employer Service Package");
                return BadRequest();
            }
        }

        /// <summary>
        /// Author : ThanhND
        /// CreatedTime : 03/08/2023
        /// </summary>
        /// <param name="model">UpdateEmployerServicePackageDTO</param>
        /// <returns></returns>
        [HttpPut("update")]
        public async Task<IActionResult> Update([FromBody] UpdateEmployerServicePackageDTO model)
        {
            try
            {
                var data = await service.UpdateAsync(model);
                return Ok(data);
            }
            catch (Exception ex)
            {
                logger.LogError(ex, $"Failed to update Employer Service Package");
                return BadRequest();
            }
        }

        /// <summary>
        /// Author : ThanhND
        /// CreatedTime : 03/08/2023
        /// </summary>
        /// <param name="id">employerServicePackageId</param>
        /// <returns></returns>
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
                logger.LogError(ex, $"Failed to soft delete employer Service Package");
                return BadRequest();
            }
        }

        /// <summary>
        /// Author : ThanhND
        /// CreatedTime : 03/08/2023
        /// </summary>
        /// <returns>List Employer Service Package</returns>
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
                logger.LogError(ex, $"Failed to get list employer service package");
                return BadRequest();
            }
        }
        #endregion

        #region Additional Resources
        [HttpGet("list-aggregate")]
        public async Task<IActionResult> GetListAggregate()
        {
            try
            {
                var data = await service.GetListAggregate();
                return Ok(data);

            }
            catch (Exception ex)
            {
                logger.LogError(ex, $"Failed to get list employer service package");
                return BadRequest();
            }
        }
        #endregion
    }
}
