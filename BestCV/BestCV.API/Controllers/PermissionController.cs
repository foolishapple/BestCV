using BestCV.Application.Models.Permissions;
using BestCV.Application.Services.Interfaces;
using BestCV.Domain.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace BestCV.API.Controllers
{
    [Route("api/permission")]
    [ApiController]
    public class PermissionController : BaseController, IBaseController<Permission, InsertPermissionDTO, UpdatePermissionDTO, PermissionDTO, int>
    {
        private readonly ILogger _logger;
        private readonly IPermissionService _permissionService;
        public PermissionController(IPermissionService permissionService, ILoggerFactory loggerFactory)
        {
            _permissionService = permissionService;
            _logger = loggerFactory.CreateLogger<PermissionController>();
        }
        /// <summary>
        /// Author: TUNGTD
        /// Created: 27/07/2023
        /// Description: API POST create new permission
        /// </summary>
        /// <param name="obj">insert permission dto object</param>
        /// <returns></returns>
        [HttpPost("add")]
        public async Task<IActionResult> Add([FromBody] InsertPermissionDTO obj)
        {
            try
            {
                var response = await _permissionService.CreateAsync(obj);
                return Ok(response);
            }
            catch (Exception e)
            {
                _logger.LogError(e, "Failed to create new permission");
                return BadRequest();
            }
        }
        /// <summary>
        /// Auhthor: TUNGTD
        /// Created: 27/07/2023
        /// Description: API POST create list permission
        /// </summary>
        /// <param name="objs">list permission</param>
        /// <returns></returns>
        [HttpPost("add-many")]
        public async Task<IActionResult> AddMany([FromBody] IEnumerable<InsertPermissionDTO> objs)
        {
            try
            {
                var response = await _permissionService.CreateListAsync(objs);
                return Ok(response);
            }
            catch (Exception e)
            {
                _logger.LogError(e, "Failed to create new list permission");
                return BadRequest();
            }
        }
        /// <summary>
        /// Author: TUNGTD
        /// Created: 27/07/2023
        /// Description: API DELETE delete permission by id
        /// </summary>
        /// <param name="id">permission id</param>
        /// <returns></returns>
        [HttpDelete("delete/{id}")]
        public async Task<IActionResult> Delete([Required(ErrorMessage = "Mã quyền không được để trống")] int id)
        {
            try
            {
                var response = await _permissionService.SoftDeleteAsync(id);
                return Ok(response);
            }
            catch (Exception e)
            {
                _logger.LogError(e, $"Failed to delete permission with id:{id}");
                return BadRequest();
            }
        }
        /// <summary>
        /// Author: TUNGTD
        /// Created: 27/07/2023
        /// Description: API GET get permission detail by id
        /// </summary>
        /// <param name="id">permission id</param>
        /// <returns></returns>
        [HttpGet("detail/{id}")]
        public async Task<IActionResult> Detail([Required(ErrorMessage = "Mã quyền không được để trống")]int id)
        {
            try
            {
                var response = await _permissionService.Detail(id);
                return Ok(response);
            }
            catch (Exception e)
            {
                _logger.LogError(e, $"Failed to delete permission by id: {id}");
                return BadRequest();
            }
        }
        /// <summary>
        /// Author: TUNGTD
        /// Created: 27/07/2023
        /// Description: API GET get list all permission
        /// </summary>
        /// <returns></returns>
        [HttpGet("list-all")]
        public async Task<IActionResult> ListAll()
        {
            try
            {
                var response = await _permissionService.GetAllAsync();
                return Ok(response);
            }
            catch (Exception e)
            {
                _logger.LogError(e, "Failed to get list all permission");
                return BadRequest();
            }
        }
        /// <summary>
        /// Author: TUNGTD
        /// Created: 27/07/2023
        /// Description: API PUT update permission
        /// </summary>
        /// <param name="obj">update permission DTO</param>
        /// <returns></returns>
        [HttpPut("update")]
        public async Task<IActionResult> Update([FromBody] UpdatePermissionDTO obj)
        {
            try
            {
                var response = await _permissionService.UpdateAsync(obj);
                return Ok(response);
            }
            catch (Exception e)
            {
                _logger.LogError(e, $"Failed to update permission with id: {obj.Id}");
                return BadRequest();
            }
        }
        /// <summary>
        /// Author: TUNGTD
        /// Created: 27/07/2023
        /// Description: API PUT update list permission
        /// </summary>
        /// <param name="objs">list update permission DTO</param>
        /// <returns></returns>
        [HttpPut("update-many")]
        public async Task<IActionResult> UpdateMany([FromBody] IEnumerable<UpdatePermissionDTO> objs)
        {
            try
            {
                var response = await _permissionService.UpdateListAsync(objs);
                return Ok(response);
            }
            catch (Exception e)
            {
                _logger.LogError(e, "Failed to update  list permission");
                return BadRequest();
            }
        }
    }
}
