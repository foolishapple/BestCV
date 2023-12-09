using BestCV.Domain.Constants;
using BestCV.Infrastructure.Repositories.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Caching.Memory;
using Newtonsoft.Json;

namespace BestCV.Web.Controllers
{
    public class BaseController : Controller
    {
        private IConfiguration configuration;

        public override async Task OnActionExecutionAsync(ActionExecutingContext filterContext, ActionExecutionDelegate next)
        {
            configuration = filterContext.HttpContext.RequestServices.GetService<IConfiguration>();
            var defaultAPIURL = configuration["DefaultAPIURL"];
            var defaultStorageURL = configuration["DefaultStorageURL"];
            var defaultURL = configuration["DefaultURL"];
            ViewBag.defaultStorageURL = defaultStorageURL;
            ViewBag.defaultAPIURL = defaultAPIURL;
            ViewBag.defaultURL = defaultURL;
            ViewBag.DefaultStorageURL = configuration["DefaultStorageURL"];
            ViewBag.IsHomePage = true;

            var cache = filterContext.HttpContext.RequestServices.GetService<IMemoryCache>();
            var systemConfigService = filterContext.HttpContext.RequestServices.GetService<ISystemConfigRepository>();
            var listSystemConfigs = await cache.GetOrCreateAsync("listSystemConfigs",
                async cacheEntry =>
                {
                    cacheEntry.SetAbsoluteExpiration(TimeSpan.FromMinutes(20));
                    return await systemConfigService.GetAllAsync();
                });

            ViewBag.Phone = listSystemConfigs.FirstOrDefault(x => x.Active && x.Key == SystemConfigConstant.PHONE_KEY)?.Value;
            ViewBag.Email = listSystemConfigs.FirstOrDefault(x => x.Active && x.Key == SystemConfigConstant.EMAIL_KEY)?.Value;
            ViewBag.Address = listSystemConfigs.FirstOrDefault(x => x.Active && x.Key == SystemConfigConstant.ADDRESS_KEY)?.Value;
            ViewBag.Address2 = listSystemConfigs.FirstOrDefault(x => x.Active && x.Key == SystemConfigConstant.ADDRESS_2)?.Value;
            ViewBag.Map = listSystemConfigs.FirstOrDefault(x => x.Active && x.Key == SystemConfigConstant.MAP_KEY)?.Value;
            ViewBag.Facebook = listSystemConfigs.FirstOrDefault(x => x.Active && x.Key == SystemConfigConstant.FACEBOOK_KEY)?.Value;
            ViewBag.Instagram = listSystemConfigs.FirstOrDefault(x => x.Active && x.Key == SystemConfigConstant.INSTAGRAM_KEY)?.Value;
            ViewBag.Linkedin = listSystemConfigs.FirstOrDefault(x => x.Active && x.Key == SystemConfigConstant.LINKEDIN_KEY)?.Value;
            ViewBag.Twitter = listSystemConfigs.FirstOrDefault(x => x.Active && x.Key == SystemConfigConstant.TWITTER_KEY)?.Value;
            //Ứng viên :
            ViewBag.Find_job = listSystemConfigs.FirstOrDefault(x => x.Active && x.Key == SystemConfigConstant.FIND_JOB_KEY)?.Value;
            ViewBag.Candidate_dashboard = listSystemConfigs.FirstOrDefault(x => x.Active && x.Key == SystemConfigConstant.CANDIDATE_DASHBOARD_KEY)?.Value;
            ViewBag.Favorite_job = listSystemConfigs.FirstOrDefault(x => x.Active && x.Key == SystemConfigConstant.FAVOURITE_JOB_KEY)?.Value;
            ViewBag.List_of_post_job = listSystemConfigs.FirstOrDefault(x => x.Active && x.Key == SystemConfigConstant.LIST_OF_POST_KEY)?.Value;

            //Nhà tuyển dụng:
            ViewBag.Company_information = listSystemConfigs.FirstOrDefault(x => x.Active && x.Key == SystemConfigConstant.COMPANY_INFORMATION_KEY)?.Value;
            ViewBag.Recruitment_campaign = listSystemConfigs.FirstOrDefault(x => x.Active && x.Key == SystemConfigConstant.RECRUITMENT_CAMPAIGN_KEY)?.Value;
            ViewBag.Candidate = listSystemConfigs.FirstOrDefault(x => x.Active && x.Key == SystemConfigConstant.CANDIDATE_KEY)?.Value;
            
            ViewBag.Contact = listSystemConfigs.FirstOrDefault(x => x.Active && x.Key == SystemConfigConstant.CONTACT_KEY)?.Value;

            //Menu Dashboard Candidate
            var systemMenuDashboardCandidate = filterContext.HttpContext.RequestServices.GetService<IMenuRepository>();
            var listMenuDashboardCandidate = await cache.GetOrCreateAsync("listMenuDashboardCandidate",
                async cacheEntry =>
                {
                    cacheEntry.SetAbsoluteExpiration(TimeSpan.FromMinutes(20));
                    return await systemMenuDashboardCandidate.GetAllAsync();
                });

            ViewBag.MenuDashBoardCandidate = JsonConvert.SerializeObject(listMenuDashboardCandidate.Where(x => x.Active && x.MenuTypeId == MenuConstant.DEFAULT_VALUE_DASHBOARD_CANDIDATE));

            

            //Header Candidate
            var systemHeaderdCandidate = filterContext.HttpContext.RequestServices.GetService<IMenuRepository>();
            var listHeaderdCandidate = await cache.GetOrCreateAsync("listHeaderdCandidate",
                async cacheEntry =>
                {
                    cacheEntry.SetAbsoluteExpiration(TimeSpan.FromMinutes(20));
                    return await systemHeaderdCandidate.GetAllAsync();
                });

            ViewBag.HeaderCandidate = JsonConvert.SerializeObject(listHeaderdCandidate.Where(x => x.Active && x.MenuTypeId == MenuConstant.DEFAULT_VALUE_HEADER_CANDIDATE));


            //Footer Candidate
            var systemFooterCandidate = filterContext.HttpContext.RequestServices.GetService<IMenuRepository>();
            var listFooterCandidate = await cache.GetOrCreateAsync("listFooterCandidate",
                async cacheEntry =>
                {
                    cacheEntry.SetAbsoluteExpiration(TimeSpan.FromMinutes(20));
                    return await systemFooterCandidate.GetAllAsync();
                });

            ViewBag.FooterCandidate = JsonConvert.SerializeObject(listFooterCandidate.Where(x => x.Active && x.MenuTypeId == MenuConstant.DEFAULT_VALUE_FOOTER_CANDIDATE));


            await base.OnActionExecutionAsync(filterContext, next);
        }
    }
}
