namespace Storage.Core.Configuration;

public static class DataConfiguration
{
    public static void SetConfiguration(IConfiguration configuration, IServiceCollection services)
    {
        SetDbContext(configuration, services);
        SetServices(services);
    }
    private static void SetDbContext(IConfiguration configuration, IServiceCollection services)
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
    private static void SetServices(IServiceCollection services)
    {
        services.AddScoped(typeof(IApiRepository<>), typeof(ApiRepository<>));
    }
}
