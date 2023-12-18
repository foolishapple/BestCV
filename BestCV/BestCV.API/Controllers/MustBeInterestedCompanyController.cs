using BestCV.Application.Models.MustBeInterestedCompany;
using BestCV.Application.Services.Implement;
using BestCV.Application.Services.Interfaces;
using BestCV.Core.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace BestCV.API.Controllers
{
    [Route("api/must-interesterd-company")]
    [ApiController]
    public class MustBeInterestedCompanyController : BaseController
    {
        private readonly IMustBeInterestedCompanyService service;
        private readonly ILogger<MustBeInterestedCompanyService> logger;
        public MustBeInterestedCompanyController(IMustBeInterestedCompanyService _service, ILoggerFactory loggerFactory)
        {
            service = _service;
            logger = loggerFactory.CreateLogger<MustBeInterestedCompanyService>();
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
                logger.LogError(ex, $"Failed to get must be interesterd company by id: {id}");
                return BadRequest();
            }
        }

        /// <summary>
        /// Author : HoanNK
        /// CreatedTime : 08/09/2023
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost("add")]
        public async Task<IActionResult> Add([FromBody] InsertMustBeInterestedCompanyDTO model)
        {
            try
            {
                var data = await service.CreateAsync(model);
                return Ok(data);
            }
            catch (Exception ex)
            {
                logger.LogError(ex, $"Failed to add must be interesterd company");
                return BadRequest();
            }
        }

        /// <summary>
        /// Author : HoanNK
        /// CreatedTime : 08/09/2023
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPut("update")]
        public async Task<IActionResult> Update([FromBody] UpdateMustBeInterestedCompanyDTO model)
        {
            try
            {
                var data = await service.UpdateAsync(model);
                return Ok(data);
            }
            catch (Exception ex)
            {
                logger.LogError(ex, $"Failed to update must be interesterd company");
                return BadRequest();
            }
        }

        /// <summary>
        /// Author : HoanNK
        /// CreatedTime : 08/09/2023
        /// </summary>
        /// <param name="id"></param>
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
                logger.LogError(ex, $"Failed to soft delete must be interesterd company");
                return BadRequest();
            }
        }

        /// <summary>
        /// Author : HoanNK
        /// CreatedTime : 08/09/2023
        /// </summary>
        /// <returns></returns>
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
                logger.LogError(ex, $"Failed to get list must be interesterd company");
                return BadRequest();
            }
        }
        #endregion


        [HttpGet("list-aggregates")]
        public async Task<IActionResult> GetListAggregates()
        {
            try
            {
                var data = await service.ListAggregatesAsync();
                return Ok(data);
            }
            catch (Exception ex)
            {
                logger.LogError(ex, $"Failed to get list must be interesterd company aggregates");
                return BadRequest();
            }
        }


        [HttpGet("list-company-selected")]
        public async Task<IActionResult> ListJobSelected()
        {
            try
            {
                var data = await service.ListCompanySelected();
                return Ok(BestCVResponse.Success(data));
            }
            catch (Exception e)
            {
                logger.LogError(e, "Failed to get list selected job");
                return BadRequest();
            }
        }


        [HttpGet("list-interested-company-job-detail")]
        public async Task<IActionResult> ListOnJobDetail()
        {
            try
            {
                var data = await service.ListCompanyInterestedOnDetailJob();
                return Ok(data);
            }
            catch (Exception e)
            {
                logger.LogError(e, "Failed to get list interested company on job detail");
                return BadRequest();
            }
        }
    }
}
