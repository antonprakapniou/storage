namespace Storage.Api.Utilities;

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
            _logger.LogError(exception.Message);
            await ExceptionHandleAsync(context, exception);
        }
    }

    private static Task ExceptionHandleAsync(HttpContext context,Exception exception)
    {
        ErrorDetails details = new();
        var response = context.Response;
        response.ContentType= "application/json";

        switch (exception)
        {
            case InvalidValueException ex:
                response.StatusCode=(int)HttpStatusCode.UnprocessableEntity;
                details.Message=ex.Message;
                details.StackTrace=ex.StackTrace!;
                break;

            case ModelNotFoundException ex:
                response.StatusCode=(int)HttpStatusCode.NotFound;
                details.Message=ex.Message;
                details.StackTrace=ex.StackTrace!;
                break;

            case InvalidRemovalException ex:
                response.StatusCode=(int)HttpStatusCode.Forbidden;
                details.Message=ex.Message;
                details.StackTrace=ex.StackTrace!;
                break;

            default:
                response.StatusCode=(int)HttpStatusCode.InternalServerError;
                details.Message=exception.Message;
                details.StackTrace=exception.StackTrace!;
                break;
        }

        return response.WriteAsync(details.ToString());
    }
}
