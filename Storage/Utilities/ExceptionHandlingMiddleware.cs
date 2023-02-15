﻿namespace Storage.Api.Utilities;

public class ExceptionHandlingMiddleware : IMiddleware
{
    private readonly ILogger<ExceptionHandlingMiddleware> _logger;

    public ExceptionHandlingMiddleware(ILogger<ExceptionHandlingMiddleware> logger)
    {
        _logger= logger;
    }

    public async Task InvokeAsync(HttpContext context, RequestDelegate next)
    {
        try
        {
            await next(context);
        }

        catch (Exception ex)
        {
            context.Response.StatusCode=(int)HttpStatusCode.BadRequest;
            _logger.LogError($"{context.GetEndpoint()} {ex.Message}");
            await context.Response.WriteAsync(ex.Message);
        }
    }
}
