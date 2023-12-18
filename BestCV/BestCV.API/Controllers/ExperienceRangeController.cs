using BestCV.Application.Models.CouponType;
using BestCV.Application.Models.ExperienceRange;
using BestCV.Application.Services.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace BestCV.API.Controllers
{
	[Route("api/experience-range")]
	[ApiController]
	public class ExperienceRangeController : BaseController
	{
		private readonly IExperienceRangeService service;
		private readonly ILogger<ExperienceRangeController> logger;
		public ExperienceRangeController(IExperienceRangeService _service, ILoggerFactory loggerFactory)
		{
			service = _service;
			logger = loggerFactory.CreateLogger<ExperienceRangeController>();
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
				logger.LogError(ex, $"Failed to get experience range by id: {id}");
				return BadRequest();
			}
		}


		[HttpPost("add")]
		public async Task<IActionResult> Add([FromBody] InsertExperienceRangeDTO model)
		{
			try
			{
				var data = await service.CreateAsync(model);
				return Ok(data);
			}
			catch (Exception ex)
			{
				logger.LogError(ex, $"Failed to add experience range");
				return BadRequest();
			}
		}


		[HttpPut("update")]
		public async Task<IActionResult> Update([FromBody] UpdateExperienceRangeDTO model)
		{
			try
			{
				var data = await service.UpdateAsync(model);
				return Ok(data);
			}
			catch (Exception ex)
			{
				logger.LogError(ex, $"Failed to update experience range");
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
				logger.LogError(ex, $"Failed to soft delete experience range");
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
				logger.LogError(ex, $"Failed to get list experience range");
				return BadRequest();
			}
		}
		#endregion


		#region Additional Resources

		#endregion
	}
}
