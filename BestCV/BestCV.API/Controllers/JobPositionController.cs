using BestCV.Application.Models.JobPosition;
using BestCV.Application.Models.SalaryType;
using BestCV.Application.Services.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace BestCV.API.Controllers
{
	[Route("api/job-position")]
	[ApiController]
	public class JobPositionController : BaseController
	{
		private readonly IJobPositionService service;
		private readonly ILogger<JobPositionController> logger;
		public JobPositionController(IJobPositionService _service, ILoggerFactory loggerFactory)
		{
			service = _service;
			logger = loggerFactory.CreateLogger<JobPositionController>();
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
				logger.LogError(ex, $"Failed to get job position by id: {id}");
				return BadRequest();
			}
		}


		[HttpPost("add")]
		public async Task<IActionResult> Add([FromBody] InsertJobPositionDTO model)
		{
			try
			{
				var data = await service.CreateAsync(model);
				return Ok(data);
			}
			catch (Exception ex)
			{
				logger.LogError(ex, $"Failed to add job position");
				return BadRequest();
			}
		}


		[HttpPut("update")]
		public async Task<IActionResult> Update([FromBody] UpdateJobPositionDTO model)
		{
			try
			{
				var data = await service.UpdateAsync(model);
				return Ok(data);
			}
			catch (Exception ex)
			{
				logger.LogError(ex, $"Failed to update job position");
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
				logger.LogError(ex, $"Failed to soft delete job position");
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
				logger.LogError(ex, $"Failed to get list job position");
				return BadRequest();
			}
		}
		#endregion


		#region Additional Resources

		#endregion
	}
}
