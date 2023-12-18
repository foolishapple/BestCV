﻿using BestCV.Application.Models.EmployerActivityLogTypes;
using BestCV.Application.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace BestCV.API.Controllers
{
    [ApiController]
    [Route("api/employer-activity-log-type")]
    public class EmployerActivityLogTypeController : BaseController
    {
        private readonly IEmployerActivityLogTypeService _service;
        private readonly ILogger _logger;
        public EmployerActivityLogTypeController(IEmployerActivityLogTypeService service, ILoggerFactory loggerFactory)
        {
            _service = service;
            _logger = loggerFactory.CreateLogger<EmployerActivityLogTypeController>();
        }
        /// <summary>
        /// Author: TUNGTD
        /// Created: 08/08/2023
        /// Description: API GET get list all Employer Activity Log Type
        /// </summary>
        /// <returns></returns>
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
                _logger.LogError(e, "Failed to get list all Employer Activity Log Type");
                return BadRequest();
            }
        }
        /// <summary>
        /// Author: TUNGTD
        /// Created: 08/08/2023
        /// Description: API GET get Employer Activity Log Type detail by id
        /// </summary>
        /// <param name="id">Employer Activity Log Type id</param>
        /// <returns></returns>
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
                _logger.LogError(e, "Failed to get Employer Activity Log Type detail");
                return BadRequest();
            }
        }
        /// <summary>
        /// Author: TUNGTD
        /// Created: 08/08/2023
        /// Description: API POST create new Employer Activity Log Type
        /// </summary>
        /// <param name="obj">insert Employer Activity Log Type DTO</param>
        /// <returns></returns>
        [HttpPost("add")]
        public async Task<IActionResult> Add([FromBody] InsertEmployerActivityLogTypeDTO obj)
        {
            try
            {
                var response = await _service.CreateAsync(obj);
                return Ok(response);
            }
            catch (Exception e)
            {
                _logger.LogError(e, "Failed to create new Employer Activity Log Type");
                return BadRequest();
            }
        }
        /// <summary>
        /// Author: TUNGTD
        /// Created: 08/08/2023
        /// Description: API PUT update Employer Activity Log Type
        /// </summary>
        /// <param name="obj">update Employer Activity Log Type DTO</param>
        /// <returns></returns>
        [HttpPut("update")]
        public async Task<IActionResult> Update([FromBody] InsertEmployerActivityLogTypeDTO obj)
        {
            try
            {
                var response = await _service.CreateAsync(obj);
                return Ok(response);
            }
            catch (Exception e)
            {
                _logger.LogError(e, "Failed to update new Employer Activity Log Type");
                return BadRequest();
            }
        }
        /// <summary>
        /// Author: TUNGTD
        /// Created: 08/08/2023
        /// Description: API DELETE soft delete Employer Activity Log Type by id
        /// </summary>
        /// <param name="id">Employer Activity Log Type id</param>
        /// <returns></returns>
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
                _logger.LogError(e, "Failed to soft delete Employer Activity Log Type");
                return BadRequest();
            }
        }
    }
}
