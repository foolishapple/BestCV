using BestCV.ScheduledJob.Extensions;
using Serilog;

var builder = WebApplication.CreateBuilder(args);

// Add seri log
Log.Logger = new LoggerConfiguration()
                .MinimumLevel.Override("Microsoft", Serilog.Events.LogEventLevel.Information)
                .Enrich.FromLogContext()
                .WriteTo.File("Logs/log.txt", rollingInterval: RollingInterval.Day, outputTemplate: "{Timestamp:yyyy-MM-dd HH:mm:ss.fff zzz} [{Level:u3}] {SourceContext}{NewLine}{Message:lj}{NewLine}{Exception}{NewLine}")
                .WriteTo.Console(outputTemplate: "{Timestamp:yyyy-MM-dd HH:mm:ss.fff zzz} [{Level:u3}] {SourceContext}{NewLine}{Message:lj}{NewLine}{Exception}{NewLine}")
                .CreateLogger();
Log.Information($"Start {builder.Environment.ApplicationName} up.");
try
{
    // Add services to the container.

    builder.Services.AddInfrastructure(builder.Configuration);

    var app = builder.Build();

    app.UseInfrastructure(builder.Configuration);
}
catch (Exception ex)
{
    string type = ex.GetType().Name;
    if (type.Equals("StopTheHostException", StringComparison.Ordinal)) throw;
    Log.Fatal(ex, $"Unhanded exception: {ex.Message}");
}
finally
{
    Log.Information($"Shut down {builder.Environment.ApplicationName} complete.");
    Log.CloseAndFlush();
}

