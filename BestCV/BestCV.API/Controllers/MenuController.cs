using BestCV.Application.Models.JobSkill;
using BestCV.Application.Models.Menu;
using BestCV.Application.Services.Interfaces;
using BestCV.Core.Entities;
using BestCV.Domain.Constants;
using BestCV.Domain.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Caching.Memory;
using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;

namespace BestCV.API.Controllers
{
    [Route("api/menu")]
    [ApiController]
    public class MenuController : BaseController
    {
        private readonly IMenuService service;
        private readonly ILogger<MenuController> logger;
        private readonly IMemoryCache cache;
        public MenuController(IMenuService _service, ILoggerFactory loggerFactory, IMemoryCache _cache)
        {
            service = _service;
            logger = loggerFactory.CreateLogger<MenuController>();
            cache = _cache;
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
                logger.LogError(ex, $"Failed to get menu by id: {id}");
                return BadRequest();
            }
        }


        [HttpPost("add")]
        public async Task<IActionResult> Add([FromBody] InsertMenuDTO model)
        {
            try
            {
                var data = await service.CreateAsync(model);

                if(model.MenuTypeId == MenuConstant.DEFAULT_VALUE_MENU_ADMIN) {
                    var cacheKey = "listMenuAdmin";
                    cache.Remove(cacheKey);
                }else if(model.MenuTypeId == MenuConstant.DEFAULT_VALUE_DASHBOARD_CANDIDATE)
                {
                    var cacheKey = "listMenuDashboardCandidate";
                    cache.Remove(cacheKey);
                }else if(model.MenuTypeId == MenuConstant.DEFAULT_VALUE_DASHBOARD_EMPLOYER)
                {
                    var cacheKey = "listMenuDashboardEmployer";
                    cache.Remove(cacheKey);
                }else if (model.MenuTypeId == MenuConstant.DEFAULT_VALUE_HEADER_CANDIDATE)
                {
                    var cacheKey = "listMenuHeaderCandidate";
                    cache.Remove(cacheKey);
                }
                
                return Ok(data);
            }
            catch (Exception ex)
            {
                logger.LogError(ex, $"Failed to add menu");
                return BadRequest();
            }
        }


        [HttpPut("update")]
        public async Task<IActionResult> Update([FromBody] UpdateMenuDTO model)
        {
            try
            {
                var data = await service.UpdateAsync(model);

                if (model.MenuTypeId == MenuConstant.DEFAULT_VALUE_MENU_ADMIN)
                {
                    var cacheKey = "listMenuAdmin";
                    cache.Remove(cacheKey);
                }
                else if (model.MenuTypeId == MenuConstant.DEFAULT_VALUE_DASHBOARD_CANDIDATE)
                {
                    var cacheKey = "listMenuDashboardCandidate";
                    cache.Remove(cacheKey);
                }
                else if (model.MenuTypeId == MenuConstant.DEFAULT_VALUE_DASHBOARD_EMPLOYER)
                {
                    var cacheKey = "listMenuDashboardEmployer";
                    cache.Remove(cacheKey);
                }else if (model.MenuTypeId == MenuConstant.DEFAULT_VALUE_HEADER_CANDIDATE)
                {
                    var cacheKey = "listMenuHeaderCandidate";
                    cache.Remove(cacheKey);
                }

                return Ok(data);
            }
            catch (Exception ex)
            {
                logger.LogError(ex, $"Failed to update menu");
                return BadRequest();
            }
        }


        [HttpDelete("delete")]
        public async Task<IActionResult> Delete([Required] int id)
        {
            try
            {
                var detailMenuType = await service.GetByIdAsync(id);
                var response = (MenuDTO)detailMenuType.Resources;

                var data = await service.SoftDeleteAsync(id);

                if (response.MenuTypeId == MenuConstant.DEFAULT_VALUE_MENU_ADMIN)
                {
                    var cacheKey = "listMenuAdmin";
                    cache.Remove(cacheKey);
                }
                else if (response.MenuTypeId == MenuConstant.DEFAULT_VALUE_DASHBOARD_CANDIDATE)
                {
                    var cacheKey = "listMenuDashboardCandidate";
                    cache.Remove(cacheKey);
                }
                else if (response.MenuTypeId == MenuConstant.DEFAULT_VALUE_DASHBOARD_EMPLOYER)
                {
                    var cacheKey = "listMenuDashboardEmployer";
                    cache.Remove(cacheKey);
                }else if(response.MenuTypeId == MenuConstant.DEFAULT_VALUE_HEADER_CANDIDATE)
                {
                    var cacheKey = "listMenuHeaderCandidate";
                    cache.Remove(cacheKey);
                }

                return Ok(data);
            }
            catch (Exception ex)
            {
                logger.LogError(ex, $"Failed to soft delete menu");
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
                logger.LogError(ex, $"Failed to get list menu");
                return BadRequest();
            }
        }

