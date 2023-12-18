using BestCV.Application.Models.Coupon;
using BestCV.Application.Models.JobSkill;
using BestCV.Application.Services.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace BestCV.API.Controllers
{
	[Route("api/coupon")]
	[ApiController]
	public class CouponController : BaseController
	{
		private readonly ICouponService service;
		private readonly ILogger<CouponController> logger;
		public CouponController(ICouponService _service, ILoggerFactory loggerFactory)
		{
			service = _service;
			logger = loggerFactory.CreateLogger<CouponController>();
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
				logger.LogError(ex, $"Failed to get coupon by id: {id}");
				return BadRequest();
			}
		}


		[HttpPost("add")]
		public async Task<IActionResult> Add([FromBody] InsertCouponDTO model)
		{
			try
			{
				var data = await service.CreateAsync(model);
				return Ok(data);
			}
			catch (Exception ex)
			{
				logger.LogError(ex, $"Failed to add coupon");
				return BadRequest();
			}
		}


		[HttpPut("update")]
		public async Task<IActionResult> Update([FromBody] UpdateCouponDTO model)
		{
			try
			{
				var data = await service.UpdateAsync(model);
				return Ok(data);
			}
			catch (Exception ex)
			{
				logger.LogError(ex, $"Failed to update coupon");
				return BadRequest();
			}
		}


		[HttpDelete("delete")]
		public async Task<IActionResult> Delete([Required] int id)
		{
			try
			{
				var data = await service.SoftDeleteAsync(id);
				return Ok(data);
			}
			catch (Exception ex)
			{
				logger.LogError(ex, $"Failed to soft delete coupon");
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
				logger.LogError(ex, $"Failed to get list coupon");
				return BadRequest();
			}
		}
        #endregion


        #region Additional Resources

        [HttpGet("list-aggregates")]
        public async Task<IActionResult> GetListAggregates()
        {
            try
            {
                var data = await service.ListAggregatesAsync();
                return Ok(data);
            }
            catch (Exception ex)
            {
                logger.LogError(ex, $"Failed to get list coupon aggregates");
                return BadRequest();
            }
        }
        #endregion
    }
}
