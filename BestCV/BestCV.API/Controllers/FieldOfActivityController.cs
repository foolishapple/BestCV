﻿using BestCV.Application.Models.FieldOfActivities;
using BestCV.Application.Services.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace BestCV.API.Controllers
{
	[Route("api/field-of-activity")]
	[ApiController]
	public class FieldOfActivityController : BaseController
	{
		private readonly IFieldOfActivityService _service;
		private readonly ILogger _logger;
		public FieldOfActivityController(IFieldOfActivityService service, ILoggerFactory loggerFactory)
		{
			_service = service;
			_logger = loggerFactory.CreateLogger<FieldOfActivityController>();
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
				_logger.LogError(e, "Failed to get list all Field Of Activity");
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
				_logger.LogError(e, "Failed to get Field Of Activity detail");
				return BadRequest();
			}
		}

		[HttpPost("add")]
		public async Task<IActionResult> Add([FromBody] InsertFieldOfActivityDTO obj)
		{
			try
			{
				var response = await _service.CreateAsync(obj);
				return Ok(response);
			}
			catch (Exception e)
			{
				_logger.LogError(e, "Failed to create new Field Of Activity");
				return BadRequest();
			}
		}

		[HttpPut("update")]
		public async Task<IActionResult> Update([FromBody] UpdateFieldOfActivityDTO obj)
		{
			try
			{
				var response = await _service.UpdateAsync(obj);
				return Ok(response);
			}
			catch (Exception e)
			{
				_logger.LogError(e, "Failed to update new Field Of Activity");
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
				_logger.LogError(e, "Failed to soft delete Field Of Activity");
				return BadRequest();
			}
		}


		[HttpGet("filter-field-of-activity")]
		public async Task<IActionResult> filterJob()
		{
			try
			{
				var data = await _service.LoadDataFilterFielOfActivityHomePageAsync();
				return Ok(data);
			}
			catch (Exception ex)
			{
				_logger.LogError(ex, $"Failed to searching job");
				return BadRequest();
			}
		}
	}
}
