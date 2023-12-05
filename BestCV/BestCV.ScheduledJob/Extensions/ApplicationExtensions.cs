using Hangfire;
using Microsoft.AspNetCore.StaticFiles;
using BestCV.Core.Entities;
using Serilog;

namespace BestCV.ScheduledJob.Extensions
{
    public static class ApplicationExtensions
    {
        public static void UseInfrastructure(this WebApplication app,IConfiguration configuration)
        {
            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseAuthorization();

            var configDashboard = configuration.GetSection("HangfireSettings:Dashboard").Get<Dashboard>();
            var hangfireSettings = configuration.GetSection("HangfireSettings").Get<HangfireSettings>();
            var hangfireRoute = hangfireSettings.Route;
            app.UseHangfireDashboard(hangfireRoute,new DashboardOptions
            {
                DashboardTitle = configDashboard.DashboardTitle,
                StatsPollingInterval= configDashboard.StatsPollingInterval,
                AppPath=configDashboard.AppPath,
                IgnoreAntiforgeryToken=true

            });
            app.MapControllers();

            app.Run();
        }
    }
}
