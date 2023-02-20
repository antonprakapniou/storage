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
        services.AddSwaggerGen(options =>
        {
            options.SwaggerDoc("v1", new OpenApiInfo
            {
                Version = "v1",
                Title = "Storage Api",
                Description = "Storage Api on the example of books based on .Net 7",
                TermsOfService = new Uri("https://example.com/terms"),
                Contact = new OpenApiContact
                {
                    Name = "Example Contact",
                    Url = new Uri("https://example.com/contact")
                },
                License = new OpenApiLicense
                {
                    Name = "Example License",
                    Url = new Uri("https://example.com/license")
                }
            });

            var xmlFilename = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
            options.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, xmlFilename));
            options.EnableAnnotations();
        });

        services.AddTransient<ExceptionHandlerMiddleware>();
    }
}
