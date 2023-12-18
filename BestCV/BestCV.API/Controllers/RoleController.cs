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
