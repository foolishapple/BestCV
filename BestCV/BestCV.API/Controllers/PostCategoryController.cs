﻿using BestCV.Application.Models.PostCategory;
using BestCV.Application.Models.PostType;
using BestCV.Application.Models.RecruitmentStatus;
using BestCV.Application.Services.Implement;
using BestCV.Application.Services.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace BestCV.API.Controllers
{
    [Route("api/post-category")]
    [ApiController]
    public class PostCategoryController : BaseController
    {
        private readonly IPostCategoryService postCategoryService;
        private readonly ILogger<PostCategoryController> logger;
        public PostCategoryController(IPostCategoryService postCategoryService, ILoggerFactory loggerFactory)
        {
            this.postCategoryService = postCategoryService;
            this.logger = loggerFactory.CreateLogger<PostCategoryController>();
        }

        #region CRUD


        [HttpGet("list")]
        public async Task<IActionResult> GetList()
        {
            try
            {
                var response = await postCategoryService.GetAllAsync();
                return Ok(response);
            }
            catch (Exception e)
            {
                logger.LogError(e, $"Failed to get post category");
                return BadRequest();
            }
        }


        [HttpGet("detail/{id}")]
        public async Task<IActionResult> Detail([Required] int id)
        {
            try
            {
                var response = await postCategoryService.GetByIdAsync(id);
                return Ok(response);
            }
            catch (Exception e)
            {

                logger.LogError(e, $"Failed to get post category by id: {id}");
                return BadRequest();
            }
        }


        [HttpPost("add")]
        public async Task<IActionResult> Add([FromBody] InsertPostCategoryDTO obj)
        {
            try
            {
                var response = await postCategoryService.CreateAsync(obj);
                return Ok(response);
            }
            catch (Exception e)
            {

                logger.LogError(e, $"Failed to insert post category: {obj}");
                return BadRequest();
            }
        }


        [HttpPut("update")]
        public async Task<IActionResult> Update([FromBody] UpdatePostCategoryDTO obj)
        {
            try
            {

                var response = await postCategoryService.UpdateAsync(obj);
                return Ok(response);

            }
            catch (Exception e)
            {

                logger.LogError(e, $"Failed to update post category: {obj}");
                return BadRequest();
            }
        }


        [HttpDelete("delete")]
        public async Task<IActionResult> Delete([Required] int id)
        {
            try
            {
                var response = await postCategoryService.SoftDeleteAsync(id);
                return Ok(response);

            }
            catch (Exception e)
            {

                logger.LogError(e, $"Failed to delete post category by id: {id}");
                return BadRequest();
            }
        }


        #endregion
    }
}
