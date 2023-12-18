using BestCV.API.Utilities;
using BestCV.Application.Models.EmployerCarts;
using BestCV.Application.Services.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace BestCV.API.Controllers
{
    [Route("api/employer-cart")]
    [ApiController]
    public class EmployerCartController : BaseController
    {
        private readonly IEmployerCartService service;
        private readonly ILogger<EmployerCartController> logger;
        public EmployerCartController(IEmployerCartService _service, ILoggerFactory loggerFactory)
        {
            service = _service;
            logger = loggerFactory.CreateLogger<EmployerCartController>();
        }

        #region CRUD

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
                logger.LogError(ex, $"Failed to get employer cart by id: {id}");
                return BadRequest();
            }
        }


        [HttpPost("add")]
        public async Task<IActionResult> Add([FromBody] InsertEmployerCartDTO model)
        {
            try
            {
                var data = await service.CreateAsync(model);
                return Ok(data);
            }
            catch (Exception ex)
            {
                logger.LogError(ex, $"Failed to add employer cart");
                return BadRequest();
            }
        }

 
        [HttpPut("update")]
        public async Task<IActionResult> Update([FromBody] UpdateEmployerCartDTO model)
        {
            try
            {
                var data = await service.UpdateAsync(model);
                return Ok(data);
            }
            catch (Exception ex)
            {
                logger.LogError(ex, $"Failed to update cart");
                return BadRequest();
            }
        }


        [HttpDelete("delete")]
        public async Task<IActionResult> Delete([Required] long id)
        {
            try
            {
                var data = await service.SoftDeleteAsync(id);
                return Ok(data);
            }
            catch (Exception ex)
            {
                logger.LogError(ex, $"Failed to soft delete employer cart");
                return BadRequest();
            }
        }


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
                logger.LogError(ex, $"Failed to get list employer cart");
                return BadRequest();
            }
        }
        #endregion

        #region Additional Resources
        [HttpGet("count")]
        public async Task<IActionResult> CountByEmployerId()
        {
            try
            {
                var employerId = this.GetLoggedInUserId();
                var data = await service.CountServicePackageInCart(employerId);
                return Ok(data);

            }
            catch (Exception ex)
            {
                logger.LogError(ex, $"Failed to count employer cart");
                return BadRequest();
            }
        }

        [HttpGet("list-by-employer-id")]
        public async Task<IActionResult> ListByEmployerId()
        {
            try
            {
                var employerId = this.GetLoggedInUserId();
                var data = await service.ListByEmployerId(employerId);
                return Ok(data);

            }
            catch (Exception ex)
            {
                logger.LogError(ex, $"Failed to list employer cart by employer id");
                return BadRequest();
            }
        }


        [HttpPost("add-to-cart/{id}")]
        public async Task<IActionResult> AddToCart(int id)
        {
            try
            {
                var employerId = this.GetLoggedInUserId();
                var data = await service.AddToCart(id, employerId);
                return Ok(data);
            }
            catch (Exception ex)
            {
                logger.LogError(ex, $"Failed to add employer cart");
                return BadRequest();
            }
        }
        #endregion
    }
}
