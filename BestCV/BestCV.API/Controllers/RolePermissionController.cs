using BestCV.Application.Models.RolePermissions;
using BestCV.Application.Services.Interfaces;
using BestCV.Core.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace BestCV.API.Controllers
{
    [Route("api/role-permission")]
    [ApiController]
    public class RolePermissionController : BaseController
    {
        private readonly IRolePermissionService _service;
        private readonly ILogger _logger;
        public RolePermissionController(IRolePermissionService service, ILoggerFactory loggerFactory)
        {
            _service = service;
            _logger = loggerFactory.CreateLogger<RolePermissionController>();
        }
        /// <summary>
        /// Author: TUNGTD
        /// Created: 08/08/2023
        /// Description: Get list all role permission
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
                _logger.LogError(e, "Failed to get list all role permission");
                return BadRequest();
            }
        }
        /// <summary>
        /// Author: TUNNGTD
        /// Created: 09/08/2023
        /// Description: Update list role permission
        /// </summary>
        /// <param name="obj">update list role permission DTO</param>
        /// <returns></returns>
        [HttpPost("update-list")]
        public async Task<IActionResult> UpdateList([FromBody] UpdateListRolePermissionDTO obj)
        {
            try
            {
                var response = await _service.UpdateList(obj);
                return Ok(response);
            }
            catch (Exception e)
            {
                _logger.LogError(e, "Failed to update lis role permission");
                return BadRequest();
            }
        }
    }
}
