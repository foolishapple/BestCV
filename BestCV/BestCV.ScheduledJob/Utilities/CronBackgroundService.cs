using Hangfire;
using BestCV.Application.Services.Implement;
using BestCV.Application.Services.Interfaces;
using BestCV.Core.Entities;
using BestCV.Core.Utilities;
using Microsoft.Extensions.Logging;
using Org.BouncyCastle.Tls;

namespace BestCV.ScheduledJob.Utilities
{
    public class CronBackgroundService : BackgroundService
    {
        private readonly IServiceProvider _serviceProvider;
        private ICronJobService _cronJobService;
        private readonly ILogger _logger;
        private readonly IConfiguration _configuration;
        public CronBackgroundService(IServiceProvider serviceProvider, ILoggerFactory loggerFactory, IConfiguration configuration)
        {
            _serviceProvider = serviceProvider;
            _logger = loggerFactory.CreateLogger<CronBackgroundService>();
            _configuration = configuration;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            try
            {
                await RunCron();
                _logger.LogInformation("Cron is running.");
                await StopAsync(default);
                
            }
            catch (Exception e)
            {
                _logger.LogError(e, "Failed to run cron job");
                await Task.Delay(6000);
                await ExecuteAsync(stoppingToken);
            }
        }
        private async Task RunCron()
        {
            using (IServiceScope scope = _serviceProvider.CreateScope())
            {
                _cronJobService = scope.ServiceProvider.GetRequiredService<ICronJobService>();
                _cronJobService.DeleteRecurring(CronUtil.Benefit.HIGHEST_PRIORITY_POSITION);
                await _cronJobService.UpdateHighestPriorityPosition();
                _cronJobService.DeleteRecurring(CronUtil.Benefit.AREA_ON_TOP);
                await _cronJobService.UpdateTopArea();
                await _cronJobService.AddRefreshDaily();
            }
        }
        public override async Task StopAsync(CancellationToken stoppingToken)
        {
            _logger.LogInformation("Cron background is stopped.");
            await base.StopAsync(stoppingToken);
        }
        public void RestartServer()
        {
            HangfireSettings settings = _configuration.GetSection("HangfireSettings").Get<HangfireSettings>();
            var monitoringApi = JobStorage.Current.GetMonitoringApi();
            // Tìm server dựa trên tên
            var serverInfos = monitoringApi.Servers();
            using (var connection = JobStorage.Current.GetConnection())
            {
                foreach (var item in serverInfos)
                {
                    connection.RemoveServer(item.Name);
                }
            }
            //var backgroundJobServer = new BackgroundJobServer(options: new()
            //{
            //    ServerName = settings.ServerName
            //});
            _logger.LogInformation(settings.ServerName+"is restarted.");
        }
    }
}
