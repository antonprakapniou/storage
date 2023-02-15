﻿using ILogger = Serilog.ILogger;

namespace Storage.Api.Configuration;

public static class ApiConfiguration
{
    public static ILogger SetLogger(IConfiguration configuration, ILoggingBuilder logging)
    {
        var logger = new LoggerConfiguration()
            .ReadFrom.Configuration(configuration)
            .CreateLogger();
        logging.ClearProviders();
        logging.AddSerilog(logger);

        return logger;
    }
    public static void SetDbContext(IConfiguration configuration, IServiceCollection services)
    {
        string connectionName = Connections.SqliteDevelopment;
        string connectionString = configuration
            .GetConnectionString(connectionName!)
            ??throw new InvalidOperationException($"Connection \"{connectionName}\" not found");

        services.AddDbContext<ApiDbContext>(options =>
        {
            options.UseSqlite(connectionString);
            options.EnableSensitiveDataLogging();
        });
    }
    public static void SetServices(IServiceCollection services) 
    {
        services.AddControllers();
        services.AddEndpointsApiExplorer();
        services.AddSwaggerGen();
        services.AddTransient<ExceptionHandlingMiddleware>();
        services.AddAutoMapper(typeof(ApiMapProfile));
        services.AddScoped<IValidator<AuthorDto>, AuthorDtoValid>();
        services.AddScoped<IValidator<TopicDto>, TopicDtoValid>();
        services.AddScoped<IValidator<BookDto>, BookDtoValid>();
        services.AddScoped(typeof(IApiRepository<>),typeof(ApiRepository<>));
        services.AddScoped(typeof(IApiService<,>),typeof(ApiService<,>));
        services.AddScoped<IApiService<Book,BookDto>,BookService>();
    }
    public static void SetMiddleware(WebApplication app)
    {
        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
        }

        app.UseMiddleware<ExceptionHandlingMiddleware>();
        app.UseHttpsRedirection();
        app.UseAuthorization();
        app.MapControllers();
        app.Run();
    }
}
