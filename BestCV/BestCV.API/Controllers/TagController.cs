using BestCV.Application.Models.PostCategory;
using BestCV.Application.Models.PostType;
using BestCV.Application.Models.RecruitmentStatus;
using BestCV.Application.Models.Tag;
using BestCV.Application.Services.Implement;
using BestCV.Application.Services.Interfaces;
using BestCV.Core.Utilities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace BestCV.API.Controllers
{
    [Route("api/tag")]
    [ApiController]
    public class TagController : BaseController
    {
        private readonly ITagService tagService;
        private readonly ILogger<TagController> logger;
        public TagController(ITagService tagService, ILoggerFactory loggerFactory)
        {
            this.tagService = tagService;
            this.logger = loggerFactory.CreateLogger<TagController>();
        }

        #region CRUD

        [HttpGet("list")]
        public async Task<IActionResult> GetList()
        {
            try
            {
                var response = await tagService.GetAllAsync();
                return Ok(response);
            }
            catch (Exception e)
            {
                logger.LogError(e, $"Failed to get tag");
                return BadRequest();
            }
        }

        [HttpGet("detail/{id}")]
        public async Task<IActionResult> Detail([Required] int id)
        {
            try
            {
                var response = await tagService.GetByIdAsync(id);
                return Ok(response);
            }
            catch (Exception e)
            {

                logger.LogError(e, $"Failed to get tag by id: {id}");
                return BadRequest();
            }
        }

        [HttpPost("add")]
        public async Task<IActionResult> Add([FromBody] InsertTagDTO obj)
        {
            try
            {
                var response = await tagService.CreateAsync(obj);
                return Ok(response);
            }
            catch (Exception e)
            {

                logger.LogError(e, $"Failed to insert tag: {obj}");
                return BadRequest();
            }
        }


        [HttpPut("update")]
        public async Task<IActionResult> Update([FromBody] UpdateTagDTO obj)
        {
            try
            {

                var response = await tagService.UpdateAsync(obj);
                return Ok(response);

            }
            catch (Exception e)
            {

                logger.LogError(e, $"Failed to update tag: {obj}");
                return BadRequest();
            }
        }

 
        [HttpDelete("delete")]
        public async Task<IActionResult> Delete([Required] int id)
        {
            try
            {
                var response = await tagService.SoftDeleteAsync(id);
                return Ok(response);

            }
            catch (Exception e)
            {

                logger.LogError(e, $"Failed to delete tag by id: {id}");
                return BadRequest();
            }
        }


        [HttpPost("add-tag-for-post")]
        public async Task<IActionResult> AddTagForPost([FromBody] InsertTagDTO obj)
        {
            try
            {
                var response = await tagService.AddTagForPostAsync(obj);
                return Ok(response);
            }
            catch (Exception e)
            {

                logger.LogError(e, $"Failed to insert tag type post: {obj}");
                return BadRequest();
            }
        }


        [HttpGet("list-tag-select")]
        public async Task<IActionResult> GetListTagSelect([FromQuery] TagForSelect2DTO obj)
        {
            try
            {
                var response = await tagService.ListSelectTagAsync(obj);
                return Ok(response);
            }
            catch (Exception e)
            {
                logger.LogError(e, $"Failed to get tag");
                return BadRequest();
            }
        }



        [HttpGet("list-tag-type-job")]
        public async Task<IActionResult> GetListTagTypeJob()
        {
            try
            {
                var response = await tagService.ListTagTypeJob();
                return Ok(response);
            }
            catch (Exception e)
            {
                logger.LogError(e, $"Failed to get list tag type job");
                return BadRequest();
            }
        }




        [HttpPost("add-tag-for-job")]
        public async Task<IActionResult> AddTagForJob(InsertTagDTO obj)
        {
            try
            {
                var response = await tagService.AddTagForJobAsync(obj);
                return Ok(response);
            }
            catch (Exception e)
            {

                logger.LogError(e, $"Failed to insert tag type job: {obj}");
                return BadRequest();
            }
        }

        [HttpGet("list-post-tag")]
        public async Task<IActionResult> ListPostTag()
        {
            try
            {
                var response = await tagService.ListTagTypePost();
                return Ok(response);
            }
            catch (Exception e)
            {
                logger.LogError(e, $"Failed to get tag");
                return BadRequest();
            }
        }

        [HttpPost]
        [Route("list-tag-aggregates-async")]
        public async Task<IActionResult> ListTagAggregatesAsync([FromBody] DTParameters parameters)
        {
            try
            {
                return Json(await tagService.ListTagAggregatesAsync(parameters));
            }
            catch (Exception ex)
            {
                logger.LogError(ex, $"Failed to get tag");
                return BadRequest();
            }
        }
        #endregion
    }
}
