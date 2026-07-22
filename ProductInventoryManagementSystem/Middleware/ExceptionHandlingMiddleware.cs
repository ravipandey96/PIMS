using System.Net;
using System.Text.Json;
using PIMS.Application.Common.Exceptions;
using PIMS.Application.Common.Models;

namespace PIMS.API.Middleware;

/// <summary>
/// Middleware that catches unhandled exceptions and returns
/// standardized JSON error responses.
/// </summary>
public sealed class ExceptionHandlingMiddleware
{
    private readonly RequestDelegate _next;

    /// <summary>
    /// Initializes a new instance of the
    /// <see cref="ExceptionHandlingMiddleware"/> class.
    /// </summary>
    /// <param name="next">
    /// The next middleware in the pipeline.
    /// </param>
    public ExceptionHandlingMiddleware(
        RequestDelegate next)
    {
        _next = next;
    }

    /// <summary>
    /// Processes the current HTTP request.
    /// </summary>
    /// <param name="context">
    /// The HTTP context.
    /// </param>
    public async Task InvokeAsync(
        HttpContext context)
    {
        try
        {
            await _next(context);
        }
        catch (Exception exception)
        {
            await HandleExceptionAsync(
                context,
                exception);
        }
    }

    /// <summary>
    /// Handles application exceptions and converts them
    /// into standardized API responses.
    /// </summary>
    /// <param name="context">
    /// HTTP context.
    /// </param>
    /// <param name="exception">
    /// The exception that occurred.
    /// </param>
    private static async Task HandleExceptionAsync(
        HttpContext context,
        Exception exception)
    {
        context.Response.ContentType = "application/json";

        HttpStatusCode statusCode;
        string message;
        IEnumerable<string>? errors = null;

        switch (exception)
        {
            case ValidationException validationException:
                statusCode = HttpStatusCode.BadRequest;
                message = validationException.Message;
                break;

            case UnauthorizedException unauthorizedException:
                statusCode = HttpStatusCode.Unauthorized;
                message = unauthorizedException.Message;
                break;

            case NotFoundException notFoundException:
                statusCode = HttpStatusCode.NotFound;
                message = notFoundException.Message;
                break;

            default:
                statusCode = HttpStatusCode.InternalServerError;
                message = "An unexpected error occurred.";
                break;
        }

        context.Response.StatusCode = (int)statusCode;

        var response = new ApiErrorResponse
        {
            Success = false,
            StatusCode = (int)statusCode,
            Message = message,
            Errors = errors,
            TimestampUtc = DateTime.UtcNow
        };

        var json = JsonSerializer.Serialize(
            response,
            new JsonSerializerOptions
            {
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
                WriteIndented = false
            });

        await context.Response.WriteAsync(json);
    }
}