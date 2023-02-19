namespace Storage.Infrastructure.Configuration;

public static class InfrastructureConfiguration
{
    public static void SetConfiguration(IServiceCollection services)
    {
        SetServices(services);
    }
    private static void SetServices(IServiceCollection services)
    {
        services.AddAutoMapper(typeof(ApiMapProfile));
        services.AddScoped<IValidator<AuthorDto>, AuthorDtoValid>();
        services.AddScoped<IValidator<TopicDto>, TopicDtoValid>();
        services.AddScoped<IValidator<BookDto>, BookDtoValid>();
        services.AddScoped<IApiService<Book, BookDto>, BookService>();
        services.AddScoped<IApiService<Author, AuthorDto>, AuthorService>();
        services.AddScoped<IApiService<Topic, TopicDto>, TopicService>();
    }
}
