using BestCV.Application.Models.Slides;
using BestCV.Application.Models.SystemConfigs;
using BestCV.Application.Services.Interfaces;
using BestCV.Core.Entities;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace BestCV.API.Controllers
{
    [ApiController]
    [Route("api/slide")]
    public class SlideController : BaseController
    {
        private readonly ISlideService _service;
        private readonly ILogger _logger;
        public SlideController(ISlideService service, ILoggerFactory loggerFactory)
        {
            _service = service;
            _logger = loggerFactory.CreateLogger<SlideController>();
        }

        [HttpGet("list")]
        public async Task<IActionResult> List()
        {
            try
            {
                var response = await _service.GetAllAsync();
                return Ok(response);
            }
            catch (Exception e)
            {
                _logger.LogError(e, "Failed to get list all slide");
                return BadRequest();
            }
        }

        [HttpGet("detail/{id}")]
        public async Task<IActionResult> Detail([Required(ErrorMessage = "Mã không được để trống")] int id)
        {
            try
            {
                var response = await _service.GetByIdAsync(id);
                return Ok(response);
            }
            catch (Exception e)
            {
                _logger.LogError(e, "Failed to get slide detail");
                return BadRequest();
            }
        }

        [HttpPost("add")]
        public async Task<IActionResult> Add([FromBody] InsertSlideDTO obj)
        {
            try
            {
                var response = await _service.CreateAsync(obj);
                return Ok(response);
            }
            catch (Exception e)
            {
                _logger.LogError(e, "Failed to create new slide");
                return BadRequest();
            }
        }

        [HttpPut("update")]
        public async Task<IActionResult> Update([FromBody] UpdateSlideDTO obj)
        
        {
            try
            {
                var response = await _service.UpdateAsync(obj);
                return Ok(response);
            }
            catch (Exception e)
            {
                _logger.LogError(e, "Failed to update new slide");
                return BadRequest();
            }
        }

        [HttpDelete("delete/{id}")]
        public async Task<IActionResult> Delete([Required(ErrorMessage = "Mã không được để trống")] int id)
        {
            try
            {
                var response = await _service.SoftDeleteAsync(id);
                return Ok(response);
            }
            catch (Exception e)
            {
                _logger.LogError(e, "Failed to soft delete slide");
                return BadRequest();
            }
        }
        [HttpPost]
        [Route("change-order-slide")]
        public async Task<IActionResult> ChangeOrderSort([FromBody] ChangeSlideDTO model)
        {
            try
            {
                var isSuccess = await _service.ChangeOrderSlide(model);
                if (isSuccess)
                {
                    return Ok(BestCVResponse.Success());
                }
                return Ok(BestCVResponse.BadRequest("Fail when change order sort"));
            }
            catch (Exception ex)
            {
                _logger.LogError("Fail when change order sort", ex);
                return BadRequest();
            }
        }


        [HttpGet("list-on-homepage")]
        public async Task<IActionResult> ListOnHomepage()
        {
            try
            {
                var response = await _service.ListSlideShowonHomepage();
                return Ok(response);
            }
            catch (Exception e)
            {
                _logger.LogError(e, "Failed to get list slide show on home page");
                return BadRequest();
            }
        }
    }
}
