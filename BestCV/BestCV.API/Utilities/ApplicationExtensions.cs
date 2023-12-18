
using BestCV.Application.Utilities.SignalRs.Hubs;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.StaticFiles;
using Serilog;

namespace BestCV.API.Utilities
{
    public static class ApplicationExtensions
    {
        public static void UseInfrastructure(this WebApplication app)
        {
            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }
            app.MapHub<CandidateNotificationHub>("/CandidateNotificationHub");
            app.MapHub<EmployerNotificationHub>("/EmployerNotificationHub");
            app.UseHttpsRedirection();
            app.UseCors("Default");
            app.UseAuthentication();
            app.UseAuthorization();          
            app.UseSerilogRequestLogging();
            app.MapControllers();

            app.Run();
        }
    }
}
