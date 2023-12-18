using BestCV.Application.Models.JobPosition;
using BestCV.Application.Models.JobSkill;
using BestCV.Application.Services.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace BestCV.API.Controllers
{
	[Route("api/job-skill")]
	[ApiController]
	public class JobSkillController : BaseController
	{
		private readonly IJobSkillService service;
		private readonly ILogger<JobSkillController> logger;
		public JobSkillController(IJobSkillService _service, ILoggerFactory loggerFactory)
		{
			service = _service;
			logger = loggerFactory.CreateLogger<JobSkillController>();
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
				logger.LogError(ex, $"Failed to get job skill by id: {id}");
				return BadRequest();
			}
		}


		[HttpPost("add")]
		public async Task<IActionResult> Add([FromBody] InsertJobSkillDTO model)
		{
			try
			{
				var data = await service.CreateAsync(model);
				return Ok(data);
			}
			catch (Exception ex)
			{
				logger.LogError(ex, $"Failed to add job skill");
				return BadRequest();
			}
		}

		[HttpPut("update")]
		public async Task<IActionResult> Update([FromBody] UpdateJobSkillDTO model)
		{
			try
			{
				var data = await service.UpdateAsync(model);
				return Ok(data);
			}
			catch (Exception ex)
			{
				logger.LogError(ex, $"Failed to update job skill");
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
				logger.LogError(ex, $"Failed to soft delete job skill");
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
				logger.LogError(ex, $"Failed to get list job skill");
				return BadRequest();
			}
		}
		#endregion


		#region Additional Resources

		#endregion
	}
}
