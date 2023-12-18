using BestCV.Application.Models.CouponType;
using BestCV.Application.Models.SalaryType;
using BestCV.Application.Services.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace BestCV.API.Controllers
{
	[Route("api/coupon-type")]
	[ApiController]
	public class CouponTypeController : BaseController
	{
		private readonly ICouponTypeService service;
		private readonly ILogger<CouponTypeController> logger;
		public CouponTypeController(ICouponTypeService _service, ILoggerFactory loggerFactory)
		{
			service = _service;
			logger = loggerFactory.CreateLogger<CouponTypeController>();
		}

		#region CRUD
		/// <summary>
		/// Author : ThanhND
		/// CreatedTime : 26/07/2023
		/// </summary>
		/// <param name="id">couponTypeId</param>
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
				logger.LogError(ex, $"Failed to get coupon type by id: {id}");
				return BadRequest();
			}
		}

		/// <summary>
		/// Author : ThanhND
		/// CreatedTime : 26/07/2023
		/// </summary>
		/// <param name="model">InsertCouponTypeDTO</param>
		/// <returns></returns>
		[HttpPost("add")]
		public async Task<IActionResult> Add([FromBody] InsertCouponTypeDTO model)
		{
			try
			{
				var data = await service.CreateAsync(model);
				return Ok(data);
			}
			catch (Exception ex)
			{
				logger.LogError(ex, $"Failed to add coupon type");
				return BadRequest();
			}
		}

		/// <summary>
		/// Author : ThanhND
		/// CreatedTime : 26/07/2023
		/// </summary>
		/// <param name="model">UpdateCouponTypeDTO</param>
		/// <returns></returns>
		[HttpPut("update")]
		public async Task<IActionResult> Update([FromBody] UpdateCouponTypeDTO model)
		{
			try
			{
				var data = await service.UpdateAsync(model);
				return Ok(data);
			}
			catch (Exception ex)
			{
				logger.LogError(ex, $"Failed to update coupon type");
				return BadRequest();
			}
		}

		/// <summary>
		/// Author : ThanhND
		/// CreatedTime : 26/07/2023
		/// </summary>
		/// <param name="id">couponTypeId</param>
		/// <returns></returns>
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
				logger.LogError(ex, $"Failed to soft delete coupon type");
				return BadRequest();
			}
		}

		/// <summary>
		/// Author : ThanhND
		/// CreatedTime : 26/07/2023
		/// </summary>
		/// <returns>List CouponType</returns>
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
				logger.LogError(ex, $"Failed to get list coupon type");
				return BadRequest();
			}
		}
		#endregion

		#region Additional Resources

		#endregion
	}
}