        [HttpGet("list-all-menu-admin")]
        public async Task<IActionResult> GetListMenuAdmin()
        {
            try
            {
                
                var listMenuAdmin = await cache.GetOrCreateAsync("listMenuAdmin",
                 async cacheEntry =>
                 {
                     cacheEntry.SetAbsoluteExpiration(TimeSpan.FromMinutes(20));
                     return JsonConvert.SerializeObject(await service.GetAllMenuAdmin());
                 });

                var data = JsonConvert.DeserializeObject(listMenuAdmin);

                return Ok(data);
            }
            catch (Exception ex)
            {
                logger.LogError(ex, $"Failed to get list menu");
                return BadRequest();
            }
        }

        [HttpGet("list-menu-admin")]
        public async Task<IActionResult> GetListMenePageAdmin()
        {
            try
            {

                var listMenuAdmin = await service.GetAllMenuAdmin();

                return Ok(listMenuAdmin);
            }
            catch (Exception ex)
            {
                logger.LogError(ex, $"Failed to get list menu");
                return BadRequest();
            }
        }


        [HttpGet("list-all-employer")]
        public async Task<IActionResult> GetListMenuDashboardEmployer()
        {
            try
            {
                var listMenuDashboardEmployer = await cache.GetOrCreateAsync("listMenuDashboardEmployer",
                 async cacheEntry =>
                 {
                     cacheEntry.SetAbsoluteExpiration(TimeSpan.FromMinutes(20));
                     return JsonConvert.SerializeObject(await service.GetAllMenuDashboardEmployer());
                 });
                var data = JsonConvert.DeserializeObject(listMenuDashboardEmployer);
                return Ok(data);
            }
            catch (Exception ex)
            {
                logger.LogError(ex, $"Failed to get list menu");
                return BadRequest();
            }
        }


        [HttpGet("list-all-candidate")]
        public async Task<IActionResult> GetListMenuDashboardCandidate()
        {
            try
            {
                var listMenuDashboardCandidate = await cache.GetOrCreateAsync("listMenuDashboardCandidate",
                 async cacheEntry =>
                 {
                     cacheEntry.SetAbsoluteExpiration(TimeSpan.FromMinutes(20));
                     return JsonConvert.SerializeObject(await service.GetAllMenuDashboardCandidate());
                 });
                var data = JsonConvert.DeserializeObject(listMenuDashboardCandidate);
                return Ok(data);
            }
            catch (Exception ex)
            {
                logger.LogError(ex, $"Failed to get list menu");
                return BadRequest();
            }
        }


        [HttpGet("list-all-header-candidate")]
        public async Task<IActionResult> GetListMenuHeaderCandidate()
        {
            try
            {
                var listMenuHeaderCandidate = await cache.GetOrCreateAsync("listHeaderdCandidate",
                 async cacheEntry =>
                 {
                     cacheEntry.SetAbsoluteExpiration(TimeSpan.FromMinutes(20));
                     return JsonConvert.SerializeObject(await service.GetAllMenuHeaderCandidate());
                 });

                var data = JsonConvert.DeserializeObject(listMenuHeaderCandidate);

                return Ok(data);
            }
            catch (Exception ex)
            {
                logger.LogError(ex, $"Failed to get list menu");
                return BadRequest();
            }
        }

        [HttpGet("list-menu")]
        public async Task<IActionResult> GetListMenu()
        {
            try
            {
                var data = await service.GetListMenuHomepage();
                return Ok(data);
            }
            catch (Exception ex)
            {
                logger.LogError(ex, $"Failed to get list menu");
                return BadRequest();
            }
        }
        #endregion


        #region Additional Resources

        #endregion
    }
}
