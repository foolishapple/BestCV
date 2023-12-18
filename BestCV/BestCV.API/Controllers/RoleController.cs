using BestCV.Application.Models.Roles;
using BestCV.Application.Services.Interfaces;
using BestCV.Core.Entities;
using BestCV.Domain.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace BestCV.API.Controllers
{
    [Route("api/role")]
    [ApiController]
    public class RoleController : BaseController, IBaseController<Role,InsertRoleDTO,UpdateRoleDTO,RoleDTO,int>
    {
        private readonly ILogger _logger;
        private readonly IRoleService _roleService;
        public RoleController(IRoleService roleService, ILoggerFactory loggerFactory)
        {
            _roleService = roleService;
            _logger = loggerFactory.CreateLogger<RoleController>();
        }
        /// <summary>
        /// Author: TUNGTD
        /// Created: 27/07/2023
        /// Description: API POST create new role
        /// </summary>
        /// <param name="obj">insert role dto object</param>
        /// <returns></returns>
        [HttpPost("add")]
        public async Task<IActionResult> Add([FromBody] InsertRoleDTO obj)
        {
            try
            {
                var response = await _roleService.CreateAsync(obj);
                return Ok(response);
            }
            catch (Exception e)
            {
                _logger.LogError(e, "Failed to create new role");
                return BadRequest();
            }
        }
        /// <summary>
        /// Auhthor: TUNGTD
        /// Created: 27/07/2023
        /// Description: API POST create list role
        /// </summary>
        /// <param name="objs">list role</param>
        /// <returns></returns>
        [HttpPost("add-many")]
        public async Task<IActionResult> AddMany([FromBody] IEnumerable<InsertRoleDTO> objs)
        {
            try
            {
                var response = await _roleService.CreateListAsync(objs);
                return Ok(response);
            }
            catch (Exception e)
            {
                _logger.LogError(e, "Failed to create new list role");
                return BadRequest();
            }
        }
        /// <summary>
        /// Author: TUNGTD
        /// Created: 27/07/2023
        /// Description: API DELETE delete role by id
        /// </summary>
        /// <param name="id">role id</param>
        /// <returns></returns>
        [HttpDelete("delete/{id}")]
        public async Task<IActionResult> Delete([Required(ErrorMessage = "Mã vai trò không được để trống")]int id)
        {
            try
            {
                var response = await _roleService.SoftDeleteAsync(id);
                return Ok(response);
            }
            catch (Exception e)
            {
                _logger.LogError(e, $"Failed to delete role with id:{id}");
                return BadRequest();
            }
        }
        /// <summary>
        /// Author: TUNGTD
        /// Created: 27/07/2023
        /// Description: API GET get role detail by id
        /// </summary>
        /// <param name="id">role id</param>
        /// <returns></returns>
        [HttpGet("detail/{id}")]
        public async Task<IActionResult> Detail(int id)
        {
            try
            {
                var response = await _roleService.GetByIdAsync(id);
                return Ok(response);
            }
            catch (Exception e)
            {
                _logger.LogError(e, $"Failed to delete role by id: {id}");
                return BadRequest();
            }
        }
        /// <summary>
        /// Author: TUNGTD
        /// Created: 27/07/2023
        /// Description: API GET get list all role
        /// </summary>
        /// <returns></returns>
        [HttpGet("list-all")]
        public async Task<IActionResult> ListAll()
        {
            try
            {
                var response = await _roleService.GetAllAsync();
                return Ok(response);
            }
            catch (Exception e)
            {
                _logger.LogError(e, "Failed to get list all role");
                return BadRequest();
            }
        }
        /// <summary>
        /// Author: TUNGTD
        /// Created: 27/07/2023
        /// Description: API PUT update role
        /// </summary>
        /// <param name="obj">update role DTO</param>
        /// <returns></returns>
        [HttpPut("update")]
        public async Task<IActionResult> Update([FromBody] UpdateRoleDTO obj)
        {
            try
            {
                var response = await _roleService.UpdateAsync(obj);
                return Ok(response);
            }
            catch (Exception e)
            {
                _logger.LogError(e, $"Failed to update role with id: {obj.Id}");
                return BadRequest();
            }
        }
        /// <summary>
        /// Author: TUNGTD
        /// Created: 27/07/2023
        /// Description: API PUT update list role
        /// </summary>
        /// <param name="obj">list update role DTO</param>
        /// <returns></returns>
        [HttpPut("update-many")]
        public async Task<IActionResult> UpdateMany([FromBody] IEnumerable<UpdateRoleDTO> objs)
        {
            try
            {
                var response = await _roleService.UpdateListAsync(objs);
                return Ok(response);
            }
            catch (Exception e)
            {
                _logger.LogError(e, "Failed to update  list role");
                return BadRequest();
            }
        }
    }
}
