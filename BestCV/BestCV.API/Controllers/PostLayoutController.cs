using BestCV.Application.Models.PostLayout;
using BestCV.Application.Models.PostType;
using BestCV.Application.Models.RecruitmentStatus;
using BestCV.Application.Services.Implement;
using BestCV.Application.Services.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace BestCV.API.Controllers
{
    [Route("api/post-layout")]
    [ApiController]
    public class PostLayoutController : BaseController
    {
        private readonly IPostLayoutService postLayoutService;
        private readonly ILogger<PostLayoutController> logger;
        public PostLayoutController(IPostLayoutService postLayoutService, ILoggerFactory loggerFactory)
        {
            this.postLayoutService = postLayoutService;
            this.logger = loggerFactory.CreateLogger<PostLayoutController>();
        }

        #region CRUD

        /// <summary>
        /// Author: NhatVi
        /// CreatedAt: 25/07/2023
        /// Description: list post layout
        /// </summary>
        /// <returns></returns>
        [HttpGet("list")]
        public async Task<IActionResult> GetList()
        {
            try
            {
                var response = await postLayoutService.GetAllAsync();
                return Ok(response);
            }
            catch (Exception e)
            {
                logger.LogError(e, $"Failed to get post layout");
                return BadRequest();
            }
        }

        /// <summary>
        /// Author: NhatVi
        /// CreatedAt: 25/07/2023
        /// Description: detail post layout
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("detail/{id}")]
        public async Task<IActionResult> Detail([Required] int id)
        {
            try
            {
                var response = await postLayoutService.GetByIdAsync(id);
                return Ok(response);
            }
            catch (Exception e)
            {

                logger.LogError(e, $"Failed to get post layout by id: {id}");
                return BadRequest();
            }
        }

        /// <summary>
        /// Author: NhatVi
        /// CreatedAt: 25/07/2023
        /// Description: insert post layout
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        [HttpPost("add")]
        public async Task<IActionResult> Add([FromBody] InsertPostLayoutDTO obj)
        {
            try
            {
                var response = await postLayoutService.CreateAsync(obj);
                return Ok(response);
            }
            catch (Exception e)
            {

                logger.LogError(e, $"Failed to insert post layout: {obj}");
                return BadRequest();
            }
        }

        /// <summary>
        /// Author: NhatVi
        /// CreatedAt: 25/07/2023
        /// Description: update post layout
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        [HttpPut("update")]
        public async Task<IActionResult> Update([FromBody] UpdatePostLayoutDTO obj)
        {
            try
            {

                var response = await postLayoutService.UpdateAsync(obj);
                return Ok(response);

            }
            catch (Exception e)
            {

                logger.LogError(e, $"Failed to update post layout: {obj}");
                return BadRequest();
            }
        }

        /// <summary>
        /// Author: NhatVi
        /// CreatedAt: 25/07/2023
        /// Description: delete post layout by id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete("delete")]
        public async Task<IActionResult> Delete([Required] int id)
        {
            try
            {
                var response = await postLayoutService.SoftDeleteAsync(id);
                return Ok(response);

            }
            catch (Exception e)
            {

                logger.LogError(e, $"Failed to delete post layout by id: {id}");
                return BadRequest();
            }
        }


        #endregion
    }
}
