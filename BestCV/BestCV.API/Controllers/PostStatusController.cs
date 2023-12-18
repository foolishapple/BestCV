using BestCV.Application.Models.PostStatus;
using BestCV.Application.Models.PostType;
using BestCV.Application.Models.RecruitmentStatus;
using BestCV.Application.Services.Implement;
using BestCV.Application.Services.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace BestCV.API.Controllers
{
    [Route("api/post-status")]
    [ApiController]
    public class PostStatusController : BaseController
    {
        private readonly IPostStatusService postStatusService;
        private readonly ILogger<PostStatusController> logger;
        public PostStatusController(IPostStatusService postStatusService, ILoggerFactory loggerFactory)
        {
            this.postStatusService = postStatusService;
            this.logger = loggerFactory.CreateLogger<PostStatusController>();
        }

        #region CRUD

        /// <summary>
        /// Author: NhatVi
        /// CreatedAt: 25/07/2023
        /// Description: list post status
        /// </summary>
        /// <returns></returns>
        [HttpGet("list")]
        public async Task<IActionResult> GetList()
        {
            try
            {
                var response = await postStatusService.GetAllAsync();
                return Ok(response);
            }
            catch (Exception e)
            {
                logger.LogError(e, $"Failed to get post status");
                return BadRequest();
            }
        }

        /// <summary>
        /// Author: NhatVi
        /// CreatedAt: 25/07/2023
        /// Description: detail post status
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("detail/{id}")]
        public async Task<IActionResult> Detail([Required] int id)
        {
            try
            {
                var response = await postStatusService.GetByIdAsync(id);
                return Ok(response);
            }
            catch (Exception e)
            {

                logger.LogError(e, $"Failed to get post status by id: {id}");
                return BadRequest();
            }
        }

        /// <summary>
        /// Author: NhatVi
        /// CreatedAt: 25/07/2023
        /// Description: insert post status
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        [HttpPost("add")]
        public async Task<IActionResult> Add([FromBody] InsertPostStatusDTO obj)
        {
            try
            {
                var response = await postStatusService.CreateAsync(obj);
                return Ok(response);
            }
            catch (Exception e)
            {

                logger.LogError(e, $"Failed to insert post status: {obj}");
                return BadRequest();
            }
        }

        /// <summary>
        /// Author: NhatVi
        /// CreatedAt: 25/07/2023
        /// Description: update post status
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        [HttpPut("update")]
        public async Task<IActionResult> Update([FromBody] UpdatePostStatusDTO obj)
        {
            try
            {

                var response = await postStatusService.UpdateAsync(obj);
                return Ok(response);

            }
            catch (Exception e)
            {

                logger.LogError(e, $"Failed to update post status: {obj}");
                return BadRequest();
            }
        }

        /// <summary>
        /// Author: NhatVi
        /// CreatedAt: 25/07/2023
        /// Description: delete post status by id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete("delete")]
        public async Task<IActionResult> Delete([Required] int id)
        {
            try
            {
                var response = await postStatusService.SoftDeleteAsync(id);
                return Ok(response);

            }
            catch (Exception e)
            {

                logger.LogError(e, $"Failed to delete post status by id: {id}");
                return BadRequest();
            }
        }


        #endregion
    }
}
