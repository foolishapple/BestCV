using BestCV.Application.Models.PostType;
using BestCV.Application.Models.RecruitmentStatus;
using BestCV.Application.Services.Implement;
using BestCV.Application.Services.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace BestCV.API.Controllers
{
    [Route("api/post-type")]
    [ApiController]
    public class PostTypeController : BaseController
    {
        private readonly IPostTypeService postTypeService;
        private readonly ILogger<PostTypeController> logger;
        public PostTypeController(IPostTypeService postTypeService, ILoggerFactory loggerFactory)
        {
            this.postTypeService = postTypeService;
            this.logger = loggerFactory.CreateLogger<PostTypeController>();
        }

        #region CRUD


        [HttpGet("list")]
        public async Task<IActionResult> GetList()
        {
            try
            {
                var response = await postTypeService.GetAllAsync();
                return Ok(response);
            }
            catch (Exception e)
            {
                logger.LogError(e, $"Failed to get post type");
                return BadRequest();
            }
        }


        [HttpGet("detail/{id}")]
        public async Task<IActionResult> Detail([Required] int id)
        {
            try
            {
                var response = await postTypeService.GetByIdAsync(id);
                return Ok(response);
            }
            catch (Exception e)
            {

                logger.LogError(e, $"Failed to get post type by id: {id}");
                return BadRequest();
            }
        }


        [HttpPost("add")]
        public async Task<IActionResult> Add([FromBody] InsertPostTypeDTO obj)
        {
            try
            {
                var response = await postTypeService.CreateAsync(obj);
                return Ok(response);
            }
            catch (Exception e)
            {

                logger.LogError(e, $"Failed to insert post type: {obj}");
                return BadRequest();
            }
        }


        [HttpPut("update")]
        public async Task<IActionResult> Update([FromBody] UpdatePostTypeDTO obj)
        {
            try
            {

                var response = await postTypeService.UpdateAsync(obj);
                return Ok(response);

            }
            catch (Exception e)
            {

                logger.LogError(e, $"Failed to update post type: {obj}");
                return BadRequest();
            }
        }


        [HttpDelete("delete")]
        public async Task<IActionResult> Delete([Required] int id)
        {
            try
            {
                var response = await postTypeService.SoftDeleteAsync(id);
                return Ok(response);

            }
            catch (Exception e)
            {

                logger.LogError(e, $"Failed to delete post type by id: {id}");
                return BadRequest();
            }
        }


        #endregion
    }
}
