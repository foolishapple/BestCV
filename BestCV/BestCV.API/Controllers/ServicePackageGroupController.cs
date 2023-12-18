using BestCV.Application.Models.EmployerBenefit;
using BestCV.Application.Models.ServicePackageGroup;
using BestCV.Application.Services.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace BestCV.API.Controllers
{
    [Route("api/service-package-group")]
    [ApiController]
    public class ServicePackageGroupController : BaseController
    {
        private readonly IServicePackageGroupService service;
        private readonly ILogger<ServicePackageGroupController> logger;
        public ServicePackageGroupController(IServicePackageGroupService _service, ILoggerFactory loggerFactory)
        {
            service = _service;
            logger = loggerFactory.CreateLogger<ServicePackageGroupController>();
        }

        #region CRUD
        /// <summary>
        /// Author : ThanhND
        /// CreatedTime : 09/08/2023
        /// </summary>
        /// <param name="id">Id</param>
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
                logger.LogError(ex, $"Failed to get service package group by id: {id}");
                return BadRequest();
            }
        }

        /// <summary>
        /// Author : ThanhND
        /// CreatedTime : 08/09/2023
        /// </summary>
        /// <param name="model">InsertServicePackageGroupDTO</param>
        /// <returns></returns>
        [HttpPost("add")]
        public async Task<IActionResult> Add([FromBody] InsertServicePackageGroupDTO model)
        {
            try
            {
                var data = await service.CreateAsync(model);
                return Ok(data);
            }
            catch (Exception ex)
            {
                logger.LogError(ex, $"Failed to add service package group");
                return BadRequest();
            }
        }

        /// <summary>
        /// Author : ThanhND
        /// CreatedTime : 09/08/2023
        /// </summary>
        /// <param name="model">UpdateServicePackageGroupDTO</param>
        /// <returns></returns>
        [HttpPut("update")]
        public async Task<IActionResult> Update([FromBody] UpdateServicePackageGroupDTO model)
        {
            try
            {
                var data = await service.UpdateAsync(model);
                return Ok(data);
            }
            catch (Exception ex)
            {
                logger.LogError(ex, $"Failed to update service package group");
                return BadRequest();
            }
        }

        /// <summary>
        /// Author : ThanhND
        /// CreatedTime : 09/08/2023
        /// </summary>
        /// <param name="id">id</param>
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
                logger.LogError(ex, $"Failed to soft delete service package group");
                return BadRequest();
            }
        }

        /// <summary>
        /// Author : ThanhND
        /// CreatedTime : 09/08/2023
        /// </summary>
        /// <returns>List</returns>
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
                logger.LogError(ex, $"Failed to get list service package group");
                return BadRequest();
            }
        }
        #endregion

        #region Additional Resources

        #endregion
    }
}
