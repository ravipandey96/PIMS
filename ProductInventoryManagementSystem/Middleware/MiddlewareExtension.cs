namespace PIMS.API.Middleware;

/// <summary>
/// Extension methods for registering middleware.
/// </summary>
public static class MiddlewareExtensions
{
    /// <summary>
    /// Adds global exception handling middleware.
    /// </summary>
    public static IApplicationBuilder UseGlobalExceptionHandling(
        this IApplicationBuilder app)
    {
        return app.UseMiddleware<ExceptionHandlingMiddleware>();
    }
}