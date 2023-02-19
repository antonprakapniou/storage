var builder = WebApplication.CreateBuilder(args);
var logger = ApiConfiguration.SetConfiguration(builder.Configuration, builder.Services,builder.Logging);
var app = builder.Build();

ApiConfiguration.SetMiddleware(app);

logger.Information("Application started");

app.Run();