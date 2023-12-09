using Microsoft.AspNetCore.Http.Features;
using Microsoft.AspNetCore.Server.Kestrel.Core;

namespace BestCV.Employer.Utilities
{
    public static class ServiceExtensions
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services)
        {
            services.AddControllersWithViews()
                .ConfigureApiBehaviorOptions(opt =>
                {
                    opt.SuppressModelStateInvalidFilter = true;
                })
                .AddSessionStateTempDataProvider();
            
            services.Configure<RouteOptions>(options =>
            {
                options.LowercaseUrls = true;
                options.LowercaseQueryStrings = true;
            });
            services.AddSignalR();

            services.AddDistributedMemoryCache();
            services.AddSession(options =>
             {
                 options.IdleTimeout = TimeSpan.FromMinutes(60);
                 options.Cookie.IsEssential = true;
                 options.Cookie.HttpOnly = true;
                 options.Cookie.Domain = "BestCV Employer";
             });
            services.AddHttpContextAccessor();
            return services;
        }
    }
}
