using BestCV.Domain.Constants;
using BestCV.Infrastructure.Repositories.Interfaces;
using MathNet.Numerics.Distributions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Caching.Memory;
using Newtonsoft.Json;

namespace BestCV.Employer.Controllers
{
    public class BaseController : Controller
    {
        private IConfiguration _configuration;

        public override async Task OnActionExecutionAsync(ActionExecutingContext filterContext, ActionExecutionDelegate next)
        {
            _configuration = filterContext.HttpContext.RequestServices.GetService<IConfiguration>();
            ViewBag.DefaultAPIURL = _configuration["DefaultAPIURL"];
            ViewBag.DefaultStorageURL = _configuration["DefaultStorageURL"];
            ViewBag.DefaultWebURL = _configuration["DefaultWebURL"];

            var cache = filterContext.HttpContext.RequestServices.GetService<IMemoryCache>();

            //Menu Dashboard Employer
            var systemMenuDashboardEmployer = filterContext.HttpContext.RequestServices.GetService<IMenuRepository>();
            var listMenuDashboardEmployer = await cache.GetOrCreateAsync("listMenuDashboardEmployer",
                async cacheEntry =>
                {
                    cacheEntry.SetAbsoluteExpiration(TimeSpan.FromMinutes(20));
                    return await systemMenuDashboardEmployer.GetAllAsync();
                });

            ViewBag.MenuDashBoardEmployer = JsonConvert.SerializeObject(listMenuDashboardEmployer.Where(x => x.Active && x.MenuTypeId == MenuConstant.DEFAULT_VALUE_DASHBOARD_EMPLOYER));
           
            await base.OnActionExecutionAsync(filterContext, next);
        }
    }
}
