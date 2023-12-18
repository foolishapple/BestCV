using BestCV.Application.Models.CandidateLevel;
using BestCV.Application.Models.CompanySize;
using BestCV.Application.Services.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace BestCV.API.Controllers
{
    [Route("api/company-size")]
    [ApiController]
    public class CompanySizeController : BaseController
    {
        private readonly ICompanySizeService service;
        private readonly ILogger<CompanySizeController> logger;
        public CompanySizeController(ICompanySizeService _service, ILoggerFactory loggerFactory)
        {
            service = _service;
            logger = loggerFactory.CreateLogger<CompanySizeController>();
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
                logger.LogError(ex, $"Failed to get company size by id: {id}");
                return BadRequest();
            }
        }


        [HttpPost("add")]
        public async Task<IActionResult> Add([FromBody] InsertCompanySizeDTO model)
        {
            try
            {
                var data = await service.CreateAsync(model); 
                return Ok(data);
            }
            catch (Exception ex)
            {
                logger.LogError(ex, $"Failed to add company size");
                return BadRequest();
            }
        }


        [HttpPut("update")]
        public async Task<IActionResult> Update([FromBody] UpdateCompanySizeDTO model)
        {
            try
            {
                var data = await service.UpdateAsync(model);
                return Ok(data);
            }
            catch (Exception ex)
            {
                logger.LogError(ex, $"Failed to update company size");
                return BadRequest();
            }
        }


        [HttpDelete("delete/{id}")]
        public async Task<IActionResult> Delete([Required] int id)
        {
            try
            {
                var data = await service.SoftDeleteAsync(id);
                return Ok(data);
            }
            catch (Exception ex)
            {
                logger.LogError(ex, $"Failed to soft delete company size");
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
                logger.LogError(ex, $"Failed to get list company size");
                return BadRequest();
            }
        }

        #endregion

        #region Additional Resources
        [HttpGet("filter-company-size")]
        public async Task<IActionResult> FilterCompanySize()
        {
            try
            {
                var data = await service.LoadDataFilterCompanySizeHomePageAsync();
                return Ok(data);

            }
            catch (Exception ex)
            {
                logger.LogError(ex, $"Failed to get list company size");
                return BadRequest();
            }
        }
        #endregion
    }
}
