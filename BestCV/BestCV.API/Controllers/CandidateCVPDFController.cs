using BestCV.API.Utilities;
using BestCV.Application.Models.CandidateCVPDF;
using BestCV.Application.Models.CandidateCVPDFTypes;
using BestCV.Application.Services.Interfaces;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace BestCV.API.Controllers
{
    [Route("api/candidate-cv-pdf")]
    [ApiController]
    public class CandidateCVPDFController : BaseController
    {
        private readonly ICandidateCVPDFService service;
        private readonly ILogger<CandidateCVPDFController> logger;
        public CandidateCVPDFController(ICandidateCVPDFService _service, ILoggerFactory loggerFactory)
        {
            service = _service;
            logger = loggerFactory.CreateLogger<CandidateCVPDFController>();
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
        public async Task<IActionResult> Add([FromBody] InsertCandidateCVPDFDTO model)
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
        public async Task<IActionResult> Update([FromBody] UpdateCandidateCVPDFDTO model)
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

        [HttpDelete("delete")]
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
        [HttpGet("list-by-candidate-id")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]

        public async Task<IActionResult> ListByCandidateId()
        {
            try
            {
                var candidateId = this.GetLoggedInUserId();
                var data = await service.GetByCandidateId(candidateId);
                return Ok(data);

            }
            catch (Exception ex)
            {
                logger.LogError(ex, $"Failed to get list candidate CV PDF by candidateId");
                return BadRequest();
            }
        }


        [HttpPost("add-cv")]
        public async Task<IActionResult> AddCV([FromBody] UploadCandidateCVPDFDTO model)
        {
            try
            {
                model.CandidateId = this.GetLoggedInUserId();
                var data = await service.UploadCV(model);
                return Ok(data);
            }
            catch (Exception ex)
            {
                logger.LogError(ex, $"Failed to add candidate cv pdf");
                return BadRequest();
            }
        }


        [HttpGet("list-by-candidate-id/{candidateId}")]

        public async Task<IActionResult> ListCVPDFByCandidateId(long candidateId)
        {
            try
            {
                var data = await service.GetByCandidateId(candidateId);
                return Ok(data);

            }
            catch (Exception ex)
            {
                logger.LogError(ex, $"Failed to get list candidate CV PDF by candidateId");
                return BadRequest();
            }
        }
        #endregion
    }
}
