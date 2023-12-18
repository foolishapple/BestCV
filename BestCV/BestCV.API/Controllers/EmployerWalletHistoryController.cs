using BestCV.API.Utilities;
using BestCV.Application.Models.EmployerWalletHistory;
using BestCV.Application.Models.ExperienceRange;
using BestCV.Application.Services.Interfaces;
using BestCV.Core.Utilities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace BestCV.API.Controllers
{
    [Route("api/employer-wallet-history")]
    [ApiController]
    public class EmployerWalletHistoryController : BaseController
    {
        private readonly IEmployerWalletHistoryService service;
        private readonly ILogger<EmployerWalletHistoryController> logger;
        public EmployerWalletHistoryController(IEmployerWalletHistoryService _service, ILoggerFactory loggerFactory)
        {
            service = _service;
            logger = loggerFactory.CreateLogger<EmployerWalletHistoryController>();
        }


        #region CRUD
        /// <summary>
        /// Author : ThanhND
        /// CreatedTime : 02/10/2023
        /// </summary>
        /// <param name="id">id</param>
        /// <returns></returns>
        [HttpGet("detail/{id}")]
        public async Task<IActionResult> Detail([Required] long id)
        {
            try
            {
                var response = await service.GetByIdAsync(id);
                return Ok(response);
            }
            catch (Exception ex)
            {
                logger.LogError(ex, $"Failed to get employer wallet history by id: {id}");
                return BadRequest();
            }
        }

        /// <summary>
        /// Author : ThanhND
        /// CreatedTime :  02/10/2023
        /// </summary>
        /// <param name="model">InsertEmployerWalletHistoryDTO</param>
        /// <returns></returns>
        [HttpPost("add")]
        public async Task<IActionResult> Add([FromBody] InsertEmployerWalletHistoryDTO model)
        {
            try
            {
                var data = await service.CreateAsync(model);
                return Ok(data);
            }
            catch (Exception ex)
            {
                logger.LogError(ex, $"Failed to add employer wallet history");
                return BadRequest();
            }
        }
        /// <summary>
        /// Author : Thoại Anh
        /// CreatedTime : 04/10/2023
        /// </summary>
        /// <param name="parameters"></param>
        /// <returns></returns>
        [HttpPost("list-employer-wallet-history")]
        public async Task<IActionResult> ListEmployerWalletHistories(DTParameters parameters)
        {
            try
            {
                return Json(await service.ListEmployerWalletHistories(parameters));
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Fail to load list order aggregates");
                return BadRequest();
            }
        }
        /// <summary>
        /// Author: Thoại Anh
        /// CreatedTime : 04/10/2023
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpPut("quick-isApproved")]
        public async Task<IActionResult> QuickIsApproved([Required] long id)
        {
            try
            {
                var response = await service.QuickIsApprovedAsync(id);
                return Ok(response);
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Fail to quick IsApproved async");
                return BadRequest();
            }
        }
        /// <summary>
        /// Author : ThanhND
        /// CreatedTime : 02/10/2023
        /// </summary>
        /// <param name="model">UpdateEmployerWalletHistoryDTO</param>
        /// <returns></returns>
        [HttpPut("update")]
        public async Task<IActionResult> Update([FromBody] UpdateEmployerWalletHistoryDTO model)
        {
            try
            {
                var data = await service.UpdateAsync(model);
                return Ok(data);
            }
            catch (Exception ex)
            {
                logger.LogError(ex, $"Failed to update employer wallet history");
                return BadRequest();
            }
        }

        /// <summary>
        /// Author : ThanhND
        /// CreatedTime : 02/10/2023
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
                logger.LogError(ex, $"Failed to soft delete employer wallet history");
                return BadRequest();
            }
        }


        /// <summary>
        /// Author : ThanhND
        /// CreatedTime :02/10/2023
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
                logger.LogError(ex, $"Failed to get list employer wallet history");
                return BadRequest();
            }
        }
        #endregion


        #region Additional Resources
        [HttpPut("report-cv-candidate")]
        public async Task<IActionResult> ReportCVCandidate([FromBody] ReportCandidateDTO model) 
        {
            try
            {
                model.EmployerId = this.GetLoggedInUserId();
                var data = await service.ReportCVCandidate(model);
                return Ok(data);
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "failed when report cv candidate");
                return BadRequest();
            }
        }
        #endregion
    }
}
