using BestCV.API.Utilities;
using BestCV.Application.Models.Post;
using BestCV.Application.Models.PostCategory;
using BestCV.Application.Services.Implement;
using BestCV.Application.Services.Interfaces;
using BestCV.Core.Utilities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace BestCV.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PostController : BaseController
    {
        private readonly IPostService postService;
        private readonly ILogger<PostController> logger;
        public PostController(IPostService postService, ILoggerFactory loggerFactory)
        {
            this.postService = postService;
            this.logger = loggerFactory.CreateLogger<PostController>();
        }



        [HttpPost("list-by-post-aggregates")]
        public async Task<IActionResult> GetListPostAggregates([FromBody] DTParameters parameters)
        {
            try
            {
				return Ok(await postService.ListPostAggregatesAsync(parameters));
			}
			catch (Exception e)
            {
                logger.LogError(e, $"Failed to get list post");
                return BadRequest();
            }
        }


        [HttpGet("detail/{id}")]
        public async Task<IActionResult> Detail([Required] int id)
        {
            try
            {
                var response = await postService.GetByIdAsync(id);
                return Ok(response);
            }
            catch (Exception e)
            {

                logger.LogError(e, $"Failed to get post by id: {id}");
                return BadRequest();
            }
        }


        [HttpPost("add")]
        public async Task<IActionResult> Add([FromBody] InsertPostDTO obj)
        {
            try
            {
                var response = await postService.CreateAsync(obj);
                return Ok(response);
            }
            catch (Exception e)
            {

                logger.LogError(e, $"Failed to insert post: {obj}");
                return BadRequest();
            }
        }


        [HttpPut("update")]
        public async Task<IActionResult> Update([FromBody] UpdatePostDTO obj)
        {
            try
            {

                var response = await postService.UpdateAsync(obj);
                return Ok(response);

            }
            catch (Exception e)
            {

                logger.LogError(e, $"Failed to update post: {obj}");
                return BadRequest();
            }
        }


        [HttpDelete("delete")]
        public async Task<IActionResult> Delete([Required] int id)
        {
            try
            {
                var response = await postService.SoftDeleteAsync(id);
                return Ok(response);

            }
            catch (Exception e)
            {

                logger.LogError(e, $"Failed to delete post by id: {id}");
                return BadRequest();
            }
        }

        [HttpPut("approved")]
        public async Task<IActionResult> ApprovedPost([FromBody] ApprovePostDTO obj)
        {
            try
            {

                var response = await postService.UpdateApproveStatusPostAsync(obj);
                return Ok(response);

            }
            catch (Exception e)
            {

                logger.LogError(e, $"Failed to approve post: {obj}");
                return BadRequest();
            }
        }

        [HttpPost("list-post")]
        public async Task<IActionResult> ListPost([FromBody] PostParameters parameters)
        {
            try
            {
                return Ok(await postService.ListPostHomePageAsync(parameters));
            }
            catch (Exception ex)
            {
                logger.LogError(ex, $"Failed to get list post");
                return BadRequest();
            }
        }
  
        [HttpGet("filter-post")]
        public async Task<IActionResult> filterPost()
        {
            try
            {
                var data = await postService.LoadDataFilterPostHomePageAsync();
                return Ok(data);
            }
            catch (Exception ex)
            {
                logger.LogError(ex, $"Failed to filter-post");
                return BadRequest();
            }
        }
    }
}
