using BestCV.Application.Models.AccountStatus;
using BestCV.Application.Models.EmployerServicePackageEmployerBenefit;
using BestCV.Application.Services.Implement;
using BestCV.Application.Services.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace BestCV.API.Controllers
{
    [Route("api/employer-service-package-employer-benefit")]
    [ApiController]
    public class EmployerServicePackageEmployerBenefitController : BaseController
    {
        private readonly IEmployerServicePackageEmployerBenefitService service;
        private readonly ILogger<EmployerServicePackageEmployerBenefitController> logger;
        public EmployerServicePackageEmployerBenefitController(IEmployerServicePackageEmployerBenefitService _service, ILoggerFactory loggerFactory)
        {
            service = _service;
            logger = loggerFactory.CreateLogger<EmployerServicePackageEmployerBenefitController>();
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
                logger.LogError(ex, $"Failed to get employer service package employer benefit  by id: {id}");
                return BadRequest();
            }
        }

        [HttpPost("add")]
        public async Task<IActionResult> Add([FromBody] InsertEmployerServicePackageEmployerBenefitDTO model)
        {
            try
            {
                var data = await service.CreateAsync(model);
                return Ok(data);
            }
            catch (Exception ex)
            {
                logger.LogError(ex, $"Failed to add employer service package employer benefit ");
                return BadRequest();
            }
        }


        [HttpPut("update")]
        public async Task<IActionResult> Update([FromBody] UpdateEmployerServicePackageEmployerBenefitDTO model)
        {
            try
            {
                var data = await service.UpdateAsync(model);
                return Ok(data);
            }
            catch (Exception ex)
            {
                logger.LogError(ex, $"Failed to update employer service package employer benefit ");
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
                logger.LogError(ex, $"Failed to soft delete employer service package employer benefit  id");
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
                logger.LogError(ex, $"Failed to get list employer service package employer benefit ");
                return BadRequest();
            }
        }
        #endregion

        #region Additional Resources
        [HttpGet("list-by-employer-service-package-id/{id}")]
        public async Task<IActionResult> GetListByEmployerServicePackageId(int id)
        {
            try
            {
                var data = await service.GetByEmployerServicePackageIdAsync(id);
                return Ok(data);

            }
            catch (Exception ex)
            {
                logger.LogError(ex, $"Failed to get list employer service package employer benefit by  employerservicepackageid");
                return BadRequest();
            }
        }

        [HttpPut("update-has-benefit/{id}")]
        public async Task<IActionResult> UpdateHasBenefit(int id)
        {
            try
            {
                var data = await service.UpdateHasBenefitAsync(id);
                return Ok(data);
            }
            catch (Exception ex)
            {
                logger.LogError(ex, $"Failed to update has benefit employer service package employer benefit");
                return BadRequest();
            }
        }
        #endregion
    }
}
