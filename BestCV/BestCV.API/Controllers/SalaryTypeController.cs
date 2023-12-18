using BestCV.Application.Models.SalaryType;
using BestCV.Application.Services.Interfaces;
using BestCV.Application.Validators.SalaryType;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace BestCV.API.Controllers
{
	[Route("api/salary-type")]
	[ApiController]
	public class SalaryTypeController : BaseController
	{
		private readonly ISalaryTypeService service;
		private readonly ILogger<SalaryTypeController> logger;
		public SalaryTypeController(ISalaryTypeService _service, ILoggerFactory loggerFactory)
		{
			service = _service;
			logger = loggerFactory.CreateLogger<SalaryTypeController>();
		}


		#region CRUD
		/// <summary>
		/// Author : ThanhND
		/// CreatedTime : 26/07/2023
		/// </summary>
		/// <param name="id">salaryTypeId</param>
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
				logger.LogError(ex, $"Failed to get salary type by id: {id}");
				return BadRequest();
			}
		}

		/// <summary>
		/// Author : ThanhND
		/// CreatedTime : 26/07/2023
		/// </summary>
		/// <param name="model">InsertSalaryTypeDTO</param>
		/// <returns></returns>
		[HttpPost("add")]
		public async Task<IActionResult> Add([FromBody] InsertSalaryTypeDTO model)
		{
			try
			{

				var data = await service.CreateAsync(model);
				return Ok(data);
			}
			catch (Exception ex)
			{
				logger.LogError(ex, $"Failed to add salary type");
				return BadRequest();
			}
		}

		/// <summary>
		/// Author : ThanhND
		/// CreatedTime : 26/07/2023
		/// </summary>
		/// <param name="model">UpdateSalaryTypeDTO</param>
		/// <returns></returns>
		[HttpPut("update")]
		public async Task<IActionResult> Update([FromBody] UpdateSalaryTypeDTO model)
		{
			try
			{
				var data = await service.UpdateAsync(model);
				return Ok(data);
			}
			catch (Exception ex)
			{
				logger.LogError(ex, $"Failed to update salary type");
				return BadRequest();
			}
		}

		/// <summary>
		/// Author : ThanhND
		/// CreatedTime : 26/07/2023
		/// </summary>
		/// <param name="id">salaryTypeId</param>
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
				logger.LogError(ex, $"Failed to soft delete salary type");
				return BadRequest();
			}
		}

		/// <summary>
		/// Author : ThanhND
		/// CreatedTime : 26/07/2023
		/// </summary>
		/// <returns>List SalaryType</returns>
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
				logger.LogError(ex, $"Failed to get list salary type");
				return BadRequest();
			}
		}
		#endregion


		#region Additional Resources

		#endregion
	}
}
