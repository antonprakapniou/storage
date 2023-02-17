﻿namespace Storage.Api.Utilities;

public class ExceptionHandlerMiddleware : IMiddleware
{
    private readonly ILogger<ExceptionHandlerMiddleware> _logger;

    public ExceptionHandlerMiddleware(ILogger<ExceptionHandlerMiddleware> logger)
    {
        _logger= logger;
    }

    public async Task InvokeAsync(HttpContext context, RequestDelegate next)
    {
        try
        {
            await next(context);
        }

        catch (Exception exception)
        {
            _logger.LogError(message: exception.Message);
            await ExceptionHandleAsync(context, exception);
        }
    }

    private static Task ExceptionHandleAsync(HttpContext context, Exception exception)
    {
        ErrorDetails details = new();
        var response = context.Response;
        response.ContentType= "application/json";

        switch (exception)
        {
            case InvalidValueException:
                response.StatusCode=(int)HttpStatusCode.UnprocessableEntity;
                break;

            case ModelNotFoundException:
                response.StatusCode=(int)HttpStatusCode.NotFound;
                break;

            case InvalidUpdatingException:
                response.StatusCode=(int)HttpStatusCode.Forbidden;
                break;

            case InvalidRemovinglException:
                response.StatusCode=(int)HttpStatusCode.Forbidden;
                break;

            default:
                response.StatusCode=(int)HttpStatusCode.InternalServerError;
                break;
        }

        details.Message = exception.Message;
        details.StackTrace=exception.StackTrace!;
        return response.WriteAsync(details.ToString());
    }
}
