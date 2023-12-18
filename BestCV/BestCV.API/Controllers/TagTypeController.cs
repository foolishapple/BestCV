
using BestCV.Application.Models.TagType;
using BestCV.Application.Services.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace BestCV.API.Controllers
{
    [Route("api/tag-type")]
    [ApiController]
    public class TagtypeController : BaseController
    {
        private readonly ITagTypeService service;
        private readonly ILogger<TagtypeController> logger;
        public TagtypeController(ITagTypeService _service, ILoggerFactory loggerFactory)
        {
            service = _service;
            logger = loggerFactory.CreateLogger<TagtypeController>();
        }

        #region CRUD
        /// <summary>
        /// Author : Thoại Anh
        /// CreatedTime : 29/08/2023
        /// </summary>
        /// <param name="id"></param>
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
                logger.LogError(ex, $"Failed to get tag type by id: {id}");
                return BadRequest();
            }
        }

        /// <summary>
        /// Author : Thoại Anh
        /// CreatedTime :29/08/2023
        /// </summary>
        /// <param name="model">InsertTagTypeDTO</param>
        /// <returns></returns>
        [HttpPost("add")]
        public async Task<IActionResult> Add([FromBody] InsertTagTypeDTO model)
        {
            try
            {
                var data = await service.CreateAsync(model);
                return Ok(data);
            }
            catch (Exception ex)
            {
                logger.LogError(ex, $"Failed to add tag type");
                return BadRequest();
            }
        }

        /// <summary>
        /// Author : Thoại Anh
        /// CreatedTime : 29/08/2023
        /// </summary>
        /// <param name="model">UpdateTagTypeDTO</param>
        /// <returns></returns>
        [HttpPut("update")]
        public async Task<IActionResult> Update([FromBody] UpdateTagTypeDTO model)
        {
            try
            {
                var data = await service.UpdateAsync(model);
                return Ok(data);
            }
            catch (Exception ex)
            {
                logger.LogError(ex, $"Failed to update tag type");
                return BadRequest();
            }
        }

        /// <summary>
        /// Author : Thoại Anh
        /// CreatedTime : 29/08/2023
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete("delete/{id}")]
        public async Task<IActionResult> Delete([Required] int id)
        {
            try
            {
                var data = await service.SoftDeleteAsync(id);
                return Ok(data);
            }
            catch (Exception ex)
            {
                logger.LogError(ex, $"Failed to soft delete tag type");
                return BadRequest();
            }
        }

        /// <summary>
        /// Author : Thoại Anh
        /// CreatedTime : 29/08/2023
        /// </summary>
        /// <returns></returns>
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
                logger.LogError(ex, $"Failed to get list tag type");
                return BadRequest();
            }
        }

        #endregion

        #region Additional Resources
        #endregion
    }
}
