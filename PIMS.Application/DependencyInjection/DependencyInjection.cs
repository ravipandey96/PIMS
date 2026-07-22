using Microsoft.Extensions.DependencyInjection;
using PIMS.Application.Interfaces.Services;
using PIMS.Application.Services;
namespace PIMS.Application.DependencyInjection;

/// <summary>
/// Provides dependency injection registration
/// for the Application layer.
/// </summary>
public static class DependencyInjection
{
    /// <summary>
    /// Registers application layer services.
    /// </summary>
    /// <param name="services">
    /// The service collection.
    /// </param>
    /// <returns>
    /// The updated service collection.
    /// </returns>
    public static IServiceCollection AddApplication(
        this IServiceCollection services)
    {
        // =====================================================
        // Register Application Services here
        //
        // Examples:
        // - MediatR (Future)
        // - FluentValidation (Future)
        // - AutoMapper (Future)
        // =====================================================
        services.AddScoped<ICategoryService, CategoryService>();
        return services;
    }
}