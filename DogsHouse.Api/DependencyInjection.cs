using Serilog;
using Serilog.Core;
using Serilog.Events;

namespace DogsHouse.Api
{
    public static class DependencyInjection
    {
        public static WebApplicationBuilder AddPresentationServices(this WebApplicationBuilder builder) 
        {
            builder.Host.UseSerilog((hostContext, services, configuration) =>
            {
                configuration.WriteTo.File("log/log.txt");
                configuration.MinimumLevel.Information();
                configuration.MinimumLevel.Override("Microsoft", new LoggingLevelSwitch(LogEventLevel.Error));
                configuration.MinimumLevel.Override("System", new LoggingLevelSwitch(LogEventLevel.Error));
            });

            builder.Services.AddControllers();

            builder.Services.AddSwaggerGen();

            return builder;
        }
    }
}
