using BestCV.Application.Models.AdminAccounts;
using BestCV.Application.Models.WorkPlace;
using BestCV.Application.Services.Interfaces;
using BestCV.Core.Utilities;
using BestCV.Infrastructure.Repositories.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;
using System.Security.Principal;

namespace BestCV.API.Controllers
{
    [Route("api/workplace")]
    [ApiController]
    public class WorkplaceController : BaseController
    {
        private readonly IWorkplaceService _workplaceService;
        private readonly ILogger<WorkplaceController> _logger;
        public WorkplaceController(IWorkplaceService workplaceService, ILoggerFactory loggerFactory)
        {
            _workplaceService = workplaceService;
            _logger = loggerFactory.CreateLogger<WorkplaceController>();
        }

        #region CRUD

        [HttpGet("detail/{id}")]
        public async Task<IActionResult> Detail([Required] int id)
        {
            try
            {
                var response = await _workplaceService.GetByIdAsync(id);
                return Ok(response);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Failed to get workplace by id: {id}");
                return BadRequest();
            }
        }

        [HttpGet("list")]
        public async Task<IActionResult> GetList()
        {
            try
            {
                var response = await _workplaceService.GetAllAsync();
                return Ok(response);
            }
            catch (Exception e)
            {
                _logger.LogError(e, $"Failed to get all workplace");
                return BadRequest();
            }
        }
        #endregion


        #region Additional Resources


        [HttpGet("get-province-data")]
        public async Task<IActionResult> GetProvinceData()
        {
            try
            {
                var response = await _workplaceService.GetProvinceDataAsync();
                return Ok(response);
            }
            catch(Exception ex)
            {
                _logger.LogError(ex, $"Failed to get province data");
                return BadRequest();
            }
        }

        [HttpGet("list-district-by-cityid/{cityId}")]
        public async Task<IActionResult> GetListDistrictByCityId(int cityId) {
            try
            {
                var response = await _workplaceService.GetListDistrictByCityIdAsync(cityId);
                return Ok(response);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Failed to get list district data");
                return BadRequest();
            }
        }


        [HttpGet("list-city")]
        public async Task<IActionResult> GetListCity()
        {
            try
            {
                var response = await _workplaceService.GetListCityAsync();
                return Ok(response);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Failed to get list city data");
                return BadRequest();
            }
        }
        #endregion
    }
}
