using BestCV.Application.Models.CandidateLevel;
using BestCV.Application.Models.EmployerBenefit;
using BestCV.Application.Services.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace BestCV.API.Controllers
{
    [Route("api/employer-benefit")]
    [ApiController]
    public class EmployerBenefitController : BaseController
    {
        private readonly IEmployerBenefitService service;
        private readonly ILogger<EmployerBenefitController> logger;
        public EmployerBenefitController(IEmployerBenefitService _service, ILoggerFactory loggerFactory)
        {
            service = _service;
            logger = loggerFactory.CreateLogger<EmployerBenefitController>();
        }

        #region CRUD
        /// <summary>
        /// Author : ThanhND
        /// CreatedTime : 09/08/2023
        /// </summary>
        /// <param name="id">employerbenefitId</param>
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
                logger.LogError(ex, $"Failed to get employer benefit by id: {id}");
                return BadRequest();
            }
        }

        /// <summary>
        /// Author : ThanhND
        /// CreatedTime : 08/08/2023
        /// </summary>
        /// <param name="model">InsertEmployerBenefitDTO</param>
        /// <returns></returns>
        [HttpPost("add")]
        public async Task<IActionResult> Add([FromBody] InsertEmployerBenefitDTO model)
        {
            try
            {
                var data = await service.CreateAsync(model);
                return Ok(data);
            }
            catch (Exception ex)
            {
                logger.LogError(ex, $"Failed to add employer benefit");
                return BadRequest();
            }
        }

        /// <summary>
        /// Author : ThanhND
        /// CreatedTime : 09/08/2023
        /// </summary>
        /// <param name="model">UpdateEmployerBenefitDTO</param>
        /// <returns></returns>
        [HttpPut("update")]
        public async Task<IActionResult> Update([FromBody] UpdateEmployerBenefitDTO model)
        {
            try
            {
                var data = await service.UpdateAsync(model);
                return Ok(data);
            }
            catch (Exception ex)
            {
                logger.LogError(ex, $"Failed to update employer benefit");
                return BadRequest();
            }
        }

        /// <summary>
        /// Author : ThanhND
        /// CreatedTime : 09/08/2023
        /// </summary>
        /// <param name="id">employerbenefitid</param>
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
                logger.LogError(ex, $"Failed to soft delete employer benefit");
                return BadRequest();
            }
        }

        /// <summary>
        /// Author : ThanhND
        /// CreatedTime : 09/08/2023
        /// </summary>
        /// <returns>List employer benefit</returns>
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
                logger.LogError(ex, $"Failed to get list employer benefit");
                return BadRequest();
            }
        }
        #endregion

        #region Additional Resources
        [HttpPost("list-exept")]
        public async Task<IActionResult> GetListExept([FromBody] List<int> arrBenefitId)
        {
            try
            {
                var data = await service.GetListBenefitExept(arrBenefitId);
                return Ok(data);

            }
            catch (Exception ex)
            {
                logger.LogError(ex, $"Failed to get list employer benefit");
                return BadRequest();
            }
        }
        #endregion
    }
}
