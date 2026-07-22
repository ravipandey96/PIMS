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
    /// Service collection.
    /// </param>
    /// <returns>
    /// Updated service collection.
    /// </returns>
    public static IServiceCollection AddApplication(
        this IServiceCollection services)
    {
        // ============================================
        // Business Services
        // ============================================

        services.AddScoped<ICategoryService, CategoryService>();

        // ============================================
        // Future Registrations
        // ============================================

        // services.AddScoped<IProductService, ProductService>();
        // services.AddScoped<IInventoryService, InventoryService>();
        // services.AddScoped<IUserService, UserService>();

        // ============================================
        // Future Libraries
        // ============================================

        // services.AddAutoMapper(...);
        // services.AddMediatR(...);
        // services.AddValidatorsFromAssembly(...);

        return services;
    }
}