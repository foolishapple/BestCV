using BestCV.Core.Entities;
using BestCV.Domain.Constants;
using BestCV.Infrastructure.Repositories.Interfaces;
using MathNet.Numerics.Distributions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Caching.Memory;
using Newtonsoft.Json;

namespace BestCV.Admin.Controllers
{
    public class BaseController : Controller
    {
        private IConfiguration configuration;

        public override async Task OnActionExecutionAsync(ActionExecutingContext filterContext, ActionExecutionDelegate next)
        {
            //Auto clear token
            if (!User.Identity.IsAuthenticated)
            {
                filterContext.HttpContext.Response.Cookies.Append("Authorization", "");
            }
            configuration = filterContext.HttpContext.RequestServices.GetService<IConfiguration>();
            var defaultAPIURL = configuration["DefaultAPIURL"];
            string defaultStorageURL = configuration["DefaultStorageURL"];
            var webURL = configuration["WebURL"];
            ViewBag.defaultStorageURL = defaultStorageURL;
			ViewBag.defaultAPIURL = defaultAPIURL;
            ViewBag.defaultWebUrl = webURL;
            var cache = filterContext.HttpContext.RequestServices.GetService<IMemoryCache>();

            //Menu Admin
            var systemMenuAdmin = filterContext.HttpContext.RequestServices.GetService<IMenuRepository>();
            var listMenuAdmin = await cache.GetOrCreateAsync("listMenuAdmin",
                async cacheEntry =>
                {
                    cacheEntry.SetAbsoluteExpiration(TimeSpan.FromMinutes(20));
                    return await systemMenuAdmin.GetAllAsync();
                });

            ViewBag.MenuAdmin = JsonConvert.SerializeObject(listMenuAdmin.Where(x => x.Active && x.MenuTypeId == MenuConstant.DEFAULT_VALUE_MENU_ADMIN));

            await base.OnActionExecutionAsync(filterContext, next);
        }
    }
}
