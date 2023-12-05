using Hangfire;
using Microsoft.AspNetCore.Http.Features;
using Microsoft.AspNetCore.Server.Kestrel.Core;
using BestCV.Application.Services.Implement;
using BestCV.Application.Services.Interfaces;
using BestCV.Core.Entities;
using BestCV.Core.Services;
using BestCV.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using BestCV.ScheduledJob.Utilities;
using BestCV.Infrastructure.Repositories.Interfaces;
using BestCV.Infrastructure.Repositories.Implement;
using BestCV.Core.Repositories;

namespace BestCV.ScheduledJob.Extensions
{
    public static class ServiceExtensions
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            services.AddEndpointsApiExplorer();
            services.AddSwaggerGen();

            var hangFireSettings = configuration.GetSection("HangfireSettings").Get<HangfireSettings>();

            services.AddHangfire(options => {
                options.SetDataCompatibilityLevel(CompatibilityLevel.Version_180)
                .UseSimpleAssemblyNameTypeSerializer()
                .UseRecommendedSerializerSettings()
                .UseSqlServerStorage(hangFireSettings.ConnectionString);
            }
                ) ;
            services.AddHangfireServer(options => options.ServerName = hangFireSettings.ServerName);
            var smtpSettings = configuration.GetSection("SmtpSettings").Get<SmtpSettings>();
            services.AddSingleton(smtpSettings);
            services.AddDbContext<JobiContext>(options => options.UseSqlServer(configuration.GetConnectionString("DefaultConnectionString")));
            services.AddTransient<IEmailService, EmailService>();
            services.AddTransient<IScheduledJobService, ScheduledJobService>();
            services.AddTransient<ICronJobService, CronJobService>();
            services.AddTransient<ITopFeatureJobRepository, TopFeatureJobRepository>();
            services.AddTransient<ITopAreaJobRepository, TopAreaJobRepository>();
            services.AddHostedService<CronBackgroundService>();
            services.AddTransient(typeof(IUnitOfWork<>), typeof(UnitOfWork<>));
            services.AddTransient<IRefreshJobRepository, RefreshJobRepository>();
            return services;
        }
    }
}
