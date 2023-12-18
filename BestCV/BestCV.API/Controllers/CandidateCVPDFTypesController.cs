using BestCV.Application.Models.AccountStatus;
using BestCV.Application.Models.CandidateCVPDFTypes;
using BestCV.Application.Services.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace BestCV.API.Controllers
{
    [Route("api/candidate-cv-pdf-type")]
    [ApiController]
    public class CandidateCVPDFTypesController : BaseController
    {
        private readonly ICandidateCVPDFTypesService service;
        private readonly ILogger<CandidateCVPDFTypesController> logger;
        public CandidateCVPDFTypesController(ICandidateCVPDFTypesService _service, ILoggerFactory loggerFactory)
        {
            service = _service;
            logger = loggerFactory.CreateLogger<CandidateCVPDFTypesController>();
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
                logger.LogError(ex, $"Failed to get candidate cv pdf by id: {id}");
                return BadRequest();
            }
        }


        [HttpPost("add")]
        public async Task<IActionResult> Add([FromBody] InsertCandidateCVPDFTypesDTO model)
        {
            try
            {
                var data = await service.CreateAsync(model);
                return Ok(data);
            }
            catch (Exception ex)
            {
                logger.LogError(ex, $"Failed to add candidate cv pdf");
                return BadRequest();
            }
        }

        [HttpPut("update")]
        public async Task<IActionResult> Update([FromBody] UpdateCandidateCVPDFTypesDTO model)
        {
            try
            {
                var data = await service.UpdateAsync(model);
                return Ok(data);
            }
            catch (Exception ex)
            {
                logger.LogError(ex, $"Failed to update candidate cv pdf");
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
                logger.LogError(ex, $"Failed to soft delete candidate cv pdf");
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
                logger.LogError(ex, $"Failed to get list candidate CV PDF");
                return BadRequest();
            }
        }
        #endregion

        #region Additional Resources

        #endregion
    }
}
