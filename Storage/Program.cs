var builder = WebApplication.CreateBuilder(args);
var logger = ApiConfiguration.SetLogger(builder.Configuration, builder.Logging);

ApiConfiguration.SetDbContext(builder.Configuration, builder.Services);
ApiConfiguration.SetServices(builder.Services);

var app = builder.Build();

ApiConfiguration.SetMiddleware(app);

logger.Information("Application started");

app.Run();