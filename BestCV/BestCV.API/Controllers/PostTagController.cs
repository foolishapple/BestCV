using BestCV.Application.Models.PostCategory;
using BestCV.Application.Models.PostTag;
using BestCV.Application.Models.PostType;
using BestCV.Application.Models.RecruitmentStatus;
using BestCV.Application.Services.Implement;
using BestCV.Application.Services.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace BestCV.API.Controllers
{
    [Route("api/post-tag")]
    [ApiController]
    public class PostTagController : BaseController
    {
        private readonly IPostTagService postTagService;
        private readonly ILogger<PostTagController> logger;
        public PostTagController(IPostTagService postTagService, ILoggerFactory loggerFactory)
        {
            this.postTagService = postTagService;
            this.logger = loggerFactory.CreateLogger<PostTagController>();
        }

        #region CRUD

        /// <summary>
        /// Author: NhatVi
        /// CreatedAt: 01/08/2023
        /// Description: list post tag
        /// </summary>
        /// <returns></returns>
        [HttpGet("list")]
        public async Task<IActionResult> GetList()
        {
            try
            {
                var response = await postTagService.GetAllAsync();
                return Ok(response);
            }
            catch (Exception e)
            {
                logger.LogError(e, $"Failed to get post tag");
                return BadRequest();
            }
        }

        /// <summary>
        /// Author: NhatVi
        /// CreatedAt: 01/08/2023
        /// Description: detail post tag
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("detail/{id}")]
        public async Task<IActionResult> Detail([Required] int id)
        {
            try
            {
                var response = await postTagService.GetByIdAsync(id);
                return Ok(response);
            }
            catch (Exception e)
            {

                logger.LogError(e, $"Failed to get post tag by id: {id}");
                return BadRequest();
            }
        }

        /// <summary>
        /// Author: NhatVi
        /// CreatedAt: 01/08/2023
        /// Description: insert post tag
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        [HttpPost("add")]
        public async Task<IActionResult> Add([FromBody] InsertPostTagDTO obj)
        {
            try
            {
                var response = await postTagService.CreateAsync(obj);
                return Ok(response);
            }
            catch (Exception e)
            {

                logger.LogError(e, $"Failed to insert post tag: {obj}");
                return BadRequest();
            }
        }

        /// <summary>
        /// Author: NhatVi
        /// CreatedAt: 01/08/2023
        /// Description: update post tag
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        [HttpPut("update")]
        public async Task<IActionResult> Update([FromBody] UpdatePostTagDTO obj)
        {
            try
            {

                var response = await postTagService.UpdateAsync(obj);
                return Ok(response);

            }
            catch (Exception e)
            {

                logger.LogError(e, $"Failed to update post tag: {obj}");
                return BadRequest();
            }
        }

        /// <summary>
        /// Author: NhatVi
        /// CreatedAt: 01/08/2023
        /// Description: delete post tag by id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete("delete")]
        public async Task<IActionResult> Delete([Required] int id)
        {
            try
            {
                var response = await postTagService.SoftDeleteAsync(id);
                return Ok(response);

            }
            catch (Exception e)
            {

                logger.LogError(e, $"Failed to delete post tag by id: {id}");
                return BadRequest();
            }
        }


        #endregion
    }
}
