using BestCV.Application.Models.JobCategory;
using BestCV.Application.Models.RecruitmentStatus;
using BestCV.Application.Services.Implement;
using BestCV.Application.Services.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace BestCV.API.Controllers
{
    [Route("api/recruitment-status")]
    [ApiController]
    public class RecruitmentStatusController : BaseController
    {
        private readonly IRecruitmentStatusService recruitmentStatusService;
        private readonly ILogger<RecruitmentStatusController> logger;
        public RecruitmentStatusController(IRecruitmentStatusService recruitmentStatusService, ILoggerFactory loggerFactory)
        {
            this.recruitmentStatusService = recruitmentStatusService;
            this.logger = loggerFactory.CreateLogger<RecruitmentStatusController>();
        }

        #region CRUD

        /// <summary>
        /// Author: NhatVi
        /// CreatedAt: 25/07/2023
        /// Description: list recruitment status
        /// </summary>
        /// <returns></returns>
        [HttpGet("list")]
        public async Task<IActionResult> GetList()
        {
            try
            {
                var response = await recruitmentStatusService.GetAllAsync();
                return Ok(response);
            }
            catch (Exception e)
            {
                logger.LogError(e, $"Failed to get recruitment status");
                return BadRequest();
            }
        }

        /// <summary>
        /// Author: NhatVi
        /// CreatedAt: 25/07/2023
        /// Description: detail recruitment status
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("detail/{id}")]
        public async Task<IActionResult> Detail([Required] int id)
        {
            try
            {
                var response = await recruitmentStatusService.GetByIdAsync(id);
                return Ok(response);
            }
            catch (Exception e)
            {

                logger.LogError(e, $"Failed to get recruitment status by id: {id}");
                return BadRequest();
            }
        }

        /// <summary>
        /// Author: NhatVi
        /// CreatedAt: 25/07/2023
        /// Description: insert recruitment status
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        [HttpPost("add")]
        public async Task<IActionResult> Add([FromBody] InsertRecruitmentStatusDTO obj)
        {
            try
            {
                var response = await recruitmentStatusService.CreateAsync(obj);
                return Ok(response);
            }
            catch (Exception e)
            {

                logger.LogError(e, $"Failed to insert recruitment status: {obj}");
                return BadRequest();
            }
        }

        /// <summary>
        /// Author: NhatVi
        /// CreatedAt: 25/07/2023
        /// Description: update recruitment status
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        [HttpPut("update")]
        public async Task<IActionResult> Update([FromBody] UpdateRecruitmentStatusDTO obj)
        {
            try
            {

                var response = await recruitmentStatusService.UpdateAsync(obj);
                return Ok(response);

            }
            catch (Exception e)
            {

                logger.LogError(e, $"Failed to update recruitment status: {obj}");
                return BadRequest();
            }
        }

        /// <summary>
        /// Author: NhatVi
        /// CreatedAt: 25/07/2023
        /// Description: delete recruitment status by id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete("delete")]
        public async Task<IActionResult> Delete([Required] int id)
        {
            try
            {
                var response = await recruitmentStatusService.SoftDeleteAsync(id);
                return Ok(response);

            }
            catch (Exception e)
            {

                logger.LogError(e, $"Failed to delete recruitment status by id: {id}");
                return BadRequest();
            }
        }


        #endregion
    }
}
