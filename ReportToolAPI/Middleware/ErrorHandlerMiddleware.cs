namespace ReportToolAPI.Middleware;

using ReportToolAPI.Exceptions;
using System.Net;
using System.Text.Json;

public class ErrorHandlerMiddleware
{
    private readonly RequestDelegate _next;

    public ErrorHandlerMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    public async Task Invoke(HttpContext context)
    {
        try
        {
            await _next(context);
        }
        catch (Exception error)
        {
            var response = context.Response;
            response.ContentType = "application/json";

            string? message = null;

            switch (error)
            {
                case NullEntityException e:
                    message = "Cannot process empty entity";
                    response.StatusCode = (int)HttpStatusCode.BadRequest;
                    break;

                case EntityExistException e:
                    message = "Entity already exists";
                    response.StatusCode = (int)HttpStatusCode.UnprocessableEntity;
                    break;

                default:
                    response.StatusCode = (int)HttpStatusCode.InternalServerError;
                    break;
            }

            var result = JsonSerializer.Serialize(new { message = message ?? error?.Message });
            await response.WriteAsync(result);
        }
    }
}