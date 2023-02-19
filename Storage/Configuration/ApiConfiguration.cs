using ILogger = Serilog.ILogger;

namespace Storage.Api.Configuration;

public static class ApiConfiguration
{
    public static ILogger SetConfiguration(IConfiguration configuration, IServiceCollection services, ILoggingBuilder logging)
    {
        DataConfiguration.SetConfiguration(configuration, services);
        InfrastructureConfiguration.SetConfiguration(services);
        SetApiServices(services);
        var logger = SetLogger(configuration,logging);
        return logger;
    }    
    public static void SetMiddleware(WebApplication app)
    {
        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
        }

        app.UseMiddleware<ExceptionHandlerMiddleware>();
        app.UseHttpsRedirection();
        app.UseAuthorization();
        app.MapControllers();
        app.Run();
    }
    private static ILogger SetLogger(IConfiguration configuration, ILoggingBuilder logging)
    {
        var logger = new LoggerConfiguration()
            .ReadFrom.Configuration(configuration)
            .CreateLogger();
        logging.ClearProviders();
        logging.AddSerilog(logger);

        return logger;
    }
    private static void SetApiServices(IServiceCollection services)
    {
        services.AddControllers();
        services.AddEndpointsApiExplorer();
        services.AddSwaggerGen();
        services.AddTransient<ExceptionHandlerMiddleware>();
    }
}
