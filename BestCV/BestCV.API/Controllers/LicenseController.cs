using BestCV.Application.Models.JobPosition;
using BestCV.Application.Models.License;
using BestCV.Application.Services.Interfaces;
using BestCV.Core.Utilities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace BestCV.API.Controllers
{
	[Route("api/license")]
	[ApiController]
	public class LicenseController : BaseController
	{
		private readonly ILicenseService service;
		private readonly ILogger<LicenseController> logger;
		public LicenseController(ILicenseService _service, ILoggerFactory loggerFactory)
		{
			service = _service;
			logger = loggerFactory.CreateLogger<LicenseController>();
		}


		#region CRUD

		[HttpGet("detail/{id}")]
		public async Task<IActionResult> Detail([Required] long id)
		{
			try
			{
				var response = await service.GetByIdAsync(id);
				return Ok(response);
			}
			catch (Exception ex)
			{
				logger.LogError(ex, $"Failed to get license by id: {id}");
				return BadRequest();
			}
		}


		[HttpPost("add")]
		public async Task<IActionResult> Add([FromBody] InsertLicenseDTO model)
		{
			try
			{
				var data = await service.CreateAsync(model);
				return Ok(data);
			}
			catch (Exception ex)
			{
				logger.LogError(ex, $"Failed to add license");
				return BadRequest();
			}
		}


		[HttpPut("update")]
		public async Task<IActionResult> Update([FromBody] UpdateLicenseDTO model)
		{
			try
			{
				var data = await service.UpdateAsync(model);
				return Ok(data);
			}
			catch (Exception ex)
			{
				logger.LogError(ex, $"Failed to update license");
				return BadRequest();
			}
		}


		[HttpDelete("delete/{id}")]
		public async Task<IActionResult> Delete([Required] long id)
		{
			try
			{
				var data = await service.SoftDeleteAsync(id);
				return Ok(data);
			}
			catch (Exception ex)
			{
				logger.LogError(ex, $"Failed to soft delete license");
				return BadRequest();
			}
		}


		[HttpGet("listByCompanyId/{id}")]
		public async Task<IActionResult> ListByCompanyId([Required] int id)
		{
			try
			{
				var response = await service.GetListByCompanyId(id);
				return Ok(response);
			}
			catch (Exception ex)
			{
				logger.LogError(ex, $"Failed to get license by company id: {id}");
				return BadRequest();
			}
		}
		#endregion


		#region Additional Resources

		[HttpPost("list-license-aggregates")]
		public async Task<IActionResult> ListLicenseAgrregates(DTParameters parameters)
		{
			try
			{
				return Json(await service.ListLicenseAggregates(parameters));
			}
			catch (Exception ex)
			{
				logger.LogError(ex, "Fail to load list license aggregates");
				return BadRequest();
			}
		}

		[HttpPut("approved")]
		public async Task<IActionResult> ApprovedLicense([FromBody] ApproveLicenseDTO obj)
		{
			try
			{

				var response = await service.UpdateApproveStatusLicenseAsync(obj);
				return Ok(response);

			}
			catch (Exception e)
			{

				logger.LogError(e, $"Failed to approve post: {obj}");
				return BadRequest();
			}
		}
		#endregion
	}
}
