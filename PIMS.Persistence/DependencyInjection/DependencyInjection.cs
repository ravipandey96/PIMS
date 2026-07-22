using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using PIMS.Application.Interfaces.Persistence;
using PIMS.Persistence.Context;
using PIMS.Persistence.Repositories;

namespace PIMS.Persistence.DependencyInjection;

/// <summary>
/// Provides extension methods for registering
/// persistence layer services.
/// </summary>
public static class DependencyInjection
{
    /// <summary>
    /// Registers persistence services.
    /// </summary>
    /// <param name="services">
    /// Service collection.
    /// </param>
    /// <param name="configuration">
    /// Application configuration.
    /// </param>
    /// <returns>
    /// Updated service collection.
    /// </returns>
    public static IServiceCollection AddPersistence(
        this IServiceCollection services,
        IConfiguration configuration)
    {
        // =========================================================
        // Database Context
        // =========================================================

        services.AddDbContext<PimsDbContext>(options =>
        {
            options.UseMySql(
                configuration.GetConnectionString("DefaultConnection"),
                ServerVersion.AutoDetect(
                    configuration.GetConnectionString("DefaultConnection")));
        });



        // =========================================================
        // Unit of Work
        // =========================================================

        services.AddScoped<IUnitOfWork, UnitOfWork>();



        // =========================================================
        // Repository Registrations
        // =========================================================
        //
        // These registrations are optional because the application
        // accesses repositories through IUnitOfWork.
        // They are useful if a repository needs to be injected
        // directly in the future.
        //

        services.AddScoped<IUserRepository, UserRepository>();

        services.AddScoped<IRoleRepository, RoleRepository>();

        services.AddScoped<IProductRepository, ProductRepository>();

        services.AddScoped<ICategoryRepository, CategoryRepository>();

        services.AddScoped<IInventoryRepository, InventoryRepository>();

        services.AddScoped<IRefreshTokenRepository,
            RefreshTokenRepository>();



        return services;
    }
}