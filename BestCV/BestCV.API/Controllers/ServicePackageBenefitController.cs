using BestCV.Application.Models.ServicePackageBenefit;
using BestCV.Application.Models.ServicePackageGroup;
using BestCV.Application.Services.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace BestCV.API.Controllers
{
    [Route("api/service-package-benefit")]
    [ApiController]
    public class ServicePackageBenefitController : BaseController
    {
        private readonly IServicePackageBenefitService service;
        private readonly ILogger<ServicePackageBenefitController> logger;
        public ServicePackageBenefitController(IServicePackageBenefitService _service, ILoggerFactory loggerFactory)
        {
            service = _service;
            logger = loggerFactory.CreateLogger<ServicePackageBenefitController>();
        }

        #region CRUD
  
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
                logger.LogError(ex, $"Failed to get service package benefit by id: {id}");
                return BadRequest();
            }
        }


        [HttpPost("add")]
        public async Task<IActionResult> Add([FromBody] InsertServicePackageBenefitDTO model)
        {
            try
            {
                var data = await service.CreateAsync(model);
                return Ok(data);
            }
            catch (Exception ex)
            {
                logger.LogError(ex, $"Failed to add service package benefit");
                return BadRequest();
            }
        }

 
        [HttpPut("update")]
        public async Task<IActionResult> Update([FromBody] UpdateServicePackageBenefitDTO model)
        {
            try
            {
                var data = await service.UpdateAsync(model);
                return Ok(data);
            }
            catch (Exception ex)
            {
                logger.LogError(ex, $"Failed to update service package benefit");
                return BadRequest();
            }
        }


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
                logger.LogError(ex, $"Failed to soft delete service package benefit");
                return BadRequest();
            }
        }


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
                logger.LogError(ex, $"Failed to get list service package benefit");
                return BadRequest();
            }
        }
        #endregion

        #region Additional Resources

        [HttpGet("list-by-service-package-id/{id}")]
        public async Task<IActionResult> GetListByServicePackageId(int id)
        {
            try
            {
                var data = await service.GetListByEmployerServicePackage(id);
                return Ok(data);

            }
            catch (Exception ex)
            {
                logger.LogError(ex, $"Failed to get list service package benefit by service package id");
                return BadRequest();
            }
        }
        #endregion
    }
}
